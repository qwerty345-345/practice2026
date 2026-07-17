using NUnit.Framework;
using System.IO;
using System;
using task19;

namespace task19tests;

[TestFixture]
public class ProgramTests
{
    [Test]
    public void TestMainExecutionAndReportGeneration()
    {

        Program.Main(Array.Empty<string>());

        DirectoryInfo? dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
        while (dir != null && !File.Exists(Path.Combine(dir.FullName, "practice2026.sln")))
        {
            dir = dir.Parent;
        }

        Assert.That(dir, Is.Not.Null, "Не удалось найти корень решения .sln");

        string resultFilePath = Path.Combine(dir!.FullName, "task19", "result.txt");

        Assert.That(File.Exists(resultFilePath), Is.True, "Файл result.txt не был создан в папке task19.");
        string content = File.ReadAllText(resultFilePath);
        Assert.That(content, Does.Contain("Отчет о выполнении"));
        Assert.That(content, Does.Contain("Всего вызовов: 15"));
    }
}