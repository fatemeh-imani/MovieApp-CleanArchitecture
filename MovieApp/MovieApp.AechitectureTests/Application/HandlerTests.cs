
using FluentAssertions;
using NetArchTest.Rules;

namespace MovieApp.AechitectureTests.Application
{
    public class HandlerTests
    {
        [Fact]
        public void Handlers_Should_be_Internal()
        {
            var result = Types
                .InAssembly(Architecture.Assemblies.Application)
                .That()
                .HaveNameEndingWith("Handdler")
                .Should()
                .NotBePublic()
                .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }
    }
}
