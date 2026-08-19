using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Application.Genres.CreateGenre;
using MovieApp.Infrastructure.Authentication.Role;

namespace MovieApp.API.Endpoints.Genre
{
    public static  class CreatGenre
    {
        public static IEndpointRouteBuilder MapCreatGenre(
            this IEndpointRouteBuilder app)
        {
            app.MapPost("/genres", async (
                [FromBody] CreateGenreCommand command,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    command, cancellationToken);

                return result.IsSuccess
                ? Results.Ok()
                :Results.BadRequest(result.Error);
            });
            return app;
        }
    }
}
