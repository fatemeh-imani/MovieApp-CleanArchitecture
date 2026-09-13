
using MediatR;
using MovieApp.SharedKernel.Results;

namespace MovieApp.Application.Movies.RateMovie
{
    public sealed record RateMovieCommand(
        Guid MovieId,int Score):IRequest<Result>;
  
}
