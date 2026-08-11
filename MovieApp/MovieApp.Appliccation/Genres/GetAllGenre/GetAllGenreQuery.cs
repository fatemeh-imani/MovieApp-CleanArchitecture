
using MediatR;
using MoviApp.SharedKernel.Result;

namespace MovieApp.Appliccation.Genres.GetAllGenre
{
    public sealed record GetAllGenreQuery:
        IRequest<Result<List<GenreResponse>>>;
   
}
