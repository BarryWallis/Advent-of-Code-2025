namespace Day4a.Tests;

/// <summary>
/// Unit tests for the <see cref="Floor"/> class.
/// Tests both the constructor's parsing logic and the <see cref="Floor.CountAccessibleRolls"/> method.
/// </summary>
public class FloorTests
{
    [Fact]
    public void ConstructorWithEmptyInputCreatesEmptyFloor()
    {
        StringReader input = new(string.Empty);

        Floor floor = new(input);

        Assert.Equal(0, floor.CountAccessibleRolls());
    }

    [Fact]
    public void ConstructorWithSingleDotCreatesEmptyFloor()
    {
        StringReader input = new(".");

        Floor floor = new(input);

        Assert.Equal(0, floor.CountAccessibleRolls());
    }

    [Fact]
    public void ConstructorWithMultipleDotsCreatesEmptyFloor()
    {
        StringReader input = new("...\n...\n...");

        Floor floor = new(input);

        Assert.Equal(0, floor.CountAccessibleRolls());
    }

    [Fact]
    public void ConstructorWithSingleAtSymbolCreatesFloorWithOneRoll()
    {
        StringReader input = new("@");

        Floor floor = new(input);

        Assert.Equal(1, floor.CountAccessibleRolls());
    }

    [Fact]
    public void ConstructorWithMultipleAtSymbolsCreatesFloorWithMultipleRolls()
    {
        StringReader input = new("@@@\n@@@\n@@@");

        Floor floor = new(input);

        Assert.Equal(4, floor.CountAccessibleRolls());
    }

    [Fact]
    public void ConstructorWithMixedDotsAndAtSymbolsCreatesFloorWithCorrectRollCount()
    {
        StringReader input = new(".@.\n@.@\n.@.");

        Floor floor = new(input);

        Assert.Equal(4, floor.CountAccessibleRolls());
    }

    [Fact]
    public void ConstructorWithSingleLineMultipleRollsCreatesCorrectFloor()
    {
        StringReader input = new("@.@.@");

        Floor floor = new(input);

        Assert.Equal(3, floor.CountAccessibleRolls());
    }

    [Fact]
    public void ConstructorWithUnevenRowLengthsCreatesCorrectFloor()
    {
        StringReader input = new("@@@\n@\n@@");

        Floor floor = new(input);

        Assert.Equal(5, floor.CountAccessibleRolls());
    }

    [Fact]
    public void ConstructorWithInvalidCharacterThrowsInvalidDataException()
    {
        StringReader input = new(".@X");

        InvalidDataException exception = Assert.Throws<InvalidDataException>(() => new Floor(input));

        Assert.Contains("Unexpected character 'X'", exception.Message);
        Assert.Contains("row 0, column 2", exception.Message);
    }

    [Fact]
    public void ConstructorWithInvalidCharacterInMiddleThrowsInvalidDataException()
    {
        StringReader input = new("...\n.#.\n...");

        InvalidDataException exception = Assert.Throws<InvalidDataException>(() => new Floor(input));

        Assert.Contains("Unexpected character '#'", exception.Message);
        Assert.Contains("row 1, column 1", exception.Message);
    }

    [Fact]
    public void ConstructorWithMultipleInvalidCharactersThrowsOnFirstInvalidCharacter()
    {
        StringReader input = new("ABC");

        InvalidDataException exception = Assert.Throws<InvalidDataException>(() => new Floor(input));

        Assert.Contains("Unexpected character 'A'", exception.Message);
        Assert.Contains("row 0, column 0", exception.Message);
    }

    [Fact]
    public void ConstructorWithEmptyLinesCreatesCorrectFloor()
    {
        StringReader input = new("@@@\n\n@@@");

        Floor floor = new(input);

        Assert.Equal(6, floor.CountAccessibleRolls());
    }

    [Fact]
    public void ConstructorWithLargeGridCreatesCorrectFloor()
    {
        string largeGrid = string.Join("\n", Enumerable.Repeat("@@@@@@@@@@@@@@@@@@@@@", 20));
        StringReader input = new(largeGrid);

        Floor floor = new(input);

        Assert.Equal(4, floor.CountAccessibleRolls());
    }

    [Fact]
    public void ConstructorWithOnlyDotsOnMultipleLinesCreatesEmptyFloor()
    {
        StringReader input = new(".....\n.....\n.....");

        Floor floor = new(input);

        Assert.Equal(0, floor.CountAccessibleRolls());
    }

