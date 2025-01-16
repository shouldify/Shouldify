using Xunit.Sdk;

namespace Shouldify.XUnit3;

public class ExceptionTests
{
    [Fact]
    public void ShouldifyThrow_ShouldThrowTheCorrectXUnit3Exception()
    {
        var action = () => Shouldify.Throw("An exception message");

        action.ShouldThrow<XunitException>();
    }

    [Fact]
    public void ShouldifyThrow_ShouldThrowTheCorrectXUnit3ExceptionWithTheCorrectMessage()
    {
        const string Message = "An exception message";

        var action = () => Shouldify.Throw(Message);

        action.ShouldThrow<XunitException>(Message);
    }
}
