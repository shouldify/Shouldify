using NUnit.Framework.Internal;

namespace Shouldify.NUnit;

public class ExceptionTests
{
    [Test]
    public void ShouldifyThrow_ShouldThrowTheCorrectNUnitException()
    {
        var action = () => Shouldify.Throw("An exception message");

        action.ShouldThrow<AssertionException>();
    }

    [Test]
    public void ShouldifyThrow_ShouldThrowTheCorrectNUnitExceptionWithTheCorrectMessage()
    {
        const string Message = "An exception message";

        var action = () => Shouldify.Throw(Message);

        action.ShouldThrow<AssertionException>(Message);
    }
}
