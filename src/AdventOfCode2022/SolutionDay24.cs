using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2022.Day24;

public static class SolutionDay24
{
	 public const uint BlizzardDirectionLeft = 0b_0001;
	 public const uint BlizzardDirectionRight = 0b_0010;
	 public const uint BlizzardDirectionUp = 0b_0100;
	 public const uint BlizzardDirectionDown = 0b_1000;
	 public const uint ClearCurrentMask = 0b11110000_11110000_11110000_11110000;

	 public static long GetMinimumMinutes(string[] input)
	 {
		  // Remember to reverse the input.
		  // I'm also assuming the input has the '#' stripped off
		  var grid = SolutionDay24.Parse(input.Reverse().ToArray());
		  var maxY = grid.GetLength(1);
		  var maxX = grid.GetLength(0);
		  var expeditions = new List<Expedition>();
		  var minutes = 0L;

		  while (true)
		  {
				minutes++;

				// Move all the blizzards around, but you can't overwrite what is there.
				var newGrid = SolutionDay24.CreateNewGrid(maxX, maxY);

				for (var y = 0; y < maxY; y++)
				{
					 for (var x = 0; x < maxX; x++)
					 {
						  var blizzards = grid[x, y];

						  for (var b = blizzards.Count - 1; b >= 0; b--)
						  {
								var blizzard = blizzards[b];

								if (blizzard == '>')
								{
									 // we go right.
									 newGrid[(x + 1) % maxX, y].Add(blizzard);
								}
								else if (blizzard == '<')
								{
									 // we go left.
									 newGrid[(x + maxX - 1) % maxX, y].Add(blizzard);
								}
								else if (blizzard == '^')
								{
									 // we go up.
									 newGrid[x, (y + 1) % maxY].Add(blizzard);
								}
								else if (blizzard == 'v')
								{
									 // we go down.
									 newGrid[x, (y + maxY - 1) % maxY].Add(blizzard);
								}

								blizzards.RemoveAt(b);
						  }
					 }
				}

				grid = newGrid;

				var newExpeditions = new List<Expedition>();

				// Check to see if the start position is open.
				// This is assuming that the current expedition has been
				// waiting until now (for some reason) to start.
				if (grid[0, maxY - 1].Count == 0)
				{
					 newExpeditions.Add(new Expedition(0, maxY - 1, minutes));
				}

				for (var e = expeditions.Count - 1; e >= 0; e--)
				{
					 var expedition = expeditions[e];

					 // Can it go right?
					 if (expedition.X <= (maxX - 2) && grid[expedition.X + 1, expedition.Y].Count == 0)
					 {
						  if (expedition.X + 1 == maxX - 1 && expedition.Y == 0)
						  {
								// +2, because you need to move, and then the next move would finish.
								return expedition.NumberOfMoves + 2;
						  }

						  newExpeditions.Add(new Expedition(expedition.X + 1, expedition.Y, minutes));
					 }

					 // Can it go left?
					 if (expedition.X >= 1 && grid[expedition.X - 1, expedition.Y].Count == 0)
					 {
						  newExpeditions.Add(new Expedition(expedition.X - 1, expedition.Y, minutes));
					 }

					 // Can it go up?
					 if (expedition.Y <= (maxY - 2) && grid[expedition.X, expedition.Y + 1].Count == 0)
					 {
						  newExpeditions.Add(new Expedition(expedition.X, expedition.Y + 1, minutes));
					 }

					 // Can it go down?
					 if (expedition.Y >= 1 && grid[expedition.X, expedition.Y - 1].Count == 0)
					 {
						  if (expedition.X == maxX - 1 && expedition.Y - 1 == 0)
						  {
								// +2, because you need to move, and then the next move would finish.
								return expedition.NumberOfMoves + 2;
						  }

						  newExpeditions.Add(new Expedition(expedition.X, expedition.Y - 1, minutes));
					 }

					 // Can it stay where it is?
					 if (grid[expedition.X, expedition.Y].Count > 0)
					 {
						  // Remove it, there's a blizzard here.
						  expeditions.RemoveAt(e);
					 }
					 else
					 {
						  // It's staying, so increase its' move count
						  expedition.NumberOfMoves++;
					 }
				}

				expeditions.AddRange(newExpeditions);

				// It's possible that we may have duplicates -
				// i.e. expeditions at the same spot that haven't moved,
				// or expeditions that just have the same location with the same amount of moves.
				// We only want unique expeditions.
				expeditions = new HashSet<Expedition>(expeditions).ToList();

				const int PruningSize = 500_000;

				// Prune the expedition list if it gets too long.
				// Order by those that are closet to the end,
				// and then have the shortest amount of time on the grid.
				if (expeditions.Count > PruningSize)
				{
					 expeditions = expeditions.OrderBy(_ => int.Abs(_.X - (maxX - 1)) + _.Y)
						  .ThenBy(_ => _.NumberOfMoves).Take(PruningSize).ToList();
				}
		  }
	 }

