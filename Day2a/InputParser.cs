using System.Numerics;

namespace Day2a;

/// <summary>
/// Provides functionality to parse input into a list of BigInteger IDs.
/// </summary>
internal static class InputParser
{
    /// <summary>
    /// Parses input containing comma-separated ranges in the format "start-end".
    /// </summary>
    /// <param name="input">The input TextReader to read from.</param>
    /// <returns>A list of BigInteger values representing all IDs in the specified ranges.</returns>
    /// <exception cref="InvalidOperationException">Thrown when input is empty.</exception>
    /// <exception cref="FormatException">Thrown when a range is not in the correct format.</exception>
    /// <example>
    /// <code>
    /// using StringReader input = new("1-3,5-7");
    /// List&lt;BigInteger&gt; ids = InputParser.GetInput(input);
    /// // ids contains: [1, 2, 3, 5, 6, 7]
    /// </code>
    /// </example>
    internal static List<BigInteger> GetInput(TextReader input)
    {
        string? line = input.ReadLine() ?? throw new InvalidOperationException("Input is empty.");

        List<BigInteger> IDs = [.. line.Split(',')
            .SelectMany(static part =>
            {
                string[] bounds = part.Split('-');
                if (bounds.Length != 2)
                {
                    throw new FormatException($"Invalid range format: {part}");
                }
                BigInteger start = BigInteger.Parse(bounds[0]);
                BigInteger end = BigInteger.Parse(bounds[1]);

                List<BigInteger> range = [];
                for (BigInteger i = start; i <= end; i++)
                {
                    range.Add(i);
                }
                return range;
            })];

        return IDs;
    }
}
