
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieApp.SharedKernel.Results;
using MovieApp.Application.Abstractions.Context;
using MovieApp.Application.CommonResponse;
using MovieApp.Domain.Entitys.Genres;

namespace MovieApp.Application.Genres.GetByIdGenre
{
    internal sealed class GetByIdGenreQureyHandler(
        IApplicationDbContext _context)
        : IRequestHandler<GetByIdGenreQuery, Result<GenreResponse>>
    {
        public async Task<Result<GenreResponse>> Handle(
            GetByIdGenreQuery request,
            CancellationToken cancellationToken)
        {
            var genre = await _context.Genres
                .FirstOrDefaultAsync(
                x => x.Id == request.GenreId);

            if (genre is null)
            {
                return Result<GenreResponse>
                    .Failure(GenreErrors.NotFound);
            }

            return Result<GenreResponse>.Success(
                new GenreResponse(
                    genre.Id,
                    genre.Title));
        }
    }
}
