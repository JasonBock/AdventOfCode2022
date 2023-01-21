So, make a grid, where each location is a List<Blizzard>. Blizzard just has one thing: a direction. If it hits 0 or the maxX or maxY, it just wraps around.


The start point is (0, maxY), the end point is (maxX, 0).

While loop. Keep a List<(Location currentLocation, long numberOfMoves) expedition> - these are the paths

Fitness function: how far away is the expedition from the end (where the end is the one location that leads to the exit) |Xexp - Xend| + |Yexp - Yend| and which has the smallest number of moves?

For each move:
    Move all the blizzards around
        Remove it from the list at its current location, determine where it's going to be, and add it to that list.
    
    Expeditions
        Has the expedition started - i.e. can it move to the start position? If so, move it to the start and add it to paths
        If it's started - i.e. paths.Length > 0:
            for each path in paths
                If it can move in a different direction, make a new expedition there
                    If that new expedition lands on the spot where you can leave, return (numberOfMoves + 1) (you're done)
                If a blizzard exists where it is, remove it from paths
        For all expeditions in the paths, increase numberOfMoves. 


Part 2, oops

Once you reach the end the first time, you need to go into the "go back" bucket, and move back into (maxX - 1, 0) when there are no blizzards there, otherwise, your time increases by 1.

Once you reach the beginning after you've reached the end, you need to go into the "go finish" bucket, and move back into (0, maxY - 1) when there are no blizzards there, otherwise, your time increases by 1.

Note that both of these lists can be pruned for uniqueness with hash sets, but they cannot be added to the main list on the grid until they can hop back on.

Optimizations

I just made an optimization where I made the arrays a `BlizzardDirection` flag enumeration. That way I didn't have a `List<char>` for each location on the grid. This helped with speed and memory allocation, but...I thought, could I make it even better?

I'm still making a new `BlizzardDirection[,]` to store the new locations, and then I assign the grid to this new grid. Could I get rid of those array allocations? I'm using 4 bits to store the 4 possible directions stored at a particular location...could I use another 4 bits to store the new locations? And then I could right shift the bits 4 places to move the "new" locations as the "current" locations.

But...in a 32-bit number, like a `uint`, I can actually store 4 locations. This would mean that if a current array needs 1,000 elements, this approach would reduce it to 250. It would also mean I have to be a bit more careful to know which bits to set depending on what "virtual" location the blizzard is in. For example, if `>` was currently at location `(3, 2)`, that means that its' X location is stored in the 3rd set of 8 bits in the `[0, 0]` location of the array. Not hard, but have to keep that all in mind.

Note that I'll have to create a set of direction values as `uint` constants. Using the enum...it would end up being cumbersome because I'd be trying to create values that are technically not valid enum values.

When I do the initial "parse", the initial locations should be considered "current" locations. These new locations will shift things over either 0, 8, 16, or 24 bits, and `or` it with the current value

When I'm going through the phase of finding the new locations...when I'm reading the "current/old" positions, they need to be shifted over 0, 8, 16, or 24 bits. When I'm setting a "new" location, I need to shift the constant value over either 4, 12, 20, or 28 bits, and `or` it with the current value.

Then, I go through the array again, and for each element, I clear out the "old" locations, and move the "new" locations into the "old" bit locations. To do this, I'll `and` each value with `0b11110000_11110000_11110000_11110000`. Then, I'll `>>>` the value 4 bits. Now the "new" are "current" (and soon to be "old").

Then I can move to making decisions about whether a party can move into a new cell. These are in the "current" location bit areas of the `uint` value.

Again, the end result of this is that I don't have to create a new grid every time I iterate. I wish I didn't have to go through the array again to shift the "new" bits to "current", but I don't see another way to do that cleanly.

"1_0000_0000_0000_0000_0000_0000"

Side note: I think if you let the `uint` store 3 8-bit sets, you could another bit to state whether it was cleared or not, which would remove the need to do another iteration on the elements. But that's not something I'm going to try...really :).

"1_0000_0000_0000_0010_0000_0010"

Side note 2: I should create a formatter to get the bits, **all** of them, and put separators at every 4th location (or defined by the user)

optimizedGrid

"0000_0001_0000_0000_0000_1010_0000_0010" "0000_0000_0000_0000_0000_0001_0000_0000"

"0000_0100_0000_1000_0000_0101_0000_0000" "0000_0000_0000_0000_0000_0001_0000_0101"

"0000_0010_0000_0000_0000_0000_0000_0010" "0000_0000_0000_0000_0000_0010_0000_0101"

"0000_0000_0000_0000_0000_0000_0000_0001" "0000_0000_0000_0000_0000_0010_0000_0000"

oneGrid

"0000_0001_0000_0000_0000_1010_0000_0010" "0000_0000_0000_0000_0000_0001_0000_0000"

"0000_0100_0000_1000_0000_0101_0000_0000" "0000_0000_0000_0000_0000_0001_0000_0101"

"0000_0010_0000_0000_0000_0000_0000_0010" "0000_0000_0000_0000_0000_0010_0000_0101"

"0000_0000_0000_0000_0000_0000_0000_0001" "0000_0000_0000_0000_0000_0010_0000_0000"

OK, it looks like the blizzard grid is working just fine. So it's gotta be the expeditions logic.

This was the culprit:

```
// Can it stay where it is?
if ((grid[expeditionQuotient, expedition.Y] & (0b1111 << expeditionRemainder * 8)) == 0)
```

It should have been:

```
// Can it stay where it is?
if ((grid[expeditionQuotient, expedition.Y] & (0b1111 << expeditionRemainder * 8)) > 0)
```

Note the change from `==` to `>`