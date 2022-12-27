using AdventOfCode2022.Day4;
using Xunit;

namespace AdventOfCode2022.Tests;

public static class SolutionDay4Tests
{
    [Theory]
    [InlineData("2-4,6-8", 0)]
    [InlineData("2-3,4-5", 0)]
    [InlineData("5-7,7-9", 0)]
    [InlineData("2-8,3-7", 1)]
    [InlineData("6-6,4-6", 1)]
    [InlineData("2-6,4-8", 0)]
    [InlineData("6-8,2-4", 0)]
    [InlineData("4-5,2-3", 0)]
    [InlineData("7-9,5-7", 0)]
    [InlineData("3-7,2-8", 1)]
    [InlineData("4-6,6-6", 1)]
    [InlineData("4-8,2-6", 0)]
    public static void RunWithPair(string assignmentPair, int expectedResult) =>
        Assert.Equal(expectedResult, SolutionDay4.Run(new[] { assignmentPair }));

    [Theory]
    [InlineData("2-4,6-8", 0)]
    [InlineData("2-3,4-5", 0)]
    [InlineData("5-7,7-9", 1)]
    [InlineData("2-8,3-7", 1)]
    [InlineData("6-6,4-6", 1)]
    [InlineData("2-6,4-8", 1)]
    [InlineData("6-8,2-4", 0)]
    [InlineData("4-5,2-3", 0)]
    [InlineData("7-9,5-7", 1)]
    [InlineData("3-7,2-8", 1)]
    [InlineData("4-6,6-6", 1)]
    [InlineData("4-8,2-6", 1)]
    public static void RunForOverlapPair(string assignmentPair, int expectedResult) =>
        Assert.Equal(expectedResult, SolutionDay4.RunForOverlap(new[] { assignmentPair }));
}