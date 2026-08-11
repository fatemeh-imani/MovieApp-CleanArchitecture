using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Appliccation.Movies.CreateMovie;

namespace MovieApp.API.Endpoints.Movie
{
    public static class CreateMovie
    {
        public static IEndpointRouteBuilder MapCreateMovie(
            this IEndpointRouteBuilder app)
        {
            app.MapPost("/movies",
                async (
                    [FromBody] CreateMovieCommand command,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(
                        command, cancellationToken);

                    return result.IsSuccess
                    ? Results.Ok()
                    : Results.BadRequest(result.Error);
                });
            return app;
                
        }

    }
}
