        ...#
        .#..
        #...
        ....
...#.......#
........#...
..#....#....
..........#.
        ...#....
        .....#..
        .#......
        ......#.

10R5L5R10L4R5L5

Main puzzle:
Max X is 150
Max Y is 200

Max answer is (1000 * 150) + (4 * 200) + 3

151003 - that's the maximum password value.

OOPS. I forgot the last number, which, with the example, wouldn't have mattered if I ran it or not. But with the input data, it DID matter.

Part 2, I think I'll pass in mapper(location, direction), which will be used to find where the piece goes next. I'm not going to try and figure out how to do this dynamically.

For the example

func mapper(location, direction)

There are 14 mappings that could happen, well, actually 7 pairs of 2

Edge connection:
1 and 12
2 and 5
3 and 4
6 and 11
7 and 10
8 and 9
14 and 13

Case 14

Y = 3

Y = 4 => X = 12
Y = 5 => X = 13

(Y % 4) + 12

Case 13

X = 11

X = 12 => Y = 4
X = 13 => Y = 5

(X % 4) + 4

Case 12

X = 11

Y = 3 => Y = 8
Y = 2 => Y = 9

|(Y % 4) - 3| + 8


Case 11

X = 0

X = 12, Y = 4
X = 13, Y = 5

(X % 4) + 4

Case 10

Y is 4

X = 8 => X = 3
X = 9 => X = 2

|(X % 4) - 3|

Case 9

Y = 4

Y = 3 => X = 7
Y = 2 => X = 6

(Y % 4) + 4

Case 8

X = 8

X = 7 => Y = 3
X = 6 => Y = 2

(X % 4)

Case 7

Y = 0

X = 0 => X = 11
X = 1 => X = 10

|(X % 4) - 3| + 8

0 => 11
1 => 10

Case 6

Y = 0

Y = 4 => X = 12
Y = 5 => X = 13

(Y % 4) + 12

Case 5

Y = 11

X = 0 => X == 11
X = 1 => X == 10

|(X mod 4) - 3| + 8

Y = 4 => X = 12
Y = 5 => X = 13

(Y mod 4) + 12

Case 4

X = 8

X = 4 => Y = 11
X = 5 => Y = 10

|(X % 4) - 3| + 8

Case 3

Y = 7

Y = 8 => X = 7
Y = 9 => X = 6
Y = 10 => X = 5
Y = 11 => X = 4

|(Y mod 4) - 3| + 4
8 => 7
9 => 6

Case 2

Y = 7

X = 8 => X = 3
X = 9 => X = 2

Case 1

X = 15

Y = 8 => Y = 3
Y = 9 => Y = 2
Y = 10 => Y = 1
Y = 11 => Y = 0

|(Y mod 4) - 3|
8 => 3
9 => 2
10 => 1
11 => 0



For the actual problem, each face is 50 in length.

Edge connection:
1 and 12
2 and 9
3 and 8
4 and 7
5 and 6
6 and 5
7 and 4
8 and 3
9 and 2
10 and 11
11 and 10
12 and 1
13 and 14
14 and 13

Case 14

X = 99

X = 100 => Y = 149
X = 101 => Y = 148

|(X % 50) - 49| + 100

100 => 149
101 => 148

Case 13

Y = 150

Y = 149 => X = 100
Y = 148 => X = 101

|(Y % 50) - 49| + 100

149 => 100
148 => 101


Case 12

X = 149

Y = 99 => Y = 150
Y = 98 => Y = 151

|(Y % 50) - 49| + 150

99 => 150
98 => 151

Case 11

X = 49

X = 50 => Y = 49
X = 51 => Y = 48

|(X % 50) - 49|

50 => 49
51 => 48

Case 10

Y = 50

Y = 0 => X = 99
Y = 1 => X = 98

|(Y % 50) - 49| + 50

0 => 99
1 => 98


Case 9

Y = 199

X = 0 => X = 100
X = 1 => X = 101

(X % 50) + 100

0 => 100
1 => 101

Case 8

Y = 199

Y = 49 => X = 50
Y = 48 => X = 51

|(Y % 50) - 49| + 50

49 => 50
48 => 51


Case 7

X = 50

Y = 99 => Y = 150
Y = 98 => Y = 151

|(Y % 50) - 49| + 150

99 => 150
98 => 151


Case 6

X = 50

X = 0 => Y = 149
X = 1 => Y = 148

|(X % 50) - 49| + 100

0 => 149
1 => 148


Case 5

Y = 99

Y = 100 => X = 49
Y = 101 => X = 48

|(Y % 50) - 49|

100 => 49
101 => 48

Case 4

X = 0

Y = 199 => Y = 50
Y = 198 => Y = 51

|(Y % 50) - 49| + 50

199 => 50
198 => 51

Case 3

X = 0

X = 50 => Y = 49
X = 51 => Y = 48

|(X % 50) - 49|

50 => 49
51 => 48

Case 2

Y = 0

X = 100 => X = 0
X = 101 => X = 1

(X % 50)

100 => 0
101 => 1

Case 1

X = 99

Y = 150 => Y = 99
Y = 151 => Y = 98

|(Y % 50) - 49| + 50

150 => 99
151 => 98