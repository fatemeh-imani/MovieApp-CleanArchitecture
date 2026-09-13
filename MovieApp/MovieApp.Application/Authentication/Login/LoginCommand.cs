
using MediatR;
using MovieApp.SharedKernel.Results;

namespace MovieApp.Application.Abstractions.Login
{
    public sealed record LoginCommand(
        string Email,
        string Password)
        :IRequest<Result<string>>;
   
}
