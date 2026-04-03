namespace FileSystemManager.Core.Commands.CommandBuilders;

public class FileCopyCommandBuilder : ICommandBuilder
{
    private string? _fromPath = null;
    private string? _toPath = null;

    public CommandBuilderResult WithFromPath(string value)
    {
        if (_fromPath is null)
        {
            _fromPath = value;
            return new CommandBuilderResult.Success();
        }

        return new CommandBuilderResult.Failure($"Invalid positional argument '{value}'");
    }

    public CommandBuilderResult WithToPath(string value)
    {
        if (_toPath is null)
        {
            _toPath = value;
            return new CommandBuilderResult.Success();
        }

        return new CommandBuilderResult.Failure($"Invalid positional argument '{value}'");
    }

    public CommandBuilderBuildResult Build()
    {
        if (_fromPath is not null && _toPath is not null)
            return new CommandBuilderBuildResult.Success(new FileCopyCommand(_fromPath, _toPath));

        if (_fromPath is null)
            return new CommandBuilderBuildResult.Failure("Source path is missing");

        return new CommandBuilderBuildResult.Failure("Destination path is missing");
    }
}