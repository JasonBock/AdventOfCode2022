using Spackle;

namespace AdventOfCode2022.Day4;

public static class SolutionDay4
{
    public static int Run(string[] assignmentPairs)
    {
        var containingAssignmentPairInstances = 0;

        for (var i = 0; i < assignmentPairs.Length; i++)
        {
            var assignmentPairValues = assignmentPairs[i].Split(',');
            var leftAssignmentPairValues = assignmentPairValues[0].Split('-');
            var rightAssignmentPairValues = assignmentPairValues[1].Split('-');

            var leftAssignment = new Range<int>(int.Parse(leftAssignmentPairValues[0]), int.Parse(leftAssignmentPairValues[1]));
            var rightAssignment = new Range<int>(int.Parse(rightAssignmentPairValues[0]), int.Parse(rightAssignmentPairValues[1]));

            var union = leftAssignment.Union(rightAssignment);

            if (union is not null &&
                ((union.Start == leftAssignment.Start &&
                union.End == leftAssignment.End) ||
                (union.Start == rightAssignment.Start &&
                union.End == rightAssignment.End)))
            {
                containingAssignmentPairInstances++;
            }
        }

        return containingAssignmentPairInstances;
    }

    public static int RunForOverlap(string[] assignmentPairs)
    {
        var containingAssignmentPairInstances = 0;

        for (var i = 0; i < assignmentPairs.Length; i++)
        {
            var assignmentPairValues = assignmentPairs[i].Split(',');
            var leftAssignmentPairValues = assignmentPairValues[0].Split('-');
            var rightAssignmentPairValues = assignmentPairValues[1].Split('-');

            var leftAssignment = new Range<int>(int.Parse(leftAssignmentPairValues[0]), int.Parse(leftAssignmentPairValues[1]));
            var rightAssignment = new Range<int>(int.Parse(rightAssignmentPairValues[0]), int.Parse(rightAssignmentPairValues[1]));

            if (leftAssignment.Union(rightAssignment) is not null)
            {
                containingAssignmentPairInstances++;
            }
        }

        return containingAssignmentPairInstances;
    }
}
