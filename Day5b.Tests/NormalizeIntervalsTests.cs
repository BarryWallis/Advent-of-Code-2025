using System.Numerics;

namespace Day5b.Tests;

/// <summary>
/// Unit tests for the NormalizeIntervals method in <see cref="Program"/>.
/// These tests verify correct merging of overlapping, adjacent, and contained intervals.
/// </summary>
public class NormalizeIntervalsTests
{
    [Fact]
    public void NormalizeIntervalsWithSingleIntervalLeavesItUnchanged()
    {
        List<Interval> intervals =
        [
            new Interval(10, 20)
        ];

        Program.NormalizeIntervals(intervals);

        Interval single = Assert.Single(intervals);
        Assert.Equal(new BigInteger(10), single.start);
        Assert.Equal(new BigInteger(20), single.end);
    }

    [Fact]
    public void NormalizeIntervalsWithNonOverlappingIntervalsLeavesThemUnchanged()
    {
        List<Interval> intervals =
        [
            new Interval(10, 20),
            new Interval(30, 40),
            new Interval(50, 60)
        ];

        Program.NormalizeIntervals(intervals);

        Assert.Equal(3, intervals.Count);
        Assert.Equal(new BigInteger(10), intervals[0].start);
        Assert.Equal(new BigInteger(20), intervals[0].end);
        Assert.Equal(new BigInteger(30), intervals[1].start);
        Assert.Equal(new BigInteger(40), intervals[1].end);
        Assert.Equal(new BigInteger(50), intervals[2].start);
        Assert.Equal(new BigInteger(60), intervals[2].end);
    }

    [Fact]
    public void NormalizeIntervalsWithOverlappingIntervalsMergesThem()
    {
        List<Interval> intervals =
        [
            new Interval(10, 20),
            new Interval(15, 25)
        ];

        Program.NormalizeIntervals(intervals);

        Interval single = Assert.Single(intervals);
        Assert.Equal(new BigInteger(10), single.start);
        Assert.Equal(new BigInteger(25), single.end);
    }

    [Fact]
    public void NormalizeIntervalsWithAdjacentIntervalsMergesThem()
    {
        List<Interval> intervals =
        [
            new Interval(10, 20),
            new Interval(21, 30)
        ];

        Program.NormalizeIntervals(intervals);

        Interval single = Assert.Single(intervals);
        Assert.Equal(new BigInteger(10), single.start);
        Assert.Equal(new BigInteger(30), single.end);
    }

    [Fact]
    public void NormalizeIntervalsWithIntervalContainedInAnotherRemovesIt()
    {
        List<Interval> intervals =
        [
            new Interval(10, 50),
            new Interval(20, 30)
        ];

        Program.NormalizeIntervals(intervals);

        Interval single = Assert.Single(intervals);
        Assert.Equal(new BigInteger(10), single.start);
        Assert.Equal(new BigInteger(50), single.end);
    }

    [Fact]
    public void NormalizeIntervalsWithIdenticalIntervalsRemovesDuplicates()
    {
        List<Interval> intervals =
        [
            new Interval(10, 20),
            new Interval(10, 20),
            new Interval(10, 20)
        ];

        Program.NormalizeIntervals(intervals);

        Interval single = Assert.Single(intervals);
        Assert.Equal(new BigInteger(10), single.start);
        Assert.Equal(new BigInteger(20), single.end);
    }

    [Fact]
    public void NormalizeIntervalsWithMultipleOverlapsCreatesOneInterval()
    {
        List<Interval> intervals =
        [
            new Interval(10, 20),
            new Interval(15, 25),
            new Interval(22, 30),
            new Interval(28, 40)
        ];

        Program.NormalizeIntervals(intervals);

        Interval single = Assert.Single(intervals);
        Assert.Equal(new BigInteger(10), single.start);
        Assert.Equal(new BigInteger(40), single.end);
    }

    [Fact]
    public void NormalizeIntervalsWithMixedOverlappingAndNonOverlappingIntervalsNormalizesCorrectly()
    {
        List<Interval> intervals =
        [
            new Interval(10, 20),
            new Interval(15, 25),
            new Interval(50, 60),
            new Interval(55, 70),
            new Interval(100, 110)
        ];

        Program.NormalizeIntervals(intervals);

        Assert.Equal(3, intervals.Count);
        Assert.Equal(new BigInteger(10), intervals[0].start);
        Assert.Equal(new BigInteger(25), intervals[0].end);
        Assert.Equal(new BigInteger(50), intervals[1].start);
        Assert.Equal(new BigInteger(70), intervals[1].end);
        Assert.Equal(new BigInteger(100), intervals[2].start);
        Assert.Equal(new BigInteger(110), intervals[2].end);
    }

    [Fact]
    public void NormalizeIntervalsWithNestedIntervalsRemovesInnerOnes()
    {
        List<Interval> intervals =
        [
            new Interval(10, 100),
            new Interval(20, 30),
            new Interval(40, 50),
            new Interval(60, 70)
        ];

        Program.NormalizeIntervals(intervals);

        Interval single = Assert.Single(intervals);
        Assert.Equal(new BigInteger(10), single.start);
        Assert.Equal(new BigInteger(100), single.end);
    }

