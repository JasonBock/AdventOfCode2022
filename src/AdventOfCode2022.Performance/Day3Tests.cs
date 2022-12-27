using AdventOfCode2022.Day3;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2022.Performance;

[MemoryDiagnoser]
public class Day3Tests
{
    [Benchmark(Baseline = true)]
    //[Arguments("QJRBMDMtRDCtJzBtJMfjNjhwvmNDvwjLVVgh", "TPSNNPZGTjgmSmvfjL", "bPlpZZbpsTlTsWprpGFCJtRtzMNdMMBBcWnJQB")]
    //[Arguments("aaaaaB", "bbbbbB", "cccccB")]
    //[Arguments(
    //    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaB",
    //    "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbB",
    //    "ccccccccccccccccccccccccccccccccccccccccccccccccccB")]
    [Arguments(
        "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaB",
        "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbB",
        "ccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccB")]
    public int GetCommomElfRucksackItem(string rucksack0, string rucksack1, string rucksack2) =>
        SolutionDay3.GetCommomElfRucksackItem(rucksack0, rucksack1, rucksack2);

    [Benchmark]
    //[Arguments("QJRBMDMtRDCtJzBtJMfjNjhwvmNDvwjLVVgh", "TPSNNPZGTjgmSmvfjL", "bPlpZZbpsTlTsWprpGFCJtRtzMNdMMBBcWnJQB")]
    //[Arguments("aaaaaB", "bbbbbB", "cccccB")]
    //[Arguments(
    //    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaB",
    //    "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbB",
    //    "ccccccccccccccccccccccccccccccccccccccccccccccccccB")]
    [Arguments(
        "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaB",
        "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbB",
        "ccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccB")]
    public int GetCommomElfRucksackItemImproved(string rucksack0, string rucksack1, string rucksack2) =>
        SolutionDay3.GetCommomElfRucksackItemImproved(rucksack0, rucksack1, rucksack2);

    [Benchmark]
    //[Arguments("QJRBMDMtRDCtJzBtJMfjNjhwvmNDvwjLVVgh", "TPSNNPZGTjgmSmvfjL", "bPlpZZbpsTlTsWprpGFCJtRtzMNdMMBBcWnJQB")]
    //[Arguments("aaaaaB", "bbbbbB", "cccccB")]
    //[Arguments(
    //    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaB",
    //    "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbB",
    //    "ccccccccccccccccccccccccccccccccccccccccccccccccccB")]
    [Arguments(
        "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaB",
        "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbB",
        "ccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccB")]
    public int GetCommomElfRucksackItemImprovedMaxNoArray(string rucksack0, string rucksack1, string rucksack2) =>
        SolutionDay3.GetCommomElfRucksackItemImprovedMaxNoArray(rucksack0, rucksack1, rucksack2);
}