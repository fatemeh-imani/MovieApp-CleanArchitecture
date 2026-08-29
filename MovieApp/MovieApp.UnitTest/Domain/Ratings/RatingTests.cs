

using FluentAssertions;
using MovieApp.Domain.Entitys.Ratings;

namespace MovieApp.UnitTests.Domain.Ratings
{
    public class RatingTests
    {
        [Fact]
        public void Create_Should_Create_Rating_With_DefaultValues()
        {
            // Arrange
            var movieId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            // Act
            var rating = Rating.Create(
                movieId,
                userId,
                8);

            // Assert
            rating.Should().NotBeNull();
            rating.Id.Should().NotBe(Guid.Empty);
            rating.MovieId.Should().Be(movieId);
            rating.UserId.Should().Be(userId);
            rating.Score.Should().Be(8);
        }
        [Fact]
        public void Update_Should_Update_Rating_Score()
        {
            // Arrange
            var movieId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var rating = Rating.Create(
                movieId,
                userId,
                8);

            // Act
            rating.Update(10);

            // Assert
            rating.Score.Should().Be(10);
        }
    }
}
