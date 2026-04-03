namespace FileSystemManager.Core.Commands.CommandBuilders;

public abstract record CommandBuilderBuildResult
{
    private CommandBuilderBuildResult() { }

    public sealed record Success(ICommand Command) : CommandBuilderBuildResult;

    public sealed record Failure(string Error) : CommandBuilderBuildResult;
}