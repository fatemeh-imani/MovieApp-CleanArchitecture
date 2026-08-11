

using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviApp.SharedKernel.Result;

using MovieApp.Appliccation.Abstractions.Context;

namespace MovieApp.Appliccation.Movies.GetAllMovie
{
    internal sealed class GetAllMovieQueryHandler(
        IApplicationDbContext _context)
        : IRequestHandler<GetAllMovieQuery, Result<List<MovieResponse>>>
    {
        public async Task<Result<List<MovieResponse>>> Handle(
            GetAllMovieQuery request, 
            CancellationToken cancellationToken)
        {
            
            var movies = await _context.Movies
                 .Select(x => new MovieResponse(
                  x.Id,
                  x.Title,
                  x.YearOfRelease))
                .ToListAsync(cancellationToken);

            return Result<List<MovieResponse>>.Success(movies);
        }
    }
}
