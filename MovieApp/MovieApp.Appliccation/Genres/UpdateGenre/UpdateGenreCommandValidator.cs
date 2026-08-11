
using FluentValidation;

namespace MovieApp.Appliccation.Genres.UpdateGenre
{
    internal sealed class UpdateGenreCommandValidator
        : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator() 
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.GenreId).NotEmpty();
        }
    }
}
