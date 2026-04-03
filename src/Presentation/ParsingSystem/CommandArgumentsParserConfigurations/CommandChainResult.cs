using FileSystemManager.Core.Commands.CommandBuilders;

namespace FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations;

public record CommandChainResult
{
    private CommandChainResult() { }

    public sealed record Success(ICommandBuilder Builder) : CommandChainResult;

    public sealed record Failure(string Error) : CommandChainResult;
}