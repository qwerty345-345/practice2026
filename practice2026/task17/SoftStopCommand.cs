namespace task17
{
    public class SoftStopCommand : ICommand
    {
        private readonly ServerThread server;

        public SoftStopCommand(ServerThread server)
        {
            this.server = server;
        }

        public void Execute()
        {
            server.RequestSoftStop();
        }
    }
}