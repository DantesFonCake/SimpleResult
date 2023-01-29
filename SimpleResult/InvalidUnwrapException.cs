namespace SimpleResult;

public class InvalidUnwrapException<T> : InvalidOperationException
{
    public T ActualResultValue { get; }
    public InvalidUnwrapException(string? message, T actualResultValue) : base(message)
    {
        ActualResultValue = actualResultValue;
    }

    public InvalidUnwrapException(string? message, Exception? innerException, T actualResultValue) : base(message, innerException)
    {
        ActualResultValue = actualResultValue;
    }
}