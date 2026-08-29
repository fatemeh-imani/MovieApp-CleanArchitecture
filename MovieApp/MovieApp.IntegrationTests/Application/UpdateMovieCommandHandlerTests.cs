
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using MovieApp.Application.Movies.UpdateMovie;
using MovieApp.Domain.Entitys.Genres;
using MovieApp.Domain.Entitys.Movies;
using MovieApp.IntegrationTests.InfraStructure;

namespace MovieApp.IntegrationTests.Application
{
    public class UpdateMovieCommandHandlerTests
    {
        [Fact]
        public async Task  Handle_Should_UpdateMovie_When_MovieAndGenresExist()
        {
            // Arrange
          using  var context = TestDbContextFactory.Create();

            var genre1 = Genre.Create("Drama");
            var genre2 = Genre.Create("Comedy");

            context.Genres.AddRange(genre1,genre2);
          
            var movie = Movie.Create("BateMan", 1998, [genre1]);
            context.Movies.Add(movie);
            await context.SaveChangesAsync();

            var logger = NullLogger<UpdateMovieCommandHandler>.Instance;

            var handler = new UpdateMovieCommandHandler(context,logger);
            var comman = new UpdateMovieCommand(movie.Id, "BateMan2", 2001, [genre2.Id]);

            //Act
            var result = await handler.Handle(comman,CancellationToken.None);

            //Assert

            result.IsSuccess.Should().BeTrue();

            var updateMovie = await context.Movies
                .Include(x => x.Genres)
                .FirstOrDefaultAsync(x => x.Id == movie.Id);

            updateMovie.Title.Should().Be("BateMan2");
            updateMovie.YearOfRelease.Should().Be(2001);
            updateMovie.Genres.Should().ContainSingle();
            updateMovie.Genres.Single().Id.Should().Be(genre2.Id);
        }
        [Fact]
        public async Task Handle_Should_ReturnFailure_When_Movie_NotFound()
        {
            // Arrange
            using var context = TestDbContextFactory.Create();

           var movieId = Guid.NewGuid();

            var logger = NullLogger<UpdateMovieCommandHandler>.Instance;

            var handler = new UpdateMovieCommandHandler(context, logger);
            var comman = new UpdateMovieCommand(movieId, "BateMan2", 2001, []);

            //Act
            var result = await handler.Handle(comman, CancellationToken.None);

            //Assert

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(MovieErrors.NotFound(movieId));
        }
        [Fact]
        public async Task Handle_Should_ReturnFailure_When_Genre_NotFound()
        {
            // Arrange
            using var context = TestDbContextFactory.Create();

            var genre1 = Genre.Create("Drama");
            
            context.Genres.Add(genre1);

            var movie = Movie.Create("BateMan", 1998, [genre1]);
            context.Movies.Add(movie);
            await context.SaveChangesAsync();

            var logger = NullLogger<UpdateMovieCommandHandler>.Instance;

            var handler = new UpdateMovieCommandHandler(context, logger);
            var comman = new UpdateMovieCommand(movie.Id,"BateMan2",2001,[Guid.NewGuid()]);

            //Act
            var result = await handler.Handle(comman, CancellationToken.None);

            //Assert

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(GenreErrors.NotFound);
        }

    }   }
