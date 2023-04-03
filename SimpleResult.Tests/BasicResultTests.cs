using FluentAssertions;
using NUnit.Framework;
using SimpleResult.Extensions;

namespace SimpleResult.Tests;

[TestFixture]
public class BasicResultTests : Base.TestsBase
{
	[Test]
	public void Value_CanBe_ImplicitlyConverted_To_Result()
	{
		Result<string, Unit> result = OkMessage1;

		result.Unwrap().Should().Be(OkMessage1);
	}

	[Test]
	public void Error_CanBe_ImplicitlyConverted_To_Result()
	{
		Result<Unit, string> result = ErrorMessage1;

		result.UnwrapError().Should().Be(ErrorMessage1);
	}

	[Test]
	public void Unwrap_Should_ThrowInvalidUnwrapException_ThenResult_IsError()
	{
		var result = ErrorMessage1.ToError();
		var unwrap = () => result.Unwrap();

		unwrap.Should().Throw<InvalidUnwrapException<string>>().And.ActualResultValue.Should().Be(ErrorMessage1);
	}
	
	[Test]
	public void UnwrapError_Should_ThrowInvalidUnwrapException_ThenResult_IsOk()
	{
		var result = OkMessage1.ToOk();
		var unwrap = () => result.UnwrapError();

		unwrap.Should().Throw<InvalidUnwrapException<string>>().And.ActualResultValue.Should().Be(OkMessage1);
	}

	[Test]
	public void Try_Should_ReturnError_WhenDelegateThrows()
	{
		var result = Result.Try<string>(() => throw new Exception(ErrorMessage1));

		result.IsError.Should().BeTrue();
		result.UnwrapError().Message.Should().Be(ErrorMessage1);
	}

	[Test]
	public void Try_Should_ReturnOk_WhenDelegateDoesNotThrow()
	{
		var result = Result.Try(() => OkMessage1);

		result.IsOk.Should().BeTrue();
		result.Unwrap().Should().Be(OkMessage1);
	}

	[Test]
	public void Try_Should_UseDataInValueFactory()
	{
		var result = Result.Try(data => data, OkMessage1);

		result.Unwrap().Should().Be(OkMessage1);
	}

	[Test]
	public void Try_Should_UseErrorDataInErrorMapper()
	{
		var result = Result.Try<Unit, string, string>(() => throw new Exception(), (_, data) => data, ErrorMessage1);

		result.UnwrapError().Should().Be(ErrorMessage1);
	}
}