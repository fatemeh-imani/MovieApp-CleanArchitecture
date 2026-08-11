using MediatR;
using MovieApp.Appliccation.Movies.GetAllMovie;

namespace MovieApp.API.Endpoints.Movie
{
    public static class GetAllMovie
    {
        public static IEndpointRouteBuilder MapGetMovies(
            this IEndpointRouteBuilder app)
        {
            app.MapGet("/movies", async (
                
                ISender sender,
                CancellationToken cancellationToken) => 
            {
                var result = await sender.Send(
                    new GetAllMovieQuery(),
                    cancellationToken);

                return result.IsSuccess
                ? Results.Ok(result.Value)
                :Results.BadRequest(result.Error);
            });

            return app;
        }
            
    }
}
