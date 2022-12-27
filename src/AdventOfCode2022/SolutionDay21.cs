using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AdventOfCode2022.Day21;

public static class SolutionDay21
{
    public static long GetRootNumber(string[] jobs)
    {
        var monkeys = new List<Operation>();

        // Parsing
        foreach (var job in jobs)
        {
            var content = job.Split(':');
            var name = content[0];

            if (long.TryParse(content[1].Trim(), out var value))
            {
                monkeys.Add(new Operation(name, value, null, null, null, null, null));
            }
            else
            {
                if (content[1].Contains('+'))
                {
                    var operationParts = content[1].Split('+');
                    monkeys.Add(new Operation(name, null, operationParts[0].Trim(), null, operationParts[1].Trim(), null, "+"));
                }
                else if (content[1].Contains('-'))
                {
                    var operationParts = content[1].Split('-');
                    monkeys.Add(new Operation(name, null, operationParts[0].Trim(), null, operationParts[1].Trim(), null, "-"));
                }
                else if (content[1].Contains('*'))
                {
                    var operationParts = content[1].Split('*');
                    monkeys.Add(new Operation(name, null, operationParts[0].Trim(), null, operationParts[1].Trim(), null, "*"));
                }
                else
                {
                    var operationParts = content[1].Split('/');
                    monkeys.Add(new Operation(name, null, operationParts[0].Trim(), null, operationParts[1].Trim(), null, "/"));
                }
            }
        }

        // Calculating
        while (true)
        {
            var numberMonkees = monkeys.Where(_ => _.Value is not null).ToList();

            foreach (var numberMonkee in numberMonkees)
            {
                foreach (var needsLeft in monkeys.Where(_ => _.Value is null && _.Left == numberMonkee.Name && _.LeftValue is null))
                {
                    needsLeft.LeftValue = numberMonkee.Value;
                }

                foreach (var needsRight in monkeys.Where(_ => _.Value is null && _.Right == numberMonkee.Name && _.RightValue is null))
                {
                    needsRight.RightValue = numberMonkee.Value;
                }
            }

            foreach (var computeMonkee in monkeys.Where(_ => _.Value is null && _.LeftValue is not null && _.RightValue is not null))
            {
                computeMonkee.Value = computeMonkee.Math switch
                {
                    "+" => computeMonkee.LeftValue + computeMonkee.RightValue,
                    "-" => computeMonkee.LeftValue - computeMonkee.RightValue,
                    "*" => computeMonkee.LeftValue * computeMonkee.RightValue,
                    "/" => computeMonkee.LeftValue / computeMonkee.RightValue,
                    _ => throw new UnreachableException()
                };
            }

            var rootMonkey = monkeys.Single(_ => _.Name == "root");

            if (rootMonkey.Value is not null)
            {
                return rootMonkey.Value.Value;
            }
        }
    }

