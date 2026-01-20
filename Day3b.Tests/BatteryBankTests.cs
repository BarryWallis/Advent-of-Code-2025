using System.Numerics;

namespace Day3b.Tests;

/// <summary>
/// Unit tests for the <see cref="BatteryBank"/> class.
/// Tests the 12-digit joltage calculation algorithm.
/// </summary>
public class BatteryBankTests
{
    [Fact]
    public void WhenBankInput987654321111111ThenTotalJoltage987654321111()
    {
        BatteryBank batteryBank = new("987654321111111");

        BigInteger actualTotalJoltage = batteryBank.TotalJoltage;

        Assert.Equal(BigInteger.Parse("987654321111"), actualTotalJoltage);
    }

    [Fact]
    public void WhenBankInput811111111111119ThenTotalJoltage811111111119()
    {
        BatteryBank batteryBank = new("811111111111119");

        BigInteger actualTotalJoltage = batteryBank.TotalJoltage;

        Assert.Equal(BigInteger.Parse("811111111119"), actualTotalJoltage);
    }

    [Fact]
    public void WhenBankInput234234234234278ThenTotalJoltage434234234278()
    {
        BatteryBank batteryBank = new("234234234234278");

        BigInteger actualTotalJoltage = batteryBank.TotalJoltage;

        Assert.Equal(BigInteger.Parse("434234234278"), actualTotalJoltage);
    }

    [Fact]
    public void WhenBankInput818181911112111ThenTotalJoltage888911112111()
    {
        BatteryBank batteryBank = new("818181911112111");

        BigInteger actualTotalJoltage = batteryBank.TotalJoltage;

        Assert.Equal(BigInteger.Parse("888911112111"), actualTotalJoltage);
    }


    [Fact]
    public void WhenBankInputHasExactlyTwelveDigitsThenTotalJoltageIsTwelveDigits()
    {
        BatteryBank batteryBank = new("987654321012");

        BigInteger actualTotalJoltage = batteryBank.TotalJoltage;

        Assert.Equal(BigInteger.Parse("987654321012"), actualTotalJoltage);
    }

    [Fact]
    public void WhenBankInputHasThirteenDigitsThenTotalJoltageIsTwelveDigits()
    {
        BatteryBank batteryBank = new("9876543210123");

        BigInteger actualTotalJoltage = batteryBank.TotalJoltage;

        Assert.Equal(BigInteger.Parse("987654321123"), actualTotalJoltage);
    }

    [Fact]
    public void WhenBankInputHasTwentyDigitsThenTotalJoltageIsTwelveDigits()
    {
        BatteryBank batteryBank = new("99999999999999999999");

        BigInteger actualTotalJoltage = batteryBank.TotalJoltage;

        Assert.Equal(BigInteger.Parse("999999999999"), actualTotalJoltage);
    }

    [Fact]
    public void WhenBankInputHasAllZerosThenTotalJoltageIsZero()
    {
        BatteryBank batteryBank = new("000000000000");

        BigInteger actualTotalJoltage = batteryBank.TotalJoltage;

        Assert.Equal(BigInteger.Zero, actualTotalJoltage);
    }

    [Fact]
    public void WhenBankInputHasMixedDigitsSelectsLargestTwelve()
    {
        BatteryBank batteryBank = new("12233445566778899");

        BigInteger actualTotalJoltage = batteryBank.TotalJoltage;

        BigInteger expectedJoltage = BigInteger.Parse("445566778899");
        Assert.Equal(expectedJoltage, actualTotalJoltage);
    }

    [Fact]
    public void WhenBankInputPreservesOrderForSelectedDigits()
    {
        BatteryBank batteryBank = new("13579246801111111");

        BigInteger actualTotalJoltage = batteryBank.TotalJoltage;

        BigInteger expectedJoltage = BigInteger.Parse("946801111111");
        Assert.Equal(expectedJoltage, actualTotalJoltage);
    }

