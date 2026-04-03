namespace FileSystemManager.Core.Commands.CommandBuilders;

public class FileDeleteCommandBuilder : ICommandBuilder
{
    private string? _path = null;

    public CommandBuilderResult WithPath(string value)
    {
        if (_path == null)
        {
            _path = value;
            return new CommandBuilderResult.Success();
        }

        return new CommandBuilderResult.Failure($"Invalid positional argument '{value}'");
    }

    public CommandBuilderBuildResult Build()
    {
        if (_path is not null)
            return new CommandBuilderBuildResult.Success(new FileDeleteCommand(_path));

        return new CommandBuilderBuildResult.Failure($"Path is missing");
    }
}