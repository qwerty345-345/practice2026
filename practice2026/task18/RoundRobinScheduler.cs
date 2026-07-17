using System;
using System.Collections.Concurrent;

namespace task18
{
    public class RoundRobinScheduler : IScheduler
    {
        private readonly ConcurrentQueue<ICommand> _tasks = new ConcurrentQueue<ICommand>();

        public bool HasCommand()
        {
            return !_tasks.IsEmpty;
        }

        public ICommand Select()
        {
            if (_tasks.TryDequeue(out var cmd))
            {
                return cmd;
            }
            throw new InvalidOperationException("Планировщик пуст!");
        }

        public void Add(ICommand cmd)
        {
            _tasks.Enqueue(cmd);
        }
    }
}