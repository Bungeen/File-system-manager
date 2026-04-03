namespace FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.CommandParserChain;

public class TreeCommandChainLink : BaseCommandChainLink
{
    private readonly ICommandChain _chain;

    public TreeCommandChainLink(ICommandChain chain)
    {
        _chain = chain;
    }

    public override CommandChainResult Apply(IEnumerator<string> iterator)
    {
        if (iterator.Current == "tree")
        {
            iterator.MoveNext();
            return _chain.Apply(iterator);
        }

        return CallNext(iterator);
    }
}