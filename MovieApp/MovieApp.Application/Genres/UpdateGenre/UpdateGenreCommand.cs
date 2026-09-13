
using MediatR;
using MovieApp.SharedKernel.Results;

namespace MovieApp.Application.Genres.UpdateGenre
{
    public sealed record UpdateGenreCommand(
        Guid GenreId,string Title)
        : IRequest<Result>;
    
   
}
