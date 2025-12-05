namespace Day2a.Tests;

/// <summary>
/// Unit tests for the <see cref="InputParser.GetInput"/> method.
/// </summary>
public class GetInputTests
{
    [Fact]
    public void GetInputWithSingleRangeReturnsAllValuesInRange()
    {
        using StringReader input = new("1-3");

        List<BigInteger> result = InputParser.GetInput(input);

        Assert.Equal(3, result.Count);
        Assert.Equal(new BigInteger(1), result[0]);
        Assert.Equal(new BigInteger(2), result[1]);
        Assert.Equal(new BigInteger(3), result[2]);
    }

    [Fact]
    public void GetInputWithMultipleRangesReturnsAllValuesFromAllRanges()
    {
        using StringReader input = new("1-3,5-7");

        List<BigInteger> result = InputParser.GetInput(input);

        Assert.Equal(6, result.Count);
        Assert.Equal(new BigInteger(1), result[0]);
        Assert.Equal(new BigInteger(2), result[1]);
        Assert.Equal(new BigInteger(3), result[2]);
        Assert.Equal(new BigInteger(5), result[3]);
        Assert.Equal(new BigInteger(6), result[4]);
        Assert.Equal(new BigInteger(7), result[5]);
    }

    [Fact]
    public void GetInputWithSingleValueRangeReturnsSingleValue()
    {
        using StringReader input = new("5-5");

        List<BigInteger> result = InputParser.GetInput(input);

        _ = Assert.Single(result);
        Assert.Equal(new BigInteger(5), result[0]);
    }

    [Fact]
    public void GetInputWithLargeNumbersReturnsCorrectBigIntegerValues()
    {
        string largeNumber = "123456789012345678901234567890";
        using StringReader input = new($"{largeNumber}-{largeNumber}");

        List<BigInteger> result = InputParser.GetInput(input);

        _ = Assert.Single(result);
        Assert.Equal(BigInteger.Parse(largeNumber), result[0]);
    }

    [Fact]
    public void GetInputWithThreeRangesReturnsAllValues()
    {
        using StringReader input = new("1-2,4-5,7-8");

        List<BigInteger> result = InputParser.GetInput(input);

        Assert.Equal(6, result.Count);
        Assert.Contains(new BigInteger(1), result);
        Assert.Contains(new BigInteger(2), result);
        Assert.Contains(new BigInteger(4), result);
        Assert.Contains(new BigInteger(5), result);
        Assert.Contains(new BigInteger(7), result);
        Assert.Contains(new BigInteger(8), result);
    }

    [Fact]
    public void GetInputWithZeroRangeIncludesZero()
    {
        using StringReader input = new("0-2");

        List<BigInteger> result = InputParser.GetInput(input);

        Assert.Equal(3, result.Count);
        Assert.Equal(BigInteger.Zero, result[0]);
        Assert.Equal(BigInteger.One, result[1]);
        Assert.Equal(new BigInteger(2), result[2]);
    }

    [Fact]
    public void GetInputWithEmptyInputThrowsInvalidOperationException()
    {
        using StringReader input = new(string.Empty);

        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(
            () => InputParser.GetInput(input));

        Assert.Equal("Input is empty.", exception.Message);
    }

    [Fact]
    public void GetInputWithEmptyStringThrowsInvalidOperationException()
    {
        using StringReader input = new(string.Empty);

        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(
            () => InputParser.GetInput(input));

        Assert.Equal("Input is empty.", exception.Message);
    }

    [Theory]
    [InlineData("1")]
    [InlineData("abc")]
    [InlineData("1-2-3")]
    public void GetInputWithInvalidRangeFormatThrowsFormatException(string invalidRange)
    {
        using StringReader input = new(invalidRange);

        FormatException exception = Assert.Throws<FormatException>(
            () => InputParser.GetInput(input));

        Assert.Equal($"Invalid range format: {invalidRange}", exception.Message);
    }

    [Theory]
    [InlineData("1-3,invalid")]
    [InlineData("valid-format,5")]
    public void GetInputWithMixedValidAndInvalidRangesThrowsFormatException(string mixedInput)
    {
        using StringReader input = new(mixedInput);

        _ = Assert.Throws<FormatException>(() => InputParser.GetInput(input));
    }

    [Theory]
    [InlineData("abc-def")]
    [InlineData("1-abc")]
    [InlineData("abc-1")]
    public void GetInputWithNonNumericValuesThrowsFormatException(string invalidInput)
    {
        using StringReader input = new(invalidInput);

        _ = Assert.Throws<FormatException>(() => InputParser.GetInput(input));
    }

    [Fact]
    public void GetInputPreservesOrderOfRanges()
    {
        using StringReader input = new("10-11,1-2,5-6");

        List<BigInteger> result = InputParser.GetInput(input);

        Assert.Equal(6, result.Count);
        Assert.Equal(new BigInteger(10), result[0]);
        Assert.Equal(new BigInteger(11), result[1]);
        Assert.Equal(new BigInteger(1), result[2]);
        Assert.Equal(new BigInteger(2), result[3]);
        Assert.Equal(new BigInteger(5), result[4]);
        Assert.Equal(new BigInteger(6), result[5]);
    }

    [Fact]
    public void GetInputWithLargeRangeReturnsCorrectCount()
    {
        using StringReader input = new("1-100");

        List<BigInteger> result = InputParser.GetInput(input);

        Assert.Equal(100, result.Count);
        Assert.Equal(BigInteger.One, result[0]);
        Assert.Equal(new BigInteger(100), result[^1]);
    }

    [Fact]
    public void GetInputWithWhitespaceAroundCommasHandlesCorrectly()
    {
        using StringReader input = new("1-2, 3-4");

        List<BigInteger> result = InputParser.GetInput(input);

        Assert.Equal(4, result.Count);
    }

    [Fact]
    public void GetInputReturnsNewListInstance()
    {
        using StringReader input1 = new("1-2");
        using StringReader input2 = new("1-2");

        List<BigInteger> result1 = InputParser.GetInput(input1);
        List<BigInteger> result2 = InputParser.GetInput(input2);

        Assert.NotSame(result1, result2);
        Assert.Equal(result1, result2);
    }

    private static readonly int[] _sourceArray = [1, 2, 3, 4, 5, 6];

    [Fact]
    public void GetInputWithConsecutiveRangesReturnsAllValues()
    {
        using StringReader input = new("1-3,4-6");

        List<BigInteger> result = InputParser.GetInput(input);

        Assert.Equal(6, result.Count);
        List<BigInteger> expected = [.. _sourceArray.Select(x => new BigInteger(x))];
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetInputWithOverlappingRangesIncludesDuplicates()
    {
        using StringReader input = new("1-3,2-4");

        List<BigInteger> result = InputParser.GetInput(input);

        Assert.Equal(6, result.Count);
        Assert.Equal(new BigInteger(1), result[0]);
        Assert.Equal(new BigInteger(2), result[1]);
        Assert.Equal(new BigInteger(3), result[2]);
        Assert.Equal(new BigInteger(2), result[3]);
        Assert.Equal(new BigInteger(3), result[4]);
        Assert.Equal(new BigInteger(4), result[5]);
    }
}
