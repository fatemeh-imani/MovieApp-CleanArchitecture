
using FluentValidation;

namespace MovieApp.Application.Genres.CreateGenre
{
    internal sealed class CreateGenreCommandValidator
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
