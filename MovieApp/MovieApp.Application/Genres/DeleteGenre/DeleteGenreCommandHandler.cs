
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviApp.SharedKernel.Result;
using MovieApp.Application.Abstractions.Context;
using MovieApp.Domain.Entitys.Genres;

namespace MovieApp.Application.Genres.DeleteGenre
{
    internal class DeleteGenreCommandHandler(
        IApplicationDbContext _context)
        : IRequestHandler<DeleteGenreCommand, Result>
    {
        public async Task<Result> Handle(
            DeleteGenreCommand request,
            CancellationToken cancellationToken)
        {
            var genre = await _context.Genres
               .FirstOrDefaultAsync(
                x => x.Id == request.GenreId,
                cancellationToken);

            if (genre is null)
            {
                return Result.Failure(GenreErrors.NotFound);
            }

            genre.Delete();
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
} 