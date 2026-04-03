using FileSystemManager.Core.Commands.CommandBuilders;
using FileSystemManager.Core.Commands.TreeListOutputConfig;
using FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.ArgumentValueChain;

namespace FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.CommandParserChain;

public class ListCommandChainLink : BaseCommandChainLink
{
    private readonly IArgumentValueChain<TreeListCommandBuilder> _chain;

    private readonly TreeListOutputValues _icons;

    public ListCommandChainLink(IArgumentValueChain<TreeListCommandBuilder> chain, TreeListOutputValues icons)
    {
        _chain = chain;
        _icons = icons;
    }

    public override CommandChainResult Apply(IEnumerator<string> iterator)
    {
        if (iterator.Current == "list")
        {
            TreeListCommandBuilder builder = new();

            builder.WithOutputValues(_icons);

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