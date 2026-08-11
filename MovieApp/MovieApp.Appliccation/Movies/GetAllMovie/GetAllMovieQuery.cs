

using MediatR;
using MoviApp.SharedKernel.Result;

namespace MovieApp.Appliccation.Movies.GetAllMovie
{
    public sealed record GetAllMovieQuery 
        : IRequest<Result<List<MovieResponse>>>;
        
    
}
