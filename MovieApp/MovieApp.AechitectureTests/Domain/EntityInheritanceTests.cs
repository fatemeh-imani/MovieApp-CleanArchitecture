
using FluentAssertions;
using MovieApp.SharedKernel.Entitys;
using MovieApp.AechitectureTests.Architecture;
using NetArchTest.Rules;

namespace MovieApp.ArchitectureTests.Domain
{
    public class EntityInheritanceTests
    {
        [Fact]
        public void Entities_Should_Inherit_From_Entity()
        {
            var result = Types
                .InAssembly(Assemblies.Domain)
                .That()
                .ResideInNamespace("MovieApp.Domain.Entitys")
                .Should()
                .Inherit(typeof(Entity))
                .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }
    }
}
