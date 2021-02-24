namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
    {
        private Node<T> _head;
        public Queue()
        {
            this._head = null;
            this.Count = 0;
        }
        public Queue(Node<T> head)
        {
            this._head = head;
            this.Count = 1;
        }

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            this.ValidateNotEmpty();
            var node = this._head;
            while (node != null)
            {
                if (node.Value.Equals(item))
                {
                    return true;
                }
                node = node.Next;
            }
            return false;
        }

        public T Dequeue()
        {
            this.ValidateNotEmpty();
            var node = this._head;
            this._head = node.Next;
            this.Count--;
            return node.Value;
        }

        public void Enqueue(T item)
        {
            var node= new Node<T>(item);

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

        public T Peek()
        {
            this.ValidateNotEmpty();
            return this._head.Value;
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
                    ("Queue is empty");
            }
        }
    }
}