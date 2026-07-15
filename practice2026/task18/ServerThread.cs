using System;
using System.Collections.Concurrent;
using System.Threading;

namespace task18
{
    public class ServerThread
    {
        private readonly BlockingCollection<ICommand> queue;
        private readonly IScheduler scheduler;
        private readonly Thread thread;

        private volatile bool hardStopRequested;
        private volatile bool softStopRequested;

        public int ThreadId { get; private set; }

        public ServerThread()
        {
            queue = new BlockingCollection<ICommand>();
            scheduler = new RoundRobinScheduler();

            thread = new Thread(Work);
            thread.IsBackground = true;

            thread.Start();

            while (ThreadId == 0)
            {
                Thread.Yield();
            }
        }

        private void Work()
        {
            ThreadId = Thread.CurrentThread.ManagedThreadId;

            while (!hardStopRequested)
            {
                ICommand? command = null;

                if (queue.TryTake(out var newCommand))
                {
                    command = newCommand;
                }
                else if (scheduler.HasCommand())
                {
                    command = scheduler.Select();
                }

                if (command == null)
                {
                    Thread.Sleep(1);
                    continue;
                }

                try
                {
                    command.Execute();

                    if (command is ILongCommand longCommand)
                    {
                        if (!longCommand.IsCompleted)
                        {
                            scheduler.Add(longCommand);
                        }
                    }
                }
                catch
                {
                }

                if (softStopRequested &&
                    !scheduler.HasCommand() &&
                    queue.Count == 0)
                {
                    break;
                }
            }
        }

        public void AddCommand(ICommand command)
        {
            queue.Add(command);
        }

        public void RequestHardStop()
        {
            if (Thread.CurrentThread.ManagedThreadId != ThreadId)
                throw new InvalidOperationException();

            hardStopRequested = true;
        }

        public void RequestSoftStop()
        {
            if (Thread.CurrentThread.ManagedThreadId != ThreadId)
                throw new InvalidOperationException();

            softStopRequested = true;
        }

        public void Wait()
        {
            thread.Join();
        }

        public bool IsAlive => thread.IsAlive;
    }
}