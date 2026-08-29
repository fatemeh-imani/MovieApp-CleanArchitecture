
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using MovieApp.Application.Abstractions.Context;
using MovieApp.Application.Authentication.Abstractions;
using MovieApp.Application.Movies.RateMovie;
using MovieApp.Domain.Entitys.Genres;
using MovieApp.Domain.Entitys.Movies;
using MovieApp.Domain.Entitys.Ratings;

namespace MovieApp.UnitTests.Application.Movies
{
    public class RateMovieCommandHandlerTests
    {
        [Fact]
        public async Task Handler_Should_ReturnFailure_When_Movie_NotFound()
        { 
            var movieId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var movies =new  List<Movie>();
            var movieMock = movies.BuildMockDbSet();

            var contextMock = new Mock<IApplicationDbContext>();
            contextMock.Setup(x => x.Movies).Returns(movieMock.Object);

            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(x => x.UserId).Returns(userId);

            var handler = new RateMovieCommandHandler(
                contextMock.Object, userContextMock.Object);

            var command = new RateMovieCommand(movieId, 8);

            //Act

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(MovieErrors.NotFound(movieId));


        }
        [Fact]
        public async Task Handler_Should_ReturnSuccess_When_Movie_Exists()
        {
            var contextMock = new Mock<IApplicationDbContext>();

            var genre = Genre.Create("Drama");
            var movie = Movie.Create("BateMan", 1998, [genre]);
            var movies = new List<Movie> { movie};

            var movieMock = movies.BuildMockDbSet();
            contextMock.Setup(x => x.Movies).Returns(movieMock.Object);

            var rating = new List<Rating>();
            var ratingMock = rating.BuildMockDbSet();
            contextMock.Setup(x => x.Ratings).Returns(ratingMock.Object);

            var userId = Guid.NewGuid();
            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(x => x.UserId).Returns(userId);

            var handler = new RateMovieCommandHandler(
               contextMock.Object, userContextMock.Object);

            var command = new RateMovieCommand(movie.Id, 8);
            //Act
            var result = await handler.Handle(command, CancellationToken.None);
            //Assert
            result.IsSuccess.Should().BeTrue();

            contextMock.Verify(x =>
              x.Ratings.Add(It.IsAny<Rating>()), Times.Once());

            contextMock.Verify(x => 
              x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());

          
        }
    }
}
