using FileSystemManager.Core.FileSystem.Interfaces;

namespace FileSystemManager.Core.FileSystem;

public class NullFileSystem : IFileSystem
{
    public bool FileExists(string path)
    {
        return false;
    }

    public bool DirectoryExists(string path)
    {
        return Directory.Exists(path);
    }

    public string PathCombine(string firstPath, string secondPath)
    {
        return string.Empty;
    }

    public string GetFileName(string path)
    {
        return string.Empty;
    }

    public string? GetDirectoryName(string path)
    {
        return null;
    }

    public FileSystemOperationResult GetFullPath(string path)
    {
        string fullPath;

        try
        {
            fullPath = Path.GetFullPath(path);
        }
        catch
        {
            return new FileSystemOperationResult.Failure("Invalid path");
        }

        return new FileSystemOperationResult.Success(fullPath);
    }

    public FileSystemOperationResult ReadFile(string path)
    {
        return new FileSystemOperationResult.Failure("File system disconnected");
    }

    public FileSystemCommandResult MoveFile(string source, string destination)
    {
        return new FileSystemCommandResult.Failure("File system disconnected");
    }

    public FileSystemCommandResult CopyFile(string source, string destination)
    {
        return new FileSystemCommandResult.Failure("File system disconnected");
    }

    public FileSystemCommandResult DeleteFile(string path)
    {
        return new FileSystemCommandResult.Failure("File system disconnected");
    }

    public IEnumerable<IFileSystemComponent> Enumerate(string currentLocalPath, int depth = 1)
    {
        return [];
    }

    public IEnumerable<string> EnumerateFileSystemEntries(string path)
    {
        return [];
    }

    public FileSystemOperationResult NormalizePath(string path, string currentLocalPath, string connectionPath)
    {
        return new FileSystemOperationResult.Failure("File system disconnected");
    }
}