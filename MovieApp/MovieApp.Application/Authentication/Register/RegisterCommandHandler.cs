using MediatR;
using MovieApp.SharedKernel.Results;
using MovieApp.Application.Authentication.Abstractions;
using MovieApp.Application.Abstractions.Context;

namespace MovieApp.Application.Abstractions.Register
{
    internal sealed class RegisterCommandHandler(
        IIdentityService _identityService)
        : IRequestHandler<RegisterCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(
            RegisterCommand request, 
            CancellationToken cancellationToken)
        {
            return await _identityService.RegisterAsync(
                request.Email, request.Password, cancellationToken);
        }
    }
}
