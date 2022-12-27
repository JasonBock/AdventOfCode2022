using AdventOfCode2022.Day24;
using Xunit;

namespace AdventOfCode2022.Tests;

public static class SolutionDay24Tests
{
    [Fact]
    public static void Parse()
    {
        var input = new[]
        {
            ">>.<^<",
            ".<..<<",
            ">v.><>",
            "<^v^^>",
        };

        var grid = SolutionDay24.Parse(input.Reverse().ToArray());

        Assert.Equal(6, grid.GetLength(0));
        Assert.Equal(4, grid.GetLength(1));

        // Spot check
        Assert.Single(grid[0, 0]);
        Assert.Contains('<', grid[0, 0]);

        Assert.Empty(grid[2, 1]);

        Assert.Single(grid[4, 3]);
        Assert.Contains('^', grid[4, 3]);
    }

    [Fact]
    public static void GetMinimumMinutes()
    {
        var input = new[]
        {
            ">>.<^<",
            ".<..<<",
            ">v.><>",
            "<^v^^>",
        };

        var minimumMinutes = SolutionDay24.GetMinimumMinutes(input);
        Assert.Equal(18, minimumMinutes);
    }

    [Fact]
    public static void GetMinimumMinutesFullExpedition()
    {
        var input = new[]
        {
            ">>.<^<",
            ".<..<<",
            ">v.><>",
            "<^v^^>",
        };

        var minimumMinutes = SolutionDay24.GetMinimumMinutesFullExpedition(input);
        Assert.Equal(54, minimumMinutes);
    }
}