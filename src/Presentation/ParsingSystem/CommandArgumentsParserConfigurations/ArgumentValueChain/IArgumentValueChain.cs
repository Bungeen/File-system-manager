namespace FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.ArgumentValueChain;

public interface IArgumentValueChain<TBuilder>
{
    CommandChainResult Apply(TBuilder builder, IEnumerator<string> iterator);
}