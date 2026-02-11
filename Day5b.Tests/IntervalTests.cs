using System.Numerics;

namespace Day5b.Tests;

/// <summary>
/// Unit tests for the <see cref="Interval"/> class.
/// These tests verify interval validation and construction.
/// </summary>
public class IntervalTests
{
    [Fact]
    public void ConstructorWithValidIntervalCreatesInterval()
    {
        BigInteger start = new(10);
        BigInteger end = new(20);

        Interval interval = new(start, end);

        Assert.Equal(start, interval.start);
        Assert.Equal(end, interval.end);
    }

    [Fact]
    public void ConstructorWithEqualStartAndEndCreatesInterval()
    {
        BigInteger value = new(15);

        Interval interval = new(value, value);

        Assert.Equal(value, interval.start);
        Assert.Equal(value, interval.end);
    }

    [Fact]
    public void ConstructorWithStartGreaterThanEndThrowsArgumentException()
    {
        BigInteger start = new(30);
        BigInteger end = new(10);

        ArgumentException exception = Assert.Throws<ArgumentException>(() => new Interval(start, end));

        Assert.Contains("Start value", exception.Message);
        Assert.Contains("must be less than or equal to end value", exception.Message);
        Assert.Equal("start", exception.ParamName);
    }

    [Fact]
    public void ConstructorWithLargeBigIntegerValuesAndValidOrderCreatesInterval()
    {
        BigInteger start = BigInteger.Parse("123456789012345678901234567890");
        BigInteger end = BigInteger.Parse("987654321098765432109876543210");

        Interval interval = new(start, end);

        Assert.Equal(start, interval.start);
        Assert.Equal(end, interval.end);
    }

    [Fact]
    public void ConstructorWithLargeBigIntegerValuesAndInvalidOrderThrowsArgumentException()
    {
        BigInteger start = BigInteger.Parse("987654321098765432109876543210");
        BigInteger end = BigInteger.Parse("123456789012345678901234567890");

        ArgumentException exception = Assert.Throws<ArgumentException>(() => new Interval(start, end));

        Assert.Equal("start", exception.ParamName);
    }

    [Fact]
    public void ConstructorWithNegativeStartThrowsArgumentException()
    {
        BigInteger start = new(-50);
        BigInteger end = new(10);

        ArgumentException exception = Assert.Throws<ArgumentException>(() => new Interval(start, end));

        Assert.Contains("Start value", exception.Message);
        Assert.Contains("must be positive", exception.Message);
        Assert.Equal("start", exception.ParamName);
    }

    [Fact]
    public void ConstructorWithNegativeEndThrowsArgumentException()
    {
        BigInteger start = new(10);
        BigInteger end = new(-25);

        ArgumentException exception = Assert.Throws<ArgumentException>(() => new Interval(start, end));

        Assert.Contains("End value", exception.Message);
        Assert.Contains("must be positive", exception.Message);
        Assert.Equal("end", exception.ParamName);
    }

    [Fact]
    public void ConstructorWithZeroStartThrowsArgumentException()
    {
        BigInteger start = BigInteger.Zero;
        BigInteger end = new(10);

        ArgumentException exception = Assert.Throws<ArgumentException>(() => new Interval(start, end));

        Assert.Contains("Start value", exception.Message);
        Assert.Contains("must be positive", exception.Message);
        Assert.Equal("start", exception.ParamName);
    }

    [Fact]
    public void SetStartToValueGreaterThanEndThrowsArgumentException()
    {
        Interval interval = new(10, 20);

        ArgumentException exception = Assert.Throws<ArgumentException>(() => interval.start = 30);

        Assert.Contains("Start value", exception.Message);
        Assert.Contains("must be less than or equal to end value", exception.Message);
        Assert.Equal("start", exception.ParamName);
    }

