using System;
using System.IO;
using System.Linq;

namespace Day3a;

/// <summary>
/// Represents a battery bank with a bank input and total joltage.
/// </summary>
/// <remarks>
/// <para>
/// A battery bank processes a string of digits to calculate total joltage by selecting
/// two specific digits based on the leftmost largest digit algorithm.
/// </para>
/// <para>
/// The joltage is calculated by finding the leftmost largest digit in all but the last character,
/// then finding the leftmost largest digit after that position, and combining them as a two-digit number.
/// </para>
/// </remarks>
/// <example>
/// <code>
/// // Create a battery bank with input "1234"
/// BatteryBank bank = new("1234");
/// Console.WriteLine(bank.TotalJoltage); // Output: 34
/// 
/// // Read from a TextReader
/// BatteryBank? bank2 = BatteryBank.ReadBatteryBank(Console.In);
/// if (bank2 is not null)
/// {
///     Console.WriteLine(bank2.TotalJoltage);
/// }
/// </code>
/// </example>
internal record BatteryBank
{
    private string _bank { get; init; }

    /// <summary>
    /// Gets the total joltage of the battery bank.
    /// </summary>
    /// <value>
    /// An integer representing the calculated joltage from the bank input.
    /// The value is derived by selecting two digits from the bank string and combining them.
    /// </value>
    /// <remarks>
    /// The calculation algorithm:
    /// <list type="number">
    /// <item><description>Find the leftmost largest digit in all but the last character</description></item>
    /// <item><description>Find the leftmost largest digit after that position</description></item>
    /// <item><description>Combine as (firstDigit * 10) + secondDigit</description></item>
    /// </list>
    /// </remarks>
    /// <example>
    /// <code>
    /// BatteryBank bank = new("1234");
    /// // Finds '3' at index 2 in "123"
    /// // Finds '4' at index 3 in "4"
    /// // Result: 34
    /// int joltage = bank.TotalJoltage;
    /// </code>
    /// </example>
    public int TotalJoltage
    {
        get
        {
            int firstDigitIndex = LeftmostLargestDigitIndex(_bank.AsSpan()[..^1]);
            int secondDigitIndex = LeftmostLargestDigitIndex(_bank.AsSpan()[(firstDigitIndex + 1)..]) + firstDigitIndex + 1;
            return ((_bank[firstDigitIndex] - '0') * 10) + (_bank[secondDigitIndex] - '0');
        }
    }

    /// <summary>
    /// Finds the index of the leftmost largest digit in a string.
    /// </summary>
    /// <param name="value">The string to search.</param>
    /// <returns>The zero-based index of the leftmost occurrence of the largest digit.</returns>
    /// <remarks>
    /// This is a convenience overload that converts the string to a <see cref="ReadOnlySpan{T}"/>
    /// and delegates to the span-based implementation.
    /// </remarks>
    /// <example>
    /// <code>
    /// int index = BatteryBank.LeftmostLargestDigitIndex("1234");
    /// Console.WriteLine(index); // Output: 3 (index of '4')
    /// 
    /// index = BatteryBank.LeftmostLargestDigitIndex("4321");
    /// Console.WriteLine(index); // Output: 0 (index of '4')
    /// </code>
    /// </example>
    internal static int LeftmostLargestDigitIndex(string value) => LeftmostLargestDigitIndex(value.AsSpan());

    /// <summary>
    /// Finds the index of the leftmost largest digit in a span of characters.
    /// </summary>
    /// <param name="value">The span of characters to search.</param>
    /// <returns>The zero-based index of the leftmost occurrence of the largest digit.</returns>
    /// <remarks>
    /// <para>
    /// When multiple instances of the largest digit exist, this method returns the index of the first occurrence.
    /// </para>
    /// <para>
    /// This method uses span-based operations for optimal performance, avoiding string allocations
    /// when working with substrings.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// string value = "9145";
    /// int index = BatteryBank.LeftmostLargestDigitIndex(value.AsSpan());
    /// Console.WriteLine(index); // Output: 0 (leftmost '9')
    /// 
    /// value = "1924";
    /// index = BatteryBank.LeftmostLargestDigitIndex(value.AsSpan());
    /// Console.WriteLine(index); // Output: 1 (leftmost '9')
    /// </code>
    /// </example>
    internal static int LeftmostLargestDigitIndex(ReadOnlySpan<char> value)
    {
        int largestDigitIndex = 0;
        char largestDigit = value[0];

        for (int i = 1; i < value.Length; i++)
        {
            if (value[i] > largestDigit)
            {
                largestDigit = value[i];
                largestDigitIndex = i;
            }
        }

        return largestDigitIndex;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BatteryBank"/> record with the specified bank input.
    /// </summary>
    /// <param name="bank">
    /// A string containing only digit characters. Must contain at least two digits.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="bank"/> is null, empty, or contains only whitespace.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="bank"/> contains non-digit characters.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="bank"/> contains fewer than two digits.
    /// </exception>
    /// <example>
    /// <code>
    /// // Valid inputs
    /// BatteryBank bank1 = new("12");
    /// BatteryBank bank2 = new("123456789");
    /// BatteryBank bank3 = new("00");
    /// 
    /// // Invalid inputs (throw ArgumentException)
    /// // BatteryBank invalid1 = new("");        // Empty string
    /// // BatteryBank invalid2 = new("1");       // Only one digit
    /// // BatteryBank invalid3 = new("12a");     // Contains non-digit
    /// // BatteryBank invalid4 = new("1 2");     // Contains space
    /// </code>
    /// </example>
    internal BatteryBank(string bank)
    {
        if (string.IsNullOrWhiteSpace(bank))
        {
            throw new ArgumentException("Cannot be null or whitespace.", nameof(bank));
        }
        if (!bank.All(char.IsDigit))
        {
            throw new ArgumentException("Must contain only digits.", nameof(bank));
        }
        if (bank.Length < 2)
        {
            throw new ArgumentException("Must contain at least two digits.", nameof(bank));
        }

        _bank = bank;
    }

    /// <summary>
    /// Reads a battery bank from a <see cref="TextReader"/>.
    /// </summary>
    /// <param name="textReader">The text reader to read from.</param>
    /// <returns>
    /// A new <see cref="BatteryBank"/> instance if a line was successfully read;
    /// otherwise, <see langword="null"/> if the end of the stream was reached.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the read line does not meet the requirements for a valid battery bank
    /// (see <see cref="BatteryBank(string)"/> for validation rules).
    /// </exception>
    /// <remarks>
    /// This method reads a single line from the text reader. Each line should contain
    /// a valid battery bank input string.
    /// </remarks>
    /// <example>
    /// <code>
    /// using StringReader reader = new("123\n456\n789");
    /// 
    /// while (BatteryBank.ReadBatteryBank(reader) is BatteryBank bank)
    /// {
    ///     Console.WriteLine(bank.TotalJoltage);
    /// }
    /// 
    /// // Reading from Console
    /// int totalJoltage = 0;
    /// while (BatteryBank.ReadBatteryBank(Console.In) is BatteryBank consoleBank)
    /// {
    ///     totalJoltage += consoleBank.TotalJoltage;
    /// }
    /// Console.WriteLine(totalJoltage);
    /// </code>
    /// </example>
    internal static BatteryBank? ReadBatteryBank(TextReader textReader)
    {
        string? line = textReader.ReadLine();
        return line is null ? null : new BatteryBank(line);
    }
}

