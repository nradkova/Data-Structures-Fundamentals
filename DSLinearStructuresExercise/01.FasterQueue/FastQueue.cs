namespace Problem01.FasterQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    //public class Node<T>
    //{
    //    public T Item { get; set; }

    //    public Node<T> Next { get; set; }
    //}
    public class FastQueue<T> : IAbstractQueue<T>
    {
        private Node<T> _head;
        private Node<T> _tail;
        public int Count { get; private set; }

        public bool Contains(T item)
        {
            var current = this._head;

            while (current != null)
            {
                if (current.Item.Equals(item))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public T Dequeue()
        {
            this.ValidateNotEmpty();
            var node = this._head;
            var newHead = this._head.Next;
            if (this.Count == 1)
            {
                this._tail = null;
                this._head = null;
            }
            else
            {
                this._head.Next=null;
                this._head = newHead;
            }
            this.Count--;
            return node.Item;

        }

        public void Enqueue(T item)
        {
            var node = new Node<T> { Item = item };
            if (this.Count == 0)
            {
                this._head = node;
                this._tail = node;
            }
            else
            {
                this._tail.Next=node;
                this._tail = node;
            }
            this.Count++;
        }

        public T Peek()
        {
            this.ValidateNotEmpty();
            return this._head.Item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this._head;
            while (current != null)
            {
                yield return current.Item;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
       => this.GetEnumerator();

        private void ValidateNotEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}