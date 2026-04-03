using FileSystemManager.Core.Commands.TreeListOutputConfig;
using FileSystemManager.Core.FileSystem.SessionSystem;
using FileSystemManager.Presentation.ParsingSystem;
using FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations;

namespace FileSystemManager.Presentation;

public class Program
{
    public static void Main(string[] args)
    {
        Parser parser = new(new TreeListOutputValues(), new ParsingChainFactory());

        var session = new FileSystemSession();

        ConsoleExecutor executor = new(parser, session);
        executor.Run();
    }
}