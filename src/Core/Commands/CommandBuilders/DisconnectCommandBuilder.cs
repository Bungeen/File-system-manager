namespace FileSystemManager.Core.Commands.CommandBuilders;

public class DisconnectCommandBuilder : ICommandBuilder
{
    public CommandBuilderBuildResult Build()
    {
        return new CommandBuilderBuildResult.Success(new DisconnectCommand());
    }
}