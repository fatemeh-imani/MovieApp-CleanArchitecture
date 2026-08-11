
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviApp.SharedKernel.Result;
using MovieApp.Appliccation.Abstractions.Authentication;
using MovieApp.Appliccation.Abstractions.Context;
using MovieApp.Domain.Entitys.Movies;

namespace MovieApp.Appliccation.Movies.RateMovie
{
    internal sealed class RateMovieCommandHandler(
        IApplicationDbContext _context,
        IUserContext _userContext)
        : IRequestHandler<RateMovieCommand, Result>
    {
        public async Task<Result> Handle(
            RateMovieCommand request,
            CancellationToken cancellationToken)
        {
            var movie = await _context.Movies
                .Include(x=>x.Ratings)
                .FirstOrDefaultAsync(
                x=>x.Id == request.MovieId,
                cancellationToken);

            if (movie is null)
            {
                return Result.Failure(
                    MovieErrors.NotFound(request.MovieId));
            }

            var result = movie.AddRating(
               _userContext.UserId,
                request.Score);

            if(result.IsFailure)
            {
                return result;
            }

            await _context.SaveChangesAsync(
                cancellationToken);

            return Result.Success();
        }

        
    }
}
