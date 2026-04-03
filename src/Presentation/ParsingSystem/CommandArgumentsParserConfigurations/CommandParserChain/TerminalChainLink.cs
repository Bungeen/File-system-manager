namespace FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.CommandParserChain;

public class TerminalChainLink : BaseCommandChainLink
{
    public override CommandChainResult Apply(IEnumerator<string> iterator)
    {
        return new CommandChainResult.Failure($"Invalid command");
    }
}