	 public static long GetMinimumMinutesOptimized(string[] input)
	 {
		  // Remember to reverse the input.
		  // I'm also assuming the input has the '#' stripped off
		  var grid = SolutionDay24.ParseOptimized(input.Reverse().ToArray());
		  var maxY = grid.GetLength(1);
		  var maxX = grid.GetLength(0);
		  var expeditions = new List<Expedition>();
		  var minutes = 0L;

		  while (true)
		  {
				minutes++;

				// Move all the blizzards around, but you can't overwrite what is there.
				grid = SolutionDay24.UpdateBlizzardsOptimized(grid);

				var newExpeditions = new List<Expedition>();

				// Check to see if the start position is open.
				// This is assuming that the current expedition has been
				// waiting until now (for some reason) to start.
				if (grid[0, maxY - 1] == BlizzardDirection.None)
				{
					 newExpeditions.Add(new Expedition(0, maxY - 1, minutes));
				}

				for (var e = expeditions.Count - 1; e >= 0; e--)
				{
					 var expedition = expeditions[e];

					 // Can it go right?
					 if (expedition.X <= (maxX - 2) && grid[expedition.X + 1, expedition.Y] == BlizzardDirection.None)
					 {
						  if (expedition.X + 1 == maxX - 1 && expedition.Y == 0)
						  {
								// +2, because you need to move, and then the next move would finish.
								return expedition.NumberOfMoves + 2;
						  }

						  newExpeditions.Add(new Expedition(expedition.X + 1, expedition.Y, minutes));
					 }

					 // Can it go left?
					 if (expedition.X >= 1 && grid[expedition.X - 1, expedition.Y] == BlizzardDirection.None)
					 {
						  newExpeditions.Add(new Expedition(expedition.X - 1, expedition.Y, minutes));
					 }

					 // Can it go up?
					 if (expedition.Y <= (maxY - 2) && grid[expedition.X, expedition.Y + 1] == BlizzardDirection.None)
					 {
						  newExpeditions.Add(new Expedition(expedition.X, expedition.Y + 1, minutes));
					 }

					 // Can it go down?
					 if (expedition.Y >= 1 && grid[expedition.X, expedition.Y - 1] == BlizzardDirection.None)
					 {
						  if (expedition.X == maxX - 1 && expedition.Y - 1 == 0)
						  {
								// +2, because you need to move, and then the next move would finish.
								return expedition.NumberOfMoves + 2;
						  }

						  newExpeditions.Add(new Expedition(expedition.X, expedition.Y - 1, minutes));
					 }

					 // Can it stay where it is?
					 if (grid[expedition.X, expedition.Y] != BlizzardDirection.None)
					 {
						  // Remove it, there's a blizzard here.
						  expeditions.RemoveAt(e);
					 }
					 else
					 {
						  // It's staying, so increase its' move count
						  expedition.NumberOfMoves++;
					 }
				}

				expeditions.AddRange(newExpeditions);

				// It's possible that we may have duplicates -
				// i.e. expeditions at the same spot that haven't moved,
				// or expeditions that just have the same location with the same amount of moves.
				// We only want unique expeditions.
				expeditions = new HashSet<Expedition>(expeditions).ToList();

				const int PruningSize = 500_000;

				// Prune the expedition list if it gets too long.
				// Order by those that are closet to the end,
				// and then have the shortest amount of time on the grid.
				if (expeditions.Count > PruningSize)
				{
					 expeditions = expeditions.OrderBy(_ => int.Abs(_.X - (maxX - 1)) + _.Y)
						  .ThenBy(_ => _.NumberOfMoves).Take(PruningSize).ToList();
				}
		  }
	 }

