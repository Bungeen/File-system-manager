using FileSystemManager.Core.FileSystem.Interfaces;

namespace FileSystemManager.Core.FileSystem.LocalFileSystemRealization;

public class LocalFileSystem : IFileSystem
{
    public bool FileExists(string path) => File.Exists(path);

    public bool DirectoryExists(string path) => Directory.Exists(path);

    public string PathCombine(string firstPath, string secondPath) => Path.Combine(firstPath, secondPath);

    public string GetFileName(string path) => Path.GetFileName(path);

    public string? GetDirectoryName(string path) => Path.GetDirectoryName(path);

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
        return new FileSystemOperationResult.Success(File.ReadAllText(path));
    }

    public FileSystemCommandResult MoveFile(string source, string destination)
    {
        File.Move(source, destination);
        return new FileSystemCommandResult.Success();
    }

    public FileSystemCommandResult CopyFile(string source, string destination)
    {
        File.Copy(source, destination);
        return new FileSystemCommandResult.Success();
    }

    public FileSystemCommandResult DeleteFile(string path)
    {
        File.Delete(path);
        return new FileSystemCommandResult.Success();
    }

    public IEnumerable<string> EnumerateFileSystemEntries(string path)
    {
        return Directory.EnumerateFileSystemEntries(path);
    }

    public FileSystemOperationResult NormalizePath(string path, string currentLocalPath, string connectionPath)
    {
        if (string.IsNullOrEmpty(path))
            return new FileSystemOperationResult.Success(currentLocalPath);

        if (path.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
            return new FileSystemOperationResult.Failure("Path contains invalid characters");

        string fullPath;
        if (path.StartsWith('/'))
        {
            string trimmedPath = path.TrimStart('/');
            try
            {
                string combined = Path.Combine(connectionPath, trimmedPath);
                fullPath = Path.GetFullPath(combined);
            }
            catch
            {
                return new FileSystemOperationResult.Failure("Invalid path format");
            }
        }
        else
        {
            string combinedPath = Path.Combine(currentLocalPath, path);
            try
            {
                fullPath = Path.GetFullPath(combinedPath);
            }
            catch
            {
                return new FileSystemOperationResult.Failure("Invalid path format");
            }
        }

        if (fullPath.StartsWith(connectionPath, StringComparison.OrdinalIgnoreCase))
            return new FileSystemOperationResult.Success(fullPath);

        return new FileSystemOperationResult.Failure("Path outside filesystem");
    }
}