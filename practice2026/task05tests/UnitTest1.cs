using System;
using System.Linq;
using Xunit;
using task05;

namespace task05tests
{
    public class TestClass
    {
        public int PublicField;
        private string _privateField;
        public int Property { get; set; }

        public void Method() { }
    }

    [Serializable]
    public class AttributedClass { }

    public class ClassAnalyzerTests
    {
        [Fact]
        public void GetPublicMethods_ReturnsCorrectMethods()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));

            var methods = analyzer.GetPublicMethods().ToList();

            Assert.Contains("Method", methods);
        }

        [Fact]
        public void GetAllFields_IncludesPrivateFields()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));

            var fields = analyzer.GetAllFields().ToList();

            Assert.Contains("_privateField", fields);
        }

        [Fact]
        public void GetProperties_ReturnsCorrectProperties()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));

            var props = analyzer.GetProperties().ToList();

            Assert.Contains("Property", props);
        }

        [Fact]
        public void HasAttribute_ReturnsTrue_WhenAttributeExists()
        {
            var analyzer = new ClassAnalyzer(typeof(AttributedClass));

            Assert.True(analyzer.HasAttribute<SerializableAttribute>());
        }

        [Fact]
        public void HasAttribute_ReturnsFalse_WhenAttributeNotExists()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));

            Assert.False(analyzer.HasAttribute<SerializableAttribute>());
        }
    }
}