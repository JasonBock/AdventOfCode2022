using AdventOfCode2022.Day16;
using Xunit;

namespace AdventOfCode2022.Tests;

public static class SolutionDay16Tests
{
    [Fact]
    public static void ParseWithMultipleConnections()
    {
        var room = Room.Parse("Valve AA has flow rate=0; tunnels lead to valves DD, II, BB");

        Assert.Equal("AA", room.Name);
        Assert.Equal(0UL, room.Rate);
        Assert.Equal(3, room.Connections.Length);
        Assert.Contains("DD", room.Connections);
        Assert.Contains("II", room.Connections);
        Assert.Contains("BB", room.Connections);
    }

    [Fact]
    public static void ParseWithOneConnection() 
    {
        var room = Room.Parse("Valve HH has flow rate=22; tunnel leads to valve GG");

        Assert.Equal("HH", room.Name);
        Assert.Equal(22UL, room.Rate);
        Assert.Single(room.Connections);
        Assert.Contains("GG", room.Connections);
    }

    [Fact]
    public static void GetMaximumPressure()
    {
        var scans = new[]
        {
            "Valve AA has flow rate=0; tunnels lead to valves DD, II, BB",
            "Valve BB has flow rate=13; tunnels lead to valves CC, AA",
            "Valve CC has flow rate=2; tunnels lead to valves DD, BB",
            "Valve DD has flow rate=20; tunnels lead to valves CC, AA, EE",
            "Valve EE has flow rate=3; tunnels lead to valves FF, DD",
            "Valve FF has flow rate=0; tunnels lead to valves EE, GG",
            "Valve GG has flow rate=0; tunnels lead to valves FF, HH",
            "Valve HH has flow rate=22; tunnel leads to valve GG",
            "Valve II has flow rate=0; tunnels lead to valves AA, JJ",
            "Valve JJ has flow rate=21; tunnel leads to valve II",
        };

        var maximumPressure = SolutionDay16.GetMaximumPressure(scans);
        Assert.Equal(1651, maximumPressure);
    }

    [Fact]
    public static void GetMaximumPressureWithElephantHelp()
    {
        var scans = new[]
        {
            "Valve AA has flow rate=0; tunnels lead to valves DD, II, BB",
            "Valve BB has flow rate=13; tunnels lead to valves CC, AA",
            "Valve CC has flow rate=2; tunnels lead to valves DD, BB",
            "Valve DD has flow rate=20; tunnels lead to valves CC, AA, EE",
            "Valve EE has flow rate=3; tunnels lead to valves FF, DD",
            "Valve FF has flow rate=0; tunnels lead to valves EE, GG",
            "Valve GG has flow rate=0; tunnels lead to valves FF, HH",
            "Valve HH has flow rate=22; tunnel leads to valve GG",
            "Valve II has flow rate=0; tunnels lead to valves AA, JJ",
            "Valve JJ has flow rate=21; tunnel leads to valve II",
        };

        var maximumPressure = SolutionDay16.GetMaximumPressureWithElephantHelp(scans);
        Assert.Equal(1707, maximumPressure);
    }
}