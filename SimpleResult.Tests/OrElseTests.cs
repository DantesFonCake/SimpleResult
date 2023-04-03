using NUnit.Framework;
using SimpleResult.Tests.Base;

namespace SimpleResult.Tests;

[TestFixture]
public class OrElseTests : OrBaseTests
{
	protected override string OperationName => nameof(Result<int, int>.OrElse);
	protected override Result<TValue, TError2> PerformOperation<TValue, TError, TError2>(Result<TValue, TError> left, Result<TValue, TError2> right)
	{
		return left.OrElse(static (_, right) => right, right);
	}
}