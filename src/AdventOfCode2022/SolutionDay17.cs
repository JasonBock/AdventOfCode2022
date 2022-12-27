using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2022.Day17;

public static class SolutionDay17
{
    public static long GetRockTowerHeight(ReadOnlySpan<char> jetMoves, long rockCount)
    {
        var generator = new RockGenerator();
        var shaft = new List<Position>();
        var jetCycle = 0;

        for (var i = 0; i < rockCount; i++)
        {
            var rock = generator.Generate();
            var maxY = shaft.Count == 0 ? -1 : shaft.Max(_ => _.Y);

            // This starts the rock 2 over from the left
            // and 3 above the highest rock (or floor).
            rock.Shift(rock.Anchor.X + 2, maxY + 4);

            while (true)
            {
                var jet = jetMoves[jetCycle];
                jetCycle = (jetCycle + 1) % jetMoves.Length;

                // Need to figure out if the rock would hit the side.
                var jetMoveRock = rock.GetCopy();
                jetMoveRock.Shift(jet == '<' ? -1 : 1, 0);
                var jetMoveRockAnchor = jetMoveRock.Anchor;

                if (jetMoveRockAnchor.X >= 0 && (jetMoveRockAnchor.X + jetMoveRock.Width) <= 7 &&
                    !shaft.ContainsAny(jetMoveRock.Positions))
                {
                    // We can move it based on the jets.
                    rock = jetMoveRock;
                }

                // Now, we need to figure out if we move it down one that it would 
                // either hit any existing rocks, or it would go beneath the floor.
                if (rock.Anchor.Y == 0)
                {
                    // We would hit the floor, so stop and add these positions
                    // to the shaft.
                    shaft.AddRange(rock.Positions);
                    break;
                }
                else
                {
                    var fallRock = rock.GetCopy();
                    fallRock.Shift(0, -1);

                    if (shaft.ContainsAny(fallRock.Positions))
                    {
                        // We can't fall, we'd move into rock.
                        shaft.AddRange(rock.Positions);
                        break;
                    }
                    else
                    {
                        // We can move down one
                        rock = fallRock;
                    }
                }
            }

            if (i % 1000 == 0)
            {
                // Prune the shaft - we only keep the top 500 rows,
                // but we don't move their positions.
                shaft.RemoveAll(_ => _.Y < maxY - 500);
            }
        }

        return shaft.Max(_ => _.Y) + 1;
    }

    public static long GetRockTowerHeightOptimized(ReadOnlySpan<char> jetMoves, long rockCount)
    {
        var generator = new RockGenerator();
        var shaft = new List<Position>();
        var jetCycle = 0;
        var rockCycles = new List<(int cycle, long rockCount, long height)>();
        var addedLargeRocks = false;

        for (var i = 0L; i < rockCount; i++)
        {
            var maxY = shaft.Count == 0 ? -1 : shaft.Max(_ => _.Y);
            var rock = generator.Generate();

            // This starts the rock 2 over from the left
            // and 3 above the highest rock (or floor).
            rock.Shift(rock.Anchor.X + 2, maxY + 4);

            while (true)
            {
                var jet = jetMoves[jetCycle];
                jetCycle = (jetCycle + 1) % jetMoves.Length;

                // Need to figure out if the rock would hit the side.
                var jetMoveRock = rock.GetCopy();
                jetMoveRock.Shift(jet == '<' ? -1 : 1, 0);
                var jetMoveRockAnchor = jetMoveRock.Anchor;

                if (jetMoveRockAnchor.X >= 0 && (jetMoveRockAnchor.X + jetMoveRock.Width) <= 7 &&
                    !shaft.ContainsAny(jetMoveRock.Positions))
                {
                    // We can move it based on the jets.
                    rock = jetMoveRock;
                }

                // Now, we need to figure out if we move it down one that it would 
                // either hit any existing rocks, or it would go beneath the floor.
                if (rock.Anchor.Y == 0)
                {
                    // We would hit the floor, so stop and add these positions
                    // to the shaft.
                    shaft.AddRange(rock.Positions);
                    break;
                }
                else
                {
                    var fallRock = rock.GetCopy();
                    fallRock.Shift(0, -1);

                    if (shaft.ContainsAny(fallRock.Positions))
                    {
                        // We can't fall, we'd move into rock.
                        shaft.AddRange(rock.Positions);
                        break;
                    }
                    else
                    {
                        // We can move down one
                        rock = fallRock;
                    }
                }
            }

            if (i % 1000 == 0)
            {
                // Prune the shaft - we only keep the top 500 rows,
                // but we don't move their positions.
                shaft.RemoveAll(_ => _.Y < maxY - 500);
            }

            if (!addedLargeRocks)
            {
                if (i % jetMoves.Length == 0)
                {
                    var newMaxY = shaft.Count == 0 ? -1 : shaft.Max(_ => _.Y);
                    var cycle = (cycle: jetCycle, rockCount: i, height: newMaxY);

                    if (rockCycles.Count == 0)
                    {
                        rockCycles.Add(cycle);
                    }
                    else
                    {
                        // Look to see if there is a cycle
                        if (rockCycles.Any(_ => _.cycle == jetCycle))
                        {
                            Console.WriteLine($"Found the cycle at {i}.");

                            // A LOT of work to shift things around needs to happen.
                            var existingCycle = rockCycles.Single(_ => _.cycle == jetCycle);
                            var largeRockCycleLength = cycle.rockCount - existingCycle.rockCount;
                            var largeRockHeight = cycle.height - existingCycle.height;

                            // How many iterations are left?
                            var remainingIterations = rockCount - i;

                            // How many large rocks do we need?
                            var largeRockCycleCount = remainingIterations / largeRockCycleLength;

                            // Make our current iteration that new point.
                            i += (largeRockCycleCount * largeRockCycleLength);

                            // Shift all the rocks in the shaft up
                            var newY = largeRockCycleCount * largeRockHeight;

                            for (var p = 0; p < shaft.Count; p++)
                            {
                                shaft[p] = new Position(shaft[p].X, shaft[p].Y + newY);
                            }

                            // Set the flag where we never do any of this again.
                            addedLargeRocks = true;
                        }
                        else
                        {
                            rockCycles.Add(cycle);
                            Console.WriteLine($"Cycle count at {i} - {rockCycles.Count}.");
                        }
                    }
                }
            }
        }

        return shaft.Max(_ => _.Y) + 1;
    }
}

