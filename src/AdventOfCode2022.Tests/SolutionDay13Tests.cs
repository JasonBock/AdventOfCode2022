using AdventOfCode2022.Day13;
using Xunit;

namespace AdventOfCode2022.Tests;

public static class SolutionDay13Tests
{
    [Fact]
    public static void DecomposeItems1()
    {
        var items = SolutionDay13.Decompose("[1,1,3,1,1]");

        Assert.Equal(5, items.Count);
        Assert.Equal(1, items[0]);
        Assert.Equal(1, items[1]);
        Assert.Equal(3, items[2]);
        Assert.Equal(1, items[3]);
        Assert.Equal(1, items[4]);
    }

    [Fact]
    public static void DecomposeItems2()
    {
        var items = SolutionDay13.Decompose("[1,1,5,1,1]");

        Assert.Equal(5, items.Count);
        Assert.Equal(1, items[0]);
        Assert.Equal(1, items[1]);
        Assert.Equal(5, items[2]);
        Assert.Equal(1, items[3]);
        Assert.Equal(1, items[4]);
    }

    [Fact]
    public static void DecomposeItems3()
    {
        var items = SolutionDay13.Decompose("[[1],[2,3,4]]");

        Assert.Equal(2, items.Count);

        var items0 = (List<object>)items[0];
        Assert.Single(items0);
        Assert.Equal(1, items0[0]);

        var items1 = (List<object>)items[1];
        Assert.Equal(3, items1.Count);
        Assert.Equal(2, items1[0]);
        Assert.Equal(3, items1[1]);
        Assert.Equal(4, items1[2]);
    }

    [Fact]
    public static void DecomposeItems4()
    {
        var items = SolutionDay13.Decompose("[[1],4]");

        Assert.Equal(2, items.Count);

        var items0 = (List<object>)items[0];
        Assert.Single(items0);
        Assert.Equal(1, items0[0]);

        Assert.Equal(4, items[1]);
    }

    [Fact]
    public static void DecomposeItems5()
    {
        var items = SolutionDay13.Decompose("[9]");

        Assert.Single(items);
        Assert.Equal(9, items[0]);
    }

    [Fact]
    public static void DecomposeItems6()
    {
        var items = SolutionDay13.Decompose("[[8,7,6]]");

        Assert.Single(items);

        var items0 = (List<object>)items[0];
        Assert.Equal(3, items0.Count);
        Assert.Equal(8, items0[0]);
        Assert.Equal(7, items0[1]);
        Assert.Equal(6, items0[2]);
    }

    [Fact]
    public static void DecomposeItems7()
    {
        var items = SolutionDay13.Decompose("[[4,4],4,4]");

        Assert.Equal(3, items.Count);

        var items0 = (List<object>)items[0];
        Assert.Equal(2, items0.Count);
        Assert.Equal(4, items0[0]);
        Assert.Equal(4, items0[1]);

        Assert.Equal(4, items[1]);
        Assert.Equal(4, items[2]);
    }

    [Fact]
    public static void DecomposeItems8()
    {
        var items = SolutionDay13.Decompose("[[4,4],4,4,4]");

        Assert.Equal(4, items.Count);

        var items0 = (List<object>)items[0];
        Assert.Equal(2, items0.Count);
        Assert.Equal(4, items0[0]);
        Assert.Equal(4, items0[1]);

        Assert.Equal(4, items[1]);
        Assert.Equal(4, items[2]);
        Assert.Equal(4, items[3]);
    }

    [Fact]
    public static void DecomposeItems9()
    {
        var items = SolutionDay13.Decompose("[7,7,7,7]");

        Assert.Equal(4, items.Count);

        Assert.Equal(7, items[0]);
        Assert.Equal(7, items[1]);
        Assert.Equal(7, items[2]);
        Assert.Equal(7, items[3]);
    }

    [Fact]
    public static void DecomposeItems10()
    {
        var items = SolutionDay13.Decompose("[7,7,7]");

        Assert.Equal(3, items.Count);

        Assert.Equal(7, items[0]);
        Assert.Equal(7, items[1]);
        Assert.Equal(7, items[2]);
    }

    [Fact]
    public static void DecomposeItems11()
    {
        var items = SolutionDay13.Decompose("[]");

        Assert.Empty(items);
    }

    [Fact]
    public static void DecomposeItems12()
    {
        var items = SolutionDay13.Decompose("[3]");

        Assert.Single(items);
        Assert.Equal(3, items[0]);
    }

    [Fact]
    public static void DecomposeItems13()
    {
        var items = SolutionDay13.Decompose("[[[]]]");

        Assert.Single(items);

        var items0 = (List<object>)items[0];
        Assert.Single(items0);

        var items00 = (List<object>)items0[0];
        Assert.Empty(items00);
    }

    [Fact]
    public static void DecomposeItems14()
    {
        var items = SolutionDay13.Decompose("[[]]");

        Assert.Single(items);

        var items0 = (List<object>)items[0];
        Assert.Empty(items0);
    }

