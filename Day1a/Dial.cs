namespace Day1a;

/// <summary>
/// Represents a circular dial with a configurable number of positions.
/// The dial supports rotation in both directions with automatic wraparound.
/// </summary>
/// <remarks>
/// <para>
/// The dial uses zero-based indexing where positions range from 0 to Count-1.
/// Positions automatically wrap around when rotating past the boundaries.
/// </para>
/// <para>
/// Supports both mutable operations via <see cref="Rotate"/> and immutable operations via the + operator.
/// </para>
/// </remarks>
/// <example>
/// <code>
/// // Create a dial with 100 positions starting at position 50
/// Dial dial = new(50, 100);
/// 
/// // Rotate right by 10 positions
/// dial.Rotate(new Rotation('R', 10));
/// Console.WriteLine(dial.Position.Value); // Output: 60
/// 
/// // Rotate left by 20 positions
/// dial.Rotate(new Rotation('L', 20));
/// Console.WriteLine(dial.Position.Value); // Output: 40
/// </code>
/// </example>
public class Dial
{
    /// <summary>
    /// Gets the total number of positions on the dial.
    /// </summary>
    /// <value>
    /// A positive integer representing the total number of positions (0 to Count-1).
    /// </value>
    public int Count { get; init; }

    /// <summary>
    /// Gets the current position on the dial.
    /// </summary>
    /// <value>
    /// A <see cref="DialPosition"/> representing the current normalized position.
    /// </value>
    public DialPosition Position { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Dial"/> class with the specified starting position and count.
    /// </summary>
    /// <param name="startingPosition">
    /// The initial position on the dial. Can be any integer; will be normalized to the valid range [0, count).
    /// Default is 0.
    /// </param>
    /// <param name="count">
    /// The total number of positions on the dial. Must be a positive integer. Default is 100.
    /// </param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="count"/> is not positive.</exception>
    /// <example>
    /// <code>
    /// // Create a dial with default settings (100 positions, starting at 0)
    /// Dial defaultDial = new();
    /// 
    /// // Create a dial starting at position 25
    /// Dial customDial = new(25);
    /// 
    /// // Create a dial with 50 positions starting at position 10
    /// Dial smallDial = new(10, 50);
    /// </code>
    /// </example>
    public Dial(int startingPosition = 0, int count = 100)
    {
        Count = count > 0 ? count : throw new ArgumentException("Count must be positive", nameof(count));
        Position = new DialPosition(startingPosition, this);
    }

    /// <summary>
    /// Creates a new <see cref="Dial"/> instance with the position rotated by the specified rotation.
    /// The original dial remains unchanged.
    /// </summary>
    /// <param name="dial">The dial to rotate.</param>
    /// <param name="rotation">The rotation to apply.</param>
    /// <returns>A new <see cref="Dial"/> instance with the rotated position.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the rotation direction is not 'L' or 'R'.
    /// </exception>
    /// <example>
    /// <code>
    /// Dial original = new(50, 100);
    /// Rotation rotation = new('R', 10);
    /// Dial rotated = original + rotation;
    /// 
    /// Console.WriteLine(original.Position.Value); // Output: 50 (unchanged)
    /// Console.WriteLine(rotated.Position.Value);  // Output: 60
    /// </code>
    /// </example>
    public static Dial operator +(Dial dial, Rotation rotation)
    {
        int offset = rotation.Direction switch
        {
            'R' => rotation.Distance,
            'L' => -rotation.Distance,
            _ => throw new InvalidOperationException($"Invalid rotation direction: {rotation.Direction}")
        };

        int newPosition = dial.Position.Value + offset;
        Dial result = new(newPosition, dial.Count);

        return result;
    }

    /// <summary>
    /// Rotates the dial by the specified rotation, updating the position in place.
    /// </summary>
    /// <param name="rotation">The rotation to apply to the dial.</param>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the rotation direction is not 'L' or 'R'.
    /// </exception>
    /// <remarks>
    /// This method mutates the current dial instance. For immutable operations, use the + operator instead.
    /// </remarks>
    /// <example>
    /// <code>
    /// Dial dial = new(0, 100);
    /// 
    /// dial.Rotate(new Rotation('R', 10));
    /// Console.WriteLine(dial.Position.Value); // Output: 10
    /// 
    /// dial.Rotate(new Rotation('L', 5));
    /// Console.WriteLine(dial.Position.Value); // Output: 5
    /// </code>
    /// </example>
    public void Rotate(Rotation rotation)
    {
        int offset = rotation.Direction switch
        {
            'R' => rotation.Distance,
            'L' => -rotation.Distance,
            _ => throw new InvalidOperationException($"Invalid rotation direction: {rotation.Direction}")
        };

        int newPosition = Position.Value + offset;
        Position = new DialPosition(newPosition, this);
    }

    /// <summary>
    /// Represents an immutable position on the dial with automatic normalization.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Position values are automatically normalized to the valid range [0, Count) of the parent dial.
    /// </para>
    /// <para>
    /// Normalization handles positive overflow, negative wraparound, and large values correctly
    /// using modulo arithmetic.
    /// </para>
    /// </remarks>
    public record DialPosition
    {
        private readonly Dial _dial;

        /// <summary>
        /// Gets the normalized position value on the dial.
        /// </summary>
        /// <value>
        /// An integer in the range [0, Count) representing the current position.
        /// </value>
        public int Value { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DialPosition"/> record.
        /// </summary>
        /// <param name="position">The raw position value to normalize.</param>
        /// <param name="dial">The parent dial that defines the valid position range.</param>
        /// <remarks>
        /// This constructor is internal to prevent external creation of dial positions
        /// without proper normalization context.
        /// </remarks>
        internal DialPosition(int position, Dial dial)
        {
            _dial = dial;
            Value = NormalizePosition(position, _dial.Count);
        }

        /// <summary>
        /// Normalizes a position value to the valid range [0, count) using modulo arithmetic.
        /// </summary>
        /// <param name="value">The position value to normalize.</param>
        /// <param name="count">The total number of positions on the dial.</param>
        /// <returns>A normalized position in the range [0, count).</returns>
        /// <remarks>
        /// <para>
        /// This method correctly handles:
        /// </para>
        /// <list type="bullet">
        /// <item><description>Values already in range (returned as-is)</description></item>
        /// <item><description>Positive overflow (wrapped using modulo)</description></item>
        /// <item><description>Negative values (wrapped to positive equivalent)</description></item>
        /// <item><description>Large positive or negative values (normalized correctly)</description></item>
        /// </list>
        /// </remarks>
        /// <example>
        /// <code>
        /// // For a dial with 100 positions:
        /// NormalizePosition(50, 100);   // Returns: 50
        /// NormalizePosition(100, 100);  // Returns: 0
        /// NormalizePosition(150, 100);  // Returns: 50
        /// NormalizePosition(-1, 100);   // Returns: 99
        /// NormalizePosition(-50, 100);  // Returns: 50
        /// </code>
        /// </example>
        private static int NormalizePosition(int value, int count) => value switch
        {
            >= 0 when value < count => value,
            < 0 => ((value % count) + count) % count,
            _ => value % count
        };
    }
}
