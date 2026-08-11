
using MediatR;
using MoviApp.SharedKernel.Result;

namespace MovieApp.Appliccation.Movies.DeleteMovie
{
    public sealed record DeleteMovieCommand(Guid MovieId)
        : IRequest<Result>;
    
    
}
