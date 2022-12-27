namespace AdventOfCode2022.Day8;

public static class SolutionDay8
{
    public static long GetVisibleTreeCount(string[] input) => 
        SolutionDay8.GetVisibleTreeCount(SolutionDay8.GetGrid(input));

    public static long GetHigestScenicScore(string[] input) =>
        SolutionDay8.GetHigestScenicScore(SolutionDay8.GetGrid(input));

    public static int[,] GetGrid(string[] input)
    {
        var grid = new int[input[0].Length, input.Length];

        for(var l = 0; l < input.Length; l++)
        {
            var line = input[l];

            for (var i = 0; i < line.Length; i++)
            {
                grid[l, i] = line[i] - '0';
            }
        }

        return grid;
    }

    public static long GetVisibleTreeCount(int[,] grid)
    {
        // We know that all of the outer trees are visible,
        // so we can start with (2 * dimension) + (2 * (dimension - 2))
        var dimension = (long)double.Sqrt(grid.Length);
        var count = (2 * dimension) + (2 * (dimension - 2));

        for(var i = 1; i < dimension - 1; i++)
        {
            for (var j = 1; j < dimension - 1; j++)
            {
                var tree = grid[i, j];

                // Now go in all 4 directions. If none of the trees
                // are the same value or bigger in any direction, add to the count.

                // Left
                var foundLeft = false;

                for (var direction = j - 1; direction >= 0; direction--)
                {
                    if (grid[i, direction] >= tree)
                    {
                        foundLeft = true;
                        break;
                    }
                }

                if(!foundLeft)
                {
                    count++;
                    continue;
                }

                // Right
                var foundRight = false;

                for (var direction = j + 1; direction < dimension; direction++)
                {
                    if (grid[i, direction] >= tree)
                    {
                        foundRight = true;
                        break;
                    }
                }

                if (!foundRight)
                {
                    count++;
                    continue;
                }

                // Up
                var foundUp = false;

                for (var direction = i - 1; direction >= 0; direction--)
                {
                    if (grid[direction, j] >= tree)
                    {
                        foundUp = true;
                        break;
                    }
                }

                if (!foundUp)
                {
                    count++;
                    continue;
                }

                // Down
                var foundDown = false;

                for (var direction = i + 1; direction < dimension; direction++)
                {
                    if (grid[direction, j] >= tree)
                    {
                        foundDown = true;
                        break;
                    }
                }

                if (!foundDown)
                {
                    count++;
                    continue;
                }
            }
        }

        return count;
    }

    public static long GetHigestScenicScore(int[,] grid)
    {
        var dimension = (long)double.Sqrt(grid.Length);

        var highestScenicScore = 0L;

        // We know that all of the outer trees have a score of 0,
        // so we can ignore them.
        for (var i = 1; i < dimension - 1; i++)
        {
            for (var j = 1; j < dimension - 1; j++)
            {
                var tree = grid[i, j];

                // Now go in all 4 directions. Once you find a tree
                // the same size or higher, stop

                // Left
                var leftCount = 0L;

                for (var direction = j - 1; direction >= 0; direction--)
                {
                    leftCount++;

                    if (grid[i, direction] >= tree)
                    {
                        break;
                    }
                }

                // Right
                var rightCount = 0L;

                for (var direction = j + 1; direction < dimension; direction++)
                {
                    rightCount++;

                    if (grid[i, direction] >= tree)
                    {
                        break;
                    }
                }

                // Up
                var upCount = 0L;

                for (var direction = i - 1; direction >= 0; direction--)
                {
                    upCount++;

                    if (grid[direction, j] >= tree)
                    {
                        break;
                    }
                }

                // Down
                var downCount = 0L;

                for (var direction = i + 1; direction < dimension; direction++)
                {
                    downCount++;

                    if (grid[direction, j] >= tree)
                    {
                        break;
                    }
                }

                var currentScenicScore = leftCount * rightCount * upCount * downCount;

                if(currentScenicScore > highestScenicScore)
                {
                    highestScenicScore = currentScenicScore;
                }
            }
        }

        return highestScenicScore;
    }
}
