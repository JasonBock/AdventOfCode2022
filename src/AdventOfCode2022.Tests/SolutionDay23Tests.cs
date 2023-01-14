using AdventOfCode2022.Day23;
using Xunit;

namespace AdventOfCode2022.Tests;

public static class SolutionDay23Tests
{
    [Fact]
    public static void GetElves() 
    {
        var input = new[]
        {
            ".....",
            "..##.",
            "..#..",
            ".....",
            "..##.",
            ".....",
        };

        var elves = SolutionDay23.GetElves(input);

        Assert.Equal(5, elves.Count);
        Assert.Contains(elves, _ => _.Location == new Location(2, 1));
        Assert.Contains(elves, _ => _.Location == new Location(3, 1));
        Assert.Contains(elves, _ => _.Location == new Location(2, 3));
        Assert.Contains(elves, _ => _.Location == new Location(2, 4));
        Assert.Contains(elves, _ => _.Location == new Location(3, 4));
    }

    [Fact]
    public static void GetProposals()
    {
        var input = new[]
        {
            ".....",
            "..##.",
            "..#..",
            ".....",
            "..##.",
            ".....",
        };

        var elves = SolutionDay23.GetElves(input);
        var proposals = new Proposals();

        foreach(var elf in elves)
        {
            proposals.MakeProposal(elf, elves);
        }

        Assert.Contains(elves, _ => _.Location == new Location(2, 4) && _.ProposedLocation!.Value == new Location(2, 5));
        Assert.Contains(elves, _ => _.Location == new Location(3, 4) && _.ProposedLocation!.Value == new Location(3, 5));
        Assert.Contains(elves, _ => _.Location == new Location(2, 3) && _.ProposedLocation!.Value == new Location(2, 2));
        Assert.Contains(elves, _ => _.Location == new Location(2, 1) && _.ProposedLocation!.Value == new Location(2, 2));
        Assert.Contains(elves, _ => _.Location == new Location(3, 1) && _.ProposedLocation!.Value == new Location(3, 2));
    }

    [Fact]
    public static void ExecuteProposals()
    {
        var input = new[]
        {
            ".....",
            "..##.",
            "..#..",
            ".....",
            "..##.",
            ".....",
        };

        var elves = SolutionDay23.GetElves(input);
        var proposals = new Proposals();

        foreach (var elf in elves)
        {
            proposals.MakeProposal(elf, elves);
        }

        SolutionDay23.ExecuteProposals(elves);

        Assert.Contains(elves, _ => _.Location == new Location(2, 1));
        Assert.Contains(elves, _ => _.Location == new Location(3, 2));
        Assert.Contains(elves, _ => _.Location == new Location(2, 3));
        Assert.Contains(elves, _ => _.Location == new Location(2, 5));
        Assert.Contains(elves, _ => _.Location == new Location(3, 5));
    }

    [Fact]
    public static void ExecuteProposalsTwice()
    {
        var input = new[]
        {
            ".....",
            "..##.",
            "..#..",
            ".....",
            "..##.",
            ".....",
        };

        var elves = SolutionDay23.GetElves(input);
        var proposals = new Proposals();

        for (var i = 0; i < 2; i++)
        {
            foreach (var elf in elves)
            {
                proposals.MakeProposal(elf, elves);
            }

            SolutionDay23.ExecuteProposals(elves);
            proposals.Rotate();

            foreach (var elf in elves)
            {
                elf.ProposedLocation = null;
            }
        }

        Assert.Contains(elves, _ => _.Location == new Location(2, 0));
        Assert.Contains(elves, _ => _.Location == new Location(4, 2));
        Assert.Contains(elves, _ => _.Location == new Location(1, 3));
        Assert.Contains(elves, _ => _.Location == new Location(2, 4));
        Assert.Contains(elves, _ => _.Location == new Location(3, 4));
    }

    [Fact]
    public static void ExecuteProposalsThreeTimes()
    {
        var input = new[]
        {
            ".....",
            "..##.",
            "..#..",
            ".....",
            "..##.",
            ".....",
        };

        var elves = SolutionDay23.GetElves(input);
        var proposals = new Proposals();

        for (var i = 0; i < 3; i++)
        {
            foreach (var elf in elves)
            {
                proposals.MakeProposal(elf, elves);
            }

            SolutionDay23.ExecuteProposals(elves);
            proposals.Rotate();

            foreach (var elf in elves)
            {
                elf.ProposedLocation = null;
            }
        }

        Assert.Contains(elves, _ => _.Location == new Location(2, 0));
        Assert.Contains(elves, _ => _.Location == new Location(4, 2));
        Assert.Contains(elves, _ => _.Location == new Location(0, 3));
        Assert.Contains(elves, _ => _.Location == new Location(4, 4));
        Assert.Contains(elves, _ => _.Location == new Location(2, 5));
    }

    [Fact]
    public static void GetEmptyGroundTiles()
    {
        var input = new[]
        {
            "....#..",
            "..###.#",
            "#...#.#",
            ".#...##",
            "#.###..",
            "##.#.##",
            ".#..#..",
        };

        var emptyGroundTiles = SolutionDay23.GetEmptyGroundTiles(input);
        Assert.Equal(110, emptyGroundTiles);
    }

    [Fact]
    public static void GetNoMovementTurn()
    {
        var input = new[]
        {
            "....#..",
            "..###.#",
            "#...#.#",
            ".#...##",
            "#.###..",
            "##.#.##",
            ".#..#..",
        };

        var noMovementTurn = SolutionDay23.GetNoMovementTurn(input);
        Assert.Equal(20, noMovementTurn);
    }

	 [Fact]
	 public static async Task GetNoMovementTurnOptimizedAsync()
	 {
		  var input = new[]
		  {
				"....#..",
				"..###.#",
				"#...#.#",
				".#...##",
				"#.###..",
				"##.#.##",
				".#..#..",
		  };

		  var noMovementTurn = await SolutionDay23.GetNoMovementTurnOptimizedAsync(input);
		  Assert.Equal(20, noMovementTurn);
	 }
}