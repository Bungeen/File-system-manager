using FileSystemManager.Core.Commands.CommandBuilders;

namespace FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.ArgumentValueChain;

public class TerminalValueChainLink<TBuilder> : BaseArgumentValueChainLink<TBuilder> where TBuilder : ICommandBuilder
{
    public override CommandChainResult Apply(TBuilder builder, IEnumerator<string> iterator)
    {
        return new CommandChainResult.Failure($"Invalid arguments");
    }
}