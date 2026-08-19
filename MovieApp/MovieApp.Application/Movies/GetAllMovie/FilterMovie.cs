
namespace MovieApp.Application.Movies.GetAllMovie
{
    public sealed record FilterMovie(
        string? Search,
        int? YearOfRelease,
        string? SortBy,
        bool SortDescending= false,
        int Page = 1,
        int PageSize = 10);
   
}
