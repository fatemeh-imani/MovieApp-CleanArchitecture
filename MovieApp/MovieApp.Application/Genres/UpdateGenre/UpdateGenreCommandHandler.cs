
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieApp.SharedKernel.Results;
using MovieApp.Application.Abstractions.Context;
using MovieApp.Application.Genres.DeleteGenre;
using MovieApp.Domain.Entitys.Genres;

namespace MovieApp.Application.Genres.UpdateGenre
{
    internal sealed class UpdateGenreCommandHandler(
        IApplicationDbContext _context)
        : IRequestHandler<UpdateGenreCommand, Result>
    {
        public async Task<Result> Handle(
            UpdateGenreCommand request,
            CancellationToken cancellationToken)
        {
            var genre = await _context.Genres
                 .FirstOrDefaultAsync(
                x => x.Id == request.GenreId,
                cancellationToken);

            if (genre is null)
            {
              return  Result.Failure(GenreErrors.NotFound);

            }

             genre.Update(request.Title);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
