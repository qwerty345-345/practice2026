using System;
using System.Threading;
using task17;
using Xunit;

namespace task17tests
{
    public class HardSoftStopTests
    {
        private class CounterCommand : ICommand
        {
            private readonly Action action;

            public CounterCommand(Action action)
            {
                this.action = action;
            }

            public void Execute()
            {
                action();
            }
        }

        [Fact]
        public void SoftStop_ShouldExecuteAllCommands()
        {
            ServerThread server = new ServerThread();

            int counter = 0;

            server.AddCommand(new CounterCommand(() => counter++));
            server.AddCommand(new CounterCommand(() => counter++));
            server.AddCommand(new SoftStopCommand(server));

            server.Wait();

            Assert.Equal(2, counter);
            Assert.False(server.IsAlive);
        }

        [Fact]
        public void HardStop_ShouldStopThread()
        {
            ServerThread server = new ServerThread();

            int counter = 0;

            server.AddCommand(new CounterCommand(() => counter++));
            server.AddCommand(new HardStopCommand(server));
            server.AddCommand(new CounterCommand(() => counter++));

            server.Wait();

            Assert.Equal(1, counter);
            Assert.False(server.IsAlive);
        }

        [Fact]
        public void HardStop_OutsideServerThread_ShouldThrowException()
        {
            ServerThread server = new ServerThread();

            Assert.Throws<InvalidOperationException>(() =>
            {
                server.RequestHardStop();
            });
        }

        [Fact]
        public void SoftStop_OutsideServerThread_ShouldThrowException()
        {
            ServerThread server = new ServerThread();

            Assert.Throws<InvalidOperationException>(() =>
            {
                server.RequestSoftStop();
            });
        }
    }
}