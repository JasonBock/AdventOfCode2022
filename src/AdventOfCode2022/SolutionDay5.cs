using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022.Day5;

public static class SolutionDay5
{
    public static string Run(string[] configuration)
    {
        /*
        There are two sections to parse.
        The first is the crate stack configuration

        The second contains the "move" instructions

        * If the line contains a '[', it's a crate stack configuration
        * If the line is blank, it means crate stack configuration is done, and we have to reverse all the stacks.
        * If the line starts with "move", it's a move instruction, but this can't happen until we hit the blank line.
        * Else, just skip it (it's probably the line with the numbers of the stacks, but we don't need that.
        */

        var stacks = new Dictionary<int, Stack<char>>();

        for (var i = 0; i < configuration.Length; i++)
        {
            var code = configuration[i];

            if (code.Contains('['))
            {
                SolutionDay5.UpdateCrateStacks(stacks, code);
            }
            else if(code.Length == 0)
            {
                stacks = SolutionDay5.ReverseStacks(stacks);
            }
            else if(code.StartsWith("move"))
            {
                SolutionDay5.MoveCrates(stacks, code);
            }
        }

        return SolutionDay5.GetMessage(stacks);
    }

    public static string RunAsIs(string[] configuration)
    {
        /*
        There are two sections to parse.
        The first is the crate stack configuration

        The second contains the "move" instructions

        * If the line contains a '[', it's a crate stack configuration
        * If the line is blank, it means crate stack configuration is done, and we have to reverse all the stacks.
        * If the line starts with "move", it's a move instruction, but this can't happen until we hit the blank line.
        * Else, just skip it (it's probably the line with the numbers of the stacks, but we don't need that.
        */

        var stacks = new Dictionary<int, Stack<char>>();

        for (var i = 0; i < configuration.Length; i++)
        {
            var code = configuration[i];

            if (code.Contains('['))
            {
                SolutionDay5.UpdateCrateStacks(stacks, code);
            }
            else if (code.Length == 0)
            {
                stacks = SolutionDay5.ReverseStacks(stacks);
            }
            else if (code.StartsWith("move"))
            {
                SolutionDay5.MoveCratesAsIs(stacks, code);
            }
        }

        return SolutionDay5.GetMessage(stacks);
    }

    public static string GetMessage(Dictionary<int, Stack<char>> stacks)
    {
        var message = string.Empty;

        foreach(var (_, value) in stacks.OrderBy(_ => _.Key))
        {
            message += value.Peek();
        }

        return message;
    }

    public static void MoveCrates(Dictionary<int, Stack<char>> stacks, string code)
    {
        // The format is:
        //
        // move 1 from 2 to 1
        //
        // So we can split the line on ' ',
        // and assume
        // [3] is the key for the source (minus 1)
        // [5] is the key for the destination (minus 1)
        // [1] is the amount to pop from the source to the destination
        var tokens = code.Split(' ');
        var sourceKey = int.Parse(tokens[3]) - 1;
        var destinationKey = int.Parse(tokens[5]) - 1;
        var amount = int.Parse(tokens[1]);

        for(var i = 0; i < amount; i++)
        {
            stacks[destinationKey].Push(stacks[sourceKey].Pop());
        }
    }

    public static void MoveCratesAsIs(Dictionary<int, Stack<char>> stacks, string code)
    {
        // The format is:
        //
        // move 1 from 2 to 1
        //
        // So we can split the line on ' ',
        // and assume
        // [3] is the key for the source (minus 1)
        // [5] is the key for the destination (minus 1)
        // [1] is the amount to pop from the source to the destination
        var tokens = code.Split(' ');
        var sourceKey = int.Parse(tokens[3]) - 1;
        var destinationKey = int.Parse(tokens[5]) - 1;
        var amount = int.Parse(tokens[1]);
        var tempStack = new Stack<char>();

        for (var i = 0; i < amount; i++)
        {
            tempStack.Push(stacks[sourceKey].Pop());
        }

        for(var t = 0; t < amount; t++)
        {
            stacks[destinationKey].Push(tempStack.Pop());
        }
    }

    public static Dictionary<int, Stack<char>> ReverseStacks(Dictionary<int, Stack<char>> stacks)
    {
        var newStacks = new Dictionary<int, Stack<char>>();

        foreach (var (key, value) in stacks)
        {
            newStacks.Add(key, new Stack<char>(value));
        }

        return newStacks;
    }

    public static void UpdateCrateStacks(Dictionary<int, Stack<char>> stacks, string code)
    {
        // We keep going through the code, finding '[',
        // and pulling out the char immediately after.
        // The position of '[' tells us the corresponding key.
        // For example:
        // [Z] [M] [P] [T]
        // 0 => 0
        // 4 => 1
        // 8 => 2
        // 12 => 3
        // Therefore, (position / 4) gives the key

        var startIndex = 0;
        var leftBrackedIndex = code.IndexOf('[');

        while (leftBrackedIndex != -1)
        {
            var key = leftBrackedIndex / 4;
            var item = code[leftBrackedIndex + 1];

            if (stacks.TryGetValue(key, out var value))
            {
                value.Push(item);
            }
            else
            {
                stacks.Add(key, new Stack<char>(new[] { item }));
            }

            startIndex = leftBrackedIndex + 4;

            if(startIndex >= code.Length)
            {
                leftBrackedIndex = -1;
            }
            else
            {
                leftBrackedIndex = code.IndexOf('[', startIndex);
            }
        }
    }
}
