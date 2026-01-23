/// <summary>
/// Entry point for Day 4a Advent of Code solution.
/// Reads floor data from standard input and outputs the count of accessible rolls.
/// </summary>
Floor floor = new(Console.In);
Console.WriteLine(floor.CountAccessibleRolls());
