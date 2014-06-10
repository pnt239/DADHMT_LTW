using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Untipic.Visualization
{
    public class DoublyLinkedListNode<T>
    {
        public DoublyLinkedListNode(T value)
        {
            Value = value;
            Nex = Pre = null;
        }

        public DoublyLinkedListNode<T> Next
        {
            get { return Nex; }
        }

        public DoublyLinkedListNode<T> Previous
        {
            get { return Pre; }
        }

        public T Value { get; set; }

        internal DoublyLinkedListNode<T> Nex;
        internal DoublyLinkedListNode<T> Pre;
    }

    public class SortedDoublyLinkedList<T> where T : IComparable<T>
    {

        public SortedDoublyLinkedList()
        {
            Count = 0;
            _head = _tail = null;
        }

        public int Count { get; private set; }

        public DoublyLinkedListNode<T> First
        {
            get { return _head; }
        }

        public DoublyLinkedListNode<T> Last
        {
            get { return _tail; }
        }


        public void Add(T value)
        {
            Add(new DoublyLinkedListNode<T>(value));
        }

        public void Add(DoublyLinkedListNode<T> newNode)
        {
            if (_head == null)   // list is empty
            {
                _head = _tail = newNode;
            }
            else if (_head == _tail)  // list has one element
            {
                if (_head.Value.CompareTo(newNode.Value) <= 0)
                {
                    _tail = newNode;
                }
                else
                {
                    _head = newNode;
                }
                connect(_head, _tail);
            }
            else        // list has 2 or more elements
            {
                if (newNode.Value.CompareTo(_head.Value) <= 0)       // insert head
                {
                    connect(newNode, _head);
                    _head = newNode;
                }
                else if (newNode.Value.CompareTo(_tail.Value) > 0)   // insert tail
                {
                    connect(_tail, newNode);
                    _tail = newNode;
                }
                else                                                // somewhere in the middle
                {
                    var current = _tail.Pre;
                    DoublyLinkedListNode<T> insertAfter = null;
                    while (current != _head)
                    {
                        if (newNode.Value.CompareTo(current.Value) > 0)
                        {
                            insertAfter = current;
                            break;
                        }
                        current = current.Pre;
                    }

                    if (insertAfter == null)
                        insertAfter = _head;
                    // disconnect(insertBefore.previous, insertBefore);     // do not, previous needed on the next line
                    connect(newNode, insertAfter.Nex);
                    connect(insertAfter, newNode);
                }
            }

            Count++;
        }

        public void Remove(DoublyLinkedListNode<T> node)
        {
            if (node == _head)
            {
                // Remove head
                node.Pre = null;
                _head = node.Next;
            }
            else if (node == _tail)
            {
                // Remove tail
                _tail = node.Pre;
                _tail.Nex = null;
            }
            else
            {
                // Remove middle
                var pre = node.Pre;
                var nex = node.Nex;
                pre.Nex = nex;
                nex.Pre = pre;
            }
            Count--;
        }

        public void Clear()
        {
            _head = null;
            _tail = null;
            Count = 0;
        }

        public void Sort()
        {
            var tlist = _head;

            Clear();
            while (tlist != null)
            {
                Add(tlist.Value);
                tlist = tlist.Next;
            }
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        public class Enumerator : IEnumerator<T>
        {
            private SortedDoublyLinkedList<T> _list;
            private DoublyLinkedListNode<T> _current;
            private bool _start;

            internal Enumerator(SortedDoublyLinkedList<T> list)
            {
                _list = list;
                _current = null;
                _start = true;
            }

            public void Reset()
            {
                _start = true;
                _current = null;
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            // Summary:
            //     Gets the element at the current position of the enumerator.
            //
            // Returns:
            //     The element in the System.Collections.Generic.LinkedList<T> at the current
            //     position of the enumerator.
            public T Current
            {
                get { return _current.Value; }
            }

            // Summary:
            //     Releases all resources used by the System.Collections.Generic.LinkedList<T>.Enumerator.
            public void Dispose()
            {
            }

            //
            // Summary:
            //     Advances the enumerator to the next element of the System.Collections.Generic.LinkedList<T>.
            //
            // Returns:
            //     true if the enumerator was successfully advanced to the next element; false
            //     if the enumerator has passed the end of the collection.
            //
            // Exceptions:
            //   System.InvalidOperationException:
            //     The collection was modified after the enumerator was created.
            public bool MoveNext()
            {
                if (_start)
                {
                    // if (current == null) {      // circular
                    _start = false;
                    _current = _list._head;
                }
                else if (_current == null)
                {
                    return false;
                }
                else
                {
                    _current = _current.Next;
                }
                return HasCurrent();
            }

            private bool HasCurrent()
            {
                return _current != null;
            }
        }

        private void connect(DoublyLinkedListNode<T> n1, DoublyLinkedListNode<T> n2)
        {
            n1.Nex = n2;
            n2.Pre = n1;
        }

        private DoublyLinkedListNode<T> _head;
        private DoublyLinkedListNode<T> _tail;
    }
}
