
using MediatR;
using MovieApp.SharedKernel.Results;
using MovieApp.Application.CommonResponse;

namespace MovieApp.Application.Genres.GetByIdGenre
{
    public sealed record GetByIdGenreQuery(Guid GenreId)
        :IRequest<Result<GenreResponse>>;
    
}