    [Fact]
    public void NormalizeIntervalsWithReverseOrderIntervalsNormalizesCorrectly()
    {
        List<Interval> intervals =
        [
            new Interval(50, 60),
            new Interval(30, 40),
            new Interval(10, 20)
        ];

        Program.NormalizeIntervals(intervals);

        Assert.Equal(3, intervals.Count);
    }

    [Fact]
    public void NormalizeIntervalsWithComplexOverlappingPatternMergesCorrectly()
    {
        List<Interval> intervals =
        [
            new Interval(10, 30),
            new Interval(20, 50),
            new Interval(40, 60),
            new Interval(70, 80),
            new Interval(75, 85)
        ];

        Program.NormalizeIntervals(intervals);

        Assert.Equal(2, intervals.Count);
        Assert.Equal(new BigInteger(10), intervals[0].start);
        Assert.Equal(new BigInteger(60), intervals[0].end);
        Assert.Equal(new BigInteger(70), intervals[1].start);
        Assert.Equal(new BigInteger(85), intervals[1].end);
    }

    [Fact]
    public void NormalizeIntervalsWithLargeBigIntegerValuesMergesCorrectly()
    {
        BigInteger largeStart1 = BigInteger.Parse("123456789012345678901234567890");
        BigInteger largeEnd1 = BigInteger.Parse("123456789012345678901234567900");
        BigInteger largeStart2 = BigInteger.Parse("123456789012345678901234567895");
        BigInteger largeEnd2 = BigInteger.Parse("123456789012345678901234567910");

        List<Interval> intervals =
        [
            new Interval(largeStart1, largeEnd1),
            new Interval(largeStart2, largeEnd2)
        ];

        Program.NormalizeIntervals(intervals);

        Interval single = Assert.Single(intervals);
        Assert.Equal(largeStart1, single.start);
        Assert.Equal(largeEnd2, single.end);
    }

    [Fact]
    public void NormalizeIntervalsWithEmptyListLeavesItEmpty()
    {
        List<Interval> intervals = [];

        Program.NormalizeIntervals(intervals);

        Assert.Empty(intervals);
    }

    [Fact]
    public void NormalizeIntervalsWithAllIntervalsOverlappingCreatesOneInterval()
    {
        List<Interval> intervals =
        [
            new Interval(10, 20),
            new Interval(12, 22),
            new Interval(14, 24),
            new Interval(16, 26),
            new Interval(18, 28)
        ];

        Program.NormalizeIntervals(intervals);

        Interval single = Assert.Single(intervals);
        Assert.Equal(new BigInteger(10), single.start);
        Assert.Equal(new BigInteger(28), single.end);
    }

    [Fact]
    public void NormalizeIntervalsWithChainOfAdjacentIntervalsMergesAll()
    {
        List<Interval> intervals =
        [
            new Interval(10, 20),
            new Interval(21, 30),
            new Interval(31, 40),
            new Interval(41, 50)
        ];

        Program.NormalizeIntervals(intervals);

        Interval single = Assert.Single(intervals);
        Assert.Equal(new BigInteger(10), single.start);
        Assert.Equal(new BigInteger(50), single.end);
    }

    [Fact]
    public void NormalizeIntervalsWithSingleValueIntervalsHandlesCorrectly()
    {
        List<Interval> intervals =
        [
            new Interval(10, 10),
            new Interval(11, 11),
            new Interval(12, 12)
        ];

        Program.NormalizeIntervals(intervals);

        Interval single = Assert.Single(intervals);
        Assert.Equal(new BigInteger(10), single.start);
        Assert.Equal(new BigInteger(12), single.end);
    }

    [Fact]
    public void NormalizeIntervalsWithPartialOverlapAtStartMerges()
    {
        List<Interval> intervals =
        [
            new Interval(10, 30),
            new Interval(5, 15)
        ];

        Program.NormalizeIntervals(intervals);

        Interval single = Assert.Single(intervals);
        Assert.Equal(new BigInteger(5), single.start);
        Assert.Equal(new BigInteger(30), single.end);
    }

    [Fact]
    public void NormalizeIntervalsWithPartialOverlapAtEndMerges()
    {
        List<Interval> intervals =
        [
            new Interval(10, 30),
            new Interval(25, 40)
        ];

        Program.NormalizeIntervals(intervals);

        Interval single = Assert.Single(intervals);
        Assert.Equal(new BigInteger(10), single.start);
        Assert.Equal(new BigInteger(40), single.end);
    }

    [Fact]
    public void NormalizeIntervalsWithOneIntervalContainingMultipleOthersRemovesAllContained()
    {
        List<Interval> intervals =
        [
            new Interval(1, 100),
            new Interval(10, 20),
            new Interval(30, 40),
            new Interval(50, 60),
            new Interval(70, 80)
        ];

        Program.NormalizeIntervals(intervals);

        Interval single = Assert.Single(intervals);
        Assert.Equal(new BigInteger(1), single.start);
        Assert.Equal(new BigInteger(100), single.end);
    }
}
