using FileSystemManager.Core.Commands;
using FileSystemManager.Core.Commands.ShowCommandModes;
using FileSystemManager.Core.Commands.TreeListOutputConfig;
using FileSystemManager.Presentation.ParsingSystem;
using FileSystemManager.Presentation.ParsingSystem.CommandArgumentsParserConfigurations;
using Xunit;

namespace FileSystemManager.Tests;

public class UnitTests
{
    [Fact]
    public void CommandParserTest_ConnectCommandWithAllArguments_ShouldParse()
    {
        Parser parser = new(new TreeListOutputValues(), new ParsingChainFactory());

        string inputString = "connect D:/test/tmp/ -m local";
        string expectedPath = "D:/test/tmp/";

        ParserResult result = parser.Parse(inputString);

        Assert.True(result is ParserResult.Success);

        if (result is ParserResult.Success success)
        {
            Assert.NotNull(success.Command);
            Assert.IsType<ConnectCommand>(success.Command);

            var connectCommand = success.Command as ConnectCommand;

            Assert.Equal(expectedPath, connectCommand?.Path);
            Assert.NotNull(connectCommand?.FileSystem);
        }
    }

    [Fact]
    public void CommandParserTest_DisconnectCommandWithAllArguments_ShouldParse()
    {
        Parser parser = new(new TreeListOutputValues(), new ParsingChainFactory());

        string inputString = "disconnect";

        ParserResult result = parser.Parse(inputString);

        Assert.True(result is ParserResult.Success);

        if (result is ParserResult.Success success)
        {
            Assert.NotNull(success.Command);
            Assert.IsType<DisconnectCommand>(success.Command);
        }
    }

    [Fact]
    public void CommandParserTest_FileCopyCommandWithAllArguments_ShouldParse()
    {
        Parser parser = new(new TreeListOutputValues(), new ParsingChainFactory());

        string inputString = "file copy D:/test/tmp/1.txt D:/";

        ParserResult result = parser.Parse(inputString);

        Assert.True(result is ParserResult.Success);

        if (result is ParserResult.Success success)
        {
            Assert.NotNull(success.Command);
            Assert.IsType<FileCopyCommand>(success.Command);

            var currentCommand = success.Command as FileCopyCommand;

            if (currentCommand is null)
                Assert.Fail();

            Assert.Equal("D:/test/tmp/1.txt", currentCommand.Source);
            Assert.Equal("D:/", currentCommand.Destination);
        }
    }

    [Fact]
    public void CommandParserTest_FileDeleteCommandWithAllArguments_ShouldParse()
    {
        Parser parser = new(new TreeListOutputValues(), new ParsingChainFactory());

        string inputString = "file delete D:/test/tmp/1.txt";

        ParserResult result = parser.Parse(inputString);

        Assert.True(result is ParserResult.Success);

        if (result is ParserResult.Success success)
        {
            Assert.NotNull(success.Command);
            Assert.IsType<FileDeleteCommand>(success.Command);

            var currentCommand = success.Command as FileDeleteCommand;

            if (currentCommand is null)
                Assert.Fail();

            Assert.Equal("D:/test/tmp/1.txt", currentCommand.Path);
        }
    }

    [Fact]
    public void CommandParserTest_FileMoveCommandWithAllArguments_ShouldParse()
    {
        Parser parser = new(new TreeListOutputValues(), new ParsingChainFactory());

        string inputString = "file move D:/test/tmp/1.txt D:";

        ParserResult result = parser.Parse(inputString);

        Assert.True(result is ParserResult.Success);

        if (result is ParserResult.Success success)
        {
            Assert.NotNull(success.Command);
            Assert.IsType<FileMoveCommand>(success.Command);

            var currentCommand = success.Command as FileMoveCommand;

            if (currentCommand is null)
                Assert.Fail();

            Assert.Equal("D:/test/tmp/1.txt", currentCommand.Source);
            Assert.Equal("D:", currentCommand.Destination);
        }
    }

    [Fact]
    public void CommandParserTest_FileRenameCommandWithAllArguments_ShouldParse()
    {
        Parser parser = new(new TreeListOutputValues(), new ParsingChainFactory());

        string inputString = "file rename D:/test/tmp/1.txt TheBestName.txt";

        ParserResult result = parser.Parse(inputString);

        Assert.True(result is ParserResult.Success);

        if (result is ParserResult.Success success)
        {
            Assert.NotNull(success.Command);
            Assert.IsType<FileRenameCommand>(success.Command);

            var currentCommand = success.Command as FileRenameCommand;

            if (currentCommand is null)
                Assert.Fail();

            Assert.Equal("TheBestName.txt", currentCommand.Name);
            Assert.Equal("D:/test/tmp/1.txt", currentCommand.Path);
        }
    }

    [Fact]
    public void CommandParserTest_FileShowCommandWithAllArguments_ShouldParse()
    {
        Parser parser = new(new TreeListOutputValues(), new ParsingChainFactory());

        string inputString = "file show D:/test/tmp/1.txt -m console";

        ParserResult result = parser.Parse(inputString);

        Assert.True(result is ParserResult.Success);

        if (result is ParserResult.Success success)
        {
            Assert.NotNull(success.Command);
            Assert.IsType<FileShowCommand>(success.Command);

            var currentCommand = success.Command as FileShowCommand;

            if (currentCommand is null)
                Assert.Fail();

            Assert.Equal("D:/test/tmp/1.txt", currentCommand.Path);
            Assert.IsType<ShowCommandConsoleMode>(currentCommand.Mode);
        }
    }

