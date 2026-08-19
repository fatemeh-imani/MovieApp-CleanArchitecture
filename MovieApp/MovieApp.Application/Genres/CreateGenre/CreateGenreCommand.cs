

using MediatR;
using MoviApp.SharedKernel.Result;

namespace MovieApp.Application.Genres.CreateGenre
{
    public sealed record CreateGenreCommand(
        string Title)
        :IRequest<Result>;
   
}
