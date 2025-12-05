namespace Day1b.Tests;

/// <summary>
/// Unit tests specifically for the "passes zero" counting logic in the <see cref="Dial"/> class.
/// These tests verify that rotations correctly count how many times they pass through or land on position 0.
/// </summary>
public class DialPassesZeroTests
{
    [Fact]
    public void RotateRightWithoutPassingZeroReturnsZero()
    {
        Dial dial = new(10, 100);
        Rotation rotation = new('R', 20);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(0, passesZero);
        Assert.Equal(30, dial.Position.Value);
    }

    [Fact]
    public void RotateLeftWithoutPassingZeroReturnsZero()
    {
        Dial dial = new(50, 100);
        Rotation rotation = new('L', 20);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(0, passesZero);
        Assert.Equal(30, dial.Position.Value);
    }

    [Fact]
    public void RotateRightExactlyToZeroCountsAsOne()
    {
        Dial dial = new(50, 100);
        Rotation rotation = new('R', 50);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(1, passesZero);
        Assert.Equal(0, dial.Position.Value);
    }

    [Fact]
    public void RotateLeftExactlyToZeroCountsAsOne()
    {
        Dial dial = new(50, 100);
        Rotation rotation = new('L', 50);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(1, passesZero);
        Assert.Equal(0, dial.Position.Value);
    }

    [Fact]
    public void RotateRightPassingThroughZeroCountsAsOne()
    {
        Dial dial = new(90, 100);
        Rotation rotation = new('R', 20);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(1, passesZero);
        Assert.Equal(10, dial.Position.Value);
    }

    [Fact]
    public void RotateLeftPassingThroughZeroCountsAsOne()
    {
        Dial dial = new(10, 100);
        Rotation rotation = new('L', 20);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(1, passesZero);
        Assert.Equal(90, dial.Position.Value);
    }

    [Fact]
    public void RotateRightFullRotationCountsAsOne()
    {
        Dial dial = new(0, 100);
        Rotation rotation = new('R', 100);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(1, passesZero);
        Assert.Equal(0, dial.Position.Value);
    }

    [Fact]
    public void RotateLeftFullRotationCountsAsOne()
    {
        Dial dial = new(0, 100);
        Rotation rotation = new('L', 100);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(1, passesZero);
        Assert.Equal(0, dial.Position.Value);
    }

    [Fact]
    public void RotateRightFullRotationFromNonZeroCountsAsOne()
    {
        Dial dial = new(25, 100);
        Rotation rotation = new('R', 100);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(1, passesZero);
        Assert.Equal(25, dial.Position.Value);
    }

    [Fact]
    public void RotateLeftFullRotationFromNonZeroCountsAsOne()
    {
        Dial dial = new(25, 100);
        Rotation rotation = new('L', 100);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(1, passesZero);
        Assert.Equal(25, dial.Position.Value);
    }

    [Fact]
    public void RotateRightTwoFullRotationsCountsAsTwo()
    {
        Dial dial = new(0, 100);
        Rotation rotation = new('R', 200);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(2, passesZero);
        Assert.Equal(0, dial.Position.Value);
    }

    [Fact]
    public void RotateLeftTwoFullRotationsCountsAsTwo()
    {
        Dial dial = new(0, 100);
        Rotation rotation = new('L', 200);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(2, passesZero);
        Assert.Equal(0, dial.Position.Value);
    }

    [Fact]
    public void RotateRightOneAndHalfRotationsCountsAsOne()
    {
        Dial dial = new(25, 100);
        Rotation rotation = new('R', 150);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(1, passesZero);
        Assert.Equal(75, dial.Position.Value);
    }

    [Fact]
    public void RotateLeftOneAndHalfRotationsCountsAsOne()
    {
        Dial dial = new(75, 100);
        Rotation rotation = new('L', 150);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(1, passesZero);
        Assert.Equal(25, dial.Position.Value);
    }

    [Fact]
    public void RotateRightAlmostFullRotationCountsAsOne()
    {
        Dial dial = new(10, 100);
        Rotation rotation = new('R', 95);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(1, passesZero);
        Assert.Equal(5, dial.Position.Value);
    }

