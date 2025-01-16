using TUnit.Assertions.Exceptions;

namespace Shouldify.TUnit;

public class ExceptionTests
{
    [Test]
    public void ShouldifyThrow_ShouldThrowTheCorrectTUnitException()
    {
        var action = () => Shouldify.Throw("An exception message");

        action.ShouldThrow<AssertionException>();
    }

    [Test]
    public void ShouldifyThrow_ShouldThrowTheCorrectTUnitExceptionWithTheCorrectMessage()
    {
        const string Message = "An exception message";

        var action = () => Shouldify.Throw(Message);

        action.ShouldThrow<AssertionException>(Message);
    }
}
