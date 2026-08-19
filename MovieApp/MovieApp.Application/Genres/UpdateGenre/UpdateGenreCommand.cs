
using MediatR;
using MoviApp.SharedKernel.Result;

namespace MovieApp.Application.Genres.UpdateGenre
{
    public sealed record UpdateGenreCommand(
        Guid GenreId,string Title)
        : IRequest<Result>;
    
   
}
