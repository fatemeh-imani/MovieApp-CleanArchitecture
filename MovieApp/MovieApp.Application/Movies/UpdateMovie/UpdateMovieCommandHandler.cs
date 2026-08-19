

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviApp.SharedKernel.Result;

using MovieApp.Application.Abstractions.Context;
using MovieApp.Domain.Entitys.Genres;
using MovieApp.Domain.Entitys.Movies;

namespace MovieApp.Application.Movies.UpdateMovie
{
    internal class UpdateMovieCommandHandler(
        IApplicationDbContext _context,
        ILogger<UpdateMovieCommandHandler> _logger)
        : IRequestHandler<UpdateMovieCommand, Result>
    {
       
        public async Task<Result> Handle(
            UpdateMovieCommand request,
            CancellationToken cancellationToken)
        {

            _logger.LogInformation(
                "Update Movie {Title}",
                request.Title,
                request.MovieId);

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

            _logger.LogInformation(
                "Movie {request.MovieId} Updated successfully",
                request.MovieId);

            return Result.Success();
        }
    }
}
