using FileSystemManager.Core.Commands.ShowCommandModes;

namespace FileSystemManager.Core.Commands.CommandBuilders;

public class FileShowCommandBuilder : ICommandBuilder
{
    private IShowCommandMode _commandMode = new ShowCommandConsoleMode();
    private string? _path = null;

    public CommandBuilderResult WithMode(IShowCommandMode mode)
    {
        _commandMode = mode;
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
            return new CommandBuilderBuildResult.Success(new FileShowCommand(path: _path, _commandMode));

        return new CommandBuilderBuildResult.Failure("Path is missing");
    }
}