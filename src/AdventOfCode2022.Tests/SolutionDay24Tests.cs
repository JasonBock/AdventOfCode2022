using AdventOfCode2022.Day24;
using Xunit;

namespace AdventOfCode2022.Tests;

public static class SolutionDay24Tests
{
	 [Fact]
	 public static void Parse()
	 {
		  var input = new[]
		  {
				">>.<^<",
				".<..<<",
				">v.><>",
				"<^v^^>",
		  };

		  var grid = SolutionDay24.Parse(input.Reverse().ToArray());

		  Assert.Equal(6, grid.GetLength(0));
		  Assert.Equal(4, grid.GetLength(1));

		  // Spot check
		  Assert.Single(grid[0, 0]);
		  Assert.Contains('<', grid[0, 0]);

		  Assert.Empty(grid[2, 1]);

		  Assert.Single(grid[4, 3]);
		  Assert.Contains('^', grid[4, 3]);
	 }

	 [Fact]
	 public static void GetMinimumMinutes()
	 {
		  var input = new[]
		  {
				">>.<^<",
				".<..<<",
				">v.><>",
				"<^v^^>",
		  };

		  var minimumMinutes = SolutionDay24.GetMinimumMinutes(input);
		  Assert.Equal(18, minimumMinutes);
	 }

	 [Fact]
	 public static void GetMinimumMinutesOptimized()
	 {
		  var input = new[]
		  {
				">>.<^<",
				".<..<<",
				">v.><>",
				"<^v^^>",
		  };

		  var minimumMinutes = SolutionDay24.GetMinimumMinutesOptimized(input);
		  Assert.Equal(18, minimumMinutes);
	 }

	 [Fact]
	 public static void GetMinimumMinutesOptimizedOneGrid()
	 {
		  var input = new[]
		  {
				">>.<^<",
				".<..<<",
				">v.><>",
				"<^v^^>",
		  };

		  var minimumMinutes = SolutionDay24.GetMinimumMinutesOptimizedOneGrid(input);
		  Assert.Equal(18, minimumMinutes);
	 }

	 [Fact]
	 public static void GetMinimumMinutesOptimizedOneGridUsingOneHashSet()
	 {
		  var input = new[]
		  {
				">>.<^<",
				".<..<<",
				">v.><>",
				"<^v^^>",
		  };

		  var minimumMinutes = SolutionDay24.GetMinimumMinutesOptimizedOneGridUsingOneHashSet(input);
		  Assert.Equal(18, minimumMinutes);
	 }

	 [Fact]
	 public static void UpdateGridOptimizedOneGrid()
	 {
		  var input = new[]
		  {
				">>.<^<",
				".<..<<",
				">v.><>",
				"<^v^^>",
		  };

		  var optimizedGrid = SolutionDay24.ParseOptimized(input.Reverse().ToArray());

		  for(var i = 0; i < 18; i++)
		  {
				optimizedGrid = SolutionDay24.UpdateBlizzardsOptimized(optimizedGrid);
		  }

		  var (oneGrid, xLength) = SolutionDay24.ParseOptimizedOneGrid(input.Reverse().ToArray());

		  for (var i = 0; i < 18; i++)
		  {
				SolutionDay24.UpdateBlizzardsOptimizedOneGrid(oneGrid, xLength);
		  }
	 }

	 [Fact]
	 public static void ParseOptimizedOneGridFirstRow()
	 {
		  var input = new[] { ">>.<^<" };
		  var (grid, xLength) = SolutionDay24.ParseOptimizedOneGrid(input);

		  Assert.Equal(6, xLength);
		  Assert.Equal(2, grid.Length);
		  Assert.Equal(0b0000_0001_0000_0000_0000_0010_0000_0010u, grid[0, 0]);
		  Assert.Equal(0b0000_0000_0000_0000_0000_0001_0000_0100u, grid[1, 0]);
	 }

	 [Fact]
	 public static void ParseOptimizedOneGridEvenDistribution()
	 {
		  var input = new[] { ">>.<^v.>" };
		  var (grid, xLength) = SolutionDay24.ParseOptimizedOneGrid(input);

		  Assert.Equal(8, xLength);
		  Assert.Equal(2, grid.Length);
		  Assert.Equal(0b0000_0001_0000_0000_0000_0010_0000_0010u, grid[0, 0]);
		  Assert.Equal(0b0000_0010_0000_0000_0000_1000_0000_0100u, grid[1, 0]);
	 }

	 [Fact]
	 public static void ParseOptimizedOneGridUnevenDistribution()
	 {
		  var input = new[] { ">>.<^v.>^" };
		  var (grid, xLength) = SolutionDay24.ParseOptimizedOneGrid(input);

		  Assert.Equal(9, xLength);
		  Assert.Equal(3, grid.Length);
		  Assert.Equal(0b0000_0001_0000_0000_0000_0010_0000_0010u, grid[0, 0]);
		  Assert.Equal(0b0000_0010_0000_0000_0000_1000_0000_0100u, grid[1, 0]);
		  Assert.Equal(0b0000_0000_0000_0000_0000_0000_0000_0100u, grid[2, 0]);
	 }

	 [Fact]
    public static void GetMinimumMinutesFullExpedition()
    {
        var input = new[]
        {
            ">>.<^<",
            ".<..<<",
            ">v.><>",
            "<^v^^>",
        };

        var minimumMinutes = SolutionDay24.GetMinimumMinutesFullExpedition(input);
        Assert.Equal(54, minimumMinutes);
    }
}