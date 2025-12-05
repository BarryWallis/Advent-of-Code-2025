namespace Day2a.Tests;

/// <summary>
/// Unit tests for the <see cref="BigIntegerExtensions"/> class.
/// </summary>
public class BigIntegerExtensionsTests
{
    [Fact]
    public void NumberOfDigitsWithZeroReturnsOne()
    {
        BigInteger value = BigInteger.Zero;

        int result = value.NumberOfDigits();

        Assert.Equal(1, result);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(5, 1)]
    [InlineData(9, 1)]
    public void NumberOfDigitsWithSingleDigitPositiveNumberReturnsOne(int input, int expected)
    {
        BigInteger value = new(input);

        int result = value.NumberOfDigits();

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(10, 2)]
    [InlineData(50, 2)]
    [InlineData(99, 2)]
    public void NumberOfDigitsWithTwoDigitNumberReturnsTwo(int input, int expected)
    {
        BigInteger value = new(input);

        int result = value.NumberOfDigits();

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(100, 3)]
    [InlineData(500, 3)]
    [InlineData(999, 3)]
    public void NumberOfDigitsWithThreeDigitNumberReturnsThree(int input, int expected)
    {
        BigInteger value = new(input);

        int result = value.NumberOfDigits();

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1000, 4)]
    [InlineData(5000, 4)]
    [InlineData(9999, 4)]
    public void NumberOfDigitsWithFourDigitNumberReturnsFour(int input, int expected)
    {
        BigInteger value = new(input);

        int result = value.NumberOfDigits();

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(-1, 1)]
    [InlineData(-5, 1)]
    [InlineData(-9, 1)]
    public void NumberOfDigitsWithSingleDigitNegativeNumberReturnsOne(int input, int expected)
    {
        BigInteger value = new(input);

        int result = value.NumberOfDigits();

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(-10, 2)]
    [InlineData(-50, 2)]
    [InlineData(-99, 2)]
    public void NumberOfDigitsWithTwoDigitNegativeNumberReturnsTwo(int input, int expected)
    {
        BigInteger value = new(input);

        int result = value.NumberOfDigits();

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(-100, 3)]
    [InlineData(-500, 3)]
    [InlineData(-999, 3)]
    public void NumberOfDigitsWithThreeDigitNegativeNumberReturnsThree(int input, int expected)
    {
        BigInteger value = new(input);

        int result = value.NumberOfDigits();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void NumberOfDigitsWithVeryLargeNumberReturnsCorrectCount()
    {
        string largeNumber = "123456789012345678901234567890";
        BigInteger value = BigInteger.Parse(largeNumber);

        int result = value.NumberOfDigits();

        Assert.Equal(30, result);
    }

    [Fact]
    public void NumberOfDigitsWithVeryLargeNegativeNumberReturnsCorrectCount()
    {
        string largeNumber = "-123456789012345678901234567890";
        BigInteger value = BigInteger.Parse(largeNumber);

        int result = value.NumberOfDigits();

        Assert.Equal(30, result);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(7)]
    [InlineData(9)]
    [InlineData(11)]
    [InlineData(99)]
    [InlineData(101)]
    [InlineData(int.MaxValue)]
    public void IsOddWithOddNumberReturnsTrue(int value)
    {
        bool result = value.IsOdd();

        Assert.True(result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(2)]
    [InlineData(4)]
    [InlineData(6)]
    [InlineData(8)]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(int.MaxValue - 1)]
    public void IsOddWithEvenNumberReturnsFalse(int value)
    {
        bool result = value.IsOdd();

        Assert.False(result);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-3)]
    [InlineData(-5)]
    [InlineData(-99)]
    [InlineData(int.MinValue + 1)]
    public void IsOddWithNegativeOddNumberReturnsTrue(int value)
    {
        bool result = value.IsOdd();

        Assert.True(result);
    }

    [Theory]
    [InlineData(-2)]
    [InlineData(-4)]
    [InlineData(-6)]
    [InlineData(-100)]
    [InlineData(int.MinValue)]
    public void IsOddWithNegativeEvenNumberReturnsFalse(int value)
    {
        bool result = value.IsOdd();

        Assert.False(result);
    }

    [Fact]
    public void IsValidWithMatchingHalvesReturnsTrue()
    {
        BigInteger value = new(1212);

        bool result = value.IsValid();

        Assert.True(result);
    }

    [Fact]
    public void IsValidWithNonMatchingHalvesReturnsFalse()
    {
        BigInteger value = new(1234);

        bool result = value.IsValid();

        Assert.False(result);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(9)]
    [InlineData(100)]
    [InlineData(999)]
    [InlineData(10000)]
    public void IsValidWithOddDigitCountReturnsFalse(int input)
    {
        BigInteger value = new(input);

        bool result = value.IsValid();

        Assert.False(result);
    }

    [Fact]
    public void IsValidWithZeroReturnsFalse()
    {
        BigInteger value = BigInteger.Zero;

        bool result = value.IsValid();

        Assert.False(result);
    }

    [Theory]
    [InlineData(1111)]
    [InlineData(2222)]
    [InlineData(9999)]
    [InlineData(1010)]
    [InlineData(5656)]
    public void IsValidWithFourDigitMatchingHalvesReturnsTrue(int input)
    {
        BigInteger value = new(input);

        bool result = value.IsValid();

        Assert.True(result);
    }

    [Theory]
    [InlineData(1122)]
    [InlineData(1234)]
    [InlineData(9876)]
    [InlineData(1000)]
    [InlineData(9900)]
    [InlineData(10)]
    public void IsValidWithEvenDigitNonMatchingHalvesReturnsFalse(int input)
    {
        BigInteger value = new(input);

        bool result = value.IsValid();

        Assert.False(result);
    }

    [Theory]
    [InlineData(123123)]
    [InlineData(456456)]
    [InlineData(999999)]
    [InlineData(100100)]
    public void IsValidWithSixDigitMatchingHalvesReturnsTrue(int input)
    {
        BigInteger value = new(input);

        bool result = value.IsValid();

        Assert.True(result);
    }

    [Theory]
    [InlineData(123456)]
    [InlineData(100200)]
    [InlineData(999888)]
    public void IsValidWithSixDigitNonMatchingHalvesReturnsFalse(int input)
    {
        BigInteger value = new(input);

        bool result = value.IsValid();

        Assert.False(result);
    }

    [Theory]
    [InlineData(-1212)]
    [InlineData(-1234)]
    [InlineData(-50)]
    [InlineData(-5)]
    public void IsValidWithNegativeNumbersReturnsFalse(int input)
    {
        BigInteger value = new(input);

        bool result = value.IsValid();

        Assert.False(result);
    }

    [Fact]
    public void IsValidWithLargeNumberMatchingHalvesReturnsTrue()
    {
        string largeNumber = "12341234";
        BigInteger value = BigInteger.Parse(largeNumber);

        bool result = value.IsValid();

        Assert.True(result);
    }

    [Fact]
    public void IsValidWithLargeNumberNonMatchingHalvesReturnsFalse()
    {
        string largeNumber = "12345678909876543210";
        BigInteger value = BigInteger.Parse(largeNumber);

        bool result = value.IsValid();

        Assert.False(result);
    }

    [Theory]
    [InlineData(11)]
    [InlineData(22)]
    [InlineData(99)]
    public void IsValidWithTwoIdenticalDigitsReturnsTrue(int input)
    {
        BigInteger value = new(input);

        bool result = value.IsValid();

        Assert.True(result);
    }

    [Theory]
    [InlineData(12)]
    [InlineData(21)]
    [InlineData(98)]
    public void IsValidWithTwoDifferentDigitsReturnsFalse(int input)
    {
        BigInteger value = new(input);

        bool result = value.IsValid();

        Assert.False(result);
    }

    [Fact]
    public void IsValidWithZerosInMatchingHalvesReturnsTrue()
    {
        BigInteger value = new(10001000);

        bool result = value.IsValid();

        Assert.True(result);
    }

    [Fact]
    public void IsValidWithZerosInNonMatchingHalvesReturnsFalse()
    {
        BigInteger value = new(10000001);

        bool result = value.IsValid();

        Assert.False(result);
    }

    [Fact]
    public void IsValidWithVeryLargeEvenDigitNumberWithMatchingHalvesReturnsTrue()
    {
        string largeNumber = "12345678901234567890";
        BigInteger value = BigInteger.Parse(largeNumber);

        bool result = value.IsValid();

        Assert.True(result);
    }

    [Fact]
    public void IsValidWithVeryLargeOddDigitNumberReturnsFalse()
    {
        string largeNumber = "1234567890123456789012345678901";
        BigInteger value = BigInteger.Parse(largeNumber);

        bool result = value.IsValid();

        Assert.False(result);
    }
}
