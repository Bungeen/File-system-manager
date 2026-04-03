using FileSystemManager.Core.Commands.CommandBuilders;
using FileSystemManager.Core.Commands.TreeListOutputConfig;
using FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations;
using FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.CommandParserChain;

namespace FileSystemManager.Presentation.ParsingSystem;

public class Parser
{
    private readonly ICommandChain _chain;

    public Parser(TreeListOutputValues icons, IParsingChainFactory chainFactory)
    {
        _chain = chainFactory.Create(icons);
    }

    public ParserResult Parse(string input)
    {
        List<string> splitted = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

        List<string>.Enumerator iterator = splitted.GetEnumerator();
        iterator.MoveNext();

        CommandChainResult result = _chain.Apply(iterator);

        if (result is CommandChainResult.Failure failure)
            return new ParserResult.Failure(failure.Error);

        if (result is CommandChainResult.Success success)
        {
            CommandBuilderBuildResult buildResult = success.Builder.Build();

            if (buildResult is CommandBuilderBuildResult.Failure failureBuild)
                return new ParserResult.Failure(failureBuild.Error);

            if (buildResult is CommandBuilderBuildResult.Success successBuild)
                return new ParserResult.Success(successBuild.Command);
        }

        return new ParserResult.Failure("Unknown command3124");
    }
}