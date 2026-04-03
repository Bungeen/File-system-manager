using FileSystemManager.Core.FileSystem;
using FileSystemManager.Core.FileSystem.SessionSystem;

namespace FileSystemManager.Core.Commands;

public class TreeGotoCommand : ICommand
{
    public string Path { get; private set; }

    public TreeGotoCommand(string path)
    {
        Path = path;
    }

    public CommandResult Execute(FileSystemSession session)
    {
        if (!session.IsConnected)
            return new CommandResult.Failure("File system disconnected");

        FileSystemOperationResult result = session.FileSystem.NormalizePath(Path, session.CurrentLocalPath, session.ConnectionPath);

        if (result is FileSystemOperationResult.Failure failure)
            return new CommandResult.Failure(failure.Error);

        if (result is FileSystemOperationResult.Success success)
            Path = success.Value;

        session.ChangeLocalPath(Path);

        return new CommandResult.Success();
    }
}