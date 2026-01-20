namespace Day3a.Tests;

/// <summary>
/// Unit tests for the <see cref="BatteryBank"/> class.
/// </summary>
public class BatteryBankTests
{
    [Fact]
    public void ConstructorWithValidInputCreatesInstance()
    {
        BatteryBank batteryBank = new("12");

        Assert.NotNull(batteryBank);
    }

    [Fact]
    public void ConstructorWithNullInputThrowsArgumentException()
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new BatteryBank(null!));

        Assert.Equal("bank", exception.ParamName);
        Assert.Contains("Cannot be null or whitespace.", exception.Message);
    }

    [Fact]
    public void ConstructorWithEmptyStringThrowsArgumentException()
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new BatteryBank(""));

        Assert.Equal("bank", exception.ParamName);
        Assert.Contains("Cannot be null or whitespace.", exception.Message);
    }

    [Fact]
    public void ConstructorWithWhitespaceThrowsArgumentException()
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new BatteryBank("   "));

        Assert.Equal("bank", exception.ParamName);
        Assert.Contains("Cannot be null or whitespace.", exception.Message);
    }

    [Fact]
    public void ConstructorWithSingleDigitThrowsArgumentException()
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new BatteryBank("5"));

        Assert.Equal("bank", exception.ParamName);
        Assert.Contains("Must contain at least two digits.", exception.Message);
    }

    [Fact]
    public void ConstructorWithNonDigitCharactersThrowsArgumentException()
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new BatteryBank("12a"));

        Assert.Equal("bank", exception.ParamName);
        Assert.Contains("Must contain only digits.", exception.Message);
    }

    [Fact]
    public void ConstructorWithSpecialCharactersThrowsArgumentException()
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new BatteryBank("12!"));

        Assert.Equal("bank", exception.ParamName);
        Assert.Contains("Must contain only digits.", exception.Message);
    }

    [Fact]
    public void ConstructorWithSpaceThrowsArgumentException()
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new BatteryBank("1 2"));

        Assert.Equal("bank", exception.ParamName);
        Assert.Contains("Must contain only digits.", exception.Message);
    }

    [Theory]
    [InlineData("12", 12)]
    [InlineData("21", 21)]
    [InlineData("34", 34)]
    [InlineData("43", 43)]
    public void TotalJoltageWithTwoDigitsReturnsCorrectValue(string bank, int expectedJoltage)
    {
        BatteryBank batteryBank = new(bank);

        int joltage = batteryBank.TotalJoltage;

        Assert.Equal(expectedJoltage, joltage);
    }

    [Theory]
    [InlineData("123", 23)]
    [InlineData("321", 32)]
    [InlineData("213", 23)]
    [InlineData("132", 32)]
    public void TotalJoltageWithThreeDigitsReturnsCorrectValue(string bank, int expectedJoltage)
    {
        BatteryBank batteryBank = new(bank);

        int joltage = batteryBank.TotalJoltage;

        Assert.Equal(expectedJoltage, joltage);
    }

    [Theory]
    [InlineData("1234", 34)]
    [InlineData("4321", 43)]
    [InlineData("1324", 34)]
    [InlineData("2413", 43)]
    public void TotalJoltageWithFourDigitsReturnsCorrectValue(string bank, int expectedJoltage)
    {
        BatteryBank batteryBank = new(bank);

        int joltage = batteryBank.TotalJoltage;

        Assert.Equal(expectedJoltage, joltage);
    }

    [Theory]
    [InlineData("12345", 45)]
    [InlineData("54321", 54)]
    [InlineData("13524", 54)]
    [InlineData("24135", 45)]
    public void TotalJoltageWithFiveDigitsReturnsCorrectValue(string bank, int expectedJoltage)
    {
        BatteryBank batteryBank = new(bank);

        int joltage = batteryBank.TotalJoltage;

        Assert.Equal(expectedJoltage, joltage);
    }

    [Theory]
    [InlineData("00", 0)]
    [InlineData("11", 11)]
    [InlineData("99", 99)]
    public void TotalJoltageWithSameDigitsReturnsCorrectValue(string bank, int expectedJoltage)
    {
        BatteryBank batteryBank = new(bank);

        int joltage = batteryBank.TotalJoltage;

        Assert.Equal(expectedJoltage, joltage);
    }

    [Theory]
    [InlineData("9145", 95)]
    [InlineData("1924", 94)]
    [InlineData("1249", 49)]
    [InlineData("1239", 39)]
    public void TotalJoltageWithMultipleNinesReturnsCorrectValue(string bank, int expectedJoltage)
    {
        BatteryBank batteryBank = new(bank);

        int joltage = batteryBank.TotalJoltage;

        Assert.Equal(expectedJoltage, joltage);
    }

    [Fact]
    public void TotalJoltageWithMinimumInputReturnsZero()
    {
        BatteryBank batteryBank = new("00");

        int joltage = batteryBank.TotalJoltage;

        Assert.Equal(0, joltage);
    }

    [Fact]
    public void TotalJoltageWithMaximumInputReturnsNinetyNine()
    {
        BatteryBank batteryBank = new("99");

        int joltage = batteryBank.TotalJoltage;

        Assert.Equal(99, joltage);
    }

    [Fact]
    public void TotalJoltageIsComputedOnlyOnce()
    {
        BatteryBank batteryBank = new("1234");

        int joltage1 = batteryBank.TotalJoltage;
        int joltage2 = batteryBank.TotalJoltage;

        Assert.Equal(joltage1, joltage2);
    }

    [Theory]
    [InlineData("90", 90)]
    [InlineData("09", 09)]
    [InlineData("19", 19)]
    [InlineData("91", 91)]
    public void TotalJoltageWithZeroAndNineReturnsCorrectValue(string bank, int expectedJoltage)
    {
        BatteryBank batteryBank = new(bank);

        int joltage = batteryBank.TotalJoltage;

        Assert.Equal(expectedJoltage, joltage);
    }

    [Theory]
    [InlineData("0123456789", 89)]
    [InlineData("9876543210", 98)]
    public void TotalJoltageWithAllTenDigitsReturnsCorrectValue(string bank, int expectedJoltage)
    {
        BatteryBank batteryBank = new(bank);

        int joltage = batteryBank.TotalJoltage;

        Assert.Equal(expectedJoltage, joltage);
    }

    [Theory]
    [InlineData("012", 12)]
    [InlineData("0123", 23)]
    [InlineData("01234", 34)]
    [InlineData("012345", 45)]
    public void TotalJoltageWithLeadingZeroReturnsCorrectValue(string bank, int expectedJoltage)
    {
        BatteryBank batteryBank = new(bank);

        int joltage = batteryBank.TotalJoltage;

        Assert.Equal(expectedJoltage, joltage);
    }

    [Fact]
    public void ReadBatteryBankWithValidInputReturnsInstance()
    {
        StringReader reader = new("12");

        BatteryBank? batteryBank = BatteryBank.ReadBatteryBank(reader);

        Assert.NotNull(batteryBank);
    }

    [Fact]
    public void ReadBatteryBankWithEmptyInputReturnsNull()
    {
        StringReader reader = new("");

        BatteryBank? batteryBank = BatteryBank.ReadBatteryBank(reader);

        Assert.Null(batteryBank);
    }

    [Fact]
    public void ReadBatteryBankWithMultipleLinesReadsFirstLine()
    {
        StringReader reader = new("123\n456\n789");

        BatteryBank? batteryBank = BatteryBank.ReadBatteryBank(reader);

        Assert.NotNull(batteryBank);
        Assert.Equal(23, batteryBank.TotalJoltage);
    }

    [Fact]
    public void ReadBatteryBankWithValidInputThenEmptyReturnsInstanceThenNull()
    {
        StringReader reader = new("123\n");

        BatteryBank? batteryBank1 = BatteryBank.ReadBatteryBank(reader);
        BatteryBank? batteryBank2 = BatteryBank.ReadBatteryBank(reader);

        Assert.NotNull(batteryBank1);
        Assert.Null(batteryBank2);
    }

    [Fact]
    public void ReadBatteryBankMultipleTimesReadsSequentialLines()
    {
        StringReader reader = new("12\n34\n56");

        BatteryBank? batteryBank1 = BatteryBank.ReadBatteryBank(reader);
        BatteryBank? batteryBank2 = BatteryBank.ReadBatteryBank(reader);
        BatteryBank? batteryBank3 = BatteryBank.ReadBatteryBank(reader);

        Assert.NotNull(batteryBank1);
        Assert.Equal(12, batteryBank1.TotalJoltage);
        Assert.NotNull(batteryBank2);
        Assert.Equal(34, batteryBank2.TotalJoltage);
        Assert.NotNull(batteryBank3);
        Assert.Equal(56, batteryBank3.TotalJoltage);
    }

    [Fact]
    public void ReadBatteryBankAtEndOfStreamReturnsNull()
    {
        StringReader reader = new("12");
        _ = BatteryBank.ReadBatteryBank(reader);

        BatteryBank? batteryBank = BatteryBank.ReadBatteryBank(reader);

        Assert.Null(batteryBank);
    }

    [Theory]
    [InlineData("1234567890", 90)]
    [InlineData("9876543210987654321", 99)]
    [InlineData("123456789012345", 95)]
    public void TotalJoltageWithLongInputReturnsCorrectValue(string bank, int expectedJoltage)
    {
        BatteryBank batteryBank = new(bank);

        int joltage = batteryBank.TotalJoltage;

        Assert.Equal(expectedJoltage, joltage);
    }

    [Theory]
    [InlineData("999", 99)]
    [InlineData("9999", 99)]
    [InlineData("99999", 99)]
    public void TotalJoltageWithAllNinesReturnsNinetyNine(string bank, int expectedJoltage)
    {
        BatteryBank batteryBank = new(bank);

        int joltage = batteryBank.TotalJoltage;

        Assert.Equal(expectedJoltage, joltage);
    }

    [Theory]
    [InlineData("000", 0)]
    [InlineData("0000", 0)]
    [InlineData("00000", 0)]
    public void TotalJoltageWithAllZerosReturnsZero(string bank, int expectedJoltage)
    {
        BatteryBank batteryBank = new(bank);

        int joltage = batteryBank.TotalJoltage;

        Assert.Equal(expectedJoltage, joltage);
    }

    [Theory]
    [InlineData("918", 98)]
    [InlineData("819", 89)]
    [InlineData("189", 89)]
    [InlineData("891", 91)]
    public void TotalJoltageSelectsLeftmostLargestDigit(string bank, int expectedJoltage)
    {
        BatteryBank batteryBank = new(bank);

        int joltage = batteryBank.TotalJoltage;

        Assert.Equal(expectedJoltage, joltage);
    }

    [Theory]
    [InlineData("9182", 98)]
    [InlineData("1928", 98)]
    [InlineData("1298", 98)]
    [InlineData("1289", 89)]
    public void TotalJoltageWithFourDigitsSelectsCorrectPair(string bank, int expectedJoltage)
    {
        BatteryBank batteryBank = new(bank);

        int joltage = batteryBank.TotalJoltage;

        Assert.Equal(expectedJoltage, joltage);
    }

    [Fact]
    public void RecordEqualityWithSameValueReturnsTrue()
    {
        BatteryBank batteryBank1 = new("123");
        BatteryBank batteryBank2 = new("123");

        bool areEqual = batteryBank1.Equals(batteryBank2);

        Assert.True(areEqual);
    }

    [Fact]
    public void RecordEqualityWithDifferentValueReturnsFalse()
    {
        BatteryBank batteryBank1 = new("123");
        BatteryBank batteryBank2 = new("456");

        bool areEqual = batteryBank1.Equals(batteryBank2);

        Assert.False(areEqual);
    }
}
