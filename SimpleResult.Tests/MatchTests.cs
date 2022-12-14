using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using SimpleResult.Tests.Base;

namespace SimpleResult.Tests;

[TestFixture]
public class MatchTests : TestsBase
{
    private Func<string, string> _valueArm = null!;
    private Func<string, string> _errorArm = null!;

    [SetUp]
    public void SetUp()
    {
        _valueArm = A.Fake<Func<string, string>>();
        A.CallTo(() => _valueArm.Invoke(OkMessage1)).Returns(OkMessage2);
        _errorArm = A.Fake<Func<string, string>>();
        A.CallTo(() => _errorArm.Invoke(ErrorMessage1)).Returns(ErrorMessage2);
    }

    [Test]
    public void Match_OnValue_ReturnsNewValue()
    {
        var ok = Result.Ok<string, string>(OkMessage1);

        var result = ok.Match(_valueArm, _errorArm);
        
        result.Should().Be(OkMessage2);
        A.CallTo(() => _valueArm.Invoke(OkMessage1)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _errorArm.Invoke(A<string>._)).MustNotHaveHappened();
    }
    
    [Test]
    public void Match_OnError_ReturnsNewError()
    {
        var ok = Result.Error<string, string>(ErrorMessage1);

        var result = ok.Match(_valueArm, _errorArm);

        result.Should().Be(ErrorMessage2);
        A.CallTo(() => _valueArm.Invoke(A<string>._)).MustNotHaveHappened();
        A.CallTo(() => _errorArm.Invoke(ErrorMessage1)).MustHaveHappenedOnceExactly();
    }
}