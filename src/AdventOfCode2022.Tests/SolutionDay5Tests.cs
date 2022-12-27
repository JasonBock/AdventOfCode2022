using AdventOfCode2022.Day5;
using Xunit;

namespace AdventOfCode2022.Tests;

public static class SolutionDay5Tests
{
    [Fact]
    public static void Run() 
    {
        var configuration = new[]
        {
            "    [D]",
            "[N] [C]",
            "[Z] [M] [P]",
            " 1   2   3",
            "",
            "move 1 from 2 to 1",
            "move 3 from 1 to 3",
            "move 2 from 2 to 1",
            "move 1 from 1 to 2"
        };

        var message = SolutionDay5.Run(configuration);
        Assert.Equal("CMZ", message);
    }

    [Fact]
    public static void RunAsIs()
    {
        var configuration = new[]
        {
            "    [D]",
            "[N] [C]",
            "[Z] [M] [P]",
            " 1   2   3",
            "",
            "move 1 from 2 to 1",
            "move 3 from 1 to 3",
            "move 2 from 2 to 1",
            "move 1 from 1 to 2"
        };

        var message = SolutionDay5.RunAsIs(configuration);
        Assert.Equal("MCD", message);
    }

    [Fact] 
    public static void UpdateCrateStacks() 
    {
        var stacks = new Dictionary<int, Stack<char>>();

        SolutionDay5.UpdateCrateStacks(stacks, "    [Q]");
        SolutionDay5.UpdateCrateStacks(stacks, "[Z] [M] [P] [T]     [X]");
        SolutionDay5.UpdateCrateStacks(stacks, "    [S]     [J]");

        Assert.Equal(5, stacks.Count);

        var stack0 = stacks[0];
        Assert.Single(stack0);
        Assert.Equal('Z', stack0.Pop());

        var stack1 = stacks[1];
        Assert.Equal(3, stack1.Count);
        Assert.Equal('S', stack1.Pop());
        Assert.Equal('M', stack1.Pop());
        Assert.Equal('Q', stack1.Pop());

        var stack2 = stacks[2];
        Assert.Single(stack2);
        Assert.Equal('P', stack2.Pop());

        var stack3 = stacks[3];
        Assert.Equal(2, stack3.Count);
        Assert.Equal('J', stack3.Pop());
        Assert.Equal('T', stack3.Pop());

        var stack4 = stacks[5];
        Assert.Single(stack4);
        Assert.Equal('X', stack4.Pop());
    }

    [Fact]
    public static void ReverseStacks()
    {
        var stacks = new Dictionary<int, Stack<char>>();

        SolutionDay5.UpdateCrateStacks(stacks, "    [Q]");
        SolutionDay5.UpdateCrateStacks(stacks, "[Z] [M] [P] [T]     [X]");
        SolutionDay5.UpdateCrateStacks(stacks, "    [S]     [J]");
        stacks = SolutionDay5.ReverseStacks(stacks);

        Assert.Equal(5, stacks.Count);

        var stack0 = stacks[0];
        Assert.Single(stack0);
        Assert.Equal('Z', stack0.Pop());

        var stack1 = stacks[1];
        Assert.Equal(3, stack1.Count);
        Assert.Equal('Q', stack1.Pop());
        Assert.Equal('M', stack1.Pop());
        Assert.Equal('S', stack1.Pop());

        var stack2 = stacks[2];
        Assert.Single(stack2);
        Assert.Equal('P', stack2.Pop());

        var stack3 = stacks[3];
        Assert.Equal(2, stack3.Count);
        Assert.Equal('T', stack3.Pop());
        Assert.Equal('J', stack3.Pop());

        var stack4 = stacks[5];
        Assert.Single(stack4);
        Assert.Equal('X', stack4.Pop());
    }

    [Fact]
    public static void MoveCrates()
    {
        var stacks = new Dictionary<int, Stack<char>>();

        SolutionDay5.UpdateCrateStacks(stacks, "    [D]");
        SolutionDay5.UpdateCrateStacks(stacks, "[N] [C]");
        SolutionDay5.UpdateCrateStacks(stacks, "[Z] [M] [P]");
        stacks = SolutionDay5.ReverseStacks(stacks);
        SolutionDay5.MoveCrates(stacks, "move 1 from 2 to 1");
        SolutionDay5.MoveCrates(stacks, "move 3 from 1 to 3");
        SolutionDay5.MoveCrates(stacks, "move 2 from 2 to 1");
        SolutionDay5.MoveCrates(stacks, "move 1 from 1 to 2");

        Assert.Equal(3, stacks.Count);

        var stack0 = stacks[0];
        Assert.Single(stack0);
        Assert.Equal('C', stack0.Pop());

        var stack1 = stacks[1];
        Assert.Single(stack1);
        Assert.Equal('M', stack1.Pop());

        var stack2 = stacks[2];
        Assert.Equal(4, stack2.Count);
        Assert.Equal('Z', stack2.Pop());
        Assert.Equal('N', stack2.Pop());
        Assert.Equal('D', stack2.Pop());
        Assert.Equal('P', stack2.Pop());
    }

    [Fact]
    public static void MoveCratesAsIs()
    {
        var stacks = new Dictionary<int, Stack<char>>();

        SolutionDay5.UpdateCrateStacks(stacks, "    [D]");
        SolutionDay5.UpdateCrateStacks(stacks, "[N] [C]");
        SolutionDay5.UpdateCrateStacks(stacks, "[Z] [M] [P]");
        stacks = SolutionDay5.ReverseStacks(stacks);
        SolutionDay5.MoveCratesAsIs(stacks, "move 1 from 2 to 1");
        SolutionDay5.MoveCratesAsIs(stacks, "move 3 from 1 to 3");
        SolutionDay5.MoveCratesAsIs(stacks, "move 2 from 2 to 1");
        SolutionDay5.MoveCratesAsIs(stacks, "move 1 from 1 to 2");

        Assert.Equal(3, stacks.Count);

        var stack0 = stacks[0];
        Assert.Single(stack0);
        Assert.Equal('M', stack0.Pop());

        var stack1 = stacks[1];
        Assert.Single(stack1);
        Assert.Equal('C', stack1.Pop());

        var stack2 = stacks[2];
        Assert.Equal(4, stack2.Count);
        Assert.Equal('D', stack2.Pop());
        Assert.Equal('N', stack2.Pop());
        Assert.Equal('Z', stack2.Pop());
        Assert.Equal('P', stack2.Pop());
    }

    [Fact]
    public static void GetMessage()
    {
        var stacks = new Dictionary<int, Stack<char>>();

        SolutionDay5.UpdateCrateStacks(stacks, "    [D]");
        SolutionDay5.UpdateCrateStacks(stacks, "[N] [C]");
        SolutionDay5.UpdateCrateStacks(stacks, "[Z] [M] [P]");
        stacks = SolutionDay5.ReverseStacks(stacks);
        SolutionDay5.MoveCrates(stacks, "move 1 from 2 to 1");
        SolutionDay5.MoveCrates(stacks, "move 3 from 1 to 3");
        SolutionDay5.MoveCrates(stacks, "move 2 from 2 to 1");
        SolutionDay5.MoveCrates(stacks, "move 1 from 1 to 2");
        var message = SolutionDay5.GetMessage(stacks);

        Assert.Equal("CMZ", message);
    }
}