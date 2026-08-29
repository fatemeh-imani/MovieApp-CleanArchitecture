
using FluentAssertions;
using MovieApp.Application.Movies.GetAllMovie;
using MovieApp.Domain.Entitys.Genres;
using MovieApp.Domain.Entitys.Movies;
using MovieApp.IntegrationTests.InfraStructure;

namespace MovieApp.IntegrationTests.Application
{
    public class GetAllMovieQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Should_ReturnFilteredMovies_When_Search_IsProvided()
        {
            //Arrang
            using var context = TestDbContextFactory.Create();
            var genre = Genre.Create("Comedy");

            context.Genres.Add(genre);

            var movie = Movie.Create("SpiderMan",2001,[genre]);

            context.Movies.Add(movie);

            await context.SaveChangesAsync();

            var handler = new GetAllMovieQueryHandler(context);

            var query = new GetAllMovieQuery(
                new FilterMovie(
                    Search: "SpiderMan",
                    YearOfRelease: null,
                    SortBy: null,
                    SortDescending: false,
                    Page: 1,
                    PageSize: 10));

            // Act
            var result = await handler.Handle(
                query,
                CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Items.Should().HaveCount(1);
            result.Value.Items.Should().Contain(x => x.Title == "SpiderMan");
        }

        [Fact]
        public async Task Handle_Should_ReturnFilteredMovies_When_YearOfRelease_IsProvided()
        {
            //Arrang
            using var context = TestDbContextFactory.Create();
            var genre = Genre.Create("Comedy");

            context.Genres.Add(genre);

            var movie = Movie.Create("SpiderMan", 2001, [genre]);

            context.Movies.Add(movie);

            await context.SaveChangesAsync();

            var handler = new GetAllMovieQueryHandler(context);

            var query = new GetAllMovieQuery(
                new FilterMovie(
                    Search: null,
                    YearOfRelease: 2001,
                    SortBy: null,
                    SortDescending: false,
                    Page: 1,
                    PageSize: 10));

            // Act
            var result = await handler.Handle(
                query,
                CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Items.Should().HaveCount(1);
            result.Value.Items.Should().Contain(x => x.YearOfRelease == 2001);
        }

        [Fact]
        public async Task Handle_Should_ReturnSortedMovies_By_Title_Ascending()
        {
            //Arrang
            using var context = TestDbContextFactory.Create();
            var genre1 = Genre.Create("Comedy");
            var genre2 = Genre.Create("Drama");

            context.Genres.AddRange(genre1,genre2);

            var movie1 = Movie.Create("SpiderMan", 2001, [genre1]);
            var movie2 = Movie.Create("BateMan", 1998, [genre2]);

            context.Movies.AddRange(movie1,movie2);

            await context.SaveChangesAsync();

            var handler = new GetAllMovieQueryHandler(context);

            var query = new GetAllMovieQuery(
                new FilterMovie(
                    Search: null,
                    YearOfRelease: null,
                    SortBy: "title",
                    SortDescending: false,
                    Page: 1,
                    PageSize: 10));

            // Act
            var result = await handler.Handle(
                query,
                CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Items.Should().HaveCount(2);
            result.Value.Items[0].Title.Should().Be("BateMan");
            result.Value.Items[1].Title.Should().Be("SpiderMan");
        }
        [Fact]
        public async Task Handle_Should_ReturnSortedMovies_By_Title_Descending()
        {
            //Arrang
            using var context = TestDbContextFactory.Create();
            var genre1 = Genre.Create("Comedy");
            var genre2 = Genre.Create("Drama");

            context.Genres.AddRange(genre1, genre2);

            var movie1 = Movie.Create("SpiderMan", 2001, [genre1]);
            var movie2 = Movie.Create("BateMan", 1998, [genre2]);

            context.Movies.AddRange(movie1, movie2);

            await context.SaveChangesAsync();

            var handler = new GetAllMovieQueryHandler(context);

            var query = new GetAllMovieQuery(
                new FilterMovie(
                    Search: null,
                    YearOfRelease: null,
                    SortBy: "title",
                    SortDescending: true,
                    Page: 1,
                    PageSize: 10));

            // Act
            var result = await handler.Handle(
                query,
                CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Items.Should().HaveCount(2);
            result.Value.Items[0].Title.Should().Be("SpiderMan");
            result.Value.Items[1].Title.Should().Be("BateMan");
        }

        [Fact]
        public async Task Handle_Should_ReturnCorrectPage_When_Pagination_IsProvided()
        {
            //Arrang
            using var context = TestDbContextFactory.Create();

            var genre1 = Genre.Create("Comedy");
            var genre2 = Genre.Create("Drama");

            context.Genres.AddRange(genre1, genre2);

            var movie1 = Movie.Create("SpiderMan", 2001, [genre1]);
            var movie2 = Movie.Create("BateMan", 1998, [genre2]);
            var movie3 = Movie.Create("SuperMan", 2005, [genre1]);

            context.Movies.AddRange(movie1, movie2, movie3);

            await context.SaveChangesAsync();

            var handler = new GetAllMovieQueryHandler(context);

            var query = new GetAllMovieQuery(
                new FilterMovie(
                    Search: null,
                    YearOfRelease: null,
                    SortBy: null,
                    SortDescending: false,
                    Page: 2,
                    PageSize: 2));

            // Act
            var result = await handler.Handle(
                query,
                CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Items.Should().ContainSingle();
            result.Value.TotalCount.Should().Be(3);
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

            movie.AddOrUpdateRating(Guid.NewGuid(), 8);
            movie.AddOrUpdateRating(Guid.NewGuid(), 10);

            context.Movies.Add(movie);

            await context.SaveChangesAsync();

            var handler = new GetAllMovieQueryHandler(context);

            var query = new GetAllMovieQuery(
                new FilterMovie(
                    Search: null,
                    YearOfRelease: null,
                    SortBy: null,
                    SortDescending: false,
                    Page: 1,
                    PageSize: 10));

            // Act
            var result = await handler.Handle(
                query,
                CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();

            result.Value.Items.Should().ContainSingle();

            var movieResponse = result.Value.Items.Single();

            movieResponse.Title.Should().Be("SpiderMan");
            movieResponse.AverageRating.Should().Be(9);
        }
    }
}