	 public static BlizzardDirection[,] UpdateBlizzardsOptimized(BlizzardDirection[,] grid)
	 {
		  var maxY = grid.GetLength(1);
		  var maxX = grid.GetLength(0);

		  // Move all the blizzards around, but you can't overwrite what is there.
		  var newGrid = new BlizzardDirection[maxX, maxY];

		  for (var y = 0; y < maxY; y++)
		  {
				for (var x = 0; x < maxX; x++)
				{
					 var blizzards = grid[x, y];

					 if (blizzards.HasFlag(BlizzardDirection.Right))
					 {
						  // we go right.
						  newGrid[(x + 1) % maxX, y] = newGrid[(x + 1) % maxX, y] | BlizzardDirection.Right;
						  grid[x, y] = grid[x, y] ^ BlizzardDirection.Right;
					 }

					 if (blizzards.HasFlag(BlizzardDirection.Left))
					 {
						  // we go left.
						  newGrid[(x + maxX - 1) % maxX, y] = newGrid[(x + maxX - 1) % maxX, y] | BlizzardDirection.Left;
						  grid[x, y] = grid[x, y] ^ BlizzardDirection.Left;
					 }

					 if (blizzards.HasFlag(BlizzardDirection.Up))
					 {
						  // we go up.
						  newGrid[x, (y + 1) % maxY] = newGrid[x, (y + 1) % maxY] | BlizzardDirection.Up;
						  grid[x, y] = grid[x, y] ^ BlizzardDirection.Up;
					 }

					 if (blizzards.HasFlag(BlizzardDirection.Down))
					 {
						  // we go down.
						  newGrid[x, (y + maxY - 1) % maxY] = newGrid[x, (y + maxY - 1) % maxY] | BlizzardDirection.Down;
						  grid[x, y] = grid[x, y] ^ BlizzardDirection.Down;
					 }
				}
		  }

		  return newGrid;
	 }

	 public static void UpdateBlizzardsOptimizedOneGrid(uint[,] grid, int xLength)
	 {
		  var maxY = grid.GetLength(1);
		  var maxX = grid.GetLength(0);

		  for (var y = 0; y < maxY; y++)
		  {
				for (var x = 0; x < maxX; x++)
				{
					 var blizzards = grid[x, y];
					 var bLimit = xLength >= (x * 4) ? 4 : (x * 4) - xLength;

					 for (var b = 0; b < bLimit; b++)
					 {
						  if ((blizzards & (SolutionDay24.BlizzardDirectionRight << (b * 8))) > 0)
						  {
								// We go right.
								var (q, r) = int.DivRem(((x * 4) + b + 1) % xLength, 4);
								grid[q, y] = grid[q, y] | (SolutionDay24.BlizzardDirectionRight << ((r * 8) + 4));
						  }

						  if ((blizzards & (SolutionDay24.BlizzardDirectionLeft << (b * 8))) > 0)
						  {
								// We go left.
								var (q, r) = int.DivRem(((x * 4) + b + xLength - 1) % xLength, 4);
								grid[q, y] = grid[q, y] | (SolutionDay24.BlizzardDirectionLeft << ((r * 8) + 4));
						  }

						  if ((blizzards & (SolutionDay24.BlizzardDirectionUp << (b * 8))) > 0)
						  {
								// We go up.
								grid[x, (y + 1) % maxY] = grid[x, (y + 1) % maxY] | (SolutionDay24.BlizzardDirectionUp << ((b * 8) + 4));
						  }

						  if ((blizzards & (SolutionDay24.BlizzardDirectionDown << (b * 8))) > 0)
						  {
								// We go down.
								grid[x, (y + maxY - 1) % maxY] = grid[x, (y + maxY - 1) % maxY] | (SolutionDay24.BlizzardDirectionDown << ((b * 8) + 4));
						  }
					 }
				}
		  }

		  // Now clear out all the "current" bits, and move all of the "new" bits to the "current" bit locations
		  for (var y = 0; y < maxY; y++)
		  {
				for (var x = 0; x < maxX; x++)
				{
					 grid[x, y] = (grid[x, y] & SolutionDay24.ClearCurrentMask) >>> 4;
				}
		  }
	 }

