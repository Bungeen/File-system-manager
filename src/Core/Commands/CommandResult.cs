namespace FileSystemManager.Core.Commands;

public abstract record CommandResult
{
    private CommandResult() { }

    public sealed record Success() : CommandResult;

    public sealed record Failure(string Error) : CommandResult;
}