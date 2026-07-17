using System;
using System.Collections.Generic;
using System.Threading;

namespace task17;

public class RoundRobinScheduler : IScheduler
{
    private readonly Queue<ICommand> _queue = new Queue<ICommand>();
    private readonly object _lock = new object();
    private bool _isRunning;
    private Thread? _workerThread;

    public void AddCommand(ICommand command)
    {
        lock (_lock)
        {
            _queue.Enqueue(command);
        }
    }

    public void Start()
    {
        _isRunning = true;
        _workerThread = new Thread(ProcessQueue);
        _workerThread.Start();
    }

    private void ProcessQueue()
    {
        while (_isRunning)
        {
            ICommand? command = null;
            lock (_lock)
            {
                if (_queue.Count > 0)
                {
                    command = _queue.Dequeue();
                }
            }

            if (command != null)
            {

                bool isFinished = command.Execute();
                if (!isFinished && _isRunning)
                {
                    lock (_lock)
                    {
                        _queue.Enqueue(command);
                    }
                }
            }
            else
            {
                Thread.Sleep(10); 
            }
            Thread.Sleep(50); 
        }
    }

    public void Stop()
    {
        _isRunning = false;
        if (_workerThread != null && _workerThread.IsAlive)
        {
            _workerThread.Join();
        }
    }
}