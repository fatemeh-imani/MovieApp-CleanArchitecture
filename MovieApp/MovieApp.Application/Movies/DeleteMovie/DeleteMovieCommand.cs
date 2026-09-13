
using MediatR;
using MovieApp.SharedKernel.Results;

namespace MovieApp.Application.Movies.DeleteMovie
{
    public sealed record DeleteMovieCommand(Guid MovieId)
        : IRequest<Result>;
    
    
}
