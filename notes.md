# Advent of Code 2022 Notes

These are my notes for each day's solutions.

# Day 1: Calorie Counting

I got this done, but then I realized I could make it faster because I didn't need to keep track of **every** calorie count for each elf. I just needed to keep track of the largest one. Also, while I got the answer correctly, I had a bug because I wasn't considering the last calorie count in both implementations.

Then on Day 15 I realized that **all** of the days have 2 parts. Oops. Getting the top 3 wasn't too bad.

# Day 2: Rock Paper Scissors

This was an illustration of getting changing requirements, which make me rethink a bit how I was creating my solutions. It made things a bit annoying, but I was able to handle it. I also debated making the `Choices` and `Outcome` constants an `enum`, but then I'd have to make casts when I wanted to do math on the values. I've always felt like this is a paper-cut in C#, in that I'd love to be able to do math on the enum values. Maybe SAMIs can be done here in the future, I dunno.

# Day 3: Rucksack Reorganization

This is legitimately the first time I've ever used a `Span<>` in code (technically a `ReadOnlySpan<char>`), and it worked really well. I didn't try to create a "devolved" version of the solutions, where I'd create new `string` values based on how I was searching through the strings, but that may be an interesting perf test to do. It was also really important to add some comments to make sure I was thinking about different conditions in the strings (e.g. where are the matching `char` items in the strings? How long are the `string` inputs?). It was fun to also use ranges in the `Span<>` - it seemed very natural to use it the way I did.

I realized that I hadn't used the technique to reduce the size of the span as I moved through the content for the elf rucksacks. I changed that, but for the input sizes and common item locations in the test data, it really didn't make a difference. Only if the common element was near the end of the strings, and the strings were fairly large, did I start to see an improvement.

|                           Method |            rucksack0 |            rucksack1 |            rucksack2 |        Mean |      Error |    StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|--------------------------------- |--------------------- |--------------------- |--------------------- |------------:|-----------:|----------:|------:|--------:|-------:|----------:|------------:|
|         GetCommomElfRucksackItem | QJRBM(...)LVVgh [36] |   TPSNNPZGTjgmSmvfjL | bPlpZ(...)WnJQB [38] |    48.97 ns |   0.812 ns |  0.760 ns |  1.00 |    0.00 | 0.0048 |      40 B |        1.00 |
| GetCommomElfRucksackItemImproved | QJRBM(...)LVVgh [36] |   TPSNNPZGTjgmSmvfjL | bPlpZ(...)WnJQB [38] |    50.72 ns |   0.619 ns |  0.579 ns |  1.04 |    0.02 | 0.0048 |      40 B |        1.00 |
|                                  |                      |                      |                      |             |            |           |       |         |        |           |             |
|         GetCommomElfRucksackItem |               aaaaaB |               bbbbbB |               cccccB |    74.64 ns |   1.216 ns |  1.078 ns |  1.00 |    0.00 | 0.0048 |      40 B |        1.00 |
| GetCommomElfRucksackItemImproved |               aaaaaB |               bbbbbB |               cccccB |    69.77 ns |   0.891 ns |  0.834 ns |  0.94 |    0.02 | 0.0048 |      40 B |        1.00 |
|                                  |                      |                      |                      |             |            |           |       |         |        |           |             |
|         GetCommomElfRucksackItem | aaaaa(...)aaaaB [51] | bbbbb(...)bbbbB [51] | ccccc(...)ccccB [51] |   610.30 ns |  11.922 ns | 11.152 ns |  1.00 |    0.00 | 0.0048 |      40 B |        1.00 |
| GetCommomElfRucksackItemImproved | aaaaa(...)aaaaB [51] | bbbbb(...)bbbbB [51] | ccccc(...)ccccB [51] |   570.74 ns |   7.303 ns |  6.474 ns |  0.94 |    0.02 | 0.0048 |      40 B |        1.00 |
|                                  |                      |                      |                      |             |            |           |       |         |        |           |             |
|         GetCommomElfRucksackItem |  aaaa(...)aaaB [201] |  bbbb(...)bbbB [201] |  cccc(...)cccB [201] | 5,067.13 ns | 100.494 ns | 98.699 ns |  1.00 |    0.00 |      - |      40 B |        1.00 |
| GetCommomElfRucksackItemImproved |  aaaa(...)aaaB [201] |  bbbb(...)bbbB [201] |  cccc(...)cccB [201] | 3,361.46 ns |  61.076 ns | 57.131 ns |  0.66 |    0.01 | 0.0038 |      40 B |        1.00 |

