// Advent of Code 2025 - Day 6, Part A
// Processes a grid of numeric values by applying column-specific operations.
// Each column is aggregated using either addition or multiplication,
// and the results are summed to produce the final total.
Grid grid = new(Console.In);

// Process each column in the grid and accumulate the results
long total = 0;
for (int column = 0; column < grid.TotalColumns; column++)
{
    // Cache column values to avoid multiple enumerations
    IEnumerable<long> columnValues = grid.Column(column);

    // Apply the operation specified for this column
    long columnResult = grid.Operation(column) switch
    {
        Operation.Add => columnValues.Sum(),
        Operation.Multiply => columnValues.Aggregate(1L, (accumulator, value) => accumulator * value),
        _ => throw new InvalidOperationException($"Unsupported operation: {grid.Operation(column)}")
    };

    total += columnResult;
}

Console.WriteLine(total);

Console.Write("Press [ENTER] to continue...");
using FileStream consoleInput = new("CONIN$", FileMode.Open, FileAccess.Read);
consoleInput.ReadByte();
