namespace FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.ArgumentValueChain;

public interface IArgumentValueChainLink<TBuilder> : IArgumentValueChain<TBuilder>
{
    IArgumentValueChainLink<TBuilder> AddNext(IArgumentValueChainLink<TBuilder> link);
}