using System.Reflection;
using CommandLib;

Assembly assembly = Assembly.LoadFrom("FileSystemCommands.dll");

var command = Activator.CreateInstance(
    assembly.GetType("FileSystemCommands.DirectorySizeCommand"),
    "C:\\Temp");

((ICommand)command).Execute();