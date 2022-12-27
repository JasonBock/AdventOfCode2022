using AdventOfCode2022.Day12;
using Xunit;

namespace AdventOfCode2022.Tests;

public static class SolutionDay12Tests
{
    [Fact]
    public static void GetStartAndEnd() 
    {
        var map = new[]
        {
            "Sabqponm",
            "abcryxxl",
            "accszExk",
            "acctuvwj",
            "abdefghi",
        };

        var (me, destination) = SolutionDay12.GetStartAndEnd(map);

        Assert.Equal(new Location(0, 0, 'S'), me);
        Assert.Equal(new Location(5, 2, 'E'), destination);
    }

    [Fact]
    public static void GetAllStartsAndEnd()
    {
        var map = new[]
        {
            "Sabqponm",
            "abcryxxl",
            "accszExk",
            "acctuvwj",
            "abdefghi",
        };

        var (starts, destination) = SolutionDay12.GetAllStartsAndEnd(map);

        Assert.Equal(6, starts.Count);
        Assert.Contains(new Location(0, 0, 'a'), starts);
        Assert.Contains(new Location(1, 0, 'a'), starts);
        Assert.Contains(new Location(0, 1, 'a'), starts);
        Assert.Contains(new Location(0, 2, 'a'), starts);
        Assert.Contains(new Location(0, 3, 'a'), starts);
        Assert.Contains(new Location(0, 4, 'a'), starts);
        Assert.Equal(new Location(5, 2, 'E'), destination);
    }

    [Fact]
    public static void GetFewestSteps()
    {
        var map = new[]
        {
            "Sabqponm",
            "abcryxxl",
            "accszExk",
            "acctuvwj",
            "abdefghi",
        };

        var score = SolutionDay12.GetFewestSteps(map);
        Assert.Equal(31, score);
    }

    [Fact]
    public static void GetFewestStepsFromAllStarts()
    {
        var map = new[]
        {
            "Sabqponm",
            "abcryxxl",
            "accszExk",
            "acctuvwj",
            "abdefghi",
        };

        var score = SolutionDay12.GetFewestStepsFromAllStarts(map);
        Assert.Equal(29, score);
    }
}