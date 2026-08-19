
using MediatR;
using MoviApp.SharedKernel.Result;

namespace MovieApp.Application.Movies.RateMovie
{
    public sealed record RateMovieCommand(
        Guid MovieId,int Score):IRequest<Result>;
  
}
