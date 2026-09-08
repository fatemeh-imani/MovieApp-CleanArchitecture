
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Entitys.Genres;
using MovieApp.Domain.Entitys.Movies;
using MovieApp.IntegrationTests.InfraStructure;

namespace MovieApp.IntegrationTests.Persistence
{
    public class MovieRelationshipTests
    {
        [Fact]
        public async Task Should_Save_Movie_With_Its_Genres()
        {
            //Arrange

            using var context = TestDbContextFactory.Create();

            var genre1 = Genre.Create("Drama");
            var genre2 = Genre.Create("Comedy");

            var movie = Movie.Create(
                "Bateman", 1998, [genre1, genre2]);

            context.Genres.AddRange(genre1, genre2);
            context.Movies.Add(movie);

           await context.SaveChangesAsync();

            //Act

            var movieFromDatabase = await context.Movies
                .Include(x => x.Genres)
                .FirstOrDefaultAsync(x => x.Id == movie.Id);

            //Assert
            movieFromDatabase.Should().NotBeNull();

            movieFromDatabase!.Genres.Should().HaveCount(2);

            movieFromDatabase.Genres.Should().Contain(x => x.Title == "Drama");

            movieFromDatabase.Genres.Should().Contain(x => x.Title == "Comedy");

        }

        [Fact]
        public async Task Should_Save_Movie_With_Its_Ratings()
        {
            //Arrange

            using var context = TestDbContextFactory.Create();

            var genre = Genre.Create("Drama");

            var movie = Movie.Create(
                "Bateman", 1998, [genre]);

            var rating1 = movie.AddOrUpdateRating(
                Guid.NewGuid(), 8);

            var rating2 = movie.AddOrUpdateRating(
                Guid.NewGuid(), 5);

            context.Genres.Add(genre);
            context.Movies.Add(movie);
            context.Ratings.AddRange(
                rating1.Value.Rating,
                rating2.Value.Rating);

            await context.SaveChangesAsync();

            //Act

            var movieFromDatabase =await context.Movies
                .Include(x => x.Ratings)
                .FirstOrDefaultAsync(x => x.Id == movie.Id);

            //Assert

            movieFromDatabase.Should().NotBeNull();

            movieFromDatabase.Ratings
                             .Should().Contain(x => x.Score == 8);

            movieFromDatabase.Ratings
                            .Should().Contain(x => x.Score == 5);

            movieFromDatabase.Ratings
                             .Should()
                             .OnlyContain(x => x.MovieId == movie.Id);
        }
    }
}
