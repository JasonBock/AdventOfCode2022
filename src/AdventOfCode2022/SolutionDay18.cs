using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022.Day18;

public static class SolutionDay18
{
    public static int GetTotalSurfaceArea(string[] coordinates)
    {
        var cubes = new List<Cube>(coordinates.Select(Cube.Parse));
        var totalSurfaceArea = 0;

        foreach (var cube in cubes)
        {
            var cubeSurfaceArea = 6;

            // Look at each side. If there's one in the list that's touching it
            // flip it's corresponding side.
            if (cube.IsXPositiveCovered)
            {
                cubeSurfaceArea--;
            }
            else
            {
                var touchingCube = cubes.Find(_ => _.X == cube.X + 1 && _.Y == cube.Y && _.Z == cube.Z);

                if (touchingCube is not null)
                {
                    cube.IsXPositiveCovered = true;
                    touchingCube.IsXNegativeCovered = true;
                    cubeSurfaceArea--;
                }
            }

            if (cube.IsXNegativeCovered)
            {
                cubeSurfaceArea--;
            }
            else
            {
                var touchingCube = cubes.Find(_ => _.X == cube.X - 1 && _.Y == cube.Y && _.Z == cube.Z);

                if (touchingCube is not null)
                {
                    cube.IsXNegativeCovered = true;
                    touchingCube.IsXPositiveCovered = true;
                    cubeSurfaceArea--;
                }
            }

            if (cube.IsYPositiveCovered)
            {
                cubeSurfaceArea--;
            }
            else
            {
                var touchingCube = cubes.Find(_ => _.X == cube.X && _.Y == cube.Y + 1 && _.Z == cube.Z);

                if (touchingCube is not null)
                {
                    cube.IsYPositiveCovered = true;
                    touchingCube.IsYNegativeCovered = true;
                    cubeSurfaceArea--;
                }
            }

            if (cube.IsYNegativeCovered)
            {
                cubeSurfaceArea--;
            }
            else
            {
                var touchingCube = cubes.Find(_ => _.X == cube.X && _.Y == cube.Y - 1 && _.Z == cube.Z);

                if (touchingCube is not null)
                {
                    cube.IsYNegativeCovered = true;
                    touchingCube.IsYPositiveCovered = true;
                    cubeSurfaceArea--;
                }
            }

            if (cube.IsZPositiveCovered)
            {
                cubeSurfaceArea--;
            }
            else
            {
                var touchingCube = cubes.Find(_ => _.X == cube.X && _.Y == cube.Y && _.Z == cube.Z + 1);

                if (touchingCube is not null)
                {
                    cube.IsZPositiveCovered = true;
                    touchingCube.IsZNegativeCovered = true;
                    cubeSurfaceArea--;
                }
            }

            if (cube.IsZNegativeCovered)
            {
                cubeSurfaceArea--;
            }
            else
            {
                var touchingCube = cubes.Find(_ => _.X == cube.X && _.Y == cube.Y && _.Z == cube.Z - 1);

                if (touchingCube is not null)
                {
                    cube.IsZNegativeCovered = true;
                    touchingCube.IsZPositiveCovered = true;
                    cubeSurfaceArea--;
                }
            }

            totalSurfaceArea += cubeSurfaceArea;
        }

        return totalSurfaceArea;
    }