Removing the array to find the minimum rucksack size helped a little bit with performance, and it removed the allocation.

|                                     Method |           rucksack0 |           rucksack1 |           rucksack2 |     Mean |     Error |    StdDev | Ratio |   Gen0 | Allocated | Alloc Ratio |
|------------------------------------------- |-------------------- |-------------------- |-------------------- |---------:|----------:|----------:|------:|-------:|----------:|------------:|
|                   GetCommomElfRucksackItem | aaaa(...)aaaB [201] | bbbb(...)bbbB [201] | cccc(...)cccB [201] | 5.031 us | 0.0836 us | 0.0741 us |  1.00 |      - |      40 B |        1.00 |
|           GetCommomElfRucksackItemImproved | aaaa(...)aaaB [201] | bbbb(...)bbbB [201] | cccc(...)cccB [201] | 3.383 us | 0.0515 us | 0.0430 us |  0.67 | 0.0038 |      40 B |        1.00 |
| GetCommomElfRucksackItemImprovedMaxNoArray | aaaa(...)aaaB [201] | bbbb(...)bbbB [201] | cccc(...)cccB [201] | 3.354 us | 0.0620 us | 0.0580 us |  0.67 |      - |         - |        0.00 |

# Day 4: Camp Cleanup

I reused my `Range` extension methods in [Spackle](https://github.com/jasonbock/spacklenet). I realized that they treat the `End` property as "inclusive", but the docs for the property clearly state that it should be an "exclusive" value. This prompted me to file a bug on my own library :). Actually, what I realized later is that I was using `System.Range`, **not** `Spackle.Range<int>`, and I really should have been using Spackle's `Range` type.

# Day 5: Supply Stacks

Big lesson in testing every specific part. I think each parse part I had messed up in some way:
* For `MoveCratesAsIs()`, I was doing the last `for` loop on the count of the temp stack, and that was going down by 1 every time.
* `ReverseStacks()`, I thought I needed to `Reverse()` the stack and pass that into a new stack, but that just put it back into the same order.
* `GetMessage()`, I had to order the pairs by the key value. It's a `Dictionary`, so it won't come out in order of the key values.

I also felt like I was hacking a bit and not being as efficient as I could be, but it worked (after I got the tests to pass.)

I also assumed the `move` instructions were always 1 to 9.

# Day 6: Tuning Trouble

Felt pretty straightforward. However, with all of these puzzles, I'm reading the contents of the input files all at once. It may be more performant to only read in chunks at a time. However, that would break my original solutions, especially this one for Day 6, as it assumes you have all the data. And if you did it in chunks, you'd have to be a bit more clever to remember what you were looking at before. Definitely doable, but it would be more work.

Another way to do this would be to allocate a `Set<>` for the current marker, and if the size of the `Set<>` wasn't the same as the current marker, that would mean you'd have a duplicate. That may make the code easier, but that's potentially a lot of allocations. I'm wondering if there's a way to do this that uses an `int` with bit flags that map to each possible character.

We know our input is all of the lowercase letters in the English alphabet, so we could map each character to a flag in an `int`. For each character, we look to see if its flag has been set. If it has, we stop, because we know we've found a duplicate.

For example, if our input marker is `acda`, we create a mask equal to 0 and set `foundDuplicate` to `false`.

`a` => `0000...0000` doesn't have the 1st flag set, so we set it - `0000...0001`.
`c` => `0000...0001` doesn't have the 3rd flag set, so we set it - `0000...0101`.
`d` => `0000...0101` doesnt' have the 4th flag set, so we set it - `0000...1101`.
`a` => `0000...1101` has the 1st flag set, so we stop and set `foundDuplicate` to `true`.

This would eliminate all the `Contains()` calls with `Span<>` creations. It would require us to do bit twiddling, but that should be faster...right? I'll only do this with the message one.

|                                Method |     Mean |    Error |   StdDev | Ratio | Allocated | Alloc Ratio |
|-------------------------------------- |---------:|---------:|---------:|------:|----------:|------------:|
|             GetEndIndexOfFirstMessage | 45.52 us | 0.600 us | 0.561 us |  1.00 |         - |          NA |
| GetEndIndexOfFirstMessageBitTwiddling | 31.09 us | 0.393 us | 0.368 us |  0.68 |         - |          NA |

So, yes, it is faster.

# Day 7: No Space Left On Device

This was all about getting the domain right. I thought of reusing things like `FileInfo` and `DirectoryInfo` because I thought they might not actually point to "real" disk entities, but I couldn't tell, so I just ended up making `Folder` and `Document`. Parsing the output wasn't too difficult, but getting the right values based on the requirements from the two parts took a bit of time, especially the second part. I also changed the file reading to `ReadLines()`, which returns `IEnumerable<string>`. 

# Day 8: Treetop Tree House

Using a multidimensional array, haven't used that in...well, I can't remember. I couldn't think of a way to optimize what I did. Not saying it couldn't be better, just couldn't think of anything.

# Day 9: Rope Bridge

Wow, this one took a **long** time to get right. I just could not get my head around the rules and how to code it correctly. I had wooden blocks on the floor trying to figure this out. I also tried to do this as soon as it was released just to see if I could get any points, and it blew me away just how fast some people solved this. It reinforced with me that it's not about the speed; it's about learning and growing through puzzle solving. Doing this competitively is a mistake for me. I stayed up way too late and was kind of cranky the next day :).

