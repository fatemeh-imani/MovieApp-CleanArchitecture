using MediatR;
using MovieApp.Application.Abstractions.Register;

namespace MovieApp.API.Endpoints.Authentication
{
    public static class Register
    {
        public static IEndpointRouteBuilder MapRegister(
            this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/auth/register", async (
                RegisterCommand command,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    command,cancellationToken);
                return result.IsSuccess
                ?Results.Ok(result.Value)
                :Results.BadRequest(result.Error);
            });
            return app;
        }
    }
}
