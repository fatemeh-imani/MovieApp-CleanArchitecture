

using FluentValidation;

namespace MovieApp.Appliccation.Movies.UpdateMovie
{
    internal class UpdateMovieCommandValidator
        : AbstractValidator<UpdateMovieCommand>
    {
        public UpdateMovieCommandValidator() 
        {
            RuleFor(x => x.MovieId).NotEmpty();

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.YearOfRelease)
                .NotEmpty();

            RuleFor(x => x.GenreIds).NotEmpty();

        }
    }
}