public static class ListExtensions
{
    public static bool ContainsAny<T>(this List<T> self, List<T> others)
    {
        foreach (var other in others)
        {
            if (self.Contains(other))
            {
                return true;
            }
        }

        return false;
    }
}
public sealed class RockGenerator
{
    private int cycle;

    public Rock Generate()
    {
        var currentCycle = this.cycle % 5;
        this.cycle++;

        return currentCycle switch
        {
            // Dash
            0 => new Rock(currentCycle, new List<Position>
                {
                    new Position { X = 0, Y = 0 },
                    new Position { X = 1, Y = 0 },
                    new Position { X = 2, Y = 0 },
                    new Position { X = 3, Y = 0 },
                }),
            // Cross
            1 => new Rock(currentCycle, new List<Position>
                {
                    new Position { X = 1, Y = 0 },
                    new Position { X = 0, Y = 1 },
                    new Position { X = 1, Y = 1 },
                    new Position { X = 2, Y = 1 },
                    new Position { X = 1, Y = 2 },
                }),
            // Reverse L
            2 => new Rock(currentCycle, new List<Position>
                {
                    new Position { X = 0, Y = 0 },
                    new Position { X = 1, Y = 0 },
                    new Position { X = 2, Y = 0 },
                    new Position { X = 2, Y = 1 },
                    new Position { X = 2, Y = 2 },
                }),
            // Pipe
            3 => new Rock(currentCycle, new List<Position>
                {
                    new Position { X = 0, Y = 0 },
                    new Position { X = 0, Y = 1 },
                    new Position { X = 0, Y = 2 },
                    new Position { X = 0, Y = 3 },
                }),
            // Square
            4 => new Rock(currentCycle, new List<Position>
                {
                    new Position { X = 0, Y = 0 },
                    new Position { X = 1, Y = 0 },
                    new Position { X = 0, Y = 1 },
                    new Position { X = 1, Y = 1 },
                }),
            _ => throw new UnreachableException()
        };
    }
}

public sealed class Rock
{
    public Rock(int cycle, List<Position> positions)
    {
        this.Cycle = cycle;
        this.Positions = new List<Position>(positions);
        this.Height = this.Positions.Max(_ => _.Y) - this.Positions.Min(_ => _.Y) + 1;
        this.Width = this.Positions.Max(_ => _.X) - this.Positions.Min(_ => _.X) + 1;
    }

    public void Shift(long xDelta, long yDelta)
    {
        for (var i = 0; i < this.Positions.Count; i++)
        {
            var position = this.Positions[i];
            this.Positions[i] = new Position(position.X + xDelta, position.Y + yDelta);
        }
    }

    public Rock GetCopy() =>
        new Rock(this.Cycle, new List<Position>(this.Positions));

    public Position Anchor => new Position(this.Positions.Min(_ => _.X), this.Positions.Min(_ => _.Y));

    public int Cycle { get; }
    public List<Position> Positions { get; }
    public long Height { get; }
    public long Width { get; }
}

public record struct Position(long X, long Y);