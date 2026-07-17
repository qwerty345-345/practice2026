using System;

namespace task18
{
    public class LongRunningCommand : ICommand
    {
        private readonly int _id;
        private readonly int _delayMs;

        public LongRunningCommand(int id, int delayMs)
        {
            _id = id;
            _delayMs = delayMs;
        }

        public bool Execute()
        {
            System.Threading.Thread.Sleep(_delayMs);
            return true;
        }
    }
}