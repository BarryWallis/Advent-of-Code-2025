using System.Numerics;

namespace Day2a;

public static class BigIntegerExtensions
{
    /// <summary>
    /// Returns the number of digits in the specified BigInteger.
    /// </summary>
    /// <param name="value">The BigInteger value.</param>
    /// <returns>The number of digits in the value.</returns>
    public static int NumberOfDigits(this BigInteger value)
        => value.IsZero ? 1 : BigInteger.Abs(value).ToString().Length;

    /// <summary>
    /// Determines whether the specified integer is odd.
    /// </summary>
    /// <param name="value">The integer value.</param>
    /// <returns>true if odd; otherwise, false.</returns>
    public static bool IsOdd(this int value) => (value & 1) == 1;

    /// <summary>
    /// Determines whether the specified BigInteger is valid.
    /// A BigInteger is valid if it has an even number of digits and the first half of the digits
    /// match the second half of the digits.
    /// </summary>
    /// <param name="value">The BigInteger value to validate.</param>
    /// <returns>true if the number has an even number of digits and the first half matches the second half; otherwise, false.</returns>
    /// <example>
    /// <code>
    /// BigInteger id1 = new BigInteger(1212);
    /// bool isValid1 = id1.IsValid(); // Returns true (12 == 12)
    /// 
    /// BigInteger id2 = new BigInteger(1234);
    /// bool isValid2 = id2.IsValid(); // Returns false (12 != 34)
    /// 
    /// BigInteger id3 = new BigInteger(123);
    /// bool isValid3 = id3.IsValid(); // Returns false (odd number of digits)
    /// </code>
    /// </example>
    public static bool IsValid(this BigInteger value)
    {
        if (value.NumberOfDigits().IsOdd())
        {
            return false;
        }

        string valueStr = value.ToString();
        int halfLength = valueStr.Length / 2;

        return valueStr.AsSpan(0, halfLength).SequenceEqual(valueStr.AsSpan(halfLength));
    }
}
