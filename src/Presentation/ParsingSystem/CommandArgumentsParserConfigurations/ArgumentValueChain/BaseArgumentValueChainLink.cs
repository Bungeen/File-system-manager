using FileSystemManager.Core.Commands.CommandBuilders;

namespace FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.ArgumentValueChain;

public abstract class BaseArgumentValueChainLink<TBuilder> : IArgumentValueChainLink<TBuilder> where TBuilder : ICommandBuilder
{
    private IArgumentValueChainLink<TBuilder>? _link;

    public abstract CommandChainResult Apply(TBuilder builder, IEnumerator<string> iterator);

    public IArgumentValueChainLink<TBuilder> AddNext(IArgumentValueChainLink<TBuilder> link)
    {
        if (_link is null)
        {
            _link = link;
            return this;
        }

        _link.AddNext(link);
        return this;
    }

    protected CommandChainResult CallNext(TBuilder builder, IEnumerator<string> request)
    {
        return _link?.Apply(builder, request)
               ?? throw new InvalidOperationException("Chain missing terminal link");
    }
}