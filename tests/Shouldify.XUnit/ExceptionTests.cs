using Xunit.Sdk;

namespace Shouldify.XUnit;

public class ExceptionTests
{
    [Fact]
    public void ShouldifyThrow_ShouldThrowTheCorrectNUnitException()
    {
        var action = () => Shouldify.Throw("An exception message");

        action.ShouldThrow<XunitException>();
    }

    [Fact]
    public void ShouldifyThrow_ShouldThrowTheCorrectNUnitExceptionWithTheCorrectMessage()
    {
        const string Message = "An exception message";

        var action = () => Shouldify.Throw(Message);

        action.ShouldThrow<XunitException>(Message);
    }
}
