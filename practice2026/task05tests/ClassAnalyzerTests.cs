using System;
using System.Linq;
using task05;
using task05;
using Xunit;

namespace task05tests
{
    public class TestClass
    {
        public int PublicField;
        private string _privateField;

        public int Property { get; set; }

        public void Method()
        {
        }

        public int Method2(int par1, bool par2)
        {
            return 666;
        }
    }

    [Serializable]
    public class AttributedClass
    {
    }

    public class ClassAnalyzerTests
    {
        [Fact]
        public void GetPublicMethods_ReturnsCorrectMethods()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));

            var methods = analyzer.GetPublicMethods();

            Assert.Contains("Method", methods);
            Assert.Contains("Method2", methods);
        }

        [Fact]
        public void GetAllFields_IncludesPrivateFields()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));

            var fields = analyzer.GetAllFields();

            Assert.Contains("_privateField", fields);
            Assert.Contains("PublicField", fields);
        }

        [Fact]
        public void GetProperties_ReturnsCorrectProperties()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));

            var properties = analyzer.GetProperties();

            Assert.Contains("Property", properties);
        }

        [Fact]
        public void GetMethodParams_WhenMethodDoesNotExist()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));

            var result = analyzer.GetMethodParams("Test").ToList();

            Assert.Empty(result);
        }

        [Fact]
        public void GetMethodParams_WhenMethodHasNoParameters()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));

            var result = analyzer.GetMethodParams("Method").ToList();

            Assert.Single(result);
            Assert.Equal("Void", result[0]);
        }

        [Fact]
        public void GetMethodParams_WhenMethodHasParameters()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));

            var result = analyzer.GetMethodParams("Method2").ToList();

            Assert.Equal("Int32", result[0]);
            Assert.Equal("Int32 par1", result[1]);
            Assert.Equal("Boolean par2", result[2]);
        }

        [Fact]
        public void HasAttribute_ReturnsTrue_WhenAttributeExists()
        {
            var analyzer = new ClassAnalyzer(typeof(AttributedClass));

            Assert.True(analyzer.HasAttribute<SerializableAttribute>());
        }

        [Fact]
        public void HasAttribute_ReturnsFalse_WhenAttributeDoesNotExist()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));

            Assert.False(analyzer.HasAttribute<SerializableAttribute>());
        }
    }
}
