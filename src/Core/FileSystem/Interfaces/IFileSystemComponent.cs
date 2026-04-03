using FileSystemManager.Core.FileSystem.LocalFileSystemRealization.FileSystemTree;

namespace FileSystemManager.Core.FileSystem.Interfaces;

public interface IFileSystemComponent
{
    string Name { get; }

    string Path { get; }

    void Accept(IFileSystemComponentVisitor visitor);
}