using System.Numerics;

using Day5b;

List<Day5b.Interval> intervals = Program.GetIntervals(Console.In);
Program.NormalizeIntervals(intervals);
BigInteger result = Program.CountIntervals(intervals);
Console.WriteLine(result);
Thread.Sleep(100000);

/// <summary>
/// Main program for Day 5 Part B of Advent of Code 2025.
/// Reads intervals from input, normalizes them by merging overlapping and adjacent intervals,
/// and counts the total number of values covered by all intervals.
/// </summary>
public partial class Program
{
    /// <summary>
    /// Counts the total number of values covered by all intervals in the list.
    /// For each interval [start, end], this includes all integers from start to end inclusive.
    /// </summary>
    /// <param name="intervals">The list of intervals to count. Intervals should be normalized (non-overlapping) for accurate results.</param>
    /// <returns>The total count of all values across all intervals.</returns>
    /// <example>
    /// <code>
    /// List&lt;Interval&gt; intervals = new()
    /// {
    ///     new Interval(10, 20),  // 11 values: 10, 11, ..., 20
    ///     new Interval(30, 35)   //  6 values: 30, 31, ..., 35
    /// };
    /// BigInteger count = CountIntervals(intervals); // Returns 17
    /// </code>
    /// </example>
    internal static BigInteger CountIntervals(List<Interval> intervals)
    {
        BigInteger count = 0;
        foreach (Interval interval in intervals)
        {
            count += interval.end - interval.start + 1;
        }

        return count;
    }

    /// <summary>
    /// Normalizes a list of intervals by merging overlapping or adjacent intervals and removing duplicates.
    /// This modifies the input list in-place. After normalization, the list contains only non-overlapping intervals.
    /// </summary>
    /// <param name="intervals">The list of intervals to normalize. This list is modified in-place.</param>
    /// <remarks>
    /// <para>The normalization process performs the following operations:</para>
    /// <list type="bullet">
    /// <item><description>Removes intervals that are entirely contained within other intervals</description></item>
    /// <item><description>Merges overlapping intervals into a single larger interval</description></item>
    /// <item><description>Merges adjacent intervals (e.g., [1-10] and [11-20] become [1-20])</description></item>
    /// </list>
    /// <para>Time complexity: O(n²) where n is the number of intervals.</para>
    /// </remarks>
    /// <example>
    /// <code>
    /// List&lt;Interval&gt; intervals = new()
    /// {
    ///     new Interval(10, 20),
    ///     new Interval(15, 25),  // Overlaps with [10-20]
    ///     new Interval(26, 30)   // Adjacent to [15-25]
    /// };
    /// NormalizeIntervals(intervals);
    /// // Result: single interval [10-30]
    /// </code>
    /// </example>
    internal static void NormalizeIntervals(List<Interval> intervals)
    {
        int i = 0;
        while (i < intervals.Count)
        {
            int j = i + 1;
            bool currentRemoved = false;

            while (j < intervals.Count)
            {
                Interval i1 = intervals[i];
                Interval i2 = intervals[j];

                // Check if I1 lies entirely inside I2
                if (i1.start >= i2.start && i1.end <= i2.end)
                {
                    intervals.RemoveAt(i);
                    currentRemoved = true;
                    break;
                }
                // Check if I2 lies entirely inside I1
                else if (i2.start >= i1.start && i2.end <= i1.end)
                {
                    intervals.RemoveAt(j);
                    // Don't increment j, check the next interval at position j
                }
                // Check if intervals overlap or touch (including adjacent intervals)
                else if (i1.start <= i2.end + 1 && i2.start <= i1.end + 1)
                {
                    // Expand I1 to encompass I2
                    BigInteger newStart = BigInteger.Min(i1.start, i2.start);
                    BigInteger newEnd = BigInteger.Max(i1.end, i2.end);

                    intervals[i] = new Interval(newStart, newEnd);
                    intervals.RemoveAt(j);

                    // Restart j to check the expanded interval against all remaining intervals
                    j = i + 1;
                }
                else
                {
                    j++;
                }
            }

            if (!currentRemoved)
            {
                i++;
            }
        }
    }

    /// <summary>
    /// Reads and parses intervals from a text input stream.
    /// Each line should contain a single interval in the format "start-end" where start and end are positive integers.
    /// Reading stops at the first blank line or end of stream.
    /// </summary>
    /// <param name="input">The text reader to read interval data from.</param>
    /// <returns>A list of parsed intervals.</returns>
    /// <exception cref="InvalidDataException">
    /// Thrown when the input contains no data or only whitespace.
    /// </exception>
    /// <exception cref="FormatException">
    /// Thrown when a line does not match the expected "start-end" format or contains non-numeric values.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when an interval has invalid values (e.g., start > end, non-positive values).
    /// </exception>
    /// <example>
    /// <code>
    /// string input = "10-20\n30-40\n50-60";
    /// using StringReader reader = new(input);
    /// List&lt;Interval&gt; intervals = GetIntervals(reader);
    /// // Returns 3 intervals: [10-20], [30-40], [50-60]
    /// </code>
    /// </example>
    public static List<Interval> GetIntervals(TextReader input)
    {
        string buffer = input.ReadToEnd();
        IEnumerable<string> lines = buffer.Split(Environment.NewLine).TakeWhile(line => !string.IsNullOrWhiteSpace(line));
        if (!lines.Any())
        {
            throw new InvalidDataException("Input contains no data. Expected at least one interval.");
        }

        List<Interval> intervals = new(lines.Count());
        foreach (string line in lines)
        {
            string[] intervalBuffer = line.Split('-');
            if (intervalBuffer.Length != 2)
            {
                throw new FormatException($"Invalid interval format: '{line}'. Expected format: 'start-end'");
            }

            Interval interval = new(BigInteger.Parse(intervalBuffer[0]), BigInteger.Parse(intervalBuffer[1]));
            intervals.Add(interval);
        }

        return intervals;
    }
}
