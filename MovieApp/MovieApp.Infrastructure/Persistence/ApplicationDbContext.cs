using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieApp.SharedKernel.Entitys;
using MovieApp.Application.Abstractions.Context;
using MovieApp.Domain.Entitys.Genres;
using MovieApp.Domain.Entitys.Movies;
using MovieApp.Domain.Entitys.Ratings;
using MovieApp.Infrastructure.Authentication.Identity;
using MovieApp.Infrastructure.Persistence.Entites;

namespace MovieApp.Infrastructure.Persistence
{
    public sealed class ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IPublisher publisher)
        : IdentityDbContext<ApplicationUser,
            IdentityRole<Guid>,Guid>(options), IApplicationDbContext
    {
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<Rating> Ratings => Set<Rating>();
        public DbSet<OutboxMessage>
                    OutboxMessages => Set<OutboxMessage>();

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
               typeof(ApplicationDbContext).Assembly );

        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            await PublishDomainEventsAsync(cancellationToken);

            return result;
        }

        private async Task PublishDomainEventsAsync(
         CancellationToken cancellationToken)
        {
            var entities = ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents.Any())
                .Select(x => x.Entity)
                .ToList();

            var domainEvents = entities
                .SelectMany(x => x.DomainEvents)
                .ToList();

            foreach (var domainEvent in domainEvents)
            {
                await publisher.Publish(
                    domainEvent,
                    cancellationToken);
            }

            foreach (var entity in entities)
            {
                entity.ClearDomainEvents();
            }
        }
    }
}
