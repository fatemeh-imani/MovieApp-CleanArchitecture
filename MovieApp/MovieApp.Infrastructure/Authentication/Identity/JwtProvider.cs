using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MovieApp.Application.Authentication.Abstractions;
using MovieApp.Infrastructure.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieApp.Infrastructure.Authentication.Identity
{
    internal sealed class JwtProvider(
        IOptions<JwtOptions> options)
        : IJwtProvider
    {
        public string GenerateToken(
            Guid userId,
            string email,
            IEnumerable<string> roles)
        {
            var jwtOptions = options.Value;

            var claims = new List<Claim>
        {
            new(
                JwtRegisteredClaimNames.Sub,
                userId.ToString()),

            new(
                JwtRegisteredClaimNames.Email,
                email)
        };

            foreach (var role in roles)
            {
                claims.Add(
                    new Claim(
                        ClaimTypes.Role,
                        role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtOptions.SecretKey));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    jwtOptions.ExpirationInMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}