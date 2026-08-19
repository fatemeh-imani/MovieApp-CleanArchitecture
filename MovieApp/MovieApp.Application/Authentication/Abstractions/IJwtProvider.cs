namespace MovieApp.Application.Authentication.Abstractions
{
    public interface IJwtProvider
    {
        string GenerateToken(
            Guid userId,
            string email,
            IEnumerable<string> roles);
    }
}
