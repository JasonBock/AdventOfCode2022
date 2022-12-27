using System;
using System.Diagnostics;
using System.Numerics;

namespace AdventOfCode2022.Day6;

public static class SolutionDay6
{
    public static int GetEndIndexOfFirstMarker(ReadOnlySpan<char> buffer) 
    {
        // mjqjpqmgbl gives 7

        // Length 10
        // 0 to 6

        for (var i = 0; i < buffer.Length - 3; i++)
        {
            var marker = buffer[i..(i + 4)];
            var foundDuplicate = false;

            for(var m = 0; m < marker.Length - 1; m++) 
            {
                if (marker[(m + 1)..].Contains(marker[m]))
                {
                    foundDuplicate = true;
                    break;
                }
            }

            if(!foundDuplicate)
            {
                return i + 4;
            }
        }

        throw new UnreachableException();
    }

    public static int GetEndIndexOfFirstMessage(ReadOnlySpan<char> buffer)
    {
        for (var i = 0; i < buffer.Length - 13; i++)
        {
            var marker = buffer[i..(i + 14)];
            var foundDuplicate = false;

            for (var m = 0; m < marker.Length - 1; m++)
            {
                if (marker[(m + 1)..].Contains(marker[m]))
                {
                    foundDuplicate = true;
                    break;
                }
            }

            if (!foundDuplicate)
            {
                return i + 14;
            }
        }

        throw new UnreachableException();
    }

    public static int GetEndIndexOfFirstMessageBitTwiddling(ReadOnlySpan<char> buffer)
    {
        for (var i = 0; i < buffer.Length - 13; i++)
        {
            var markerMask = 0;
            var foundDuplicate = false;

            for (var m = i; m < i + 14; m++)
            {
                // 'a' is 97
                var bitValue = 1 << (buffer[m] - 97);

                if((markerMask & bitValue) > 0)
                {
                    foundDuplicate = true;
                    break;
                }
                else
                {
                    markerMask ^= bitValue;
                }
            }

            if (!foundDuplicate)
            {
                return i + 14;
            }
        }

        throw new UnreachableException();
    }
}