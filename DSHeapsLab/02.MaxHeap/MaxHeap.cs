namespace _02.MaxHeap
{
    using System;
    using System.Collections.Generic;

    public class MaxHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> elements;

        public MaxHeap()
        {
            this.elements = new List<T>();
        }

        public int Size => this.elements.Count;

        public void Add(T element)
        {
            this.elements.Add(element);
            this.HeapifyUp();
        }

        public T Peek()
        {
            this.EnsureNotEmpty();
            return this.elements[0];
        }

        private void EnsureNotEmpty()
        {
            if (this.elements.Count==0)
            {
                throw new InvalidOperationException();
            }
        }

        private void HeapifyUp()
        {
            int currentInd = this.Size - 1;
            int parentInd = this.GetParentIndex(currentInd);
            while (this.IsValidIndex(currentInd)
                && IsGreater(currentInd, parentInd))
            {
                Swap(currentInd, parentInd);
                currentInd = parentInd;
                parentInd = this.GetParentIndex(currentInd);
            }
        }

        private void Swap(int childInd, int parentInd)
        {
            var temp = elements[childInd];
            elements[childInd] = elements[parentInd];
            elements[parentInd] = temp;
        }

        private bool IsGreater(int childInd, int parentInd)
        {
            return this.elements[childInd]
                .CompareTo(this.elements[parentInd]) > 0;
        }

        private int GetParentIndex(int childIndex)
        {
            return (childIndex - 1) / 2;
        }
        private bool IsValidIndex(int index)
        {
            return index > 0;
        }
    }
}
