using System.Numerics;

Grid grid = new(Console.In);

BigInteger total = 0;
for(int column = 0; column < grid.TotalColumns; column++)
{
    switch (grid.Operation(column))
    {
        case Operation.Add:
            total += grid.Column(column).Aggregate(0, (accumulator, x) => accumulator + x);
            break;
        case Operation.Multiply:
            total += grid.Column(column).Aggregate(1, (accumulator, x) => accumulator * x);
            break;
    }
}

Console.WriteLine(total.ToString());