    [Fact]
    public void WhenBankInputIsNullThenThrowsArgumentException()
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new BatteryBank(null!));

        Assert.Equal("bank", exception.ParamName);
        Assert.Contains("Cannot be null or whitespace.", exception.Message);
    }

    [Fact]
    public void WhenBankInputIsEmptyThenThrowsArgumentException()
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new BatteryBank(""));

        Assert.Equal("bank", exception.ParamName);
        Assert.Contains("Cannot be null or whitespace.", exception.Message);
    }

    [Fact]
    public void WhenBankInputIsWhitespaceThenThrowsArgumentException()
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new BatteryBank("   "));

        Assert.Equal("bank", exception.ParamName);
        Assert.Contains("Cannot be null or whitespace.", exception.Message);
    }

    [Fact]
    public void WhenBankInputContainsNonDigitsThenThrowsArgumentException()
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new BatteryBank("12a34"));

        Assert.Equal("bank", exception.ParamName);
        Assert.Contains("Must contain only digits.", exception.Message);
    }

    [Fact]
    public void WhenBankInputContainsSpaceThenThrowsArgumentException()
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new BatteryBank("12 34"));

        Assert.Equal("bank", exception.ParamName);
        Assert.Contains("Must contain only digits.", exception.Message);
    }

    [Fact]
    public void WhenBankInputHasOnlyOneDigitThenThrowsArgumentException()
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new BatteryBank("1"));

        Assert.Equal("bank", exception.ParamName);
        Assert.Contains("Must contain at least two digits.", exception.Message);
    }

    [Fact]
    public void WhenReadingFromTextReaderWithValidInputThenReturnsBatteryBank()
    {
        using StringReader reader = new("987654321111111");

        BatteryBank? batteryBank = BatteryBank.ReadBatteryBank(reader);

        Assert.NotNull(batteryBank);
        Assert.Equal(BigInteger.Parse("987654321111"), batteryBank.TotalJoltage);
    }

    [Fact]
    public void WhenReadingFromTextReaderAtEndOfStreamThenReturnsNull()
    {
        using StringReader reader = new("");

        BatteryBank? batteryBank = BatteryBank.ReadBatteryBank(reader);

        Assert.Null(batteryBank);
    }

    [Fact]
    public void WhenReadingMultipleLinesFromTextReaderThenReturnsEachBatteryBank()
    {
        using StringReader reader = new("987654321111111\n811111111111119\n234234234234278\n818181911112111");

        BatteryBank? bank1 = BatteryBank.ReadBatteryBank(reader);
        BatteryBank? bank2 = BatteryBank.ReadBatteryBank(reader);
        BatteryBank? bank3 = BatteryBank.ReadBatteryBank(reader);
        BatteryBank? bank4 = BatteryBank.ReadBatteryBank(reader);
        BatteryBank? bank5 = BatteryBank.ReadBatteryBank(reader);

        Assert.NotNull(bank1);
        Assert.Equal(BigInteger.Parse("987654321111"), bank1.TotalJoltage);
        Assert.NotNull(bank2);
        Assert.Equal(BigInteger.Parse("811111111119"), bank2.TotalJoltage);
        Assert.NotNull(bank3);
        Assert.Equal(BigInteger.Parse("434234234278"), bank3.TotalJoltage);
        Assert.NotNull(bank4);
        Assert.Equal(BigInteger.Parse("888911112111"), bank4.TotalJoltage);
        Assert.Null(bank5);
    }

    [Fact]
    public void WhenReadingInvalidInputFromTextReaderThenThrowsArgumentException()
    {
        using StringReader reader = new("12a34");

        ArgumentException exception = Assert.Throws<ArgumentException>(() => BatteryBank.ReadBatteryBank(reader));

        Assert.Equal("bank", exception.ParamName);
        Assert.Contains("Must contain only digits.", exception.Message);
    }
}
