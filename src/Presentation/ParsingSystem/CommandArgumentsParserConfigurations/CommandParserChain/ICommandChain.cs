namespace FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.CommandParserChain;

public interface ICommandChain
{
    CommandChainResult Apply(IEnumerator<string> iterator);
}