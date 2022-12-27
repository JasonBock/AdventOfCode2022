using System;
using System.Collections.Generic;

namespace AdventOfCode2022.Day13;

public static class SolutionDay13
{
    public static int GetDecoderKey(string[] input)
    {
        var packets = new List<List<object>>();
        var twoDividerPacket = SolutionDay13.Decompose("[[2]]");
        var sixDividerPacket = SolutionDay13.Decompose("[[6]]");

        packets.Add(twoDividerPacket);
        packets.Add(sixDividerPacket);

        foreach (var packet in input)
        {
            if(packet.Length > 0)
            {
                packets.Add(SolutionDay13.Decompose(packet));
            }
        }

        packets.Sort(SolutionDay13.Compare);

        return (packets.IndexOf(twoDividerPacket) + 1) *
            (packets.IndexOf(sixDividerPacket) + 1);
    }

    public static int GetCorrectPairSum(string[] input) 
    {
        var pairSum = 0;
        var pairCount = 0;

        for(var i = 0; i < input.Length; i++)
        {
            var left = input[i];
            var right = input[i + 1];

            if (SolutionDay13.Compare(
                SolutionDay13.Decompose(left), SolutionDay13.Decompose(right)) <= 0)
            {
                pairSum += pairCount + 1;
            }

            i += 2;
            pairCount++;
        }

        return pairSum;
    }

    public static List<object> Decompose(string content)
    {
        var items = new List<object>();
        // We assume that the content is "[...]"
        SolutionDay13.Decompose(items, content.AsSpan()[1..^1]);
        return items;
    }

    public static int Compare(List<object> left, List<object> right)
    {
        for (var i = 0; i < int.Min(left.Count, right.Count); i++)
        {
            if (left[i] is int leftInt && right[i] is int rightInt)
            {
                if(leftInt != rightInt)
                {
                    return leftInt.CompareTo(rightInt);
                }
            }
            else
            {
                if (left[i] is int && right[i] is List<object>)
                {
                    left[i] = new List<object>
                    {
                        left[i]
                    };
                }
                else if (left[i] is List<object> && right[i] is int)
                {
                    right[i] = new List<object>
                    {
                        right[i]
                    };
                }

                var itemCompare = SolutionDay13.Compare((List<object>)left[i], (List<object>)right[i]);

                if(itemCompare != 0)
                {
                    return itemCompare;
                }
            }
        }

        return left.Count.CompareTo(right.Count);
    }

    public static void Decompose(List<object> items, ReadOnlySpan<char> content)
    {
        for (var i = 0; i < content.Length; i++)
        {
            if (content[i] == '[')
            {
                var startingSubBracket = i;
                var endingSubBracket = 1;

                while (endingSubBracket > 0)
                {
                    i++;

                    if (content[i] == '[')
                    {
                        endingSubBracket++;
                    }
                    else if (content[i] == ']')
                    {
                        endingSubBracket--;
                    }
                }

                var newItem = new List<object>();
                items.Add(newItem);
                SolutionDay13.Decompose(newItem, content[(startingSubBracket + 1)..i]);
            }
            else if (content[i] >= '0' && content[i] <= '9')
            {
                var startingNumber = i;

                while (content[i] != ',')
                {
                    i++;

                    if (i == content.Length)
                    {
                        break;
                    }
                }

                items.Add(int.Parse(content[startingNumber..i]));
            }
        }
    }
}
