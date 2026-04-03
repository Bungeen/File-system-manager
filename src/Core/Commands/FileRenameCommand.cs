using FileSystemManager.Core.FileSystem;
using FileSystemManager.Core.FileSystem.SessionSystem;

namespace FileSystemManager.Core.Commands;

public class FileRenameCommand : ICommand
{
    public string Name { get; private set; }

    public string Path { get; private set; }

    public FileRenameCommand(string name, string path)
    {
        Name = name;
        Path = path;
    }

    public CommandResult Execute(FileSystemSession session)
    {
        FileSystemOperationResult result = session.FileSystem.NormalizePath(Path, session.CurrentLocalPath, session.ConnectionPath);

        if (result is FileSystemOperationResult.Failure failure)
            return new CommandResult.Failure(failure.Error);

        if (result is FileSystemOperationResult.Success success)
            Path = success.Value;

        if (!session.FileSystem.FileExists(Path))
            return new CommandResult.Failure("File not found");

        string? rootDirectory = session.FileSystem.GetDirectoryName(Path);
        if (string.IsNullOrEmpty(rootDirectory))
            return new CommandResult.Failure("Invalid path");

        string newFilePath = session.FileSystem.PathCombine(rootDirectory, Name);

        if (session.FileSystem.FileExists(newFilePath))
            return new CommandResult.Failure("File with this name already exists");

        session.FileSystem.MoveFile(Path, newFilePath);

        return new CommandResult.Success();
    }
}