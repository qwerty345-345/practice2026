using System;

namespace task17;

public class TestCommand : ICommand
{
    public int Id { get; }
    private int _remainingCalls;
    private readonly Action<int> _logAction;

    public TestCommand(int id, int calls, Action<int> logAction)
    {
        Id = id;
        _remainingCalls = calls;
        _logAction = logAction;
    }

    public bool Execute()
    {
        _logAction(Id);
        _remainingCalls--;
        return _remainingCalls == 0; 
    }
}