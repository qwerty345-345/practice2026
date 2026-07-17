namespace task17;

public interface ICommand
{
    bool Execute(); 
}

public interface IScheduler
{
    void AddCommand(ICommand command);
    void Start();
    void Stop();
}