using MovieApp.ArchitectureTests.Architecture;
using NetArchTest.Rules;

namespace MovieApp.AechitectureTests.Application
{
    public class ApplicationTests
    {
        [Fact]
        public void Application_Should_Not_HAve_Dependency_On_Infrastraucture()
        {
            var result = Types
                .InAssembly(Architecture.Assemblies.Application)
                .ShouldNot()
                .HaveDependencyOnAny(
                 Namespaces.Infrastructure,
                 Namespaces.Presentation)
                 .GetResult();
        }
    }
}
