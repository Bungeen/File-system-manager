using FileSystemManager.Core.Commands.CommandBuilders;
using FileSystemManager.Core.FileSystem.LocalFileSystemRealization;

namespace FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.ArgumentValueChain;

public class ConnectLocalModeChainLink : BaseArgumentValueChainLink<ConnectCommandBuilder>
{
    public override CommandChainResult Apply(ConnectCommandBuilder builder, IEnumerator<string> iterator)
    {
        if (iterator.Current != "-m")
            return CallNext(builder, iterator);

        if (!iterator.MoveNext())
            return new CommandChainResult.Failure("Mode value is missing");

        if (iterator.Current == "local")
        {
            builder.WithMode(new LocalFileSystem());
            return new CommandChainResult.Success(builder);
        }

        return CallNext(builder, iterator);
    }
}