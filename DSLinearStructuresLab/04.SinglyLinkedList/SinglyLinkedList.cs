namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> _head;
        public SinglyLinkedList()
        {
            this._head = null;
            this.Count = 0;
        }
        public SinglyLinkedList(Node<T> head)
        {
            this._head = head;
            this.Count = 1;
        }

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var node = new Node<T>(item);
            node.Next = this._head;
            this._head = node;
            this.Count++;
        }

        public void AddLast(T item)
        {
            var node = new Node<T>(item);
            if (this.Count == 0)
            {
                this._head = node;
            }
            else
            {
                var current = this._head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = node;
            }
            this.Count++;
        }

        public T GetFirst()
        {
            this.ValidateNotEmpty();
            var node = this._head;
            return node.Value;
        }

        public T GetLast()
        {
            this.ValidateNotEmpty();
            var node = this._head;
            while (node.Next != null)
            {
                node = node.Next;
            }
            return node.Value;
        }

        public T RemoveFirst()
        {
            this.ValidateNotEmpty();
            var node = this._head;
            this._head = node.Next;
            this.Count--;
            return node.Value;
        }

        public T RemoveLast()
        {
            this.ValidateNotEmpty();
            var node = this._head;
            if (this.Count == 1)
            {
                this._head = null;
            }
            var prev = node;
            while (node.Next != null)
            {
                prev = node;
                node = node.Next;
            }
            prev.Next = null;
            this.Count--;
            return node.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var node = this._head;
            while (node != null)
            {
                yield return node.Value;
                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void ValidateNotEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException
                    ("SinglyLinkedList is empty");
            }
        }
    }
}