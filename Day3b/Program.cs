// Advent of Code 2025 - Day 3 Part B
// Reads multiple battery banks from standard input and calculates the sum of their total joltages.
// Each battery bank is represented by a line of digits, and the total joltage is the largest
// 12-digit number that can be formed from those digits while maintaining their original order.

using System.Numerics;

using Day3b;

BatteryBank? batteryBank;
BigInteger totalJoltage = 0;

// Read battery banks from standard input until end of stream
while ((batteryBank = BatteryBank.ReadBatteryBank(Console.In)) is not null)
{
    // Accumulate the total joltage from each battery bank
    totalJoltage += batteryBank.TotalJoltage;
}

// Output the final sum of all battery bank joltages
Console.WriteLine(totalJoltage);
