using MediatR;
using MovieApp.API.Request.Movie;
using MovieApp.Appliccation.Movies.RateMovie;

namespace MovieApp.API.Endpoints.Movie
{
    public static class RateMovie
    {
        public static IEndpointRouteBuilder MapRateMovie(
            this IEndpointRouteBuilder app)
        {
            app.MapPost("/movies.{movieId}/ratinds", async (
               Guid  movieId,
               RateMovieRequest request,
               ISender sender,
               CancellationToken cancellation) =>
            {
                var command = new RateMovieCommand(
                    movieId, request.Score);

                var result = await sender.Send(
                    command,cancellation);

                return result.IsSuccess
                ? Results.Ok()
                : Results.BadRequest(result.Error);
                
            });
            return app;
        }
    }
}
