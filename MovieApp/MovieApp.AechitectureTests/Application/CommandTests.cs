
using FluentAssertions;
using NetArchTest.Rules;

namespace MovieApp.AechitectureTests.Application
{
    public class CommandTests
    {
        [Fact]
        public void Commmand_Should_Be_Sealed()
        {
            var result = Types
                .InAssembly(Architecture.Assemblies.Application)
                .That()
                .HaveNameEndingWith("Command")
                .Should()
                .BeSealed()
                .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }
    }
}
