namespace FileSystemManager.Core.FileSystem;

public abstract record FileSystemCommandResult
{
    private FileSystemCommandResult() { }

    public sealed record Success() : FileSystemCommandResult;

    public sealed record Failure(string Error) : FileSystemCommandResult;
}