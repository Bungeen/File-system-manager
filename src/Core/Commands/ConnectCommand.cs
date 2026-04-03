using FileSystemManager.Core.FileSystem;
using FileSystemManager.Core.FileSystem.Interfaces;
using FileSystemManager.Core.FileSystem.SessionSystem;

namespace FileSystemManager.Core.Commands;

public class ConnectCommand : ICommand
{
    public IFileSystem FileSystem { get; }

    public string Path { get; }

    public ConnectCommand(IFileSystem fileSystem, string path)
    {
        FileSystem = fileSystem;
        Path = path;
    }

    public CommandResult Execute(FileSystemSession session)
    {
        if (session.IsConnected)
            return new CommandResult.Failure("Session already connected");

        FileSystemOperationResult fullPath = FileSystem.GetFullPath(Path);

        if (fullPath is FileSystemOperationResult.Failure pathFailure)
            return new CommandResult.Failure(pathFailure.Error);

        if (fullPath is FileSystemOperationResult.Success success)
        {
            if (FileSystem.DirectoryExists(success.Value))
            {
                session.Connect(FileSystem, success.Value);
                return new CommandResult.Success();
            }
        }

        return new CommandResult.Failure("Invalid path");
    }
}