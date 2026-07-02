using System.Collections.Generic;
using System.Linq;
using task03;
using Xunit;

namespace task03tests
{
    public class IteratorTests
    {
        [Fact]
        public void CustomCollection_GetEnumerator_ReturnsAllItems()
        {
            var collection = new CustomCollection<int>();

            collection.Add(1);
            collection.Add(2);

            var result = new List<int>();

            foreach (var item in collection)
            {
                result.Add(item);
            }

            Assert.Equal(new[] { 1, 2 }, result);
        }

        [Fact]
        public void GetReverseEnumerator_ReturnsItemsInReverseOrder()
        {
            var collection = new CustomCollection<int>();

            collection.Add(1);
            collection.Add(2);
            collection.Add(3);

            var result = collection.GetReverseEnumerator().ToList();

            Assert.Equal(new[] { 3, 2, 1 }, result);
        }

        [Fact]
        public void GenerateSequence_ReturnsCorrectSequence()
        {
            var result = CustomCollection<int>.GenerateSequence(5, 3).ToList();

            Assert.Equal(new[] { 5, 6, 7 }, result);
        }

        [Fact]
        public void FilterAndSort_ReturnsFilteredAndSortedItems()
        {
            var collection = new CustomCollection<int>();

            collection.Add(3);
            collection.Add(1);
            collection.Add(2);
            collection.Add(5);

            var result = collection.FilterAndSort(x => x > 1, x => x).ToList();

            Assert.Equal(new[] { 2, 3, 5 }, result);
        }

        [Fact]
        public void Remove_RemovesItem()
        {
            var collection = new CustomCollection<int>();

            collection.Add(1);
            collection.Add(2);

            collection.Remove(1);

            var result = collection.ToList();

            Assert.Equal(new[] { 2 }, result);
        }
    }
}