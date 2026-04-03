using FileSystemManager.Core.Commands.CommandBuilders;

namespace FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations.ArgumentValueChain;

public class TreeListDepthChainLink : BaseArgumentValueChainLink<TreeListCommandBuilder>
{
    public override CommandChainResult Apply(TreeListCommandBuilder builder, IEnumerator<string> iterator)
    {
        if (iterator.Current != "-d")
            return CallNext(builder, iterator);

        if (!iterator.MoveNext())
            return new CommandChainResult.Failure("Depth value is missing");

        if (int.TryParse(iterator.Current, out int number))
        {
            builder.WithDepth(number);
            return new CommandChainResult.Success(builder);
        }
        else
        {
            return new CommandChainResult.Failure("Depth value is invalid");
        }
    }
}