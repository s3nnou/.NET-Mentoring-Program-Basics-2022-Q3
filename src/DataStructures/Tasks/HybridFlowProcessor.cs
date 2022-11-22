using System;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        private IDoublyLinkedList<T> _list = new DoublyLinkedList<T>();

        public T Dequeue()
        {
            ValidateCollectionLength();

            return _list.RemoveAt(0);
        }

        public void Enqueue(T item) => _list.Add(item);

        public T Pop()
        {
            ValidateCollectionLength();

            return _list.RemoveAt(0);
        }

        public void Push(T item) => _list.AddAt(0, item);

        private void ValidateCollectionLength()
        {
            if (_list.Length <= 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
