namespace Problem01.List
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class List<T> : IAbstractList<T>
    {
        private const int DEFAULT_CAPACITY = 4;
        private T[] _items;

        public List()
            : this(DEFAULT_CAPACITY)
        {
        }

        public List(int capacity)
        {
            if (capacity<0)
            {
                throw new ArgumentOutOfRangeException
                    ("Invalid capacity");
            }
            this._items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                this.ValidateIndex(index);
                return this._items[index];
            }
            set
            {
                this.ValidateIndex(index);
                this._items[index]=value;
            }
        }


        public int Count { get; private set; }

        public void Add(T item)
        {
            this.GrowIfNecessary();
            this._items[this.Count] = item;
            this.Count++;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < this._items.Length; i++)
            {
                if (this._items[i].Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < this._items.Length; i++)
            {
                if (this._items[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            this.ValidateIndex(index);
            this.GrowIfNecessary();
            for (int i = this._items.Length - 1; i > index; i--)
            {
                this._items[i] = this._items[i - 1];
            }
            this._items[index] = item;
            Count++;
        }

        public bool Remove(T item)
        {
            var indexToRemove = this.IndexOf(item);
           
            if (indexToRemove==-1)
            {
                return false;
            }
            else
            {
                for (int i = indexToRemove; i < this.Count-1; i++)
                {
                    this._items[i] = this._items[i + 1];
                }
                this._items[this.Count - 1] = default;
                this.Count--;
                return true;
            }
        }
        public void RemoveAt(int index)
        {
            this.ValidateIndex(index);
            for (int i = index; i < this.Count - 1; i++)
            {
                this._items[i] = this._items[i + 1];
            }
            this._items[this.Count - 1] = default;
            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this._items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() 
            => this.GetEnumerator();

        private void GrowIfNecessary()
        {
            if (this.Count == this._items.Length)
            {
                this._items = this.Grow();
            }
        }

        private T[] Grow()
        {
            var newArr = new T[this._items.Length * 2];
            Array.Copy(this._items, newArr, this._items.Length);
            return newArr;
        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException
                    ("Invalid index");
            }
        }
    }
}