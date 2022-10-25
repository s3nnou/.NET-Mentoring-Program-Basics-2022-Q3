using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AdvancedCSharp
{
    public class Node : IEnumerable<Node>
    {
        public Node(Leaf data, Node parent)
        {
            Data = data;
            Children = new ObservableCollection<Node>();
            Parent = parent;
        }

        public Node(Node node)
        {
            Data = node.Data;
            Children = node.Children;
            Parent = node.Parent;
        }

        public Node(Leaf data)
        {
            Data = data;
            Children = new ObservableCollection<Node>();
        }

        public Node Parent { get; set; }

        public Leaf Data { get; set; }

        public ObservableCollection<Node> Children { get; set; } = new ObservableCollection<Node>();

        public IEnumerator<Node> GetEnumerator()
        {
            yield return this;

            foreach (var children in Children)
            {
                foreach (var grandchildren in children)
                {
                    yield return grandchildren;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
