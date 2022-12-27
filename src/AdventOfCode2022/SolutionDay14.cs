using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AdventOfCode2022.Day14;

public static class SolutionDay14
{
    public static void Run() { }

    public static int GetSandBlockCountForAbyssPath(string[] scan)
    {
        var map = SolutionDay14.GetMap(scan);
        var abyss = map.Max(_ => _.Y);

        while (true)
        {
            var currentSand = new Position(500, 0);
            var stop = false;

            while (!stop)
            {
                if (currentSand.Y == abyss)
                {
                    // We've hit the end (abyss)
                    break;
                }
                // Look down.
                else if (!map.Contains(new(currentSand.X, currentSand.Y + 1, '#')) &&
                    !map.Contains(new(currentSand.X, currentSand.Y + 1, 'o')))
                {
                    // It's air, it can move there.
                    currentSand = new Position(currentSand.X, currentSand.Y + 1);
                }
                // Have to check down and to the left.
                else if (!map.Contains(new(currentSand.X - 1, currentSand.Y + 1, '#')) &&
                    !map.Contains(new(currentSand.X - 1, currentSand.Y + 1, 'o')))
                {
                    // It's air, it can move there.
                    currentSand = new Position(currentSand.X - 1, currentSand.Y + 1);
                }
                // Have to check down and to the right.
                else if (!map.Contains(new(currentSand.X + 1, currentSand.Y + 1, '#')) &&
                    !map.Contains(new(currentSand.X + 1, currentSand.Y + 1, 'o')))
                {
                    // It's air, it can move there.
                    currentSand = new Position(currentSand.X + 1, currentSand.Y + 1);
                }
                else
                {
                    // It can't go anywhere, put it where it is.
                    map.Add(new(currentSand.X, currentSand.Y, 'o'));
                    stop = true;
                }
            }

            if (currentSand.Y == abyss)
            {
                // We've hit the end (abyss)
                break;
            }
        }

        return map.Count(_ => _.Value == 'o');
    }

    public static int GetSandBlockCountForFloor(string[] scan)
    {
        var map = SolutionDay14.GetMap(scan);
        var floor = map.Max(_ => _.Y) + 2;

        while(true)
        {
            var currentSand = new Position(500, 0);
            var stop = false;

            while(!stop)
            {
                if (currentSand.Y == floor - 1)
                {
                    // It's right above the floor, put it where it is.
                    map.Add(new(currentSand.X, currentSand.Y, 'o'));
                    stop = true;
                }
                // Look down.
                else if (!map.Contains(new(currentSand.X, currentSand.Y + 1, '#')) &&
                    !map.Contains(new(currentSand.X, currentSand.Y + 1, 'o')))
                {
                    // It's air, it can move there.
                    currentSand = new Position(currentSand.X, currentSand.Y + 1);
                }
                // Have to check down and to the left.
                else if (!map.Contains(new(currentSand.X - 1, currentSand.Y + 1, '#')) &&
                    !map.Contains(new(currentSand.X - 1, currentSand.Y + 1, 'o')))
                {
                    // It's air, it can move there.
                    currentSand = new Position(currentSand.X - 1, currentSand.Y + 1);
                }
                // Have to check down and to the right.
                else if (!map.Contains(new(currentSand.X + 1, currentSand.Y + 1, '#')) && 
                    !map.Contains(new(currentSand.X + 1, currentSand.Y + 1, 'o')))
                {
                    // It's air, it can move there.
                    currentSand = new Position(currentSand.X + 1, currentSand.Y + 1);
                }
                else
                {
                    // It can't go anywhere, put it where it is.
                    map.Add(new(currentSand.X, currentSand.Y, 'o'));
                    stop = true;
                }
            }

            if (map.Contains(new(500, 0, 'o')))
            {
                // That means we're plugged.
                break;
            }
        }

        return map.Count(_ => _.Value == 'o');
    }

    public static HashSet<Location> GetMap(string[] scan)
    {
        var rocks = new HashSet<Location>();

        foreach (var input in scan)
        {
            foreach (var (rockX, rockY) in SolutionDay14.GetRocks(input))
            {
                rocks.Add(new(rockX, rockY, '#'));
            }
        }

        return rocks;
    }

    public static ImmutableHashSet<(int x, int y)> GetRocks(string input)
    {
        var ends = new List<Position>();
        var rocks = ImmutableHashSet.CreateBuilder<(int, int)>();

        foreach (var coordinate in input.Split("->"))
        {
            var parts = coordinate.Split(',');
            ends.Add(new(int.Parse(parts[0].Trim()), int.Parse(parts[1].Trim())));

            if (ends.Count > 1)
            {
                var beginning = ends[^2];
                var end = ends[^1];

                if (beginning.X == end.X)
                {
                    var yStart = int.Min(beginning.Y, end.Y);
                    for (var i = yStart; i <= yStart + int.Abs(beginning.Y - end.Y); i++)
                    {
                        rocks.Add((beginning.X, i));
                    }
                }
                else
                {
                    var xStart = int.Min(beginning.X, end.X);
                    for (var i = xStart; i <= xStart + int.Abs(beginning.X - end.X); i++)
                    {
                        rocks.Add((i, beginning.Y));
                    }
                }
            }
        }


        return rocks.ToImmutable();
    }
}

public record struct Position(int X, int Y);

public record struct Location(int X, int Y, char Value);