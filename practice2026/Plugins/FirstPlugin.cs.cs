using PluginCore;
using System;
using System.Windows.Input;

namespace Plugins
{
    [PluginLoad]
    public class FirstPlugin : PluginCore.ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Первый плагин выполнен");
        }
    }
}