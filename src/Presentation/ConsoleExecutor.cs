using FileSystemManager.Core.FileSystem.SessionSystem;
using FileSystemManager.Presentation.ParsingSystem;

namespace FileSystemManager.Presentation;

public class ConsoleExecutor
{
    private readonly Parser _parser;

    private readonly FileSystemSession _session;

    public ConsoleExecutor(Parser parser, FileSystemSession session)
    {
        _parser = parser;
        _session = session;
    }

    public void Run()
    {
        while (true)
        {
            string? input = Console.ReadLine();

            if (input is null)
                continue;

            if (input == "exit")
                break;

            ParserResult result = _parser.Parse(input);

            if (result is ParserResult.Failure failure)
            {
                Console.WriteLine(failure.Error);
                continue;
            }

            if (result is ParserResult.Success success)
            {
                FileSystemSessionResult commandResult = _session.ExecuteCommand(success.Command);

                if (commandResult is FileSystemSessionResult.Failure failureCommand)
                {
                    Console.WriteLine(failureCommand.Error);
                }
            }
        }
    }
}