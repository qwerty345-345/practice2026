using System;
using System.Collections.Generic;
using System.Threading;

namespace task18;

public class Scheduler
{
    private readonly Queue<ICommand> _queue = new Queue<ICommand>();
    private bool _isRunning = false;
    private Thread? _workerThread;

    public void AddCommand(ICommand command) => _queue.Enqueue(command);

    public void Start()
    {
        _isRunning = true;
        _workerThread = new Thread(ProcessQueue);
        _workerThread.Start();
    }

    private void ProcessQueue()
    {
        while (_isRunning && _queue.Count > 0)
        {
            var command = _queue.Dequeue();
            bool isFinished = command.Execute();

            if (!isFinished && _isRunning)
            {
                _queue.Enqueue(command); 
            }
            Thread.Sleep(100); 
        }
    }

    public void HardStop()
    {
        _isRunning = false;
        Console.WriteLine("--- Вызван HardStop. Планировщик остановлен ---");
        if (_workerThread != null && _workerThread.IsAlive)
        {
            _workerThread.Join();
        }
    }
}