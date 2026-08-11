using MediatR;
using MovieApp.Appliccation.Genres.DeleteGenre;

namespace MovieApp.API.Endpoints.Genre
{
    public static class DeleteGenre
    {
        public static IEndpointRouteBuilder MapDeleteGenre(
            this IEndpointRouteBuilder app)
        {
            app.MapDelete("/genres/{genreId}", async (
                Guid genreId,
                ISender sender,
                CancellationToken cancellation) =>
            {
                var result = await sender.Send(
                    new DeleteGenreCommand(genreId),
                    cancellation);

                return result.IsSuccess
                ?Results.Ok()
                :Results.BadRequest(result.Error);
            });

            return app;
        }
    }
}
