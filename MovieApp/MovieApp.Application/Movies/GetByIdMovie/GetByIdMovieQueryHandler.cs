using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviApp.SharedKernel.Result;
using MovieApp.Application.CommonResponse;
using MovieApp.Application.Abstractions.Context;
using MovieApp.Domain.Entitys.Movies;

namespace MovieApp.Application.Movies.GetByIdMovie
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
            //Projection
            return Result<MovieResponse>.Success(
                new MovieResponse(
                    movie.Id,
                    movie.Title,
                    movie.YearOfRelease,
                    movie.Genres
                    .Select(g => new GenreResponse(
                       g.Id,
                       g.Title)).ToList(),
                    movie.Ratings
                    .Select(r=>(double?)r.Score).Average()));
        }
    }
}
