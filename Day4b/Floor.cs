/// <summary>
/// Represents a floor containing rolls of paper at various positions.
/// The floor is constructed by parsing input text where '@' represents a roll and '.' represents empty space.
/// </summary>
internal class Floor
{
    /// <summary>
    /// Collection of all roll positions on the floor, indexed by (row, column) for O(1) lookup.
    /// </summary>
    private readonly Dictionary<(int Row, int Column), RollOfPaper> _rolls = [];

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
                        RollOfPaper roll = new(row, column);
                        _rolls[(row, column)] = roll;
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
        foreach (RollOfPaper roll in _rolls.Values)
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
    /// Removes all rolls from the floor by iteratively removing accessible rolls until no more can be removed.
    /// </summary>
    /// <returns>The total number of rolls removed from the floor.</returns>
    /// <remarks>
    /// This method repeatedly:
    /// <list type="number">
    /// <item><description>Identifies all currently accessible rolls (those with 4 or fewer neighbors)</description></item>
    /// <item><description>Removes those rolls from the floor</description></item>
    /// <item><description>Continues until no accessible rolls remain</description></item>
    /// </list>
    /// The process continues in iterations because removing rolls may make previously inaccessible rolls become accessible.
    /// </remarks>
    internal int RemoveAllRolls()
    {
        int removedCount;
        int totalRemovedCount = 0;
        do
        {
            List<(int Row, int Column)> removedKeys = GetAccessibleRollKeys();
            removedCount = removedKeys.Count;
            totalRemovedCount += removedKeys.Count;

            foreach ((int row, int col) key in removedKeys)
            {
                _ = _rolls.Remove(key);
            }

        } while (removedCount > 0);

        return totalRemovedCount;
    }

    /// <summary>
    /// Identifies and returns the keys of all currently accessible rolls without removing them from the floor.
    /// </summary>
    /// <returns>A list containing the keys of all accessible rolls (those with 4 or fewer neighbors including themselves).</returns>
    /// <remarks>
    /// This method does not modify the floor state. It only identifies which rolls are currently accessible
    /// based on the criterion that a roll is accessible if it has 4 or fewer neighboring rolls (including itself) in a 3×3 grid.
    /// </remarks>
    private List<(int Row, int Column)> GetAccessibleRollKeys()
    {
        List<(int Row, int Column)> accessibleKeys = [];
        foreach (RollOfPaper roll in _rolls.Values)
        {
            int count = CountNeighbors(roll);
            if (count <= 4)
            {
                accessibleKeys.Add((roll.Row, roll.Column));
            }
        }
        return accessibleKeys;
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

        // Check all 9 positions in 3×3 grid centered on the roll
        // Using value tuples for dictionary lookups eliminates temporary RollOfPaper allocations
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (_rolls.ContainsKey((row + i, col + j)))
                {
                    count++;
                }
            }
        }

        return count;
    }
}
