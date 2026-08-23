using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using MockQueryable.Moq;
using Moq;
using MoviApp.SharedKernel.Result;
using MovieApp.Application.Abstractions.Context;
using MovieApp.Application.Movies.UpdateMovie;
using MovieApp.Domain.Entitys.Genres;
using MovieApp.Domain.Entitys.Movies;

namespace MovieApp.UnitTests.Application.Movies
{
    public class UpdateMovieCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_ReturnFailure_When_Movie_DoseNotExist()
        {
            //Arrang
            var movies = new List<Movie>();
            var moviesMock = movies.BuildMockDbSet();

            var genre = Genre.Create("Derama");
            var genres = new List<Genre> { genre};

            var contextMock = new Mock<IApplicationDbContext>();
                contextMock.Setup(x => x.Movies).Returns(moviesMock.Object);

            var logger = NullLogger<UpdateMovieCommandHandler>.Instance;

            var handler = new UpdateMovieCommandHandler(
                contextMock.Object, logger );

            var command = new UpdateMovieCommand(
                Guid.NewGuid(), "BateMan", 1998, [genre.Id]);
            //Act

            var result = await handler.Handle(
                command, CancellationToken.None );

            //Asser

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(MovieErrors.NotFound(command.MovieId));

            contextMock.Verify(
                x=> x.SaveChangesAsync(It.IsAny<CancellationToken>()),Times.Never);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_When_Genre_DoseNotExist()
        {
            //var genre = Genre.Create("Drama");
            var genres = new List<Genre>();
            var genresMock = genres.BuildMockDbSet();    

            var movie = Movie.Create(
               "BateMan", 1998, genres );
            var movies = new List<Movie> { movie };

            var movieMock = movies.BuildMockDbSet();

                var contextMock =  new Mock<IApplicationDbContext>();
                    contextMock.Setup(x => x.Movies)
                               .Returns(movieMock.Object);

                    contextMock.Setup(x => x.Genres)
                               .Returns(genresMock.Object);

                var logger = NullLogger<UpdateMovieCommandHandler>.Instance;
               
            var handler = new UpdateMovieCommandHandler(
                              contextMock.Object, logger );

            var command = new UpdateMovieCommand(
                movie.Id, "BateMan" ,1998, [Guid.NewGuid()]);

            //Act
             Result result = await handler.Handle(
                             command, CancellationToken.None );

            //Assert

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(GenreErrors.NotFound);

            contextMock.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>())
                      ,Times.Never());

        }
        
        [Fact]
        public async Task Handle_Should_ReturnSuccess_When_Movie_AND_Genre_Exist()
        {
            //Arrang
            var genre = Genre.Create("Drama");
            var genres = new List<Genre> { genre };

            var movie = Movie.Create("BateMan", 1998, [genre]);
            var Movies = new List<Movie> { movie };

            var genreMock = genres.BuildMockDbSet();
            var movieMock = Movies.BuildMockDbSet();

            var contextMock = new Mock<IApplicationDbContext>();

            contextMock.Setup(x => x.Genres).Returns(genreMock.Object);
            contextMock.Setup(x => x.Movies).Returns(movieMock.Object);

            var logger = NullLogger<UpdateMovieCommandHandler>.Instance;

            var handler = new UpdateMovieCommandHandler(
                          contextMock.Object, logger );

            var command = new UpdateMovieCommand(
                          movie.Id, "BateMan2", 2000, [genre.Id]);

            //Act
             Result result = await handler.Handle(
                 command, CancellationToken.None );

            //Assert

            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();

            contextMock.Verify(x => 
               x.SaveChangesAsync(It.IsAny<CancellationToken>()),
               Times.Once);
          
        }
    }
}
