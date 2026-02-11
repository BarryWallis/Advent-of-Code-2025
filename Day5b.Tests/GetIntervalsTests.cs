using System.Numerics;

namespace Day5b.Tests;

/// <summary>
/// Unit tests for the GetIntervals method in <see cref="Program"/>.
/// These tests verify correct parsing of interval data from input streams.
/// </summary>
public class GetIntervalsTests
{
    /// <summary>
    /// Helper method to create a StringReader from text input.
    /// </summary>
    private static StringReader CreateReader(string input) => new(input);

    [Fact]
    public void GetIntervalsWithSingleIntervalReturnsSingleInterval()
    {
        string input = "10-20";
        StringReader reader = CreateReader(input);

        List<Interval> intervals = Program.GetIntervals(reader);

        _ = Assert.Single(intervals);
        Assert.Equal(new BigInteger(10), intervals[0].start);
        Assert.Equal(new BigInteger(20), intervals[0].end);
    }

    [Fact]
    public void GetIntervalsWithMultipleIntervalsReturnsAllIntervals()
    {
        string input = $"10-20{Environment.NewLine}30-40{Environment.NewLine}50-60";
        StringReader reader = CreateReader(input);

        List<Interval> intervals = Program.GetIntervals(reader);

        Assert.Equal(3, intervals.Count);
        Assert.Equal(new BigInteger(10), intervals[0].start);
        Assert.Equal(new BigInteger(20), intervals[0].end);
        Assert.Equal(new BigInteger(30), intervals[1].start);
        Assert.Equal(new BigInteger(40), intervals[1].end);
        Assert.Equal(new BigInteger(50), intervals[2].start);
        Assert.Equal(new BigInteger(60), intervals[2].end);
    }

    [Fact]
    public void GetIntervalsWithLargeBigIntegerValuesHandlesThemCorrectly()
    {
        string input = "123456789012345678901234567890-987654321098765432109876543210";
        StringReader reader = CreateReader(input);

        List<Interval> intervals = Program.GetIntervals(reader);

        _ = Assert.Single(intervals);
        Assert.Equal(BigInteger.Parse("123456789012345678901234567890"), intervals[0].start);
        Assert.Equal(BigInteger.Parse("987654321098765432109876543210"), intervals[0].end);
    }

    [Fact]
    public void GetIntervalsWithTrailingBlankLinesStopsAtFirstBlankLine()
    {
        string input = $"10-20{Environment.NewLine}30-40{Environment.NewLine}{Environment.NewLine}50-60";
        StringReader reader = CreateReader(input);

        List<Interval> intervals = Program.GetIntervals(reader);

        Assert.Equal(2, intervals.Count);
        Assert.Equal(new BigInteger(10), intervals[0].start);
        Assert.Equal(new BigInteger(20), intervals[0].end);
        Assert.Equal(new BigInteger(30), intervals[1].start);
        Assert.Equal(new BigInteger(40), intervals[1].end);
    }

    [Fact]
    public void GetIntervalsWithEmptyInputThrowsInvalidDataException()
    {
        string input = string.Empty;
        StringReader reader = CreateReader(input);

        InvalidDataException exception = Assert.Throws<InvalidDataException>(() => Program.GetIntervals(reader));

        Assert.Equal("Input contains no data. Expected at least one interval.", exception.Message);
    }

    [Fact]
    public void GetIntervalsWithOnlyWhitespaceThrowsInvalidDataException()
    {
        string input = $"{Environment.NewLine}{Environment.NewLine}";
        StringReader reader = CreateReader(input);

        InvalidDataException exception = Assert.Throws<InvalidDataException>(() => Program.GetIntervals(reader));

        Assert.Equal("Input contains no data. Expected at least one interval.", exception.Message);
    }

    [Fact]
    public void GetIntervalsWithInvalidFormatMissingDashThrowsFormatException()
    {
        string input = "10 20";
        StringReader reader = CreateReader(input);

        FormatException exception = Assert.Throws<FormatException>(() => Program.GetIntervals(reader));

        Assert.Equal("Invalid interval format: '10 20'. Expected format: 'start-end'", exception.Message);
    }

    [Fact]
    public void GetIntervalsWithInvalidFormatMultipleDashesThrowsFormatException()
    {
        string input = "10-20-30";
        StringReader reader = CreateReader(input);

        FormatException exception = Assert.Throws<FormatException>(() => Program.GetIntervals(reader));

        Assert.Equal("Invalid interval format: '10-20-30'. Expected format: 'start-end'", exception.Message);
    }

    [Fact]
    public void GetIntervalsWithNonNumericStartThrowsFormatException()
    {
        string input = "abc-20";
        StringReader reader = CreateReader(input);

        _ = Assert.Throws<FormatException>(() => Program.GetIntervals(reader));
    }

    [Fact]
    public void GetIntervalsWithNonNumericEndThrowsFormatException()
    {
        string input = "10-xyz";
        StringReader reader = CreateReader(input);

        _ = Assert.Throws<FormatException>(() => Program.GetIntervals(reader));
    }

    [Fact]
    public void GetIntervalsWithInvertedRangeThrowsArgumentException()
    {
        string input = "10-5";
        StringReader reader = CreateReader(input);

        ArgumentException exception = Assert.Throws<ArgumentException>(() => Program.GetIntervals(reader));

        Assert.Contains("Start value", exception.Message);
        Assert.Contains("must be less than or equal to end value", exception.Message);
    }

    [Fact]
    public void GetIntervalsWithZeroValuesThrowsArgumentException()
    {
        string input = "0-0";
        StringReader reader = CreateReader(input);

        ArgumentException exception = Assert.Throws<ArgumentException>(() => Program.GetIntervals(reader));

        Assert.Contains("must be positive", exception.Message);
    }

    [Fact]
    public void GetIntervalsWithMixedValidAndInvalidLinesThrowsFormatException()
    {
        string input = $"10-20{Environment.NewLine}invalid{Environment.NewLine}30-40";
        StringReader reader = CreateReader(input);

        FormatException exception = Assert.Throws<FormatException>(() => Program.GetIntervals(reader));

        Assert.Equal("Invalid interval format: 'invalid'. Expected format: 'start-end'", exception.Message);
    }
}
