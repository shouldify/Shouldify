using System.Diagnostics.CodeAnalysis;

namespace Shouldify;

internal static class Shouldify
{
    private static readonly (string AssemblyName, string ExceptionFullName)[] _testingFrameworks =
    [
        ("Microsoft.VisualStudio.TestPlatform.TestFramework", "Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException"),
        ("nunit.framework", "NUnit.Framework.AssertionException"),
        ("xunit.assert", "Xunit.Sdk.XunitException"),
        ("xunit.v3.assert", "Xunit.Sdk.XunitException"),
        ("TUnit.Assertions", "TUnit.Assertions.Exceptions.AssertionException"),
    ];

    private static readonly Func<string, Exception> _exceptionFactory = GetExceptionFactory();

    [DoesNotReturn]
    public static void Throw(string message) => throw _exceptionFactory(message);

    private static Func<string, Exception> GetExceptionFactory()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var (assemblyName, exceptionFullName) in _testingFrameworks)
        {
            var assembly = assemblies.FirstOrDefault(a => a.GetName().Name == assemblyName);

            if (assembly is null)
            {
                continue;
            }

            var exceptionType = assembly.GetType(exceptionFullName);

            return message => (Exception)Activator.CreateInstance(exceptionType, message);
        }

        throw new InvalidOperationException("Could not find the testing framework.");
    }
}
