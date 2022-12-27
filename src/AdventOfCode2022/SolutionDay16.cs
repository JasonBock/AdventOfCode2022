using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022.Day16;

public static class SolutionDay16
{
    public static long GetMaximumPressure(string[] scans)
    {
        static IEnumerable<Path> MakeNewPaths(Path path, string[] connections)
        {
            foreach (var connection in connections)
            {
                yield return new Path(connection, new List<OpenedRoom>(path.OpenedRooms), path.CurrentPressure);
            }
        }

        var caves = new Dictionary<string, Room>();

        foreach (var scan in scans)
        {
            var room = Room.Parse(scan);
            caves.Add(room.Name, room);
        }

        var roomsWithPressure = caves.Values.Count(_ => _.Rate > 0);

        var possiblePaths = new List<Path> { new("AA", new List<OpenedRoom>(), 0) };
        var completedPaths = new List<Path>();

        for (var m = 1u; m < 31; m++)
        {
            for (var p = possiblePaths.Count - 1; p >= 0; p--)
            {
                var path = possiblePaths[p];

                if (path.OpenedRooms.Count == roomsWithPressure)
                {
                    completedPaths.Add(path);
                }
                else
                {
                    // Make new paths for the all the possible connections
                    possiblePaths.AddRange(MakeNewPaths(path, caves[path.CurrentRoom].Connections));

                    if (caves[path.CurrentRoom].Rate != 0 &&
                        !path.OpenedRooms.Any(_ => _.Name == path.CurrentRoom))
                    {
                        // Add new path path where we add to OpenedRooms
                        var openedPath = new Path(path.CurrentRoom,
                            new List<OpenedRoom>(path.OpenedRooms) { new OpenedRoom(path.CurrentRoom, 30 - m) },
                            path.CurrentPressure + (caves[path.CurrentRoom].Rate * (30 - m)));
                        possiblePaths.Add(openedPath);
                    }
                }

                possiblePaths.RemoveAt(p);
            }

            // Complete guess. We just start pruning lists, keeping only the top X

            if (possiblePaths.Count > 50_000)
            {
                possiblePaths = possiblePaths.OrderByDescending(_ => _.CurrentPressure)
                    .Take(50_000).ToList();
            }
        }

        if (completedPaths.Count > 0)
        {
            return completedPaths.Max(_ => (long)_.CurrentPressure);
        }
        else
        {
            return possiblePaths.Max(_ => (long)_.CurrentPressure);
        }
    }

