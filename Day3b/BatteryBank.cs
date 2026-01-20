using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace Day3b;

/// <summary>
/// Represents a battery bank that stores a sequence of digits where each digit represents the power of a battery and 
/// calculates the maximum possible joltage by selecting a subset of digits in their original order.
/// </summary>
/// <remarks>
/// The battery bank uses a greedy algorithm to determine the largest possible 12-digit number that can be
/// formed by selecting digits from the input sequence while maintaining their original order.
/// </remarks>
internal record BatteryBank
{
    /// <summary>
    /// The number of batteries to select for the total joltage calculation.
    /// </summary>
    private const int BatteryCount = 12;

    /// <summary>
    /// The input sequence of battery powers from which the joltage is calculated.
    /// </summary>
    private string _bank { get; init; }

    /// <summary>
    /// Gets the maximum possible joltage by selecting the largest 12-digit number from the battery bank
    /// while maintaining the original order of digits.
    /// </summary>
    /// <value>A <see cref="BigInteger"/> representing the largest 12-digit number achievable.</value>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the battery bank contains fewer than 12 digits.
    /// </exception>
    /// <example>
    /// For a battery bank with digits "98765432109876", the algorithm selects "987654321098"
    /// by choosing the largest digit at each position that still allows 12 total digits to be selected.
    /// </example>
    public BigInteger TotalJoltage
    {
        get
        {
            if (_bank.Length < BatteryCount)
            {
                throw new InvalidOperationException($"Battery bank must contain at least {BatteryCount} digits to " +
                    $"calculate total joltage.");
            }

            StringBuilder selectedDigits = new();
            string remainingDigits = _bank;
            // Greedily select the largest digit at each position while ensuring enough digits remain
            while (selectedDigits.Length < BatteryCount)
            {
                // Find the window containing the largest available digit
                remainingDigits = FindLargestDigit(remainingDigits, selectedDigits.Length);
                // Select the first digit in the window (which is the largest)
                _ = selectedDigits.Append(remainingDigits[0]);
                // Move past the selected digit
                remainingDigits = remainingDigits[1..];
            }

            return BigInteger.Parse(selectedDigits.ToString());
        }
    }

    /// <summary>
    /// Finds the largest digit within a valid selection window that ensures enough digits remain
    /// to complete the required count.
    /// </summary>
    /// <param name="remainingDigits">The remaining unselected digits to search.</param>
    /// <param name="selectedDigitsLength">The number of digits already selected.</param>
    /// <returns>
    /// A substring starting at the position of the largest digit, or the entire remaining string
    /// if all remaining digits must be selected.
    /// </returns>
    /// <exception cref="UnreachableException">
    /// Thrown if no valid largest digit is found, which should never occur with valid input.
    /// </exception>
    /// <remarks>
    /// The search window is constrained to ensure that after selecting a digit, enough digits remain
    /// to reach the target count of <see cref="BatteryCount"/>.
    /// </remarks>
    private static string FindLargestDigit(string remainingDigits, int selectedDigitsLength)
    {
        // If all remaining digits are needed, return them all one at a time.
        if (remainingDigits.Length + selectedDigitsLength <= BatteryCount)
        {
            return remainingDigits;
        }

        // Initialize to a value less than any digit character ('0' = 48, so '/' = 47)
        const char lowerThanDigits = (char)('0' - 1);
        char largestDigit = lowerThanDigits;
        int largestDigitIndex = -1;
        
        // Search only the window where we can still select enough remaining digits
        // Window size = remainingDigits.Length - (BatteryCount - selectedDigitsLength) + 1
        for (int i = 0; i <= remainingDigits.Length - (BatteryCount - selectedDigitsLength); i++)
        {
            if (remainingDigits[i] > largestDigit)
            {
                largestDigit = remainingDigits[i];
                largestDigitIndex = i;
            }
        }

        return largestDigitIndex < 0 ? throw new UnreachableException() : remainingDigits[largestDigitIndex..];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BatteryBank"/> class with the specified digit sequence.
    /// </summary>
    /// <param name="bank">A string containing only digit characters (0-9).</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="bank"/> is null, whitespace, contains non-digit characters,
    /// or has fewer than two digits.
    /// </exception>
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
    /// Reads a battery bank from a text reader.
    /// </summary>
    /// <param name="textReader">The text reader to read from.</param>
    /// <returns>
    /// A new <see cref="BatteryBank"/> instance if a line is successfully read; otherwise, <c>null</c>
    /// if the end of the stream is reached.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the line read does not meet the requirements for a valid battery bank.
    /// </exception>
    internal static BatteryBank? ReadBatteryBank(TextReader textReader)
    {
        string? line = textReader.ReadLine();
        return line is null ? null : new BatteryBank(line);
    }
}