using System;
using System.Collections.Concurrent;
using System.Threading;

namespace task17
{
    public class ServerThread
    {
        private readonly BlockingCollection<ICommand> queue;
        private readonly Thread thread;

        private volatile bool hardStopRequested;
        private volatile bool softStopRequested;

        public int ThreadId { get; private set; }

        public ServerThread()
        {
            queue = new BlockingCollection<ICommand>();

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

            while (true)
            {
                if (!queue.TryTake(out ICommand command, Timeout.Infinite))
                    continue;

                try
                {
                    command.Execute();
                }
                catch
                {
                }

                if (hardStopRequested)
                {
                    break;
                }

                if (softStopRequested && queue.Count == 0)
                {
                    break;
                }
            }
        }
        public void AddCommand(ICommand command)
        {
            if (queue.IsAddingCompleted)
                throw new InvalidOperationException("Сервер уже остановлен.");

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