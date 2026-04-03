using FileSystemManager.Core.Commands.ShowCommandModes;
using FileSystemManager.Core.FileSystem;
using FileSystemManager.Core.FileSystem.SessionSystem;

namespace FileSystemManager.Core.Commands;

public class FileShowCommand : ICommand
{
    public string Path { get; private set; }

    public IShowCommandMode Mode { get; private set; }

    public FileShowCommand(string path, IShowCommandMode mode)
    {
        Path = path;
        Mode = mode;
    }

    public CommandResult Execute(FileSystemSession session)
    {
        FileSystemOperationResult resultPath = session.FileSystem.NormalizePath(Path, session.CurrentLocalPath, session.ConnectionPath);

        if (resultPath is FileSystemOperationResult.Failure failurePath)
            return new CommandResult.Failure(failurePath.Error);

        if (resultPath is FileSystemOperationResult.Success successPath)
        {
            if (!session.FileSystem.FileExists(successPath.Value))
                return new CommandResult.Failure("File not found");

            FileSystemOperationResult result = session.FileSystem.ReadFile(successPath.Value);

            if (result is FileSystemOperationResult.Success success)
            {
                Mode.ShowString(success.Value);
                return new CommandResult.Success();
            }

            if (result is FileSystemOperationResult.Failure failure)
                return new CommandResult.Failure(failure.Error);
        }

        return new CommandResult.Failure("Invalid path");
    }
}