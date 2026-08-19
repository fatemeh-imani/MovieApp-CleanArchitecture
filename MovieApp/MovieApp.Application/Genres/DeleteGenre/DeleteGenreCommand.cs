
using MediatR;
using MoviApp.SharedKernel.Result;

namespace MovieApp.Application.Genres.DeleteGenre
{
    public sealed record DeleteGenreCommand(Guid GenreId)
        :IRequest<Result>;
    
}
