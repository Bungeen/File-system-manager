namespace FileSystemManager.Core.FileSystem;

public abstract record FileSystemOperationResult
{
    private FileSystemOperationResult() { }

    public sealed record Success(string Value) : FileSystemOperationResult;

    public sealed record Failure(string Error) : FileSystemOperationResult;
}