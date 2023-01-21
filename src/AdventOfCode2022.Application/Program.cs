using AdventOfCode2022.Day15;
using AdventOfCode2022.Day22;
using AdventOfCode2022.Day23;
using AdventOfCode2022.Day24;
using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;

//RunDay1();
//RunDay2();
//RunDay3();
//RunDay4();
//RunDay5();
//RunDay6();
//RunDay7();
//await RunDay8();
//RunDay9();
//RunDay10();
//RunDay11();
//RunDay12();
//RunDay13();
//RunDay14();
//RunDay15();
//RunDay16();
//RunDay17();
//RunDay18();
//RunDay19();
//RunDay20();
//RunDay21();
//RunDay22();
//RunDay23();
RunDay24();
//RunDay25();

/*
static void RunDay1()
{
    Console.WriteLine(nameof(RunDay1));
    var input = File.ReadAllLines("Day1Input.txt");
    Console.WriteLine(SolutionDay1.Run(input));
    Console.WriteLine(SolutionDay1.GetTop3Total(input));
}

static void RunDay2()
{
    Console.WriteLine(nameof(RunDay2));
    var input = File.ReadAllLines("Day2Input.txt");
    Console.WriteLine($"Guessed: {SolutionDay2.Run(input)}");
    Console.WriteLine($"Actual: {SolutionDay2.RunCorrectly(input)}");
}

static void RunDay3()
{
    Console.WriteLine(nameof(RunDay3));
    var input = File.ReadAllLines("Day3Input.txt");
    Console.WriteLine($"Common Item in Rucksack: {SolutionDay3.Run(input)}");
    Console.WriteLine($"Common Item in 3 Elf Rucksacks: {SolutionDay3.RunForCommonElfItem(input)}");
    Console.WriteLine($"Common Item in 3 Elf Rucksacks - Improved: {SolutionDay3.RunForCommonElfItemImproved(input)}");
}

static void RunDay4() 
{
    Console.WriteLine(nameof(RunDay4));
    var input = File.ReadAllLines("Day4Input.txt");
    Console.WriteLine($"Run: {SolutionDay4.Run(input)}");
    Console.WriteLine($"Run for overlap: {SolutionDay4.RunForOverlap(input)}");
}

static void RunDay5() 
{
    Console.WriteLine(nameof(RunDay5));
    var input = File.ReadAllLines("Day5Input.txt");
    Console.WriteLine($"Run: {SolutionDay5.Run(input)}");
    Console.WriteLine($"Run As-Is: {SolutionDay5.RunAsIs(input)}");
}

static void RunDay6() 
{
    Console.WriteLine(nameof(RunDay6));
    var input = File.ReadAllText("Day6Input.txt");
    Console.WriteLine($"GetEndIndexOfFirstMarker: {SolutionDay6.GetEndIndexOfFirstMarker(input)}");
    Console.WriteLine($"GetEndIndexOfFirstMessage: {SolutionDay6.GetEndIndexOfFirstMessage(input)}");
    Console.WriteLine($"GetEndIndexOfFirstMessageBitTwiddling: {SolutionDay6.GetEndIndexOfFirstMessageBitTwiddling(input)}");
}

static void RunDay7() 
{
    Console.WriteLine(nameof(RunDay7));
    var input = File.ReadLines("Day7Input.txt");
    Console.WriteLine($"GetTotalSizeAtMost100000: {SolutionDay7.GetTotalSizeAtMost100000(input)}");
    Console.WriteLine($"GetMinimumSizeDirectory: {SolutionDay7.GetMinimumSizeDirectory(input)}");
}

static async Task RunDay8() 
{
    Console.WriteLine(nameof(RunDay8));
    var input = await File.ReadAllLinesAsync("Day8Input.txt");
    Console.WriteLine($"GetVisibleTreeCount: {SolutionDay8.GetVisibleTreeCount(input)}");
    Console.WriteLine($"GetHigestScenicScore: {SolutionDay8.GetHigestScenicScore(input)}");
}

static void RunDay9() 
{
    Console.WriteLine(nameof(RunDay9));
    var input = File.ReadLines("Day9Input.txt");
    Console.WriteLine($"GetTailVisitationPositionCount: {SolutionDay9.GetTailVisitationPositionCountHardcoded(input)}");
    Console.WriteLine($"GetLongTailVisitationPositionCountForReals: {SolutionDay9.GetTailVisitationPositionCount(input, 10)}");
}

static void RunDay10() 
{
    Console.WriteLine(nameof(RunDay10));
    var input = File.ReadLines("Day10Input.txt");
    var cpu = SolutionDay10.RunCPU(input);
    Console.WriteLine($"Signal Strength: {cpu.SignalStrength}");

    foreach(var displayLine in cpu.Display)
    {
        Console.WriteLine(displayLine);
    }
}

static void RunDay11() 
{
    Console.WriteLine(nameof(RunDay11));
    var monkeys = SolutionDay11.GetMonkeysDayTest(true);
    Console.WriteLine($"Monkey business: {SolutionDay11.GetMonkeyBusiness(monkeys, 20)}");
    var monkeysLong = SolutionDay11.GetMonkeysDayTest(false);
    Console.WriteLine($"Monkey business (big): {SolutionDay11.GetMonkeyBusiness(monkeysLong, 10_000)}");
}

static void RunDay12() 
{
    Console.WriteLine(nameof(RunDay12));
    var map = File.ReadAllLines("Day12Input.txt");
    Console.WriteLine($"Fewest steps: {SolutionDay12.GetFewestSteps(map)}");
    Console.WriteLine($"Fewest steps from all starts: {SolutionDay12.GetFewestStepsFromAllStarts(map)}");
}

static void RunDay13() 
{
    Console.WriteLine(nameof(RunDay13));
    var input = File.ReadAllLines("Day13Input.txt");
    Console.WriteLine($"Pair sum: {SolutionDay13.GetCorrectPairSum(input)}");
    Console.WriteLine($"Decoder key: {SolutionDay13.GetDecoderKey(input)}");
}

static void RunDay14() 
{
    Console.WriteLine(nameof(RunDay14));
    var scan = File.ReadAllLines("Day14Input.txt");
    Console.WriteLine($"Sand block count for abyss: {SolutionDay14.GetSandBlockCountForAbyssPath(scan)}");
    Console.WriteLine($"Sand block count for floor: {SolutionDay14.GetSandBlockCountForFloor(scan)}");
}

static void RunDay15()
{
	 Console.WriteLine(nameof(RunDay15));
	 var sensorReports = File.ReadAllLines("Day15Input.txt");
	 //Console.WriteLine($"Non-beacon locations at 2000000: {SolutionDay15.GetNonBeaconLocations(sensorReports, 2000000)}");
	 Console.WriteLine($"Tuning frequency for grid (0, 0), (4000000, 4000000): {SolutionDay15.GetTuningFrequency(sensorReports, ((0, 0), (4000000, 4000000)))}");
}

static void RunDay16() 
{
    Console.WriteLine(nameof(RunDay16));
    var scans = File.ReadAllLines("Day16Input.txt");
    //Console.WriteLine($"Maximum pressure: {SolutionDay16.GetMaximumPressure(scans)}");
    Console.WriteLine($"Maximum pressure with elephant help: {SolutionDay16.GetMaximumPressureWithElephantHelp(scans)}");
}

static void RunDay17() 
{
    Console.WriteLine(nameof(RunDay17));
    var jets = File.ReadAllText("Day17Input.txt");
    //Console.WriteLine($"Rock tower height: {SolutionDay17.GetRockTowerHeight(jets, 2022)}");
    Console.WriteLine($"Huge rock tower height: {SolutionDay17.GetRockTowerHeightOptimized(jets, 1_000_000_000_000)}");
}

static void RunDay18() 
{
    Console.WriteLine(nameof(RunDay18));
    var coordinates = File.ReadAllLines("Day18Input.txt");
    Console.WriteLine($"Total surface area: {SolutionDay18.GetTotalSurfaceArea(coordinates)}");
    Console.WriteLine($"Total surface area with air bubbles: {SolutionDay18.GetTotalSurfaceAreaWithAirBubbles(coordinates)}");
}

static void RunDay19() 
{
    Console.WriteLine(nameof(RunDay19));

    var blueprints = new Blueprint[]
    {
        new Blueprint(3, 3, (3, 19), (3, 17)), // 1
        new Blueprint(3, 4, (3, 17), (3, 8)), // 2
        new Blueprint(2, 3, (3, 16), (2, 11)), // 3
        new Blueprint(4, 4, (2, 14), (4, 15)), // 4
        new Blueprint(4, 3, (2, 17), (3, 16)), // 5
        new Blueprint(4, 3, (3, 7), (2, 7)), // 6
        new Blueprint(4, 3, (4, 19), (4, 12)), // 7
        new Blueprint(4, 4, (2, 10), (3, 14)), // 8
        new Blueprint(4, 4, (4, 8), (3, 19)), // 9
        new Blueprint(2, 3, (2, 14), (3, 20)), // 10
        new Blueprint(4, 4, (2, 11), (3, 14)), // 11
        new Blueprint(3, 4, (3, 6), (4, 11)), // 12
        new Blueprint(4, 3, (4, 20), (4, 8)), // 13
        new Blueprint(3, 4, (3, 19), (3, 8)), // 14
        new Blueprint(2, 4, (4, 13), (3, 11)), // 15
        new Blueprint(4, 4, (2, 17), (3, 11)), // 16
        new Blueprint(4, 3, (2, 7), (3, 8)), // 17
        new Blueprint(2, 4, (4, 15), (2, 15)), // 18
        new Blueprint(3, 4, (3, 18), (4, 16)), // 19
        new Blueprint(4, 4, (3, 5), (3, 18)), // 20
        new Blueprint(4, 3, (4, 6), (3, 11)), // 21
        new Blueprint(4, 4, (3, 19), (4, 15)), // 22
        new Blueprint(2, 3, (2, 17), (3, 19)), // 23
        new Blueprint(4, 3, (3, 18), (4, 8)), // 24
        new Blueprint(3, 4, (4, 17), (4, 16)), // 25
        new Blueprint(2, 4, (3, 17), (4, 20)), // 26
        new Blueprint(4, 3, (3, 10), (3, 10)), // 27
        new Blueprint(4, 3, (2, 10), (4, 10)), // 28
        new Blueprint(2, 3, (2, 14), (3, 8)), // 29
        new Blueprint(4, 4, (2, 11), (4, 8)) // 30
    };

    //Console.WriteLine($"Total quality level: {SolutionDay19.GetTotalQualityLevel(blueprints)}");

    var firstThreeBlueprints = blueprints.Take(3).ToArray();
    Console.WriteLine($"First three blueprints: {SolutionDay19.GetMaximumGeodesMultiplication(firstThreeBlueprints)}");
}

static void RunDay20() 
{
    Console.WriteLine(nameof(RunDay20));
    var coordinates = File.ReadAllLines("Day20Input.txt");
    //var grooveCoordinatesSum = SolutionDay20.GetGroveCoordinateSum(coordinates);
    //Console.WriteLine($"Coordinate sum: {grooveCoordinatesSum}");
    var grooveCoordinatesSumWithDecryptionKey = SolutionDay20.GetGroveCoordinateSumWithDecryptionKey(coordinates);
    Console.WriteLine($"Coordinate sum with decrypting key: {grooveCoordinatesSumWithDecryptionKey}");
}

static void RunDay21() 
{
    Console.WriteLine(nameof(RunDay21));
    var jobs = File.ReadAllLines("Day21Input.txt");
    //var rootNumber = SolutionDay21.GetRootNumber(jobs);
    //Console.WriteLine($"Root number: {rootNumber}");

    var (leftExpression, rightExpression) = SolutionDay21.GetHumanNumberExpressions(jobs);
    Console.WriteLine(nameof(leftExpression));
    Console.WriteLine(leftExpression);
    Console.WriteLine();
    Console.WriteLine(nameof(rightExpression));
    Console.WriteLine(rightExpression);
}

static void RunDay22()
{
	 Console.WriteLine(nameof(RunDay22));
	 var jobs = File.ReadAllLines("Day22Input.txt");
	 //var password = SolutionDay22.GetPassword(jobs);
	 //Console.WriteLine($"Password: {password}");

	 static (Location nextLocation, Direction nextDirection) NextMapping(Location currentLocation, Direction currentDirection, ImmutableArray<string> tiles)
	 {
		  if (currentLocation.X == 149 && currentLocation.Y >= 150 && currentLocation.Y <= 199 && currentDirection == Direction.Right)
		  {
				// Case 1 - moving right from 1, goes left on 12
				return (
					 new Location(
						  99,
						  int.Abs((currentLocation.Y % 50) - 49) + 50),
					 Direction.Left);
		  }

		  if (currentLocation.X >= 100 && currentLocation.X <= 149 && currentLocation.Y == 199 && currentDirection == Direction.Up)
		  {
				// Case 2 - moving up from 2, goes up on 9
				return (
					 new Location(
						  currentLocation.X % 50,
						  0),
					 Direction.Up);
		  }

		  if (currentLocation.X >= 50 && currentLocation.X <= 99 && currentLocation.Y == 199 && currentDirection == Direction.Up)
		  {
				// Case 3 - moving up from 3, goes right on 8
				return (
					 new Location(
						  0,
						  int.Abs((currentLocation.X % 50) - 49)),
					 Direction.Right);
		  }

		  if (currentLocation.X == 50 && currentLocation.Y >= 150 && currentLocation.Y <= 199 && currentDirection == Direction.Left)
		  {
				// Case 4 - moving left from 4, goes right on 7
				return (new Location(
					 0,
					 int.Abs((currentLocation.Y % 50) - 49) + 50),
				Direction.Right);
		  }

		  if (currentLocation.X == 50 && currentLocation.Y >= 100 && currentLocation.Y <= 149 && currentDirection == Direction.Left)
		  {
				// Case 5 - moving left from 5, goes down on 6
				return (
					 new Location(
						  int.Abs((currentLocation.Y % 50) - 49),
						  99),
					 Direction.Down);
		  }

		  if (currentLocation.X >= 0 && currentLocation.X <= 49 && currentLocation.Y == 99 && currentDirection == Direction.Up)
		  {
				// Case 6 - moving up from 6, goes right on 5
				return (
					 new Location(
						  50,
						  int.Abs((currentLocation.X % 50) - 49) + 100),
					 Direction.Right);
		  }

		  if (currentLocation.X == 0 && currentLocation.Y >= 50 && currentLocation.Y <= 99 && currentDirection == Direction.Left)
		  {
				// Case 7 - moving left from 7, goes right on 4
				return (
					 new Location(
						  50,
						  int.Abs((currentLocation.Y % 50) - 49) + 150),
					 Direction.Right);
		  }

		  if (currentLocation.X == 0 && currentLocation.Y >= 0 && currentLocation.Y <= 49 && currentDirection == Direction.Left)
		  {
				// Case 8 - moving left from 8, goes down on 3
				return (
					 new Location(
						  int.Abs((currentLocation.Y % 50) - 49) + 50,
						  199),
					 Direction.Down);
		  }

		  if (currentLocation.X >= 0 && currentLocation.X <= 49 && currentLocation.Y == 0 && currentDirection == Direction.Down)
		  {
				// Case 9 - moving down from 9, goes down on 2
				return (
					 new Location(
						  (currentLocation.X % 50) + 100,
						  199),
					 Direction.Down);
		  }

		  if (currentLocation.X == 49 && currentLocation.Y >= 0 && currentLocation.Y <= 49 && currentDirection == Direction.Right)
		  {
				// Case 10 - moving right from 10, goes up on 11
				return (
					 new Location(
						  int.Abs((currentLocation.Y % 50) - 49) + 50,
						  50),
					 Direction.Up);
		  }

		  if (currentLocation.X >= 50 && currentLocation.X <= 99 && currentLocation.Y == 50 && currentDirection == Direction.Down)
		  {
				// Case 11 - moving down from 11, goes left on 10
				return (
					 new Location(
						  49,
						  int.Abs((currentLocation.X % 50) - 49)),
					 Direction.Left);
		  }

		  if (currentLocation.X == 99 && currentLocation.Y >= 50 && currentLocation.Y <= 99 && currentDirection == Direction.Right)
		  {
				// Case 12 - moving right from 12, goes left on 1
				return (
					 new Location(
						  149,
						  int.Abs((currentLocation.Y % 50) - 49) + 150),
					 Direction.Left);
		  }

		  if (currentLocation.X == 99 && currentLocation.Y >= 100 && currentLocation.Y <= 149 && currentDirection == Direction.Right)
		  {
				// Case 13 - moving right from 13, goes up on 14
				return (
					 new Location(
						  int.Abs((currentLocation.Y % 50) - 49) + 100,
						  150),
					 Direction.Up);
		  }

		  if (currentLocation.X >= 100 && currentLocation.X <= 149 && currentLocation.Y == 150 && currentDirection == Direction.Down)
		  {
				// Case 14 - moving down from 14, goes left on 13
				return (
					 new Location(
						  99,
						  int.Abs((currentLocation.X % 50) - 49) + 100),
					 Direction.Left);
		  }

		  // Otherwise, it will just move on the current face.
		  // Note that we don't have to mod-restrict the moves anymore.
		  return (currentDirection switch
		  {
				Direction.Right => new Location(currentLocation.X + 1, currentLocation.Y),
				Direction.Left => new Location(currentLocation.X - 1, currentLocation.Y),
				Direction.Up => new Location(currentLocation.X, currentLocation.Y + 1),
				Direction.Down => new Location(currentLocation.X, currentLocation.Y - 1),
				_ => throw new UnreachableException()
		  }, currentDirection);
	 }

	 var cubePassword = SolutionDay22.GetCubePassword(jobs, NextMapping);
	 Console.WriteLine($"Cube password: {cubePassword}");
}

static void RunDay23()
{
	 Console.WriteLine(nameof(RunDay23));
	 var input = File.ReadAllLines("Day23Input.txt");
	 //var emptyGroundTiles = SolutionDay23.GetEmptyGroundTiles(input);
	 //Console.WriteLine($"Empty ground tiles: {emptyGroundTiles}");
	 //var noMovementTurn = SolutionDay23.GetNoMovementTurn(input);
	 //Console.WriteLine($"No movement turn: {noMovementTurn}");
}
*/

