using MediatR;
using MoviApp.SharedKernel.Result;
using MovieApp.Application.CommonResponse;

namespace MovieApp.Application.Movies.GetByIdMovie
{
    public sealed record GetByIdMovieQuery(
        Guid MovieId)
        : IRequest<Result<MovieResponse>>;
   
}
