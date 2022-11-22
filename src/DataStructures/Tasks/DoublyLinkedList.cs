using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedListEnumerator<T> : IEnumerator<T>
    {
        private Node<T> _head;
        private Node<T> _current;

        public DoublyLinkedListEnumerator(Node<T> head)
        {
            _head = head;
        }

        public T Current => _current.Data;

        object IEnumerator.Current => _current.Data;

        public void Dispose()
        {
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

    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private Node<T> _head;

        public int Length { get; private set; }

        public void Add(T e)
        {
            var newNode = new Node<T> { Data = e };

            if(_head == null)
            {
                _head = newNode;
            }
            else
            {
                var currentNode = _head;

                while (currentNode.Next != null)
                {
                    currentNode = currentNode.Next;
                }

                currentNode.Next = newNode;
                newNode.Prev = currentNode;
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
                var newNode = new Node<T> { Data = e };

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
                    var currNode = _head;
                    while (currNode.Next != null)
                    {
                        currNode = currNode.Next;
                    }

                    currNode.Next = newNode;
                    newNode.Prev = currNode;
                }
                else
                {
                    var currentNode = _head;
                    for (var i = 0; i < index; i++)
                    {                        
                        currentNode = currentNode.Next;
                    }

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

            var currentNode = _head;
            for (var i = 0; i < index; i++)
            {
                currentNode = currentNode.Next;
            }

            return currentNode.Data;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new DoublyLinkedListEnumerator<T>(_head);
        }

        public void Remove(T item)
        {
            if (_head.Data.Equals(item))
            {
                if (_head.Next == null)
                {
                    _head = null;
                }
                else
                {
                    _head = _head.Next;
                    _head.Prev = null;
                }

                Length--;
                return;
            }

            var currentNode = _head;
            for (var i = 0; i < Length; i++)
            {
                if (currentNode.Data.Equals(item))
                {
                    if (currentNode.Next == null)
                    {
                        currentNode.Prev.Next = null;
                        Length--;
                        return;
                    }

                    currentNode.Prev.Next = currentNode.Next;
                    currentNode.Next.Prev = currentNode.Prev;
                    Length--;
                    return;
                }

                if (currentNode.Next != null)
                {
                    currentNode = currentNode.Next;
                }         
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
                var headValue = _head.Data;
                if (_head.Next == null)
                {
                    _head = null;
                }
                else
                {
                    _head = _head.Next;
                    _head.Prev = null;
                }

                Length--;
                return headValue;
            }

            var currentNode = _head;
            for (var i = 0; i < index; i++)
            {
                currentNode = currentNode.Next;
            }

            if (index == Length - 1)
            {
                currentNode.Prev.Next = null;
                Length--;
                return currentNode.Data;
            }

            currentNode.Prev.Next = currentNode.Next;
            currentNode.Next.Prev = currentNode.Prev;
            Length--;

            return currentNode.Data;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
