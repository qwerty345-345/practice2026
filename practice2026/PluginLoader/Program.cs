using PluginLoader;

PluginManager manager = new PluginManager();

manager.LoadAndExecutePlugins(AppDomain.CurrentDomain.BaseDirectory);