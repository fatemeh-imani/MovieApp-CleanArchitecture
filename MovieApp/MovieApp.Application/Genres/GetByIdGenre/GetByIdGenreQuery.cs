
using MediatR;
using MoviApp.SharedKernel.Result;
using MovieApp.Application.CommonResponse;

namespace MovieApp.Application.Genres.GetByIdGenre
{
    public sealed record GetByIdGenreQuery(Guid GenreId)
        :IRequest<Result<GenreResponse>>;
    
}
