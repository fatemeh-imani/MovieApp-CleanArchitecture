
using MediatR;
using MoviApp.SharedKernel.Result;

namespace MovieApp.Application.Abstractions.Register
{
    public sealed record RegisterCommand(
        string Email,string Password)
        : IRequest<Result<Guid>>;
    
}
