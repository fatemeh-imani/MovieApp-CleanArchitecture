using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Application.Movies.CreateMovie;
using MovieApp.SharedKernel.Errors;

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
     : result.Error.Type switch
     {
         ErrorType.NotFound => Results.NotFound(result.Error),
         ErrorType.Conflict => Results.Conflict(result.Error),
         ErrorType.Validation => Results.BadRequest(result.Error),
         ErrorType.Unauthorized => Results.Unauthorized(),
         ErrorType.Forbidden => Results.Forbid(),
         _ => Results.StatusCode(500)
     };
                });
            return app;
                
        }

    }
}
