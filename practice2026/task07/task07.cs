using System;
using System.Reflection;

namespace task07
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class DisplayNameAttribute : Attribute
    {
        public string DisplayName { get; }

        public DisplayNameAttribute(string displayName)
        {
            DisplayName = displayName;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class VersionAttribute : Attribute
    {
        public int Major { get; }
        public int Minor { get; }

        public VersionAttribute(int major, int minor)
        {
            Major = major;
            Minor = minor;
        }
    }

    [task07.DisplayName("Пример класса")]
    [Version(1, 0)]
    public class SampleClass
    {
        [task07.DisplayName("Числовое свойство")]
        public int Number { get; set; }

        [task07.DisplayName("Тестовый метод")]
        public void TestMethod()
        {
        }
    }

    public static class ReflectionHelper
    {
        public static void PrintTypeInfo(Type type)
        {
            var display = type.GetCustomAttribute<task07.DisplayNameAttribute>();

            if (display != null)
            {
                Console.WriteLine(display.DisplayName);
            }

            var version = type.GetCustomAttribute<VersionAttribute>();

            if (version != null)
            {
                Console.WriteLine($"{version.Major}.{version.Minor}");
            }

            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                var attr = method.GetCustomAttribute<task07.DisplayNameAttribute>();

                if (attr != null)
                {
                    Console.WriteLine($"{method.Name} - {attr.DisplayName}");
                }
            }

            foreach (var property in type.GetProperties())
            {
                var attr = property.GetCustomAttribute<task07.DisplayNameAttribute>();

                if (attr != null)
                {
                    Console.WriteLine($"{property.Name} - {attr.DisplayName}");
                }
            }
        }
    }
}