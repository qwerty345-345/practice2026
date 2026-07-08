using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace task._05
{
    public class ClassAnalyzer
    {
        private Type _type;

        public ClassAnalyzer(Type type)
        {
            _type = type;
        }

        public IEnumerable<string> GetPublicMethods()
        {
            return _type.GetMethods()
                .Where(m => !m.IsSpecialName)
                .Select(m => m.Name);
        }

        public IEnumerable<string> GetMethodParams(string methodname)
        {
            var method = _type.GetMethod(methodname);

            if (method == null)
            {
                return Enumerable.Empty<string>();
            }

            var result = new List<string>
            {
                method.ReturnType.Name
            };

            foreach (var p in method.GetParameters())
            {
                result.Add($"{p.ParameterType.Name} {p.Name}");
            }

            return result;
        }

        public IEnumerable<string> GetAllFields()
        {
            return _type.GetFields(BindingFlags.Public |
                                   BindingFlags.NonPublic |
                                   BindingFlags.Instance |
                                   BindingFlags.Static)
                        .Select(f => f.Name);
        }

        public IEnumerable<string> GetProperties()
        {
            return _type.GetProperties()
                        .Select(p => p.Name);
        }

        public bool HasAttribute<T>() where T : Attribute
        {
            return _type.GetCustomAttributes(typeof(T), true).Any();
        }
    }
}