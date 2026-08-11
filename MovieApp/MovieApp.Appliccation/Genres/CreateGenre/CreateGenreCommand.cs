

using MediatR;
using MoviApp.SharedKernel.Result;

namespace MovieApp.Appliccation.Genres.CreateGenre
{
    public sealed record CreateGenreCommand(
        string Title)
        :IRequest<Result>;
   
}
