using FileSystemManager.Core.Commands;

namespace FileSystemManager.Presentation.ParsingSystem;

public abstract record ParserResult
{
    private ParserResult() { }

    public sealed record Success(ICommand Command) : ParserResult;

    public sealed record Failure(string Error) : ParserResult;
}