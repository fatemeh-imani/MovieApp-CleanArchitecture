

using MediatR;
using MoviApp.SharedKernel.Result;
using MovieApp.Application.CommonResponse;

namespace MovieApp.Application.Movies.GetAllMovie
{
    public sealed record GetAllMovieQuery (
        FilterMovie Filter)
        : IRequest<Result<PageResultMovie<MovieResponse>>>;
        
    
}
