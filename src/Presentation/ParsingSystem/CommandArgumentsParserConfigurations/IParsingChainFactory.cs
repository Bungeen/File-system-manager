using FileSystemManager.Core.Commands.TreeListOutputConfig;
using FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.CommandParserChain;

namespace FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations;

public interface IParsingChainFactory
{
    ICommandChain Create(TreeListOutputValues icons);
}