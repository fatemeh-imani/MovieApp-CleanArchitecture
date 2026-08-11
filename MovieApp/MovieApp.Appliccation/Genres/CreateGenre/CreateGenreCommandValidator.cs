
using FluentValidation;

namespace MovieApp.Appliccation.Genres.CreateGenre
{
    internal class CreateGenreCommandValidator
        : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator() 
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);


        }
    }
}
