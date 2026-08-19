using MovieApp.Application.CommonResponse;


namespace MovieApp.Application.CommonResponse
{
    public sealed record MovieResponse(
        Guid MovieId,
        string Title,
        int YearOfRelease,
        List<GenreResponse> Genres,
         double? AverageRating );
   
}
