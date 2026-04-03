using FileSystemManager.Core.FileSystem.Interfaces;
using FileSystemManager.Core.FileSystem.LocalFileSystemRealization;

namespace FileSystemManager.Core.Commands.CommandBuilders;

public class ConnectCommandBuilder : ICommandBuilder
{
    private IFileSystem _mode = new LocalFileSystem();
    private string? _path = null;

    public CommandBuilderResult WithMode(IFileSystem value)
    {
        _mode = value;

        return new CommandBuilderResult.Success();
    }

    public CommandBuilderResult WithPath(string value)
    {
        if (_path is not null)
            return new CommandBuilderResult.Failure($"Invalid positional argument '{value}'");

        _path = value;

        return new CommandBuilderResult.Success();
    }

    public CommandBuilderBuildResult Build()
    {
        if (_path is not null)
            return new CommandBuilderBuildResult.Success(new ConnectCommand(_mode, _path));

        return new CommandBuilderBuildResult.Failure("Path is missing");
    }
}