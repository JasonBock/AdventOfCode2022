Wow, another TSP

Each minute creates another path to choose from. For example, from the site:

Blueprint 1:
  Each ore robot costs 4 ore.
  Each clay robot costs 2 ore.
  Each obsidian robot costs 3 ore and 14 clay.
  Each geode robot costs 2 ore and 7 obsidian.

At minute 1,
    create 1 ore.
At minute 2, 
    create 1 ore.
At minute 3, 
    create 1 ore, and you can choose to make a clay robot, or bank the ore

So here's where the branching starts. One path, you have 3 ore, the other, you have 1 ore, and 1 clay robot.

You have a collection of robots.

public record struct Blueprint(int OreRobotCost, int ClayRobotCost, (int ore, int clay) ObsidianRobotCost, (int ore, int obsidian) GeodeRobotCost);

public record class Collection(int OreRobots, int ClayRobots, int ObsidianRobots, int GeodeRobots, 
    int NewOreRobots, int NewClayRobots, int NewObsidianRobots, int NewGeodeRobots,
    int Ore, int Clay, int Obsidian, int Geode)
{
    public void Update()
    {
        // Update resource from robots that exist - e.g. Ore += OreRobots
        // Add robots from the "new" pile - e.g. OreRobots += NewOreRobots
        // Clear out the new robots - e.g. NewOreRobots = 0
    }
}

You start with one Collection: where OreRobots == 1

At each minute:
* For each existing collection
    * Go through each robot type and determine if you could make one of that type. If so, 
        * make a new collection
            * One where you decide not to build that robot type, and you don't recuse with that robot type any more
            * One with the resources subtracted and that robot added as a "new" robot, and recurse to see if you can create two new collections.
    * Always return the current one, which didn't make any new robots
* For all new collections, make new resources from the robots that existed when this minute started, NOT from robots that were just built.
* There could be a method on the Collection at the end called BringRobotsOnline() - that would add robots that were made into the main collection for the next minute
* Substitute the new collections for the old ones.

* Minute 1
    * Collection
        * Robots
            * 1 ore
            * 0 clay
            * 0 obsidian
            * 0 geode
        * Resources
            * 1 ore
            * 0 clay
            * 0 obsidian
            * 0 geode
* Minute 2
    * Collection
        * Robots
            * 1 ore
            * 0 clay
            * 0 obsidian
            * 0 geode
        * Resources
            * 2 ore
            * 0 clay
            * 0 obsidian
            * 0 geode
* Minute 3
    * Collection
        * Robots
            * 1 ore
            * 0 clay
            * 0 obsidian
            * 0 geode
        * Resources
            * 3 ore
            * 0 clay
            * 0 obsidian
            * 0 geode
    * Collection
        * Robots
            * 1 ore
            * 1 clay
            * 0 obsidian
            * 0 geode
        * Resources
            * 1 ore
            * 0 clay
            * 0 obsidian
            * 0 geode
* Minute 4
    * Collection
        * Robots
            * 1 ore
            * 0 clay
            * 0 obsidian
            * 0 geode
        * Resources
            * 4 ore
            * 0 clay
            * 0 obsidian
            * 0 geode
    * Collection
        * Robots
            * 1 ore
            * 1 clay
            * 0 obsidian
            * 0 geode
        * Resources
            * 2 ore
            * 0 clay
            * 0 obsidian
            * 0 geode
    * Collection
        * Robots
            * 1 ore
            * 1 clay
            * 0 obsidian
            * 0 geode
        * Resources
            * 2 ore
            * 1 clay
            * 0 obsidian
            * 0 geode


Note that you can end up with identical configurations from different paths. It doesn't make sense to keep identical paths that have the exact same configuration, so only unique collections should be kept after each minute.