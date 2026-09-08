
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using MovieApp.Application.Movies.CreateMovie;
using MovieApp.Domain.Entitys.Genres;
using MovieApp.Domain.Entitys.Movies;
using MovieApp.Domain.Entitys.Movies.Event;
using MovieApp.IntegrationTests.InfraStructure;

namespace MovieApp.IntegrationTests.Application
{
    public class CreateMovieCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Create_Movie_When_Genre_Exist()
        {
            //Arrange
            using var context = TestDbContextFactory.Create();

            var genre = Genre.Create("Drama");

            context.Genres.Add(genre);
            await context.SaveChangesAsync();

            var logger = NullLogger<CreateMovieCommandHandler>.Instance;

            var handler = new CreateMovieCommandHandler(context, logger);

            var command = new CreateMovieCommand([genre.Id], "BateMan", 1998);


            //Act

            var result =await handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();

            var movie = await context.Movies
                .Include(x => x.Genres)
                .FirstOrDefaultAsync(x => x.Title == "BateMan");

            movie.Should().NotBeNull();
            movie.YearOfRelease.Should().Be(1998);

            movie.Genres.Should().ContainSingle();
            movie.Genres.Single().Id.Should().Be(genre.Id);

        }
         
        [Fact]
        public async Task Handle_Should_Create_Movie_When_Genre_DosNotExist()
        {
            //Arrange
            using var context = TestDbContextFactory.Create();

            var genre = Genre.Create("Drama");

            context.Genres.Add(genre);
            await context.SaveChangesAsync();

            var logger = NullLogger<CreateMovieCommandHandler>.Instance;

            var handler = new CreateMovieCommandHandler(context, logger);

            var command = new CreateMovieCommand([Guid.NewGuid()], "BateMan", 1998);

            //Act

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(MovieErrors.GenreNotFound);
        }

        [Fact]
        public void Create_Should_Raise_MovieCreatedDomainEvent()
        {
            // Arrange
            var genre = Genre.Create("Drama");

            // Act
            var movie = Movie.Create(
                "Batman",
                1998,
                [genre]);

            // Assert
            movie.DomainEvents.Should().ContainSingle();

            movie.DomainEvents
                .Single()
                .Should()
                .BeOfType<MovieCreatedDomainEvent>();
        }
    }
}
