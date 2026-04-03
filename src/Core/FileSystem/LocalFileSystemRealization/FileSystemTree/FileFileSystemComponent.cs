using FileSystemManager.Core.FileSystem.Interfaces;

namespace FileSystemManager.Core.FileSystem.LocalFileSystemRealization.FileSystemTree;

public class FileFileSystemComponent : IFileSystemComponent
{
    public FileFileSystemComponent(string name, string path)
    {
        Name = name;
        Path = path;
    }

    public string Name { get; }

    public string Path { get; }

    public void Accept(IFileSystemComponentVisitor visitor)
    {
        visitor.Visit(this);
    }
}