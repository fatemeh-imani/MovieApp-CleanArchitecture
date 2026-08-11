
using MediatR;
using MoviApp.SharedKernel.Result;

namespace MovieApp.Appliccation.Genres.UpdateGenre
{
    public sealed record UpdateGenreCommand(
        Guid GenreId,string Title)
        : IRequest<Result>;
    
   
}
