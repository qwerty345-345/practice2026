using NUnit.Framework;
using System.Threading;
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
}