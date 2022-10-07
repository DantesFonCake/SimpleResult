using FluentAssertions;
using NUnit.Framework;

namespace SimpleResult.Tests;
public abstract class LogicalBaseTests
{
    protected const string OkMessage1 = "According to all laws of aviation";
    protected const string OkMessage2 = "A bee should not be able to fly";

    protected const string ErrorMessage1 = "That's an error alright";
    protected const string ErrorMessage2 = "Just not a super-one";
    
    protected abstract string OperationName { get; }
    protected abstract Result<TValue2, TError2> PerformOperation<TValue, TError, TValue2, TError2>(Result<TValue, TError> left, 
        Result<TValue2, TError2> right);
    
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

        result.Value.Should().Be(OkMessage2, $"Ok {OperationName} Ok should return second Ok");
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
        
        result.Error.Should().Be(ErrorMessage1, $"Ok {OperationName} Error should return provided Error");
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
        
        result.Error.Should().Be(ErrorMessage1, $"Error {OperationName} Ok should return provided Error");
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
        
        result.Error.Should().Be(ErrorMessage1, $"Error {OperationName} Error should return first Error");
    }
}