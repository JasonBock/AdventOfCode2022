using AdventOfCode2022.Day17;
using Xunit;

namespace AdventOfCode2022.Tests;

public static class SolutionDay17Tests
{
    [Fact]
    public static void GenerateDashRock() 
    {
        var generator = new RockGenerator();
        var rock = generator.Generate();

        Assert.Equal(4, rock.Width);
        Assert.Equal(1, rock.Height);
        Assert.Equal(4, rock.Positions.Count);
        Assert.Contains(new Position(0, 0), rock.Positions);
        Assert.Contains(new Position(1, 0), rock.Positions);
        Assert.Contains(new Position(2, 0), rock.Positions);
        Assert.Contains(new Position(3, 0), rock.Positions);
    }

    [Fact]
    public static void GenerateCrossRock()
    {
        var generator = new RockGenerator();
        _ = generator.Generate();
        var rock = generator.Generate();

        Assert.Equal(3, rock.Width);
        Assert.Equal(3, rock.Height);
        Assert.Equal(5, rock.Positions.Count);
        Assert.Contains(new Position(1, 0), rock.Positions);
        Assert.Contains(new Position(0, 1), rock.Positions);
        Assert.Contains(new Position(1, 1), rock.Positions);
        Assert.Contains(new Position(2, 1), rock.Positions);
        Assert.Contains(new Position(1, 2), rock.Positions);
    }

    [Fact]
    public static void GenerateReverseLRock()
    {
        var generator = new RockGenerator();
        _ = generator.Generate();
        _ = generator.Generate();
        var rock = generator.Generate();

        Assert.Equal(3, rock.Width);
        Assert.Equal(3, rock.Height);
        Assert.Equal(5, rock.Positions.Count);
        Assert.Contains(new Position(0, 0), rock.Positions);
        Assert.Contains(new Position(1, 0), rock.Positions);
        Assert.Contains(new Position(2, 0), rock.Positions);
        Assert.Contains(new Position(2, 1), rock.Positions);
        Assert.Contains(new Position(2, 2), rock.Positions);
    }

    [Fact]
    public static void GeneratePipeRock()
    {
        var generator = new RockGenerator();
        _ = generator.Generate();
        _ = generator.Generate();
        _ = generator.Generate();
        var rock = generator.Generate();

        Assert.Equal(1, rock.Width);
        Assert.Equal(4, rock.Height);
        Assert.Equal(4, rock.Positions.Count);
        Assert.Contains(new Position(0, 0), rock.Positions);
        Assert.Contains(new Position(0, 1), rock.Positions);
        Assert.Contains(new Position(0, 2), rock.Positions);
        Assert.Contains(new Position(0, 3), rock.Positions);
    }

    [Fact]
    public static void GenerateSquareRock()
    {
        var generator = new RockGenerator();
        _ = generator.Generate();
        _ = generator.Generate();
        _ = generator.Generate();
        _ = generator.Generate();
        var rock = generator.Generate();

        Assert.Equal(2, rock.Width);
        Assert.Equal(2, rock.Height);
        Assert.Equal(4, rock.Positions.Count);
        Assert.Contains(new Position(0, 0), rock.Positions);
        Assert.Contains(new Position(1, 0), rock.Positions);
        Assert.Contains(new Position(0, 1), rock.Positions);
        Assert.Contains(new Position(1, 1), rock.Positions);
    }

    [Fact]
    public static void GenerateDashRockWraparound()
    {
        var generator = new RockGenerator();
        _ = generator.Generate();
        _ = generator.Generate();
        _ = generator.Generate();
        _ = generator.Generate();
        _ = generator.Generate();
        var rock = generator.Generate();

        Assert.Equal(4, rock.Width);
        Assert.Equal(1, rock.Height);
        Assert.Equal(4, rock.Positions.Count);
        Assert.Contains(new Position(0, 0), rock.Positions);
        Assert.Contains(new Position(1, 0), rock.Positions);
        Assert.Contains(new Position(2, 0), rock.Positions);
        Assert.Contains(new Position(3, 0), rock.Positions);
    }

    [Fact]
    public static void GetRockTowerHeight()
    {
        var height = SolutionDay17.GetRockTowerHeight(
            ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>", 2022);

        Assert.Equal(3068, height);
    }

    [Fact]
    public static void GetRockTowerHeightOptimized()
    {
        var height = SolutionDay17.GetRockTowerHeightOptimized(
            ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>", 2022);

        Assert.Equal(3068, height);
    }

    [Fact]
    public static void GetRockTowerHeightWithLotsOfRocksOptimized()
    {
        var height = SolutionDay17.GetRockTowerHeightOptimized(
            ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>", 1_000_000_000_000);

        Assert.Equal(1_514_285_714_288, height);
    }
}