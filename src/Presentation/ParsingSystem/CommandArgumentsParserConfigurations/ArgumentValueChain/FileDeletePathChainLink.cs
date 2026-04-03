using FileSystemManager.Core.Commands.CommandBuilders;

namespace FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.ArgumentValueChain;

public class FileDeletePathChainLink : BaseArgumentValueChainLink<FileDeleteCommandBuilder>
{
    public override CommandChainResult Apply(FileDeleteCommandBuilder builder, IEnumerator<string> iterator)
    {
        if (iterator.Current.StartsWith('-'))
            return CallNext(builder, iterator);

        CommandBuilderResult result = builder.WithPath(iterator.Current);

        if (result is CommandBuilderResult.Success success)
        {
            return new CommandChainResult.Success(builder);
        }

        return CallNext(builder, iterator);
    }
}