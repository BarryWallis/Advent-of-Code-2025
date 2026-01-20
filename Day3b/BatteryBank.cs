using System;
using System.IO;
using System.Linq;
using System.Numerics;

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

            Span<char> selectedDigits = stackalloc char[BatteryCount];
            ReadOnlySpan<char> remainingDigits = _bank.AsSpan();
            int selectedCount = 0;

            // Greedily select the largest digit at each position while ensuring enough digits remain
            while (selectedCount < BatteryCount)
            {
                // Find the index of the largest digit within the valid window
                int largestIndex = FindLargestDigitIndex(remainingDigits, selectedCount);
                // Select the digit
                selectedDigits[selectedCount++] = remainingDigits[largestIndex];
                // Move past the selected digit
                remainingDigits = remainingDigits[(largestIndex + 1)..];
            }

            return BigInteger.Parse(selectedDigits);
        }
    }

    /// <summary>
    /// Finds the index of the largest digit within a valid selection window that ensures enough digits remain
    /// to complete the required count.
    /// </summary>
    /// <param name="remainingDigits">The remaining unselected digits to search.</param>
    /// <param name="selectedCount">The number of digits already selected.</param>
    /// <returns>
    /// The index of the largest digit within the valid window.
    /// </returns>
    /// <remarks>
    /// The search window is constrained to ensure that after selecting a digit, enough digits remain
    /// to reach the target count of <see cref="BatteryCount"/>.
    /// If all remaining digits are needed, returns 0 (the first remaining digit).
    /// </remarks>
    private static int FindLargestDigitIndex(ReadOnlySpan<char> remainingDigits, int selectedCount)
    {
        // If all remaining digits are needed, return the first one
        if (remainingDigits.Length + selectedCount <= BatteryCount)
        {
            return 0;
        }

        // Calculate the window size where we can still select enough remaining digits
        int windowSize = remainingDigits.Length - (BatteryCount - selectedCount) + 1;
        
        // Find the index of the largest digit in the window
        int maxIndex = 0;
        for (int i = 1; i < windowSize; i++)
        {
            if (remainingDigits[i] > remainingDigits[maxIndex])
            {
                maxIndex = i;
            }
        }
        
        return maxIndex;
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
