
using FluentAssertions;
using MovieApp.Domain.Entitys.Genres;

namespace MovieApp.UnitTests.Domain.Genres
{
    public class GenreTests
    {
        [Fact]
        public void  Create_Shoud_Create_Genre_With_DefultValues()
        {
            //Arrange
            //Act
            var genre = Genre.Create("Drama");

            //Assert

            genre.Should().NotBeNull();
            genre.Id.Should().NotBe(Guid.Empty);
            genre.Title.Should().Be("Drama");

        }
        [Fact]
        public void  Update_Should_Update_Genre_Title()
        {
            //Arrange
            var genre = Genre.Create("Drama");

            //Act
             genre.Update("Comedi");

            //Assert

            genre.Title.Should().Be("Comedi");
            
        }
    }
}
