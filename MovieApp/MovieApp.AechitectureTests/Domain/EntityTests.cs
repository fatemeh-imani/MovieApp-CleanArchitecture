
using FluentAssertions;
using MovieApp.SharedKernel.Entitys;
using MovieApp.AechitectureTests.Architecture;
using NetArchTest.Rules;

namespace MovieApp.ArchitectureTests.Domain
{
    public class EntityTests
    {
        [Fact]
        public void Entities_Should_be_Seald()
        {
            var result =Types
                .InAssembly(Assemblies.Domain)
                .That()
                .Inherit(typeof(Entity))
                .Should()
                .BeSealed()
                .GetResult();

            result.IsSuccessful.Should().BeTrue();  
        }
    }
}
