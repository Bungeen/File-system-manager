using FileSystemManager.Core.Commands.ShowCommandModes;
using FileSystemManager.Core.Commands.TreeListOutputConfig;
using FileSystemManager.Core.FileSystem.LocalFileSystemRealization.FileSystemTree;
using FileSystemManager.Core.FileSystem.SessionSystem;

namespace FileSystemManager.Core.Commands;

public class TreeListCommand : ICommand
{
    public int Depth { get; private set; }

    public TreeListOutputValues OutputValues { get; private set; }

    public IShowCommandMode Mode { get; private set; }

    public TreeListCommand(int depth, TreeListOutputValues outputValues, IShowCommandMode mode)
    {
        Depth = depth;
        OutputValues = outputValues;
        Mode = mode;
    }

    public CommandResult Execute(FileSystemSession session)
    {
        if (!session.IsConnected)
            return new CommandResult.Failure("File system disconnected");

        FormattingVisitor visitor = new(Depth, OutputValues);

        if (!session.FileSystem.DirectoryExists(session.CurrentLocalPath))
            return new CommandResult.Failure("Invalid directory");

        string? name = session.FileSystem.GetFileName(session.CurrentLocalPath);

        if (string.IsNullOrEmpty(name))
            return new CommandResult.Failure("Invalid directory name");
        DirectoryFileSystemComponent start = new(name, session.CurrentLocalPath, session.FileSystem);

        start.Accept(visitor);

        Mode.ShowString(visitor.Value);

        return new CommandResult.Success();
    }
}