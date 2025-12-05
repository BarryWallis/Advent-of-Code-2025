namespace Day1b.Tests;

/// <summary>
/// Unit tests for the <see cref="Dial"/> class.
/// </summary>
public class DialTests
{
    [Fact]
    public void ConstructorWithDefaultParametersCreatesDialAtPosition0With100Positions()
    {
        Dial dial = new();

        Assert.Equal(100, dial.Count);
        Assert.Equal(0, dial.Position.Value);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(25)]
    [InlineData(50)]
    [InlineData(99)]
    public void ConstructorWithStartingPositionCreatesDialAtThatPosition(int startingPosition)
    {
        Dial dial = new(startingPosition);

        Assert.Equal(100, dial.Count);
        Assert.Equal(startingPosition, dial.Position.Value);
    }

    [Theory]
    [InlineData(0, 50)]
    [InlineData(10, 100)]
    [InlineData(25, 200)]
    public void ConstructorWithStartingPositionAndCountCreatesDialWithSpecifiedValues(int startingPosition, int count)
    {
        Dial dial = new(startingPosition, count);

        Assert.Equal(count, dial.Count);
        Assert.Equal(startingPosition, dial.Position.Value);
    }

    [Theory]
    [InlineData(100, 100)]
    [InlineData(101, 100)]
    [InlineData(150, 100)]
    public void ConstructorNormalizesStartingPositionGreaterThanOrEqualToCount(int startingPosition, int count)
    {
        Dial dial = new(startingPosition, count);

        Assert.Equal(startingPosition % count, dial.Position.Value);
    }

    [Theory]
    [InlineData(-1, 100, 99)]
    [InlineData(-10, 100, 90)]
    [InlineData(-50, 100, 50)]
    public void ConstructorNormalizesNegativeStartingPosition(int startingPosition, int count, int expectedPosition)
    {
        Dial dial = new(startingPosition, count);

        Assert.Equal(expectedPosition, dial.Position.Value);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void ConstructorWithNonPositiveCountThrowsArgumentException(int invalidCount)
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new Dial(0, invalidCount));

        Assert.Equal("Count must be positive (Parameter 'count')", exception.Message);
        Assert.Equal("count", exception.ParamName);
    }

    [Fact]
    public void RotateRightUpdatesPosition()
    {
        Dial dial = new(0, 100);
        Rotation rotation = new('R', 10);

        _ = dial.Rotate(rotation);

        Assert.Equal(10, dial.Position.Value);
    }

    [Fact]
    public void RotateLeftUpdatesPosition()
    {
        Dial dial = new(50, 100);
        Rotation rotation = new('L', 10);

        _ = dial.Rotate(rotation);

        Assert.Equal(40, dial.Position.Value);
    }

    [Fact]
    public void RotateRightWrapsAroundAtMaximum()
    {
        Dial dial = new(95, 100);
        Rotation rotation = new('R', 10);

        _ = dial.Rotate(rotation);

        Assert.Equal(5, dial.Position.Value);
    }

    [Fact]
    public void RotateLeftWrapsAroundAtZero()
    {
        Dial dial = new(5, 100);
        Rotation rotation = new('L', 10);

        _ = dial.Rotate(rotation);

        Assert.Equal(95, dial.Position.Value);
    }

    [Theory]
    [InlineData(0, 'R', 100, 0)]
    [InlineData(50, 'R', 100, 50)]
    [InlineData(25, 'R', 100, 25)]
    public void RotateRightByMultipleOfCountReturnsToSamePosition(int startingPosition, char direction, int distance, int expectedPosition)
    {
        Dial dial = new(startingPosition, 100);
        Rotation rotation = new(direction, distance);

        _ = dial.Rotate(rotation);

        Assert.Equal(expectedPosition, dial.Position.Value);
    }

    [Theory]
    [InlineData(0, 'L', 100, 0)]
    [InlineData(50, 'L', 100, 50)]
    [InlineData(25, 'L', 100, 25)]
    public void RotateLeftByMultipleOfCountReturnsToSamePosition(int startingPosition, char direction, int distance, int expectedPosition)
    {
        Dial dial = new(startingPosition, 100);
        Rotation rotation = new(direction, distance);

        _ = dial.Rotate(rotation);

        Assert.Equal(expectedPosition, dial.Position.Value);
    }

    [Fact]
    public void MultipleRotationsUpdatePositionCorrectly()
    {
        Dial dial = new(0, 100);

        _ = dial.Rotate(new Rotation('R', 10));
        _ = dial.Rotate(new Rotation('R', 15));
        _ = dial.Rotate(new Rotation('L', 5));

        Assert.Equal(20, dial.Position.Value);
    }

    [Fact]
    public void AddOperatorCreatesNewDialWithRotatedPosition()
    {
        Dial original = new(50, 100);
        Rotation rotation = new('R', 10);

        Dial result = original + rotation;

        Assert.Equal(50, original.Position.Value);
        Assert.Equal(60, result.Position.Value);
        Assert.Equal(100, result.Count);
    }

    [Fact]
    public void AddOperatorWithLeftRotationCreatesNewDialWithCorrectPosition()
    {
        Dial original = new(50, 100);
        Rotation rotation = new('L', 20);

        Dial result = original + rotation;

        Assert.Equal(50, original.Position.Value);
        Assert.Equal(30, result.Position.Value);
    }

    [Fact]
    public void AddOperatorWithWrapAroundCreatesNewDialWithNormalizedPosition()
    {
        Dial original = new(95, 100);
        Rotation rotation = new('R', 10);

        Dial result = original + rotation;

        Assert.Equal(95, original.Position.Value);
        Assert.Equal(5, result.Position.Value);
    }

    [Theory]
    [InlineData(50)]
    [InlineData(100)]
    [InlineData(200)]
    [InlineData(500)]
    public void CountPropertyReturnsCorrectValue(int count)
    {
        Dial dial = new(0, count);

        Assert.Equal(count, dial.Count);
    }

    [Fact]
    public void PositionPropertyReturnsCurrentPosition()
    {
        Dial dial = new(42, 100);

        Assert.Equal(42, dial.Position.Value);
    }

    [Fact]
    public void RotateUpdatesPositionProperty()
    {
        Dial dial = new(0, 100);

        _ = dial.Rotate(new Rotation('R', 25));

        Assert.Equal(25, dial.Position.Value);
    }

    [Theory]
    [InlineData(0, 100, 0)]
    [InlineData(25, 100, 25)]
    [InlineData(99, 100, 99)]
    public void DialPositionValuePropertyReturnsNormalizedPosition(int startingPosition, int count, int expectedValue)
    {
        Dial dial = new(startingPosition, count);

        Assert.Equal(expectedValue, dial.Position.Value);
    }

    [Fact]
    public void RotateRightFromPositiveDoesNotPassZero()
    {
        Dial dial = new(10, 100);
        Rotation rotation = new('R', 5);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(0, passesZero);
        Assert.Equal(15, dial.Position.Value);
    }

    [Fact]
    public void RotateLeftWrappingAroundMayPassZero()
    {
        Dial dial = new(10, 100);
        Rotation rotation = new('L', 20);

        _ = dial.Rotate(rotation);

        Assert.Equal(90, dial.Position.Value);
    }

    [Fact]
    public void RotateReturnsZeroWhenNotPassingZero()
    {
        Dial dial = new(50, 100);
        Rotation rotation = new('R', 10);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(0, passesZero);
    }
}
