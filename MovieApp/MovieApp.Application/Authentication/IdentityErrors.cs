using MoviApp.SharedKernel.Errors;

namespace MovieApp.Application.Abstractions
{
    public static class IdentityErrors
    {
        public static readonly Error EmailAlreadyExists = new(
                "Identity.EmailAlreadyExists",
                "Email already exists.",
                 ErrorType.Conflict);

        public static readonly Error InvalidCredentials = new(
             "Identity.InvalidCredentials",
             "Invalid email or password.",
              ErrorType.Unauthorized);

        public static readonly Error RegisterFailed = new(
             "Identity.RegisterFailed",
             "User registration failed.",
              ErrorType.Failure);

    }
}
