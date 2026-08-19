
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Reflection;
using System.Reflection.Metadata;

namespace MovieApp.AechitectureTests.Architecture
{
    internal static class Assemblies
    {
        internal static readonly Assembly Domain =
            typeof(AssemblyReference).Assembly;

        internal static readonly Assembly Application =
            typeof(MovieApp.Application.AssemblyRefrence).Assembly;

        internal static readonly Assembly Infrastructure =
            typeof(MovieApp.Infrastructure.AssemblyRefrence).Assembly;
        
        internal static readonly Assembly API =
                    typeof(Program).Assembly;

    }
}
