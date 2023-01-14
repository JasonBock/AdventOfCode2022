using AdventOfCode2022.Day23;
using BenchmarkDotNet.Attributes;
using System.IO;
using System.Threading.Tasks;

namespace AdventOfCode2022.Performance;

[MemoryDiagnoser]
public class Day23Tests
{
	 private readonly string[] input;

	 public Day23Tests() =>
		  this.input = new[]
		  {
				"....#..",
				"..###.#",
				"#...#.#",
				".#...##",
				"#.###..",
				"##.#.##",
				".#..#..",
		  };

	 [Benchmark(Baseline = true)]
	 public long GetNoMovementTurn() =>
		  SolutionDay23.GetNoMovementTurn(this.input);

	 [Benchmark]
	 public async Task<long> GetNoMovementTurnOptimizedAsync() =>
		  await SolutionDay23.GetNoMovementTurnOptimizedAsync(this.input);
}