using SimpleResult.Tests.Base;

namespace SimpleResult.Tests;

public class OrTests : OrWithValueBaseTests
{
	protected override string OperationName => "Or";
	protected override Result<TValue, TError2> PerformOperation<TValue, TError, TError2>(Result<TValue, TError> left, Result<TValue, TError2> right)
	{
		return left.Or(right);
	}

	protected override Result<TValue, TError2> PerformValueOperation<TValue, TError, TError2>(Result<TValue, TError> left, TError2 right)
	{
		return left.Or(right);
	}
}