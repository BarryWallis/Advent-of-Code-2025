namespace Day1a.Tests;

/// <summary>
/// Unit tests for the <see cref="Dial"/> and <see cref="Dial.DialPosition"/> classes.
/// </summary>
public class DialPositionTests
{
    [Fact]
    public void DialConstructorWithDefaultCountSetsCountTo100()
    {
        Dial dial = new();

        Assert.Equal(100, dial.Count);
    }

    [Theory]
    [InlineData(75)]
    [InlineData(100)]
    [InlineData(200)]
    public void DialConstructorWithCustomCountSetsCountCorrectly(int count)
    {
        Dial dial = new(count: count);

        Assert.Equal(count, dial.Count);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(50, 50)]
    [InlineData(99, 99)]
    public void DialConstructorWithValidPositionSetsCorrectly(int position, int expected)
    {
        Dial dial = new(startingPosition: position);

        Assert.Equal(expected, dial.Position.Value);
    }

    [Theory]
    [InlineData(100, 0)]
    [InlineData(101, 1)]
    [InlineData(199, 99)]
    [InlineData(200, 0)]
    public void DialConstructorWithPositionOver99WrapsUsingModulo(int position, int expected)
    {
        Dial dial = new(startingPosition: position);

        Assert.Equal(expected, dial.Position.Value);
    }

    [Theory]
    [InlineData(-1, 99)]
    [InlineData(-50, 50)]
    [InlineData(-100, 0)]
    [InlineData(-101, 99)]
    public void DialConstructorWithNegativePositionWrapsCorrectly(int position, int expected)
    {
        Dial dial = new(startingPosition: position);

        Assert.Equal(expected, dial.Position.Value);
    }

    [Theory]
    [InlineData(-200, 0)]
    [InlineData(-250, 50)]
    [InlineData(-300, 0)]
    [InlineData(-10000, 0)]
    public void DialConstructorWithLargeNegativePositionWrapsCorrectly(int position, int expected)
    {
        Dial dial = new(startingPosition: position);

        Assert.Equal(expected, dial.Position.Value);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    [InlineData(-100)]
    public void DialConstructorWithNonPositiveCountThrowsArgumentException(int count)
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new Dial(count: count));

