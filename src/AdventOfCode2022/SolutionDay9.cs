using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AdventOfCode2022.Day9;

public static class SolutionDay9
{
    public static int GetTailVisitationPositionCountHardcoded(IEnumerable<string> commands)
    {
        var head = new Knot();
        var tail = new Knot();

        var visits = new HashSet<Knot>
        {
            tail
        };

        foreach (var command in commands)
        {
            var commandParts = command.Split(' ');
            var direction = commandParts[0];
            var length = int.Parse(commandParts[1]);

            if (direction == "R")
            {
                for (var p = 0; p < length; p++)
                {
                    head = head.AddX(1);

                    if (head != tail)
                    {
                        if (tail.X < (head.X - 1))
                        {
                            tail = new Knot(head.X - 1, head.Y);
                        }
                    }

                    visits.Add(tail);
                }
            }
            else if (direction == "L")
            {
                for (var p = 0; p < length; p++)
                {
                    head = head.AddX(-1);

                    if (head != tail)
                    {
                        if (tail.X > (head.X + 1))
                        {
                            tail = new Knot(head.X + 1, head.Y);
                        }
                    }

                    visits.Add(tail);
                }
            }
            else if (direction == "U")
            {
                for (var p = 0; p < length; p++)
                {
                    head = head.AddY(1);

                    if (head != tail)
                    {
                        if (tail.Y < (head.Y - 1))
                        {
                            tail = new Knot(head.X, head.Y - 1);
                        }
                    }

                    visits.Add(tail);
                }
            }
            else if (direction == "D")
            {
                for (var p = 0; p < length; p++)
                {
                    head = head.AddY(-1);

                    if (head != tail)
                    {
                        if (tail.Y > (head.Y + 1))
                        {
                            tail = new Knot(head.X, head.Y + 1);
                        }
                    }

                    visits.Add(tail);
                }
            }
            else
            {
                throw new UnreachableException();
            }
        }

        return visits.Count;
    }

    public static Knot GetNewKnotPosition(Knot knot, string direction) =>
        direction switch
        {
            "R" => new Knot(knot.X + 1, knot.Y),
            "L" => new Knot(knot.X - 1, knot.Y),
            "U" => new Knot(knot.X, knot.Y + 1),
            "D" => new Knot(knot.X, knot.Y - 1),
            _ => throw new UnreachableException()
        };

    public static int GetTailVisitationPositionCount(IEnumerable<string> commands, int ropeKnots)
    {
        var rope = new Knot[ropeKnots];

        var visits = new HashSet<Knot>
        {
            rope[^1]
        };

        foreach (var command in commands)
        {
            var commandParts = command.Split(' ');
            var headDirection = commandParts[0];
            var length = int.Parse(commandParts[1]);

            for (var l = 0; l < length; l++)
            {
                var tailsDirection = headDirection;
                Knot? translation = null;

                rope[0] = SolutionDay9.GetNewKnotPosition(rope[0], headDirection);

                for (var p = 1; p < ropeKnots; p++)
                {
                    var tail = rope[p];
                    var head = rope[p - 1];

                    // Is there a disconnect?
                    if (int.Abs(tail.Y - head.Y) > 1 ||
                        int.Abs(tail.X - head.X) > 1)
                    {
                        // Is there a sharing?
                        if (tail.Y == head.Y ||
                            tail.X == head.X)
                        {
                            tailsDirection = tail.X == head.X ?
                                (tail.Y < head.Y ? "U" : "D") :
                                (tail.X < head.X ? "R" : "L");
                            rope[p] = SolutionDay9.GetNewKnotPosition(tail, tailsDirection);
                            translation = null;
                        }
                        else
                        {
                            // Is a translation defined?
                            if (translation is null)
                            {
                                var xTranslation = head.X > tail.X ? 1 : -1;
                                var yTranslation = head.Y > tail.Y ? 1 : -1;
                                translation = new Knot(xTranslation, yTranslation);

                                var newTail = SolutionDay9.GetNewKnotPosition(tail, tailsDirection);

                                if(tailsDirection == "R" || tailsDirection == "L")
                                {
                                    rope[p] = new Knot(newTail.X, head.Y);
                                }
                                else
                                {
                                    rope[p] = new Knot(head.X, newTail.Y);
                                }
                            }
                            else
                            {
                                rope[p] = new Knot(rope[p].X + translation.Value.X, rope[p].Y + translation.Value.Y);
                            }
                        }

                        visits.Add(rope[^1]);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        return visits.Count;
    }
}

public record struct Knot(int X, int Y)
{
    public Knot AddX(int x) =>
        new Knot(this.X + x, this.Y);

    public Knot AddY(int y) =>
        new Knot(this.X, this.Y + y);
}