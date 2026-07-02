using Xunit;
using task01;

namespace task01tests
{
    public class StringExtensionTests
    {
        [Fact]
        public void test1()
        {
            Assert.True("А роза упала на лапу Азора".IsPalindrome());
        }

        [Fact]
        public void test2()
        {
            Assert.False("hello".IsPalindrome());
        }

        [Fact]
        public void test3()
        {
            Assert.True("казак".IsPalindrome());
        }

        [Fact]
        public void test4()
        {
            Assert.True("12321".IsPalindrome());
        }

        [Fact]
        public void test5()
        {
            Assert.False("12345".IsPalindrome());
        }

        [Fact]
        public void test6()
        {
            Assert.False("".IsPalindrome());
        }
    }
}
