using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviApp.SharedKernel.Result;

using MovieApp.Appliccation.Abstractions.Context;
using MovieApp.Domain.Entitys.Movies;

namespace MovieApp.Appliccation.Movies.GetByIdMovie
{
    internal sealed class GetByIdMovieQueryHandler(
        IApplicationDbContext _context)
        : IRequestHandler<GetByIdMovieQuery, 
            Result<MovieResponse>>

    {
        public async Task<Result<MovieResponse>> Handle(
            GetByIdMovieQuery request, CancellationToken cancellationToken)
        {
               var movie = await _context.Movies
                .AsNoTracking()
                .FirstOrDefaultAsync(
                x=> x.Id == request.MovieId,
                cancellationToken);
               
            if(movie is null)
            {
                return Result<MovieResponse>.Failure(
                       MovieErrors.NotFound(request.MovieId));
            }

            return Result<MovieResponse>.Success(
                new MovieResponse(
                    movie.Id,
                    movie.Title,
                    movie.YearOfRelease));
        }
    }
}
