
using FluentAssertions;
using NetArchTest.Rules;

namespace MovieApp.AechitectureTests.Application
{
    public class QueryTests
    {
        [Fact]
        public void Queries_Should_Be_Sealed()
        {
            var result = Types
                .InAssembly(Architecture.Assemblies.Application)
                .That()
                .HaveNameEndingWith("Query")
                .Should()
                .BeSealed()
                .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }
    }
}
