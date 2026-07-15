using System;
using task18;
using Xunit;

namespace task18tests
{
    public class SchedulerTests
    {
        [Fact]
        public void LongCommand_ShouldExecuteUntilComplete()
        {
            ServerThread server = new ServerThread();

            TestLongCommand command = new TestLongCommand();

            server.AddCommand(command);
            server.AddCommand(new SoftStopCommand(server));

            server.Wait();

            Assert.True(command.IsCompleted);
        }


        [Fact]
        public void RoundRobin_ShouldReturnCommands()
        {
            RoundRobinScheduler scheduler = new RoundRobinScheduler();

            TestCommand first = new TestCommand();
            TestCommand second = new TestCommand();

            scheduler.Add(first);
            scheduler.Add(second);

            Assert.True(scheduler.HasCommand());

            ICommand command1 = scheduler.Select();
            ICommand command2 = scheduler.Select();

            Assert.NotNull(command1);
            Assert.NotNull(command2);
        }


        private class TestCommand : ICommand
        {
            public void Execute()
            {
            }
        }


        private class TestLongCommand : ILongCommand
        {
            private int count = 0;

            public bool IsCompleted => count >= 3;

            public void Execute()
            {
                count++;
            }
        }
    }
}