using AdventOfCode2022.Day24;
using AdventOfCode2022.Day3;
using BenchmarkDotNet.Attributes;
using System.IO;

namespace AdventOfCode2022.Performance;

[MemoryDiagnoser]
public class Day24Tests
{
	 private readonly string[] input;

	 public Day24Tests() =>
		  this.input = File.ReadAllLines("Day24Input.txt");

	 [Benchmark(Baseline = true)]
	 public long GetMinimumMinutes() =>
		  SolutionDay24.GetMinimumMinutes(this.input);

	 [Benchmark]
	 public long GetMinimumMinutesOptimized() =>
		  SolutionDay24.GetMinimumMinutesOptimized(this.input);
}