using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieApp.API.Request.Genre;
using MovieApp.Appliccation.Genres.UpdateGenre;

namespace MovieApp.API.Endpoints.Genre
{
    public static class UpdateGenre
    {
        public static IEndpointRouteBuilder MapUpdateGenre(
            this IEndpointRouteBuilder app)
        {
            app.MapPut("/genrs/{genreId}", async (
                Guid genreId,
                [FromBody] UpdateGenreRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new UpdateGenreCommand(
                    genreId,
                    request.Title);

                var result = await sender.Send(
                    command, cancellationToken);

                return result.IsSuccess
                ?Results.Ok()
                :Results.BadRequest(result.Error);
            });
            return app;
        }
    }
}
