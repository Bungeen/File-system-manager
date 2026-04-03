using FileSystemManager.Core.FileSystem.SessionSystem;

namespace FileSystemManager.Core.Commands;

public class DisconnectCommand : ICommand
{
    public CommandResult Execute(FileSystemSession session)
    {
        if (!session.IsConnected)
            return new CommandResult.Failure("Already disconnected");

        session.Disconnect();

        return new CommandResult.Success();
    }
}