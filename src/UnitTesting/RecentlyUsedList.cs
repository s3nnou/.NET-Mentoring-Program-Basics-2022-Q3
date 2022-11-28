using System.Collections;

namespace UnitTesting
{
    public interface IRecentlyUsedList : IEnumerable<string>
    {
        void Add(string item);
    }

    public class RecentlyUsedList : IRecentlyUsedList
    {        
        private List<string> _items;

        public int Count { get; private set; }

        public int Size { get; private set; }

        public string this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerator<string> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(string item)
        {
            throw new NotImplementedException();
        }
    }
}