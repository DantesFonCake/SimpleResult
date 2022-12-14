using NUnit.Framework;
using SimpleResult.Tests.Base;

namespace SimpleResult.Tests;

[TestFixture]
public class AndTests: AndWithValueBaseTests
{
    protected override string OperationName => "And";
    protected override Result<TValue2, TError> PerformOperation<TValue, TError, TValue2>(Result<TValue, TError> left, Result<TValue2, TError> right)
    {
        return left.And(right);
    }

    protected override Result<TValue2, TError> PerformValueOperation<TValue, TError, TValue2>(Result<TValue, TError> left, TValue2 value)
    {
        return left.And(value);
    }
}