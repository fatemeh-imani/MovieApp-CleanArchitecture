
using MediatR;
using MoviApp.SharedKernel.Result;
using MovieApp.Application.CommonResponse;

namespace MovieApp.Application.Genres.GetAllGenre
{
    public sealed record GetAllGenreQuery:
        IRequest<Result<List<GenreResponse>>>;
   
}
