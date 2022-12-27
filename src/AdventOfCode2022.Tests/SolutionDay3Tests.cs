using AdventOfCode2022.Day3;
using System.Diagnostics;
using Xunit;

namespace AdventOfCode2022.Tests;

public static class SolutionDay3Tests
{
    [Theory]
    [InlineData('a', 1)]
    [InlineData('b', 2)]
    [InlineData('c', 3)]
    [InlineData('d', 4)]
    [InlineData('e', 5)]
    [InlineData('f', 6)]
    [InlineData('g', 7)]
    [InlineData('h', 8)]
    [InlineData('i', 9)]
    [InlineData('j', 10)]
    [InlineData('k', 11)]
    [InlineData('l', 12)]
    [InlineData('m', 13)]
    [InlineData('n', 14)]
    [InlineData('o', 15)]
    [InlineData('p', 16)]
    [InlineData('q', 17)]
    [InlineData('r', 18)]
    [InlineData('s', 19)]
    [InlineData('t', 20)]
    [InlineData('u', 21)]
    [InlineData('v', 22)]
    [InlineData('w', 23)]
    [InlineData('x', 24)]
    [InlineData('y', 25)]
    [InlineData('z', 26)]
    [InlineData('A', 27)]
    [InlineData('B', 28)]
    [InlineData('C', 29)]
    [InlineData('D', 30)]
    [InlineData('E', 31)]
    [InlineData('F', 32)]
    [InlineData('G', 33)]
    [InlineData('H', 34)]
    [InlineData('I', 35)]
    [InlineData('J', 36)]
    [InlineData('K', 37)]
    [InlineData('L', 38)]
    [InlineData('M', 39)]
    [InlineData('N', 40)]
    [InlineData('O', 41)]
    [InlineData('P', 42)]
    [InlineData('Q', 43)]
    [InlineData('R', 44)]
    [InlineData('S', 45)]
    [InlineData('T', 46)]
    [InlineData('U', 47)]
    [InlineData('V', 48)]
    [InlineData('W', 49)]
    [InlineData('X', 50)]
    [InlineData('Y', 51)]
    [InlineData('Z', 52)]
    public static void GetPriority(char item, int expectedPriority) =>
        Assert.Equal(expectedPriority, SolutionDay3.GetPriority(item));

    [Theory]
    [InlineData("vJrwpWtwJgWrhcsFMMfFFhFp", 'p')]
    [InlineData("jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", 'L')]
    [InlineData("PmmdzqPrVvPwwTWBwg", 'P')]
    [InlineData("wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn", 'v')]
    [InlineData("ttgJtRGJQctTZtZT", 't')]
    [InlineData("CrZsJsPPZsGzwwsLwLmpwMDw", 's')]
    public static void GetCommonItem(string rucksack, char expectedCommonItem) =>
        Assert.Equal(expectedCommonItem, SolutionDay3.GetCommonItem(rucksack));

    [Fact]
    public static void GetCommonItemWhenNoItemExists() =>
        Assert.Throws<UnreachableException>(() => SolutionDay3.GetCommonItem("abcABC"));

    [Theory]
    [MemberData(nameof(Rucksacks))]
    public static void Run(string[] input, int expectedValue) =>
        Assert.Equal(expectedValue, SolutionDay3.Run(input));

    [Theory]
    [InlineData("vJrwpWtwJgWrhcsFMMfFFhFp", "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", "PmmdzqPrVvPwwTWBwg", 'r')]
    [InlineData("wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn", "ttgJtRGJQctTZtZT", "CrZsJsPPZsGzwwsLwLmpwMDw", 'Z')]
    public static void GetCommomElfRucksackItem(string elfRucksack0, string elfRucksack1, string elfRucksack2, char expectedCommonItem) =>
        Assert.Equal(expectedCommonItem, SolutionDay3.GetCommomElfRucksackItem(elfRucksack0, elfRucksack1, elfRucksack2));

    [Theory]
    [MemberData(nameof(RucksacksForElves))]
    public static void RunForCommonElfItem(string[] input, int expectedValue) =>
        Assert.Equal(expectedValue, SolutionDay3.RunForCommonElfItem(input));

    [Theory]
    [InlineData("vJrwpWtwJgWrhcsFMMfFFhFp", "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", "PmmdzqPrVvPwwTWBwg", 'r')]
    [InlineData("wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn", "ttgJtRGJQctTZtZT", "CrZsJsPPZsGzwwsLwLmpwMDw", 'Z')]
    public static void GetCommomElfRucksackItemImproved(string elfRucksack0, string elfRucksack1, string elfRucksack2, char expectedCommonItem) =>
        Assert.Equal(expectedCommonItem, SolutionDay3.GetCommomElfRucksackItemImproved(elfRucksack0, elfRucksack1, elfRucksack2));

    [Theory]
    [MemberData(nameof(RucksacksForElves))]
    public static void RunForCommonElfItemImproved(string[] input, int expectedValue) =>
        Assert.Equal(expectedValue, SolutionDay3.RunForCommonElfItemImproved(input));

    public static IEnumerable<object[]> Rucksacks()
    {
        yield return new object[] 
        { 
            new[] 
            { 
                "vJrwpWtwJgWrhcsFMMfFFhFp", 
                "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", 
                "PmmdzqPrVvPwwTWBwg", 
                "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn", 
                "ttgJtRGJQctTZtZT", 
                "CrZsJsPPZsGzwwsLwLmpwMDw"
            },
            157 
        };

        yield return new object[]
        {
            new[]
            {
                "jj",
                "abjABj",
                "jabABj",
                "abjjAB",
            },
            40
        };
    }

    public static IEnumerable<object[]> RucksacksForElves()
    {
        yield return new object[]
        {
            new[]
            {
                "vJrwpWtwJgWrhcsFMMfFFhFp",
                "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
                "PmmdzqPrVvPwwTWBwg",
                "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
                "ttgJtRGJQctTZtZT",
                "CrZsJsPPZsGzwwsLwLmpwMDw"
            },
            70
        };

        yield return new object[]
        {
            new[]
            {
                "jj",
                "emReRj",
                "abCdeFghIwmnOpqRstUvwj",
            },
            10
        };
    }
}