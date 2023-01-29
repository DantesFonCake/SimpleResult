using FluentAssertions;
using NUnit.Framework;

namespace SimpleResult.Tests.Base;

public abstract class OrBaseTests : LogicalBaseTests
{
    protected abstract Result<TValue, TError2> PerformOperation<TValue, TError, TError2>(Result<TValue, TError> left, 
        Result<TValue, TError2> right);
    
    [Test]
    public void Ok_Operation_Ok_ReturnsOk()
    {
        var ok1 = Result.Ok();
        var ok2 = Result.Ok();

        var result = PerformOperation(ok1, ok2);

        result.IsOk.Should().BeTrue($"Ok {OperationName} Ok should produce Ok");
    }
    
    [Test]
    public void Ok1_Operation_Ok2_ReturnsOk1()
    {
        var ok1 = Result.Ok(OkMessage1);
        var ok2 = Result.Ok(OkMessage2);

        var result = PerformOperation(ok1, ok2);

        result.Unwrap().Should().Be(OkMessage1, $"Ok {OperationName} Ok should return first Ok");
    }

    [Test]
    public void Ok_Operation_Error_ReturnsOk()
    {
        var ok = Result.Ok();
        var error = Result.Error();

        var result = PerformOperation(ok, error);

        result.IsOk.Should().BeTrue($"Ok {OperationName} Error should return Ok");
    }
    
    [Test]
    public void Ok_Operation_Error_ReturnsSameOk()
    {
        var ok = Result.Ok<string, Unit>(OkMessage1);
        var error = Result.Error<string, Unit>(default);

        var result = PerformOperation(ok,error);
        
        result.Unwrap().Should().Be(OkMessage1, $"Ok {OperationName} Error should return provided Ok");
    }
    
    [Test]
    public void Error_Operation_Ok_ReturnsOk()
    {
        var error = Result.Error();
        var ok = Result.Ok();

        var result = PerformOperation(error, ok);

        result.IsOk.Should().BeTrue($"Error {OperationName} Ok should return Ok");
    }
    
    [Test]
    public void Error_Operation_Ok_ReturnsSameOk()
    {
        var error = Result.Error<string, Unit>(default);
        var ok = Result.Ok<string, Unit>(OkMessage1);

        var result = PerformOperation(error, ok);
        
        result.Unwrap().Should().Be(OkMessage1, $"Error {OperationName} Ok should return provided Ok");
    }
    
    [Test]
    public void Error_Operation_Error_ReturnsError()
    {
        var error1 = Result.Error();
        var error2 = Result.Error();

        var result = PerformOperation(error1, error2);

        result.IsError.Should().BeTrue($"Error {OperationName} Error should return Error");
    }
    
    [Test]
    public void Error1_Operation_Error2_ReturnsError2()
    {
        var error1 = Result.Error(ErrorMessage1);
        var error2 = Result.Error(ErrorMessage2);

        var result = PerformOperation(error1, error2);
        
        result.UnwrapError().Should().Be(ErrorMessage2, $"Error {OperationName} Error should return first Error");
    }
}