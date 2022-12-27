using AdventOfCode2022.Day11;
using System.Numerics;
using Xunit;

namespace AdventOfCode2022.Tests;

public static class SolutionDay11Tests
{
    [Fact]
    public static void InspectMonkeyHasRelief()
    {
        var throws = new List<(int, BigInteger)>();

        var throwMonkeyIndexWorryLevel = new Action<int, BigInteger>((index, worryLevel) => throws.Add((index, worryLevel)));
        var monkey = new Monkey(new BigInteger[] { 79, 98 }, old => old * 19, true, 96577, 23, 2, 3, throwMonkeyIndexWorryLevel);

        monkey.Inspect();

        Assert.Equal(2, monkey.Inspections);
        Assert.Equal(2, throws.Count);
        Assert.Contains((3, 500), throws);
        Assert.Contains((3, 620), throws);
    }

    [Fact]
    public static void InspectMonkeyHasNoRelief()
    {
        var throws = new List<(int, BigInteger)>();

        var throwMonkeyIndexWorryLevel = new Action<int, BigInteger>((index, worryLevel) => throws.Add((index, worryLevel)));
        var monkey = new Monkey(new BigInteger[] { 79, 98 }, old => old * 19, false, 96577, 23, 2, 3, throwMonkeyIndexWorryLevel);

        monkey.Inspect();

        Assert.Equal(2, monkey.Inspections);
        Assert.Equal(2, throws.Count);
        Assert.Contains((3, 1501), throws);
        Assert.Contains((3, 1862), throws);
    }

    [Fact]
    public static void GetMonkeyBusinessExampleHasRelief() 
    {
        var monkeys = SolutionDay11.GetMonkeysExample(true);
        var monkeyBusiness = SolutionDay11.GetMonkeyBusiness(monkeys, 20);

        Assert.Equal(10605, monkeyBusiness);
    }

    [Fact]
    public static void GetMonkeyBusinessExampleBigIntegerHasNoRelief()
    {
        var monkeys = SolutionDay11.GetMonkeysExample(false);
        var monkeyBusiness = SolutionDay11.GetMonkeyBusiness(monkeys, 10000);

        Assert.Equal(2713310158, monkeyBusiness);
    }
}