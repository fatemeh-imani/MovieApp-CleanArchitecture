using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Application.CommonResponse;
using MovieApp.Domain.Entitys.Genres;
using MovieApp.Domain.Entitys.Movies;
using MovieApp.Infrastructure.Persistence;
using MovieApp.IntegrationTests.Infrastructure;
using MovieApp.IntegrationTests.InfraStructure;
using System.Net;
using System.Net.Http.Json;

namespace MovieApp.IntegrationTests.Api
{
    public class MovieEndpointsTests(
        CustomWebApplicationFactory factory)
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task GetAllMovie_Should_ReturnOk()
        {
            // Arrange
            using var scope =
                factory.Services.CreateScope();

            var context =
                scope.ServiceProvider
                    .GetRequiredService<ApplicationDbContext>();

            var genre = Genre.Create("Drama");

            var movie = Movie.Create(
                "Bateman",
                1998,
                [genre]);

            context.Genres.Add(genre);
            context.Movies.Add(movie);

            await context.SaveChangesAsync();

            //Act

            var response = await _client.GetAsync("/api/movies");

            // Assert
            response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            var movies =
                await response.Content
                    .ReadFromJsonAsync<List<MovieResponse>>();

            movies.Should().NotBeNull();

            movies!.Should().ContainSingle();

            var result = movies.Single();

           result.Title.Should().Be("Bateman");

           result.YearOfRelease.Should().Be(1998);

            result.Genres.Should().ContainSingle("Drama");
        }
       
        [Fact]
        public async Task CreateMovie_Should_CreateMovie_When_GenresExist()
        {
            //Arrange
            using var scope = factory.Services.CreateScope();

            var context = scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();

            var genre = Genre.Create("Drama");

            context.Genres.Add(genre);
            await context.SaveChangesAsync();

            var reques = new
            {
                title = "Bateman",
                yearOfRelease = 1998,

                genres = new[]
               {
                   genre.Id
               }
            };

            //Act

            var response = await _client.PostAsJsonAsync(
                                "/api/movies",reques);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var createdMovie = await response.Content
                .ReadFromJsonAsync<MovieResponse>();

            createdMovie.Should().NotBeNull();

            createdMovie!.Title.Should().Be("Bateman");

            createdMovie.YearOfRelease.Should().Be(1998);

            createdMovie.Genres.Should().ContainSingle("Drama");
        }

        [Fact]
        public async Task CreateMovie_ReturnNotFound_When_GenreNotFound()
        {
            //Arrange

            var request = new
            {
                title = "Bateman",
                yearOfRelease = 1998,
                genres = new[] { Guid.NewGuid() }
            };

            //Act

            var responce = await _client.PostAsJsonAsync(
                "/api/movies",request);

            //Assert

            responce.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

       [Fact]
        public async Task UpdateMovie_Should_UpdateMovie()
        {
            // Arrange

            using var scope =
                factory.Services.CreateScope();

            var context =
                scope.ServiceProvider
                    .GetRequiredService<ApplicationDbContext>();

            var genre1 = Genre.Create("Drama");
            var genre2 = Genre.Create("Comedy");

            var movie = Movie.Create(
                "Bateman",
                1998,
                [genre1]);

            context.Genres.AddRange(
                genre1,
                genre2);

            context.Movies.Add(movie);

            await context.SaveChangesAsync();

            var request = new
            {
                title = "Batman",
                yearOfRelease = 2000,
                genreIds = new[]
                {
                    genre2.Id
                }
            };

            // Act

            var response =
                await _client.PutAsJsonAsync(
                    $"/movies/{movie.Id}",
                    request);

            // Assert

            response.StatusCode
                .Should()
                .Be(HttpStatusCode.OK);

            var movieFromDatabase =
                await context.Movies
                    .Include(x => x.Genres)
                    .FirstOrDefaultAsync(
                        x => x.Id == movie.Id);

            movieFromDatabase.Should().NotBeNull();

            movieFromDatabase!.Title
                .Should()
                .Be("Batman");

            movieFromDatabase.YearOfRelease
                .Should()
                .Be(2000);

            movieFromDatabase.Genres
                .Should()
                .ContainSingle(x => x.Id == genre2.Id);
        }

        [Fact]
        public async Task DeleteMovie_Should_DeleteMovie()
        {
            // Arrange

            using var scope =
                factory.Services.CreateScope();

            var context =
                scope.ServiceProvider
                    .GetRequiredService<ApplicationDbContext>();

            var genre1 = Genre.Create("Drama");
           
            var movie = Movie.Create(
                "Bateman",
                1998,
                [genre1]);

            context.Genres.AddRange(
                genre1);

            context.Movies.Add(movie);

            await context.SaveChangesAsync();

            //Act
            var responce =
               await _client.DeleteAsync(
                    $"/movies/{movie.Id}");

            //Assert

           responce.StatusCode.Should().Be(HttpStatusCode.OK);

            var movieFromDatabase = await context.Movies
                .FirstOrDefaultAsync(x => x.Id ==  movie.Id);

            movieFromDatabase.Should().BeNull();

            var deletedMovie = 
                 await context.Movies
                 .IgnoreQueryFilters()
                 .FirstOrDefaultAsync(x => x.Id == movie.Id);

            deletedMovie.Should().NotBeNull();

            deletedMovie!.IsDelete.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteMovie_Should_ReturnBadRequest_When_MovieDoesNotExist()
        {
            //Arrange

            var movieId = Guid.NewGuid();

            //Act

            var response = await _client.DeleteAsync(
                $"/movies/{movieId}");

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
