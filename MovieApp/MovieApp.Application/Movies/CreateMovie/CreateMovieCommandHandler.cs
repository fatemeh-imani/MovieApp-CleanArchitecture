using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieApp.SharedKernel.Results;
using MovieApp.Application.Abstractions.Context;
using MovieApp.Domain.Entitys.Movies;


namespace MovieApp.Application.Movies.CreateMovie
{
    internal sealed class CreateMovieCommandHandler(
         IApplicationDbContext _context,
         ILogger<CreateMovieCommandHandler> _logger)
        : IRequestHandler<CreateMovieCommand, Result>
    {

         public async Task<Result> Handle(
         CreateMovieCommand request,
         CancellationToken cancellationToken)
        {

            _logger.LogInformation(
                "Create  movie {Title}.",
                   request.Title);

            var genres = await _context.Genres
                .Where(x => request.GenreIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            if(genres.Count != request.GenreIds.Count)
            {
                _logger.LogWarning(
                   "Some genres were not found for movie {Title}.",
                    request.Title);

                return Result.Failure(MovieErrors.GenreNotFound);
            }

            var movie = Movie.Create(
                request.Title,
                request.YearOfRelease,
                genres);

          

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
               "Movie {MovieId} created successfully.",
                  movie.Id);

            return Result.Success();

        }
    }
}
