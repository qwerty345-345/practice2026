using System.Reflection;
using PluginCore;

string path = "Plugins.dll";

Assembly assembly = Assembly.LoadFrom(path);

var plugins = assembly.GetTypes()
    .Where(t => typeof(ICommand).IsAssignableFrom(t)
    && t.GetCustomAttribute<PluginLoadAttribute>() != null)
    .ToList();


var loaded = new HashSet<string>();


void LoadPlugin(Type plugin)
{
    if (loaded.Contains(plugin.Name))
    {
        return;
    }

    var attribute = plugin.GetCustomAttribute<PluginLoadAttribute>();

    foreach (var dependency in attribute.Dependencies)
    {
        var dep = plugins.FirstOrDefault(x => x.Name == dependency);

        if (dep != null)
        {
            LoadPlugin(dep);
        }
    }

    var instance = Activator.CreateInstance(plugin) as ICommand;

    instance.Execute();

    loaded.Add(plugin.Name);
}


foreach (var plugin in plugins)
{
    LoadPlugin(plugin);
}