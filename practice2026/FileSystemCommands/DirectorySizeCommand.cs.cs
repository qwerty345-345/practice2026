using System.IO;
using System.Linq;
using CommandLib;

namespace FileSystemCommands
{
    public class DirectorySizeCommand : ICommand
    {
        private string path;

        public long Size { get; private set; }

        public DirectorySizeCommand(string path)
        {
            this.path = path;
        }

        public void Execute()
        {
            Size = Directory.GetFiles(path, "*", SearchOption.AllDirectories)
                .Sum(x => new FileInfo(x).Length);
        }
    }
}