# Day 10: Cathode-Ray Tube

Didn't seem as bad as Day 9 :). I initially got the timing of updating the cycle and geting the signal strength wrong, but fortunately I was able to figure it out. Drawing the screen seemed relatively straightforward, even though the explanation wasn't very clear at first.

I could make the abstraction better. Having the "screen" off of the CPU doesn't make a lot of sense. It works, but I'd rather make a `Computer` that has a `Screen` and a `CPU` instead.

# Day 11: Monkey in the Middle 

I got Part 1 done, then Part 2 threw me for a loop. Part of it was the unboundedness of the calculations, which will overflow both an `int` and a `long`. Changing it to a `BigInteger` made it take forever (I waited 30 minutes and gave up). I noticed the "divisible by" values were all primes, so I restricted the size of the answers to the multiple of the primes, and that fixed it. But I also was passing in the monkey values from the Part 1 solution, which was totally wrong. Once I finally figured out that my input was incorrect, I got the right answer, so at least I had the approach correct.

I didn't parse the input either in code. I just created the monkey array based off of what I read in the input. That would be something I'd update.

# Day 12: Hill Climbing Algorithm

The first part took time just to get the path logic right. I duplicated code, which I eventually refactored to a local method. I also wasn't pruning the paths after every iteration, and that made it spin wildly out of control.

The second part was pretty easy. I just has to get all the 'a' starting locations.

It would be kind of cool to render this as the path finder does its' thing.

# Day 13: Distress Signal

This wasn't too bad. I did a fair amount of pseudo-coding to work out the parsing. Fortunately there were a lot of test cases to use, and that helped me to root out a couple of bugs. But once I had that, creating the comparison wasn't too bad. I lucked out a bit in that I made a `Compare()` function that worked perfectly for the sorting required in Part 2. So it made Part 2 pretty easy to finish.

# Day 14: Regolith Reservoir

Got Part 1 done using a multi-dimensional array, but Part 2 made me realize that I couldn't try to do an "infinite" x direction. So I came up with a scheme that maps the rocks in a `HashSet<>`. It wasn't terribly efficient, but it got the answer relatively quickly. Problem was that I was doing this in the evening and I completely mashed up Part 1 and 2, so while I was able to recover Part 2, Part 1 was wrecked. Still don't know how I did that. But that forced me to use the `HashSet<>` approach there as well, which was a good thing.

# Day 15: Beacon Exclusion Zone

The first part wasn't too bad. The 2nd, I realized right away that a brute-force search would never get done. So I came up with a way to skip over coverage areas and reduce the number of locations I had to figure out if I was in coverage or not. I also knew the answer to Part 2 could overflow an `int`, so I had it return a `long`, but even then, I didn't do it right, because I used `4000000` as the literal, not `4000000L`.

But I think I can optimize it further.
* If I realize I'm in range, then I move outside of it, but I go through all the pairs again. I know I've exited a pair, so I shouldn't revisit that one. This isn't a big savings, but it is less work.
* Parallelize the work. 4,000,000 / processors, or just do Parallel.ForEachAsync(), calling GetTuningFrequency() (well, one that already has the pairs parsed) with different grids. The key would be to signal other workers when one finds it so they all stop. Maybe create a shared LocationFound class that has the (x, y) set, and an instance of this is passed to each searcher. They just have to check every so often to see if another one found it (simple if-check).

And then I realized **all** of the days has 2 parts, and I never did the 2nd part to Day 1!.

# Day 16: Proboscidea Volcanium

