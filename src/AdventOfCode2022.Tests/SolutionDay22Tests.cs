using AdventOfCode2022.Day22;
using System.Collections.Immutable;
using System.Diagnostics;
using Xunit;

namespace AdventOfCode2022.Tests;

public static class SolutionDay22Tests
{
    [Fact]
    public static void CreateNotes()
    {
        var notes = new Notes(new[]
        {
            "        ...#",
            "        .#..",
            "        #...",
            "        ....",
            "...#.......#",
            "........#...",
            "..#....#....",
            "..........#.",
            "        ...#....",
            "        .....#..",
            "        .#......",
            "        ......#.",
            "",
            "10R5L5R10L4R5L5"
        });

        Assert.Equal("10R5L5R10L4R5L5", notes.Path);
        Assert.Equal(12, notes.Tiles.Length);
        Assert.Equal("        ......#.", notes.Tiles[0]);
        Assert.Equal("        ...#", notes.Tiles[^1]);
        Assert.Equal(new Location(8, 11), notes.StartingLocation);
        Assert.Equal(Direction.Right, notes.StartingDirection);
        Assert.Equal(new Location(7, 6), notes.CurrentLocation);
        Assert.Equal(Direction.Right, notes.CurrentDirection);
        Assert.Equal(6032, notes.Password);
    }

    [Fact]
    public static void CreateCubeNotes()
    {
        static (Location nextLocation, Direction nextDirection) NextMapping(Location currentLocation, Direction currentDirection, ImmutableArray<string> tiles)
        {
            // There are 14 cases that will move to a new face.

            if (currentLocation.X == 11 && currentLocation.Y >= 8 && currentLocation.Y <= 11 && currentDirection == Direction.Right)
            {
                // Case 1 - moving right from 1, goes left on 12
                return (new Location(15, int.Abs((currentLocation.Y % 4) - 3)), Direction.Left);
            }
            else if (currentLocation.X >= 8 && currentLocation.X <= 11 && currentLocation.Y == 11 && currentDirection == Direction.Up)
            {
                // Case 2 - moving up from 2, goes down on 5
                return (new Location(int.Abs((currentLocation.X % 4) - 3), 7), Direction.Down);
            }
            else if (currentLocation.X == 8 && currentLocation.Y >= 8 && currentLocation.Y <= 11 && currentDirection == Direction.Left)
            {
                // Case 3 - moving left from 3, goes down on 4
                return (new Location(int.Abs((currentLocation.Y % 4) - 3) + 4, 7), Direction.Down);
            }
            else if (currentLocation.X >= 4 && currentLocation.X <= 7 && currentLocation.Y == 7 && currentDirection == Direction.Up)
            {
                // Case 4 - moving up from 4, goes right on 3
                return (new Location(8, int.Abs((currentLocation.X % 4) - 3) + 8), Direction.Right);
            }
            else if (currentLocation.X >= 0 && currentLocation.X <= 3 && currentLocation.Y == 7 && currentDirection == Direction.Up)
            {
                // Case 5 - moving up from 5, goes down on 2
                return (new Location(int.Abs((currentLocation.X % 4) - 3) + 8, 11), Direction.Down);
            }
            else if (currentLocation.X == 0 && currentLocation.Y >= 4 && currentLocation.Y <= 7 && currentDirection == Direction.Left)
            {
                // Case 6 - moving left from 6, goes up on 11
                return (new Location(int.Abs(currentLocation.Y % 4) + 12, 0), Direction.Up);
            }
            else if (currentLocation.X >= 0 && currentLocation.X <= 3 && currentLocation.Y == 4 && currentDirection == Direction.Down)
            {
                // Case 7 - moving down from 7, goes up on 10
                return (new Location(int.Abs((currentLocation.X % 4) - 3) + 8, 0), Direction.Up);
            }
            else if (currentLocation.X >= 4 && currentLocation.X <= 7 && currentLocation.Y == 4 && currentDirection == Direction.Down)
            {
                // Case 8 - moving down from 8, goes right on 9
                return (new Location(8, currentLocation.X % 4), Direction.Right);
            }
            else if (currentLocation.X == 8 && currentLocation.Y >= 0 && currentLocation.Y <= 3 && currentDirection == Direction.Left)
            {
                // Case 9 - moving left from 9, goes up on 8
                return (new Location((currentLocation.Y % 4) + 4, 4), Direction.Up);
            }
            else if (currentLocation.X >= 8 && currentLocation.X <= 11 && currentLocation.Y == 0 && currentDirection == Direction.Down)
            {
                // Case 10 - moving down from 10, goes up on 7
                return (new Location(int.Abs((currentLocation.X % 4) - 3), 4), Direction.Up);
            }
            else if (currentLocation.X >= 12 && currentLocation.X <= 15 && currentLocation.Y == 0 && currentDirection == Direction.Down)
            {
                // Case 11 - moving down from 11, goes right on 6
                return (new Location(0, (currentLocation.X % 4) + 4), Direction.Right);
            }
            else if (currentLocation.X == 15 && currentLocation.Y >= 0 && currentLocation.Y <= 3 && currentDirection == Direction.Right)
            {
                // Case 12 - moving right from 12, goes left on 1
                return (new Location(11, int.Abs((currentLocation.Y % 4) - 3) + 8), Direction.Left);
            }
            else if (currentLocation.X >= 12 && currentLocation.X <= 15 && currentLocation.Y == 3 && currentDirection == Direction.Up)
            {
                // Case 13 - moving up from 13, goes left on 14
                return (new Location(11, (currentLocation.X % 4) + 4), Direction.Left);
            }
            else if (currentLocation.X == 11 && currentLocation.Y >= 4 && currentLocation.Y <= 7 && currentDirection == Direction.Right)
            {
                // Case 14 - moving right from 14, goes down on 13
                return (new Location((currentLocation.Y % 4) + 12, 3), Direction.Down);
            }

            // Otherwise, it will just move on the current face.
            // Note that we don't have to mod-restrict the moves anymore.
            return (currentDirection switch
            {
                Direction.Right => new Location(currentLocation.X + 1, currentLocation.Y),
                Direction.Left => new Location(currentLocation.X - 1, currentLocation.Y),
                Direction.Up => new Location(currentLocation.X, currentLocation.Y + 1),
                Direction.Down => new Location(currentLocation.X, currentLocation.Y - 1),
                _ => throw new UnreachableException()
            }, currentDirection);
        }

        var notes = new CubeNotes(new[]
        {
            "        ...#",
            "        .#..",
            "        #...",
            "        ....",
            "...#.......#",
            "........#...",
            "..#....#....",
            "..........#.",
            "        ...#....",
            "        .....#..",
            "        .#......",
            "        ......#.",
            "",
            "10R5L5R10L4R5L5"
        }, NextMapping);

        Assert.Equal("10R5L5R10L4R5L5", notes.Path);
        Assert.Equal(12, notes.Tiles.Length);
        Assert.Equal("        ......#.", notes.Tiles[0]);
        Assert.Equal("        ...#", notes.Tiles[^1]);
        Assert.Equal(new Location(8, 11), notes.StartingLocation);
        Assert.Equal(Direction.Right, notes.StartingDirection);
        Assert.Equal(new Location(6, 7), notes.CurrentLocation);
        Assert.Equal(Direction.Up, notes.CurrentDirection);
        Assert.Equal(5031, notes.Password);
    }
}