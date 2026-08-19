
using FluentAssertions;
using MovieApp.AechitectureTests.Architecture;
using NetArchTest.Rules;

namespace MovieApp.ArchitectureTests.Application
{
    public class ValidatorTests
    {
        [Fact]
        public void Validators_Should_Be_Internal()
        {
            var result = Types
                .InAssembly(Assemblies.Application)
                .That()
                .HaveNameEndingWith("Validator")
                .Should()
                .NotBePublic()
                .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Validator_Should_Be_Sealed()
        {
            var result = Types
                .InAssembly(Assemblies.Application)
                .That()
                .HaveNameEndingWith("Validator")
                .Should()
                .BeSealed()
                .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }
    }

}
