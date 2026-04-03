namespace FileSystemManager.Core.FileSystem.SessionSystem;

public abstract record FileSystemSessionResult
{
    private FileSystemSessionResult() { }

    public sealed record Success() : FileSystemSessionResult;

    public sealed record Failure(string Error) : FileSystemSessionResult;
}