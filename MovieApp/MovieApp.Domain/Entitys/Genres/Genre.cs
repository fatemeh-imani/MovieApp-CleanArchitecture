
using MoviApp.SharedKernel.Entitys;
using MoviApp.SharedKernel.Result;
using MovieApp.Domain.Entitys.Movies;

namespace MovieApp.Domain.Entitys.Genres
{
    public sealed class Genre : Entity
    {
        public string Title { get;private set; }
        public ICollection<Movie> Movies { get; private set; } = [];
      
        private Genre( Guid Id, string title):base(Id)
        {
            Title = title;
        }

        public static Genre Create(string title)
        {

            var genre = new Genre(Guid.NewGuid(),title);
            return genre;
        }

        public void Update(string title)
        {
            
            Title = title;

        }
    }
}
