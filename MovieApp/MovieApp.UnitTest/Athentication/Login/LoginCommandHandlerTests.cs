using FluentAssertions;
using Moq;
using MoviApp.SharedKernel.Result;
using MovieApp.Application.Abstractions;
using MovieApp.Application.Abstractions.Login;
using MovieApp.Application.Authentication.Abstractions;

namespace MovieApp.UnitTests.Application.Authentication
{
    public class LoginCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_ReturnSuccess_When_Login_IsSuccessful()
        {
            // Arrange
            var identityServiceMock = new Mock<IIdentityService>();

            var expectedResult =
                Result<string>.Success("token");

            var email = "ali@test.com";
            var password = "123456";

            identityServiceMock
                .Setup(x => x.LoginAsync(
                    email,
                    password,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var handler = new LoginCommandHandler(
                identityServiceMock.Object);

            var command = new LoginCommand(
                email,
                password);

            // Act
            var result = await handler.Handle(
                command,
                CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be("token");

            identityServiceMock.Verify(
                x => x.LoginAsync(
                    email,
                    password,
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_When_Login_Fails()
        {
            // Arrange
            var identityServiceMock = new Mock<IIdentityService>();

            var email = "ali@test.com";
            var password = "wrong-password";

            var expectedResult =
                Result<string>.Failure(
                    IdentityErrors.InvalidCredentials);

            identityServiceMock
                .Setup(x => x.LoginAsync(
                    email,
                    password,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var handler = new LoginCommandHandler(
                identityServiceMock.Object);

            var command = new LoginCommand(
                email,
                password);

            // Act
            var result = await handler.Handle(
                command,
                CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should()
                .Be(IdentityErrors.InvalidCredentials);

            identityServiceMock.Verify(
                x => x.LoginAsync(
                    email,
                    password,
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}