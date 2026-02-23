/// <summary>
/// Represents mathematical operations that can be applied to grid columns.
/// </summary>
internal enum Operation
{
    Add = '+',
    Multiply = '*',
}

/// <summary>
/// Represents a grid of numeric values with associated column operations.
/// </summary>
internal class Grid
{
    private readonly List<List<long>> _rows = [];
    private readonly List<Operation> _operations = [];

    /// <summary>
    /// Gets the total number of columns in the grid.
    /// </summary>
    public int TotalColumns => _rows.Count > 0 ? _rows[0].Count : 0;

    /// <summary>
    /// Gets the total number of rows in the grid.
    /// </summary>
    public int TotalRows => _rows.Count;

    /// <summary>
    /// Initializes a new instance of the <see cref="Grid"/> class from text input.
    /// </summary>
    /// <param name="input">The text reader containing grid data and operations.</param>
    /// <exception cref="InvalidOperationException">Thrown when rows have inconsistent column counts, operation count doesn't match column count, or invalid operations are encountered.</exception>
    public Grid(TextReader input)
    {
        string[] lines = input.ReadToEnd().Split('\n');
        foreach (string line in lines[0..^1])
        {
            _rows.Add([.. line.Split([' ', '\t'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                              .Select(long.Parse)]);
        }

        _operations = [.. lines[^1].Split([' ', '\t'],
                                          StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                   .Select(static op => (Operation)op[0])];

        ValidateRowColumnCounts();
        ValidateOperationCount();
        ValidateOperations();
    }

    /// <summary>
    /// Retrieves all values in the specified column.
    /// </summary>
    /// <param name="column">The zero-based column index.</param>
    /// <returns>An enumerable of values in the column.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the column index is out of range.</exception>
    internal IEnumerable<long> Column(int column) 
        => column < 0 || column >= TotalColumns 
            ? throw new ArgumentOutOfRangeException(nameof(column), column, 
                                                    $"Column index must be between 0 and {TotalColumns - 1}.")
            : [.. _rows.Select(x => x[column])];

    /// <summary>
    /// Gets the operation associated with the specified column.
    /// </summary>
    /// <param name="column">The zero-based column index.</param>
    /// <returns>The operation to apply to the column.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the column index is out of range.</exception>
    internal Operation Operation(int column) => column < 0 || column >= TotalColumns
            ? throw new ArgumentOutOfRangeException(nameof(column), column,
                                                    $"Column index must be between 0 and {TotalColumns - 1}.")
            : _operations[column];

    /// <summary>
    /// Validates that all rows in the grid have the same number of columns.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when any row has a different column count than the first row.</exception>
    private void ValidateRowColumnCounts()
    {
        if (_rows.Count == 0)
        {
            return;
        }

        int expectedColumnCount = _rows[0].Count;
        for (int i = 1; i < _rows.Count; i++)
        {
            if (_rows[i].Count != expectedColumnCount)
            {
                throw new InvalidOperationException(
                    $"Row {i} has {_rows[i].Count} columns, but expected {expectedColumnCount} columns.");
            }
        }
    }

    /// <summary>
    /// Validates that the number of operations matches the number of columns in the grid.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the operation count doesn't match the column count.</exception>
    private void ValidateOperationCount()
    {
        if (_rows.Count > 0 && _operations.Count != TotalColumns)
        {
            throw new InvalidOperationException(
                $"Operation count ({_operations.Count}) does not match column count ({TotalColumns}).");
        }
    }

    /// <summary>
    /// Validates that all operations are valid enum values.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when an operation has an invalid enum value.</exception>
    private void ValidateOperations()
    {
        foreach (Operation op in _operations)
        {
            if (!Enum.IsDefined(op))
            {
                throw new InvalidOperationException($"Invalid operation value: {(char)op}");
            }
        }
    }
}
