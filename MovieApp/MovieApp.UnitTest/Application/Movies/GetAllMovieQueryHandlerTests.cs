
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using MovieApp.SharedKernel.Results;
using MovieApp.Application.Abstractions.Context;
using MovieApp.Application.Genres.GetAllGenre;
using MovieApp.Application.Movies.GetAllMovie;
using MovieApp.Domain.Entitys.Genres;
using MovieApp.Domain.Entitys.Movies;

namespace MovieApp.UnitTests.Application.Movies
{
    public class GetAllMovieQueryHandlerTests
    {

        [Fact]
        public async Task Handle_Should_ReturnFilteredMovies_When_Search_IsProvided()
        {
            //Arrang
            var genre1 = Genre.Create("Drama");
            var genre2 = Genre.Create("Comedi");
          

            var movie1 = Movie.Create("BateMan", 1998, [genre1]);
            var movie2 = Movie.Create("SpiderMan", 2001, [genre2]);
            var movies = new List<Movie> { movie1, movie2 };

            var movieMock = movies.BuildMockDbSet();
            var contextMock = new Mock<IApplicationDbContext>();
            contextMock.Setup(x => x.Movies).Returns(movieMock.Object);

            var handler = new GetAllMovieQueryHandler(contextMock.Object);

            var query = new GetAllMovieQuery(
                new FilterMovie(
                    Search: "SpiderMan",
                   YearOfRelease: null,
                   SortBy: null, 
                   SortDescending:false,
                  Page: 1,
                  PageSize : 10));

            //Act

            var result =await handler.Handle(
                query, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Items.Should().HaveCount(1);
            result.Value.Items.Should().Contain(x => x.Title == "SpiderMan");
        }

        [Fact]
        public async Task Handle_Should_ReturnFilteredMovies_When_YearOfRelease_IsProvided()
        {
            //Arrang
            var genre1 = Genre.Create("Drama");
            var genre2 = Genre.Create("Comedi");


            var movie1 = Movie.Create("BateMan", 1998, [genre1]);
            var movie2 = Movie.Create("SpiderMan", 2001, [genre2]);
            var movies = new List<Movie> { movie1, movie2 };

            var movieMock = movies.BuildMockDbSet();
            var contextMock = new Mock<IApplicationDbContext>();
            contextMock.Setup(x => x.Movies).Returns(movieMock.Object);

            var handler = new GetAllMovieQueryHandler(contextMock.Object);

            var query = new GetAllMovieQuery(
                new FilterMovie(
                    Search: null,
                   YearOfRelease: 2001,
                   SortBy: null,
                   SortDescending: false,
                  Page: 1,
                  PageSize: 10));

            //Act

            var result = await handler.Handle(
                query, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Items.Should().HaveCount(1);
            result.Value.Items.Should().Contain(x => x.Title == "SpiderMan");
        }
        [Fact]
        public async Task Handle_Should_ReturnSortedMovies_By_Title_Ascending()
        {
            //Arrang
            var genre1 = Genre.Create("Drama");
            var genre2 = Genre.Create("Comedi");


            var movie1 = Movie.Create("BateMan", 1998, [genre1]);
            var movie2 = Movie.Create("SpiderMan", 2001, [genre2]);
            var movies = new List<Movie> { movie1, movie2 };

            var movieMock = movies.BuildMockDbSet();
            var contextMock = new Mock<IApplicationDbContext>();
            contextMock.Setup(x => x.Movies).Returns(movieMock.Object);

            var handler = new GetAllMovieQueryHandler(contextMock.Object);

            var query = new GetAllMovieQuery(
                new FilterMovie(
                    Search: null,
                   YearOfRelease: null,
                   SortBy: "title",
                   SortDescending: false,
                  Page: 1,
                  PageSize: 10));

            //Act

            var result = await handler.Handle(
                query, CancellationToken.None);

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
            var genre1 = Genre.Create("Drama");
            var genre2 = Genre.Create("Comedi");


            var movie1 = Movie.Create("BateMan", 1998, [genre1]);
            var movie2 = Movie.Create("SpiderMan", 2001, [genre2]);
            var movies = new List<Movie> { movie1, movie2 };

            var movieMock = movies.BuildMockDbSet();
            var contextMock = new Mock<IApplicationDbContext>();
            contextMock.Setup(x => x.Movies).Returns(movieMock.Object);

            var handler = new GetAllMovieQueryHandler(contextMock.Object);

            var query = new GetAllMovieQuery(
                new FilterMovie(
                    Search: null,
                   YearOfRelease: null,
                   SortBy: "title",
                   SortDescending: true,
                  Page: 1,
                  PageSize: 10));

            //Act

            var result = await handler.Handle(
                query, CancellationToken.None);

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
            var genre1 = Genre.Create("Drama");
            var genre2 = Genre.Create("Comedi");


            var movie1 = Movie.Create("BateMan", 1998, [genre1]);
            var movie2 = Movie.Create("SpiderMan", 2001, [genre2]);
            var movies = new List<Movie> { movie1, movie2 };

            var movieMock = movies.BuildMockDbSet();
            var contextMock = new Mock<IApplicationDbContext>();
            contextMock.Setup(x => x.Movies).Returns(movieMock.Object);

            var handler = new GetAllMovieQueryHandler(contextMock.Object);

            var query = new GetAllMovieQuery(
                new FilterMovie(
                    Search: null,
                   YearOfRelease: null,
                   SortBy: null,
                   SortDescending: false,
                  Page: 1,
                  PageSize: 2));

            //Act

            var result = await handler.Handle(
                query, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.TotalCount.Should().Be(2);
            result.Value.Items.Should().HaveCount(2);
            result.Value.Items
                .First().YearOfRelease.Should().Be(1998);
        }
        [Fact]
        public async Task Handle_Should_ProjectMovies_WithAverageRating()
        {
            //Arrang
            var genre = Genre.Create("Comedi");
                        
            var movie = Movie.Create("SpiderMan", 2001, [genre]);
            movie.AddOrUpdateRating(Guid.NewGuid(),8);
            movie.AddOrUpdateRating(Guid.NewGuid(),10);

            var movies = new List<Movie> { movie };

            var movieMock = movies.BuildMockDbSet();
            var contextMock = new Mock<IApplicationDbContext>();
            contextMock.Setup(x => x.Movies).Returns(movieMock.Object);

            var handler = new GetAllMovieQueryHandler(contextMock.Object);

            var query = new GetAllMovieQuery(
                new FilterMovie(
                    Search: null,
                   YearOfRelease: null,
                   SortBy: null,
                   SortDescending: false,
                  Page: 1,
                  PageSize: 10));

            //Act

            var result = await handler.Handle(
                query, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();

            //result.Value.Items.Should().ContainSingle();

            //result.Value.Items.Single().AverageRating.Should().Be(9);

            result.Value.Items.Should().ContainSingle(x =>
                     x.Title == "SpiderMan" &&
                      x.AverageRating == 9);
        }

    }
}
