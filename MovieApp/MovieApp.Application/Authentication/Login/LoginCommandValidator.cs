
using FluentValidation;

namespace MovieApp.Application.Abstractions.Login
{
    internal sealed class LoginCommandValidator
        :AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
