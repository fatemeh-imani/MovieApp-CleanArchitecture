using MediatR;
using MoviApp.SharedKernel.Result;

namespace MovieApp.Appliccation.Movies.GetByIdMovie
{
    public sealed record GetByIdMovieQuery(Guid MovieId)
        : IRequest<Result<MovieResponse>>;
   
}
