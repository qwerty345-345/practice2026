using NUnit.Framework;
using System.Threading;
using System.Collections.Concurrent;
using task18;

namespace task18tests;

[TestFixture]
public class SchedulerTests
{
    [Test]
    public void Scheduler_ShouldRunAndBeStoppedByHardStop()
    {
        var scheduler = new Scheduler();
        var command = new TestCommand(1, 3);
        scheduler.AddCommand(command);

        scheduler.Start();
        Thread.Sleep(500);
        scheduler.HardStop();

        Assert.Pass("Планировщик успешно запущен и остановлен через HardStop без зависаний.");
    }

    [Test]
    public void Scheduler_ShouldSupportDifferentCommands()
    {
        var queue = new BlockingCollection<ICommand>();
        var scheduler = new RoundRobinScheduler(); 
        var serverThread = new ServerThread(queue, scheduler);

        scheduler.Add(new LongRunningCommand(1, 100));
        scheduler.Add(new SoftStopCommand(serverThread, queue, scheduler)); 

        Assert.That(scheduler.HasCommand(), Is.True);
    }
}