	 public static long GetMinimumMinutesOptimizedOneGrid(string[] input)
	 {
		  // Remember to reverse the input.
		  // I'm also assuming the input has the '#' stripped off
		  var (grid, xLength) = SolutionDay24.ParseOptimizedOneGrid(input.Reverse().ToArray());
		  var maxY = grid.GetLength(1);
		  var maxX = grid.GetLength(0);
		  var expeditions = new List<Expedition>();
		  var minutes = 0L;

		  while (true)
		  {
				minutes++;

				// Move all the blizzards around to the "new" bit sections.
				SolutionDay24.UpdateBlizzardsOptimizedOneGrid(grid, xLength);

				var newExpeditions = new List<Expedition>();

				// Check to see if the start position is open.
				// This is assuming that the current expedition has been
				// waiting until now (for some reason) to start.
				if ((grid[0, maxY - 1] & 0b_1111) == 0)
				{
					 newExpeditions.Add(new Expedition(0, maxY - 1, minutes));
				}

				for (var e = expeditions.Count - 1; e >= 0; e--)
				{
					 var expedition = expeditions[e];

					 {
						  var (expeditionQuotient, expeditionRemainder) = int.DivRem(expedition.X + 1, 4);

						  // Can it go right?
						  if (expedition.X <= (xLength - 2) &&
								(grid[expeditionQuotient, expedition.Y] & (0b1111 << expeditionRemainder * 8)) == 0)
						  {
								if (expedition.X + 1 == xLength - 1 && expedition.Y == 0)
								{
									 // +2, because you need to move, and then the next move would finish.
									 return expedition.NumberOfMoves + 2;
								}

								newExpeditions.Add(new Expedition(expedition.X + 1, expedition.Y, minutes));
						  }
					 }

					 {
						  var (expeditionQuotient, expeditionRemainder) = int.DivRem(expedition.X - 1, 4);

						  // Can it go left?
						  if (expedition.X >= 1 &&
								(grid[expeditionQuotient, expedition.Y] & (0b1111 << expeditionRemainder * 8)) == 0)
						  {
								newExpeditions.Add(new Expedition(expedition.X - 1, expedition.Y, minutes));
						  }
					 }

					 {
						  var (expeditionQuotient, expeditionRemainder) = int.DivRem(expedition.X, 4);

						  // Can it go up?
						  if (expedition.Y <= (maxY - 2) &&
								(grid[expeditionQuotient, expedition.Y + 1] & (0b1111 << expeditionRemainder * 8)) == 0)
						  {
								newExpeditions.Add(new Expedition(expedition.X, expedition.Y + 1, minutes));
						  }

						  // Can it go down?
						  if (expedition.Y >= 1 &&
								(grid[expeditionQuotient, expedition.Y - 1] & (0b1111 << expeditionRemainder * 8)) == 0)
						  {
								if (expedition.X == xLength - 1 && expedition.Y - 1 == 0)
								{
									 // +2, because you need to move, and then the next move would finish.
									 return expedition.NumberOfMoves + 2;
								}

								newExpeditions.Add(new Expedition(expedition.X, expedition.Y - 1, minutes));
						  }

						  // Can it stay where it is?
						  if ((grid[expeditionQuotient, expedition.Y] & (0b1111 << expeditionRemainder * 8)) > 0)
						  {
								// Remove it, there's a blizzard here.
								expeditions.RemoveAt(e);
						  }
						  else
						  {
								// It's staying, so increase its' move count
								expedition.NumberOfMoves++;
						  }
					 }
				}

				expeditions.AddRange(newExpeditions);

				// It's possible that we may have duplicates -
				// i.e. expeditions at the same spot that haven't moved,
				// or expeditions that just have the same location with the same amount of moves.
				// We only want unique expeditions.
				expeditions = new HashSet<Expedition>(expeditions).ToList();

				const int PruningSize = 500_000;

				// Prune the expedition list if it gets too long.
				// Order by those that are closet to the end,
				// and then have the shortest amount of time on the grid.
				if (expeditions.Count > PruningSize)
				{
					 expeditions = expeditions.OrderBy(_ => int.Abs(_.X - (xLength - 1)) + _.Y)
						  .ThenBy(_ => _.NumberOfMoves).Take(PruningSize).ToList();
				}
		  }
	 }

