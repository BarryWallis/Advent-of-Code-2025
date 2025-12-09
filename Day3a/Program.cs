using Day3a;

BatteryBank? batteryBank;
int totalJoltage = 0;
while ((batteryBank = BatteryBank.ReadBatteryBank(Console.In)) is not null)
{
    totalJoltage += batteryBank.TotalJoltage;
}

Console.WriteLine(totalJoltage);

