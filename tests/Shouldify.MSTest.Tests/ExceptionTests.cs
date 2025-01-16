namespace Shouldify.MSTest;

[TestClass]
public sealed class ExceptionTests
{
    [TestMethod]
    public void ShouldifyThrow_ShouldThrowTheCorrectMSTestException()
    {
        var action = () => Shouldify.Throw("An exception message");

        action.ShouldThrow<AssertFailedException>();
    }

    [TestMethod]
    public void ShouldifyThrow_ShouldThrowTheCorrectMSTestExceptionWithTheCorrectMessage()
    {
        const string Message = "An exception message";

        var action = () => Shouldify.Throw(Message);

        action.ShouldThrow<AssertFailedException>(Message);
    }
}