static void RunDay24()
{
	 Console.WriteLine(nameof(RunDay24));
	 var input = File.ReadAllLines("Day24Input.txt");
	 var minimumMinutes = SolutionDay24.GetMinimumMinutes(input);
	 Console.WriteLine($"Minimum minutes: {minimumMinutes}");
	 var minimumMinutesOptimized = SolutionDay24.GetMinimumMinutesOptimized(input);
	 Console.WriteLine($"Minimum minutes (optimized): {minimumMinutesOptimized}");
	 var minimumMinutesOptimizedOneGrid = SolutionDay24.GetMinimumMinutesOptimizedOneGrid(input);
	 Console.WriteLine($"Minimum minutes (optimized, one grid): {minimumMinutesOptimizedOneGrid}");
	 //var minimumMinutesFullExpedition = SolutionDay24.GetMinimumMinutesFullExpedition(input);
	 //Console.WriteLine($"Minimum minutes - full expedition: {minimumMinutesFullExpedition}");
}

/*
static void RunDay25() 
{
    Console.WriteLine(nameof(RunDay25));
    var input = File.ReadAllLines("Day25Input.txt");
    var (decimalSum, snafuSum) = SolutionDay25.GetFuelRequirementsSum(input);
    Console.WriteLine($"Decimal sum: {decimalSum}, snafu sum: {snafuSum}");
}
*/
