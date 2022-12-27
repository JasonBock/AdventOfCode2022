using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2022.Day22;

public static class SolutionDay22
{
    public static long GetPassword(string[] input)
    {
        var notes = new Notes(input);
        return notes.Password;
    }

    public static long GetCubePassword(string[] input, Func<Location, Direction, ImmutableArray<string>, (Location nextLocation, Direction nextDirection)> mapping)
    {
        var notes = new CubeNotes(input, mapping);
        return notes.Password;
    }
}

public sealed class Notes
{
    public Notes(string[] input)
    {
        static IEnumerable<(int? tileMoves, char? turn)> VisitPath(string path)
        {
            var currentIndex = 0;
            var currentNumber = new List<char>();

            while (true)
            {
                if (currentIndex == path.Length)
                {
                    if (currentNumber.Count > 0)
                    {
                        yield return (int.Parse(currentNumber.ToArray()), null);
                    }

                    break;
                }
                else if (path[currentIndex] == 'R' || path[currentIndex] == 'L')
                {
                    if (currentNumber.Count > 0)
                    {
                        yield return (int.Parse(currentNumber.ToArray()), null);
                        currentNumber.Clear();
                    }

                    yield return (null, path[currentIndex]);
                }
                else
                {
                    currentNumber.Add(path[currentIndex]);
                }

                currentIndex++;
            }
        }

        // Parse the input.
        // I'm reversing it because it makes more sense to me to do it that way.
        this.Tiles = input[0..^2].Reverse().ToImmutableArray();
        this.Path = input[^1];

        // Set the starting and current points.
        this.StartingLocation = new Location(this.Tiles[0].IndexOf(this.Tiles[0].First(_ => _ != ' ')), this.Tiles.Length - 1);
        this.StartingDirection = Direction.Right;

        this.CurrentLocation = this.StartingLocation;
        this.CurrentDirection = this.StartingDirection;

        // Visit the path
        foreach (var (tileMoves, turn) in VisitPath(this.Path))
        {
            if (turn is not null)
            {
                // We change our direction.
                this.CurrentDirection = turn switch
                {
                    'R' => (Direction)(((int)this.CurrentDirection + 1) % 4),
                    'L' => (Direction)(((int)this.CurrentDirection + 3) % 4),
                    _ => throw new UnreachableException()
                };
            }
            else
            {
                // We move until we hit a wall or the coast is clear..
                for (var i = 0; i < tileMoves; i++)
                {
                    static Location FindNextLocation(ImmutableArray<string> tiles, Location currentLocation, Func<ImmutableArray<string>, Location, Location> generator)
                    {
                        var nextLocation = currentLocation;

                        while (true)
                        {
                            nextLocation = generator(tiles, nextLocation);

                            if (nextLocation.X < tiles[nextLocation.Y].Length &&
                                tiles[nextLocation.Y][nextLocation.X] != ' ')
                            {
                                // We won't go out of bounds on a line,
                                // and it won't end up in blank space, this is good.
                                return nextLocation;
                            }
                        }
                    }

                    // Remember to wrap around,
                    // but, if the move was up or down, gotta be careful,
                    // because you may move to a line where the X location doesn't exist
                    // (because the line is too short) or it's in "blank" space
                    // A move to the right needs to ignore white space, but it'll always
                    // at least avoid an index out of bounds issue.
                    var nextLocation = this.CurrentDirection switch
                    {
                        Direction.Right => FindNextLocation(this.Tiles, this.CurrentLocation,
                            (tiles, location) => new Location((location.X + 1) % tiles[location.Y].Length, location.Y)),
                        Direction.Left => FindNextLocation(this.Tiles, this.CurrentLocation, 
                            (tiles, location) => new Location((location.X + tiles[location.Y].Length - 1) % tiles[location.Y].Length, location.Y)),
                        Direction.Up => FindNextLocation(this.Tiles, this.CurrentLocation, 
                            (tiles, location) => new Location(location.X, (location.Y + 1) % tiles.Length)),
                        Direction.Down => FindNextLocation(this.Tiles, this.CurrentLocation,
                            (tiles, location) => new Location(location.X, (location.Y + tiles.Length - 1) % tiles.Length)),
                        _ => throw new UnreachableException()
                    };

                    var nextTile = this.Tiles[nextLocation.Y][nextLocation.X];

                    if (nextTile == '#')
                    {
                        // We hit a wall, so we stop.
                        break;
                    }
                    else
                    {
                        // Move to the next location.
                        this.CurrentLocation = nextLocation;
                    }
                }
            }
        }

        // Compute the password.
        this.Password = (1000L * (this.Tiles.Length - this.CurrentLocation.Y)) + (4L * (this.CurrentLocation.X + 1)) +
            (this.CurrentDirection == Direction.Right ? 0L :
                this.CurrentDirection == Direction.Down ? 1L :
                this.CurrentDirection == Direction.Left ? 2L : 3L);
    }

