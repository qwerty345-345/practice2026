using PluginCore;
using System;
using System.Windows.Input;

namespace Plugins
{
    [PluginLoad(typeof(FirstPlugin))]
    public class SecondPlugin : PluginCore.ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Второй плагин выполнен");
        }
    }
}