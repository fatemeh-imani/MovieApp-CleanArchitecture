using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using MovieApp.Application.Abstractions.Context;
using MovieApp.Application.Movies.GetAllMovie;
using MovieApp.Application.Movies.GetByIdMovie;
using MovieApp.Domain.Entitys.Genres;
using MovieApp.Domain.Entitys.Movies;

namespace MovieApp.UnitTests.Application.Movies
{
    public class GetByIdMovieQueryHAndlerTest
    {
        [Fact]
        public async Task Handle_Should_ReturnFailure_When_Mvoie_NotFound()
        {
            //Arrang
            var movieId = Guid.NewGuid();

            var movies = new List<Movie>();

            var movieMock = movies.BuildMockDbSet();
            var contextMock = new Mock<IApplicationDbContext>();

            contextMock.Setup(x => x.Movies).Returns(movieMock.Object);

            var handler = new GetByIdMovieQueryHandler(
                contextMock.Object);

            var query = new GetByIdMovieQuery(movieId);
            //Act

            var result = await handler.Handle(query,CancellationToken.None);

            //Assert

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(MovieErrors.NotFound);

        }
        [Fact]
         public async Task Handle_Should_ReturnSuccess_When_Mvoie_Existed()
        {
            //Arrang
            var genre = Genre.Create("Drama");
            var movie = Movie.Create("BateMan", 1998, [genre]);
            var movies = new List<Movie> { movie};

            var movieMock = movies.BuildMockDbSet();
            var contextMock = new Mock<IApplicationDbContext>();

            contextMock.Setup(x => x.Movies).Returns(movieMock.Object);

            var handler = new GetByIdMovieQueryHandler(
                contextMock.Object);

            var query = new GetByIdMovieQuery(movie.Id);
            //Act

            var result = await handler.Handle(query, CancellationToken.None);

            //Assert

            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();

            result.Value.Title.Should().Be(movie.Title);

            result.Value.Genres.Should().ContainSingle();
            result.Value.Genres[0].Title.Should().Be(movie.Title);

        }
        [Fact]
        public async Task Handle_Should_ProjectMovies_WithAverageRating()
        {
            //Arrang
            var genre = Genre.Create("Comedi");

            var movie = Movie.Create("SpiderMan", 2001, [genre]);
            movie.AddOrUpdateRating(Guid.NewGuid(), 8);
            movie.AddOrUpdateRating(Guid.NewGuid(), 10);

            var movies = new List<Movie> { movie };

            var movieMock = movies.BuildMockDbSet();
            var contextMock = new Mock<IApplicationDbContext>();
            contextMock.Setup(x => x.Movies).Returns(movieMock.Object);

            var handler = new GetByIdMovieQueryHandler(contextMock.Object);

            var query = new GetByIdMovieQuery(movie.Id);

            //Act

            var result = await handler.Handle(
                query, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();

            result.Value.Title.Should().Be("SpidrMan");
            result.Value.AverageRating.Should().Be(9);
        }

    }
}
