using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using PluginCore;

namespace PluginLoader
{
    public class PluginManager
    {
        public List<Type> SortPluginsByDependencies(List<Type> plugins)
        {
            List<Type> result = new();
            HashSet<Type> visited = new();
            HashSet<Type> visiting = new();

            void Visit(Type plugin)
            {
                if (visited.Contains(plugin))
                    return;

                if (visiting.Contains(plugin))
                    throw new InvalidOperationException("Обнаружена циклическая зависимость");

                visiting.Add(plugin);

                var attribute = plugin.GetCustomAttribute<PluginLoadAttribute>();

                if (attribute != null)
                {
                    foreach (var dependency in attribute.Dependencies)
                    {
                        if (plugins.Contains(dependency))
                        {
                            Visit(dependency);
                        }
                    }
                }

                visiting.Remove(plugin);
                visited.Add(plugin);
                result.Add(plugin);
            }

            foreach (var plugin in plugins)
            {
                Visit(plugin);
            }

            return result;
        }

        public void LoadAndExecutePlugins(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException();

            var dllFiles = Directory.GetFiles(folderPath, "*.dll");

            foreach (var file in dllFiles)
            {
                Assembly assembly = Assembly.LoadFrom(file);

                var plugins = assembly.GetTypes()
                    .Where(t =>
                        typeof(ICommand).IsAssignableFrom(t) &&
                        !t.IsInterface &&
                        t.GetCustomAttribute<PluginLoadAttribute>() != null)
                    .ToList();

                plugins = SortPluginsByDependencies(plugins);

                foreach (var plugin in plugins)
                {
                    ICommand command = (ICommand)Activator.CreateInstance(plugin)!;
                    command.Execute();
                }
            }
        }
    }
}