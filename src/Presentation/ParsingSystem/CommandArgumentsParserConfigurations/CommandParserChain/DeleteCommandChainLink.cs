using FileSystemManager.Core.Commands.CommandBuilders;
using FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.ArgumentValueChain;

namespace FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.CommandParserChain;

public class DeleteCommandChainLink : BaseCommandChainLink
{
    private readonly IArgumentValueChain<FileDeleteCommandBuilder> _chain;

    public DeleteCommandChainLink(IArgumentValueChain<FileDeleteCommandBuilder> chain)
    {
        _chain = chain;
    }

    public override CommandChainResult Apply(IEnumerator<string> iterator)
    {
        if (iterator.Current == "delete")
        {
            FileDeleteCommandBuilder builder = new();

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