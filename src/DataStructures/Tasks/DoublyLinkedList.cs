using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Tasks.DoNotChange;

namespace Tasks
{
    internal sealed class DoublyLinkedListEnumerator<T> : IEnumerator<T>
    {
        private Node<T> _head;
        private Node<T> _current;

        public DoublyLinkedListEnumerator(Node<T> head)
        {
            _head = head;
        }

        public T Current
        {
            get
            {
                try
                {
                    return _current.Data;
                }
                catch (NullReferenceException)
                {
                    throw new NullReferenceException();
                }
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            _current = null;
            _head = null;
        }

        public bool MoveNext()
        {
            if (_current == null)
            {
                _current = _head;
                return true;
            }

            if (_current.Next != null)
            {
                _current = _current.Next;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            _current = null;
        }
    }

    public sealed class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private Node<T> _head;

        public int Length { get; private set; }

        public void Add(T e)
        {
            var newNode = new Node<T>(e);

            if(_head == null)
            {
                _head = newNode;
            }
            else
            {
                AddToTail(newNode);
            }

            Length++;
        }

        public void AddAt(int index, T e)
        {
            if(index < 0 || index > Length)
            {
                throw new IndexOutOfRangeException($"Cannot place element at {index}, because it index is out of range!");
            }
            else
            {
                var newNode = new Node<T>(e);

                if (index == 0)
                {
                    if (_head == null)
                    {
                        _head = newNode;
                    }
                    else
                    {
                        newNode.Next = _head;
                        _head.Prev = newNode;
                        _head = newNode;
                    }
                }
                else if (index == Length)
                {
                    AddToTail(newNode);
                }
                else
                {
                    var currentNode = GetNodeByIndex(index);

                    newNode.Prev = currentNode.Prev;
                    newNode.Next = currentNode;
                    currentNode.Prev.Next = newNode;
                    currentNode.Prev = newNode;
                }
            }

            Length++;
        }

        public T ElementAt(int index)
        {
            if (index < 0 || index >= Length)
            {
                throw new IndexOutOfRangeException($"Cannot get element at {index}, because it index is out of range!");
            }

            if (index == 0)
            {
                return _head.Data;
            }

            var searchedNode = GetNodeByIndex(index);

            return searchedNode.Data;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new DoublyLinkedListEnumerator<T>(_head);
        }

        public void Remove(T item)
        {
            if (_head.Data.Equals(item))
            {
                RemoveHead();
                return;
            }

            var currentNode = _head;
            while (currentNode != null)
            {
                if (currentNode.Data.Equals(item))
                {
                    if (currentNode.Next == null)
                    {
                        RemoveTail(currentNode);
                        return;
                    }

                    RemoveNode(currentNode);
                    return;
                }

                currentNode = currentNode.Next;
            }
        }

        public T RemoveAt(int index)
        {
            if (index < 0 || index >= Length || Length == 0)
            {
                throw new IndexOutOfRangeException($"Cannot remove element at {index}, because it index is out of range!");
            }

            if (index == 0)
            {
                return RemoveHead();
            }

            var currentNode = GetNodeByIndex(index);

            if (index == Length - 1)
            {
                return RemoveTail(currentNode);
            }

            return RemoveNode(currentNode);
        }

        private T RemoveHead()
        {
            var headValue = _head.Data;
            if (_head.Next == null)
            {
                _head.Clear();
                _head = null;
            }
            else
            {
                var temp = _head;
                _head = _head.Next;
                _head.Prev = null;
                temp.Clear();
            }

            Length--;
            return headValue;
        }

        private T RemoveTail(Node<T> node)
        {
            node.Prev.Next = null;
            Length--;
            return node.Data;
        }

        private void AddToTail(Node<T> newNode)
        {
            var currentNode = _head;

            while (currentNode.Next != null)
            {
                currentNode = currentNode.Next;
            }

            currentNode.Next = newNode;
            newNode.Prev = currentNode;
        }

        private Node<T> GetNodeByIndex(int index)
        {
            var currentNode = _head;
            for (var i = 0; i < index; i++)
            {
                currentNode = currentNode.Next;
            }

            return currentNode;
        }

        private T RemoveNode(Node<T> node)
        {
            var tempNode = node;
            node.Prev.Next = node.Next;
            node.Next.Prev = node.Prev;
            tempNode.Clear();
            Length--;
            return node.Data;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
