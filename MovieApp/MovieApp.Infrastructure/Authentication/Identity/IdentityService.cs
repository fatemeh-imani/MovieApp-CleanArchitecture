using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MoviApp.SharedKernel.Result;
using MovieApp.Application.Abstractions;
using MovieApp.Application.Authentication.Abstractions;
using MovieApp.Infrastructure.Authentication.Role;


namespace MovieApp.Infrastructure.Authentication.Identity
{
    public class IdentityService(
        UserManager<ApplicationUser> _userManager,
        IJwtProvider _jwtProvider,
        ILogger<IdentityService> _logger) 
        : IIdentityService
    {
        public async Task<Result<Guid>> RegisterAsync(
                string email,
                string password,
                CancellationToken cancellationToken)
        {
            var existingUser =
                await _userManager.FindByEmailAsync(email);

            if (existingUser is not null)
            {
               
                return Result<Guid>.Failure(
                    IdentityErrors.EmailAlreadyExists);
            }

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            var result =
                await _userManager.CreateAsync(
                    user,password);

            if (!result.Succeeded)
            {
                return Result<Guid>.Failure(
                    IdentityErrors.RegisterFailed);
            }

            await _userManager.AddToRoleAsync(
                user,Roles.User);

            return Result<Guid>.Success(user.Id);
        }



        public async Task<Result<string>> LoginAsync(
                  string email,
                  string password,
                  CancellationToken cancellationToken)
        {
            var user =
                await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                _logger.LogWarning(
                   "Login failed: User not found for email{Email}.",email);

                return Result<string>.Failure(
                    IdentityErrors.InvalidCredentials);
            }

            var isPasswordValid =
                await _userManager.CheckPasswordAsync(
                    user,
                    password);

            if (!isPasswordValid)
            {
                _logger.LogWarning(
                   "Login failed: invalid password for user {UserId}.",
                   user.Id);
                return Result<string>.Failure(
                    IdentityErrors.InvalidCredentials);
            }

            var roles =
                await _userManager.GetRolesAsync(user);

            var token =
                _jwtProvider.GenerateToken(
                    user.Id,
                    user.Email!,
                    roles);

            return Result<string>.Success(token);
        }

    }
}
