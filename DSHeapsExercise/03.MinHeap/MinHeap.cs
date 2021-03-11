namespace _03.MinHeap
{
    using System;
    using System.Collections.Generic;

    public class MinHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> _elements;

        public MinHeap()
        {
            this._elements = new List<T>();
        }

        public int Size => this._elements.Count;

        public T Dequeue()
        {
            this.EnsureNotEmpty();
            var node = this.Peek();
            this.Swap(0, this.Size - 1);
            this._elements.RemoveAt(this.Size - 1);
            this.HeapifyDown();
            return node;
        }

        public void Add(T element)
        {
            this._elements.Add(element);
            this.HeapifyUp();
        }

        public T Peek()
        {
            this.EnsureNotEmpty();
            return this._elements[0];
        }

        private void HeapifyDown()
        {
            int index = 0;
            int leftChildInd = this.GetLeftChildIndex(index);
            while (IsValidChild(leftChildInd)
                && IsGreater( index,leftChildInd))
            {
                int indToSwap = leftChildInd;
                int rightChildInd = this.GetRightChildIndex(index);
                if (IsValidChild(rightChildInd)
                    && IsGreater(indToSwap,rightChildInd))
                {
                    indToSwap = rightChildInd;
                }
                this.Swap(index, indToSwap);
                index = indToSwap;
                leftChildInd = this.GetLeftChildIndex(index);
            }
        }

        private int GetRightChildIndex(int index)
        {
            return index * 2 + 2;
        }

        private bool IsValidChild(int index)
        {
            return index < this.Size;
        }

        private int GetLeftChildIndex(int index)
        {
            return index * 2 + 1;
        }

        private void EnsureNotEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException();
            }
        }

        private void HeapifyUp()
        {
            int currentInd = this.Size - 1;
            int parentInd = this.GetParentIndex(currentInd);
            while (IsValidIndex(currentInd)
                && IsLess(currentInd, parentInd))
            {
                this.Swap(currentInd, parentInd);
                currentInd = parentInd;
                parentInd = this.GetParentIndex(currentInd);
            }
        }

        private void Swap(int currentInd, int parentInd)
        {
            var temp = this._elements[currentInd];
            this._elements[currentInd] = this._elements[parentInd];
            this._elements[parentInd] = temp;
        }

        private bool IsLess(int currentInd, int parentInd)
        {
            return this._elements[currentInd]
                 .CompareTo(this._elements[parentInd])< 0;
        }
        private bool IsGreater(int currentInd, int parentInd)
        {
            return this._elements[currentInd]
                 .CompareTo(this._elements[parentInd]) > 0;
        }

        private bool IsValidIndex(int index)
        {
            return index > 0;
        }

        private int GetParentIndex(int index)
        {
            return (index - 1) / 2;
        }

    }
}
