
using FluentValidation;

namespace MovieApp.Application.Movies.GetAllMovie
{
    internal sealed class FiltermovieValidation
        :AbstractValidator<FilterMovie>
    {
        public FiltermovieValidation() 
        {

            RuleFor(x => x.SortBy)
                .Must(x =>
                string.IsNullOrWhiteSpace(x) ||
                x.Equals("title", StringComparison.OrdinalIgnoreCase) ||
                x.Equals("year", StringComparison.OrdinalIgnoreCase))
                .WithMessage("SortBy must be 'title' or 'year'.");
           
               
            RuleFor(x=>x.Page).GreaterThan(0);
            RuleFor(x=>x.PageSize).InclusiveBetween(1,100);
        }
    }
}
