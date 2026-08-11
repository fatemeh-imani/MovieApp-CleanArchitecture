
using FluentValidation;

namespace MovieApp.Appliccation.Movies.CreateMovie
{
    internal class CreateMovieCommandValidator
        : AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidator() 
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(x => x.YearOfRelease)
                .NotEmpty();
                
        }
    }
}
