namespace Day1a.Tests;

/// <summary>
/// Unit tests for the <see cref="Rotation"/> class.
/// </summary>
public class RotationTests
{
    [Theory]
    [InlineData('L', 1)]
    [InlineData('L', 10)]
    [InlineData('L', 100)]
    [InlineData('R', 1)]
    [InlineData('R', 10)]
    [InlineData('R', 100)]
    public void ConstructorWithValidDirectionAndDistanceCreatesRotation(char direction, int distance)
    {
        Rotation rotation = new(direction, distance);

        Assert.Equal(direction, rotation.Direction);
        Assert.Equal(distance, rotation.Distance);
    }

    [Theory]
    [InlineData('L', 1)]
    [InlineData('R', 999)]
    public void DirectionPropertyReturnsCorrectValue(char direction, int distance)
    {
        Rotation rotation = new(direction, distance);

        Assert.Equal(direction, rotation.Direction);
    }

    [Theory]
    [InlineData('L', 1)]
    [InlineData('R', 999)]
    public void DistancePropertyReturnsCorrectValue(char direction, int distance)
    {
        Rotation rotation = new(direction, distance);

        Assert.Equal(distance, rotation.Distance);
    }

    [Fact]
    public void ConstructorWithLeftDirectionAndDistance1CreatesRotation()
    {
        Rotation rotation = new('L', 1);

        Assert.Equal('L', rotation.Direction);
        Assert.Equal(1, rotation.Distance);
    }

    [Fact]
    public void ConstructorWithRightDirectionAndDistance1CreatesRotation()
    {
        Rotation rotation = new('R', 1);

        Assert.Equal('R', rotation.Direction);
        Assert.Equal(1, rotation.Distance);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(50)]
    [InlineData(100)]
    [InlineData(1000)]
    [InlineData(int.MaxValue)]
    public void ConstructorWithLeftDirectionAndVariousDistancesCreatesRotation(int distance)
    {
        Rotation rotation = new('L', distance);

        Assert.Equal('L', rotation.Direction);
        Assert.Equal(distance, rotation.Distance);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(50)]
    [InlineData(100)]
    [InlineData(1000)]
    [InlineData(int.MaxValue)]
    public void ConstructorWithRightDirectionAndVariousDistancesCreatesRotation(int distance)
    {
        Rotation rotation = new('R', distance);

        Assert.Equal('R', rotation.Direction);
        Assert.Equal(distance, rotation.Distance);
    }

    [Fact]
    public void TwoRotationsWithSameValuesAreEqual()
    {
        Rotation rotation1 = new('L', 5);
        Rotation rotation2 = new('L', 5);

        Assert.Equal(rotation1, rotation2);
        Assert.True(rotation1.Equals(rotation2));
        Assert.True(rotation1 == rotation2);
    }

    [Fact]
    public void TwoRotationsWithDifferentValuesAreNotEqual()
    {
        Rotation rotation1 = new('L', 5);
        Rotation rotation2 = new('R', 5);
        Rotation rotation3 = new('L', 10);

        Assert.NotEqual(rotation1, rotation2);
        Assert.NotEqual(rotation1, rotation3);
        Assert.False(rotation1 == rotation2);
        Assert.False(rotation1 == rotation3);
    }

    [Fact]
    public void RotationIsEqualToItself()
    {
        Rotation rotation = new('L', 5);

        Assert.Equal(rotation, rotation);
        Assert.True(rotation.Equals(rotation));
#pragma warning disable CS1718 // Comparison made to same variable
        Assert.True(rotation == rotation);
#pragma warning restore CS1718 // Comparison made to same variable
    }

    [Theory]
    [InlineData('U')]
    [InlineData('D')]
    [InlineData('A')]
    [InlineData('Z')]
    [InlineData('l')]
    [InlineData('r')]
    [InlineData('0')]
    [InlineData(' ')]
    public void ObjectInitializerWithInvalidDirectionThrowsArgumentException(char invalidDirection)
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new Rotation('L', 1) { Direction = invalidDirection });

        Assert.Equal("Direction must be 'L' or 'R' (Parameter 'Direction')", exception.Message);
        Assert.Equal("Direction", exception.ParamName);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    [InlineData(-100)]
    public void ObjectInitializerWithNonPositiveDistanceThrowsArgumentException(int invalidDistance)
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new Rotation('L', 1) { Distance = invalidDistance });

        Assert.Equal("Distance must be positive (Parameter 'Distance')", exception.Message);
        Assert.Equal("Distance", exception.ParamName);
    }

    [Theory]
    [InlineData('U')]
    [InlineData('D')]
    [InlineData('A')]
    [InlineData('Z')]
    [InlineData('l')]
    [InlineData('r')]
    [InlineData('0')]
    [InlineData(' ')]
    public void WithExpressionWithInvalidDirectionThrowsArgumentException(char invalidDirection)
    {
        Rotation rotation = new('L', 1);

        ArgumentException exception = Assert.Throws<ArgumentException>(() => rotation with { Direction = invalidDirection });

        Assert.Equal("Direction must be 'L' or 'R' (Parameter 'Direction')", exception.Message);
        Assert.Equal("Direction", exception.ParamName);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    [InlineData(-100)]
    public void WithExpressionWithNonPositiveDistanceThrowsArgumentException(int invalidDistance)
    {
        Rotation rotation = new('L', 1);

        ArgumentException exception = Assert.Throws<ArgumentException>(() => rotation with { Distance = invalidDistance });

        Assert.Equal("Distance must be positive (Parameter 'Distance')", exception.Message);
        Assert.Equal("Distance", exception.ParamName);
    }

    [Theory]
    [InlineData('L', 'R')]
    [InlineData('R', 'L')]
    public void WithExpressionCanChangeDirection(char originalDirection, char newDirection)
    {
        Rotation original = new(originalDirection, 10);
        Rotation modified = original with { Direction = newDirection };

        Assert.Equal(newDirection, modified.Direction);
        Assert.Equal(10, modified.Distance);
        Assert.NotEqual(original, modified);
    }

    [Theory]
    [InlineData(10, 20)]
    [InlineData(1, 100)]
    public void WithExpressionCanChangeDistance(int originalDistance, int newDistance)
    {
        Rotation original = new('L', originalDistance);
        Rotation modified = original with { Distance = newDistance };

        Assert.Equal('L', modified.Direction);
        Assert.Equal(newDistance, modified.Distance);
        Assert.NotEqual(original, modified);
    }

    [Fact]
    public void WithExpressionCanChangeBothProperties()
    {
        Rotation original = new('L', 10);
        Rotation modified = original with { Direction = 'R', Distance = 20 };

        Assert.Equal('R', modified.Direction);
        Assert.Equal(20, modified.Distance);
        Assert.NotEqual(original, modified);
    }

    [Fact]
    public void WithExpressionWithNoChangesCreatesEqualRotation()
    {
        Rotation original = new('L', 10);
        Rotation copy = original with { };

        Assert.Equal(original, copy);
        Assert.NotSame(original, copy);
    }

    [Theory]
    [InlineData('L', 5)]
    [InlineData('R', 100)]
    public void RotationToStringReturnsRepresentation(char direction, int distance)
    {
        Rotation rotation = new(direction, distance);
        string result = rotation.ToString();

        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }
}
