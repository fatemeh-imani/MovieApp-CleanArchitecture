using MediatR;
using MovieApp.Application.Movies.GetAllMovie;

namespace MovieApp.API.Endpoints.Movie
{
    public static class GetAllMovie
    {
        public static IEndpointRouteBuilder MapGetAllMovies(
            this IEndpointRouteBuilder app)
        {
            app.MapGet("/movies", async (
               [AsParameters] FilterMovie filterMovie,
                ISender sender,
                CancellationToken cancellationToken) => 
            {
                var result = await sender.Send(
                    new GetAllMovieQuery(filterMovie),
                    cancellationToken);

                return result.IsSuccess
                ? Results.Ok(result.Value)
                :Results.BadRequest(result.Error);
            });

            return app;
        }
            
    }
}
