using StreamHub.Common.Types;

namespace StreamHub.Common.Tests.Types;

public class ResultTests
{
    [Fact]
    public void Bind_ShouldChain_WhenSuccessful()
    {
        var result = Result<int>.Success(5);
        var chained = result.Bind(x => Result<string>.Success($"Value: {x}"));

        Assert.True(chained.IsSuccess);
        Assert.Equal("Value: 5", chained.Value);
    }

    [Fact]
    public void Bind_ShouldReturnFailure_WhenOriginalFailed()
    {
        var result = Result<int>.Failure(new Exception("fail"));
        var chained = result.Bind(x => Result<string>.Success($"Value: {x}"));

        Assert.False(chained.IsSuccess);
    }

    [Fact]
    public async Task Combine_Async_ShouldReturnFailure_WhenOriginalIsFailure()
    {
        // Arrange
        var original = Result<int>.Failure(new InvalidOperationException("Original failed"));

        // Act
        var combined = await original.Combine<object>(_ => throw new Exception("This should not be called"));

        // Assert
        Assert.False(combined.IsSuccess);
        Assert.IsType<InvalidOperationException>(combined.Exception);
        Assert.Equal("Original failed", combined.Exception?.Message);
    }

    [Fact]
    public async Task Combine_Async_ShouldWorkCorrectly()
    {
        var result = Result<int>.Success(3);
        var combined = await result.Combine(async x =>
        {
            await Task.Delay(10);
            return Result<string>.Success($"Hello {x}");
        });

        Assert.True(combined.IsSuccess);
        Assert.Equal("Hello 3", combined.Value);
    }

    [Fact]
    public void Combine_ShouldReturnFirstFailure()
    {
        var first = Result<int>.Failure(new Exception("First failed"));
        var second = Result<string>.Success("OK");

        var combined = first.Combine(second);

        Assert.False(combined.IsSuccess);
        Assert.Equal("First failed", combined.Exception?.Message);
    }

    [Fact]
    public void Combine_ShouldReturnSuccess_WhenBothResultsAreSuccessful()
    {
        var result1 = Result<int>.Success(1);
        var result2 = Result<string>.Success("ok");

        var combined = result1.Combine(result2);

        Assert.True(combined.IsSuccess);
        Assert.Equal((1, "ok"), combined.Value);
    }

    [Fact]
    public void Failure_ShouldHaveException_AndNotBeSuccessful()
    {
        var ex = new InvalidOperationException("Something went wrong");
        var result = Result<string>.Failure(ex);

        Assert.False(result.IsSuccess);
        Assert.Equal(ex, result.Exception);
        Assert.Null(result.Value);
    }

    [Fact]
    public void Map_ShouldReturnFailure_WhenOriginalFailed()
    {
        var result = Result<int>.Failure(new Exception("fail"));
        var mapped = result.Map(x => x * 2);

        Assert.False(mapped.IsSuccess);
        Assert.NotNull(mapped.Exception);
    }

    [Fact]
    public void Map_ShouldTransformValue_WhenSuccessful()
    {
        var result = Result<int>.Success(2);
        var mapped = result.Map(x => x * 2);

        Assert.True(mapped.IsSuccess);
        Assert.Equal(4, mapped.Value);
    }

    [Fact]
    public void Match_Result_ShouldReturnFailureValue()
    {
        // Arrange
        var result = Result<int>.Failure(new Exception("fail"));

        // Act
        var value = result.Match(
            x => x * 2,
            _ => -1);

        // Assert
        Assert.Equal(-1, value);
    }

    [Fact]
    public void Match_Result_ShouldReturnSuccessValue()
    {
        // Arrange
        var result = Result<int>.Success(42);

        // Act
        var value = result.Match(
            x => x * 2,
            _ => -1);

        // Assert
        Assert.Equal(84, value);
    }

    [Fact]
    public void Match_Void_ShouldCallOnFailure_WhenFailure()
    {
        // Arrange
        var result = Result<string>.Failure(new InvalidOperationException("fail"));
        var successCalled = false;
        var failureCalled = false;

        // Act
        result.Match(
            _ => successCalled = true,
            _ => failureCalled = true);

        // Assert
        Assert.False(successCalled);
        Assert.True(failureCalled);
    }

    [Fact]
    public void Match_Void_ShouldCallOnSuccess_WhenSuccess()
    {
        // Arrange
        var result = Result<string>.Success("hello");
        var called = false;

        // Act
        result.Match(
            _ => called = true,
            _ => throw new Exception("Should not be called"));

        // Assert
        Assert.True(called);
    }

    [Fact]
    public void Success_ShouldHaveValue_AndBeSuccessful()
    {
        var result = Result<string>.Success("Test");

        Assert.True(result.IsSuccess);
        Assert.Equal("Test", result.Value);
        Assert.Null(result.Exception);
    }

    [Fact]
    public void ToString_ShouldReturnProperFormat()
    {
        var ok = Result<string>.Success("yay");
        var fail = Result<string>.Failure(new Exception("oops"));

        Assert.StartsWith("Success", ok.ToString());
        Assert.StartsWith("Failure", fail.ToString());
    }
}