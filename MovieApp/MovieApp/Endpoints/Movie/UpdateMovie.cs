using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieApp.API.Request.Movie;
using MovieApp.Appliccation.Movies.UpdateMovie;

namespace MovieApp.API.Endpoints.Movie
{
    public static class UpdateMovie
    {
        public static IEndpointRouteBuilder MapUpdateMovie(
           this IEndpointRouteBuilder app)
        {
            app.MapPut("/movies/{movieId}", async (
              Guid movieId,
              [FromBody] UpdateGenreRequest request,
              ISender sender,
              CancellationToken cancellationToken) => 
            {
                var command = new UpdateMovieCommand(
                  movieId, request.Title, request.YearOfRelease
                    , request.GenreIds);

                var result = await sender.Send(
                    command,cancellationToken);

                return result.IsSuccess
                ? Results.Ok()
                : Results.BadRequest();
            });

            return app;
        }
    }
}
