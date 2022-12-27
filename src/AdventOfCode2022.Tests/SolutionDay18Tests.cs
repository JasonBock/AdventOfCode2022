using AdventOfCode2022.Day18;
using Xunit;

namespace AdventOfCode2022.Tests;

public static class SolutionDay18Tests
{
    [Fact]
    public static void AreCubesEqual()
    {
        var cube1 = new Cube(2, 3, 6) { IsXNegativeCovered = true };
        var cube2 = new Cube(2, 3, 6) { IsYPositiveCovered = true };

        Assert.Equal(cube1, cube2);
    }

    [Fact]
    public static void Parse()
    {
        var cube = Cube.Parse("2,3,6");

        Assert.Equal(2, cube.X);
        Assert.Equal(3, cube.Y);
        Assert.Equal(6, cube.Z);

        Assert.False(cube.IsXPositiveCovered);
        Assert.False(cube.IsYPositiveCovered);
        Assert.False(cube.IsZPositiveCovered);
        Assert.False(cube.IsXNegativeCovered);
        Assert.False(cube.IsYNegativeCovered);
        Assert.False(cube.IsZNegativeCovered);
    }

    [Fact]
    public static void GetTotalSurfaceArea() 
    {
        var coordinates = new[]
        {
            "2,2,2",
            "1,2,2",
            "3,2,2",
            "2,1,2",
            "2,3,2",
            "2,2,1",
            "2,2,3",
            "2,2,4",
            "2,2,6",
            "1,2,5",
            "3,2,5",
            "2,1,5",
            "2,3,5"
        };

        var totalSurfaceArea = SolutionDay18.GetTotalSurfaceArea(coordinates);
        Assert.Equal(64, totalSurfaceArea);
    }

    [Fact]
    public static void GetTotalSurfaceAreaWithAirBubbles()
    {
        var coordinates = new[]
        {
            "2,2,2",
            "1,2,2",
            "3,2,2",
            "2,1,2",
            "2,3,2",
            "2,2,1",
            "2,2,3",
            "2,2,4",
            "2,2,6",
            "1,2,5",
            "3,2,5",
            "2,1,5",
            "2,3,5"
        };

        var totalSurfaceArea = SolutionDay18.GetTotalSurfaceAreaWithAirBubbles(coordinates);
        Assert.Equal(58, totalSurfaceArea);
    }
}