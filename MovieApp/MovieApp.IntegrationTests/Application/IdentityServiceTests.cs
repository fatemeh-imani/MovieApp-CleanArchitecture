using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Application.Abstractions;
using MovieApp.Application.Authentication.Abstractions;
using MovieApp.Infrastructure.Authentication.Identity;
using MovieApp.Infrastructure.Authentication.Role;
using MovieApp.Infrastructure.Persistence;
using MovieApp.IntegrationTests.Infrastructure;
using MovieApp.IntegrationTests.InfraStructure;

namespace MovieApp.IntegrationTests.Application
{
    public class IdentityServiceTests(
      CustomWebApplicationFactory factory)
      : IClassFixture<CustomWebApplicationFactory>
    {
        [Fact]
        public async Task RegisterAsync_Should_Create_User_When_Data_IsValid()
        {

            // Arrange

            using var scope =
                 factory.Services.CreateScope();

            var context =
                scope.ServiceProvider
                    .GetRequiredService<ApplicationDbContext>();

            var identityService =
                scope.ServiceProvider
                    .GetRequiredService<IIdentityService>();

            var email = "register-valid@test.com";
            var password = "Password123!";

            // Act
            var result = await identityService.RegisterAsync(
                email,
                password,
                CancellationToken.None);

            result.IsSuccess.Should().BeTrue(
    result.IsFailure
        ? result.Error.ToString()
        : "Register succeeded");

            // Assert

            result.IsSuccess.Should().BeTrue();

            var user = await context.Users
                .FirstOrDefaultAsync(x => x.Email == email);

            user.Should().NotBeNull();
            user!.Email.Should().Be(email);
            user.UserName.Should().Be(email);

            var userManager =
                scope.ServiceProvider
                    .GetRequiredService<UserManager<ApplicationUser>>();

            var passwordValid =
                await userManager.CheckPasswordAsync(
                    user,
                    password);

            passwordValid.Should().BeTrue();

            var roles =
                await userManager.GetRolesAsync(user);

            roles.Should().Contain(Roles.User);
        }

        [Fact]
        public async Task RegisterAsync_Should_ReturnFailure_When_Email_AlreadyExists()
        {
            // Arrange

            using var scope =
                 factory.Services.CreateScope();

            var identityService =
                   scope.ServiceProvider
                    .GetRequiredService<IIdentityService>();

            var userManager =
                   scope.ServiceProvider
                    .GetRequiredService<UserManager<ApplicationUser>>();

            var email = "test@test.com";
            var password = "Password123!";

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            await userManager.CreateAsync(user, password);

            // Act
            var result = await identityService.RegisterAsync(
                email,
                password,
                CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(IdentityErrors.EmailAlreadyExists);
        }

        [Fact]
        public async Task LoginAsync_Should_ReturnToken_When_Credentials_Are_Valid()
        {
            // Arrange

            using var scope =
                   factory.Services.CreateScope();

            var context =
                scope.ServiceProvider
                    .GetRequiredService<ApplicationDbContext>();

            var roleManager =
                   scope.ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            await roleManager.CreateAsync(
                new IdentityRole<Guid>(Roles.User));

            var identityService =
                 scope.ServiceProvider
                    .GetRequiredService<IIdentityService>();

            var userManager =
                 scope.ServiceProvider
                    .GetRequiredService<UserManager<ApplicationUser>>();

            var email = "test@test.com";
            var password = "Password123!";

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            await userManager.CreateAsync(user, password);

            await userManager.AddToRoleAsync(
                user,
                Roles.User);

            // Act
            var result = await identityService.LoginAsync(
                email,
                password,
                CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task LoginAsync_Should_ReturnFailure_When_Email_Does_Not_Exist()
        {
            // Arrange

            using var scope =
                factory.Services.CreateScope();

           
            var identityService =
              scope.ServiceProvider
                    .GetRequiredService<IIdentityService>();

            // Act
            var result = await identityService.LoginAsync(
                "notfound@test.com",
                "Password123!",
                CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(
                IdentityErrors.InvalidCredentials);
        }

        [Fact]
        public async Task LoginAsync_Should_ReturnFailure_When_Password_Is_Invalid()
        {
            // Arrange

            using var scope =
                     factory.Services.CreateScope();                

            var context =
                scope.ServiceProvider
                    .GetRequiredService<ApplicationDbContext>();

            var identityService =
                   scope.ServiceProvider
                    .GetRequiredService<IIdentityService>();

            var userManager =
                   scope.ServiceProvider
                    .GetRequiredService<UserManager<ApplicationUser>>();

            var email = "test@test.com";
            var password = "Password123!";

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            await userManager.CreateAsync(user, password);

            // Act
            var result = await identityService.LoginAsync(
                email,
                "WrongPassword123!",
                CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(
                IdentityErrors.InvalidCredentials);
        }
    }
}
