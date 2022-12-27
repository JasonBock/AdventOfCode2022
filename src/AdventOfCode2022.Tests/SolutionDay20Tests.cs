using AdventOfCode2022.Day20;
using Xunit;

namespace AdventOfCode2022.Tests;

public static class SolutionDay20Tests
{
    [Fact]
    public static void GetGroveCoordinateSum()
    {
        var coordinates = new[]
        {
            "1",
            "2",
            "-3",
            "3",
            "-2",
            "0",
            "4"
        };

        var coordinateSum = SolutionDay20.GetGroveCoordinateSum(coordinates);
        Assert.Equal(3, coordinateSum);
    }

    [Fact]
    public static void GetGroveCoordinateSumWithDecryptionKey()
    {
        var coordinates = new[]
        {
            "1",
            "2",
            "-3",
            "3",
            "-2",
            "0",
            "4"
        };

        var coordinateSum = SolutionDay20.GetGroveCoordinateSumWithDecryptionKey(coordinates);
        Assert.Equal(1623178306L, coordinateSum);
    }
}