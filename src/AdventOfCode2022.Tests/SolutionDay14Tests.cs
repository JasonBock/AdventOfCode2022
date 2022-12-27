using AdventOfCode2022.Day14;
using Xunit;

namespace AdventOfCode2022.Tests;

public static class SolutionDay14Tests
{
    [Fact]
    public static void GetLineEndpoints()
    {
        var rocks = SolutionDay14.GetRocks("498,4 -> 498,6 -> 496,6");

        Assert.Equal(5, rocks.Count);
        Assert.Contains((498, 4), rocks);
        Assert.Contains((498, 5), rocks);
        Assert.Contains((498, 6), rocks);
        Assert.Contains((497, 6), rocks);
        Assert.Contains((496, 6), rocks);
    }

    [Fact]
    public static void GetMap()
    {
        var scan = new[]
        {
            "498,4 -> 498,6 -> 496,6",
            "503,4 -> 502,4 -> 502,9 -> 494,9",
        };

        var map = SolutionDay14.GetMap(scan);
        Assert.Equal(20, map.Count);
        Assert.Contains(new Location(498, 4, '#'), map);
        Assert.Contains(new Location(498, 5, '#'), map);
        Assert.Contains(new Location(498, 6, '#'), map);
        Assert.Contains(new Location(497, 6, '#'), map);
        Assert.Contains(new Location(496, 6, '#'), map);
        Assert.Contains(new Location(503, 4, '#'), map);
        Assert.Contains(new Location(502, 4, '#'), map);
        Assert.Contains(new Location(502, 5, '#'), map);
        Assert.Contains(new Location(502, 6, '#'), map);
        Assert.Contains(new Location(502, 7, '#'), map);
        Assert.Contains(new Location(502, 8, '#'), map);
        Assert.Contains(new Location(502, 9, '#'), map);
        Assert.Contains(new Location(501, 9, '#'), map);
        Assert.Contains(new Location(500, 9, '#'), map);
        Assert.Contains(new Location(499, 9, '#'), map);
        Assert.Contains(new Location(498, 9, '#'), map);
        Assert.Contains(new Location(497, 9, '#'), map);
        Assert.Contains(new Location(496, 9, '#'), map);
        Assert.Contains(new Location(495, 9, '#'), map);
        Assert.Contains(new Location(494, 9, '#'), map);
    }

    [Fact]
    public static void GetSandBlockCountForAbyssPath()
    {
        var scan = new[]
        {
            "498,4 -> 498,6 -> 496,6",
            "503,4 -> 502,4 -> 502,9 -> 494,9",
        };

        var sandBlocks = SolutionDay14.GetSandBlockCountForAbyssPath(scan);

        Assert.Equal(24, sandBlocks);
    }

    [Fact]
    public static void GetSandBlockCountForFloor()
    {
        var scan = new[]
        {
            "498,4 -> 498,6 -> 496,6",
            "503,4 -> 502,4 -> 502,9 -> 494,9",
        };

        var sandBlocks = SolutionDay14.GetSandBlockCountForFloor(scan);

        Assert.Equal(93, sandBlocks);
    }
}