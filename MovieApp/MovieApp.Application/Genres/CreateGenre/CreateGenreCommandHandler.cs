
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviApp.SharedKernel.Result;

using MovieApp.Application.Abstractions.Context;
using MovieApp.Domain.Entitys.Genres;

namespace MovieApp.Application.Genres.CreateGenre
{
    internal sealed class CreateGenreCommandHandler(
        IApplicationDbContext _context)
        : IRequestHandler<CreateGenreCommand, Result>
    {
        public async Task<Result> Handle(
            CreateGenreCommand request, 
            CancellationToken cancellationToken)
        {
            var exists = await _context.Genres
                .AnyAsync(x=>x.Title == request.Title);

            if (exists)
            {
                return Result.Failure(GenreErrors.AlreadyExists);
            }


             var genre = Genre.Create(request.Title);

            _context.Genres.Add(genre);
           await  _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
            
        }
    }
}
