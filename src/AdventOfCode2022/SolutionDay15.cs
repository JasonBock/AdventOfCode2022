using System.Collections.Generic;
using System.Diagnostics;

namespace AdventOfCode2022.Day15;

public static class SolutionDay15
{
    public static long GetTuningFrequency(string[] sensorReports, ((int x, int y) min, (int x, int y) max) grid)
    {
        var pairs = new List<Pair>();

        foreach (var sensorReport in sensorReports)
        {
            pairs.Add(SolutionDay15.Parse(sensorReport));
        }

        for (var y = grid.min.y; y < grid.max.y; y++)
        {
            var x = grid.min.x;

            for (var p = 0; p < pairs.Count; p++)
            {
                var pair = pairs[p];

                if ((pair.Sensor.X == x && pair.Sensor.Y == y) ||
                    (pair.Beacon.X == x && pair.Beacon.Y == y))
                {
                    // We're on a sensor or beacon, doesn't matter, move over.
                    // and back up the pair
                    x++;
                    p--;
                }
                else if (pair.InRange(x, y))
                {
                    // Figure out where the search moves forward to,
                    // and go back through all the pairs
                    x = (pair.Sensor.X + pair.Distance) - int.Abs(pair.Sensor.Y - y) + 1;
                    p = -1;
                }
            }

            if (x >= grid.min.x && x <= grid.max.x && y >= grid.min.y && y <= grid.max.y)
            {
                return (4000000L * x) + y;
            }
        }

        // Oops, we should've found something.
        throw new UnreachableException();
    }

    public static int GetNonBeaconLocations(string[] sensorReports, int lineY)
    {
        var noBeacon = 0;

        var pairs = new List<Pair>();
        int? minX = null;
        int? maxX = null;

        foreach (var sensorReport in sensorReports)
        {
            var pair = SolutionDay15.Parse(sensorReport);

            minX = minX is null ? pair.MinX : int.Min(minX.Value, pair.MinX);
            maxX = maxX is null ? pair.MaxX : int.Max(maxX.Value, pair.MaxX);

            pairs.Add(pair);
        }

        var beaconExistsCount = 0;

        for (var x = minX!.Value; x <= maxX!.Value; x++)
        {
            var foundContainment = false;

            foreach (var pair in pairs)
            {
                if (pair.Beacon.X == x && pair.Beacon.Y == lineY)
                {
                    beaconExistsCount++;
                }

                if (pair.InRange(x, lineY))
                {
                    foundContainment = true;
                    break;
                }
            }

            if (foundContainment)
            {
                noBeacon++;
            }
        }

        return noBeacon - beaconExistsCount;
    }

    public static Pair Parse(string sensorReport)
    {
        // Example:
        // Sensor at x=2, y=18: closest beacon is at x=-2, y=15

        // Split on the ':'.
        var pairs = sensorReport.Split(':');

        // For both parts, find the index of 'x' and 'y', move 2 over,
        // and int.Parse from there up to where the comma is for x, and ^0 for the y
        var sensor = pairs[0];
        var sensorXChar = sensor.IndexOf('x') + 2;
        var sensorX = int.Parse(sensor[sensorXChar..sensor.IndexOf(',', sensorXChar)]);
        var sensorYChar = sensor.IndexOf('y') + 2;
        var sensorY = int.Parse(sensor[sensorYChar..^0]);

        var beacon = pairs[1];
        var beaconXChar = beacon.IndexOf('x') + 2;
        var beaconX = int.Parse(beacon[beaconXChar..beacon.IndexOf(',', beaconXChar)]);
        var beaconYChar = beacon.IndexOf('y') + 2;
        var beaconY = int.Parse(beacon[beaconYChar..^0]);

        return new Pair(new Sensor(sensorX, sensorY), new Beacon(beaconX, beaconY));
    }
}

public record struct Beacon(int X, int Y);

public record struct Sensor(int X, int Y);

public record struct Pair
{
    public Pair(Sensor source, Beacon beacon)
    {
        (this.Sensor, this.Beacon) = (source, beacon);
        this.Distance = int.Abs(source.X - beacon.X) + int.Abs(source.Y - beacon.Y);
        this.MinX = source.X - this.Distance;
        this.MaxX = source.X + this.Distance;
        this.MinY = source.Y - this.Distance;
        this.MaxY = source.Y + this.Distance;
    }

    public bool InRange(int x, int y) =>
        int.Abs(this.Sensor.X - x) + int.Abs(this.Sensor.Y - y) <= this.Distance;

    public Beacon Beacon { get; }
    public int Distance { get; }
    public Sensor Sensor { get; }
    public int MaxX { get; }
    public int MaxY { get; }
    public int MinX { get; }
    public int MinY { get; }
}
