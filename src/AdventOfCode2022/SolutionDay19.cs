using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022.Day19;

public static class SolutionDay19
{
    public static int GetTotalQualityLevel(Blueprint[] blueprints)
    {
        var totalQualityLevel = 0;

        for (var i = 0; i < blueprints.Length; i++)
        {
            var blueprint = blueprints[i];
            var maximumGeodeLevel = SolutionDay19.GetMaximumGeodeLevel(blueprint, 24);
            totalQualityLevel += maximumGeodeLevel * (i + 1);
        }

        return totalQualityLevel;
    }

    public static int GetMaximumGeodesMultiplication(Blueprint[] blueprints)
    {
        var totalQualityLevel = 1;

        for (var i = 0; i < blueprints.Length; i++)
        {
            var blueprint = blueprints[i];
            var maximumGeodeLevel = SolutionDay19.GetMaximumGeodeLevel(blueprint, 32);
            totalQualityLevel *= maximumGeodeLevel;
        }

        return totalQualityLevel;
    }

    public static int GetMaximumGeodeLevel(Blueprint blueprint, int minutes)
    {
        static IEnumerable<Resources> MakeNewResources(Resources resources, Blueprint blueprint)
        {
            // Can I make an ore robot?
            if (resources.Ore >= blueprint.OreRobotCost)
            {
                // Yes, I can.
                var newResources = new Resources(resources.OreRobots, resources.ClayRobots, resources.ObsidianRobots, resources.GeodeRobots,
                    resources.NewOreRobots + 1, resources.NewClayRobots, resources.NewObsidianRobots, resources.NewGeodeRobots,
                    resources.Ore - blueprint.OreRobotCost, resources.Clay, resources.Obsidian, resources.Geode);
                yield return newResources;
                // And now see if it can do even more of that kind.
                MakeNewResources(newResources, blueprint);
            }

            // Can I make a clay robot?
            if (resources.Ore >= blueprint.ClayRobotCost)
            {
                // Yes, I can.
                var newResources = new Resources(resources.OreRobots, resources.ClayRobots, resources.ObsidianRobots, resources.GeodeRobots,
                    resources.NewOreRobots, resources.NewClayRobots + 1, resources.NewObsidianRobots, resources.NewGeodeRobots,
                    resources.Ore - blueprint.ClayRobotCost, resources.Clay, resources.Obsidian, resources.Geode);
                yield return newResources;
                // And now see if it can do even more of that kind.
                MakeNewResources(newResources, blueprint);
            }

            // Can I make an obsidian robot?
            if (resources.Ore >= blueprint.ObsidianRobotCost.ore && resources.Clay >= blueprint.ObsidianRobotCost.clay)
            {
                // Yes, I can.
                var newResources = new Resources(resources.OreRobots, resources.ClayRobots, resources.ObsidianRobots, resources.GeodeRobots,
                    resources.NewOreRobots, resources.NewClayRobots, resources.NewObsidianRobots + 1, resources.NewGeodeRobots,
                    resources.Ore - blueprint.ObsidianRobotCost.ore, resources.Clay - blueprint.ObsidianRobotCost.clay, resources.Obsidian, resources.Geode);
                yield return newResources;
                // And now see if it can do even more of that kind.
                MakeNewResources(newResources, blueprint);
            }

            // Can I make an geode robot?
            if (resources.Ore >= blueprint.GeodeRobotCost.ore && resources.Obsidian >= blueprint.GeodeRobotCost.obsidian)
            {
                // Yes, I can.
                var newResources = new Resources(resources.OreRobots, resources.ClayRobots, resources.ObsidianRobots, resources.GeodeRobots,
                    resources.NewOreRobots, resources.NewClayRobots, resources.NewObsidianRobots, resources.NewGeodeRobots + 1,
                    resources.Ore - blueprint.GeodeRobotCost.ore, resources.Clay, resources.Obsidian - blueprint.GeodeRobotCost.obsidian, resources.Geode);
                yield return newResources;
                // And now see if it can do even more of that kind.
                MakeNewResources(newResources, blueprint);
            }

            // Always return the current one.
            yield return resources;
        }

        var resources = new List<Resources>
        {
            new Resources(1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
        };

        for (var m = 1; m <= minutes; m++)
        {
            var newResources = new List<Resources>();

            foreach (var resource in resources)
            {
                newResources.AddRange(MakeNewResources(resource, blueprint));
            }

            foreach (var newResource in newResources)
            {
                newResource.Update();
            }

            resources = newResources;

            if (resources.Count > 75_000)
            {
                resources = resources
                    .OrderByDescending(_ => _.Geode)
                    .ThenByDescending(_ => _.GeodeRobots)
                    .ThenByDescending(_ => _.Obsidian)
                    .ThenByDescending(_ => _.ObsidianRobots)
                    .ThenByDescending(_ => _.Clay)
                    .ThenByDescending(_ => _.ClayRobots)
                    .ThenByDescending(_ => _.Ore)
                    .ThenByDescending(_ => _.OreRobots)
                    .Take(75_000).ToList();
            }
        }

        return resources.Max(_ => _.Geode);
    }
}

public record struct Blueprint(int OreRobotCost, int ClayRobotCost, (int ore, int clay) ObsidianRobotCost, (int ore, int obsidian) GeodeRobotCost);

public sealed class Resources
{
    public Resources(int oreRobots, int clayRobots, int obsidianRobots, int geodeRobots,
        int newOreRobots, int newClayRobots, int newObsidianRobots, int newGeodeRobots,
        int ore, int clay, int obsidian, int geode)
    {
        this.OreRobots = oreRobots;
        this.ClayRobots = clayRobots;
        this.ObsidianRobots = obsidianRobots;
        this.GeodeRobots = geodeRobots;
        this.NewOreRobots = newOreRobots;
        this.NewClayRobots = newClayRobots;
        this.NewObsidianRobots = newObsidianRobots;
        this.NewGeodeRobots = newGeodeRobots;
        this.Ore = ore;
        this.Clay = clay;
        this.Obsidian = obsidian;
        this.Geode = geode;
    }

    public void Update()
    {
        // Update resource from robots that exist - e.g. Ore += OreRobots
        this.Ore += this.OreRobots;
        this.Clay += this.ClayRobots;
        this.Obsidian += this.ObsidianRobots;
        this.Geode += this.GeodeRobots;

        // Add robots from the "new" pile - e.g. OreRobots += NewOreRobots
        this.OreRobots += this.NewOreRobots;
        this.ClayRobots += this.NewClayRobots;
        this.ObsidianRobots += this.NewObsidianRobots;
        this.GeodeRobots += this.NewGeodeRobots;

        // Clear out the new robots - e.g. NewOreRobots = 0
        this.NewOreRobots = 0;
        this.NewClayRobots = 0;
        this.NewObsidianRobots = 0;
        this.NewGeodeRobots = 0;
    }

    public int OreRobots { get; private set; }
    public int ClayRobots { get; private set; }
    public int ObsidianRobots { get; private set; }
    public int GeodeRobots { get; private set; }
    public int NewOreRobots { get; private set; }
    public int NewClayRobots { get; private set; }
    public int NewObsidianRobots { get; private set; }
    public int NewGeodeRobots { get; private set; }
    public int Ore { get; private set; }
    public int Clay { get; private set; }
    public int Obsidian { get; private set; }
    public int Geode { get; private set; }
}
