using FluentAssertions;
using MovieApp.Application.Movies.GetAllMovie;
using MovieApp.Application.Movies.GetByIdMovie;
using MovieApp.Domain.Entitys.Genres;
using MovieApp.Domain.Entitys.Movies;
using MovieApp.IntegrationTests.InfraStructure;
using SQLitePCL;

namespace MovieApp.IntegrationTests.Application
{
    public class GetByIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Should_ReturnFailure_When_Mvoie_NotFound()
        {
            //Arrang
            using var context = TestDbContextFactory.Create();

           var movieId = Guid.NewGuid();

            var query = new GetByIdMovieQuery(movieId);

            var handler = new GetByIdMovieQueryHandler(context);

            //Act
            var result = await handler.Handle(query,CancellationToken.None);

            //Assert

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(MovieErrors.NotFound(movieId));
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess_When_Mvoie_Existed()
        {
            //Arrang
            using var context = TestDbContextFactory.Create();

            var genre = Genre.Create("Drama");
            context.Genres.Add(genre);

            var movie = Movie.Create("BateMan", 1998, [genre]);
            context.Movies.Add(movie);

            context.SaveChanges();

            var query = new GetByIdMovieQuery(movie.Id);

            var handler = new GetByIdMovieQueryHandler(context);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert

            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();

            result.Value!.Title.Should().Be("BateMan");
            result.Value.Genres.Should().ContainSingle();
            result.Value.Genres[0].Title.Should().Be(genre.Title);
        }

        [Fact]
        public async Task Handle_Should_ReturnAverageRating()
        {
            // Arrange
            using var context = TestDbContextFactory.Create();

            var genre = Genre.Create("Comedy");

            context.Genres.Add(genre);

            var movie = Movie.Create(
                "SpiderMan",
                2001,
                [genre]);

           var rating1 = movie.AddOrUpdateRating(Guid.NewGuid(), 8);
           var rating2 = movie.AddOrUpdateRating(Guid.NewGuid(), 10);

            context.Ratings.AddRange(rating1.Value!.Rating, rating2.Value!.Rating);

            context.Movies.Add(movie);

            await context.SaveChangesAsync();

            var handler = new GetByIdMovieQueryHandler(context);

            var query = new GetByIdMovieQuery(movie.Id);

            // Act
            var result = await handler.Handle(
                query,
                CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();

            var movieResponse = result.Value;

            movieResponse.Title.Should().Be("SpiderMan");
            movieResponse.AverageRating.Should().Be(9);
        }

    }
}
