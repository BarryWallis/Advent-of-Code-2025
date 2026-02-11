using System.Numerics;

namespace Day5b;

/// <summary>
/// Represents a closed interval with positive BigInteger start and end values.
/// An interval [start, end] includes all integers from start to end, inclusive.
/// Both start and end must be positive (> 0) and start must be less than or equal to end.
/// </summary>
public record Interval
{
    private BigInteger _start;
    private BigInteger _end;

    /// <summary>
    /// Gets or sets the start value of the interval.
    /// Must be positive (> 0) and less than or equal to <see cref="end"/>.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// Thrown when the value is less than or equal to zero, or when the value is greater than <see cref="end"/>.
    /// </exception>
    public BigInteger start
    {
        get => _start;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException(MustBePositiveError(nameof(start), value), nameof(start));
            }
            if (value > _end)
            {
                throw new ArgumentException(MustBeLessThanOrEqualError(value, _end), nameof(start));
            }
            _start = value;
        }
    }

    /// <summary>
    /// Gets or sets the end value of the interval.
    /// Must be positive (> 0) and greater than or equal to <see cref="start"/>.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// Thrown when the value is less than or equal to zero, or when the value is less than <see cref="start"/>.
    /// </exception>
    public BigInteger end
    {
        get => _end;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException(MustBePositiveError(nameof(end), value), nameof(end));
            }
            if (_start > value)
            {
                throw new ArgumentException(MustBeLessThanOrEqualError(_start, value), nameof(end));
            }
            _end = value;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Interval"/> record.
    /// </summary>
    /// <param name="start">The start value of the interval. Must be positive (> 0) and less than or equal to <paramref name="end"/>.</param>
    /// <param name="end">The end value of the interval. Must be positive (> 0) and greater than or equal to <paramref name="start"/>.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="start"/> or <paramref name="end"/> is less than or equal to zero,
    /// or when <paramref name="start"/> is greater than <paramref name="end"/>.
    /// </exception>
    /// <example>
    /// <code>
    /// // Create an interval from 10 to 20 (inclusive)
    /// Interval interval = new Interval(10, 20);
    /// 
    /// // Single-value interval
    /// Interval single = new Interval(5, 5);
    /// </code>
    /// </example>
    public Interval(BigInteger start, BigInteger end)
    {
        if (start <= 0)
        {
            throw new ArgumentException(MustBePositiveError(nameof(start), start), nameof(start));
        }
        if (end <= 0)
        {
            throw new ArgumentException(MustBePositiveError(nameof(end), end), nameof(end));
        }
        if (start > end)
        {
            throw new ArgumentException(MustBeLessThanOrEqualError(start, end), nameof(start));
        }

        _start = start;
        _end = end;
    }

    /// <summary>
    /// Generates an error message for values that must be positive.
    /// </summary>
    /// <param name="paramName">The name of the parameter (e.g., "start" or "end").</param>
    /// <param name="value">The invalid value that was provided.</param>
    /// <returns>A formatted error message indicating the parameter must be positive.</returns>
    private static string MustBePositiveError(string paramName, BigInteger value)
        => $"{char.ToUpper(paramName[0])}{paramName[1..]} value ({value}) must be positive.";

    /// <summary>
    /// Generates an error message for when the start value must be less than or equal to the end value.
    /// </summary>
    /// <param name="start">The start value.</param>
    /// <param name="end">The end value.</param>
    /// <returns>A formatted error message indicating the ordering constraint violation.</returns>
    private static string MustBeLessThanOrEqualError(BigInteger start, BigInteger end)
        => $"Start value ({start}) must be less than or equal to end value ({end}).";
}
