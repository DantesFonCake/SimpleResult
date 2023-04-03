namespace SimpleResult;

public sealed class InvalidUnwrapException<T> : InvalidOperationException
{
    public T ActualResultValue { get; }
    public InvalidUnwrapException(string? message, T actualResultValue) : base(message)
    {
        ActualResultValue = actualResultValue;
    }
}