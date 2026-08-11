
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MoviApp.SharedKernel.Result;

using MovieApp.Appliccation.Abstractions.Context;
using MovieApp.Domain.Entitys.Movies;

namespace MovieApp.Appliccation.Movies.CreateMovie
{
    internal sealed class CreateMovieCommandHandler(
         IApplicationDbContext _context)
        : IRequestHandler<CreateMovieCommand, Result>
    {
         public async Task<Result> Handle(
         CreateMovieCommand request,
         CancellationToken cancellationToken)
        {
            

            var genres = await _context.Genres
                .Where(x => request.GenreIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            if(genres.Count != request.GenreIds.Count)
            {
                return Result.Failure(MovieErrors.GenreNotFound);
            }

            var movie = Movie.Create(
                request.Title,
                request.YearOfRelease,
                genres);

          

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
    }
}
