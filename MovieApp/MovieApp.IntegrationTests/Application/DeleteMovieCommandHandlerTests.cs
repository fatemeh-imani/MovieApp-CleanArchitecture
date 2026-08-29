
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using MovieApp.Application.Movies.DeleteMovie;
using MovieApp.Domain.Entitys.Genres;
using MovieApp.Domain.Entitys.Movies;
using MovieApp.IntegrationTests.InfraStructure;

namespace MovieApp.IntegrationTests.Application
{
    public class DeleteMovieCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Return_Failure_When_Movie_NotFound()
        {
            //Arrange
            using var context = TestDbContextFactory.Create();
            var movieId = Guid.NewGuid();
            var logger = NullLogger<DeleteMovieCommandHandler>.Instance;

            var handle = new DeleteMovieCommandHandler(context, logger);

            var command = new DeleteMovieCommand(movieId);

            //Act
             var result = await handle.Handle(command,CancellationToken.None);

            //Assert

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(MovieErrors.NotFound(movieId));
        }

        [Fact]
        public async Task Handle_Should_Delete_Movie()
        {
            //Arrange
            using var context = TestDbContextFactory.Create();

            var genre = Genre.Create("Drama");
            context.Genres.Add(genre);
           

            var movie = Movie.Create("BateMan", 1998, [genre]);
            context.Movies.Add(movie);
            await context.SaveChangesAsync();

            var logger = NullLogger<DeleteMovieCommandHandler>.Instance;

            var handle = new DeleteMovieCommandHandler(context, logger);

            var command = new DeleteMovieCommand(movie.Id);

            //Act
            var result = await handle.Handle(command, CancellationToken.None);

            //Assert

            result.IsSuccess.Should().BeTrue();


            var deleteMovie = await context.Movies
                .FirstOrDefaultAsync(x => x.Id == movie.Id);

            deleteMovie.Should().BeNull();

            var movieFromDatabse = await context.Movies
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == movie.Id);

            movieFromDatabse.Should().NotBeNull();
            movieFromDatabse.IsDelete.Should().BeTrue();
        
        }

    }
}
