using System.Collections;

namespace UnitTesting
{
    public interface IRecentlyUsedList : IEnumerable<string>
    {
        void Add(string item);
    }

    public class RecentlyUsedList : IRecentlyUsedList
    {        
        private const int DefaultSize = 5;

        private List<string> _items;

        public RecentlyUsedList()
        {
            _items = new List<string>();
            Count = 0;
            Size = DefaultSize;
        }

        public RecentlyUsedList(int limit)
        {
            _items = new List<string>();
            Count = 0;
            Size = limit;
        }

        public int Count { get; private set; }

        public int Size { get; private set; }

        public string this[int index]
        {
            get
            {
                if (index >= Size || index < 0)
                    throw new IndexOutOfRangeException();

                return _items.ElementAt(index);
            }
        }

        public void Add(string item)
        {
            if(Count >= Size)
            {
                return;
            }

            if (string.IsNullOrEmpty(item))
                throw new ArgumentException();

            RemoveDuplicate(item);
            _items.Insert(0, item);
            Count++;
        }

        private void RemoveDuplicate(string item)
        {

            var index = _items.IndexOf(item);
            if (index != -1)
            {
                _items.RemoveAt(index);
                Count--;
            }
        }

        public IEnumerator<string> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
}