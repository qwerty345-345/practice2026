using System;

namespace task18;

public class TestCommand : ICommand
{
    private readonly int _id;
    private int _counter = 0;
    private readonly int _maxSteps;

    public TestCommand(int id, int maxSteps)
    {
        _id = id;
        _maxSteps = maxSteps;
    }

    public bool Execute()
    {
        Console.WriteLine($"Поток {_id} вызов {++_counter}");
        return _counter >= _maxSteps;
    }
}