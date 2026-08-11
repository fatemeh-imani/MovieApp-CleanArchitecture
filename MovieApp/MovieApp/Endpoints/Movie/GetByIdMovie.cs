using MediatR;
using MovieApp.Appliccation.Movies.GetByIdMovie;

namespace MovieApp.API.Endpoints.Movie
{
    public static class GetByIdMovie
    {
        public static IEndpointRouteBuilder MapGetByIdMovie(
            this IEndpointRouteBuilder app)
        {
            app.MapGet("/movies/{movieId}", async (
                Guid movieId,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new GetByIdMovieQuery(movieId),
                     cancellationToken);

                return result.IsSuccess
                ?Results.Ok(result.Value)
                : Results.NotFound(result.Error);
            });
            return app;
        }
            

    }
}
