
using MediatR;
using MoviApp.SharedKernel.Result;

namespace MovieApp.Appliccation.Genres.GetByIdGenre
{
    public sealed record GetByIdGenreQuery(Guid GenreId)
        :IRequest<Result<GenreResponse>>;
    
}