    [Fact]
    public void ConstructorWithSingleAtInLargeGridCreatesFloorWithOneRoll()
    {
        StringReader input = new(".....\n..@..\n.....");

        Floor floor = new(input);

        Assert.Equal(1, floor.CountAccessibleRolls());
    }

    [Fact]
    public void ConstructorWithPatternCreatesCorrectFloor()
    {
        StringReader input = new("@.@.@\n.@.@.\n@.@.@");

        Floor floor = new(input);

        Assert.Equal(6, floor.CountAccessibleRolls());
    }

    [Fact]
    public void CountAccessibleRollsWithTwoAdjacentRollsReturnsBothAccessible()
    {
        StringReader input = new("@@");

        Floor floor = new(input);

        Assert.Equal(2, floor.CountAccessibleRolls());
    }

    [Fact]
    public void CountAccessibleRollsWithVerticalLineOfThreeRollsReturnsAllAccessible()
    {
        StringReader input = new("@\n@\n@");

        Floor floor = new(input);

        Assert.Equal(3, floor.CountAccessibleRolls());
    }

    [Fact]
    public void CountAccessibleRollsWithHorizontalLineOfFourRollsReturnsAllAccessible()
    {
        StringReader input = new("@@@@");

        Floor floor = new(input);

        Assert.Equal(4, floor.CountAccessibleRolls());
    }

    [Fact]
    public void CountAccessibleRollsWithHorizontalLineOfFiveRollsReturnsAllAccessible()
    {
        StringReader input = new("@@@@@");

        Floor floor = new(input);

        Assert.Equal(5, floor.CountAccessibleRolls());
    }

    [Fact]
    public void CountAccessibleRollsWithTwoByTwoGridReturnsAllAccessible()
    {
        StringReader input = new("@@\n@@");

        Floor floor = new(input);

        Assert.Equal(4, floor.CountAccessibleRolls());
    }

    [Fact]
    public void CountAccessibleRollsWithLShapeReturnsMostAccessible()
    {
        StringReader input = new("@..\n@..\n@@@");

        Floor floor = new(input);

        Assert.Equal(5, floor.CountAccessibleRolls());
    }

    [Fact]
    public void CountAccessibleRollsWithTShapeReturnsCorrectCount()
    {
        StringReader input = new("@@@\n.@.\n.@.");

        Floor floor = new(input);

        Assert.Equal(4, floor.CountAccessibleRolls());
    }

    [Fact]
    public void CountAccessibleRollsWithCrossPatternReturnsOnlyOuterAccessible()
    {
        StringReader input = new(".@.\n@@@\n.@.");

        Floor floor = new(input);

        Assert.Equal(4, floor.CountAccessibleRolls());
    }

    [Fact]
    public void CountAccessibleRollsWithDiagonalLineReturnsAllAccessible()
    {
        StringReader input = new("@..\n.@.\n..@");

        Floor floor = new(input);

        Assert.Equal(3, floor.CountAccessibleRolls());
    }

    [Fact]
    public void CountAccessibleRollsWithIsolatedRollsReturnsAllAccessible()
    {
        StringReader input = new("@....\n.....\n.....\n.....\n....@");

        Floor floor = new(input);

        Assert.Equal(2, floor.CountAccessibleRolls());
    }

    [Fact]
    public void CountAccessibleRollsWithFourByFourGridReturnsOnlyCornersAccessible()
    {
        StringReader input = new("@@@@\n@@@@\n@@@@\n@@@@");

        Floor floor = new(input);

        Assert.Equal(4, floor.CountAccessibleRolls());
    }

    [Fact]
    public void CountAccessibleRollsWithHollowSquareReturnsOnlyCornersAccessible()
    {
        StringReader input = new("@@@\n@.@\n@@@");

        Floor floor = new(input);

        Assert.Equal(4, floor.CountAccessibleRolls());
    }

    [Fact]
    public void CountAccessibleRollsWithSingleRollHasFourOrFewerNeighbors()
    {
        StringReader input = new("...\n.@.\n...");

        Floor floor = new(input);

        Assert.Equal(1, floor.CountAccessibleRolls());
    }

    [Fact]
    public void CountAccessibleRollsWithVerticalLineOfFiveRollsReturnsAllAccessible()
    {
        StringReader input = new("@\n@\n@\n@\n@");

        Floor floor = new(input);

        Assert.Equal(5, floor.CountAccessibleRolls());
    }

    [Fact]
    public void CountAccessibleRollsWithZigZagPatternReturnsCorrectCount()
    {
        StringReader input = new("@@.\n.@@\n@@.");

        Floor floor = new(input);

        Assert.Equal(5, floor.CountAccessibleRolls());
    }
}
