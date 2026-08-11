

namespace MovieApp.Appliccation.Movies.GetAllMovie
{
    public sealed record MovieResponse(
        Guid MovieId,
        string Title,
        int YearOfRelease);
   
}
