namespace MovieApp.API.Request.Movie
{
    public sealed record UpdateMovieRequest(
        string Title,
        int YearOfRelease,List<Guid> GenreIds);
    
}
