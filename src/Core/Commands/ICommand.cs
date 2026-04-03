using FileSystemManager.Core.FileSystem.SessionSystem;

namespace FileSystemManager.Core.Commands;

public interface ICommand
{
    CommandResult Execute(FileSystemSession session);
}