        Assert.Equal("Count must be positive (Parameter 'count')", exception.Message);
        Assert.Equal("count", exception.ParamName);
    }

    [Theory]
    [InlineData(0, 50)]
    [InlineData(-1, 0)]
    [InlineData(-100, 25)]
    public void DialConstructorWithNonPositiveCountAndPositionThrowsArgumentException(int count, int position)
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new Dial(position, count));

        Assert.Equal("Count must be positive (Parameter 'count')", exception.Message);
        Assert.Equal("count", exception.ParamName);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(100)]
    [InlineData(1000)]
    public void DialConstructorWithPositiveCountSetsCorrectly(int count)
    {
        Dial dial = new(count: count);

        Assert.Equal(count, dial.Count);
    }

    [Theory]
    [InlineData(100, 0, 0)]
    [InlineData(75, 50, 50)]
    [InlineData(200, 150, 150)]
    [InlineData(150, -25, 125)]
    public void DialConstructorNormalizesPositionAndSetsCount(int count, int position, int expectedPosition)
    {
        Dial dial = new(position, count);

        Assert.Equal(expectedPosition, dial.Position.Value);
        Assert.Equal(count, dial.Count);
    }

    [Theory]
    [InlineData(50, 50, 0)]
    [InlineData(50, 75, 25)]
    [InlineData(50, -10, 40)]
    [InlineData(50, 150, 0)]
    public void PositionWrapsBasedOnDialCount(int count, int position, int expected)
    {
        Dial dial = new(position, count);

        Assert.Equal(expected, dial.Position.Value);
    }

    [Theory]
    [InlineData(10, 10, 0)]
    [InlineData(10, 25, 5)]
    [InlineData(10, -5, 5)]
    [InlineData(10, -15, 5)]
    public void PositionWrapsCorrectlyForCount10(int count, int position, int expected)
    {
        Dial dial = new(position, count);

        Assert.Equal(expected, dial.Position.Value);
    }

    [Theory]
    [InlineData(1, 0, 0)]
    [InlineData(1, 5, 0)]
    [InlineData(1, -10, 0)]
    public void PositionWithCount1AlwaysReturnsZero(int count, int position, int expected)
    {
        Dial dial = new(position, count);

        Assert.Equal(expected, dial.Position.Value);
    }

    [Fact]
    public void DialCountIsImmutableAfterConstruction()
    {
        Dial dial = new(50, 100);
        int originalCount = dial.Count;

        Assert.Equal(originalCount, dial.Count);
    }

    [Theory]
    [InlineData(100, 0)]
    [InlineData(50, 25)]
    [InlineData(200, 150)]
    public void DialPositionValueReflectsNormalizedPosition(int count, int position)
    {
        Dial dial = new(position, count);
        int expected = position % count;

        Assert.Equal(expected, dial.Position.Value);
    }

    [Fact]
    public void TwoDialPositionsWithSameValueFromSameDialShareReference()
    {
        Dial dial = new(25, 100);
        Dial.DialPosition position1 = dial.Position;
        Dial.DialPosition position2 = dial.Position;

        Assert.Same(position1, position2);
        Assert.Equal(position1, position2);
    }

    [Fact]
    public void TwoDialPositionsFromDifferentDialsWithSameValuesAreNotEqual()
    {
        Dial dial1 = new(25, 100);
        Dial dial2 = new(25, 100);

        Assert.NotEqual(dial1.Position, dial2.Position);
        Assert.False(dial1.Position == dial2.Position);
    }

    [Fact]
    public void DialPositionIsEqualToItself()
    {
        Dial dial = new(25, 100);
        Dial.DialPosition position = dial.Position;

        Assert.Equal(position, position);
        Assert.True(position.Equals(position));
#pragma warning disable CS1718 // Comparison made to same variable
        Assert.True(position == position);
#pragma warning restore CS1718 // Comparison made to same variable
    }

    [Fact]
    public void DialPositionSupportsRecordEquality()
    {
        Dial dial = new(25, 100);
        Dial.DialPosition position = dial.Position;

        Assert.Equal(position, position);
#pragma warning disable CS1718 // Comparison made to same variable
        Assert.True(position == position);
#pragma warning restore CS1718 // Comparison made to same variable
    }

    [Fact]
    public void DialPositionWithDifferentDialReferencesAreNotEqual()
    {
        Dial dial1 = new(50, 100);
        Dial dial2 = new(50, 100);

        Assert.NotSame(dial1, dial2);
        Assert.NotEqual(dial1.Position, dial2.Position);
        Assert.False(dial1.Position == dial2.Position);
    }

    [Fact]
    public void DialPositionToStringReturnsRepresentation()
    {
        Dial dial = new(25, 100);
        string result = dial.Position.ToString();

        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Theory]
    [InlineData(0, 100)]
    [InlineData(50, 100)]
    [InlineData(25, 50)]
    public void DialPositionHashCodeIsConsistent(int position, int count)
    {
        Dial dial = new(position, count);
        int hashCode1 = dial.Position.GetHashCode();
        int hashCode2 = dial.Position.GetHashCode();

        Assert.Equal(hashCode1, hashCode2);
    }

    [Theory]
    [InlineData(100, 0, 100, 0)]
    [InlineData(50, 25, 50, 25)]
    public void DialPositionsFromDifferentDialsWithSameValuesHaveDifferentHashCodes(int count1, int position1, int count2, int position2)
    {
        Dial dial1 = new(position1, count1);
        Dial dial2 = new(position2, count2);

        Assert.NotEqual(dial1.Position.GetHashCode(), dial2.Position.GetHashCode());
    }

    [Theory]
    [InlineData(0, 10, 10)]
    [InlineData(50, 10, 60)]
    [InlineData(90, 10, 0)]
    [InlineData(95, 10, 5)]
    public void DialPlusRotationRightCreatesNewDialWithCorrectPosition(int startPosition, int distance, int expectedPosition)
    {
        Dial dial = new(startPosition, 100);
        Rotation rotation = new('R', distance);

        Dial result = dial + rotation;

        Assert.Equal(expectedPosition, result.Position.Value);
        Assert.Equal(100, result.Count);
        Assert.NotSame(dial, result);
    }

    [Theory]
    [InlineData(10, 10, 0)]
    [InlineData(50, 10, 40)]
    [InlineData(0, 10, 90)]
    [InlineData(5, 10, 95)]
    public void DialPlusRotationLeftCreatesNewDialWithCorrectPosition(int startPosition, int distance, int expectedPosition)
    {
        Dial dial = new(startPosition, 100);
        Rotation rotation = new('L', distance);

        Dial result = dial + rotation;

        Assert.Equal(expectedPosition, result.Position.Value);
        Assert.Equal(100, result.Count);
        Assert.NotSame(dial, result);
    }

    [Theory]
    [InlineData(0, 10, 10)]
    [InlineData(50, 10, 60)]
    [InlineData(90, 10, 0)]
    [InlineData(95, 10, 5)]
    public void DialRotateRightUpdatesPositionInPlace(int startPosition, int distance, int expectedPosition)
    {
        Dial dial = new(startPosition, 100);
        Rotation rotation = new('R', distance);

        dial.Rotate(rotation);

        Assert.Equal(expectedPosition, dial.Position.Value);
        Assert.Equal(100, dial.Count);
    }

    [Theory]
    [InlineData(10, 10, 0)]
    [InlineData(50, 10, 40)]
    [InlineData(0, 10, 90)]
    [InlineData(5, 10, 95)]
    public void DialRotateLeftUpdatesPositionInPlace(int startPosition, int distance, int expectedPosition)
    {
        Dial dial = new(startPosition, 100);
        Rotation rotation = new('L', distance);

        dial.Rotate(rotation);

        Assert.Equal(expectedPosition, dial.Position.Value);
        Assert.Equal(100, dial.Count);
    }

    [Fact]
    public void DialRotateMultipleTimesAccumulatesCorrectly()
    {
        Dial dial = new(0, 100);

        dial.Rotate(new Rotation('R', 10));
        dial.Rotate(new Rotation('R', 20));
        dial.Rotate(new Rotation('L', 5));

        Assert.Equal(25, dial.Position.Value);
    }

    [Theory]
    [InlineData(50, 10)]
    [InlineData(75, 5)]
    public void DialPlusEqualsRotationUpdatesDialReference(int startPosition, int distance)
    {
        Dial dial = new(startPosition, 100);
        Rotation rotation = new('R', distance);
        int expectedPosition = (startPosition + distance) % 100;

        dial += rotation;

        Assert.Equal(expectedPosition, dial.Position.Value);
    }

    [Fact]
    public void DialPlusRotationPreservesOriginalDial()
    {
        Dial original = new(50, 100);
        Rotation rotation = new('R', 10);

        Dial result = original + rotation;

        Assert.Equal(50, original.Position.Value);
        Assert.Equal(60, result.Position.Value);
    }

    [Theory]
    [InlineData(50, 200, 50)]
    [InlineData(10, 95, 5)]
    public void DialPlusRotationWithLargeDistanceWrapsCorrectly(int startPosition, int distance, int expectedPosition)
    {
        Dial dial = new(startPosition, 100);
        Rotation rotation = new('R', distance);

        Dial result = dial + rotation;

        Assert.Equal(expectedPosition, result.Position.Value);
    }

    [Theory]
    [InlineData(50, 200, 50)]
    [InlineData(10, 95, 5)]
    public void DialRotateWithLargeDistanceWrapsCorrectly(int startPosition, int distance, int expectedPosition)
    {
        Dial dial = new(startPosition, 100);
        Rotation rotation = new('R', distance);

        dial.Rotate(rotation);

        Assert.Equal(expectedPosition, dial.Position.Value);
    }
}
