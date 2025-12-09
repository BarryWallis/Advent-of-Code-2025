namespace Day3a.Tests;

/// <summary>
/// Unit tests for the <see cref="BatteryBank"/> helper methods.
/// </summary>
public class BatteryBankHelperTests
{
    [Theory]
    [InlineData("0", 0)]
    [InlineData("5", 0)]
    [InlineData("9", 0)]
    public void LeftmostLargestDigitIndexWithSingleDigitReturnsZero(string value, int expected)
    {
        int result = BatteryBank.LeftmostLargestDigitIndex(value);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("12", 1)]
    [InlineData("21", 0)]
    [InlineData("05", 1)]
    [InlineData("50", 0)]
    [InlineData("99", 0)]
    public void LeftmostLargestDigitIndexWithTwoDigitsReturnsCorrectIndex(string value, int expected)
    {
        int result = BatteryBank.LeftmostLargestDigitIndex(value);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("123", 2)]
    [InlineData("321", 0)]
    [InlineData("213", 2)]
    [InlineData("132", 1)]
    public void LeftmostLargestDigitIndexWithThreeDigitsReturnsCorrectIndex(string value, int expected)
    {
        int result = BatteryBank.LeftmostLargestDigitIndex(value);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("1234", 3)]
    [InlineData("4321", 0)]
    [InlineData("1324", 3)]
    [InlineData("2413", 1)]
    public void LeftmostLargestDigitIndexWithFourDigitsReturnsCorrectIndex(string value, int expected)
    {
        int result = BatteryBank.LeftmostLargestDigitIndex(value);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("98765", 0)]
    [InlineData("12345", 4)]
    [InlineData("54321", 0)]
    [InlineData("13579", 4)]
    public void LeftmostLargestDigitIndexWithFiveDigitsReturnsCorrectIndex(string value, int expected)
    {
        int result = BatteryBank.LeftmostLargestDigitIndex(value);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("0000", 0)]
    [InlineData("1111", 0)]
    [InlineData("9999", 0)]
    public void LeftmostLargestDigitIndexWithAllSameDigitsReturnsZero(string value, int expected)
    {
        int result = BatteryBank.LeftmostLargestDigitIndex(value);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("0123456789", 9)]
    [InlineData("9876543210", 0)]
    public void LeftmostLargestDigitIndexWithAllTenDigitsReturnsCorrectIndex(string value, int expected)
    {
        int result = BatteryBank.LeftmostLargestDigitIndex(value);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("9145", 0)]
    [InlineData("1924", 1)]
    [InlineData("1249", 3)]
    [InlineData("1239", 3)]
    public void LeftmostLargestDigitIndexReturnsLeftmostWhenMultipleNinesExist(string value, int expected)
    {
        int result = BatteryBank.LeftmostLargestDigitIndex(value);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("9123", 0)]
    [InlineData("1923", 1)]
    [InlineData("1293", 2)]
    [InlineData("1239", 3)]
    public void LeftmostLargestDigitIndexWithNineAtDifferentPositionsReturnsCorrectIndex(string value, int expected)
    {
        int result = BatteryBank.LeftmostLargestDigitIndex(value);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("0999", 1)]
    [InlineData("9099", 0)]
    [InlineData("9909", 0)]
    [InlineData("9990", 0)]
    public void LeftmostLargestDigitIndexWithZeroAndNinesReturnsLeftmostNineIndex(string value, int expected)
    {
        int result = BatteryBank.LeftmostLargestDigitIndex(value);

        Assert.Equal(expected, result);
    }
}