	 public static long GetMinimumMinutesOptimizedOneGridUsingOneHashSet(string[] input)
	 {
		  // Remember to reverse the input.
		  // I'm also assuming the input has the '#' stripped off
		  var (grid, xLength) = SolutionDay24.ParseOptimizedOneGrid(input.Reverse().ToArray());
		  var maxY = grid.GetLength(1);
		  var maxX = grid.GetLength(0);
		  var expeditions = new List<Expedition>();
		  var minutes = 0L;

		  while (true)
		  {
				minutes++;

				// Move all the blizzards around to the "new" bit sections.
				SolutionDay24.UpdateBlizzardsOptimizedOneGrid(grid, xLength);

				var newExpeditions = new HashSet<Expedition>();

				// Check to see if the start position is open.
				// This is assuming that the current expedition has been
				// waiting until now (for some reason) to start.
				if ((grid[0, maxY - 1] & 0b_1111) == 0)
				{
					 newExpeditions.Add(new Expedition(0, maxY - 1, minutes));
				}

				for (var e = expeditions.Count - 1; e >= 0; e--)
				{
					 var expedition = expeditions[e];

					 {
						  var (expeditionQuotient, expeditionRemainder) = int.DivRem(expedition.X + 1, 4);

						  // Can it go right?
						  if (expedition.X <= (xLength - 2) &&
								(grid[expeditionQuotient, expedition.Y] & (0b1111 << expeditionRemainder * 8)) == 0)
						  {
								if (expedition.X + 1 == xLength - 1 && expedition.Y == 0)
								{
									 // +2, because you need to move, and then the next move would finish.
									 return expedition.NumberOfMoves + 2;
								}

								newExpeditions.Add(new Expedition(expedition.X + 1, expedition.Y, minutes));
						  }
					 }

					 {
						  var (expeditionQuotient, expeditionRemainder) = int.DivRem(expedition.X - 1, 4);

						  // Can it go left?
						  if (expedition.X >= 1 &&
								(grid[expeditionQuotient, expedition.Y] & (0b1111 << expeditionRemainder * 8)) == 0)
						  {
								newExpeditions.Add(new Expedition(expedition.X - 1, expedition.Y, minutes));
						  }
					 }

					 {
						  var (expeditionQuotient, expeditionRemainder) = int.DivRem(expedition.X, 4);

						  // Can it go up?
						  if (expedition.Y <= (maxY - 2) &&
								(grid[expeditionQuotient, expedition.Y + 1] & (0b1111 << expeditionRemainder * 8)) == 0)
						  {
								newExpeditions.Add(new Expedition(expedition.X, expedition.Y + 1, minutes));
						  }

						  // Can it go down?
						  if (expedition.Y >= 1 &&
								(grid[expeditionQuotient, expedition.Y - 1] & (0b1111 << expeditionRemainder * 8)) == 0)
						  {
								if (expedition.X == xLength - 1 && expedition.Y - 1 == 0)
								{
									 // +2, because you need to move, and then the next move would finish.
									 return expedition.NumberOfMoves + 2;
								}

								newExpeditions.Add(new Expedition(expedition.X, expedition.Y - 1, minutes));
						  }

						  // Can it stay where it is?
						  if ((grid[expeditionQuotient, expedition.Y] & (0b1111 << expeditionRemainder * 8)) > 0)
						  {
								// Remove it, there's a blizzard here.
								expeditions.RemoveAt(e);
						  }
						  else
						  {
								// It's staying, so increase its' move count
								expedition.NumberOfMoves++;
						  }
					 }
				}

				expeditions.AddRange(newExpeditions);
		  }
	 }

