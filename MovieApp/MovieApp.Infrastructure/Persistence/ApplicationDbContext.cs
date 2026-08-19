using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieApp.Application.Abstractions.Context;
using MovieApp.Domain.Entitys.Genres;
using MovieApp.Domain.Entitys.Movies;
using MovieApp.Domain.Entitys.Ratings;
using MovieApp.Infrastructure.Authentication.Identity;

namespace MovieApp.Infrastructure.Persistence
{
    public sealed class ApplicationDbContext
        : IdentityDbContext<ApplicationUser,
            IdentityRole<Guid>,Guid>, IApplicationDbContext
    {
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<Rating> Ratings => Set<Rating>();

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {}

       
        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
               typeof(ApplicationDbContext).Assembly );

        }
    }
}
