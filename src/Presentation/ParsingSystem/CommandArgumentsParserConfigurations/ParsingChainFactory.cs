using FileSystemManager.Core.Commands.CommandBuilders;
using FileSystemManager.Core.Commands.TreeListOutputConfig;
using FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.ArgumentValueChain;
using FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.CommandParserChain;

namespace FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations;

public class ParsingChainFactory : IParsingChainFactory
{
    public ICommandChain Create(TreeListOutputValues icons)
    {
        IArgumentValueChain<FileCopyCommandBuilder> fileCopyChain =
            new FileCopySourcePathChainLink().AddNext(new FileCopyDestinationPathChainLink())
                .AddNext(new TerminalValueChainLink<FileCopyCommandBuilder>());

        IArgumentValueChain<FileRenameCommandBuilder> fileRenameChain =
            new FileRenamePathChainLink().AddNext(new FileRenameNameChainLink())
                .AddNext(new TerminalValueChainLink<FileRenameCommandBuilder>());

        IArgumentValueChain<FileMoveCommandBuilder> fileMoveChain =
            new FileMoveSourcePathChainLink().AddNext(new FileMoveDestinationPathChainLink())
                .AddNext(new TerminalValueChainLink<FileMoveCommandBuilder>());

        IArgumentValueChain<FileShowCommandBuilder> fileShowChain =
            new FileShowPathChainLink().AddNext(new FileShowConsoleModeChainLink())
                .AddNext(new TerminalValueChainLink<FileShowCommandBuilder>());

        IArgumentValueChain<FileDeleteCommandBuilder> fileDeleteChain = new FileDeletePathChainLink()
            .AddNext(new TerminalValueChainLink<FileDeleteCommandBuilder>());

        IArgumentValueChain<ConnectCommandBuilder> connectChainValue =
            new ConnectPathChainLink().AddNext(new ConnectLocalModeChainLink())
                .AddNext(new TerminalValueChainLink<ConnectCommandBuilder>());

        IArgumentValueChain<DisconnectCommandBuilder> disconnectChainValue =
            new TerminalValueChainLink<DisconnectCommandBuilder>();

        IArgumentValueChain<TreeListCommandBuilder> treeListChain = new TreeListDepthChainLink()
            .AddNext(new TerminalValueChainLink<TreeListCommandBuilder>());

        IArgumentValueChain<TreeGotoCommandBuilder> treeGotoChain = new TreeGotoPathChainLink()
            .AddNext(new TerminalValueChainLink<TreeGotoCommandBuilder>());

        ICommandChain fileChain = new CopyCommandChainLink(fileCopyChain)
            .AddNext(new MoveCommandChainLink(fileMoveChain))
            .AddNext(new RenameCommandChainLink(fileRenameChain))
            .AddNext(new ShowCommandChainLink(fileShowChain))
            .AddNext(new DeleteCommandChainLink(fileDeleteChain))
            .AddNext(new TerminalChainLink());

        ICommandChain treeChain = new GotoCommandChainLink(treeGotoChain)
            .AddNext(new ListCommandChainLink(treeListChain, icons))
            .AddNext(new TerminalChainLink());

        ICommandChainLink connectChain = new ConnectCommandChainLink(connectChainValue);

        ICommandChainLink disconnectChain = new DisconnectCommandChainLink(disconnectChainValue);

        ICommandChain chain = new FileCommandChainLink(fileChain)
            .AddNext(new TreeCommandChainLink(treeChain))
            .AddNext(connectChain)
            .AddNext(disconnectChain)
            .AddNext(new TerminalChainLink());

        return chain;
    }
}