	 public static long GetMinimumMinutesFullExpedition(string[] input)
	 {
		  // Remember to reverse the input.
		  // I'm also assuming the input has the '#' stripped off
		  var grid = SolutionDay24.Parse(input.Reverse().ToArray());
		  var maxY = grid.GetLength(1);
		  var maxX = grid.GetLength(0);
		  var expeditions = new List<FullExpedition>();
		  var goBackExpeditions = new List<FullExpedition>();
		  var goFinishExpeditions = new List<FullExpedition>();
		  var minutes = 0L;

		  while (true)
		  {
				minutes++;

				// Move all the blizzards around, but you can't overwrite what is there.
				var newGrid = SolutionDay24.CreateNewGrid(maxX, maxY);

				for (var y = 0; y < maxY; y++)
				{
					 for (var x = 0; x < maxX; x++)
					 {
						  var blizzards = grid[x, y];

						  for (var b = blizzards.Count - 1; b >= 0; b--)
						  {
								var blizzard = blizzards[b];

								if (blizzard == '>')
								{
									 // we go right.
									 newGrid[(x + 1) % maxX, y].Add(blizzard);
								}
								else if (blizzard == '<')
								{
									 // we go left.
									 newGrid[(x + maxX - 1) % maxX, y].Add(blizzard);
								}
								else if (blizzard == '^')
								{
									 // we go up.
									 newGrid[x, (y + 1) % maxY].Add(blizzard);
								}
								else if (blizzard == 'v')
								{
									 // we go down.
									 newGrid[x, (y + maxY - 1) % maxY].Add(blizzard);
								}

								blizzards.RemoveAt(b);
						  }
					 }
				}

				grid = newGrid;

				var newExpeditions = new List<FullExpedition>();
				var newGoBackExpeditions = new List<FullExpedition>();
				var newGoFinishExpeditions = new List<FullExpedition>();

				// Check to see if the start position is open.
				// This is assuming that the current expedition has been
				// waiting until now (for some reason) to start.
				if (grid[0, maxY - 1].Count == 0)
				{
					 newExpeditions.Add(new FullExpedition(0, maxY - 1, minutes, false, false));
				}

				for (var e = expeditions.Count - 1; e >= 0; e--)
				{
					 var expedition = expeditions[e];

					 // Can it go right?
					 if (expedition.X == maxX - 1 && expedition.Y == 0 &&
						  !expedition.ReachedEnd && !expedition.ReachedBeginning)
					 {
						  // This expedition reached the end, but now it must wait to see
						  // when it can start going back.
						  newGoBackExpeditions.Add(new FullExpedition(maxX - 1, 0, minutes,
								true, expedition.ReachedBeginning));
					 }
					 else if (expedition.X <= (maxX - 2) && grid[expedition.X + 1, expedition.Y].Count == 0)
					 {
						  if (expedition.X + 1 == maxX - 1 && expedition.Y == 0 &&
								expedition.ReachedEnd && expedition.ReachedBeginning)
						  {
								// +2, because you need to move, and then the next move would finish.
								return expedition.NumberOfMoves + 2;
						  }

						  newExpeditions.Add(new FullExpedition(expedition.X + 1, expedition.Y, minutes,
								expedition.ReachedEnd, expedition.ReachedBeginning));
					 }

					 // Can it go left?
					 if (expedition.X == 0 && expedition.Y == maxY - 1 &&
						  expedition.ReachedEnd && !expedition.ReachedBeginning)
					 {
						  // This expedition reached the end and now it reached the beginning,
						  // but now it must wait to see
						  // when it can start going back.
						  newGoFinishExpeditions.Add(new FullExpedition(0, maxY - 1, minutes,
								true, true));
					 }
					 else if (expedition.X >= 1 && grid[expedition.X - 1, expedition.Y].Count == 0)
					 {
						  newExpeditions.Add(new FullExpedition(expedition.X - 1, expedition.Y, minutes,
								expedition.ReachedEnd, expedition.ReachedBeginning));
					 }

					 // Can it go up?
					 if (expedition.X == 0 && expedition.Y == maxY - 1 &&
						  expedition.ReachedEnd && !expedition.ReachedBeginning)
					 {
						  // This expedition reached the end and now it reached the beginning,
						  // but now it must wait to see
						  // when it can start going back.
						  newGoFinishExpeditions.Add(new FullExpedition(0, maxY - 1, minutes,
								true, true));
					 }
					 else if (expedition.Y <= (maxY - 2) && grid[expedition.X, expedition.Y + 1].Count == 0)
					 {
						  newExpeditions.Add(new FullExpedition(expedition.X, expedition.Y + 1, minutes,
								expedition.ReachedEnd, expedition.ReachedBeginning));
					 }

					 // Can it go down?
					 if (expedition.X == maxX - 1 && expedition.Y == 0 &&
						  !expedition.ReachedEnd && !expedition.ReachedBeginning)
					 {
						  // This expedition reached the end, but now it must wait to see
						  // when it can start going back.
						  newGoBackExpeditions.Add(new FullExpedition(maxX - 1, 0, minutes,
								true, expedition.ReachedBeginning));
					 }
					 else if (expedition.Y >= 1 && grid[expedition.X, expedition.Y - 1].Count == 0)
					 {
						  if (expedition.X == maxX - 1 && expedition.Y - 1 == 0 &&
								expedition.ReachedEnd && expedition.ReachedBeginning)
						  {
								// +2, because you need to move, and then the next move would finish.
								return expedition.NumberOfMoves + 2;
						  }

						  newExpeditions.Add(new FullExpedition(expedition.X, expedition.Y - 1, minutes,
								expedition.ReachedEnd, expedition.ReachedBeginning));
					 }

					 // Can it stay where it is?
					 if (grid[expedition.X, expedition.Y].Count > 0)
					 {
						  // Remove it, there's a blizzard here.
						  expeditions.RemoveAt(e);
					 }
					 else
					 {
						  // It's staying, so increase its' move count
						  expedition.NumberOfMoves++;
					 }
				}

				// Go through the existing go-back and go-finish lists
				// and see if they can go back on the grid as new expeditions.
				if (grid[maxX - 1, 0].Count == 0)
				{
					 foreach (var goBackExpedition in goBackExpeditions)
					 {
						  newExpeditions.Add(new FullExpedition(goBackExpedition.X, goBackExpedition.Y, minutes,
								goBackExpedition.ReachedEnd, goBackExpedition.ReachedBeginning));
					 }

					 goBackExpeditions.Clear();
				}

				if (grid[0, maxY - 1].Count == 0)
				{
					 foreach (var goFinishExpedition in goFinishExpeditions)
					 {
						  newExpeditions.Add(new FullExpedition(goFinishExpedition.X, goFinishExpedition.Y, minutes,
								goFinishExpedition.ReachedEnd, goFinishExpedition.ReachedBeginning));
					 }

					 goFinishExpeditions.Clear();
				}

				// Add all of the new expeditions
				expeditions.AddRange(newExpeditions);

				// Move the new go-back and go-finish expeditions to the 
				// go-back and go-finish lists.
				goBackExpeditions.AddRange(newGoBackExpeditions);
				goFinishExpeditions.AddRange(newGoFinishExpeditions);

				// It's possible that we may have duplicates -
				// i.e. expeditions at the same spot that haven't moved,
				// or expeditions that just have the same location with the same amount of moves.
				// We only want unique expeditions.
				expeditions = new HashSet<FullExpedition>(expeditions).ToList();

				/*
            const int PruningSize = 500_000;

            // Prune the expedition list if it gets too long.
            // Order by those that are closet to the end,
            // and then have the shortest amount of time on the grid.
            if (expeditions.Count > PruningSize)
            {
                expeditions = expeditions.OrderBy(_ => int.Abs(_.X - (maxX - 1)) + _.Y)
                    .ThenBy(_ => _.NumberOfMoves).Take(PruningSize).ToList();
            }
            */

				//if (expeditions.Count == 0)
				//{
				//    throw new UnreachableException("No expeditions.");
				//}

				Console.WriteLine($"Minute {minutes}, expedition count: {expeditions.Count}");
		  }
	 }