    [Fact]
    public void SetEndToValueLessThanStartThrowsArgumentException()
    {
        Interval interval = new(10, 20);

        ArgumentException exception = Assert.Throws<ArgumentException>(() => interval.end = 5);

        Assert.Contains("Start value", exception.Message);
        Assert.Contains("must be less than or equal to end value", exception.Message);
        Assert.Equal("end", exception.ParamName);
    }

    [Fact]
    public void SetStartToValidValueUpdatesStart()
    {
        Interval interval = new(10, 20)
        {
            start = 15
        };

        Assert.Equal(new BigInteger(15), interval.start);
        Assert.Equal(new BigInteger(20), interval.end);
    }

    [Fact]
    public void SetEndToValidValueUpdatesEnd()
    {
        Interval interval = new(10, 20)
        {
            end = 25
        };

        Assert.Equal(new BigInteger(10), interval.start);
        Assert.Equal(new BigInteger(25), interval.end);
    }

    [Fact]
    public void SetStartToEqualEndUpdatesStart()
    {
        Interval interval = new(10, 20)
        {
            start = 20
        };

        Assert.Equal(new BigInteger(20), interval.start);
        Assert.Equal(new BigInteger(20), interval.end);
    }

    [Fact]
    public void SetEndToEqualStartUpdatesEnd()
    {
        Interval interval = new(10, 20)
        {
            end = 10
        };

        Assert.Equal(new BigInteger(10), interval.start);
        Assert.Equal(new BigInteger(10), interval.end);
    }

    [Fact]
    public void ConstructorWithBothNegativeValuesThrowsArgumentException()
    {
        BigInteger start = new(-50);
        BigInteger end = new(-10);

        ArgumentException exception = Assert.Throws<ArgumentException>(() => new Interval(start, end));

        Assert.Contains("must be positive", exception.Message);
    }

    [Fact]
    public void SetStartToNegativeValueThrowsArgumentException()
    {
        Interval interval = new(10, 20);

        ArgumentException exception = Assert.Throws<ArgumentException>(() => interval.start = -5);

        Assert.Contains("Start value", exception.Message);
        Assert.Contains("must be positive", exception.Message);
        Assert.Equal("start", exception.ParamName);
    }

    [Fact]
    public void SetEndToNegativeValueThrowsArgumentException()
    {
        Interval interval = new(10, 20);

        ArgumentException exception = Assert.Throws<ArgumentException>(() => interval.end = -5);

        Assert.Contains("End value", exception.Message);
        Assert.Contains("must be positive", exception.Message);
        Assert.Equal("end", exception.ParamName);
    }

    [Fact]
    public void ConstructorWithZeroEndThrowsArgumentException()
    {
        BigInteger start = new(10);
        BigInteger end = BigInteger.Zero;

        ArgumentException exception = Assert.Throws<ArgumentException>(() => new Interval(start, end));

        Assert.Contains("End value", exception.Message);
        Assert.Contains("must be positive", exception.Message);
        Assert.Equal("end", exception.ParamName);
    }

    [Fact]
    public void ConstructorWithBothZeroValuesThrowsArgumentException()
    {
        BigInteger start = BigInteger.Zero;
        BigInteger end = BigInteger.Zero;

        ArgumentException exception = Assert.Throws<ArgumentException>(() => new Interval(start, end));

        Assert.Contains("must be positive", exception.Message);
    }

    [Fact]
    public void SetStartToZeroThrowsArgumentException()
    {
        Interval interval = new(10, 20);

        ArgumentException exception = Assert.Throws<ArgumentException>(() => interval.start = 0);

        Assert.Contains("Start value", exception.Message);
        Assert.Contains("must be positive", exception.Message);
        Assert.Equal("start", exception.ParamName);
    }

    [Fact]
    public void SetEndToZeroThrowsArgumentException()
    {
        Interval interval = new(10, 20);

        ArgumentException exception = Assert.Throws<ArgumentException>(() => interval.end = 0);

        Assert.Contains("End value", exception.Message);
        Assert.Contains("must be positive", exception.Message);
        Assert.Equal("end", exception.ParamName);
    }
}