    [Fact]
    public void RotateLeftAlmostFullRotationCountsAsOne()
    {
        Dial dial = new(90, 100);
        Rotation rotation = new('L', 95);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(1, passesZero);
        Assert.Equal(95, dial.Position.Value);
    }

    [Fact]
    public void StartingAtZeroAndRotatingRightDoesNotCountAsPassingZero()
    {
        Dial dial = new(0, 100);
        Rotation rotation = new('R', 10);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(0, passesZero);
        Assert.Equal(10, dial.Position.Value);
    }

    [Fact]
    public void StartingAtZeroAndRotatingLeftDoesNotCountAsPassingZero()
    {
        Dial dial = new(0, 100);
        Rotation rotation = new('L', 10);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(0, passesZero);
        Assert.Equal(90, dial.Position.Value);
    }

    [Theory]
    [InlineData(50, 'R', 50, 1)]
    [InlineData(50, 'R', 150, 2)]
    [InlineData(50, 'R', 250, 3)]
    [InlineData(25, 'R', 75, 1)]
    [InlineData(25, 'R', 175, 2)]
    [InlineData(1, 'R', 99, 1)]
    [InlineData(1, 'R', 199, 2)]
    public void RotateRightVariousDistancesCountsCorrectly(int start, char direction, int distance, int expectedPasses)
    {
        Dial dial = new(start, 100);
        Rotation rotation = new(direction, distance);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(expectedPasses, passesZero);
    }

    [Theory]
    [InlineData(50, 'L', 50, 1)]
    [InlineData(50, 'L', 150, 2)]
    [InlineData(50, 'L', 250, 3)]
    [InlineData(75, 'L', 75, 1)]
    [InlineData(75, 'L', 175, 2)]
    [InlineData(99, 'L', 99, 1)]
    [InlineData(99, 'L', 199, 2)]
    public void RotateLeftVariousDistancesCountsCorrectly(int start, char direction, int distance, int expectedPasses)
    {
        Dial dial = new(start, 100);
        Rotation rotation = new(direction, distance);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(expectedPasses, passesZero);
    }

    [Fact]
    public void MultipleRotationsAccumulatePassesCorrectly()
    {
        Dial dial = new(50, 100);
        int totalPasses = 0;

        totalPasses += dial.Rotate(new Rotation('R', 60));
        totalPasses += dial.Rotate(new Rotation('L', 120));
        totalPasses += dial.Rotate(new Rotation('R', 100));

        Assert.Equal(4, totalPasses);
    }

    [Fact]
    public void RotateWithSmallDialCountsCorrectly()
    {
        Dial dial = new(5, 10);
        Rotation rotation = new('R', 8);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(1, passesZero);
        Assert.Equal(3, dial.Position.Value);
    }

    [Fact]
    public void RotateWithSmallDialMultipleRotationsCountsCorrectly()
    {
        Dial dial = new(5, 10);
        Rotation rotation = new('R', 25);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(3, passesZero);
        Assert.Equal(0, dial.Position.Value);
    }

    [Fact]
    public void ProgramScenarioStartingAt50RotateRight10()
    {
        Dial dial = new(50);
        Rotation rotation = new('R', 10);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(0, passesZero);
        Assert.Equal(60, dial.Position.Value);
    }

    [Fact]
    public void ProgramScenarioStartingAt50RotateRight60()
    {
        Dial dial = new(50);
        Rotation rotation = new('R', 60);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(1, passesZero);
        Assert.Equal(10, dial.Position.Value);
    }

    [Fact]
    public void ProgramScenarioStartingAt50RotateLeft60()
    {
        Dial dial = new(50);
        Rotation rotation = new('L', 60);

        int passesZero = dial.Rotate(rotation);

        Assert.Equal(1, passesZero);
        Assert.Equal(90, dial.Position.Value);
    }

    [Fact]
    public void ProgramScenarioMultipleRotations()
    {
        Dial dial = new(50);
        int totalPasses = 0;

        totalPasses += dial.Rotate(new Rotation('R', 10));
        totalPasses += dial.Rotate(new Rotation('R', 60));
        totalPasses += dial.Rotate(new Rotation('L', 80));
        totalPasses += dial.Rotate(new Rotation('R', 100));

        Assert.Equal(3, totalPasses);
    }
}
