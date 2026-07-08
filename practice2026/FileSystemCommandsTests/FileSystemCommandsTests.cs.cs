using FileSystemCommands;
using Xunit;

namespace FileSystemCommandsTests
{
    public class FileSystemCommandsTests
    {
        [Fact]
        public void DirectorySizeCommand_ShouldCalculateSize()
        {
            var path = Path.Combine(Path.GetTempPath(), "testdir");

            Directory.CreateDirectory(path);

            File.WriteAllText(
                Path.Combine(path, "a.txt"),
                "hello");

            var command = new DirectorySizeCommand(path);

            command.Execute();

            Assert.True(command.Size > 0);

            Directory.Delete(path, true);
        }


        [Fact]
        public void FindFilesCommand_ShouldFindFiles()
        {
            var path = Path.Combine(Path.GetTempPath(), "testdir2");

            Directory.CreateDirectory(path);

            File.WriteAllText(
                Path.Combine(path, "a.txt"),
                "text");

            File.WriteAllText(
                Path.Combine(path, "b.log"),
                "log");


            var command = new FindFilesCommand(path, "*.txt");

            command.Execute();


            Assert.Single(command.Files);

            Directory.Delete(path, true);
        }
    }
}