using FluentAssertions;
using NUnit.Framework;

namespace SimpleResult.Tests.Base;

public abstract class AndBaseTests : LogicalBaseTests
{
    protected abstract Result<TValue2, TError> PerformOperation<TValue, TError, TValue2>(Result<TValue, TError> left, 
        Result<TValue2, TError> right);
    
    [Test]
    public void Ok_Operation_Ok_ReturnsOk()
    {
        var ok1 = Result.Ok();
        var ok2 = Result.Ok();

        var result = PerformOperation(ok1, ok2);

        result.IsOk.Should().BeTrue($"Ok {OperationName} Ok should produce Ok");
    }
    
    [Test]
    public void Ok1_Operation_Ok2_ReturnsOk2()
    {
        var ok1 = Result.Ok(OkMessage1);
        var ok2 = Result.Ok(OkMessage2);

        var result = PerformOperation(ok1, ok2);

        result.Unwrap().Should().Be(OkMessage2, $"Ok {OperationName} Ok should return second Ok");
    }

    [Test]
    public void Ok_Operation_Error_ReturnsError()
    {
        var ok = Result.Ok();
        var error = Result.Error();

        var result = PerformOperation(ok, error);

        result.IsError.Should().BeTrue($"Ok {OperationName} Error should return Error");
    }
    
    [Test]
    public void Ok_Operation_Error_ReturnsSameError()
    {
        var ok = Result.Ok<Unit, string>(default);
        var error = Result.Error(ErrorMessage1);

        var result = PerformOperation(ok,error);
        
        result.UnwrapError().Should().Be(ErrorMessage1, $"Ok {OperationName} Error should return provided Error");
    }
    
    [Test]
    public void Error_Operation_Ok_ReturnsError()
    {
        var error = Result.Error();
        var ok = Result.Ok();

        var result = PerformOperation(error, ok);

        result.IsError.Should().BeTrue($"Error {OperationName} Ok should return Error");
    }
    
    [Test]
    public void Error_Operation_Ok_ReturnsSameError()
    {
        var error = Result.Error(ErrorMessage1);
        var ok = Result.Ok<Unit, string>(default);

        var result = PerformOperation(error, ok);
        
        result.UnwrapError().Should().Be(ErrorMessage1, $"Error {OperationName} Ok should return provided Error");
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
    public void Error1_Operation_Error2_ReturnsError1()
    {
        var error1 = Result.Error(ErrorMessage1);
        var error2 = Result.Error(ErrorMessage2);

        var result = PerformOperation(error1, error2);
        
        result.UnwrapError().Should().Be(ErrorMessage1, $"Error {OperationName} Error should return first Error");
    }
}