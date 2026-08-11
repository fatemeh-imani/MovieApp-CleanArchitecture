
using FluentValidation;

namespace MovieApp.Appliccation.Genres.DeleteGenre
{
    internal sealed class DeleteGenreCommandValidator
        : AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator() 
        {
            RuleFor(x => x.GenreId).NotEmpty();
        }
    }
}