	 public static List<char>[,] CreateNewGrid(int maxX, int maxY)
	 {
		  var grid = new List<char>[maxX, maxY];

		  for (var y = 0; y < maxY; y++)
		  {
				for (var x = 0; x < maxX; x++)
				{
					 grid[x, y] = new List<char>();
				}
		  }

		  return grid;
	 }

	 public static List<char>[,] Parse(string[] input)
	 {
		  var grid = new List<char>[input[0].Length, input.Length];

		  for (var y = 0; y < input.Length; y++)
		  {
				var line = input[y];

				for (var x = 0; x < line.Length; x++)
				{
					 if (grid[x, y] is null)
					 {
						  grid[x, y] = new List<char>();
					 }

					 if (line[x] != '.')
					 {
						  grid[x, y].Add(line[x]);
					 }
				}
		  }

		  return grid;
	 }

	 public static BlizzardDirection[,] ParseOptimized(string[] input)
	 {
		  var grid = new BlizzardDirection[input[0].Length, input.Length];

		  for (var y = 0; y < input.Length; y++)
		  {
				var line = input[y];

				for (var x = 0; x < line.Length; x++)
				{
					 grid[x, y] = line[x] switch
					 {
						  '>' => BlizzardDirection.Right,
						  '<' => BlizzardDirection.Left,
						  '^' => BlizzardDirection.Up,
						  'v' => BlizzardDirection.Down,
						  '.' => BlizzardDirection.None,
						  _ => throw new UnreachableException()
					 };
				}
		  }

		  return grid;
	 }

