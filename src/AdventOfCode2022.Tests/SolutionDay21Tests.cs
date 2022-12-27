using AdventOfCode2022.Day16;
using AdventOfCode2022.Day21;
using Xunit;

namespace AdventOfCode2022.Tests;

public static class SolutionDay21Tests
{
    [Fact]
    public static void GetRootNumber()
    {
        var jobs = new[]
        {
            "root: pppw + sjmn",
            "dbpl: 5",
            "cczh: sllz + lgvd",
            "zczc: 2",
            "ptdq: humn - dvpt",
            "dvpt: 3",
            "lfqf: 4",
            "humn: 5",
            "ljgn: 2",
            "sjmn: drzm * dbpl",
            "sllz: 4",
            "pppw: cczh / lfqf",
            "lgvd: ljgn * ptdq",
            "drzm: hmdt - zczc",
            "hmdt: 32",
        };

        var rootNumber = SolutionDay21.GetRootNumber(jobs);
        Assert.Equal(152, rootNumber);
    }

    [Fact]
    public static void GetHumanNumberExpressions()
    {
        var jobs = new[]
        {
            "root: pppw + sjmn",
            "dbpl: 5",
            "cczh: sllz + lgvd",
            "zczc: 2",
            "ptdq: humn - dvpt",
            "dvpt: 3",
            "lfqf: 4",
            "humn: 5",
            "ljgn: 2",
            "sjmn: drzm * dbpl",
            "sllz: 4",
            "pppw: cczh / lfqf",
            "lgvd: ljgn * ptdq",
            "drzm: hmdt - zczc",
            "hmdt: 32",
        };

        var (leftExpression, rightExpression) = SolutionDay21.GetHumanNumberExpressions(jobs);

        Assert.Equal("((4 + (2 * (humn - 3))) / 4)", leftExpression);
        Assert.Equal("(30 * 5)", rightExpression);
    }
}