    public static long GetMaximumPressureWithElephantHelp(string[] scans)
    {
        static IEnumerable<ElephantPath> MakeNewPaths(ElephantPath path, string[] humanConnections, string[] elephantConnections)
        {
            foreach (var humanConnection in humanConnections)
            {
                foreach (var elephantConnection in elephantConnections)
                {
                    yield return new ElephantPath(humanConnection, elephantConnection,
                        new List<OpenedRoom>(path.OpenedRooms), path.CurrentPressure);
                }
            }
        }

        var caves = new Dictionary<string, Room>();

        foreach (var scan in scans)
        {
            var room = Room.Parse(scan);
            caves.Add(room.Name, room);
        }

        var roomsWithPressure = caves.Values.Count(_ => _.Rate > 0);

        var possiblePaths = new List<ElephantPath> { new("AA", "AA", new List<OpenedRoom>(), 0) };
        var completedPaths = new List<ElephantPath>();

        for (var m = 1u; m < 27; m++)
        {
            for (var p = possiblePaths.Count - 1; p >= 0; p--)
            {
                var path = possiblePaths[p];

                if (path.OpenedRooms.Count == roomsWithPressure)
                {
                    completedPaths.Add(path);
                }
                else
                {
                    // Make new paths for the all the possible human and elephant rooms
                    possiblePaths.AddRange(MakeNewPaths(path,
                        caves[path.HumanCurrentRoom].Connections,
                        caves[path.ElephantCurrentRoom].Connections));

                    var humanCouldOpen = caves[path.HumanCurrentRoom].Rate != 0 &&
                        !path.OpenedRooms.Any(_ => _.Name == path.HumanCurrentRoom);
                    var elephantCouldOpen = caves[path.ElephantCurrentRoom].Rate != 0 &&
                        !path.OpenedRooms.Any(_ => _.Name == path.ElephantCurrentRoom);

                    // Both the human and the elephant could be in zero-pressure rooms, 
                    // or in rooms that have been opened
                    if(humanCouldOpen && elephantCouldOpen)
                    {
                        // If they're in the same room,
                        // only one opens it, and the other leaves,
                        // this is (humanOpen, elephantConnections) and (humanConnections, elephantOpens)
                        if(path.HumanCurrentRoom != path.ElephantCurrentRoom)
                        {
                            // Add new path path where we add to OpenedRooms BOTH
                            // the human and elephant rooms
                            var openedPath = new ElephantPath(path.HumanCurrentRoom, path.ElephantCurrentRoom,
                                new List<OpenedRoom>(path.OpenedRooms)
                                {
                                    new OpenedRoom(path.HumanCurrentRoom, 26 - m),
                                    new OpenedRoom(path.ElephantCurrentRoom, 26 - m),
                                },
                                path.CurrentPressure +
                                    (caves[path.HumanCurrentRoom].Rate * (26 - m)) + (caves[path.ElephantCurrentRoom].Rate * (26 - m)));
                            possiblePaths.Add(openedPath);
                        }

                        // It's possible that one opens
                        // and other just leaves their room.
                        foreach (var elephantConnection in caves[path.ElephantCurrentRoom].Connections)
                        {
                            var openedHumanPath = new ElephantPath(path.HumanCurrentRoom, elephantConnection,
                                new List<OpenedRoom>(path.OpenedRooms)
                                {
                                    new OpenedRoom(path.HumanCurrentRoom, 26 - m),
                                },
                                path.CurrentPressure + (caves[path.HumanCurrentRoom].Rate * (26 - m)));
                            possiblePaths.Add(openedHumanPath);
                        }

                        foreach (var humanConnection in caves[path.HumanCurrentRoom].Connections)
                        {
                            var openedElephantPath = new ElephantPath(humanConnection, path.ElephantCurrentRoom,
                                new List<OpenedRoom>(path.OpenedRooms)
                                {
                                    new OpenedRoom(path.ElephantCurrentRoom, 26 - m),
                                },
                                path.CurrentPressure + (caves[path.ElephantCurrentRoom].Rate * (26 - m)));
                            possiblePaths.Add(openedElephantPath);
                        }
                    }
                    else if(humanCouldOpen)
                    {
                        // Add new paths where we add to OpenedRooms 
                        // the human room being opened,
                        // and the new elephant destination
                        foreach(var elephantConnection in caves[path.ElephantCurrentRoom].Connections)
                        {
                            var openedPath = new ElephantPath(path.HumanCurrentRoom, elephantConnection,
                                new List<OpenedRoom>(path.OpenedRooms)
                                {
                                    new OpenedRoom(path.HumanCurrentRoom, 26 - m),
                                },
                                path.CurrentPressure + (caves[path.HumanCurrentRoom].Rate * (26 - m)));
                            possiblePaths.Add(openedPath);
                        }
                    }
                    else if (elephantCouldOpen)
                    {
                        // Add new paths where we add to OpenedRooms 
                        // the elephant room being opened,
                        // and the new human destination
                        foreach (var humanConnection in caves[path.HumanCurrentRoom].Connections)
                        {
                            var openedPath = new ElephantPath(humanConnection, path.ElephantCurrentRoom,
                                new List<OpenedRoom>(path.OpenedRooms)
                                {
                                    new OpenedRoom(path.ElephantCurrentRoom, 26 - m),
                                },
                                path.CurrentPressure + (caves[path.ElephantCurrentRoom].Rate * (26 - m)));
                            possiblePaths.Add(openedPath);
                        }
                    }
                }

                possiblePaths.RemoveAt(p);
            }

            // Complete guess. We just start pruning lists, keeping only the top X

            if (possiblePaths.Count > 50_000)
            {
                possiblePaths = possiblePaths.OrderByDescending(_ => _.CurrentPressure)
                    .Take(50_000).ToList();
            }
        }

        if (completedPaths.Count > 0)
        {
            return completedPaths.Max(_ => (long)_.CurrentPressure);
        }
        else
        {
            return possiblePaths.Max(_ => (long)_.CurrentPressure);
        }
    }
}

public record struct OpenedRoom(string Name, uint Minute);

public record Path(string CurrentRoom, List<OpenedRoom> OpenedRooms, ulong CurrentPressure);

public record ElephantPath(string HumanCurrentRoom, string ElephantCurrentRoom, List<OpenedRoom> OpenedRooms, ulong CurrentPressure);

public record struct Room(string Name, ulong Rate, string[] Connections)
{
    public static Room Parse(string scan)
    {
        var parts = scan.Split(';');

        var name = parts[0][(parts[0].IndexOf(' ') + 1)..(parts[0].IndexOf(' ') + 3)];
        var rate = ulong.Parse(parts[0][(parts[0].IndexOf('=') + 1)..]);

        var connections = parts[1].Contains("tunnels") ?
            parts[1][(parts[1].IndexOf("valves") + 7)..].Split(',', StringSplitOptions.TrimEntries) :
            new[] { parts[1][(parts[1].IndexOf("valve") + 6)..] };

        return new(name, rate, connections);
    }
}