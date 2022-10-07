using NUnit.Framework;

namespace SimpleResult.Tests;

[TestFixture]
public class AndTests : LogicalWithValueBaseTests
{
    protected override string OperationName => "And";
    protected override Result<TValue2, TError2> PerformOperation<TValue, TError, TValue2, TError2>(Result<TValue, TError> left, Result<TValue2, TError2> right)
    {
        return left.And(right);
    }

    protected override Result<TValue2, TError> PerformValueOperation<TValue, TError, TValue2>(Result<TValue, TError> left, TValue2 value)
    {
        return left.And(value);
    }
}