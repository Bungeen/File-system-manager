using FileSystemManager.Core.Commands.CommandBuilders;
using FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.ArgumentValueChain;

namespace FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.CommandParserChain;

public class RenameCommandChainLink : BaseCommandChainLink
{
    private readonly IArgumentValueChain<FileRenameCommandBuilder> _chain;

    public RenameCommandChainLink(IArgumentValueChain<FileRenameCommandBuilder> chain)
    {
        _chain = chain;
    }

    public override CommandChainResult Apply(IEnumerator<string> iterator)
    {
        if (iterator.Current == "rename")
        {
            FileRenameCommandBuilder builder = new();

            while (iterator.MoveNext())
            {
                CommandChainResult result = _chain.Apply(builder, iterator);

                if (result is CommandChainResult.Failure failure)
                    return result;
            }

            return new CommandChainResult.Success(builder);
        }

        return CallNext(iterator);
    }
}