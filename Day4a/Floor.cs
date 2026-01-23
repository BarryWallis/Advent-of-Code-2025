/// <summary>
/// Represents a floor containing rolls of paper at various positions.
/// The floor is constructed by parsing input text where '@' represents a roll and '.' represents empty space.
/// </summary>
internal record Floor
{
    /// <summary>
    /// Collection of all roll positions on the floor.
    /// </summary>
    private readonly HashSet<RollOfPaper> _rolls = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="Floor"/> class by parsing input text.
    /// </summary>
    /// <param name="input">Text reader containing the floor layout where '@' represents rolls and '.' represents empty spaces.</param>
    /// <exception cref="InvalidDataException">Thrown when an unexpected character (not '@' or '.') is encountered in the input.</exception>
    /// <remarks>
    /// The input format consists of lines of text where:
    /// <list type="bullet">
    /// <item><description>'@' indicates a roll of paper at that position</description></item>
    /// <item><description>'.' indicates an empty space</description></item>
    /// </list>
    /// Position coordinates are zero-based, with (0,0) at the top-left corner.
    /// </remarks>
    public Floor(TextReader input)
    {
        string? line;
        int row = -1;
        while ((line = input.ReadLine()) != null)
        {
            row += 1;
            int column = -1;
            foreach (char c in line)
            {
                column += 1;

                // Process each character
                switch (c)
                {
                    case '.':
                        // Handle empty space by doing nothing
                        break;
                    case '@':
                        // Handle space with a roll of paper
                        _ = _rolls.Add(new RollOfPaper(row, column));
                        break;
                    default:
                        // Handle other cases
                        throw new InvalidDataException($"Unexpected character '{c}' at row {row}, column {column}");
                }
            }
        }
    }

    /// <summary>
    /// Counts the number of accessible rolls on the floor.
    /// A roll is considered accessible if it has 4 or fewer neighboring rolls (including itself) in a 3×3 grid.
    /// </summary>
    /// <returns>The count of accessible rolls.</returns>
    /// <remarks>
    /// For each roll, this method:
    /// <list type="number">
    /// <item><description>Examines all positions in a 3×3 grid centered on the roll</description></item>
    /// <item><description>Counts how many of those positions contain rolls (including the center roll itself)</description></item>
    /// <item><description>Considers the roll accessible if the neighbor count is ≤ 4</description></item>
    /// </list>
    /// </remarks>
    internal int CountAccessibleRolls()
    {
        int accessibleCount = 0;
        foreach (RollOfPaper roll in _rolls)
        {
            int count = CountNeighbors(roll);

            if (count <= 4)
            {
                accessibleCount += 1;
            }
        }

        return accessibleCount;
    }

    /// <summary>
    /// Counts the number of neighboring rolls (including the roll itself) in a 3×3 grid.
    /// </summary>
    /// <param name="roll">The roll to count neighbors for.</param>
    /// <returns>The count of neighboring rolls including the center roll.</returns>
    private int CountNeighbors(RollOfPaper roll)
    {
        int count = 0;
        int row = roll.Row;
        int col = roll.Column;

        // Check all 9 positions in 3×3 grid without allocating objects
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                // Use SetEqualityComparer to avoid allocation
                if (_rolls.TryGetValue(new RollOfPaper(row + i, col + j), out _))
                {
                    count++;
                }
            }
        }

        return count;
    }
}
