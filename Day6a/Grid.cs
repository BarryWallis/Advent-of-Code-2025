internal enum Operation
{
    Add,
    Multiply
}

internal class Grid
{
    public Grid(TextReader input) => throw new NotImplementedException();

    public int TotalColumns { get; internal set; }
    public int TotalRows { get; internal set; }

    internal IEnumerable<int> Column(int column) => throw new NotImplementedException();
    internal Operation Operation(int column) => throw new NotImplementedException();
}
