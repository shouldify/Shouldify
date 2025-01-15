using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Shouldify;

internal static class Shouldify
{
    private static readonly (string AssemblyName, string ExceptionFullName, bool LoadAssembly)[] _testingFrameworks =
    [
        ("nunit.framework", "NUnit.Framework.AssertionException", false),
        ("xunit.assert", "Xunit.Sdk.XunitException", true),
        ("xunit.v3.assert", "Xunit.Sdk.XunitException", true),
        ("TUnit.Assertions.Exceptions.AssertionException", "TUnit.Assertions", true),
    ];

    private static readonly Func<string, Exception> _exceptionFactory = GetExceptionFactory();

    [DoesNotReturn]
    public static void Throw(string message) => throw _exceptionFactory(message);

    private static Func<string, Exception> GetExceptionFactory()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var (assemblyName, exceptionFullName, loadAssembly) in _testingFrameworks)
        {
            if (!TryGetAssembly(assemblies, assemblyName, loadAssembly, out var assembly))
            {
                continue;
            }

            var exceptionType = assembly.GetType(exceptionFullName)!;

            return message => (Exception)Activator.CreateInstance(exceptionType, message)!;
        }

        throw new InvalidOperationException("Could not find the testing framework.");
    }

    private static bool TryGetAssembly(Assembly[] assemblies, string assemblyName, bool loadAssembly, [NotNullWhen(true)] out Assembly? assembly)
    {
        assembly = assemblies.FirstOrDefault(a => a.GetName().Name == assemblyName);

        if (assembly is not null)
        {
            return true;
        }

        if (!loadAssembly)
        {
            return false;
        }

        try
        {
            assembly = Assembly.Load(new AssemblyName(assemblyName));
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
}
