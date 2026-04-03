using FileSystemManager.Core.Commands.CommandBuilders;
using FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.ArgumentValueChain;

namespace FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.CommandParserChain;

public class ConnectCommandChainLink : BaseCommandChainLink
{
    private readonly IArgumentValueChain<ConnectCommandBuilder> _chain;

    public ConnectCommandChainLink(IArgumentValueChain<ConnectCommandBuilder> chain)
    {
        _chain = chain;
    }

    public override CommandChainResult Apply(IEnumerator<string> iterator)
    {
        if (iterator.Current == "connect")
        {
            ConnectCommandBuilder builder = new();

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