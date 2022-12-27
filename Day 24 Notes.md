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