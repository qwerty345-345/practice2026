using System;
using System.Collections.Generic;
using PluginCore;
using PluginLoader;
using Xunit;

namespace PluginSystemTests
{
    public class PluginSystemTests
    {
        private readonly PluginManager manager;

        [PluginLoad]
        public class MockPluginA : ICommand
        {
            public void Execute() { }
        }

        [PluginLoad(typeof(MockPluginA))]
        public class MockPluginB : ICommand
        {
            public void Execute() { }
        }

        [PluginLoad(typeof(MockPluginB))]
        public class MockPluginC : ICommand
        {
            public void Execute() { }
        }

        [PluginLoad(typeof(MockPluginY))]
        public class MockPluginX : ICommand
        {
            public void Execute() { }
        }

        [PluginLoad(typeof(MockPluginX))]
        public class MockPluginY : ICommand
        {
            public void Execute() { }
        }

        [PluginLoad]
        public class MockPluginIsolated : ICommand
        {
            public void Execute() { }
        }

        public PluginSystemTests()
        {
            manager = new PluginManager();
        }

        [Fact]
        public void SortPluginsByDependencies_ShouldCorrectlyOrderDependencies()
        {
            var plugins = new List<Type>
            {
                typeof(MockPluginC),
                typeof(MockPluginB),
                typeof(MockPluginA)
            };

            var result = manager.SortPluginsByDependencies(plugins);

            Assert.Equal(typeof(MockPluginA), result[0]);
            Assert.Equal(typeof(MockPluginB), result[1]);
            Assert.Equal(typeof(MockPluginC), result[2]);
        }

        [Fact]
        public void SortPluginsByDependencies_ShouldThrowException_WhenCycleExists()
        {
            var plugins = new List<Type>
            {
                typeof(MockPluginX),
                typeof(MockPluginY)
            };

            Assert.Throws<InvalidOperationException>(() =>
                manager.SortPluginsByDependencies(plugins));
        }

        [Fact]
        public void SortPluginsByDependencies_ShouldHandleIndependentPlugins()
        {
            var plugins = new List<Type>
            {
                typeof(MockPluginA),
                typeof(MockPluginIsolated)
            };

            var result = manager.SortPluginsByDependencies(plugins);

            Assert.Equal(2, result.Count);
            Assert.Contains(typeof(MockPluginA), result);
            Assert.Contains(typeof(MockPluginIsolated), result);
        }

        [Fact]
        public void LoadAndExecutePlugins_ShouldThrowDirectoryNotFoundException()
        {
            Assert.Throws<System.IO.DirectoryNotFoundException>(() =>
                manager.LoadAndExecutePlugins(@"C:\ТакойПапкиНет"));
        }
    }
}