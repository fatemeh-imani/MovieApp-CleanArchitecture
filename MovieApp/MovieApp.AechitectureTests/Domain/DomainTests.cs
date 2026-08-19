
using FluentAssertions;
using NetArchTest.Rules;

namespace MovieApp.AechitectureTests.Domain
{
    public class DomainTests
    {
        [Fact]
        public void Domain_Should_Not_Have_Dependency_On_Other_Projects()
        {
            var result = Types
                .InAssembly(Architecture.Assemblies.Domain)
                .ShouldNot()
                .HaveDependencyOnAll(
                 "MovieApp.Application",
                 "MovieApp.Infrastructure",
                 "MovieApp.API")
                .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }
    }
}
