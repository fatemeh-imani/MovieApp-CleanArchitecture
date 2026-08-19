using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviApp.SharedKernel.Result;
using MovieApp.Application.Authentication.Abstractions;
using MovieApp.Application.Abstractions.Context;
using MovieApp.Domain.Entitys.Movies;

namespace MovieApp.Application.Movies.RateMovie
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

            var result = movie.AddOrUpdateRating(
               _userContext.UserId,
                request.Score);

            if(result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            _context.Ratings.Add(result.Value!);

           
            await _context.SaveChangesAsync(
                cancellationToken);

            return Result.Success();
        }

        
    }
}
