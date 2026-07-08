using System;
using PluginCore;

namespace Plugins
{
    [PluginLoad]
    public class FirstPlugin : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("первый плагин выполнен");
        }
    }
}