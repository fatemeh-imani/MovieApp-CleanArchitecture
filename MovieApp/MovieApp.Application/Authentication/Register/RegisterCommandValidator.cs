
using FluentValidation;

namespace MovieApp.Application.Abstractions.Register
{
    internal sealed class RegisterCommandValidator
        : AbstractValidator <RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}
