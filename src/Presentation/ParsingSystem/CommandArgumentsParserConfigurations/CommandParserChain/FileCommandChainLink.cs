namespace FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.CommandParserChain;

public class FileCommandChainLink : BaseCommandChainLink
{
    private readonly ICommandChain _chain;

    public FileCommandChainLink(ICommandChain chain)
    {
        _chain = chain;
    }

    public override CommandChainResult Apply(IEnumerator<string> iterator)
    {
        if (iterator.Current == "file")
        {
            iterator.MoveNext();
            return _chain.Apply(iterator);
        }

        return CallNext(iterator);
    }
}