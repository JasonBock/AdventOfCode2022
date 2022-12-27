using AdventOfCode2022.Day1;
using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2022.Performance;

[MemoryDiagnoser]
public class Day1Tests
{
    [Benchmark]
    [ArgumentsSource(nameof(Calories))]
    public int Run(string[] input) =>
        SolutionDay1.Run(input);

    [Benchmark]
    [ArgumentsSource(nameof(Calories))]
    public int RunFaster(string[] input) =>
        SolutionDay1.RunFaster(input);

    public static IEnumerable<object[]> Calories()
    {
        yield return new[] { "1000", "2000", "3000", "", "4000", "", "5000", "6000", "", "7000", "8000", "9000", "", "10000" };
        yield return File.ReadAllLines("Day1Input.txt");
    }
}