using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace task03
{
    public class CustomCollection<T> : IEnumerable<T>
    {
        private List<T> _items;

        public CustomCollection()
        {
            _items = new List<T>();
        }

        public CustomCollection(List<T> numbers)
        {
            _items = new List<T>(numbers);
        }

        public void Add(T item) => _items.Add(item);

        public void Remove(T item) => _items.Remove(item);

        public IEnumerable<T> GetReverseEnumerator()
        {
            for (int i = _items.Count - 1; i >= 0; i--)
                yield return _items[i];
        }

        public static IEnumerable<int> GenerateSequence(int start, int count)
        {
            for (int i = 0; i < count; i++)
                yield return start + i;
        }

        public IEnumerable<T> FilterAndSort(Func<T, bool> predicate, Func<T, IComparable> keySelector)
        {
            return _items.Where(predicate).OrderBy(keySelector);
        }

        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}