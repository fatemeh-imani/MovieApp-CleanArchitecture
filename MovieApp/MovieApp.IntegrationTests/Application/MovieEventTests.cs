using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Domain.Entitys.Genres;
using MovieApp.Domain.Entitys.Movies;
using MovieApp.Infrastructure.Persistence;
using MovieApp.IntegrationTests.Infrastructure;


namespace MovieApp.IntegrationTests.Application;

public class MovieEventTests(
      CustomWebApplicationFactory factory)
        : IClassFixture<CustomWebApplicationFactory>
{
    [Fact]
    public async Task Should_Publish_MovieCreatedDomainEvent_After_SaveChanges()
    {
        // Arrange
        using var scope =
             factory.Services.CreateScope();

        var context =
            scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();

        await context.Database.EnsureCreatedAsync();

        var genre = Genre.Create("Drama");

        var movie = Movie.Create(
            "Bateman",
            1998,
            [genre]);

        context.Genres.Add(genre);
        context.Movies.Add(movie);

        // Act
        await context.SaveChangesAsync();

        // Assert
        movie.DomainEvents.Should().BeEmpty();
    }
}