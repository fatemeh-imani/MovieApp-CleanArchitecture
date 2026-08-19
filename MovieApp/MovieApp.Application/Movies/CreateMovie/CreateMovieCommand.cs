using MediatR;
using MoviApp.SharedKernel.Result;

namespace MovieApp.Application.Movies.CreateMovie
{
    public sealed record CreateMovieCommand(
      List<Guid> GenreIds , string Title,int YearOfRelease)
        : IRequest<Result>;
    
}
