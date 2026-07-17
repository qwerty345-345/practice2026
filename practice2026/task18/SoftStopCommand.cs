using System.Collections.Concurrent;

namespace task18
{
    public class SoftStopCommand : ICommand
    {
        private readonly ServerThread _serverThread;
        private readonly BlockingCollection<ICommand> _queue;
        private readonly IScheduler _scheduler;

        public SoftStopCommand(ServerThread serverThread, BlockingCollection<ICommand> queue, IScheduler scheduler)
        {
            _serverThread = serverThread;
            _queue = queue;
            _scheduler = scheduler;
        }

        public void Execute()
        {
            if (_queue.Count == 0 && !_scheduler.HasCommand())
            {
                _serverThread.Stop();
            }
            else
            {
                _queue.Add(this);
            }
        }
    }
}