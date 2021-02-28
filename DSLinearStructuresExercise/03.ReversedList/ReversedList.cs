namespace Problem03.ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IAbstractList<T>
    {
        private const int DefaultCapacity = 4;

        private T[] _items;

        public ReversedList()
            : this(DefaultCapacity) { }

        public ReversedList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException
                    (nameof(capacity));

            this._items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                ValidateIndex(index);
                return this._items[this.Count-1-index];
            }
            set
            {
                ValidateIndex(index);
                this._items[index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            this.ResizeIfNeeded();
            this[this.Count++] = item;
            //this.Count++;
        }

        public bool Contains(T item)
        {
            if (this.IndexOf(item)!=-1)
            {
                return true;
            }
            return false;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this._items[this.Count-1-i]
                    .Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            this.ResizeIfNeeded();
            this.ValidateIndex(index);
            index = this.Count- index;
            for (int i = this.Count; i > index; i--)
            {
                this._items[i] = this._items[i - 1];
            }
            this._items[index] = item;
            this.Count++;
        }

        public bool Remove(T item)
        {
            var index = this.IndexOf(item);
            if (index != -1)
            {
                this.RemoveAt(index);
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            this.ValidateIndex(index);
            index = this.Count-1 - index;
            for (int i = index; i < this.Count - 1; i++)
            {
                this._items[i] = this._items[i + 1];
            }
            this._items[this.Count - 1] = default;
            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.Count- 1; i >= 0; i--)
            {
                yield return this._items[i];

            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        => this.GetEnumerator();

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException
                    ("Invalid Index");
            }
        }
        private void ResizeIfNeeded()
        {
            if (this.Count == this._items.Length)
            {
               this._items= this.Resize();
            }
        }

        private T[] Resize()
        {
            var newArr = new T[this._items.Length * 2];
            Array.Copy(this._items, newArr, this._items.Length);
            return newArr;
        }
    }
}