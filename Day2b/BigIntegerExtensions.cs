using System.Numerics;

namespace Day2b;

public static class BigIntegerExtensions
{
    /// <summary>
    /// Determines whether the specified <see cref="BigInteger"/> is valid.
    /// A <see cref="BigInteger"/> is valid if it does not contain any repeating pattern.
    /// </summary>
    /// <param name="value">The <see cref="BigInteger"/> value to validate.</param>
    /// <returns>
    /// <see langword="true"/> if the number does not contain any repeating pattern; 
    /// otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// This method checks all possible divisor lengths of the string representation 
    /// to determine if the value consists of a repeating pattern.
    /// </para>
    /// <para>
    /// A number is considered invalid if it can be expressed as repeated segments of equal length
    /// where all segments are identical.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// BigInteger id1 = new BigInteger(1212);
    /// bool isValid1 = id1.IsValid(); // Returns false (repeating pattern "12")
    /// 
    /// BigInteger id2 = new BigInteger(1234);
    /// bool isValid2 = id2.IsValid(); // Returns true (no repeating pattern)
    /// 
    /// BigInteger id3 = new BigInteger(123123123);
    /// bool isValid3 = id3.IsValid(); // Returns false (repeating pattern "123")
    /// 
    /// BigInteger id4 = new BigInteger(111111);
    /// bool isValid4 = id4.IsValid(); // Returns false (repeating pattern "1")
    /// </code>
    /// </example>
    public static bool IsValid(this BigInteger value)
    {
        string valueStr = value.ToString();
        
        for (int patternLength = 1; patternLength <= valueStr.Length / 2; patternLength++)
        {
            if (valueStr.Length % patternLength != 0)
            {
                continue;
            }

            bool hasRepeatingPattern = true;
            for (int position = patternLength; position < valueStr.Length; position += patternLength)
            {
                if (!valueStr.AsSpan(0, patternLength).SequenceEqual(valueStr.AsSpan(position, patternLength)))
                {
                    hasRepeatingPattern = false;
                    break;
                }
            }

            if (hasRepeatingPattern)
            {
                return false;
            }
        }

        return true;
    }
}
