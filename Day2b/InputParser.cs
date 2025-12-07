using System.Numerics;

namespace Day2b;

/// <summary>
/// Provides functionality to parse input into a list of BigInteger IDs.
/// </summary>
internal static class InputParser
{
    /// <summary>
    /// Parses input containing comma-separated ranges in the format "start-end".
    /// </summary>
    /// <param name="input">The input <see cref="TextReader"/> to read from.</param>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="BigInteger"/> values representing all IDs 
    /// in the specified ranges, in sequential order.
    /// </returns>
    /// <exception cref="InvalidOperationException">Thrown when the input is empty or null.</exception>
    /// <exception cref="FormatException">
    /// Thrown when a range is not in the correct "start-end" format, when the start or end 
    /// values cannot be parsed as valid <see cref="BigInteger"/> values, when the start 
    /// of a range is greater than the end, when the range contains negative numbers, or
    /// when spaces are present around the hyphen separator.
    /// </exception>
    /// <remarks>
    /// <para>
    /// Each range is inclusive of both start and end values. Multiple ranges can be specified
    /// by separating them with commas.
    /// </para>
    /// <para>
    /// The method validates that each range has exactly two values separated by a hyphen,
    /// that the start value is less than or equal to the end value, that both values
    /// are non-negative, and that no spaces are present around the hyphen separator.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Parse a simple range
    /// using StringReader input1 = new("1-3");
    /// List&lt;BigInteger&gt; ids1 = InputParser.GetInput(input1);
    /// // ids1 contains: [1, 2, 3]
    /// 
    /// // Parse multiple ranges
    /// using StringReader input2 = new("1-3,5-7");
    /// List&lt;BigInteger&gt; ids2 = InputParser.GetInput(input2);
    /// // ids2 contains: [1, 2, 3, 5, 6, 7]
    /// 
    /// // Parse large numbers
    /// using StringReader input3 = new("999999999999-1000000000001");
    /// List&lt;BigInteger&gt; ids3 = InputParser.GetInput(input3);
    /// // ids3 contains: [999999999999, 1000000000000, 1000000000001]
    /// 
    /// // Invalid range (start > end) throws FormatException
    /// using StringReader input4 = new("10-5");
    /// // List&lt;BigInteger&gt; ids4 = InputParser.GetInput(input4); // Throws FormatException
    /// 
    /// // Invalid range (negative numbers) throws FormatException
    /// using StringReader input5 = new("-3--1");
    /// // List&lt;BigInteger&gt; ids5 = InputParser.GetInput(input5); // Throws FormatException
    /// 
    /// // Invalid range (spaces around hyphen) throws FormatException
    /// using StringReader input6 = new("1 - 3");
    /// // List&lt;BigInteger&gt; ids6 = InputParser.GetInput(input6); // Throws FormatException
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
                
                string startStr = bounds[0];
                string endStr = bounds[1];
                
                if (startStr != startStr.Trim() || endStr != endStr.Trim())
                {
                    throw new FormatException($"Spaces not allowed around hyphen in range: {part}");
                }
                
                BigInteger start = BigInteger.Parse(startStr);
                BigInteger end = BigInteger.Parse(endStr);
                if (start < BigInteger.Zero)
                {
                    throw new FormatException($"Range start cannot be negative: {part}");
                }
                if (end < BigInteger.Zero)
                {
                    throw new FormatException($"Range end cannot be negative: {part}");
                }
                if (start > end)
                {
                    throw new FormatException($"Start of range must be less than or equal to end: {part}");
                }

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
