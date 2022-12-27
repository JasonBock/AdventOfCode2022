using AdventOfCode2022.Day15;
using Xunit;

namespace AdventOfCode2022.Tests;

public static class SolutionDay15Tests
{
    [Fact]
    public static void Pair()
    {
        var source = new Sensor(2, 18);
        var beacon = new Beacon(-2, 15);
        var pair = new Pair(source, beacon);

        Assert.Equal(source, pair.Sensor);
        Assert.Equal(beacon, pair.Beacon);
        Assert.Equal(7, pair.Distance);

        Assert.Equal(-5, pair.MinX);
        Assert.Equal(9, pair.MaxX);
        Assert.Equal(11, pair.MinY);
        Assert.Equal(25, pair.MaxY);
    }

    [Theory]
    [InlineData(2, 15, true)]
    [InlineData(2, 11, true)]
    [InlineData(2, 10, false)]
    [InlineData(5, 18, true)]
    [InlineData(9, 18, true)]
    [InlineData(10, 18, false)]
    public static void IsLocationInRange(int x, int y, bool expectedResult)
    {
        var source = new Sensor(2, 18);
        var beacon = new Beacon(-2, 15);
        var pair = new Pair(source, beacon);
        Assert.Equal(expectedResult, pair.InRange(x, y));
    }

    [Fact]
    public static void Parse()
    {
        var pair = SolutionDay15.Parse("Sensor at x=2, y=18: closest beacon is at x=-2, y=15");

        Assert.Equal(2, pair.Sensor.X);
        Assert.Equal(18, pair.Sensor.Y);
        Assert.Equal(-2, pair.Beacon.X);
        Assert.Equal(15, pair.Beacon.Y);
        Assert.Equal(7, pair.Distance);

        Assert.Equal(-5, pair.MinX);
        Assert.Equal(9, pair.MaxX);
        Assert.Equal(11, pair.MinY);
        Assert.Equal(25, pair.MaxY);
    }

    [Fact]
    public static void GetNonBeaconLocations()
    {
        var sensorReports = new[]
        {
            "Sensor at x=2, y=18: closest beacon is at x=-2, y=15",
            "Sensor at x=9, y=16: closest beacon is at x=10, y=16",
            "Sensor at x=13, y=2: closest beacon is at x=15, y=3",
            "Sensor at x=12, y=14: closest beacon is at x=10, y=16",
            "Sensor at x=10, y=20: closest beacon is at x=10, y=16",
            "Sensor at x=14, y=17: closest beacon is at x=10, y=16",
            "Sensor at x=8, y=7: closest beacon is at x=2, y=10",
            "Sensor at x=2, y=0: closest beacon is at x=2, y=10",
            "Sensor at x=0, y=11: closest beacon is at x=2, y=10",
            "Sensor at x=20, y=14: closest beacon is at x=25, y=17",
            "Sensor at x=17, y=20: closest beacon is at x=21, y=22",
            "Sensor at x=16, y=7: closest beacon is at x=15, y=3",
            "Sensor at x=14, y=3: closest beacon is at x=15, y=3",
            "Sensor at x=20, y=1: closest beacon is at x=15, y=3"
        };

        var noBeacons = SolutionDay15.GetNonBeaconLocations(sensorReports, 10);
        Assert.Equal(26, noBeacons);
    }

    [Fact]
    public static void GetTuningFrequency()
    {
        var sensorReports = new[]
        {
            "Sensor at x=2, y=18: closest beacon is at x=-2, y=15",
            "Sensor at x=9, y=16: closest beacon is at x=10, y=16",
            "Sensor at x=13, y=2: closest beacon is at x=15, y=3",
            "Sensor at x=12, y=14: closest beacon is at x=10, y=16",
            "Sensor at x=10, y=20: closest beacon is at x=10, y=16",
            "Sensor at x=14, y=17: closest beacon is at x=10, y=16",
            "Sensor at x=8, y=7: closest beacon is at x=2, y=10",
            "Sensor at x=2, y=0: closest beacon is at x=2, y=10",
            "Sensor at x=0, y=11: closest beacon is at x=2, y=10",
            "Sensor at x=20, y=14: closest beacon is at x=25, y=17",
            "Sensor at x=17, y=20: closest beacon is at x=21, y=22",
            "Sensor at x=16, y=7: closest beacon is at x=15, y=3",
            "Sensor at x=14, y=3: closest beacon is at x=15, y=3",
            "Sensor at x=20, y=1: closest beacon is at x=15, y=3"
        };

        var frequency = SolutionDay15.GetTuningFrequency(sensorReports, ((0, 0), (20, 20)));
        Assert.Equal(56000011, frequency);
    }
}