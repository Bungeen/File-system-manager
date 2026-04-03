using FileSystemManager.Core.FileSystem;
using FileSystemManager.Core.FileSystem.SessionSystem;

namespace FileSystemManager.Core.Commands;

public class FileMoveCommand : ICommand
{
    public string Source { get; private set; }

    public string Destination { get; private set; }

    public FileMoveCommand(string source, string destination)
    {
        Source = source;
        Destination = destination;
    }

    public CommandResult Execute(FileSystemSession session)
    {
        FileSystemOperationResult result = session.FileSystem.NormalizePath(Source, session.CurrentLocalPath, session.ConnectionPath);

        if (result is FileSystemOperationResult.Failure failure)
            return new CommandResult.Failure(failure.Error);

        if (result is FileSystemOperationResult.Success success)
            Source = success.Value;

        result = session.FileSystem.NormalizePath(Destination, session.CurrentLocalPath, session.ConnectionPath);

        if (result is FileSystemOperationResult.Failure failureDestination)
            return new CommandResult.Failure(failureDestination.Error);

        if (result is FileSystemOperationResult.Success successDestination)
            Destination = successDestination.Value;

        if (!session.FileSystem.FileExists(Source))
            return new CommandResult.Failure("File not found");

        if (session.FileSystem.FileExists(Destination))
            return new CommandResult.Failure("Destination path must be a directory, not a file");

        if (!session.FileSystem.DirectoryExists(Destination))
            return new CommandResult.Failure("Invalid directory");

        string newFilePath = session.FileSystem.PathCombine(Destination, session.FileSystem.GetFileName(Source));

        if (Source == newFilePath)
            return new CommandResult.Failure("File with same already exists");

        if (session.FileSystem.FileExists(newFilePath))
            return new CommandResult.Failure("File with this name already exists");

        session.FileSystem.MoveFile(Source, newFilePath);

        return new CommandResult.Success();
    }
}