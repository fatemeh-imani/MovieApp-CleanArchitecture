using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviApp.SharedKernel.Result;
using MovieApp.Application.CommonResponse;
using MovieApp.Application.Abstractions.Context;
using MovieApp.Domain.Entitys.Movies;


namespace MovieApp.Application.Movies.GetAllMovie
{
    internal sealed class GetAllMovieQueryHandler(
        IApplicationDbContext _context)
        : IRequestHandler<GetAllMovieQuery, Result<PageResultMovie<MovieResponse>>>
    {
        public async Task<Result<PageResultMovie<MovieResponse>>> Handle(
            GetAllMovieQuery request, 
            CancellationToken cancellationToken)
        {
            int page = request.Filter.Page;
            int pageSize = request.Filter.PageSize;
            //Deferred Execution
            IQueryable<Movie> query = _context.Movies;

            if(!string.IsNullOrWhiteSpace(request.Filter.Search))
            {
                query = query.Where(x =>
                x.Title.Contains(request.Filter.Search) ||
                 x.Genres.Any(g =>
                    g.Title.Contains(request.Filter.Search)));
            }

            if(request.Filter.YearOfRelease.HasValue)
            {
                query = query.Where(x =>
                x.YearOfRelease == request.Filter.YearOfRelease.Value);
            }
            
            int totalCount = await query.CountAsync(cancellationToken);

            if(!string.IsNullOrWhiteSpace(request.Filter.SortBy))
            {
                query = request.Filter.SortBy.ToLower() switch
                {
                    "title" => request.Filter.SortDescending
                    ? query.OrderByDescending(x => x.Title)
                    : query.OrderBy(x => x.Title),
                    "year" => request.Filter.SortDescending
                    ? query.OrderByDescending(x => x.YearOfRelease)
                    : query.OrderBy(x => x.YearOfRelease),
                    _ => query
                };
            }
            //Projection
            List<MovieResponse> movies = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                 .Select(x => new MovieResponse(
                  x.Id,
                  x.Title,
                  x.YearOfRelease,
                  x.Genres
                    .Select(g => new GenreResponse(
                        g.Id,
                        g.Title)).ToList(),
                  x.Ratings
                  .Select(r=> (double?)r.Score).Average()))
                .ToListAsync(cancellationToken);
            var pageResult = new PageResultMovie<MovieResponse>
            {
                Items = movies,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return Result<PageResultMovie<MovieResponse>>.Success(pageResult);
        }
    }
}
