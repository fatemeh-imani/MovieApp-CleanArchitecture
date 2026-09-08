using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Entitys.Genres;
using MovieApp.Domain.Entitys.Movies;
using MovieApp.IntegrationTests.InfraStructure;

namespace MovieApp.IntegrationTests.Persistence
{
    public class MoviePersistenceTests
    {
        [Fact]
        public async Task Should_Not_Return_Deleted_Movie_Whith_Normal_Query()
        {
            //Arrange

            using var context = TestDbContextFactory.Create();

            var genre = Genre.Create("Drama");

            var movie = Movie.Create("Batema", 1998, [genre]);

            context.Genres.Add(genre);
            context.Movies.Add(movie);

            movie.Delete();

            await context.SaveChangesAsync();


            //Act

            var result = await context.Movies
                .FirstOrDefaultAsync(x => x.Id == movie.Id);

            //Assert

            result.Should().BeNull();
        }


        [Fact]
        public async Task Should_Return_Delete_Movie_When_QueryFilter_Is_Ignored()
        {
            //Arrang 
            using var context = TestDbContextFactory.Create();

            var genre = Genre.Create("Drama");
            var movie = Movie.Create(
                "Bateman", 1998, [genre]);

            context.Genres.Add(genre);
            context.Movies.Add(movie);
            movie.Delete();

            await context.SaveChangesAsync();

            //Act 
            var result = await context.Movies
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync();

            //Assert

            result.Should().NotBeNull();
            result.Id.Should().Be(movie.Id);
            result.IsDelete.Should().BeTrue();

        }

    }
}
