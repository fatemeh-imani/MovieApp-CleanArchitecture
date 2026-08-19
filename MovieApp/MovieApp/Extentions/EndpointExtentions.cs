using MovieApp.API.Endpoints.Authentication;
using MovieApp.API.Endpoints.Genre;
using MovieApp.API.Endpoints.Movie;
using MovieApp.Infrastructure.Authentication.Role;

namespace MovieApp.API.Extentions
{
    public static class EndpointExtentions
    {
        public static WebApplication MapEndpoint(
            this WebApplication app)
        {
            var adminGroup = app.MapGroup("/api")
                .RequireAuthorization(policy => 
                policy.RequireRole(Roles.Admin));

            var userGroup = app.MapGroup("/api")
                .RequireAuthorization();

            //Admin
            adminGroup.MapCreateMovie();
            adminGroup.MapDeleteMovie();
            adminGroup.MapUpdateMovie();

            adminGroup.MapCreatGenre();
            adminGroup.MapDeleteGenre();
            adminGroup.MapGetAllGenres();
            adminGroup.MapUpdateGenre();

            //User
           userGroup.MapRateMovie();
             //Public
            app.MapGetByIdMovie();
            app.MapGetAllMovies();


            //Authentication
            app.MapRegister();
            app.MapLogin();

            return app;
        }
    }
}
