using Microsoft.EntityFrameworkCore;
using MovieApp.Appliccation.Abstractions.Context;
using MovieApp.Domain.Entitys.Genres;
using MovieApp.Domain.Entitys.Movies;
using MovieApp.Domain.Entitys.Ratings;

namespace MovieApp.Infrastructure.Persistence
{
    public sealed class ApplicationDbContext
        : DbContext, IApplicationDbContext
    {
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<Rating> Ratings => Set<Rating>();

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
               typeof(ApplicationDbContext).Assembly );

            base.OnModelCreating(modelBuilder);
        }
    }
}
