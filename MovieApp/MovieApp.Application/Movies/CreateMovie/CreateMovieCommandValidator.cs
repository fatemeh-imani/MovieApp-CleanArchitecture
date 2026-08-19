
using FluentValidation;

namespace MovieApp.Application.Movies.CreateMovie
{
    internal sealed class CreateMovieCommandValidator
        : AbstractValidator<CreateMovieCommand>
    {

        public CreateMovieCommandValidator() 
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(x => x.YearOfRelease)
                .GreaterThan(0);
                
        }
    }
}
