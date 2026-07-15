using System;
using System.IO;
using System.Linq;
using System.Reflection;
using task07;

namespace task09
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Укажите путь к библиотеке.");
                return;
            }

            string assemblyPath = args[0];

            if (!File.Exists(assemblyPath))
            {
                Console.WriteLine("Файл не найден.");
                return;
            }

            Assembly assembly = Assembly.LoadFrom(assemblyPath);

            foreach (Type type in assembly.GetTypes())
            {
                if (!type.IsClass)
                    continue;

                Console.WriteLine($"Класс: {type.Name}");

                ReflectionHelper.PrintTypeInfo(type);

                Console.WriteLine("Конструкторы:");

                ConstructorInfo[] constructors = type.GetConstructors(
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance |
                    BindingFlags.Static);

                foreach (ConstructorInfo constructor in constructors)
                {
                    string parameters = string.Join(", ",
                        constructor.GetParameters()
                                   .Select(p => $"{p.ParameterType.Name} {p.Name}"));

                    Console.WriteLine($"  {type.Name}({parameters})");
                }

                Console.WriteLine("Методы:");

                MethodInfo[] methods = type.GetMethods(
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance |
                    BindingFlags.Static |
                    BindingFlags.DeclaredOnly);

                foreach (MethodInfo method in methods)
                {
                    if (method.IsSpecialName)
                        continue;

                    var displayAttribute = method.GetCustomAttribute<DisplayNameAttribute>();

                    string displayName = "";

                    if (displayAttribute != null)
                    {
                        displayName = $" [{displayAttribute.DisplayName}]";
                    }

                    string parameters = string.Join(", ",
                        method.GetParameters()
                              .Select(p => $"{p.ParameterType.Name} {p.Name}"));

                    Console.WriteLine($"  {method.ReturnType.Name} {method.Name}({parameters}){displayName}");
                }

                Console.WriteLine();
            }
        }
    }
}