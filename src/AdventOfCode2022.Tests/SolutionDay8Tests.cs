using AdventOfCode2022.Day8;
using Xunit;

namespace AdventOfCode2022.Tests;

public static class SolutionDay8Tests
{
    [Fact]
    public static void GetGrid() 
    {
        var input = new[]
        {
            "392",
            "409",
            "178",
        };

        var grid = SolutionDay8.GetGrid(input);

        Assert.Equal(3, grid[0, 0]);
        Assert.Equal(9, grid[0, 1]);
        Assert.Equal(2, grid[0, 2]);

        Assert.Equal(4, grid[1, 0]);
        Assert.Equal(0, grid[1, 1]);
        Assert.Equal(9, grid[1, 2]);

        Assert.Equal(1, grid[2, 0]);
        Assert.Equal(7, grid[2, 1]);
        Assert.Equal(8, grid[2, 2]);
    }

    [Fact]
    public static void GetVisibleTreeCount()
    {
        var input = new[]
        {
            "30373",
            "25512",
            "65332",
            "33549",
            "35390",
        };

        Assert.Equal(21, SolutionDay8.GetVisibleTreeCount(input));
    }

    [Fact]
    public static void GetHigestScenicScore()
    {
        var input = new[]
        {
            "30373",
            "25512",
            "65332",
            "33549",
            "35390",
        };

        Assert.Equal(8, SolutionDay8.GetHigestScenicScore(input));
    }
}