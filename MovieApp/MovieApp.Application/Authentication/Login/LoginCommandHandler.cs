
using MediatR;
using MovieApp.SharedKernel.Results;
using MovieApp.Application.Authentication.Abstractions;

namespace MovieApp.Application.Abstractions.Login
{
    internal sealed class LoginCommandHandler(
        IIdentityService _identityService)
        : IRequestHandler<LoginCommand, Result<string>>
    {

        public async Task<Result<string>> Handle(
            LoginCommand request, 
            CancellationToken cancellationToken)
        {
           return await _identityService.LoginAsync(
               request.Email,
               request.Password,
               cancellationToken);
        }
    }
}

