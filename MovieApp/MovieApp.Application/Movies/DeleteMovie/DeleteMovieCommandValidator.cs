
using FluentValidation;

namespace MovieApp.Application.Movies.DeleteMovie
{
    internal sealed class DeleteMovieCommandValidator
        : AbstractValidator<DeleteMovieCommand>
    {
        public DeleteMovieCommandValidator() 
        {
            RuleFor(x => x.MovieId).NotEmpty();
        }

    }
}
