
using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Entitys.Genres;
using MovieApp.Domain.Entitys.Movies;
using MovieApp.Domain.Entitys.Ratings;

namespace MovieApp.Application.Abstractions.Context
{
    public interface IApplicationDbContext
    {
        DbSet<Movie> Movies { get; }
        DbSet<Genre> Genres { get; }
        DbSet<Rating> Ratings { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        
        //modelBuilder.Entity<Movie>().HasQueryFilter(x => !x.IsDeleted);
    }
}
