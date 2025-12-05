namespace Day1a;

/// <summary>
/// Represents a rotation command for a dial, specifying direction and distance.
/// </summary>
/// <param name="Direction">The direction of rotation: 'L' for left (counter-clockwise) or 'R' for right (clockwise).</param>
/// <param name="Distance">The distance to rotate, must be a positive integer.</param>
/// <exception cref="ArgumentException">Thrown when Direction is not 'L' or 'R', or when Distance is not positive.</exception>
/// <example>
/// <code>
/// // Create a rotation 10 positions to the right
/// Rotation rightRotation = new('R', 10);
/// 
/// // Create a rotation 5 positions to the left
/// Rotation leftRotation = new('L', 5);
/// </code>
/// </example>
public record Rotation(char Direction, int Distance)
{
    /// <summary>
    /// Gets the direction of rotation.
    /// </summary>
    /// <value>
    /// 'L' for left (counter-clockwise) rotation or 'R' for right (clockwise) rotation.
    /// </value>
    /// <exception cref="ArgumentException">Thrown when the value is not 'L' or 'R'.</exception>
    public char Direction
    {
        get;
        init
        {
            if (value is not 'L' and not 'R')
            {
                throw new ArgumentException("Direction must be 'L' or 'R'", nameof(Direction));
            }
            field = value;
        }
    } = Direction;

    /// <summary>
    /// Gets the distance to rotate.
    /// </summary>
    /// <value>
    /// A positive integer representing the number of positions to rotate.
    /// </value>
    /// <exception cref="ArgumentException">Thrown when the value is not positive.</exception>
    public int Distance
    {
        get => field;
        init
        {
            if (value <= 0)
            {
                throw new ArgumentException("Distance must be positive", nameof(Distance));
            }
            field = value;
        }
    } = Distance;
}
