namespace Shouldify;

public static class ExceptionExtensions
{
    public static void ShouldThrow<TException>(this Action action, string? message = null)
        where TException : Exception
    {
        try
        {
            action();
        }
        catch (TException exception) when (message is null || exception.Message == message)
        {
            return;
        }
        catch (Exception exception)
        {
            Shouldify.Throw($"Expected '{typeof(TException).Name}' to be thrown, but '{exception.GetType().Name}' was thrown.");
        }

        Shouldify.Throw("Expected '{typeof(TException).Name}', but no exception was thrown.");
    }
}
