using FluentAssertions;
using NUnit.Framework;

namespace SimpleResult.Tests;

public abstract class LogicalWithValueBaseTests : LogicalBaseTests
{
    protected abstract Result<TValue2, TError> PerformValueOperation<TValue, TError, TValue2>(Result<TValue, TError> ok, TValue2 unit);
    
    [Test]
    public void Ok_Operation_Value_ReturnsOk()
    {
        var ok = Result.Ok();

        var result = PerformValueOperation(ok, new Unit());

        result.IsOk.Should().BeTrue($"Ok {OperationName} Value should return Ok");
    }
    
    [Test]
    public void Ok_Operation_Value_ReturnsValue()
    {
        var ok = Result.Ok();

        var result = PerformValueOperation(ok, OkMessage1);

        result.Value.Should().Be(OkMessage1, $"Ok {OperationName} Value should return provided Value");
    }
    
    [Test]
    public void Error_Operation_Value_ReturnsError()
    {
        var error = Result.Error();

        var result = PerformValueOperation(error, new Unit());

        result.IsError.Should().BeTrue($"Error {OperationName} Value should return Error");
    }
    
    [Test]
    public void Error_Operation_Value_ReturnsSameError()
    {
        var error = Result.Error(ErrorMessage1);

        var result = PerformValueOperation(error, ErrorMessage2);

        result.Error.Should().Be(ErrorMessage1, $"Error {OperationName} Value should return provided Error");
    }
}