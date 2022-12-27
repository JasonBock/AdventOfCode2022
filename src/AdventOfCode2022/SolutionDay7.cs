using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2022.Day7;

public static class SolutionDay7
{
    private const long MaximumFolderSize = 100_000L;
    private const long TotalDiskSize = 70_000_000L;
    private const long MinimumFreeSpace = 30_000_000L;

    public static long GetMinimumSizeDirectory(IEnumerable<string> outputs)
    {
        static long FindSmallestFolderSize(Folder folder, long minimumSize)
        {
            var smallestFolderSize = 0L;

            if(folder.Size > minimumSize)
            {
                smallestFolderSize = folder.Size;

                foreach(var subFolder in folder.EnumerateFolders())
                {
                    var subFolderSmallestSize = FindSmallestFolderSize(subFolder, minimumSize);
                    smallestFolderSize = subFolderSmallestSize > 0L && subFolderSmallestSize < smallestFolderSize ?
                        subFolderSmallestSize : smallestFolderSize;
                }
            }

            return smallestFolderSize;
        }

        var root = SolutionDay7.GetRootFolder(outputs);
        var rootSize = root.Size;

        var currentFreeSpace = SolutionDay7.TotalDiskSize - rootSize;

        if(currentFreeSpace < SolutionDay7.MinimumFreeSpace)
        {
            return FindSmallestFolderSize(root, SolutionDay7.MinimumFreeSpace - currentFreeSpace);
        }
        else
        {
            throw new UnreachableException();
        }
    }

    public static long GetTotalSizeAtMost100000(IEnumerable<string> outputs)
    {
        static long GetTotal(Folder folder)
        {
            var total = 0L;

            if(folder.Size <= SolutionDay7.MaximumFolderSize)
            {
                total = folder.Size;
            }

            foreach (var subFolder in folder.EnumerateFolders())
            {
                total += GetTotal(subFolder);
            }

            return total;
        }

        return GetTotal(SolutionDay7.GetRootFolder(outputs));
    }

    public static Folder GetRootFolder(IEnumerable<string> outputs)
    {
        // We know we're going to have a root folder.
        var root = new Folder();
        var inListMode = false;
        var currentFolder = root;

        foreach (var output in outputs)
        {
            if (output[0] == '$')
            {
                // It's a command, either ls or cd
                if (output[2..4] == "cd")
                {
                    inListMode = false;

                    // what folder do we want to go to?
                    var targetFolder = output[5..];

                    if (targetFolder == "/")
                    {
                        currentFolder = currentFolder.Root;
                    }
                    else if (targetFolder == "..")
                    {
                        currentFolder = currentFolder.Parent ?? currentFolder.Root;
                    }
                    else
                    {
                        // we need to go in, so get it
                        // (it should've been created already)
                        currentFolder = currentFolder.GetFolder(targetFolder);
                    }
                }
                else
                {
                    // It's ls, so we need to grab the folder's content.
                    inListMode = true;
                }
            }
            else
            {
                // It's a result, so we have to be in list mode.

                if (!inListMode)
                {
                    throw new UnreachableException();
                }

                // It could be a folder, or a file
                var resultParts = output.Split(' ');

                if (resultParts[0] == "dir")
                {
                    // Need to add this folder to the current folder.
                    currentFolder.AddFolder(resultParts[1]);
                }
                else
                {
                    // Need to add this file to the current folder
                    currentFolder.AddDocument(resultParts[1], long.Parse(resultParts[0]));
                }
            }
        }

        return root;
    }
}

public sealed class Document
{
    public Document(string name, long size, Folder folder) =>
        (this.Name, this.Size, this.Folder) = (name, size, folder);

    public Folder Folder { get; }
    public string Name { get; }
    public long Size { get; }
}

public sealed class Folder
{
    private readonly List<Document> documents = new();
    private readonly List<Folder> folders = new();

    public Folder() =>
        this.Path = "/";

    private Folder(string path, Folder parent) =>
        (this.Path, this.Parent) = (path, parent);

    public void AddDocument(string name, long size) => 
        this.documents.Add(new Document(name, size, this));

    public Folder GetFolder(string path) => 
        this.folders.Single(_ => _.Path == path);

    public IEnumerable<Document> EnumerateDocuments()
    {
        foreach (var document in this.documents)
        {
            yield return document;
        }
    }

    public IEnumerable<Folder> EnumerateFolders()
    {
        foreach(var folder in this.folders) 
        {
            yield return folder;
        }
    }

    public Folder AddFolder(string path)
    {
        var folder = new Folder(path, this);
        this.folders.Add(folder);
        return folder;
    }

    public string Path { get; }

    public long Size
    {
        get
        {
            var totalSize = 0L;

            foreach (var document in this.documents)
            {
                totalSize += document.Size;
            }

            foreach (var folder in this.folders)
            {
                totalSize += folder.Size;
            }

            return totalSize;
        }
    }

    public Folder? Parent { get; }

    public Folder Root
    {
        get
        {
            var root = this;

            while(root.Parent is not null)
            {
                root = root.Parent;
            }

            return root;
        }
    }
}