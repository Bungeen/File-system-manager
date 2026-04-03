namespace FileSystemManager.Core.Commands.CommandBuilders;

public abstract record CommandBuilderResult
{
    private CommandBuilderResult() { }

    public sealed record Success() : CommandBuilderResult;

    public sealed record Failure(string Error) : CommandBuilderResult;
}