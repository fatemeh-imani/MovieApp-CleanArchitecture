using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieApp.SharedKernel.Results;
using MovieApp.Application.Abstractions.Context;
using MovieApp.Domain.Entitys.Movies;

namespace MovieApp.Application.Movies.DeleteMovie
{
    internal sealed class DeleteMovieCommandHandler(
        IApplicationDbContext _context,
        ILogger<DeleteMovieCommandHandler> _logger)
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
            //HasQueryFilter
            movie.Delete();

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
              "Movie {MovieId} deleted successfully.",
                request.MovieId);

            return Result.Success();
        }
    }
}
