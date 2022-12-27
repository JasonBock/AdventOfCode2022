using AdventOfCode2022.Day1;
using Xunit;

namespace AdventOfCode2022.Tests;

public static class SolutionDay1Tests
{
    public static IEnumerable<object[]> Calories()
    {
        yield return new object[] { new[] { "1000", "2000", "3000", "", "4000", "", "5000", "6000", "", "7000", "8000", "9000", "", "10000" }, 24000 };
        yield return new object[] { new[] { "1000", "", "10000" }, 10000 };
        yield return new object[] { new[] { "1000", "", "10000", "" }, 10000 };
    }

    [Theory]
    [MemberData(nameof(Calories))]
    public static void Run(string[] input, int expectedValue) => 
        Assert.Equal(expectedValue, SolutionDay1.Run(input));

    [Theory]
    [MemberData(nameof(Calories))]
    public static void RunFaster(string[] input, int expectedValue) =>
        Assert.Equal(expectedValue, SolutionDay1.RunFaster(input));

    [Fact]
    public static void GetTop3Total()
    {
        var calories = new[] { "1000", "2000", "3000", "", "4000", "", "5000", "6000", "", "7000", "8000", "9000", "", "10000" };
        Assert.Equal(45000, SolutionDay1.GetTop3Total(calories));
    }
}