    public static int GetTotalSurfaceAreaWithAirBubbles(string[] coordinates)
    {
        var cubes = new List<Cube>(coordinates.Select(Cube.Parse));
        var airBubbles = new List<AirBubble>();

        var dropletXMin = cubes.Min(_ => _.X);
        var dropletXMax = cubes.Max(_ => _.X);
        var dropletYMin = cubes.Min(_ => _.Y);
        var dropletYMax = cubes.Max(_ => _.Y);
        var dropletZMin = cubes.Min(_ => _.Z);
        var dropletZMax = cubes.Max(_ => _.Z);

        for (var x = dropletXMin; x <= dropletXMax; x++)
        {
            for (var y = dropletYMin; y <= dropletYMax; y++)
            {
                for (var z = dropletZMin; z <= dropletZMax; z++)
                {
                    if (cubes.Any(_ => _.X < x && _.Y == y && _.Z == z) &&
                        cubes.Any(_ => _.X > x && _.Y == y && _.Z == z) &&
                        cubes.Any(_ => _.X == x && _.Y < y && _.Z == z) &&
                        cubes.Any(_ => _.X == x && _.Y > y && _.Z == z) &&
                        cubes.Any(_ => _.X == x && _.Y == y && _.Z < z) &&
                        cubes.Any(_ => _.X == x && _.Y == y && _.Z > z) &&
                        !cubes.Any(_ => _.X == x && _.Y == y && _.Z == z))
                    {
                        airBubbles.Add(new(x, y, z));
                    }
                }
            }
        }

        var currentAirBubbleCount = airBubbles.Count;

        while (true)
        {
            // Now we have to find and remove the air bubbles that
            // are actually exposed to air and not in the droplet,
            // and keep looping until we got rid of all of them.
            for (var a = airBubbles.Count - 1; a >= 0; a--)
            {
                var airBubble = airBubbles[a];
                var isEnclosed = true;

                for (var x = airBubble.X - 1; x > dropletXMin; x--)
                {
                    if (cubes.Any(_ => _.X == x && _.Y == airBubble.Y && _.Z == airBubble.Z))
                    {
                        // We're good.
                        break;
                    }
                    else if (airBubbles.Any(_ => _.X == x && _.Y == airBubble.Y && _.Z == airBubble.Z))
                    {
                        // We're good, but we need to keep going.
                        continue;
                    }
                    else
                    {
                        isEnclosed = false;
                        break;
                    }
                }

                if (!isEnclosed)
                {
                    airBubbles.RemoveAt(a);
                    break;
                }

                for (var x = airBubble.X + 1; x < dropletXMax; x++)
                {
                    if (cubes.Any(_ => _.X == x && _.Y == airBubble.Y && _.Z == airBubble.Z))
                    {
                        // We're good.
                        break;
                    }
                    else if (airBubbles.Any(_ => _.X == x && _.Y == airBubble.Y && _.Z == airBubble.Z))
                    {
                        // We're good, but we need to keep going.
                        continue;
                    }
                    else
                    {
                        isEnclosed = false;
                        break;
                    }
                }

                if (!isEnclosed)
                {
                    airBubbles.RemoveAt(a);
                    break;
                }

                for (var y = airBubble.Y - 1; y > dropletYMin; y--)
                {
                    if (cubes.Any(_ => _.X == airBubble.X && _.Y == y && _.Z == airBubble.Z))
                    {
                        // We're good.
                        break;
                    }
                    else if (airBubbles.Any(_ => _.X == airBubble.X && _.Y == y && _.Z == airBubble.Z))
                    {
                        // We're good, but we need to keep going.
                        continue;
                    }
                    else
                    {
                        isEnclosed = false;
                        break;
                    }
                }

                if (!isEnclosed)
                {
                    airBubbles.RemoveAt(a);
                    break;
                }

                for (var y = airBubble.Y + 1; y < dropletYMax; y++)
                {
                    if (cubes.Any(_ => _.X == airBubble.X && _.Y == y && _.Z == airBubble.Z))
                    {
                        // We're good.
                        break;
                    }
                    else if (airBubbles.Any(_ => _.X == airBubble.X && _.Y == y && _.Z == airBubble.Z))
                    {
                        // We're good, but we need to keep going.
                        continue;
                    }
                    else
                    {
                        isEnclosed = false;
                        break;
                    }
                }

                if (!isEnclosed)
                {
                    airBubbles.RemoveAt(a);
                    break;
                }

                for (var z = airBubble.Z - 1; z > dropletZMin; z--)
                {
                    if (cubes.Any(_ => _.X == airBubble.X && _.Y == airBubble.Y && _.Z == z))
                    {
                        // We're good.
                        break;
                    }
                    else if (airBubbles.Any(_ => _.X == airBubble.X && _.Y == airBubble.Y && _.Z == z))
                    {
                        // We're good, but we need to keep going.
                        continue;
                    }
                    else
                    {
                        isEnclosed = false;
                        break;
                    }
                }

                if (!isEnclosed)
                {
                    airBubbles.RemoveAt(a);
                    break;
                }

                for (var z = airBubble.Z + 1; z < dropletZMax; z++)
                {
                    if (cubes.Any(_ => _.X == airBubble.X && _.Y == airBubble.Y && _.Z == z))
                    {
                        // We're good.
                        break;
                    }
                    else if (airBubbles.Any(_ => _.X == airBubble.X && _.Y == airBubble.Y && _.Z == z))
                    {
                        // We're good, but we need to keep going.
                        continue;
                    }
                    else
                    {
                        isEnclosed = false;
                        break;
                    }
                }

                if (!isEnclosed)
                {
                    airBubbles.RemoveAt(a);
                    break;
                }
            }

            if(airBubbles.Count == currentAirBubbleCount)
            {
                break;
            }
            else
            {
                currentAirBubbleCount = airBubbles.Count;
            }
        }

        var totalSurfaceArea = 0;

        foreach (var cube in cubes)
        {
            var cubeSurfaceArea = 6;

            // Look at each side. If there's one in the list that's touching it
            // flip it's corresponding side.
            // If there's an air bubble on that side, we also consider it covered.
            if (cube.IsXPositiveCovered)
            {
                cubeSurfaceArea--;
            }
            else
            {
                var touchingCube = cubes.Find(_ => _.X == cube.X + 1 && _.Y == cube.Y && _.Z == cube.Z);

                if (touchingCube is not null)
                {
                    cube.IsXPositiveCovered = true;
                    touchingCube.IsXNegativeCovered = true;
                    cubeSurfaceArea--;
                }
                else
                {
                    var airBubble = airBubbles.Find(_ => _.X == cube.X + 1 && _.Y == cube.Y && _.Z == cube.Z);

                    if (airBubble is not null)
                    {
                        cube.IsXPositiveCovered = true;
                        cubeSurfaceArea--;
                    }
                }
            }

            if (cube.IsXNegativeCovered)
            {
                cubeSurfaceArea--;
            }
            else
            {
                var touchingCube = cubes.Find(_ => _.X == cube.X - 1 && _.Y == cube.Y && _.Z == cube.Z);

                if (touchingCube is not null)
                {
                    cube.IsXNegativeCovered = true;
                    touchingCube.IsXPositiveCovered = true;
                    cubeSurfaceArea--;
                }
                else
                {
                    var airBubble = airBubbles.Find(_ => _.X == cube.X - 1 && _.Y == cube.Y && _.Z == cube.Z);

                    if (airBubble is not null)
                    {
                        cube.IsXNegativeCovered = true;
                        cubeSurfaceArea--;
                    }
                }
            }

            if (cube.IsYPositiveCovered)
            {
                cubeSurfaceArea--;
            }
            else
            {
                var touchingCube = cubes.Find(_ => _.X == cube.X && _.Y == cube.Y + 1 && _.Z == cube.Z);

                if (touchingCube is not null)
                {
                    cube.IsYPositiveCovered = true;
                    touchingCube.IsYNegativeCovered = true;
                    cubeSurfaceArea--;
                }
                else
                {
                    var airBubble = airBubbles.Find(_ => _.X == cube.X && _.Y == cube.Y + 1 && _.Z == cube.Z);

                    if (airBubble is not null)
                    {
                        cube.IsYPositiveCovered = true;
                        cubeSurfaceArea--;
                    }
                }
            }

            if (cube.IsYNegativeCovered)
            {
                cubeSurfaceArea--;
            }
            else
            {
                var touchingCube = cubes.Find(_ => _.X == cube.X && _.Y == cube.Y - 1 && _.Z == cube.Z);

                if (touchingCube is not null)
                {
                    cube.IsYNegativeCovered = true;
                    touchingCube.IsYPositiveCovered = true;
                    cubeSurfaceArea--;
                }
                else
                {
                    var airBubble = airBubbles.Find(_ => _.X == cube.X && _.Y == cube.Y - 1 && _.Z == cube.Z);

                    if (airBubble is not null)
                    {
                        cube.IsYNegativeCovered = true;
                        cubeSurfaceArea--;
                    }
                }
            }

            if (cube.IsZPositiveCovered)
            {
                cubeSurfaceArea--;
            }
            else
            {
                var touchingCube = cubes.Find(_ => _.X == cube.X && _.Y == cube.Y && _.Z == cube.Z + 1);

                if (touchingCube is not null)
                {
                    cube.IsZPositiveCovered = true;
                    touchingCube.IsZNegativeCovered = true;
                    cubeSurfaceArea--;
                }
                else
                {
                    var airBubble = airBubbles.Find(_ => _.X == cube.X && _.Y == cube.Y && _.Z == cube.Z + 1);

                    if (airBubble is not null)
                    {
                        cube.IsZPositiveCovered = true;
                        cubeSurfaceArea--;
                    }
                }
            }

            if (cube.IsZNegativeCovered)
            {
                cubeSurfaceArea--;
            }
            else
            {
                var touchingCube = cubes.Find(_ => _.X == cube.X && _.Y == cube.Y && _.Z == cube.Z - 1);

                if (touchingCube is not null)
                {
                    cube.IsZNegativeCovered = true;
                    touchingCube.IsZPositiveCovered = true;
                    cubeSurfaceArea--;
                }
                else
                {
                    var airBubble = airBubbles.Find(_ => _.X == cube.X && _.Y == cube.Y && _.Z == cube.Z - 1);

                    if (airBubble is not null)
                    {
                        cube.IsZNegativeCovered = true;
                        cubeSurfaceArea--;
                    }
                }
            }

            totalSurfaceArea += cubeSurfaceArea;
        }

        return totalSurfaceArea;
    }
}

public sealed class Cube
    : IEquatable<Cube>
{
    public Cube(int x, int y, int z) =>
        (this.X, this.Y, this.Z) = (x, y, z);

    public static Cube Parse(string input)
    {
        var coordinates = input.Split(',');
        return new Cube(int.Parse(coordinates[0]), int.Parse(coordinates[1]), int.Parse(coordinates[2]));
    }

    public bool Equals(Cube? other) =>
        other is not null && this.X == other.X && this.Y == other.Y && this.Z == other.Z;

    public bool IsXPositiveCovered { get; set; }
    public bool IsXNegativeCovered { get; set; }
    public bool IsYPositiveCovered { get; set; }
    public bool IsYNegativeCovered { get; set; }
    public bool IsZPositiveCovered { get; set; }
    public bool IsZNegativeCovered { get; set; }
    public int X { get; }
    public int Y { get; }
    public int Z { get; }
}

public record AirBubble(int X, int Y, int Z);