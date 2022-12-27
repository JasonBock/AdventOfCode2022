using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2022.Day12;

public static class SolutionDay12
{
    public static int GetFewestSteps(string[] map)
    {
        static void AddNewPath(Location destination, List<List<Location>> newPaths, List<Location> path, Location currentLocation, Location newLocation)
        {
            var difference = currentLocation.Value == 'S' ? 1 : newLocation.Value - currentLocation.Value;

            if ((newLocation == destination && currentLocation.Value == 'z') ||
                (newLocation != destination && !path.Contains(newLocation) && difference <= 1))
            {
                newPaths.Add(new List<Location>(path)
                {
                    newLocation
                });
            }
        }

        var lowestSteps = int.MaxValue;
        var (me, destination) = SolutionDay12.GetStartAndEnd(map);

        var paths = new List<List<Location>>()
        {
            new() { me }
        };

        var maxX = map[0].Length;
        var maxY = map.Length;

        while (true)
        {
            var newPaths = new List<List<Location>>();

            for (var i = paths.Count - 1; i >= 0; i--)
            {
                var path = paths[i];
                var currentLocation = path[^1];

                if (currentLocation == destination)
                {
                    // We've hit the destination with this one, just add it.
                    newPaths.Add(path);
                }
                else
                {
                    // Can we go up?
                    if (currentLocation.Y < maxY - 1)
                    {
                        var newLocation = new Location(currentLocation.X, currentLocation.Y + 1,
                            map[currentLocation.Y + 1][currentLocation.X]);

                        AddNewPath(destination, newPaths, path, currentLocation, newLocation);
                    }

                    // Can we go down?
                    if (currentLocation.Y > 0)
                    {
                        var newLocation = new Location(currentLocation.X, currentLocation.Y - 1,
                            map[currentLocation.Y - 1][currentLocation.X]);

                        AddNewPath(destination, newPaths, path, currentLocation, newLocation);
                    }

                    // Can we go left?
                    if (currentLocation.X > 0)
                    {
                        var newLocation = new Location(currentLocation.X - 1, currentLocation.Y,
                            map[currentLocation.Y][currentLocation.X - 1]);

                        AddNewPath(destination, newPaths, path, currentLocation, newLocation);
                    }

                    // Can we go right?
                    if (currentLocation.X < maxX - 1)
                    {
                        var newLocation = new Location(currentLocation.X + 1, currentLocation.Y,
                            map[currentLocation.Y][currentLocation.X + 1]);

                        AddNewPath(destination, newPaths, path, currentLocation, newLocation);
                    }
                }
            }

            if (newPaths.Count == 0)
            {
                throw new UnreachableException();
            }

            // If all the newPaths end in Destination, then we find the lowest score and break out;
            if (newPaths.All(_ => _[^1].Value == 'E'))
            {
                var shortestPath = newPaths.OrderBy(p => p.Count).Take(1).ToList()[0];
                lowestSteps = shortestPath.Count - 1;
                break;
            }
            else
            {
                // We have to prune the new paths, otherwise this will spiral out of control.
                for(var n = newPaths.Count - 1; n >= 0; n--)
                {
                    var newPath = newPaths[n];
                    var newPathEnd = newPath[^1];

                    if(newPaths.Any(_ => _ != newPath && _.Contains(newPathEnd)))
                    {
                        newPaths.RemoveAt(n);
                        continue;
                    }
                }

                paths = newPaths;
            }
        }

        return lowestSteps;
    }

