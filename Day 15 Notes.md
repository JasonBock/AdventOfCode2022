M = |Sx - Bx| + |Sy - By|

First, parse the input, and create a HashSet of sensors and beacon locations. Also, return the min and max of X that could possibly be relevant for all S/B pairs. To do this, get minSx and maxSx for where the B would be at the same Y as S (you know what the Manhattan distance is for S/B, X = Sx +- M). Keep one or the other if it's the min/max found so far.

Then, for a given Y-coordinate, sweep across minX and maxX. For each sensor/beacon pair, determine if that (x, y) position is within the Manhattan distance of the S/B pair. If it is, then a beacon cannot be there, so noBeacon++;

For Part 2, if I did a brute force search, it would be 16,000,000,000,000 searches, or 16 trillion. I could parallelize the search across 8 processors, but that would only reduce it to 2,000,000,000,000, or 2 trillion. Way too many.

Could we skip? If you're at the Distance, then you can skip over all the parts that you know you'd be in range. If you're within the Distance, then you can skip over to that distance. But then you may be thrown into another pair, and then you need to figure out how to completely get out of it without just incrementing x

Let's say you were just incrementing, and then a pair says you're in range. Then you can skip X to (2 * |Sx - x|) + 1.

That's an "easy" case. But what if you start being "in" a range, not on the edge? Or, by the previous skip operation, you're throw into the middle of a range?

This is the general "newX" formula, if InRange() returns true for a given Pair.

(Sx + D) - |Sy - y| + 1

Also, always skip if the current (x, y) is on a beacon or sensor



...#...
..###..
.#####.
###S##B
.#####.
..###..
...#...

S = (3, 3)
D = 3

Let's say we were at (2, 1)

newX = (3 + 3) - |3 - 1| + 1
     = 6 - 2 + 1 = 5

(5, 1)

if we ever go outside the bounds of the search ((0,0) to (4m, 4m)), we're done on the current row, and move to the next.

So how do we know we found the position?
* Going through all the remaining Pair objects in our foreach (because we may have went through previous ones and moved on, BUT! if we were in range of a Pair, we have to start all over again with the Pair list, because a previous one may not one that we will end up "in range"), we are not in range of any remaining Pair objects, nor are we on a Beacon or Sensor, and we are within the search bounds.

The final calculation should return a `long` and be done with casted `long` values, because we need to multiple a coordinate by 4,000,000, and we could easily overflow if we're not careful.