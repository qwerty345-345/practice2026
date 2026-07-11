using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;

namespace task11
{
    public static class CalculatorFactory
    {
        public static ICalculator CreateCalculator()
        {
            string code = @"
using System;

namespace RuntimeCalculator
{
    public class Calculator : task11.ICalculator
    {
        public int Add(int a, int b) => a + b;
        public int Minus(int a, int b) => a - b;
        public int Mul(int a, int b) => a * b;
        public int Div(int a, int b) => a / b;
    }
}";

            var tree = CSharpSyntaxTree.ParseText(code);

            var references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ICalculator).Assembly.Location)
            };

            var compilation =
                CSharpCompilation.Create(
                    "RuntimeCalculator")
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                .AddReferences(references)
                .AddSyntaxTrees(tree);

            using var stream = new MemoryStream();

            var result = compilation.Emit(stream);

            if (!result.Success)
                throw new Exception("Ошибка компиляции");

            stream.Position = 0;

            var assembly = Assembly.Load(stream.ToArray());

            var type = assembly.GetType("RuntimeCalculator.Calculator");

            return (ICalculator)Activator.CreateInstance(type)!;
        }
    }
}