    [Fact]
    public void CommandParserTest_TreeGotoCommandWithAllArguments_ShouldParse()
    {
        Parser parser = new(new TreeListOutputValues(), new ParsingChainFactory());

        string inputString = "tree goto D:/test/tmp/1.txt";

        ParserResult result = parser.Parse(inputString);

        Assert.True(result is ParserResult.Success);

        if (result is ParserResult.Success success)
        {
            Assert.NotNull(success.Command);
            Assert.IsType<TreeGotoCommand>(success.Command);

            var currentCommand = success.Command as TreeGotoCommand;

            if (currentCommand is null)
                Assert.Fail();

            Assert.Equal("D:/test/tmp/1.txt", currentCommand.Path);
        }
    }

    [Fact]
    public void CommandParserTest_TreeListCommandWithAllArguments_ShouldParse()
    {
        Parser parser = new(new TreeListOutputValues(), new ParsingChainFactory());

        string inputString = "tree list -d 10";

        ParserResult result = parser.Parse(inputString);

        Assert.True(result is ParserResult.Success);

        if (result is ParserResult.Success success)
        {
            Assert.NotNull(success.Command);
            Assert.IsType<TreeListCommand>(success.Command);

            var currentCommand = success.Command as TreeListCommand;

            if (currentCommand is null)
                Assert.Fail();

            Assert.Equal(10, currentCommand.Depth);
            Assert.IsType<ShowCommandConsoleMode>(currentCommand.Mode);
        }
    }

    [Fact]
    public void CommandParserTest_ConnectCommandWithWrongArguments_ShouldNotParse()
    {
        Parser parser = new(new TreeListOutputValues(), new ParsingChainFactory());

        string inputString = "connect D:/test/tmp/ -m localPath";

        ParserResult result = parser.Parse(inputString);

        Assert.False(result is ParserResult.Success);
    }

    [Fact]
    public void CommandParserTest_DisconnectCommandWithWrongArguments_ShouldNotParse()
    {
        Parser parser = new(new TreeListOutputValues(), new ParsingChainFactory());

        string inputString = "disconnect -m";

        ParserResult result = parser.Parse(inputString);

        Assert.False(result is ParserResult.Success);
    }

    [Fact]
    public void CommandParserTest_FileCopyCommandWithWrongArguments_ShouldNotParse()
    {
        Parser parser = new(new TreeListOutputValues(), new ParsingChainFactory());

        string inputString = "file copy D:/test/tmp/1.txt D:/ AnotherPositional";

        ParserResult result = parser.Parse(inputString);

        Assert.False(result is ParserResult.Success);
    }

    [Fact]
    public void CommandParserTest_FileDeleteCommandWithWrongArguments_ShouldNotParse()
    {
        Parser parser = new(new TreeListOutputValues(), new ParsingChainFactory());

        string inputString = "file delete D:/test/tmp/1.txt -m force_delete";

        ParserResult result = parser.Parse(inputString);

        Assert.False(result is ParserResult.Success);
    }

    [Fact]
    public void CommandParserTest_FileMoveCommandWithWrongArguments_ShouldNotParse()
    {
        Parser parser = new(new TreeListOutputValues(), new ParsingChainFactory());

        string inputString = "file move D:/test/tmp/1.txt D: -forced";

        ParserResult result = parser.Parse(inputString);

        Assert.False(result is ParserResult.Success);
    }

    [Fact]
    public void CommandParserTest_FileRenameCommandWithWrongArguments_ShouldNotParse()
    {
        Parser parser = new(new TreeListOutputValues(), new ParsingChainFactory());

        string inputString = "file rename D:/test/tmp/1.txt";

        ParserResult result = parser.Parse(inputString);

        Assert.False(result is ParserResult.Success);
    }

    [Fact]
    public void CommandParserTest_FileShowCommandWithWrongArguments_ShouldNotParse()
    {
        Parser parser = new(new TreeListOutputValues(), new ParsingChainFactory());

        string inputString = "file show D:/test/tmp/1.txt -m file";

        ParserResult result = parser.Parse(inputString);

        Assert.False(result is ParserResult.Success);
    }

    [Fact]
    public void CommandParserTest_TreeGotoCommandWithWrongArguments_ShouldNotParse()
    {
        Parser parser = new(new TreeListOutputValues(), new ParsingChainFactory());
        string inputString = "tree goto";

        ParserResult result = parser.Parse(inputString);

        Assert.False(result is ParserResult.Success);
    }

    [Fact]
    public void CommandParserTest_TreeListCommandWithWrongArguments_ShouldNotParse()
    {
        Parser parser = new(new TreeListOutputValues(), new ParsingChainFactory());

        string inputString = "tree list -d 10 -l 20";

        ParserResult result = parser.Parse(inputString);

        Assert.False(result is ParserResult.Success);
    }
}