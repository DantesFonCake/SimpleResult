namespace SimpleResult.Tests;

public class AndThenTests : LogicalBaseTests
{
    protected override string OperationName => "AndThen";
    protected override Result<TValue2, TError> PerformOperation<TValue, TError, TValue2>(Result<TValue, TError> left, Result<TValue2, TError> right)
    {
        return left.AndThen((_, data) => data, right);
    }
}