This felt like the traveling salesman problem, and the number of possible paths would spiral out of control. For Part 1, I put in a pruning check to only keep the top 25,000 scores after every minute. This isn't a guarantee that I'm finding the best path, but it gave the right answer for Part 1.

Part 2, you have to keep track of an elephant and you running around in only 26 minutes. So I hard-coded in more possible paths, and I made the mistake of adding both the elephant and the human opening a value if they were in the same room. Oops.

# Day 17: Pyroclastic Flow 

Part 1 was pretty simple, just had to move the rocks around and determine how to place them. Part 2, I saw that there was no way to manually do all the rocks requested, but it took some time to figure out the "trick" where you didn't have to go through all of them.

# Day 18: Boiling Boulders

Part 1 felt pretty easy. But Part 2 took some time because I needed to really get my head around what it meant for a region to have an air bubble. I had to do a couple of iterations to remove all those coordinates that I thought was "internal" but really wasn't.

# Day 19: Not Enough Minerals

Another TSP problem, but I was able to get the model down without a lot of fuss, and after adding in the right pruning optimization, I got Parts 1 and 2 done without a lot of issue. I didn't write a parser to get the blueprint data. I decided to input it by hand, I thought that would be easier, but now...I think writing the parser would've been the right approach.

For Part 2, I made the initial value 0, not 1, so anything multiplied by 0 is...zero! Oops.

# Day 20: Grove Positioning System

I really did not like the explanation of getting the final 3 coordinates. It was not clear where you start from. You literally needed to find where `0` was in the resulting list, and you needed to assume there was only one of them. I feel like the explanation wasn't clear on that.

Part 1 also "hid" a bug I had in Part 2. I was using a `Mod()` method to reduce the number of iterations, but when the shift value was longer than the length, I needed to reduce that by one to move all the values around correctly. Literally an "off by one" bug :).

I'm also noticing that since I've had some incorrect answers, I'm no longer being told if I'm too low or high.

# Day 21: Monkey Math

I was up late, so I tried to get Part 1 done as fast as I can. Amazingly, it worked on the first try (no unit tests either - I added those later). But it took me 25 minutes, and the leader was done in less than 2. I just don't work that fast, but at least I got it done.

The 2nd part was easy to understand conceptually, but I had no idea how to actually solve for the variable...other than to try and generate the expression and use a tool like Wolfram to do it. And that's exactly how I finished it. The resulting expression was too long, so I had to keep getting simplified versions and shortening it until Wolfram would let it go through the site.

# Day 22: Monkey Map

Part 1, I forgot that, depending on where you move, you may end up in an empty space, or out of range of the next string. Once I cleared up those errors, I was able to get the answer.

Part 2, I understood the problem, but...for some reason, I wasn't really motivated to figure out the mappings for the cube faces and what happens when you move from one to the other. That took some time to get it right. I actually created a physical model of the map layout on paper so I could fold it up and see how the edges mapped. I got the smaller example working, but then, moving to the puzzle input, I uncovered errors in my mappings (as well as an error getting the initial location that didn't show up with the smaller example). I finally used the character could that I create when I visit the path to narrow down where the exceptions were occurring, and that made it go by quicker.

I'm sure there was a simpler way to do Part 2, but what I did worked.

# Day 23: Unstable Diffusion

This seemed like a straightforward problem. Getting both answers wasn't too hard. I had some bugs in Part 1 that made it take longer. Part 2 was easy to code, but it took a long time to get to the answer. I'm thinking parallelization would help a bit.

# Day 24: Blizzard Basin

The key to Part 1 was to remove duplicate expeditions. Otherwise the number would grow a lot where a lot of them had the same location and minute count.

Part 2 wasn't too tricky. The main fault I had was that, once an expedition reached the end for the first time, I had to put it into a "waiting" bucket to see when a blizzard wasn't in that "end" position (where you could exit the grid). Same for the beginning position. Once I did that, it went by smoothly.

# Day 25: Full of Hot Air

There's really only one part. Translating from SNAFU to decimal wasn't too hard, but...for some reason, going the other way really through me for a loop. I finally landed on a solution that's probably not ideal, but it worked.

And once I solved Part 2 of Day 22, I saw all the pretty balloons :).

# TODOs

* Would be fun to explore the optimization ideas in Day 15
* Should look at parallelization for Day 23
* With Day 24, I could make the grids a Direction[,], where Direction is a flags enum. Because each location can have only one of each Direction at a time, and it can have at most all 4, doing a flag should be quicker than using a List<char>.