using AdventOfCode2022.Day2;
using System.Diagnostics;
using Xunit;

namespace AdventOfCode2022.Tests;

public static class SolutionDay2Tests
{
    [Theory]
    [InlineData("A", Choices.Rock)]
    [InlineData("B", Choices.Paper)]
    [InlineData("C", Choices.Scissors)]
    [InlineData("X", Choices.Rock)]
    [InlineData("Y", Choices.Paper)]
    [InlineData("Z", Choices.Scissors)]
    public static void GetValue(string choice, int expectedValue) =>
        Assert.Equal(expectedValue, SolutionDay2.GetValue(choice));

    [Fact]
    public static void GetValueWithUnexpectedValue() =>
        Assert.Throws<UnreachableException>(() => SolutionDay2.GetValue("Q"));

    [Theory]
    [InlineData(Choices.Rock, Choices.Rock, Outcome.Tie)]
    [InlineData(Choices.Rock, Choices.Paper, Outcome.Win)]
    [InlineData(Choices.Rock, Choices.Scissors, Outcome.Loss)]
    [InlineData(Choices.Paper, Choices.Rock, Outcome.Loss)]
    [InlineData(Choices.Paper, Choices.Paper, Outcome.Tie)]
    [InlineData(Choices.Paper, Choices.Scissors, Outcome.Win)]
    [InlineData(Choices.Scissors, Choices.Rock, Outcome.Win)]
    [InlineData(Choices.Scissors, Choices.Paper, Outcome.Loss)]
    [InlineData(Choices.Scissors, Choices.Scissors, Outcome.Tie)]
    public static void GetOutcome(int opponent, int you, int expectedValue) =>
        Assert.Equal(expectedValue, SolutionDay2.GetOutcome(opponent, you));

    [Theory]
    [InlineData(Choices.Rock, 0)]
    [InlineData(Choices.Paper, 0)]
    [InlineData(Choices.Scissors, 0)]
    [InlineData(0, Choices.Rock)]
    [InlineData(0, Choices.Paper)]
    [InlineData(0, Choices.Scissors)]
    public static void GetOutcomeWithUnexpectedValue(int opponent, int you) =>
        Assert.Throws<UnreachableException>(() => SolutionDay2.GetOutcome(opponent, you));

    [Theory]
    [MemberData(nameof(StrategyGuide))]
    public static void Run(string[] strategyGuide, int expectedValue) =>
        Assert.Equal(expectedValue, SolutionDay2.Run(strategyGuide));

    [Theory]
    [InlineData("A", "X", Choices.Rock, Outcome.Loss)]
    [InlineData("B", "Y", Choices.Paper, Outcome.Tie)]
    [InlineData("C", "Z", Choices.Scissors, Outcome.Win)]
    public static void GetValuesCorrectly(string opponentChoice, string yourOutcome, int expectedOpponentChoiceValue, int expectedYourOutcomeValue)
    {
        (var opponentChoiceResult, var yourOutcomeResult) = SolutionDay2.GetValuesCorrectly(opponentChoice, yourOutcome);

        Assert.Multiple(
            () => Assert.Equal(expectedOpponentChoiceValue, opponentChoiceResult),
            () => Assert.Equal(expectedYourOutcomeValue, yourOutcomeResult));
    }

    [Theory]
    [InlineData("Q", "X")]
    [InlineData("A", "Q")]
    public static void GetValuesCorrectlyWithUnexpectedValue(string opponentChoice, string yourOutcome) => 
        Assert.Throws<UnreachableException>(() => SolutionDay2.GetValuesCorrectly(opponentChoice, yourOutcome));

    [Theory]
    [InlineData(Choices.Rock, Outcome.Loss, Choices.Scissors)]
    [InlineData(Choices.Rock, Outcome.Tie, Choices.Rock)]
    [InlineData(Choices.Rock, Outcome.Win, Choices.Paper)]
    [InlineData(Choices.Paper, Outcome.Loss, Choices.Rock)]
    [InlineData(Choices.Paper, Outcome.Tie, Choices.Paper)]
    [InlineData(Choices.Paper, Outcome.Win, Choices.Scissors)]
    [InlineData(Choices.Scissors, Outcome.Loss, Choices.Paper)]
    [InlineData(Choices.Scissors, Outcome.Tie, Choices.Scissors)]
    [InlineData(Choices.Scissors, Outcome.Win, Choices.Rock)]
    public static void GetChoiceCorrectly(int opponentChoice, int yourOutcome, int expectedYourChoice) => 
        Assert.Equal(expectedYourChoice, SolutionDay2.GetChoiceCorrectly(opponentChoice, yourOutcome));

    [Theory]
    [InlineData(Choices.Rock, -1)]
    [InlineData(Choices.Paper, -1)]
    [InlineData(Choices.Scissors, -1)]
    [InlineData(-1, Outcome.Loss)]
    [InlineData(-1, Outcome.Tie)]
    [InlineData(-1, Outcome.Win)]
    public static void GetChoiceCorrectlyWithUnexpectedValue(int opponentChoice, int yourOutcome) =>
        Assert.Throws<UnreachableException>(() => SolutionDay2.GetChoiceCorrectly(opponentChoice, yourOutcome));

    [Theory]
    [MemberData(nameof(CorrectStrategyGuide))]
    public static void RunCorrectly(string[] strategyGuide, int expectedValue) =>
        Assert.Equal(expectedValue, SolutionDay2.RunCorrectly(strategyGuide));

    public static IEnumerable<object[]> StrategyGuide()
    {
        yield return new object[] { new[] { "A Y", "B X", "C Z" }, 15 };
    }

    public static IEnumerable<object[]> CorrectStrategyGuide()
    {
        yield return new object[] { new[] { "A Y", "B X", "C Z" }, 12 };
    }
}