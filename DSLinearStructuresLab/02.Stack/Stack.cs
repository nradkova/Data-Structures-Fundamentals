namespace Problem02.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IAbstractStack<T>
    {
        private Node<T> _top;
        public Stack()
        {
            this._top = null;
            this.Count = 0;
        }
        public Stack(Node<T> node)
        {
            this._top = node;
            this.Count = 1;
        }

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            //this.ValidateNotEmpty();
            var node = this._top;
            while (node != null)
            {
                var current = node.Value;
                if (current.Equals(item))
                {
                    return true;
                }
                node = node.Next;
            }
            return false;
        }

        public T Peek()
        {
            this.ValidateNotEmpty();
            return this._top.Value;
        }


        public T Pop()
        {
            this.ValidateNotEmpty();
            var node = this._top;
            this._top = node.Next;
            this.Count--;
            return node.Value;
        }

        public void Push(T item)
        {
            var node = new Node<T>(item);
            node.Next = this._top;
            this._top = node;
            this.Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var node = this._top;
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
            if (this._top == null)
            {
                throw new InvalidOperationException
                    ("Stack is empty");
            }
        }
    }
}