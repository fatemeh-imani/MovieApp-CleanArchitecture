
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviApp.SharedKernel.Result;

using MovieApp.Appliccation.Abstractions.Context;
using MovieApp.Domain.Entitys.Movies;

namespace MovieApp.Appliccation.Movies.DeleteMovie
{
    internal sealed class DeleteMovieCommandHandler(
        IApplicationDbContext _context)
        : IRequestHandler<DeleteMovieCommand, Result>
    {
        public async Task<Result> Handle(
            DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            
            var movie =await _context.Movies
                .FirstOrDefaultAsync(
                x=> x.Id == request.MovieId,
                cancellationToken);

            if(movie is null)
            {
                return Result.Failure(
                    MovieErrors.NotFound(request.MovieId));
            }

            movie.Delete();
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