    public ImmutableArray<string> Tiles { get; }
    public string Path { get; }
    public Location CurrentLocation { get; }
    public Direction CurrentDirection { get; }
    public Location StartingLocation { get; }
    public Direction StartingDirection { get; }
    public long Password { get; }
}

public sealed class CubeNotes
{
    // The mapping function should look at the current location and direction,
    // and determine if it's going to move to another face of the cube,
    // or just move on the current face.
    // No matter what, it'll return what the next location and direction would be
    // IF it would go that way. It's up to this instance
    // to determine if we hit a wall or not.
    // Note that it's assumed that the mapping function
    // will return valid locations and directions - i.e.
    // a "next location" should not go beyond the length of a string,
    // or end up in empty space.
    public CubeNotes(string[] input, Func<Location, Direction, ImmutableArray<string>, (Location nextLocation, Direction nextDirection)> nextMapping)
    {
        static IEnumerable<(int? tileMoves, char? turn)> VisitPath(string path)
        {
            var currentIndex = 0;
            var currentNumber = new List<char>();

            while (true)
            {
                if (currentIndex == path.Length)
                {
                    if (currentNumber.Count > 0)
                    {
                        yield return (int.Parse(currentNumber.ToArray()), null);
                    }

                    break;
                }
                else if (path[currentIndex] == 'R' || path[currentIndex] == 'L')
                {
                    if (currentNumber.Count > 0)
                    {
                        yield return (int.Parse(currentNumber.ToArray()), null);
                        currentNumber.Clear();
                    }

                    yield return (null, path[currentIndex]);
                }
                else
                {
                    currentNumber.Add(path[currentIndex]);
                }

                currentIndex++;
            }
        }

        // Parse the input.
        // I'm reversing it because it makes more sense to me to do it that way.
        this.Tiles = input[0..^2].Reverse().ToImmutableArray();
        this.Path = input[^1];
        this.NextMapping = nextMapping;

        // Set the starting and current points.
        this.StartingLocation = new Location(this.Tiles[^1].IndexOf(this.Tiles[0].First(_ => _ != ' ')), this.Tiles.Length - 1);
        this.StartingDirection = Direction.Right;

        this.CurrentLocation = this.StartingLocation;
        this.CurrentDirection = this.StartingDirection;

        // Visit the path
        foreach (var (tileMoves, turn) in VisitPath(this.Path))
        {
            if (turn is not null)
            {
                // We change our direction.
                this.CurrentDirection = turn switch
                {
                    'R' => (Direction)(((int)this.CurrentDirection + 1) % 4),
                    'L' => (Direction)(((int)this.CurrentDirection + 3) % 4),
                    _ => throw new UnreachableException()
                };
            }
            else
            {
                // We move until we hit a wall or the coast is clear.
                for (var i = 0; i < tileMoves; i++)
                {
                    var (nextLocation, nextDirection) = this.NextMapping(this.CurrentLocation, this.CurrentDirection, this.Tiles);

                    var nextTile = this.Tiles[nextLocation.Y][nextLocation.X];

                    if (nextTile == '#')
                    {
                        // We hit a wall, so we stop.
                        break;
                    }
                    else
                    {
                        // Move to the next location
                        // and change the direction if necesary.
                        this.CurrentLocation = nextLocation;
                        this.CurrentDirection = nextDirection;
                    }
                }
            }
        }

        // Compute the password.
        this.Password = (1000L * (this.Tiles.Length - this.CurrentLocation.Y)) + (4L * (this.CurrentLocation.X + 1)) +
            (this.CurrentDirection == Direction.Right ? 0L :
                this.CurrentDirection == Direction.Down ? 1L :
                this.CurrentDirection == Direction.Left ? 2L : 3L);
    }

    public ImmutableArray<string> Tiles { get; }
    public string Path { get; }
    public Func<Location, Direction, ImmutableArray<string>, (Location nextLocation, Direction nextDirection)> NextMapping { get; }
    public Location CurrentLocation { get; }
    public Direction CurrentDirection { get; }
    public Location StartingLocation { get; }
    public Direction StartingDirection { get; }
    public long Password { get; }
}


public enum Direction { Right = 0, Down, Left, Up }
public record struct Location(int X, int Y);