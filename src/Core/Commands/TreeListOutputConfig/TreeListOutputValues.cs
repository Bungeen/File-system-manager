namespace FileSystemManager.Core.Commands.TreeListOutputConfig;

public record TreeListOutputValues
{
    public string Folder { get; init; } = "D";

    public string File { get; init; } = "F";

    public string Splitter { get; init; } = "-";

    public TreeListOutputValues(string folder, string file, string splitter)
    {
        Folder = folder;
        File = file;
        Splitter = splitter;
    }

    public TreeListOutputValues()
    {
    }
}