using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using MockQueryable;
using MockQueryable.Moq;
using Moq;
using MovieApp.SharedKernel.Results;
using MovieApp.Application.Abstractions.Context;
using MovieApp.Application.Movies.DeleteMovie;
using MovieApp.Domain.Entitys.Genres;
using MovieApp.Domain.Entitys.Movies;

namespace MovieApp.UnitTests.Application.Movies
{
    public class DeleteMovieCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_ReturnFailure_When_Movie_NotFound()
        {
            //Arrange
            var movies = new List<Movie>();

            var movieMcok = movies.BuildMockDbSet();

            var contextMock = new Mock<IApplicationDbContext>();
            contextMock.Setup(x => x.Movies)
                       .Returns(movieMcok.Object);

            var logger = NullLogger<DeleteMovieCommandHandler>.Instance;

            var handler = new DeleteMovieCommandHandler(
                contextMock.Object, logger);

            var command = new DeleteMovieCommand(Guid.NewGuid());

            //Act

            var result = await handler.Handle(command,CancellationToken.None);

            //Assert

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(MovieErrors.NotFound(command.MovieId));

            contextMock.Verify(x=>x.SaveChangesAsync(
                It.IsAny<CancellationToken>()),Times.Never);

        }
    
        [Fact]
        public async Task Handle_Should_ReturnSuccess_When_Movie_Deleted()
        {
            //Arrang
            var genre = Genre.Create("Drama");

            var movie = Movie.Create(
                "BateMan", 1998, [genre]);
            var movies= new List<Movie> { movie };
            var movieMock = movies.BuildMockDbSet();
            
            var contextMock = new Mock<IApplicationDbContext>();
                contextMock.Setup(x => x.Movies)
                           .Returns(movieMock.Object);
          
            var logger = NullLogger<DeleteMovieCommandHandler>.Instance;

            var handler = new DeleteMovieCommandHandler(
                contextMock.Object, logger );

            var command = new DeleteMovieCommand(movie.Id);

            //Act

            Result result = await handler.Handle(
                command, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
            movie.IsDelete.Should().BeTrue();
            contextMock.Verify(x=> 
            x.SaveChangesAsync(It.IsAny<CancellationToken>()),Times.Once);


        }
    }
}
