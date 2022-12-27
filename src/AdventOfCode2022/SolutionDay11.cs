using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode2022.Day11;

public static class SolutionDay11
{
    public static BigInteger GetMonkeyBusiness(Monkey[] monkeys, int rounds)
    {
        for (var r = 0; r < rounds; r++)
        {
            for (var m = 0; m < monkeys.Length; m++)
            {
                monkeys[m].Inspect();
            }
        }

        var activeMonkeys = monkeys.OrderByDescending(m => m.Inspections).Take(2).ToArray();

        return activeMonkeys[0].Inspections * activeMonkeys[1].Inspections;
    }

    public static Monkey[] GetMonkeysExample(bool hasRelief)
    {
        var range = 96577L;
        var monkeys = new Monkey[4];

        var throwMonkeyIndexWorryLevel = new Action<int, BigInteger>((index, worryLevel) => monkeys[index].AddItem(worryLevel));

        monkeys[0] = new Monkey(new BigInteger[] { 79, 98 }, old => old * 19, 
            hasRelief, range, 23, 2, 3, throwMonkeyIndexWorryLevel);
        monkeys[1] = new Monkey(new BigInteger[] { 54, 65, 75, 74 }, old => old + 6, 
            hasRelief, range, 19, 2, 0, throwMonkeyIndexWorryLevel);
        monkeys[2] = new Monkey(new BigInteger[] { 79, 60, 97 }, old => old * old, 
            hasRelief, range, 13, 1, 3, throwMonkeyIndexWorryLevel);
        monkeys[3] = new Monkey(new BigInteger[] { 74 }, old => old + 3, 
            hasRelief, range, 17, 0, 1, throwMonkeyIndexWorryLevel);



        return monkeys;
    }

    public static Monkey[] GetMonkeysDayTest(bool hasRelief)
    {
        var range = 9699690L;
        var monkeys = new Monkey[8];

        var throwMonkeyIndexWorryLevel = new Action<int, BigInteger>((index, worryLevel) => monkeys[index].AddItem(worryLevel));

        monkeys[0] = new Monkey(new BigInteger[] { 75, 63 }, old => old * 3, 
            hasRelief, range, 11, 7, 2, throwMonkeyIndexWorryLevel);
        monkeys[1] = new Monkey(new BigInteger[] { 65, 79, 98, 77, 56, 54, 83, 94 }, old => old + 3, 
            hasRelief, range, 2, 2, 0, throwMonkeyIndexWorryLevel);
        monkeys[2] = new Monkey(new BigInteger[] { 66 }, old => old + 5, 
            hasRelief, range, 5, 7, 5, throwMonkeyIndexWorryLevel);
        monkeys[3] = new Monkey(new BigInteger[] { 51, 89, 90 }, old => old * 19, 
            hasRelief, range, 7, 6, 4, throwMonkeyIndexWorryLevel);
        monkeys[4] = new Monkey(new BigInteger[] { 75, 94, 66, 90, 77, 82, 61 }, old => old + 1, 
            hasRelief, range, 17, 6, 1, throwMonkeyIndexWorryLevel);
        monkeys[5] = new Monkey(new BigInteger[] { 53, 76, 59, 92, 95 }, old => old + 2, 
            hasRelief, range, 19, 4, 3, throwMonkeyIndexWorryLevel);
        monkeys[6] = new Monkey(new BigInteger[] { 81, 61, 75, 89, 70, 92 }, old => old * old, 
            hasRelief, range, 3, 0, 1, throwMonkeyIndexWorryLevel);
        monkeys[7] = new Monkey(new BigInteger[] { 81, 86, 62, 87 }, old => old + 8, 
            hasRelief, range, 13, 3, 5, throwMonkeyIndexWorryLevel);

        return monkeys;
    }
}

public sealed class Monkey
{
    private readonly BigInteger divisibleTest;
    private readonly bool hasRelief;
    private readonly Queue<BigInteger> items;
    private readonly Action<int, BigInteger> monkeyThrowIndexWorryLevelCallback;
    private readonly int monkeyIndexFalse;
    private readonly int monkeyIndexTrue;
    private readonly Func<BigInteger, BigInteger> operation;
    private readonly BigInteger range;

    public Monkey(BigInteger[] items, Func<BigInteger, BigInteger> operation, bool hasRelief, BigInteger range, BigInteger divisibleTest,
        int monkeyIndexTrue, int monkeyIndexFalse, Action<int, BigInteger> monkeyThrowIndexWorryLevelCallback) =>
        (this.items, this.operation, this.hasRelief, this.range, this.divisibleTest, this.monkeyIndexTrue, this.monkeyIndexFalse, this.monkeyThrowIndexWorryLevelCallback) =
            (new Queue<BigInteger>(items), operation, hasRelief, range, divisibleTest, monkeyIndexTrue, monkeyIndexFalse, monkeyThrowIndexWorryLevelCallback);

    public void AddItem(BigInteger worryLevel) => this.items.Enqueue(worryLevel);

    public void Inspect()
    {
        while (this.items.TryDequeue(out var item))
        {
            this.Inspections++;

            var newWorryLevel = (this.hasRelief ? (this.operation(item) / 3) : this.operation(item)) % this.range;
            var monkeyThrowIndex = newWorryLevel % this.divisibleTest == 0 ?
                this.monkeyIndexTrue : this.monkeyIndexFalse;
            this.monkeyThrowIndexWorryLevelCallback(monkeyThrowIndex, newWorryLevel);
        }
    }

    public BigInteger Inspections { get; private set; }
}