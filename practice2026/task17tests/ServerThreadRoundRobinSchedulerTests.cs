using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using task17;

namespace task17tests;

[TestFixture]
public class ServerThreadRoundRobinSchedulerTests
{
    [Test]
    public void TestRoundRobinExecutionAndGenerateReport()
    {
        // Arrange
        var scheduler = new RoundRobinScheduler();
        var serverThread = new ServerThread(scheduler);
        var history = new List<int>();
        var lockObj = new object();

        Action<int> logAction = (id) =>
        {
            lock (lockObj)
            {
                history.Add(id);
            }
        };

        int numCommands = 5;
        int callsPerCommand = 3;

        for (int i = 1; i <= numCommands; i++)
        {
            scheduler.AddCommand(new TestCommand(i, callsPerCommand, logAction));
        }

        serverThread.Start();

        int expectedTotalCalls = numCommands * callsPerCommand;
        int timeoutMs = 5000;
        int elapsedMs = 0;

        while (true)
        {
            lock (lockObj)
            {
                if (history.Count >= expectedTotalCalls)
                    break;
            }

            Thread.Sleep(50);
            elapsedMs += 50;
            if (elapsedMs > timeoutMs)
            {
                Assert.Fail("Превышено время ожидания выполнения команд планировщика.");
            }
        }

        serverThread.Stop();

        Assert.That(history.Count, Is.EqualTo(expectedTotalCalls));

        DirectoryInfo? dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
        while (dir != null && !File.Exists(Path.Combine(dir.FullName, "practice2026.sln")))
        {
            dir = dir.Parent;
        }

        string targetDir = dir != null 
            ? Path.Combine(dir.FullName, "task17") 
            : AppDomain.CurrentDomain.BaseDirectory;

        var sb = new StringBuilder();
        sb.AppendLine("Отчет о выполнении\n");
        sb.AppendLine("Порядок выполнения команд:");
        for (int i = 0; i < history.Count; i++)
        {
            sb.AppendLine($"    {i + 1} -> команда {history[i]}");
        }
        sb.AppendLine();
        sb.AppendLine($"Всего вызовов: {history.Count}");
        sb.AppendLine($"Команд: {numCommands}");
        sb.AppendLine($"Вызовов на команду: {callsPerCommand}");
        sb.AppendLine($"Ожидалось: {expectedTotalCalls}");

        string filePath = Path.Combine(targetDir, "result.txt");
        File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);

        Console.WriteLine($"Отчет успешно сгенерирован и сохранен в: {filePath}");
    }
}