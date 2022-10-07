namespace SimpleResult.Tests;

public class OrTests : LogicalWithValueBaseTests
{
	protected override string OperationName => "Or";
	protected override Result<TValue2, TError> PerformOperation<TValue, TError, TValue2>(Result<TValue, TError> left, Result<TValue2, TError> right)
	{
		return left.Or(right);
	}

	protected override Result<TValue2, TError> PerformValueOperation<TValue, TError, TValue2>(Result<TValue, TError> ok, TValue2 unit)
	{
		throw new NotImplementedException();
	}
}