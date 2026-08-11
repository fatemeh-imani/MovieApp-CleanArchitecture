using MoviApp.SharedKernel.Entitys;
using MoviApp.SharedKernel.Result;
using MovieApp.Domain.Entitys.Genres;
using MovieApp.Domain.Entitys.Ratings;


namespace MovieApp.Domain.Entitys.Movies
{
    public sealed class Movie : Entity
    {
        public string Title {  get; private set; }
        public int YearOfRelease { get; private set; }

        private readonly List<Rating> _ratings = new();
        private readonly List<Genre> _genres = new();

        public IReadOnlyCollection<Rating> Ratings  => _ratings;     
        public IReadOnlyCollection<Genre> Genres  => _genres;



        private Movie(
           Guid id, string title, int yearOfRelease
         )
            : base(id)
        {
            
            Title = title;
            YearOfRelease = yearOfRelease;
          
        }

        public static Movie Create(
            string title,int yearOfRelease,
           IEnumerable<Genre> genres)
        {
            var movie = new Movie(
                Guid.NewGuid(),title, yearOfRelease);

          movie._genres.AddRange(genres);
            return movie;
        }


        public Result AddRating(Guid userId, int score)
        {
            if (_ratings.Any(r => r.UserId == userId))
            {
                return Result.Failure(
                    MovieErrors.AlreadyExistsRated);
            }

            var rating = Rating.Create(
                Id, userId, score);

            _ratings.Add(rating);

            return Result.Success();
        }
        public Result Update(
            string title, int yearOfRelease
            ,IEnumerable<Genre> genres)
        {
            if (!genres.Any())
            {
                return Result.Failure(MovieErrors.GenreNotFound);
            }
            Title = title;
           YearOfRelease= yearOfRelease;

            _genres.Clear();
            _genres.AddRange(genres);
            return Result.Success();
           
        }
    }
}
