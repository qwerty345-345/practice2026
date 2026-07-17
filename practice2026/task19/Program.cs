using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using task17; // Используем планировщик и команды из task17

namespace task19;

public class Program
{
    public static void Main(string[] args)
    {
        var scheduler = new RoundRobinScheduler();
        var serverThread = new ServerThread(scheduler);
        var history = new List<int>();
        var lockObj = new object();

        // Записываем порядок вызовов
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

        Console.WriteLine("Запуск планировщика в task19...");
        serverThread.Start();

        // Ожидаем выполнения всех 15 шагов
        int expectedTotalCalls = numCommands * callsPerCommand;
        while (true)
        {
            lock (lockObj)
            {
                if (history.Count >= expectedTotalCalls)
                    break;
            }
            Thread.Sleep(50);
        }

        serverThread.Stop();
        Console.WriteLine("Выполнение завершено. Генерация отчета...");

        // Определяем путь к папке task19
        string targetDir = AppDomain.CurrentDomain.BaseDirectory;
        DirectoryInfo? dir = new DirectoryInfo(targetDir);
        while (dir != null && !File.Exists(Path.Combine(dir.FullName, "practice2026.sln")))
        {
            dir = dir.Parent;
        }

        if (dir != null)
        {
            targetDir = Path.Combine(dir.FullName, "task19");
        }

        // Формируем текст отчета
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

        Console.WriteLine($"Отчет успешно сохранен в: {filePath}");
    }
}
