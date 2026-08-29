
using FluentAssertions;
using MovieApp.Domain.Entitys.Genres;
using MovieApp.Domain.Entitys.Movies;
using MovieApp.Domain.Entitys.Ratings;


namespace MovieApp.UnitTests.Domain.Movies
{
    public class MovieTests
    {
        [Fact]
        public async Task Create_Shoulde_Create_Movie_Whith_DefaultValues()
        {
            //Arrange

            var genre = Genre.Create("Drama");

            //Act

           var movie = Movie.Create(
                "BateMan", 1998, [genre]);

            //Assert

            movie.Should().NotBeNull();
            movie.Id.Should().NotBe(Guid.Empty);
            movie.Title.Should().Be("BateMan");
            movie.YearOfRelease.Should().Be(1998);
            movie.Genres.Should().ContainSingle();

        }
        [Fact]
        public async Task Update_Should_Not_Update_Movie_When_Genre_DosNotExists()
        {
            //Arrange
            
            var genre = Genre.Create("Drama");

            var movie = Movie.Create(
                "BateMan", 1998 , [genre]);

            var emptyGenres = new List<Genre>();

            //Act

            var result = movie.Update(
                "BateMan", 1998, emptyGenres);

            //Assert

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(MovieErrors.GenreNotFound);
        }

        [Fact]
        public async Task Update_Should_Update_Movie_When_Genre_Exists()
        {
            //Arrange

            var genre = Genre.Create("Drama");

            var movie = Movie.Create(
                "BateMan", 1998, [genre]);

            var newGenre = Genre.Create("Comedy");

            //Act

            var result = movie.Update(
                "BateMan2", 2001, [newGenre]);

            //Assert

            result.IsSuccess.Should().BeTrue();
            movie.Title.Should().Be("BateMan2");
            movie.YearOfRelease.Should().Be(2001);
            movie.Genres.Should().ContainSingle();
            movie.Genres.Should().Contain(newGenre);
        }

        [Fact]
        public async Task AddOrUpdateRating_Should_Update_When_Rate_Exists()
        {
            //Arrange
            var genre = Genre.Create("Drama");
            var userId = Guid.NewGuid();

            var movie = Movie.Create(
                "BateMan", 1998, [genre]);

              movie.AddOrUpdateRating(userId, 8);
            
            //Act

            var result = movie.AddOrUpdateRating(
               userId, 9);

            //Assert

            result.IsSuccess.Should().BeTrue();
            movie.Ratings.Should().ContainSingle();

            var rating = movie.Ratings.Single();

            rating.UserId.Should().Be(userId);
            rating.Score.Should().Be(9);
        }
        public async Task AddOrUpdateRating_Should_Add_When_Rate_DosNotExists()
        {
            //Arrange
            var genre = Genre.Create("Drama");
            var userId = Guid.NewGuid();

            var movie = Movie.Create(
                "BateMan", 1998, [genre]);
            //Act
            var result = movie.AddOrUpdateRating(
              userId, 9);
            //Assert
            result.IsSuccess.Should().BeTrue();

            var rating = movie.Ratings.Single();
            rating.Score.Should().Be(9);
            rating.UserId.Should().Be(userId);
        }

    }
}
