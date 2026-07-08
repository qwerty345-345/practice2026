using System;
using PluginCore;

namespace Plugins
{
    [PluginLoad("FirstPlugin")]
    public class SecondPlugin : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("второй плагин выполнен");
        }
    }
}