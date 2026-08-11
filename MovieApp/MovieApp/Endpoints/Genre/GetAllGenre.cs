using MediatR;
using MovieApp.Appliccation.Genres.GetAllGenre;

namespace MovieApp.API.Endpoints.Genre
{
    public static class GetAllGenre
    {
        public static IEndpointRouteBuilder MapGetAllGenres(
            this IEndpointRouteBuilder app)
        {
            app.MapGet("/genres", async (
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                 var result = await sender.Send(
                     new GetAllGenreQuery(),cancellationToken);

                return result.IsSuccess
                ?Results.Ok()
                :Results.BadRequest(result.Error);
            });
            return app;
        }
    }
}
