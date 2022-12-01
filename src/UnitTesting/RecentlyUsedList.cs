using System.Collections;

namespace UnitTesting
{
    public interface IRecentlyUsedList : IEnumerable<string>
    {
        int Count { get;}

        int Size { get;}

        string this[int index] { get; }

        void Add(string item);
    }

    public class RecentlyUsedList : IRecentlyUsedList
    {        
        private const int DefaultLimitSize = 5;

        private List<string> _items;

        public RecentlyUsedList()
        {
            _items = new List<string>();
            Count = 0;
        }

        public RecentlyUsedList(int limit = 0)
        {
            _items = new List<string>();
            Count = 0;
            Size = limit == 0 ? DefaultLimitSize : limit;
        }

        public int Count { get; private set; }

        public int Size { get; private set; } = -1;

        public string this[int index]
        {
            get
            {
                if (index >= Count || index < 0)
                    throw new IndexOutOfRangeException();

                return _items.ElementAt(index);
            }
        }

        public void Add(string item)
        {
            if(Size != -1 && Count >= Size)
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