using AdventOfCode2022.Day7;
using Xunit;

namespace AdventOfCode2022.Tests;

public static class SolutionDay7Tests
{
    [Fact]
    public static void CreateFolderStructure()
    {
        var folder = new Folder();

        var aFolder = folder.AddFolder("a");
        aFolder.AddDocument("aTextFile.txt", 10);
        aFolder.AddDocument("aJsonFile.json", 20);

        var bFolder = folder.AddFolder("b");
        bFolder.AddDocument("bTextFile.txt", 100);
        bFolder.AddDocument("bJsonFile.json", 200);

        var cFolder = bFolder.AddFolder("c");
        cFolder.AddDocument("cTextFile.txt", 1000);
        cFolder.AddDocument("cJsonFile.json", 2000);

        Assert.Equal(3330, folder.Size);
        Assert.Equal(30, aFolder.Size);
        Assert.Equal(3300, bFolder.Size);
        Assert.Equal(3000, cFolder.Size);

        Assert.Null(folder.Parent);
        Assert.Equal(folder, aFolder.Parent);
        Assert.Equal(folder, bFolder.Parent);
        Assert.Equal(bFolder, cFolder.Parent);

        Assert.Equal(folder, folder.Root);
        Assert.Equal(folder, aFolder.Root);
        Assert.Equal(folder, bFolder.Root);
        Assert.Equal(folder, cFolder.Root);
    }

    [Fact]
    public static void GetRoot()
    {
        var outputs = File.ReadLines("GetRootInput.txt");
        var root = SolutionDay7.GetRootFolder(outputs);

        Assert.Equal(48381165, root.Size);
        var aFolder = root.GetFolder("a");
        Assert.Equal(94853, aFolder.Size);
        var eFolder = aFolder.GetFolder("e");
        Assert.Equal(584, eFolder.Size);
        var dFolder = root.GetFolder("d");
        Assert.Equal(24933642, dFolder.Size);
    }

    [Fact]
    public static void GetTotalSizeAtMost100000()
    {
        var outputs = File.ReadLines("GetRootInput.txt");
        Assert.Equal(95437, SolutionDay7.GetTotalSizeAtMost100000(outputs));
    }

    [Fact]
    public static void GetMinimumSizeDirectory()
    {
        var outputs = File.ReadLines("GetRootInput.txt");
        Assert.Equal(24933642, SolutionDay7.GetMinimumSizeDirectory(outputs));
    }
}