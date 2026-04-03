using FileSystemManager.Core.Commands;
using FileSystemManager.Core.FileSystem.Interfaces;

namespace FileSystemManager.Core.FileSystem.SessionSystem;

public class FileSystemSession
{
    public IFileSystem FileSystem { get; private set; } = new NullFileSystem();

    public bool IsConnected => FileSystem is not NullFileSystem;

    public string ConnectionPath { get; private set; } = string.Empty;

    public string CurrentLocalPath { get; private set; } = string.Empty;

    public FileSystemSessionResult ExecuteCommand(ICommand command)
    {
        CommandResult result = command.Execute(this);

        if (result is CommandResult.Failure failure)
            return new FileSystemSessionResult.Failure(failure.Error);

        return new FileSystemSessionResult.Success();
    }

    public void Connect(IFileSystem fileSystem, string connectionPath)
    {
        FileSystem = fileSystem;

        ConnectionPath = connectionPath;
        CurrentLocalPath = ConnectionPath;
    }

    public void Disconnect()
    {
        FileSystem = new NullFileSystem();
        ConnectionPath = string.Empty;
        CurrentLocalPath = string.Empty;
    }

    public void ChangeLocalPath(string path)
    {
        CurrentLocalPath = path;
    }
}