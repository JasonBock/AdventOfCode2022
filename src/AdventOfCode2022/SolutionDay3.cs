using System;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2022.Day3;

public static class SolutionDay3
{
    public static int Run(string[] rucksacks)
    {
        var totalPriority = 0;

        for (var i = 0; i < rucksacks.Length; i++)
        {
            var commonItem = SolutionDay3.GetCommonItem(rucksacks[i]);
            totalPriority += SolutionDay3.GetPriority(commonItem);
        }

        return totalPriority;
    }

    public static int RunForCommonElfItem(string[] rucksacks)
    {
        var totalPriority = 0;

        for (var i = 0; i < rucksacks.Length; i += 3)
        {
            var commonItem = SolutionDay3.GetCommomElfRucksackItem(
                rucksacks[i], rucksacks[i + 1], rucksacks[i + 2]);
            totalPriority += SolutionDay3.GetPriority(commonItem);
        }

        return totalPriority;
    }

    public static int RunForCommonElfItemImproved(string[] rucksacks)
    {
        var totalPriority = 0;

        for (var i = 0; i < rucksacks.Length; i += 3)
        {
            var commonItem = SolutionDay3.GetCommomElfRucksackItemImproved(
                rucksacks[i], rucksacks[i + 1], rucksacks[i + 2]);
            totalPriority += SolutionDay3.GetPriority(commonItem);
        }

        return totalPriority;
    }

    // 'a' == 97 and should be 1, so the difference is 96
    // 'A' == 65 and should be 27, so the difference is 38
    public static int GetPriority(char item) =>
        char.IsAsciiLetterLower(item) ? item - 96 : item - 38;

    public static char GetCommonItem(ReadOnlySpan<char> rucksack)
    {
        var left = rucksack[..(rucksack.Length / 2)];
        var right = rucksack[(rucksack.Length / 2)..];

        /*
        Let's say we have 
        
        aBcDefGhBj

        That splits into "aBcDe" and "fGhBj".

        We get the 0th char from left and right, and see if it's in the other span.
        If it isn't, then we can reduce the span by 1.
        */

        for (var i = 0; i < rucksack.Length / 2; i++)
        {
            var leftItem = left[0]; 
            var rightItem = right[0];

            if(right.Contains(leftItem))
            {
                return leftItem;
            }
            else if (left.Contains(rightItem))
            {
                return rightItem;
            }
            else
            {
                left = left[1..];
                right = right[1..];
            }
        }

        throw new UnreachableException();
    }

    public static char GetCommomElfRucksackItem(ReadOnlySpan<char> rucksack0, ReadOnlySpan<char> rucksack1, ReadOnlySpan<char> rucksack2) 
    {
        /*
        We need to go to the longest span, but we also have to be careful
        not to go beyond spans that are shorter.
        */
        var longestSpan = new[] { rucksack0.Length, rucksack1.Length, rucksack2.Length }.Max();

        for(var i = 0; i < longestSpan; i++)
        {
            if(i < rucksack0.Length)
            {
                var rucksack0Item = rucksack0[i];

                if(rucksack1.Contains(rucksack0Item) && rucksack2.Contains(rucksack0Item))
                {
                    return rucksack0Item;
                }
            }

            if (i < rucksack1.Length)
            {
                var rucksack1Item = rucksack1[i];

                if (rucksack0.Contains(rucksack1Item) && rucksack2.Contains(rucksack1Item))
                {
                    return rucksack1Item;
                }
            }

            if (i < rucksack2.Length)
            {
                var rucksack2Item = rucksack2[i];

                if (rucksack0.Contains(rucksack2Item) && rucksack1.Contains(rucksack2Item))
                {
                    return rucksack2Item;
                }
            }
        }

        throw new UnreachableException();
    }

    public static char GetCommomElfRucksackItemImproved(ReadOnlySpan<char> rucksack0, ReadOnlySpan<char> rucksack1, ReadOnlySpan<char> rucksack2)
    {
        /*
        We only need to go the length of the shortest span.
        */
        var shortestSpan = new[] { rucksack0.Length, rucksack1.Length, rucksack2.Length }.Min();

        var rucksack0Span = rucksack0;
        var rucksack1Span = rucksack1;
        var rucksack2Span = rucksack2;

        for (var i = 0; i < shortestSpan; i++)
        {
            var rucksack0Item = rucksack0Span[0];

            if (rucksack1Span.Contains(rucksack0Item) && rucksack2Span.Contains(rucksack0Item))
            {
                return rucksack0Item;
            }

            var rucksack1Item = rucksack1Span[0];

            if (rucksack0Span.Contains(rucksack1Item) && rucksack2Span.Contains(rucksack1Item))
            {
                return rucksack1Item;
            }

            var rucksack2Item = rucksack2Span[0];

            if (rucksack0Span.Contains(rucksack2Item) && rucksack1Span.Contains(rucksack2Item))
            {
                return rucksack2Item;
            }

            rucksack0Span = rucksack0Span[1..];
            rucksack1Span = rucksack1Span[1..];
            rucksack2Span = rucksack2Span[1..];
        }

        throw new UnreachableException();
    }

    public static char GetCommomElfRucksackItemImprovedMaxNoArray(ReadOnlySpan<char> rucksack0, ReadOnlySpan<char> rucksack1, ReadOnlySpan<char> rucksack2)
    {
        /*
        We only need to go the length of the shortest span.
        */
        var shortestSpan = rucksack0.Length;
        if (rucksack1.Length < shortestSpan) { shortestSpan = rucksack1.Length; }
        if (rucksack2.Length < shortestSpan) { shortestSpan = rucksack2.Length; }

        var rucksack0Span = rucksack0;
        var rucksack1Span = rucksack1;
        var rucksack2Span = rucksack2;

        for (var i = 0; i < shortestSpan; i++)
        {
            var rucksack0Item = rucksack0Span[0];

            if (rucksack1Span.Contains(rucksack0Item) && rucksack2Span.Contains(rucksack0Item))
            {
                return rucksack0Item;
            }

            var rucksack1Item = rucksack1Span[0];

            if (rucksack0Span.Contains(rucksack1Item) && rucksack2Span.Contains(rucksack1Item))
            {
                return rucksack1Item;
            }

            var rucksack2Item = rucksack2Span[0];

            if (rucksack0Span.Contains(rucksack2Item) && rucksack1Span.Contains(rucksack2Item))
            {
                return rucksack2Item;
            }

            rucksack0Span = rucksack0Span[1..];
            rucksack1Span = rucksack1Span[1..];
            rucksack2Span = rucksack2Span[1..];
        }

        throw new UnreachableException();
    }
}
