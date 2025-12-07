using System.Numerics;

namespace Day2b.Tests;

/// <summary>
/// Unit tests for the <see cref="InputParser.GetInput"/> method.
/// </summary>
public class InputParserTests
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
    public void GetInputReturnsNewListInstance()
    {
        using StringReader input1 = new("1-2");
        using StringReader input2 = new("1-2");

        List<BigInteger> result1 = InputParser.GetInput(input1);
        List<BigInteger> result2 = InputParser.GetInput(input2);

        Assert.NotSame(result1, result2);
        Assert.Equal(result1, result2);
    }

    [Fact]
    public void GetInputWithConsecutiveRangesReturnsAllValues()
    {
        using StringReader input = new("1-3,4-6");

        List<BigInteger> result = InputParser.GetInput(input);

        Assert.Equal(6, result.Count);
        Assert.Equal(new BigInteger(1), result[0]);
        Assert.Equal(new BigInteger(2), result[1]);
        Assert.Equal(new BigInteger(3), result[2]);
        Assert.Equal(new BigInteger(4), result[3]);
        Assert.Equal(new BigInteger(5), result[4]);
        Assert.Equal(new BigInteger(6), result[5]);
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

    [Fact]
    public void GetInputWithReversedRangeThrowsFormatException()
    {
        using StringReader input = new("10-5");

        FormatException exception = Assert.Throws<FormatException>(
            () => InputParser.GetInput(input));

        Assert.Equal("Start of range must be less than or equal to end: 10-5", exception.Message);
    }

    [Fact]
    public void GetInputWithMultipleRangesIncludingReversedRangeThrowsFormatException()
    {
        using StringReader input = new("1-3,10-5,7-9");

        FormatException exception = Assert.Throws<FormatException>(
            () => InputParser.GetInput(input));

        Assert.Equal("Start of range must be less than or equal to end: 10-5", exception.Message);
    }

    [Fact]
    public void GetInputWithNegativeStartThrowsFormatException()
    {
        using StringReader input = new("-3--1");

        FormatException exception = Assert.Throws<FormatException>(
            () => InputParser.GetInput(input));

        Assert.Equal("Invalid range format: -3--1", exception.Message);
    }

    [Fact]
    public void GetInputWithNegativeEndThrowsFormatException()
    {
        using StringReader input = new("1--5");

        FormatException exception = Assert.Throws<FormatException>(
            () => InputParser.GetInput(input));

        Assert.Equal("Invalid range format: 1--5", exception.Message);
    }

    [Fact]
    public void GetInputWithMixedNegativeAndPositiveRangeThrowsFormatException()
    {
        using StringReader input = new("-2-2");

        FormatException exception = Assert.Throws<FormatException>(
            () => InputParser.GetInput(input));

        Assert.Equal("Invalid range format: -2-2", exception.Message);
    }

    [Fact]
    public void GetInputWithVeryLargeRangeOfLargeNumbersReturnsCorrectValues()
    {
        string start = "999999999999999999";
        string end = "1000000000000000001";
        using StringReader input = new($"{start}-{end}");

        List<BigInteger> result = InputParser.GetInput(input);

        Assert.Equal(3, result.Count);
        Assert.Equal(BigInteger.Parse(start), result[0]);
        Assert.Equal(BigInteger.Parse("1000000000000000000"), result[1]);
        Assert.Equal(BigInteger.Parse(end), result[2]);
    }

    [Fact]
    public void GetInputWithWhitespaceBeforeAndAfterRangeThrowsFormatException()
    {
        using StringReader input = new(" 1-3 ");

        FormatException exception = Assert.Throws<FormatException>(
            () => InputParser.GetInput(input));

        Assert.Equal("Spaces not allowed around hyphen in range:  1-3 ", exception.Message);
    }

    [Theory]
    [InlineData("1 - 3")]
    [InlineData("1 -3")]
    [InlineData("1- 3")]
    [InlineData("1  -  3")]
    public void GetInputWithSpacesAroundHyphenThrowsFormatException(string invalidInput)
    {
        using StringReader input = new(invalidInput);

        FormatException exception = Assert.Throws<FormatException>(
            () => InputParser.GetInput(input));

        Assert.Equal($"Spaces not allowed around hyphen in range: {invalidInput}", exception.Message);
    }

    [Fact]
    public void GetInputWithMultipleConsecutiveCommasThrowsFormatException()
    {
        using StringReader input = new("1-2,,3-4");

        _ = Assert.Throws<FormatException>(() => InputParser.GetInput(input));
    }

    [Fact]
    public void GetInputWithTrailingCommaThrowsFormatException()
    {
        using StringReader input = new("1-2,3-4,");

        _ = Assert.Throws<FormatException>(() => InputParser.GetInput(input));
    }

    [Fact]
    public void GetInputWithLeadingCommaThrowsFormatException()
    {
        using StringReader input = new(",1-2,3-4");

        _ = Assert.Throws<FormatException>(() => InputParser.GetInput(input));
    }

    [Fact]
    public void GetInputWithOnlyCommasThrowsFormatException()
    {
        using StringReader input = new(",,,");

        _ = Assert.Throws<FormatException>(() => InputParser.GetInput(input));
    }

    [Fact]
    public void GetInputWithSingleHyphenThrowsFormatException()
    {
        using StringReader input = new("-");

        _ = Assert.Throws<FormatException>(() => InputParser.GetInput(input));
    }

    [Fact]
    public void GetInputWithRangeStartingAtZeroReturnsCorrectValues()
    {
        using StringReader input = new("0-5");

        List<BigInteger> result = InputParser.GetInput(input);

        Assert.Equal(6, result.Count);
        Assert.Equal(BigInteger.Zero, result[0]);
        Assert.Equal(new BigInteger(5), result[^1]);
    }

    [Fact]
    public void GetInputWithManySmallRangesReturnsCorrectCount()
    {
        using StringReader input = new("1-1,2-2,3-3,4-4,5-5");

        List<BigInteger> result = InputParser.GetInput(input);

        Assert.Equal(5, result.Count);
    }

    [Fact]
    public void GetInputWithScientificNotationThrowsFormatException()
    {
        using StringReader input = new("1e5-1e6");

        _ = Assert.Throws<FormatException>(() => InputParser.GetInput(input));
    }

    [Fact]
    public void GetInputWithDecimalNumbersThrowsFormatException()
    {
        using StringReader input = new("1.5-3.5");

        _ = Assert.Throws<FormatException>(() => InputParser.GetInput(input));
    }
}
