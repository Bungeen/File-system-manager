using FileSystemManager.Core.FileSystem;
using FileSystemManager.Core.FileSystem.SessionSystem;

namespace FileSystemManager.Core.Commands;

public class FileDeleteCommand : ICommand
{
    public string Path { get; private set; }

    public FileDeleteCommand(string path)
    {
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

        session.FileSystem.DeleteFile(Path);

        return new CommandResult.Success();
    }
}