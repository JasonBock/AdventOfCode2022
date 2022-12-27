OK, we can't do 1_000_000_000_000 rocks, especially keeping all the rocks in the shaft.

I think we can only keep the top N rocks. We don't change their position values, we just remove ones that are (probably) too low. Given the cycle of the rocks, I guess there could be a case where all the rocks get magically pushed to a side, and the "pipe" keeps sneaking through, or the "square" shimmies back and forth.

I'm guessing every 1000 rocks, we could only keep the top 500 levels.

I don't see how can skip rocks either. We have to run the simulation for all the rocks. But, even if I keep the shaft small, I have to go through 1_000_000_000_000. In debug mode, a rough guess says I can do 1_000 rocks a second. That would take 31 YEARS to finish. There's no way I could ever try to do all of them. There has to be a trick to this.

I'm wondering if there's a "cycle" we can rely on. I doubt it, but the jet cycle and the rocks for the small example is (40*5) = 200. I really don't see that you can just take what the height would be at 200 and extrapolate from there.

Unless there is a case where the entire row is closed, or close to it. Because then that becomes the floor, and...well, you can't repeat that.

 200 ->  306
 400 ->  607   301
 600 ->  913   306
 800 -> 1216   303
1000 -> 1519   303
1200 -> 1820   301
1400 -> 2126   306
1600 -> 2426   300
1800 -> 2727   301   
2000 -> 3033   306

Bleh

If we do find a row that is completely full, that still doesn't mean it's a cycle, because that doesn't mean it happened on the rock/jet cycle. If it DID, then that would be an out.

>>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>

21s

  65
 489
 913
1337

BOOM - the difference is 424. The cycle repeats when the jetCycle is 21

int cycle, int rockCount, long height, long difference

+		[0]	(0, 0, -1, 0)	(int, int, long, long)
+		[1]	(21, 40, 65, 66)	(int, int, long, long)
+		[2]	(10, 80, 124, 59)	(int, int, long, long)
+		[3]	(2, 120, 183, 59)	(int, int, long, long)
+		[4]	(28, 160, 247, 64)	(int, int, long, long)
+		[5]	(15, 200, 307, 60)	(int, int, long, long)
+		[6]	(5, 240, 368, 61)	(int, int, long, long)
+		[7]	(34, 280, 430, 62)	(int, int, long, long)
+		[8]	(21, 320, 489, 59)	(int, int, long, long)

...

+		[43]	(21, 1720, 2609, 59)	(int, int, long, long)
+		[44]	(10, 1760, 2668, 59)	(int, int, long, long)
+		[45]	(2, 1800, 2727, 59)	(int, int, long, long)
+		[46]	(28, 1840, 2791, 64)	(int, int, long, long)
+		[47]	(15, 1880, 2851, 60)	(int, int, long, long)
+		[48]	(5, 1920, 2912, 61)	(int, int, long, long)
+		[49]	(34, 1960, 2974, 62)	(int, int, long, long)
+		[50]	(21, 2000, 3033, 59)	(int, int, long, long)
 

In our case, the jetCycle is 21, and it repeats every 280 rock generations. It creates a height of 424, and it started at rock generation == 40 with a maxY of 65. But keep in mind, we're at rock generation == 320, we've already done 1 cycle.

So, we have a bottom part of 489 (65 + 424)

We subtract the total number of rocks - 320, so 2022 - 320 = 1702. How many remaining repeating segments of rock count 280 can we fit in there?

1702 / 280 = 6

So, that means we can add 6 segments of height 424 to 489 (65 + 424) => (424 * 6) + 489 = 3033. That's the new maxY, so all of the rocks have to be shifted from what the maxY was at the first cycle to this. 3033 - 489 = 2544

So, all of the rocks in the shaft need their Y increased by 2544, and the i (currentRockGeneration) needs (280 * 6) added to it. We should be able to let it run the rest of the way, and we'll get our value without having to calculate the entire shaft.