using FileSystemManager.Core.Commands.TreeListOutputConfig;
using FileSystemManager.Core.FileSystem.Interfaces;
using System.Text;

namespace FileSystemManager.Core.FileSystem.LocalFileSystemRealization.FileSystemTree;

public sealed class FormattingVisitor : IFileSystemComponentVisitor
{
    private readonly StringBuilder _builder = new();

    private readonly int _maxDepth;

    private readonly TreeListOutputValues _icons;

    private int _padding;

    public FormattingVisitor(int maxDepth, TreeListOutputValues icons)
    {
        _maxDepth = maxDepth;
        _icons = icons;
    }

    public string Value => _builder.ToString();

    public void Visit(FileFileSystemComponent component)
    {
        if (_padding > _maxDepth)
            return;

        for (int i = 0; i < _padding; ++i)
        {
            _builder.Append(_icons.Splitter);
        }

        _builder.Append(_icons.File);
        _builder.Append(component.Name);
        _builder.Append("\n");
    }

    public void Visit(DirectoryFileSystemComponent component)
    {
        for (int i = 0; i < _padding; ++i)
        {
            _builder.Append(_icons.Splitter);
        }

        _builder.Append(_icons.Folder);
        _builder.Append(component.Name);
        _builder.Append("\n");

        _padding += 1;

        foreach (IFileSystemComponent child in component.Components())
        {
            if (_padding > _maxDepth)
            {
                _padding -= 1;
                return;
            }

            child.Accept(this);
        }

        _padding -= 1;
    }
}