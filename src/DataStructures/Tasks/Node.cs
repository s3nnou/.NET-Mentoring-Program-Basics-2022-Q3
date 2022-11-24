namespace Tasks
{
    public sealed class Node<T>
    {
        public Node(T data)
        {
            Data = data;
        }

        public T Data { get; private set; }

        public Node<T> Next { get; set; }

        public Node<T> Prev { get; set; }

        public void Clear()
        {
            Next = null;
            Prev = null;
        }
    }
}
