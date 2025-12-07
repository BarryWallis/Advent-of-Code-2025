using System.Numerics;

namespace Day2b.Tests;

/// <summary>
/// Unit tests for the <see cref="BigIntegerExtensions"/> class.
/// </summary>
public class BigIntegerExtensionsTests
{
    [Theory]
    [InlineData("1234")]
    [InlineData("5678")]
    [InlineData("9012")]
    [InlineData("1357")]
    public void IsValidWithNoRepeatingPatternReturnsTrue(string input)
    {
        BigInteger value = BigInteger.Parse(input);

        bool result = value.IsValid();

        Assert.True(result);
    }

    [Theory]
    [InlineData("1212")]
    [InlineData("5656")]
    [InlineData("9999")]
    [InlineData("1010")]
    public void IsValidWithRepeatingTwoDigitPatternReturnsFalse(string input)
    {
        BigInteger value = BigInteger.Parse(input);

        bool result = value.IsValid();

        Assert.False(result);
    }

    [Theory]
    [InlineData("123123")]
    [InlineData("456456")]
    [InlineData("789789")]
    public void IsValidWithRepeatingThreeDigitPatternReturnsFalse(string input)
    {
        BigInteger value = BigInteger.Parse(input);

        bool result = value.IsValid();

        Assert.False(result);
    }

    [Theory]
    [InlineData("11")]
    [InlineData("22")]
    [InlineData("33")]
    [InlineData("99")]
    public void IsValidWithRepeatingOneDigitPatternReturnsFalse(string input)
    {
        BigInteger value = BigInteger.Parse(input);

        bool result = value.IsValid();

        Assert.False(result);
    }

    [Theory]
    [InlineData("1111")]
    [InlineData("2222")]
    [InlineData("5555")]
    public void IsValidWithAllSameDigitsReturnsFalse(string input)
    {
        BigInteger value = BigInteger.Parse(input);

        bool result = value.IsValid();

        Assert.False(result);
    }

    [Theory]
    [InlineData("12")]
    [InlineData("23")]
    [InlineData("98")]
    [InlineData("45")]
    public void IsValidWithTwoDifferentDigitsReturnsTrue(string input)
    {
        BigInteger value = BigInteger.Parse(input);

        bool result = value.IsValid();

        Assert.True(result);
    }

    [Theory]
    [InlineData("123")]
    [InlineData("456")]
    [InlineData("789")]
    [InlineData("100")]
    public void IsValidWithThreeDigitsReturnsTrue(string input)
    {
        BigInteger value = BigInteger.Parse(input);

        bool result = value.IsValid();

        Assert.True(result);
    }

    [Theory]
    [InlineData("1")]
    [InlineData("5")]
    [InlineData("9")]
    public void IsValidWithSingleDigitReturnsTrue(string input)
    {
        BigInteger value = BigInteger.Parse(input);

        bool result = value.IsValid();

        Assert.True(result);
    }

    [Fact]
    public void IsValidWithZeroReturnsTrue()
    {
        BigInteger value = BigInteger.Zero;

        bool result = value.IsValid();

        Assert.True(result);
    }

    [Theory]
    [InlineData("12341234")]
    [InlineData("56785678")]
    public void IsValidWithRepeatingFourDigitPatternReturnsFalse(string input)
    {
        BigInteger value = BigInteger.Parse(input);

        bool result = value.IsValid();

        Assert.False(result);
    }

    [Theory]
    [InlineData("12345678")]
    [InlineData("98765432")]
    [InlineData("10203040")]
    public void IsValidWithEightUniqueDigitsReturnsTrue(string input)
    {
        BigInteger value = BigInteger.Parse(input);

        bool result = value.IsValid();

        Assert.True(result);
    }

    [Fact]
    public void IsValidWithLargeRepeatingPatternReturnsFalse()
    {
        string pattern = "123456";
        string repeating = pattern + pattern + pattern;
        BigInteger value = BigInteger.Parse(repeating);

        bool result = value.IsValid();

        Assert.False(result);
    }

    [Fact]
    public void IsValidWithLargeNonRepeatingNumberReturnsTrue()
    {
        string largeNumber = "12345678901234567890123456789012345678";
        BigInteger value = BigInteger.Parse(largeNumber);

        bool result = value.IsValid();

        Assert.True(result);
    }

    [Fact]
    public void IsValidWithZerosInRepeatingPatternReturnsFalse()
    {
        BigInteger value = BigInteger.Parse("100100");

        bool result = value.IsValid();

        Assert.False(result);
    }

    [Fact]
    public void IsValidWithZerosButNoRepeatingPatternReturnsTrue()
    {
        BigInteger value = BigInteger.Parse("10203040");

        bool result = value.IsValid();

        Assert.True(result);
    }

    [Theory]
    [InlineData("123123123")]
    [InlineData("456456456")]
    public void IsValidWithThreeRepetitionsOfThreeDigitPatternReturnsFalse(string input)
    {
        BigInteger value = BigInteger.Parse(input);

        bool result = value.IsValid();

        Assert.False(result);
    }

    [Fact]
    public void IsValidWithComplexLargeNumberReturnsCorrectResult()
    {
        string nonRepeating = "123456789987654321234567899876543210";
        BigInteger value = BigInteger.Parse(nonRepeating);

        bool result = value.IsValid();

        Assert.True(result);
    }

    [Theory]
    [InlineData("1234512345")]
    [InlineData("9876598765")]
    public void IsValidWithRepeatingFiveDigitPatternReturnsFalse(string input)
    {
        BigInteger value = BigInteger.Parse(input);

        bool result = value.IsValid();

        Assert.False(result);
    }

    [Theory]
    [InlineData("121212")]
    [InlineData("343434")]
    public void IsValidWithTripleRepeatingTwoDigitPatternReturnsFalse(string input)
    {
        BigInteger value = BigInteger.Parse(input);

        bool result = value.IsValid();

        Assert.False(result);
    }
}
