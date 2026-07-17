using NUnit.Framework;
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
    [TestFixture]
    public class SchedulerTests
    {
       [Test]
        public void Scheduler_ShouldSupportDifferentCommands()
        {
            var queue = new BlockingCollection<ICommand>();
            var scheduler = new RoundRobinScheduler();
            var serverThread = new ServerThread(queue, scheduler);

            scheduler.Add(new LongRunningCommand("C1", scheduler, 1, new List<string>()));
            scheduler.Add(new SoftStopCommand(serverThread, queue, scheduler));
            
            Assert.That(scheduler.HasCommand(), Is.True);
        }

        [Test]
        public void Scheduler_ShouldExecuteCommandsInRoundRobinOrder()
        {
            var queue = new BlockingCollection<ICommand>();
            var scheduler = new RoundRobinScheduler();
            var serverThread = new ServerThread(queue, scheduler);
            var log = new List<string>();

            scheduler.Add(new LongRunningCommand("A", scheduler, 2, log));
            scheduler.Add(new LongRunningCommand("B", scheduler, 2, log));

            serverThread.Start();
            while (scheduler.HasCommand()) Thread.Sleep(10);
            serverThread.Stop();
            serverThread.Join();

            Assert.That(log.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Benchmark_And_GenerateChart()
        {
            List<double> taskCounts = new List<double> { 1, 2, 3 };
            List<double> times = new List<double>();

            foreach (var count in taskCounts)
            {
                var queue = new BlockingCollection<ICommand>();
                var scheduler = new RoundRobinScheduler();
                var server = new ServerThread(queue, scheduler);

                for (int i = 0; i < count; i++)
                    // Передаем scheduler (аргумент 2)
                    scheduler.Add(new LongRunningCommand("T", scheduler, 5, new List<string>()));

                var sw = System.Diagnostics.Stopwatch.StartNew();
                server.Start();
                while (scheduler.HasCommand()) Thread.Sleep(10);
                server.Stop();
                server.Join();
                sw.Stop();
                times.Add(sw.Elapsed.TotalMilliseconds);
            }

            var plt = new ScottPlot.Plot();
            plt.Add.Scatter(taskCounts.ToArray(), times.ToArray());
            plt.Title("Performance");
            plt.SavePng("scheduler_performance.png", 800, 600);
            
            Assert.Pass();
        }
    }
}
