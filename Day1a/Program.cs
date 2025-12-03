using Day1a;

Console.WriteLine("Day 1a - Advent of Code 2025");

Dial dial = new(50);
string? line;
int zeroCount = 0;
while ((line = Console.ReadLine()) is not null)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        continue;
    }

    if (line.Length < 2)
    {
        Console.Error.WriteLine($"Invalid input: '{line}'. Expected format: <direction><distance> (e.g., R10, L25)");
        continue;
    }

    char direction = line[0];
    string distanceText = line[1..];

    if (!int.TryParse(distanceText, out int distance))
    {
        Console.Error.WriteLine($"Invalid distance: '{distanceText}'. Distance must be a valid integer.");
        continue;
    }

    try
    {
        Rotation rotation = new(direction, distance);
        dial.Rotate(rotation);

        if (dial.Position.Value == 0)
        {
            zeroCount++;
        }
    }
    catch (ArgumentException ex)
    {
        Console.Error.WriteLine($"Invalid rotation: {ex.Message}");
        continue;
    }
}

Console.WriteLine($"{zeroCount}");

