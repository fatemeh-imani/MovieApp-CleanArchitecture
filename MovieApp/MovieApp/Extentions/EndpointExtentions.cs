using MovieApp.API.Endpoints.Genre;
using MovieApp.API.Endpoints.Movie;

namespace MovieApp.API.Extentions
{
    public static class EndpointExtentions
    {
        public static WebApplication MapEndpoint(
            this WebApplication app)
        {
            app.MapCreateMovie();
            app.MapDeleteMovie();
            app.MapUpdateMovie();
            app.MapGetAllGenres();
            app.MapGetByIdMovie();
            app.MapRateMovie();

            app.MapCreatGenre();
            app.MapDeleteGenre();
            app.MapGetAllGenres();
            app.MapUpdateGenre();
            return app;
        }
    }
}
