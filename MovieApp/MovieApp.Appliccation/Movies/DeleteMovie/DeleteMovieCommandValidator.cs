
using FluentValidation;

namespace MovieApp.Appliccation.Movies.DeleteMovie
{
    internal class DeleteMovieCommandValidator
        : AbstractValidator<DeleteMovieCommand>
    {
        public DeleteMovieCommandValidator() 
        {
            RuleFor(x => x.MovieId).NotEmpty();
        }

    }
}
