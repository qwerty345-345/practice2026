using System.Collections.Generic;

namespace task18
{
    public class RoundRobinScheduler : IScheduler
    {
        private readonly Queue<ICommand> commands = new Queue<ICommand>();

        public bool HasCommand()
        {
            return commands.Count > 0;
        }

        public ICommand Select()
        {
            return commands.Dequeue();
        }

        public void Add(ICommand cmd)
        {
            commands.Enqueue(cmd);
        }
    }
}