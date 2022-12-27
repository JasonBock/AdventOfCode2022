using AdventOfCode2022.Day25;
using System.Numerics;
using Xunit;

namespace AdventOfCode2022.Tests;

public static class SolutionDay25Tests
{
    [Theory]
    [InlineData("1", 1)]
    [InlineData("2", 2)]
    [InlineData("1=", 3)]
    [InlineData("1-", 4)]
    [InlineData("10", 5)]
    [InlineData("11", 6)]
    [InlineData("12", 7)]
    [InlineData("2=", 8)]
    [InlineData("2-", 9)]
    [InlineData("20", 10)]
    [InlineData("1=0", 15)]
    [InlineData("1-0", 20)]
    [InlineData("1=11-2", 2022)]
    [InlineData("1-0---0", 12345)]
    [InlineData("1121-1110-1=0", 314159265)]
    [InlineData("1=-0-2", 1747)]
    [InlineData("12111", 906)]
    [InlineData("2=0=", 198)]
    [InlineData("21", 11)]
    [InlineData("2=01", 201)]
    [InlineData("111", 31)]
    [InlineData("20012", 1257)]
    [InlineData("112", 32)]
    [InlineData("1=-1=", 353)]
    [InlineData("1-12", 107)]
    [InlineData("122", 37)]
    public static void TranslateToDecimal(string snafu, long expectedValue) =>
        Assert.Equal(new BigInteger(expectedValue), SolutionDay25.TranslateToDecimal(snafu));

    [Theory]
    [InlineData(1, "1")]
    [InlineData(2, "2")]
    [InlineData(3, "1=")]
    [InlineData(4, "1-")]
    [InlineData(5, "10")]
    [InlineData(6, "11")]
    [InlineData(7, "12")]
    [InlineData(8, "2=")]
    [InlineData(9, "2-")]
    [InlineData(10, "20")]
    [InlineData(15, "1=0")]
    [InlineData(20, "1-0")]
    [InlineData(2022, "1=11-2")]
    [InlineData(12345, "1-0---0")]
    [InlineData(314159265, "1121-1110-1=0")]
    [InlineData(1747, "1=-0-2")]
    [InlineData(906, "12111")]
    [InlineData(198, "2=0=")]
    [InlineData(11, "21")]
    [InlineData(201, "2=01")]
    [InlineData(31, "111")]
    [InlineData(1257, "20012")]
    [InlineData(32, "112")]
    [InlineData(353, "1=-1=")]
    [InlineData(107, "1-12")]
    [InlineData(37, "122")]
    public static void TranslateToSnafu(long value, string expectedValue) =>
        Assert.Equal(expectedValue, SolutionDay25.TranslateToSnafu(new BigInteger(value)));

    [Fact]
    public static void GetFuelRequirementsSum()
    {
        var input = new[]
        {
            "1=-0-2",
            "12111",
            "2=0=",
            "21",
            "2=01",
            "111",
            "20012",
            "112",
            "1=-1=",
            "1-12",
            "12",
            "1=",
            "122"
        };

        var (decimalSum, snafuSum) = SolutionDay25.GetFuelRequirementsSum(input);

        Assert.Equal(4890, decimalSum);
        Assert.Equal("2=-1=0", snafuSum);
    }
}