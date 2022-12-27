using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022.Day20;

public static class SolutionDay20
{
    public static long GetGroveCoordinateSum(string[] coordinates)
    {
        var length = coordinates.Length;
        var initialLayout = new List<Tag>(length);
        var decryptedLayout = new List<Tag>(length);

        for (var i = 0; i < length; i++)
        {
            var coordinate = int.Parse(coordinates[i]);
            initialLayout.Add(new Tag(i, coordinate));
            decryptedLayout.Add(new Tag(i, coordinate));
        }

        for (var i = 0; i < length; i++)
        {
            var initialTag = initialLayout[i];

            var targetTag = decryptedLayout.Single(_ => _.Id == initialTag.Id);
            var targetCurrentIndex = decryptedLayout.IndexOf(targetTag);
            var shift = targetTag.Value.Mod(length - 1);

            for (var s = targetCurrentIndex; s < targetCurrentIndex + shift; s++)
            {
                (decryptedLayout[(s + 1).Mod(length)], decryptedLayout[s.Mod(length)]) =
                    (decryptedLayout[s.Mod(length)], decryptedLayout[(s + 1).Mod(length)]);
            }
        }

        var finalMoveIndex = decryptedLayout.IndexOf(decryptedLayout.Single(_ => _.Value == 0));
        var coordinate1000 = decryptedLayout[(finalMoveIndex + 1000).Mod(length)].Value;
        var coordinate2000 = decryptedLayout[(finalMoveIndex + 2000).Mod(length)].Value;
        var coordinate3000 = decryptedLayout[(finalMoveIndex + 3000).Mod(length)].Value;

        return coordinate1000 + coordinate2000 + coordinate3000;
    }

    public static long GetGroveCoordinateSumWithDecryptionKey(string[] coordinates)
    {
        const long DecryptionKey = 811_589_153;

        var length = coordinates.Length;
        var initialLayout = new List<Tag>(length);
        var decryptedLayout = new List<Tag>(length);

        for (var i = 0; i < length; i++)
        {
            var coordinate = long.Parse(coordinates[i]);
            initialLayout.Add(new Tag(i, coordinate * DecryptionKey));
            decryptedLayout.Add(new Tag(i, coordinate * DecryptionKey));
        }

        for (var r = 0; r < 10; r++)
        {
            for (var i = 0; i < length; i++)
            {
                var initialTag = initialLayout[i];

                var targetTag = decryptedLayout.Single(_ => _.Id == initialTag.Id);
                var targetCurrentIndex = decryptedLayout.IndexOf(targetTag);
                var shift = targetTag.Value.Mod(length - 1);

                for (var s = targetCurrentIndex; s < targetCurrentIndex + shift; s++)
                {
                    (decryptedLayout[(s + 1).Mod(length)], decryptedLayout[s.Mod(length)]) =
                        (decryptedLayout[s.Mod(length)], decryptedLayout[(s + 1).Mod(length)]);
                }
            }
        }

        var finalMoveIndex = decryptedLayout.IndexOf(decryptedLayout.Single(_ => _.Value == 0));
        var coordinate1000 = decryptedLayout[(finalMoveIndex + 1000).Mod(length)].Value;
        var coordinate2000 = decryptedLayout[(finalMoveIndex + 2000).Mod(length)].Value;
        var coordinate3000 = decryptedLayout[(finalMoveIndex + 3000).Mod(length)].Value;

        return coordinate1000 + coordinate2000 + coordinate3000;
    }

    // From https://stackoverflow.com/questions/1082917/mod-of-negative-number-is-melting-my-brain
    private static int Mod(this int self, int m)
    {
        var r = self % m;
        return r < 0 ? r + m : r;
    }

    private static long Mod(this long self, long m)
    {
        var r = self % m;
        return r < 0 ? r + m : r;
    }
}

public record struct Tag(int Id, long Value);