namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    //public class Node<T>
    //{
    //    public T Item { get; set; }

    //    public Node<T> Next { get; set; }
    //    public Node<T> Prev { get; set; }

    //}
    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> head;
        private Node<T> tail;


        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var node= new Node<T>() { Item = item };
            if (this.Count==0)
            {
                this.head = this.tail = node;
            }
            else
            {
                var prevHead = this.head;
                this.head = node;
                this.head.Next = prevHead;
                prevHead.Prev = this.head;
            }
            this.Count++;
        }

        public void AddLast(T item)
        {
            var node = new Node<T>() { Item = item };
            if (this.Count == 0)
            {
                this.head = this.tail = node;
            }
            else
            {
                var prevHead = this.tail;
                prevHead.Next = node;
                node.Prev = prevHead;
                this.tail = node;
            }
            this.Count++;
        }

        public T GetFirst()
        {
            this.EnsureNotEmpty();
            return this.head.Item;
        }


        public T GetLast()
        {
            this.EnsureNotEmpty();
            return this.tail.Item;
        }

        public T RemoveFirst()
        {
            this.EnsureNotEmpty();
            var node = this.head;
            if (this.Count==1)
            {
                this.head = this.tail = null;
            }
            else
            {
            this.head = node.Next;
            this.head.Prev = null;
            }
            this.Count--;
            return node.Item;
        }

        public T RemoveLast()
        {
            this.EnsureNotEmpty();
            var node = this.tail;
            if (this.Count == 1)
            {
                this.head = this.tail = null;
            }
            else
            {
            this.tail = node.Prev;
            this.tail.Next = null;
            }
            this.Count--;
            return node.Item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var node = this.head;
            while (node.Next!=null)
            {
                yield return node.Item;
                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        => this.GetEnumerator();

        private void EnsureNotEmpty()
        {
            if (this.Count==0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}