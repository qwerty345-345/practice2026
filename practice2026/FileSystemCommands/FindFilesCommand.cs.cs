using CommandLib;

namespace FileSystemCommands
{
    public class FindFilesCommand : ICommand
    {
        private string path;
        private string mask;

        public string[] Files { get; private set; }

        public FindFilesCommand(string path, string mask)
        {
            this.path = path;
            this.mask = mask;
        }

        public void Execute()
        {
            Files = Directory.GetFiles(path, mask);
        }
    }
}