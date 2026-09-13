

using MediatR;
using MovieApp.SharedKernel.Results;

namespace MovieApp.Application.Genres.CreateGenre
{
    public sealed record CreateGenreCommand(
        string Title)
        :IRequest<Result>;
   
}
