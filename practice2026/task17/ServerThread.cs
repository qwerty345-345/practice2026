using System.Threading;

namespace task17;

public class ServerThread
{
    private readonly IScheduler _scheduler;
    private bool _isRunning;
    private Thread? _thread;

    public ServerThread(IScheduler scheduler)
    {
        _scheduler = scheduler;
    }

    public void Start()
    {
        _isRunning = true;
        _thread = new Thread(Run);
        _thread.Start();
    }

    private void Run()
    {
        _scheduler.Start();
        while (_isRunning)
        {
            Thread.Sleep(100);
        }
        _scheduler.Stop();
    }

    public void Stop()
    {
        _isRunning = false;
        if (_thread != null && _thread.IsAlive)
        {
            _thread.Join();
        }
    }
}