    public static int GetFewestStepsFromAllStarts(string[] map)
    {
        static void AddNewPath(Location destination, List<List<Location>> newPaths, List<Location> path, Location currentLocation, Location newLocation)
        {
            var difference = currentLocation.Value == 'S' ? 1 : newLocation.Value - currentLocation.Value;

            if ((newLocation == destination && currentLocation.Value == 'z') ||
                (newLocation != destination && !path.Contains(newLocation) && difference <= 1))
            {
                newPaths.Add(new List<Location>(path)
                {
                    newLocation
                });
            }
        }

        var lowestSteps = int.MaxValue;
        var (starts, destination) = SolutionDay12.GetAllStartsAndEnd(map);
        var paths = new List<List<Location>>();

        foreach (var start in starts)
        {
            paths.Add(new() { start });
        }

        var maxX = map[0].Length;
        var maxY = map.Length;

        while (true)
        {
            var newPaths = new List<List<Location>>();

            for (var i = paths.Count - 1; i >= 0; i--)
            {
                var path = paths[i];
                var currentLocation = path[^1];

                if (currentLocation == destination)
                {
                    // We've hit the destination with this one, just add it.
                    newPaths.Add(path);
                }
                else
                {
                    // Can we go up?
                    if (currentLocation.Y < maxY - 1)
                    {
                        var newLocation = new Location(currentLocation.X, currentLocation.Y + 1,
                            map[currentLocation.Y + 1][currentLocation.X]);

                        AddNewPath(destination, newPaths, path, currentLocation, newLocation);
                    }

                    // Can we go down?
                    if (currentLocation.Y > 0)
                    {
                        var newLocation = new Location(currentLocation.X, currentLocation.Y - 1,
                            map[currentLocation.Y - 1][currentLocation.X]);

                        AddNewPath(destination, newPaths, path, currentLocation, newLocation);
                    }

                    // Can we go left?
                    if (currentLocation.X > 0)
                    {
                        var newLocation = new Location(currentLocation.X - 1, currentLocation.Y,
                            map[currentLocation.Y][currentLocation.X - 1]);

                        AddNewPath(destination, newPaths, path, currentLocation, newLocation);
                    }

                    // Can we go right?
                    if (currentLocation.X < maxX - 1)
                    {
                        var newLocation = new Location(currentLocation.X + 1, currentLocation.Y,
                            map[currentLocation.Y][currentLocation.X + 1]);

                        AddNewPath(destination, newPaths, path, currentLocation, newLocation);
                    }
                }
            }

            if (newPaths.Count == 0)
            {
                throw new UnreachableException();
            }

            // If all the newPaths end in Destination, then we find the lowest score and break out;
            if (newPaths.All(_ => _[^1].Value == 'E'))
            {
                var shortestPath = newPaths.OrderBy(p => p.Count).Take(1).ToList()[0];
                lowestSteps = shortestPath.Count - 1;
                break;
            }
            else
            {
                // We have to prune the new paths, otherwise this will spiral out of control.
                for (var n = newPaths.Count - 1; n >= 0; n--)
                {
                    var newPath = newPaths[n];
                    var newPathEnd = newPath[^1];

                    if (newPaths.Any(_ => _ != newPath && _.Contains(newPathEnd)))
                    {
                        newPaths.RemoveAt(n);
                        continue;
                    }
                }

                paths = newPaths;
            }
        }

        return lowestSteps;
    }
    public static (Location me, Location destination) GetStartAndEnd(string[] map)
    {
        var me = new Location();
        var destination = new Location();

        var foundMe = false;
        var foundDestination = false;

        for (var i = 0; i < map.Length; i++)
        {
            var line = map[i];

            if (!foundMe)
            {
                var startIndex = line.IndexOf('S');

                if (startIndex >= 0)
                {
                    me = new Location(startIndex, i, 'S');
                    foundMe = true;
                }
            }

            if (!foundDestination)
            {
                var endIndex = line.IndexOf('E');

                if (endIndex >= 0)
                {
                    destination = new Location(endIndex, i, 'E');
                    foundDestination = true;
                }
            }

            if (foundMe && foundDestination)
            {
                return (me, destination);
            }
        }

        throw new UnreachableException();
    }

    public static (List<Location> starts, Location destination) GetAllStartsAndEnd(string[] map)
    {
        var starts = new List<Location>();
        var destination = new Location();

        var foundDestination = false;

        for (var i = 0; i < map.Length; i++)
        {
            var line = map[i];

            var index = 0;

            while (true)
            {
                index = line.IndexOf('a', index);

                if (index >= 0)
                {
                    starts.Add(new Location(index, i, 'a'));
                    break;
                }
            }

            index = line.IndexOf('S');

            if (index >= 0)
            {
                starts.Add(new Location(index, i, 'a'));
            }

            if (!foundDestination)
            {
                var endIndex = line.IndexOf('E');

                if (endIndex >= 0)
                {
                    destination = new Location(endIndex, i, 'E');
                    foundDestination = true;
                }
            }
        }

        return (starts, destination);
    }
}

public record struct Location(int X, int Y, char Value);