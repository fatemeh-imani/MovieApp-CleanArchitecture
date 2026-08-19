
using MoviApp.SharedKernel.Result;

namespace MovieApp.Application.Authentication.Abstractions
{
    public interface IIdentityService
    {
        Task<Result<Guid>> RegisterAsync(
            string email,
            string password,
            CancellationToken cancellationToken);

        Task<Result<string>> LoginAsync(
            string email,
            string password,
            CancellationToken cancellationToken);
    }
}
