
using MediatR;
using MoviApp.SharedKernel.Result;

namespace MovieApp.Application.Movies.DeleteMovie
{
    public sealed record DeleteMovieCommand(Guid MovieId)
        : IRequest<Result>;
    
    
}
