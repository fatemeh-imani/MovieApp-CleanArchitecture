using MediatR;
using MovieApp.Application.Abstractions.Login;

namespace MovieApp.API.Endpoints.Authentication
{
    public static class Login
    {
        public static IEndpointRouteBuilder MapLogin(
            this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/auth/login", async (
                    LoginCommand command,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(
                        command, cancellationToken);

                    return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : Results.Unauthorized();


                });
            return app;
        }
    }
}
