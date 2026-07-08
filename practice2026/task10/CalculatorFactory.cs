using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace task11
{
    public static class CalculatorFactory
    {
        public static ICalculator CreateCalculator()
        {
            string code = @"
using task11;

public class Calculator : ICalculator
{
    public int Add(int a, int b) => a + b;
    public int Minus(int a, int b) => a - b;
    public int Mul(int a, int b) => a * b;
    public int Div(int a, int b) => a / b;
}";

            var tree = CSharpSyntaxTree.ParseText(code);

            var references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ICalculator).Assembly.Location)
            };

            var compilation = CSharpCompilation.Create(
                "Calculator",
                new[] { tree },
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using var stream = new MemoryStream();

            var result = compilation.Emit(stream);

            if (!result.Success)
            {
                throw new Exception("Ошибка компиляции");
            }

            stream.Position = 0;

            var assembly = Assembly.Load(stream.ToArray());

            var type = assembly.GetType("Calculator");

            return (ICalculator)Activator.CreateInstance(type)!;
        }
    }
}