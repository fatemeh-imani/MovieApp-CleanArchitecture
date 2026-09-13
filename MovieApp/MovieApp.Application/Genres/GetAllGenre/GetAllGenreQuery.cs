
using MediatR;
using MovieApp.SharedKernel.Results;
using MovieApp.Application.CommonResponse;

namespace MovieApp.Application.Genres.GetAllGenre
{
    public sealed record GetAllGenreQuery:
        IRequest<Result<List<GenreResponse>>>;
   
}
