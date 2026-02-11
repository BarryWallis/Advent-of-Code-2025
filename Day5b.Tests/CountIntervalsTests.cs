using System.Numerics;

namespace Day5b.Tests;

/// <summary>
/// Unit tests for the CountIntervals method in <see cref="Program"/>.
/// These tests verify correct counting of values covered by intervals.
/// </summary>
public class CountIntervalsTests
{
    [Fact]
    public void CountIntervalsWithSingleIntervalReturnsCorrectCount()
    {
        List<Interval> intervals =
        [
            new Interval(10, 20)
        ];

        BigInteger count = Program.CountIntervals(intervals);

        Assert.Equal(new BigInteger(11), count);
    }

    [Fact]
    public void CountIntervalsWithMultipleNonOverlappingIntervalsReturnsSumOfAllCounts()
    {
        List<Interval> intervals =
        [
            new Interval(10, 20),
            new Interval(30, 40),
            new Interval(50, 60)
        ];

        BigInteger count = Program.CountIntervals(intervals);

        Assert.Equal(new BigInteger(33), count);
    }

    [Fact]
    public void CountIntervalsWithSingleValueIntervalReturnsOne()
    {
        List<Interval> intervals =
        [
            new Interval(15, 15)
        ];

        BigInteger count = Program.CountIntervals(intervals);

        Assert.Equal(BigInteger.One, count);
    }

    [Fact]
    public void CountIntervalsWithMultipleSingleValueIntervalsReturnsSumOfOnes()
    {
        List<Interval> intervals =
        [
            new Interval(10, 10),
            new Interval(20, 20),
            new Interval(30, 30)
        ];

        BigInteger count = Program.CountIntervals(intervals);

        Assert.Equal(new BigInteger(3), count);
    }

    [Fact]
    public void CountIntervalsWithEmptyListReturnsZero()
    {
        List<Interval> intervals = [];

        BigInteger count = Program.CountIntervals(intervals);

        Assert.Equal(BigInteger.Zero, count);
    }

    [Fact]
    public void CountIntervalsWithLargeBigIntegerValuesReturnsCorrectCount()
    {
        BigInteger largeStart = BigInteger.Parse("123456789012345678901234567890");
        BigInteger largeEnd = BigInteger.Parse("123456789012345678901234567900");

        List<Interval> intervals =
        [
            new Interval(largeStart, largeEnd)
        ];

        BigInteger count = Program.CountIntervals(intervals);

        Assert.Equal(new BigInteger(11), count);
    }

    [Fact]
    public void CountIntervalsWithVeryLargeIntervalReturnsCorrectCount()
    {
        BigInteger start = BigInteger.One;
        BigInteger end = BigInteger.Parse("1000000000000000000000000");

        List<Interval> intervals =
        [
            new Interval(start, end)
        ];

        BigInteger count = Program.CountIntervals(intervals);

        Assert.Equal(BigInteger.Parse("1000000000000000000000000"), count);
    }

    [Fact]
    public void CountIntervalsWithMixedSizesReturnsCorrectSum()
    {
        List<Interval> intervals =
        [
            new Interval(1, 1),
            new Interval(10, 20),
            new Interval(100, 200)
        ];

        BigInteger count = Program.CountIntervals(intervals);

        Assert.Equal(new BigInteger(113), count);
    }

    [Fact]
    public void CountIntervalsWithConsecutiveRangesReturnsCorrectCount()
    {
        List<Interval> intervals =
        [
            new Interval(1, 10),
            new Interval(11, 20),
            new Interval(21, 30)
        ];

        BigInteger count = Program.CountIntervals(intervals);

        Assert.Equal(new BigInteger(30), count);
    }

    [Fact]
    public void CountIntervalsWithOverlappingIntervalsCountsEachSeparately()
    {
        List<Interval> intervals =
        [
            new Interval(10, 20),
            new Interval(15, 25)
        ];

        BigInteger count = Program.CountIntervals(intervals);

        Assert.Equal(new BigInteger(22), count);
    }

    [Fact]
    public void CountIntervalsAfterNormalizationWithOverlappingIntervalsReturnsCorrectCount()
    {
        List<Interval> intervals =
        [
            new Interval(10, 20),
            new Interval(15, 25)
        ];
        Program.NormalizeIntervals(intervals);

        BigInteger count = Program.CountIntervals(intervals);

        Assert.Equal(new BigInteger(16), count);
    }

    [Fact]
    public void CountIntervalsWithIntervalOfTwoValuesReturnsTwo()
    {
        List<Interval> intervals =
        [
            new Interval(10, 11)
        ];

        BigInteger count = Program.CountIntervals(intervals);

        Assert.Equal(new BigInteger(2), count);
    }

    [Fact]
    public void CountIntervalsWithMaximumBigIntegerValueHandlesCorrectly()
    {
        BigInteger veryLarge = BigInteger.Parse("999999999999999999999999999999999999999999");
        BigInteger veryLargePlus100 = veryLarge + 100;

        List<Interval> intervals =
        [
            new Interval(veryLarge, veryLargePlus100)
        ];

        BigInteger count = Program.CountIntervals(intervals);

        Assert.Equal(new BigInteger(101), count);
    }

    [Fact]
    public void CountIntervalsWithMultipleLargeIntervalsReturnsCorrectSum()
    {
        BigInteger base1 = BigInteger.Parse("100000000000000000000000");
        BigInteger base2 = BigInteger.Parse("200000000000000000000000");

        List<Interval> intervals =
        [
            new Interval(base1, base1 + 999),
            new Interval(base2, base2 + 999)
        ];

        BigInteger count = Program.CountIntervals(intervals);

        Assert.Equal(new BigInteger(2000), count);
    }

    [Fact]
    public void CountIntervalsWithComplexNormalizedSetReturnsCorrectCount()
    {
        List<Interval> intervals =
        [
            new Interval(1, 100),
            new Interval(50, 150),
            new Interval(200, 300)
        ];
        Program.NormalizeIntervals(intervals);

        BigInteger count = Program.CountIntervals(intervals);

        Assert.Equal(new BigInteger(251), count);
    }

    [Fact]
    public void CountIntervalsWithOneHundredIntervalsOfSingleValuesReturnsOneHundred()
    {
        List<Interval> intervals = [];
        for (int i = 1; i <= 100; i++)
        {
            intervals.Add(new Interval(i * 10, i * 10));
        }

        BigInteger count = Program.CountIntervals(intervals);

        Assert.Equal(new BigInteger(100), count);
    }

    [Fact]
    public void CountIntervalsWithIntervalsOneApartReturnsCorrectCount()
    {
        List<Interval> intervals =
        [
            new Interval(1, 5),
            new Interval(7, 11),
            new Interval(13, 17)
        ];

        BigInteger count = Program.CountIntervals(intervals);

        Assert.Equal(new BigInteger(15), count);
    }

    [Fact]
    public void CountIntervalsWithSmallAndLargeIntervalsMixedReturnsCorrectSum()
    {
        List<Interval> intervals =
        [
            new Interval(1, 1),
            new Interval(1000, 2000),
            new Interval(5, 5),
            new Interval(10000, 20000)
        ];

        BigInteger count = Program.CountIntervals(intervals);

        Assert.Equal(new BigInteger(11004), count);
    }
}
