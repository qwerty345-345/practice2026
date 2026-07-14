namespace task17
{
    public class HardStopCommand : ICommand
    {
        private readonly ServerThread server;

        public HardStopCommand(ServerThread server)
        {
            this.server = server;
        }

        public void Execute()
        {
            server.RequestHardStop();
        }
    }
}