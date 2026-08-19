
using FluentValidation;

namespace MovieApp.Application.Genres.DeleteGenre
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
