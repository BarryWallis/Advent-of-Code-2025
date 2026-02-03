using System.Numerics;

using IntervalTree;

namespace Day5a;

/// <summary>
/// Database for managing and querying fresh ingredient ID ranges.
/// </summary>
public class FreshIdDatabase
{
    private readonly IntervalTree<BigInteger, int> _intervalTree = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="FreshIdDatabase"/> class by parsing ID ranges from input.
    /// </summary>
    /// <param name="input">Text reader containing ID ranges in the format "start-end", one per line.</param>
    public FreshIdDatabase(TextReader input)
    {
        int index = 0;
        foreach (string line in ReadNonEmptyLines(input))
        {
            string[] ids = line.Split('-');
            _intervalTree.Add(BigInteger.Parse(ids[0]), BigInteger.Parse(ids[1]), ++index);
        }
    }

    /// <summary>
    /// Counts how many ingredient IDs from the input exist in the database's ID ranges.
    /// </summary>
    /// <param name="input">Text reader containing ingredient IDs to check, one per line.</param>
    /// <returns>The count of ingredient IDs that match any range in the database.</returns>
    public BigInteger CheckIngredients(TextReader input)
        => ReadNonEmptyLines(input).Select(BigInteger.Parse).Count(id => _intervalTree.Query(id).Any());

    /// <summary>
    /// Reads non-empty, non-whitespace lines from a text reader.
    /// </summary>
    /// <param name="input">The text reader to read from.</param>
    /// <returns>An enumerable of non-empty lines.</returns>
    private static IEnumerable<string> ReadNonEmptyLines(TextReader input)
    {
        string? line;
        while (!string.IsNullOrWhiteSpace(line = input.ReadLine()))
        {
            yield return line;
        }
    }
}
