using System;
using System.Collections.Concurrent;
using System.Threading;

namespace task18
{
    public class ServerThread
    {
        private readonly BlockingCollection<ICommand> _queue;
        private readonly IScheduler _scheduler;
        private readonly Thread _thread;
        private bool _stop = false;

        public ServerThread(BlockingCollection<ICommand> queue, IScheduler scheduler)
        {
            _queue = queue;
            _scheduler = scheduler;
            _thread = new Thread(Run);
        }

        public void Start() => _thread.Start();

        private void Run()
        {
            while (!_stop)
            {
                ICommand cmd = null;
                if (_scheduler.HasCommand())
                {
                    if (_queue.TryTake(out var queueCmd)) cmd = queueCmd;
                    else cmd = _scheduler.Select();
                }
                else
                {
                    // Попытка взять команду, чтобы не висеть вечно
                    _queue.TryTake(out var queueCmd, 100);
                    cmd = queueCmd;
                }

                if (cmd != null)
                {
                    try { cmd.Execute(); }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }
            }
        }

        public void Stop() => _stop = true;
        public void Join() => _thread.Join();
    }
}