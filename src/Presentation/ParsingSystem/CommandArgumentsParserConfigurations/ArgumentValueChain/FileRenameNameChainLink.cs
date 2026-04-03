using FileSystemManager.Core.Commands.CommandBuilders;

namespace FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.ArgumentValueChain;

public class FileRenameNameChainLink : BaseArgumentValueChainLink<FileRenameCommandBuilder>
{
    public override CommandChainResult Apply(FileRenameCommandBuilder builder, IEnumerator<string> iterator)
    {
        if (iterator.Current.StartsWith('-'))
            return CallNext(builder, iterator);

        CommandBuilderResult result = builder.WithName(iterator.Current);

        if (result is CommandBuilderResult.Success success)
        {
            return new CommandChainResult.Success(builder);
        }

        return CallNext(builder, iterator);
    }
}