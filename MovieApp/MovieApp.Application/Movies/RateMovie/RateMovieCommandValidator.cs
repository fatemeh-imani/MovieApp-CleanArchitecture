
using FluentValidation;

namespace MovieApp.Application.Movies.RateMovie
{
    internal sealed class RateMovieCommandValidator
        :AbstractValidator<RateMovieCommand>
    {
        public RateMovieCommandValidator() 
        {
            RuleFor(x => x.MovieId).NotEmpty();
            RuleFor(x => x.Score)
                .NotEmpty()
                .InclusiveBetween(1,10);


        }
    }
}
