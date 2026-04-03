namespace FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.CommandParserChain;

public interface ICommandChainLink : ICommandChain
{
    ICommandChainLink AddNext(ICommandChainLink link);
}