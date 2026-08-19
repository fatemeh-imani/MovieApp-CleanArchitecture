
using MediatR;
using MoviApp.SharedKernel.Result;

namespace MovieApp.Application.Abstractions.Login
{
    public sealed record LoginCommand(
        string Email,
        string Password)
        :IRequest<Result<string>>;
   
}
