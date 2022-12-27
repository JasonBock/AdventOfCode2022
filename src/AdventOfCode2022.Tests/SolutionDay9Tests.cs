using AdventOfCode2022.Day9;
using Xunit;

namespace AdventOfCode2022.Tests;

public static class SolutionDay9Tests
{
    [Fact]
    public static void GetTailVisitationPositionCountHardcoded() 
    {
        var input = new[]
        {
            "R 4",
            "U 4",
            "L 3",
            "D 1",
            "R 4",
            "D 1",
            "L 5",
            "R 2"
        };

        Assert.Equal(13, SolutionDay9.GetTailVisitationPositionCountHardcoded(input));
    }

    [Fact]
    public static void GetTailVisitationPositionCountUsingGeneralizedApproach()
    {
        var input = new[]
        {
            "R 4",
            "U 4",
            "L 3",
            "D 1",
            "R 4",
            "D 1",
            "L 5",
            "R 2"
        };

        Assert.Equal(13, SolutionDay9.GetTailVisitationPositionCount(input, 2));
    }

    [Fact]
    public static void GetLongTailVisitationPositionCount()
    {
        var input = new[]
        {
            "R 4",
            "U 4",
            "L 3",
            "D 1",
            "R 4",
            "D 1",
            "L 5",
            "R 2"
        };

        Assert.Equal(1, SolutionDay9.GetTailVisitationPositionCount(input, 10));
    }

    [Fact]
    public static void GetLongTailVisitationPositionCountLongerCommands()
    {
        var input = new[]
        {
            "R 5",
            "U 8",
            "L 8",
            "D 3",
            "R 17",
            "D 10",
            "L 25",
            "U 20"
        };

        Assert.Equal(36, SolutionDay9.GetTailVisitationPositionCount(input, 10));
    }
}