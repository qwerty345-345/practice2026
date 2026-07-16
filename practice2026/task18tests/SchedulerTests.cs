using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using NUnit.Framework;
using task18;
using ScottPlot;

namespace task18tests
{
    [TestFixture]
    public class SchedulerTests
    {
        [Test]
        public void Scheduler_ShouldExecuteCommandsInRoundRobinOrder()
        {
            var queue = new BlockingCollection<ICommand>();
            var scheduler = new RoundRobinScheduler();
            var serverThread = new ServerThread(queue, scheduler);
            var log = new List<string>();

            scheduler.Add(new LongRunningCommand("A", scheduler, 3, log));
            scheduler.Add(new LongRunningCommand("B", scheduler, 3, log));

            serverThread.Start();
            while (scheduler.HasCommand()) Thread.Sleep(10);
            serverThread.Stop();
            serverThread.Join();

            Assert.That(log, Is.EqualTo(new List<string> { "A_Step1", "B_Step1", "A_Step2", "B_Step2", "A_Step3", "B_Step3" }));
        }

        [Test]
        public void Benchmark_And_GenerateChart()
        {
            List<double> taskCounts = new List<double>();
            List<double> times = new List<double>();

            for (int count = 1; count <= 5; count++)
            {
                var queue = new BlockingCollection<ICommand>();
                var scheduler = new RoundRobinScheduler();
                var server = new ServerThread(queue, scheduler);
                
                for (int i = 0; i < count; i++)
                    scheduler.Add(new LongRunningCommand("T", scheduler, 10, new List<string>()));

                var sw = System.Diagnostics.Stopwatch.StartNew();
                server.Start();
                while (scheduler.HasCommand()) Thread.Sleep(10);
                server.Stop();
                server.Join();
                sw.Stop();

                taskCounts.Add(count);
                times.Add(sw.Elapsed.TotalMilliseconds);
            }

            // Генерируем график
            var plt = new ScottPlot.Plot();
            plt.Add.Scatter(taskCounts.ToArray(), times.ToArray());
            plt.Title("Производительность планировщика");
            plt.XLabel("Количество задач");
            plt.YLabel("Время (мс)");
            
            // Сохраняем в папку bin/Debug/net10.0/ (это всегда разрешено)
            string fileName = "scheduler_performance.png";
            plt.SavePng(fileName, 800, 600);
            
            Console.WriteLine($"График создан по пути: {Path.GetFullPath(fileName)}");
            Assert.Pass();
        }
    }
}