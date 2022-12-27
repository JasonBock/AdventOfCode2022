using AdventOfCode2022.Day19;
using Xunit;

namespace AdventOfCode2022.Tests;

public static class SolutionDay19Tests
{
    [Fact]
    public static void UpdateResources() 
    {
        var resources = new Resources(30, 40, 50, 60, 3, 4, 5, 6, 300, 400, 500, 600);
        resources.Update();

        Assert.Equal(33, resources.OreRobots);
        Assert.Equal(44, resources.ClayRobots);
        Assert.Equal(55, resources.ObsidianRobots);
        Assert.Equal(66, resources.GeodeRobots);

        Assert.Equal(330, resources.Ore);
        Assert.Equal(440, resources.Clay);
        Assert.Equal(550, resources.Obsidian);
        Assert.Equal(660, resources.Geode);

        Assert.Equal(0, resources.NewOreRobots);
        Assert.Equal(0, resources.NewClayRobots);
        Assert.Equal(0, resources.NewObsidianRobots);
        Assert.Equal(0, resources.NewGeodeRobots);
    }

    [Theory]
    [InlineData(24, 9)]
    [InlineData(32, 56)]
    public static void GetMaximumGeodeLevelBlueprint1(int minutes, int expectedValue) 
    {
        var blueprint = new Blueprint(4, 2, (3, 14), (2, 7));
        var maximumGeodeLevel = SolutionDay19.GetMaximumGeodeLevel(blueprint, minutes);
        Assert.Equal(expectedValue, maximumGeodeLevel);
    }

    [Theory]
    [InlineData(24, 12)]
    [InlineData(32, 62)]
    public static void GetMaximumGeodeLevelBlueprint2(int minutes, int expectedValue)
    {
        var blueprint = new Blueprint(2, 3, (3, 8), (3, 12));
        var maximumGeodeLevel = SolutionDay19.GetMaximumGeodeLevel(blueprint, minutes);
        Assert.Equal(expectedValue, maximumGeodeLevel);
    }

    [Fact]
    public static void GetTotalQualityLevel()
    {
        var blueprints = new Blueprint[]
        {
            new Blueprint(4, 2, (3, 14), (2, 7)),
            new Blueprint(2, 3, (3, 8), (3, 12))
        };

        var totalQualityLevel = SolutionDay19.GetTotalQualityLevel(blueprints);
        Assert.Equal(33, totalQualityLevel);
    }
}