
using MediatR;
using MovieApp.SharedKernel.Results;

namespace MovieApp.Application.Genres.DeleteGenre
{
    public sealed record DeleteGenreCommand(Guid GenreId)
        :IRequest<Result>;
    
}
