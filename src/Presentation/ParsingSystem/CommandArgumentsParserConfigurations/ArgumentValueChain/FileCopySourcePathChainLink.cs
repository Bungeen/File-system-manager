using FileSystemManager.Core.Commands.CommandBuilders;

namespace FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.ArgumentValueChain;

public class FileCopySourcePathChainLink : BaseArgumentValueChainLink<FileCopyCommandBuilder>
{
    public override CommandChainResult Apply(FileCopyCommandBuilder builder, IEnumerator<string> iterator)
    {
        if (iterator.Current.StartsWith('-'))
            return CallNext(builder, iterator);

        CommandBuilderResult result = builder.WithFromPath(iterator.Current);

        if (result is CommandBuilderResult.Success success)
        {
            return new CommandChainResult.Success(builder);
        }

        return CallNext(builder, iterator);
    }
}