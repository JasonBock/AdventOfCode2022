using AdventOfCode2022.Day6;
using BenchmarkDotNet.Attributes;
using System.IO;

namespace AdventOfCode2022.Performance;

[MemoryDiagnoser]
public class Day6Tests
{
    private readonly string buffer;

    public Day6Tests() => 
        this.buffer = File.ReadAllText("Day6Input.txt");

    [Benchmark(Baseline = true)]
    public int GetEndIndexOfFirstMessage() =>
        SolutionDay6.GetEndIndexOfFirstMessage(this.buffer);

    [Benchmark]
    public int GetEndIndexOfFirstMessageBitTwiddling() =>
        SolutionDay6.GetEndIndexOfFirstMessageBitTwiddling(this.buffer);
}