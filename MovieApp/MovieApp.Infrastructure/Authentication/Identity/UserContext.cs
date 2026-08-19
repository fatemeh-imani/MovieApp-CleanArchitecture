using Microsoft.AspNetCore.Http;
using MovieApp.Application.Authentication.Abstractions;
using System.Security.Claims;

namespace MovieApp.Infrastructure.Authentication.Identity
{
    internal sealed class UserContext(
        IHttpContextAccessor _httpContextAccessor) : IUserContext
    {
        public Guid UserId
        {
            get
            {
                var userId =
                    _httpContextAccessor
                    .HttpContext?
                    .User
                    .FindFirstValue(ClaimTypes.NameIdentifier);

               return Guid.Parse(userId!);
            }
        }
    }
}
