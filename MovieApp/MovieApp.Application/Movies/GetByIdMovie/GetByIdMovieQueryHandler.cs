using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieApp.SharedKernel.Results;
using MovieApp.Application.CommonResponse;
using MovieApp.Application.Abstractions.Context;
using MovieApp.Domain.Entitys.Movies;
using MovieApp.Application.Movies.GetAllMovie;

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
               .Where(x => x.Id == request.MovieId)
               .Select(x => new MovieResponse(
                          x.Id,
                          x.Title,
                          x.YearOfRelease,
                          x.Genres
                      .Select(g => new GenreResponse(
                          g.Id,
                          g.Title)).ToList(),
                          x.Ratings
                            .Select(r => (double?)r.Score)
                                   .Average()))
               .FirstOrDefaultAsync(cancellationToken);

            if (movie is null)
            {
                return Result<MovieResponse>.Failure(
                    MovieErrors.NotFound(request.MovieId));
            }

            return Result<MovieResponse>.Success(movie);
        }
    }
}
