using FluentAssertions;
using NUnit.Framework;

namespace SimpleResult.Tests.Base;

public abstract class OrWithValueBaseTests : OrBaseTests
{
    protected abstract Result<TValue, TError2> PerformValueOperation<TValue, TError, TError2>(Result<TValue, TError> ok, TError2 unit);
    
    [Test]
    public void Ok_Operation_Value_ReturnsOk()
    {
        var ok = Result.Ok();

        var result = PerformValueOperation(ok, new Unit());

        result.IsOk.Should().BeTrue($"Ok {OperationName} Value should return Ok");
    }
    
    [Test]
    public void Ok_Operation_Value_ReturnsOriginalOk()
    {
        var ok = Result.Ok(OkMessage1);

        var result = PerformValueOperation(ok, ErrorMessage1);

        result.Unwrap().Should().Be(OkMessage1, $"Ok {OperationName} Value should return original Ok");
    }
    
    [Test]
    public void Error_Operation_Value_ReturnsError()
    {
        var error = Result.Error();

        var result = PerformValueOperation(error, new Unit());

        result.IsError.Should().BeTrue($"Error {OperationName} Value should return Error");
    }
    
    [Test]
    public void Error_Operation_Value_ReturnsValue()
    {
        var error = Result.Error(ErrorMessage1);

        var result = PerformValueOperation(error, ErrorMessage2);

        result.UnwrapError().Should().Be(ErrorMessage2, $"Error {OperationName} Value should return provided Error");
    }
}