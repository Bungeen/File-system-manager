namespace FileSystemManager.Core.Commands.CommandBuilders;

public class FileRenameCommandBuilder : ICommandBuilder
{
    private string? _path = null;
    private string? _name = null;

    public CommandBuilderResult WithPath(string value)
    {
        if (_path is null)
        {
            _path = value;
            return new CommandBuilderResult.Success();
        }

        return new CommandBuilderResult.Failure($"Invalid positional argument '{value}'");
    }

    public CommandBuilderResult WithName(string value)
    {
        if (_name is null)
        {
            _name = value;
            return new CommandBuilderResult.Success();
        }

        return new CommandBuilderResult.Failure($"Invalid positional argument '{value}'");
    }

    public CommandBuilderBuildResult Build()
    {
        if (_path is not null && _name is not null)
            return new CommandBuilderBuildResult.Success(new FileRenameCommand(_name, _path));

        if (_path is null)
            return new CommandBuilderBuildResult.Failure("Path is missing");

        return new CommandBuilderBuildResult.Failure("Name is missing");
    }
}