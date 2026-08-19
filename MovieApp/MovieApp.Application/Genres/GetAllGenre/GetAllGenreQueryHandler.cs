
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviApp.SharedKernel.Result;
using MovieApp.Application.Abstractions.Context;
using MovieApp.Application.CommonResponse;

namespace MovieApp.Application.Genres.GetAllGenre
{
    internal sealed class GetAllGenreQueryHandler(
        IApplicationDbContext _context)
        : IRequestHandler<GetAllGenreQuery,
            Result<List<GenreResponse>>>
    {
        public async Task<Result<List<GenreResponse>>> Handle(
            GetAllGenreQuery request,
            CancellationToken cancellationToken)
        {
            var genres = await _context.Genres
                .Select(x => new GenreResponse(
                    x.Id,
                    x.Title))
                .ToListAsync(cancellationToken);

            return Result<List<GenreResponse>>.Success(genres);
        }
    }
}
