using FileSystemManager.Core.Commands.ShowCommandModes;
using FileSystemManager.Core.Commands.TreeListOutputConfig;

namespace FileSystemManager.Core.Commands.CommandBuilders;

public class TreeListCommandBuilder : ICommandBuilder
{
    private int _depth = 1;
    private TreeListOutputValues? _outputValues = null;
    private IShowCommandMode _mode = new ShowCommandConsoleMode();

    public CommandBuilderResult WithDepth(int number)
    {
        _depth = number;

        return new CommandBuilderResult.Success();
    }

    public CommandBuilderResult WithOutputValues(TreeListOutputValues outputValues)
    {
        _outputValues = outputValues;

        return new CommandBuilderResult.Success();
    }

    public CommandBuilderResult WithConsoleMode(IShowCommandMode mode)
    {
        _mode = mode;

        return new CommandBuilderResult.Success();
    }

    public CommandBuilderBuildResult Build()
    {
        if (_outputValues is not null)
            return new CommandBuilderBuildResult.Success(new TreeListCommand(_depth, _outputValues, _mode));

        return new CommandBuilderBuildResult.Failure($"Depth is missing");
    }
}