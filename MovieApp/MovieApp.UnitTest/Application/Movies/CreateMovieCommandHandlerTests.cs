using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using MovieApp.SharedKernel.Results;
using MovieApp.Application.Abstractions.Context;
using MovieApp.Application.Movies.CreateMovie;
using MovieApp.Domain.Entitys.Genres;
using MovieApp.Domain.Entitys.Movies;
using Microsoft.Extensions.Logging.Abstractions;



namespace MovieApp.UnitTest.Application.Movies
{
    public class CreateMovieCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_ReturnFailure_when_Genre_DoesNotExist()
        {
            //Arrange
            var genres = new List<Genre>();
            var genreMock = genres.BuildMockDbSet();

            var contextMock = new Mock<IApplicationDbContext>();
            contextMock.Setup(x => x.Genres)
                        .Returns(genreMock.Object);


            var logger =
                   NullLogger<CreateMovieCommandHandler>.Instance;

            var handler = new CreateMovieCommandHandler(
                contextMock.Object, logger);

            var command = new CreateMovieCommand(
             [Guid.NewGuid()], "BateMan", 1998);

            //Act

            Result result = await handler.Handle(
                command,CancellationToken.None );

            //Assert
            
            result.IsFailure.Should().BeTrue();

            result.Error.Should().Be(MovieErrors.GenreNotFound);

            contextMock.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess_When_Genre_Exist()
        {
            //Arrange
            var genre = Genre.Create("Deram");
            var genres = new List<Genre>
            { genre };
            var genresMock = genres.BuildMockDbSet();
            
            var movies = new List<Movie>();
            var moviesMock = movies.BuildMockDbSet();

            var contextMock = new Mock<IApplicationDbContext>();
            contextMock.Setup (x => x.Genres)
                     .Returns(genresMock.Object);

            contextMock.Setup(x => x.Movies)
                .Returns(moviesMock.Object);

            var logger = NullLogger<CreateMovieCommandHandler>.Instance;

            var handler = new CreateMovieCommandHandler(
                contextMock.Object, logger);


            var command = new CreateMovieCommand(
                [genre.Id], "BateMan", 1998);
            
            //Act

            Result result = await handler.Handle(
                command,CancellationToken.None );

            //Assert

            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();

            moviesMock.Verify(
                x => x.Add(It.IsAny<Movie>()), Times.Once);

            contextMock.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>())
                ,Times.Once);








        }
    }
}
