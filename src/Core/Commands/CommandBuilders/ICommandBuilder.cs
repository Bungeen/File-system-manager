namespace FileSystemManager.Core.Commands.CommandBuilders;

public interface ICommandBuilder
{
    CommandBuilderBuildResult Build();
}