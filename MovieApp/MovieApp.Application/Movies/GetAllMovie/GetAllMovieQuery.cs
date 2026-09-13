

using MediatR;
using MovieApp.SharedKernel.Results;
using MovieApp.Application.CommonResponse;

namespace MovieApp.Application.Movies.GetAllMovie
{
    public sealed record GetAllMovieQuery (
        FilterMovie Filter)
        : IRequest<Result<PageResultMovie<MovieResponse>>>;
        
    
}
