using FileSystemManager.Core.Commands.CommandBuilders;
using FileSystemManager.Core.Commands.ShowCommandModes;

namespace FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.ArgumentValueChain;

public class FileShowConsoleModeChainLink : BaseArgumentValueChainLink<FileShowCommandBuilder>
{
    public override CommandChainResult Apply(FileShowCommandBuilder builder, IEnumerator<string> iterator)
    {
        if (iterator.Current != "-m")
            return CallNext(builder, iterator);

        if (!iterator.MoveNext())
            return new CommandChainResult.Failure("Mode value is missing");

        if (iterator.Current == "console")
        {
            builder.WithMode(new ShowCommandConsoleMode());
            return new CommandChainResult.Success(builder);
        }

        return CallNext(builder, iterator);
    }
}