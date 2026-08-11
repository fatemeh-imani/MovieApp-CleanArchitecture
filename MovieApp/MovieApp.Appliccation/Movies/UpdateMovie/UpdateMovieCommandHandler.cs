

using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviApp.SharedKernel.Result;

using MovieApp.Appliccation.Abstractions.Context;
using MovieApp.Domain.Entitys.Genres;
using MovieApp.Domain.Entitys.Movies;

namespace MovieApp.Appliccation.Movies.UpdateMovie
{
    internal class UpdateMovieCommandHandler(
        IApplicationDbContext _context)
        : IRequestHandler<UpdateMovieCommand, Result>
    {
       
        public async Task<Result> Handle(
            UpdateMovieCommand request,
            CancellationToken cancellationToken)
        {
           

            var movie = await _context.Movies
                .Include(x=>x.Genres)
                .FirstOrDefaultAsync(
                x=> x.Id == request.MovieId,
                    cancellationToken); 

            if (movie is null)
            {
                 return Result.Failure
                    (MovieErrors.NotFound(request.MovieId));
            }
            var genres = await _context.Genres
                .Where(x => request.GenreIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            if(genres.Count != request.GenreIds.Count)
            {
                return Result.Failure(GenreErrors.NotFound);
            }

           var result = movie.Update(
               request.Title,
               request.YearOfRelease,
               genres);

            if(result.IsFailure)
            {
                return result;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
