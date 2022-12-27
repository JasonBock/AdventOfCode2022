using System.Diagnostics;

namespace AdventOfCode2022.Day2;

public static class SolutionDay2
{
    public static int GetValue(string choice) =>
        choice switch
        {
            "A" or "X" => Choices.Rock,
            "B" or "Y" => Choices.Paper,
            "C" or "Z" => Choices.Scissors,
            _ => throw new UnreachableException()
        };

    public static int GetOutcome(int opponentChoice, int youChoice) => 
        opponentChoice switch
        {
            Choices.Rock => youChoice switch
            {
                Choices.Rock => Outcome.Tie,
                Choices.Paper => Outcome.Win,
                Choices.Scissors => Outcome.Loss,
                _ => throw new UnreachableException()
            },
            Choices.Paper => youChoice switch
            {
                Choices.Rock => Outcome.Loss,
                Choices.Paper => Outcome.Tie,
                Choices.Scissors => Outcome.Win,
                _ => throw new UnreachableException()
            },
            Choices.Scissors => youChoice switch
            {
                Choices.Rock => Outcome.Win,
                Choices.Paper => Outcome.Loss,
                Choices.Scissors => Outcome.Tie,
                _ => throw new UnreachableException()
            },
            _ => throw new UnreachableException()
        };

    public static int Run(string[] strategy) 
    {
        var score = 0;

        for(var i = 0; i < strategy.Length; i++) 
        { 
            var round = strategy[i].Split(' ');
            var opponent = SolutionDay2.GetValue(round[0]);
            var you = SolutionDay2.GetValue(round[1]);

            var outcome = SolutionDay2.GetOutcome(opponent, you);

            score += outcome + you;
        }

        return score;
    }

    public static (int opponentChoice, int yourOutcome) GetValuesCorrectly(string opponentChoice, string yourOutcome) => 
        (opponentChoice == "A" ? Choices.Rock : opponentChoice == "B" ? Choices.Paper : opponentChoice == "C" ? Choices.Scissors : throw new UnreachableException(),
            yourOutcome == "X" ? Outcome.Loss : yourOutcome == "Y" ? Outcome.Tie : yourOutcome == "Z" ? Outcome.Win : throw new UnreachableException());

    public static int GetChoiceCorrectly(int opponentChoice, int yourOutcome) =>
        opponentChoice switch
        {
            Choices.Rock => yourOutcome switch
            {
                Outcome.Loss => Choices.Scissors,
                Outcome.Tie => Choices.Rock,
                Outcome.Win => Choices.Paper,
                _ => throw new UnreachableException()
            },
            Choices.Paper => yourOutcome switch
            {
                Outcome.Loss => Choices.Rock,
                Outcome.Tie => Choices.Paper,
                Outcome.Win => Choices.Scissors,
                _ => throw new UnreachableException()
            },
            Choices.Scissors => yourOutcome switch
            {
                Outcome.Loss => Choices.Paper,
                Outcome.Tie => Choices.Scissors,
                Outcome.Win => Choices.Rock,
                _ => throw new UnreachableException()
            },
            _ => throw new UnreachableException()
        };

    public static int RunCorrectly(string[] strategy)
    {
        var score = 0;

        for (var i = 0; i < strategy.Length; i++)
        {
            var round = strategy[i].Split(' ');
            (var opponentChoice, var yourOutcome) = SolutionDay2.GetValuesCorrectly(round[0], round[1]);

            var yourChoice = SolutionDay2.GetChoiceCorrectly(opponentChoice, yourOutcome);

            score += yourOutcome + yourChoice;
        }

        return score;
    }
}

public static class Choices
{
    public const int Rock = 1;
    public const int Paper = 2;
    public const int Scissors = 3;
}

public static class Outcome
{
    public const int Loss = 0;
    public const int Tie = 3;
    public const int Win = 6;
}