    [Fact]
    public static void DecomposeItems15()
    {
        var items = SolutionDay13.Decompose("[1,[2,[3,[4,[5,6,7]]]],8,9]");

        Assert.Equal(4, items.Count);

        var items0 = (List<object>)items[1];
        Assert.Equal(2, items0.Count);
        Assert.Equal(2, items0[0]);

        var items00 = (List<object>)items0[1];
        Assert.Equal(2, items00.Count);
        Assert.Equal(3, items00[0]);

        var items000 = (List<object>)items00[1];
        Assert.Equal(2, items000.Count);
        Assert.Equal(4, items000[0]);

        var items0000 = (List<object>)items000[1];
        Assert.Equal(3, items0000.Count);
        Assert.Equal(5, items0000[0]);
        Assert.Equal(6, items0000[1]);
        Assert.Equal(7, items0000[2]);

        Assert.Equal(1, items[0]);
        Assert.Equal(8, items[2]);
        Assert.Equal(9, items[3]);
    }

    [Fact]
    public static void DecomposeItems16()
    {
        var items = SolutionDay13.Decompose("[1,[2,[3,[4,[5,6,0]]]],8,9]");

        Assert.Equal(4, items.Count);

        var items0 = (List<object>)items[1];
        Assert.Equal(2, items0.Count);
        Assert.Equal(2, items0[0]);

        var items00 = (List<object>)items0[1];
        Assert.Equal(2, items00.Count);
        Assert.Equal(3, items00[0]);

        var items000 = (List<object>)items00[1];
        Assert.Equal(2, items000.Count);
        Assert.Equal(4, items000[0]);

        var items0000 = (List<object>)items000[1];
        Assert.Equal(3, items0000.Count);
        Assert.Equal(5, items0000[0]);
        Assert.Equal(6, items0000[1]);
        Assert.Equal(0, items0000[2]);

        Assert.Equal(1, items[0]);
        Assert.Equal(8, items[2]);
        Assert.Equal(9, items[3]);
    }

    [Fact]
    public static void DecomposeItemsLargerNumbers()
    {
        var items = SolutionDay13.Decompose("[334,2,[2135,10],44]");

        Assert.Equal(4, items.Count);
        Assert.Equal(334, items[0]);
        Assert.Equal(2, items[1]);

        var items0 = (List<object>)items[2];
        Assert.Equal(2, items0.Count);
        Assert.Equal(2135, items0[0]);
        Assert.Equal(10, items0[1]);

        Assert.Equal(44, items[3]);
    }

    [Theory]
    [InlineData("[1,1,3,1,1]", "[1,1,5,1,1]", -1)]
    [InlineData("[[1],[2,3,4]]", "[[1],4]", -1)]
    [InlineData("[9]", "[[8,7,6]]", 1)]
    [InlineData("[[4,4],4,4]", "[[4,4],4,4,4]", -1)]
    [InlineData("[7,7,7,7]", "[7,7,7]", 1)]
    [InlineData("[]", "[3]", -1)]
    [InlineData("[1,[2,[3,[4,[5,6,7]]]],8,9]", "[1,[2,[3,[4,[5,6,0]]]],8,9]", 1)]
    [InlineData("[334,2,[2135,10],44]", "[334,2,[2135,10],44]", 0)]
    public static void ComparePairs(string leftPacket, string rightPacket, int expectedResult)
    {
        var leftPair = SolutionDay13.Decompose(leftPacket);
        var rightPair = SolutionDay13.Decompose(rightPacket);

        Assert.Equal(expectedResult, SolutionDay13.Compare(leftPair, rightPair));
    }

    [Fact]
    public static void GetCorrectPairSum()
    {
        var input = new[]
        {
            "[1, 1, 3, 1, 1]",
            "[1, 1, 5, 1, 1]",
            "",
            "[[1],[2, 3, 4]]",
            "[[1],4]",
            "",
            "[9]",
            "[[8,7,6]]",
            "",
            "[[4,4],4,4]",
            "[[4,4],4,4,4]",
            "",
            "[7,7,7,7]",
            "[7,7,7]",
            "",
            "[]",
            "[3]",
            "",
            "[[[]]]",
            "[[]]",
            "",
            "[1,[2,[3,[4,[5,6,7]]]],8,9]",
            "[1,[2,[3,[4,[5,6,0]]]],8,9]"
        };

        Assert.Equal(13, SolutionDay13.GetCorrectPairSum(input));
    }

    [Fact]
    public static void GetDecoderKey()
    {
        var input = new[]
        {
            "[1, 1, 3, 1, 1]",
            "[1, 1, 5, 1, 1]",
            "",
            "[[1],[2, 3, 4]]",
            "[[1],4]",
            "",
            "[9]",
            "[[8,7,6]]",
            "",
            "[[4,4],4,4]",
            "[[4,4],4,4,4]",
            "",
            "[7,7,7,7]",
            "[7,7,7]",
            "",
            "[]",
            "[3]",
            "",
            "[[[]]]",
            "[[]]",
            "",
            "[1,[2,[3,[4,[5,6,7]]]],8,9]",
            "[1,[2,[3,[4,[5,6,0]]]],8,9]"
        };

        Assert.Equal(140, SolutionDay13.GetDecoderKey(input));
    }
}