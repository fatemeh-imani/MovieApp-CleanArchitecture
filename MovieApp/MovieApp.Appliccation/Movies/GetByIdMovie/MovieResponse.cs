
namespace MovieApp.Appliccation.Movies.GetByIdMovie
{
    public sealed record MovieResponse(
        Guid MovieId,
        string Title,
        int YearOfRelease);
   
}
