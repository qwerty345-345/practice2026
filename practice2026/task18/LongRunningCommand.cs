using System;
using System.Collections.Generic;

namespace task18
{
    public class LongRunningCommand : ICommand
    {
        private readonly IScheduler _scheduler;
        private readonly int _totalSteps;
        private int _currentStep = 0;
        private readonly List<string> _log;
        private readonly string _name;

        public LongRunningCommand(string name, IScheduler scheduler, int totalSteps, List<string> log)
        {
            _name = name;
            _scheduler = scheduler;
            _totalSteps = totalSteps;
            _log = log;
        }

        public void Execute()
        {
            _currentStep++;
            _log.Add($"{_name}_Step{_currentStep}");

            if (_currentStep < _totalSteps)
            {
                _scheduler.Add(this);
            }
        }
    }
}