	 public static (uint[,] grid, int xLength) ParseOptimizedOneGrid(string[] input)
	 {
		  var (quotient, remainder) = uint.DivRem((uint)input[0].Length, 4);

		  var grid = new uint[remainder > 0 ? quotient + 1 : quotient, input.Length];

		  for (var y = 0; y < input.Length; y++)
		  {
				var line = input[y];

				for (var x = 0; x < line.Length; x++)
				{
					 var (xQuotient, xRemainder) = int.DivRem(x, 4);

					 grid[xQuotient, y] = line[x] switch
					 {
						  '>' => grid[xQuotient, y] | (SolutionDay24.BlizzardDirectionRight << (xRemainder * 8)),
						  '<' => grid[xQuotient, y] | (SolutionDay24.BlizzardDirectionLeft << (xRemainder * 8)),
						  '^' => grid[xQuotient, y] | (SolutionDay24.BlizzardDirectionUp << (xRemainder * 8)),
						  'v' => grid[xQuotient, y] | (SolutionDay24.BlizzardDirectionDown << (xRemainder * 8)),
						  '.' => grid[xQuotient, y],
						  _ => throw new UnreachableException()
					 };
				}
		  }

		  return (grid, input[0].Length);
	 }
}

public sealed class Expedition
	 : IEquatable<Expedition>
{
	 public Expedition(int x, int y, long numberOfMoves)
	 {
		  this.X = x;
		  this.Y = y;
		  this.NumberOfMoves = numberOfMoves;
	 }

	 public int X { get; set; }
	 public int Y { get; set; }
	 public long NumberOfMoves { get; set; }

	 public bool Equals(Expedition? other) => other is not null &&
		  this.X == other.X && this.Y == other.Y &&
		  this.NumberOfMoves == other.NumberOfMoves;

	 public override bool Equals(object? obj) => this.Equals(obj as Expedition);

	 public override int GetHashCode() => HashCode.Combine(this.X, this.Y, this.NumberOfMoves);
}

public sealed class FullExpedition
	 : IEquatable<FullExpedition>
{
	 public FullExpedition(int x, int y, long numberOfMoves, bool reachedEnd, bool reachedBeginning)
	 {
		  this.X = x;
		  this.Y = y;
		  this.NumberOfMoves = numberOfMoves;
		  this.ReachedEnd = reachedEnd;
		  this.ReachedBeginning = reachedBeginning;
	 }

	 public int X { get; set; }
	 public int Y { get; set; }
	 public long NumberOfMoves { get; set; }
	 public bool ReachedEnd { get; set; }
	 public bool ReachedBeginning { get; set; }

	 public bool Equals(FullExpedition? other) => other is not null &&
		  this.X == other.X && this.Y == other.Y &&
		  this.NumberOfMoves == other.NumberOfMoves &&
		  this.ReachedBeginning == other.ReachedBeginning &&
		  this.ReachedEnd == other.ReachedEnd;

	 public override bool Equals(object? obj) => this.Equals(obj as FullExpedition);

	 public override int GetHashCode() =>
		  HashCode.Combine(this.X, this.Y, this.NumberOfMoves, this.ReachedBeginning, this.ReachedEnd);
}

[Flags]
public enum BlizzardDirection
{
	 None = 0b_0000,
	 Left = 0b_0001,
	 Right = 0b_0010,
	 Up = 0b_0100,
	 Down = 0b_1000
}