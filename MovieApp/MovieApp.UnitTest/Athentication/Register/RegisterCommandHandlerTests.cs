using FluentAssertions;
using Moq;
using MoviApp.SharedKernel.Result;
using MovieApp.Application.Abstractions;
using MovieApp.Application.Abstractions.Register;
using MovieApp.Application.Authentication.Abstractions;

namespace MovieApp.UnitTests.Application.Authentication
{
    public class RegisterCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_ReturnSuccess_When_Register_IsSuccessful()
        {
            // Arrange
            var identityServiceMock = new Mock<IIdentityService>();

            var email = "ali@test.com";
            var password = "123456";
            var userId = Guid.NewGuid();

            var expectedResult =
                Result<Guid>.Success(userId);

            identityServiceMock
                .Setup(x => x.RegisterAsync(
                    email,
                    password,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var handler = new RegisterCommandHandler(
                identityServiceMock.Object);

            var command = new RegisterCommand(
                email,
                password);

            // Act
            var result = await handler.Handle(
                command,
                CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(userId);

            identityServiceMock.Verify(
                x => x.RegisterAsync(
                    email,
                    password,
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_When_Register_Fails()
        {
            // Arrange
            var identityServiceMock = new Mock<IIdentityService>();

            var email = "ali@test.com";
            var password = "123456";

            var expectedResult =
                Result<Guid>.Failure(
                    IdentityErrors.EmailAlreadyExists);

            identityServiceMock
                .Setup(x => x.RegisterAsync(
                    email,
                    password,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var handler = new RegisterCommandHandler(
                identityServiceMock.Object);

            var command = new RegisterCommand(
                email,
                password);

            // Act
            var result = await handler.Handle(
                command,
                CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should()
                .Be(IdentityErrors.EmailAlreadyExists);

            identityServiceMock.Verify(
                x => x.RegisterAsync(
                    email,
                    password,
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}