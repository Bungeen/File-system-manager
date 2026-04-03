using FileSystemManager.Core.FileSystem.Interfaces;

namespace FileSystemManager.Core.FileSystem.LocalFileSystemRealization.FileSystemTree;

public class DirectoryFileSystemComponent : IFileSystemComponent
{
    public DirectoryFileSystemComponent(string name, string path, IFileSystem fileSystem)
    {
        Name = name;
        Path = path;
        _fileSystem = fileSystem;
    }

    public string Name { get; }

    public string Path { get; }

    private readonly IFileSystem _fileSystem;

    public IEnumerable<IFileSystemComponent> Components()
    {
        List<IFileSystemComponent> components = [];

        foreach (string item in _fileSystem.EnumerateFileSystemEntries(Path))
        {
            if (_fileSystem.FileExists(item))
            {
                components.Add(new FileFileSystemComponent(_fileSystem.GetFileName(item), item));
            }
            else
            {
                string? name = _fileSystem.GetFileName(item);
                if (string.IsNullOrEmpty(name))
                    continue;
                components.Add(new DirectoryFileSystemComponent(name, item, _fileSystem));
            }
        }

        return components;
    }

    public void Accept(IFileSystemComponentVisitor visitor)
    {
        visitor.Visit(this);
    }
}