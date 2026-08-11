

using MediatR;
using MoviApp.SharedKernel.Result;

namespace MovieApp.Appliccation.Movies.UpdateMovie
{
    public sealed record UpdateMovieCommand(
       Guid MovieId, string Title,
       int YearOfRelease,List<Guid> GenreIds)
        :IRequest<Result>;
    
}
