
using MediatR;
using MovieApp.SharedKernel.Results;

namespace MovieApp.Application.Abstractions.Register
{
    public sealed record RegisterCommand(
        string Email,string Password)
        : IRequest<Result<Guid>>;
    
}
