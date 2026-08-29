
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using MovieApp.Application.Authentication.Abstractions;
using MovieApp.Application.Movies.RateMovie;
using MovieApp.Domain.Entitys.Genres;
using MovieApp.Domain.Entitys.Movies;
using MovieApp.IntegrationTests.InfraStructure;

namespace MovieApp.IntegrationTests.Application
{
    public class RateMovieCommandHandlerTest
    {
        [Fact]
        public async Task Handle_Should_ReturnFailure_When_Movie_NotFound()
        {
            //Arrang
            using var context = TestDbContextFactory.Create();
            var movieId = Guid.NewGuid();

            var userId = Guid.NewGuid();
            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(x => x.UserId).Returns(userId);

            var command = new RateMovieCommand(movieId, 8);

            var handler = new RateMovieCommandHandler(
                context, userContextMock.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);
            //Assert

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(
                   MovieErrors.NotFound(movieId));
        }


        [Fact]
        public async Task Handle_Should_AddRating_When_Movie_Exists()
        {
            //Arrange
            using var context = TestDbContextFactory.Create();

            var genre = Genre.Create("Drama");
            var movie = Movie.Create("BateMan", 1998, [genre]);

            context.Genres.Add(genre);
            context.Movies.Add(movie);

            await context.SaveChangesAsync();

            var userId = Guid.NewGuid();
            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup( x => x.UserId).Returns(userId);

            var command = new RateMovieCommand(movie.Id , 8);

            var handler = new RateMovieCommandHandler(context, userContextMock.Object);

            //Act

            var result = await handler.Handle(command, CancellationToken.None);
            //Assert

            result.IsSuccess.Should().BeTrue();

            var rating = await context.Ratings
                .FirstOrDefaultAsync(x => 
                x.UserId == userId && x.MovieId == movie.Id);

            rating.Should().NotBeNull();
            rating.Score.Should().Be(8);

        }

        [Fact]
        public async Task Handle_Should_UpdateRating_When_User_Already_RatedMovie()
        {
            //Arrange
            using var context = TestDbContextFactory.Create();

            var userId = Guid.NewGuid();
            var genre = Genre.Create("Drama");
            var movie = Movie.Create("BateMan", 1998, [genre]);

            var oldRating = movie.AddOrUpdateRating(userId, 8);

            context.Genres.Add(genre);
            context.Movies.Add(movie);
            context.Ratings.Add(oldRating.Value!.Rating);

            await context.SaveChangesAsync();

            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(x => x.UserId).Returns(userId);

            var command = new RateMovieCommand(movie.Id, 10);

            var handler = new RateMovieCommandHandler(context, userContextMock.Object);

            //Act

            var result = await handler.Handle(command, CancellationToken.None);
            //Assert

            result.IsSuccess.Should().BeTrue();

            var rating = await context.Ratings
                .Where(x =>
                x.UserId == userId && x.MovieId == movie.Id).ToListAsync();

            rating.Should().ContainSingle();
            rating.Single().Score.Should().Be(10);

        }

    }
}
