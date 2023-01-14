using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day23;

public static class SolutionDay23
{
    public static long GetEmptyGroundTiles(string[] input)
    {
        var elves = GetElves(input);
        var proposals = new Proposals();

        for (var r = 0; r < 10; r++)
        {
            // First half: proposals
            foreach (var elf in elves)
            {
                proposals.MakeProposal(elf, elves);
            }

            // Second half: execution
            SolutionDay23.ExecuteProposals(elves);

            // Rotate direction consideration
            proposals.Rotate();

            // Clear out proposals.
            foreach (var elf in elves)
            {
                elf.ProposedLocation = null;
            }
        }

        // Now find the smallest rectangle.
        var minX = elves.Min(_ => _.Location.X);
        var maxX = elves.Max(_ => _.Location.X);
        var minY = elves.Min(_ => _.Location.Y);
        var maxY = elves.Max(_ => _.Location.Y);

        // We know how many elves are in this rectangle, 
        // we just need to subtract that from the total number of locations.
        // That's the number of empty ground tiles.
        return (maxX - minX + 1) * (maxY - minY + 1) - elves.Count;
    }

    public static long GetNoMovementTurn(string[] input)
    {
        var elves = GetElves(input);
        var proposals = new Proposals();

        var turn = 1L;

        while(true)
        {
            // First half: proposals
            foreach (var elf in elves)
            {
                proposals.MakeProposal(elf, elves);
            }

            if(!elves.Any(_ => _.ProposedLocation is not null))
            {
                return turn;
            }

            // Second half: execution
            SolutionDay23.ExecuteProposals(elves);

            // Rotate direction consideration
            proposals.Rotate();

            // Clear out proposals.
            foreach (var elf in elves)
            {
                elf.ProposedLocation = null;
            }

            turn++;
        }
    }

	 public static async Task<long> GetNoMovementTurnOptimizedAsync(string[] input)
	 {
		  var elves = GetElves(input);
		  var proposals = new Proposals();

		  var turn = 1L;

		  while (true)
		  {
				var options = new ParallelOptions();

				// First half: proposals
				await Parallel.ForEachAsync(elves, async (elf, token) => proposals.MakeProposal(elf, elves));

				if (!elves.Any(_ => _.ProposedLocation is not null))
				{
					 return turn;
				}

				// Second half: execution
				SolutionDay23.ExecuteProposals(elves);

				// Rotate direction consideration
				proposals.Rotate();

				// Clear out proposals.
				foreach (var elf in elves)
				{
					 elf.ProposedLocation = null;
				}

				turn++;
		  }
	 }
	 
	 public static void ExecuteProposals(ImmutableList<Elf> elves)
    {
        foreach (var elf in elves)
        {
            if (elf.ProposedLocation is not null &&
                !elves.Any(_ => _ != elf && _.ProposedLocation == elf.ProposedLocation))
            {
                elf.Location = elf.ProposedLocation.Value;
            }
        }
    }

    public static ImmutableList<Elf> GetElves(string[] input)
    {
        var elves = ImmutableList.CreateBuilder<Elf>();

        var l = 0;

        foreach (var line in input.Reverse())
        {
            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] == '#')
                {
                    elves.Add(new Elf(new Location(i, l)));
                }
            }

            l++;
        }

        return elves.ToImmutable();
    }
}

public sealed class Elf
{
    public Elf(Location location) =>
        this.Location = location;

    public Location Location { get; set; }
    public Location? ProposedLocation { get; set; }
}

public record struct Location(long X, long Y);

public enum Direction { North, South, West, East }

public sealed class Proposals
{
    public Proposals() =>
        this.Direction = Direction.North;

    public void MakeProposal(Elf elf, ImmutableList<Elf> elves)
    {
        // If there are no elves around the current elf, it doesn't propose a new move.
        if (!elves.Any(_ => _.Location == new Location(elf.Location.X, elf.Location.Y + 1)) &&
            !elves.Any(_ => _.Location == new Location(elf.Location.X + 1, elf.Location.Y + 1)) &&
            !elves.Any(_ => _.Location == new Location(elf.Location.X + 1, elf.Location.Y)) &&
            !elves.Any(_ => _.Location == new Location(elf.Location.X + 1, elf.Location.Y - 1)) &&
            !elves.Any(_ => _.Location == new Location(elf.Location.X, elf.Location.Y - 1)) &&
            !elves.Any(_ => _.Location == new Location(elf.Location.X - 1, elf.Location.Y - 1)) &&
            !elves.Any(_ => _.Location == new Location(elf.Location.X - 1, elf.Location.Y)) &&
            !elves.Any(_ => _.Location == new Location(elf.Location.X - 1, elf.Location.Y + 1)))
        {
            elf.ProposedLocation = null;
        }
        else
        {
            var currentDirection = this.Direction;

            // If we never break out of this loop, the elf doesn't move.
            for (var d = 0; d < 4; d++)
            {
                if (currentDirection == Direction.North)
                {
                    // N, NE, and NW
                    if (!elves.Any(_ => _.Location == new Location(elf.Location.X, elf.Location.Y + 1)) &&
                        !elves.Any(_ => _.Location == new Location(elf.Location.X + 1, elf.Location.Y + 1)) &&
                        !elves.Any(_ => _.Location == new Location(elf.Location.X - 1, elf.Location.Y + 1)))
                    {
                        // Go north
                        elf.ProposedLocation = new Location(elf.Location.X, elf.Location.Y + 1);
                        break;
                    }
                }
                else if (currentDirection == Direction.South)
                {
                    // S, SE, and SW
                    if (!elves.Any(_ => _.Location == new Location(elf.Location.X, elf.Location.Y - 1)) &&
                        !elves.Any(_ => _.Location == new Location(elf.Location.X + 1, elf.Location.Y - 1)) &&
                        !elves.Any(_ => _.Location == new Location(elf.Location.X - 1, elf.Location.Y - 1)))
                    {
                        // Go south
                        elf.ProposedLocation = new Location(elf.Location.X, elf.Location.Y - 1);
                        break;
                    }
                }
                else if (currentDirection == Direction.West)
                {
                    // W, NW, and SW
                    if (!elves.Any(_ => _.Location == new Location(elf.Location.X - 1, elf.Location.Y)) &&
                        !elves.Any(_ => _.Location == new Location(elf.Location.X - 1, elf.Location.Y + 1)) &&
                        !elves.Any(_ => _.Location == new Location(elf.Location.X - 1, elf.Location.Y - 1)))
                    {
                        // Go west
                        elf.ProposedLocation = new Location(elf.Location.X - 1, elf.Location.Y);
                        break;
                    }
                }
                else
                {
                    // E, NE and SE
                    if (!elves.Any(_ => _.Location == new Location(elf.Location.X + 1, elf.Location.Y)) &&
                        !elves.Any(_ => _.Location == new Location(elf.Location.X + 1, elf.Location.Y + 1)) &&
                        !elves.Any(_ => _.Location == new Location(elf.Location.X + 1, elf.Location.Y - 1)))
                    {
                        // Go east
                        elf.ProposedLocation = new Location(elf.Location.X + 1, elf.Location.Y);
                        break;
                    }
                }

                currentDirection = (Direction)(((int)currentDirection + 1) % 4);
            }
        }
    }

    public void Rotate() =>
        this.Direction = (Direction)(((int)this.Direction + 1) % 4);

    private Direction Direction { get; set; }
}