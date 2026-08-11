
using MediatR;
using MoviApp.SharedKernel.Result;

namespace MovieApp.Appliccation.Genres.DeleteGenre
{
    public sealed record DeleteGenreCommand(Guid GenreId)
        :IRequest<Result>;
    
}
