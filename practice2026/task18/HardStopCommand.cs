using System;

namespace task18
{
    public class HardStopCommand : ICommand
    {
        private readonly ServerThread _serverThread;

        public HardStopCommand(ServerThread serverThread)
        {
            _serverThread = serverThread;
        }

        public bool Execute()
        {
            _serverThread.Stop();
            return true;
        }
    }
}