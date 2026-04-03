namespace FileSystemManager.Core.FileSystem.LocalFileSystemRealization.FileSystemTree;

public interface IFileSystemComponentVisitor
{
    void Visit(FileFileSystemComponent component);

    void Visit(DirectoryFileSystemComponent component);
}