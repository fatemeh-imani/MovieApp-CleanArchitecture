
using MediatR;
using MoviApp.SharedKernel.Result;

namespace MovieApp.Appliccation.Movies.RateMovie
{
    public sealed record RateMovieCommand(
        Guid MovieId,int Score):IRequest<Result>;
  
}
