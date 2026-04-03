namespace FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.CommandParserChain;

public abstract class BaseCommandChainLink : ICommandChainLink
{
    private ICommandChainLink? _link;

    public abstract CommandChainResult Apply(IEnumerator<string> iterator);

    public ICommandChainLink AddNext(ICommandChainLink link)
    {
        if (_link is null)
        {
            _link = link;
            return this;
        }

        _link.AddNext(link);
        return this;
    }

    protected CommandChainResult CallNext(IEnumerator<string> request)
    {
        return _link?.Apply(request)
            ?? throw new InvalidOperationException("Chain missing terminal link");
    }
}