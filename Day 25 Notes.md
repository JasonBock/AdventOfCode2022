Start from the end. That's 5^0, or 1. The next one is 5^1, or 5. The next one is 5^2, or 25. And so on.

2 => 2
1 => 1
0 => 0
- => -1
= => -2

|  5^5 |  5^4 |  5^3 |  5^2 |  5^1 |  5^0 |
| 3125 |  625 |  125 |   25 |    5 |    1 |

Take "1=11-2". The length is 6, so the starting power is 5 (definitely use BigInteger). 

| 5^5 | 5^4 | 5^3 | 5^2 | 5^1 | 5^0 |

|   1 |   = |   1 |   1 |   - |   2 | => ((5^5) * 1) + ((5^4) * -2) + ((5^3) * 1) + ((5^2) * 1) + ((5^1) * -1) + ((5^0) * 2) => 2_022

OK, so we know how to translate SNAFU to decimal. Doing it the other way

How many divide by 5s can we do? (2022 ^ (1/5)) = 4.58...

Let's do it "normal"

((5^4) * 3) + ((5^3) * 1) + ((5^2) * 0) + ((5^1) * 4) + ((5^0) * 2) => 31042


A really bad brute force way.

(BTW I think a negative is just the "mod" of the symbols at that position)

(-1)  -3 =      '-', '2'
( 0)  -2 =           '='
( 1)  -1 =           '-'
( 2)   0 =           '0'
( 3)   1 =           '1'
( 4)   2 =           '2'
( 5)   3 =      '1', '='
( 6)   4 =      '1', '-'
( 7)   5 =      '1', '0'
( 8)   6 =      '1', '1'
( 9)   7 =      '1', '2'
( 10)  8 =      '2', '='
( 11)  9 =      '2', '-'
( 12) 10 =      '2', '0'
( 13) 11 =      '2', '1'
( 14) 12 =      '2', '2'
( 15) 13 = '1', '=', '='
( 16) 14 = '1', '=', '-'
( 17) 15 = '1', '=', '0'
( 18) 16 = '1', '=', '1'
( 19) 17 = '1', '=', '2'
( 20) 18 = '1', '-', '='
( 21) 19 = '1', '-', '-'
( 22) 20 = '1', '-', '0'
( 23) 21 = '1', '-', '1'
( 24) 22 = '1', '-', '2'
( 25) 23 = '1', '0', '='
( 26) 24 = '1', '0', '-'
( 27) 25 = '1', '0', '0'

The first range is ((5^0) * 2), or 2

The next range is ((5^1) * 2) + ((5^0) * 2), or 12

The next range is ((5^2) * 2) + ((5^1) * 2) + ((5^0) * 2), or 62

The next range is ((5^3) * 2) + ((5^2) * 2) + ((5^1) * 2) + ((5^0) * 2), or 312

The diffs are 10, 50, 250...see the pattern?