    public static (string leftSide, string rightSide) GetHumanNumberExpressions(string[] jobs)
    {
        const string Root = "root";
        const string Human = "humn";

        var monkeys = new List<ExpressionOperation>();

        // Parsing
        // drzm: hmdt - zczc
        // hmdt: 32
        // Note that "root" will be "="
        // and "humn" will be a variable,
        // so it won't be added
        foreach (var job in jobs)
        {
            var content = job.Split(':');
            var name = content[0];

            if(name == Root)
            {
                var expression = content[1].Replace('+', '=').Replace('-', '=').Replace('*', '=').Replace('-', '=');
                var operationParts = expression.Split('=');
                monkeys.Add(new ExpressionOperation(name, null, null, operationParts[0].Trim(), null, operationParts[1].Trim(), null, "="));
            }
            else if(name != Human)
            {
                if (long.TryParse(content[1].Trim(), out var value))
                {
                    monkeys.Add(new ExpressionOperation(name, value, null, null, null, null, null, null));
                }
                else
                {
                    if (content[1].Contains('+'))
                    {
                        var operationParts = content[1].Split('+');
                        monkeys.Add(new ExpressionOperation(name, null, null, operationParts[0].Trim(), null, operationParts[1].Trim(), null, "+"));
                    }
                    else if (content[1].Contains('-'))
                    {
                        var operationParts = content[1].Split('-');
                        monkeys.Add(new ExpressionOperation(name, null, null, operationParts[0].Trim(), null, operationParts[1].Trim(), null, "-"));
                    }
                    else if (content[1].Contains('*'))
                    {
                        var operationParts = content[1].Split('*');
                        monkeys.Add(new ExpressionOperation(name, null, null, operationParts[0].Trim(), null, operationParts[1].Trim(), null, "*"));
                    }
                    else
                    {
                        var operationParts = content[1].Split('/');
                        monkeys.Add(new ExpressionOperation(name, null, null, operationParts[0].Trim(), null, operationParts[1].Trim(), null, "/"));
                    }
                }
            }
        }

        // Calculating
        // Note: need to ignore Root
        // and Human won't be in the list, but will be in the variables.
        while (true)
        {
            var numberMonkees = monkeys.Where(_ => _.Name != Root && _.Value is not null).ToList();

            // This sets values that have a literal value
            foreach (var numberMonkee in numberMonkees)
            {
                foreach (var needsLeft in monkeys.Where(_ => _.Name != Root && _.Value is null && _.Left == numberMonkee.Name && _.LeftValue is null))
                {
                    needsLeft.LeftValue = numberMonkee.Value;
                }

                foreach (var needsRight in monkeys.Where(_ => _.Name != Root && _.Value is null && _.Right == numberMonkee.Name && _.RightValue is null))
                {
                    needsRight.RightValue = numberMonkee.Value;
                }
            }

            // This sets values that have literal left and right values for the math computation.
            foreach (var computeMonkee in monkeys.Where(_ => _.Name != Root && _.Value is null && _.LeftValue is not null && _.RightValue is not null))
            {
                computeMonkee.Value = computeMonkee.Math switch
                {
                    "+" => computeMonkee.LeftValue + computeMonkee.RightValue,
                    "-" => computeMonkee.LeftValue - computeMonkee.RightValue,
                    "*" => computeMonkee.LeftValue * computeMonkee.RightValue,
                    "/" => computeMonkee.LeftValue / computeMonkee.RightValue,
                    _ => throw new UnreachableException()
                };
            }

            // This sets expressions where Human is involved and math is involved.
            // Or the monkey named by either left or right has an expression containing Human
            foreach (var humanUsesMonkee in monkeys.Where(_ => _.Name != Root && _.Expression is null && 
                _.Math is not null &&
                (_.Left == Human || 
                    (monkeys.Single(m => m.Name == _.Left)!.Expression?.Contains(Human) ?? false) ||
                    (monkeys.Single(m => m.Name == _.Left)!.Value is not null)) &&
                (_.Right == Human || 
                    (monkeys.Single(m => m.Name == _.Right)!.Expression?.Contains(Human) ?? false) ||
                    (monkeys.Single(m => m.Name == _.Right)!.Value is not null))))
            {
                var leftSide = humanUsesMonkee.Left! == Human ? Human :
                    humanUsesMonkee.LeftValue is not null ? humanUsesMonkee.LeftValue.ToString() : 
                        monkeys.Single(m => m.Name == humanUsesMonkee.Left)!.Expression! ?? monkeys.Single(m => m.Name == humanUsesMonkee.Left)!.Value!.Value.ToString();
                var rightSide = humanUsesMonkee.Right! == Human ? Human :
                    humanUsesMonkee.RightValue is not null ? humanUsesMonkee.RightValue.ToString() : 
                        monkeys.Single(m => m.Name == humanUsesMonkee.Right)!.Expression! ?? monkeys.Single(m => m.Name == humanUsesMonkee.Right)!.Value!.Value.ToString();

                if(leftSide is null || rightSide is null)
                {
                    Debugger.Break();
                }

                humanUsesMonkee.Expression = $"({leftSide} {humanUsesMonkee.Math} {rightSide})";
            }

            var rootMonkey = monkeys.Single(_ => _.Name == "root");
            var rootLeftExpression = monkeys.Find(_ => _.Name == rootMonkey.Left)!;
            var rootRightExpression = monkeys.Find(_ => _.Name == rootMonkey.Right)!;

            if ((rootLeftExpression.Expression is not null || rootLeftExpression.Value is not null) && 
                (rootRightExpression.Expression is not null || rootRightExpression.Value is not null))
            {
                return (rootLeftExpression.Expression ?? rootLeftExpression.Value!.Value.ToString(), 
                    rootRightExpression.Expression ?? rootRightExpression.Value!.Value.ToString());
            }
        }
    }
}

public class ExpressionOperation
{
    public ExpressionOperation(string name, long? value, string? expression, string? left, long? leftValue, string? right, long? rightValue, string? math)
    {
        this.Name = name;
        this.Value = value;
        this.Expression = expression;
        this.Left = left;
        this.LeftValue = leftValue;
        this.Right = right;
        this.RightValue = rightValue;
        this.Math = math;
    }

    public string Name { get; }
    public long? Value { get; set; }
    public string? Expression { get; set; }
    public string? Left { get; }
    public long? LeftValue { get; set; }
    public string? Right { get; }
    public long? RightValue { get; set; }
    public string? Math { get; set; }
}

public class Operation
{
    public Operation(string name, long? value, string? left, long? leftValue, string? right, long? rightValue, string? math)
    {
        this.Name = name;
        this.Value = value;
        this.Left = left;
        this.LeftValue = leftValue;
        this.Right = right;
        this.RightValue = rightValue;
        this.Math = math;
    }

    public string Name { get; }
    public long? Value { get; set; }
    public string? Left { get; set; }
    public long? LeftValue { get; set; }
    public string? Right { get; set; }
    public long? RightValue { get; set; }
    public string? Math { get; set; }
}
