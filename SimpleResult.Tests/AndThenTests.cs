using NUnit.Framework;
using SimpleResult.Tests.Base;

namespace SimpleResult.Tests;

[TestFixture]
public class AndThenTests : AndBaseTests
{
    protected override string OperationName => nameof(Result<int, int>.AndThen);

    protected override Result<TValue2, TError> PerformOperation<TValue, TError, TValue2>(Result<TValue, TError> left,
        Result<TValue2, TError> right)
    {
        return left.AndThen((_, data) => data, right);
    }
}