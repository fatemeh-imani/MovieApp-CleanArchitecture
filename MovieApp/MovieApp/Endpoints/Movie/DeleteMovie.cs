using MediatR;
using MovieApp.Application.Movies.DeleteMovie;

namespace MovieApp.API.Endpoints.Movie
{
    public static class DeleteMovie
    {
        public static IEndpointRouteBuilder MapDeleteMovie(
            this IEndpointRouteBuilder app)
        {
            app.MapDelete("/movies/{movieId}", async (
                Guid movieId,
                ISender sender,
                CancellationToken cansellationToken) =>
            {
                var result = await sender.Send(
                    new DeleteMovieCommand(movieId),
                    cansellationToken);
                return result.IsSuccess
                ? Results.Ok()
                : Results.NotFound(result.Error);
            });
            return app;
        }
    }
}
