namespace FileSystemManager.Core.Commands.ShowCommandModes;

public class ShowCommandConsoleMode : IShowCommandMode
{
    public void ShowString(string message)
    {
        Console.WriteLine(message);
    }
}