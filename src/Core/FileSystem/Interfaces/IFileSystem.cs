namespace FileSystemManager.Core.FileSystem.Interfaces;

public interface IFileSystem
{
    bool FileExists(string path);

    bool DirectoryExists(string path);

    string PathCombine(string firstPath, string secondPath);

    string GetFileName(string path);

    string? GetDirectoryName(string path);

    FileSystemOperationResult GetFullPath(string path);

    FileSystemOperationResult ReadFile(string path);

    FileSystemCommandResult MoveFile(string source, string destination);

    FileSystemCommandResult CopyFile(string source, string destination);

    FileSystemCommandResult DeleteFile(string path);

    IEnumerable<string> EnumerateFileSystemEntries(string path);

    FileSystemOperationResult NormalizePath(string path, string currentLocalPath, string connectionPath);
}