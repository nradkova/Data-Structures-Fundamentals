namespace _03.PriorityQueue
{
    using System;
    using System.Collections.Generic;

    public class PriorityQueue<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> elements;
        public PriorityQueue()
        {
            this.elements = new List<T>();
        }
        public int Size => this.elements.Count;

        public T Dequeue()
        {
            var firstElement = this.Peek();
            Swap(0, this.Size - 1);
            this.elements.RemoveAt(this.Size - 1);
            this.HeapifyDown();

            return firstElement;
        }

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

        private void HeapifyDown()
        {
            int index = 0;
            int leftChildInd = this.GetLeftChildInd(index);
            while (leftChildInd<this.Size&&IsLess(index,leftChildInd))
            {
                int indexToSwap = leftChildInd;
                int rightChildInd = this.GetRightChildInd(index);
                if (rightChildInd<this.Size 
                    && IsLess(indexToSwap, rightChildInd))
                {
                    indexToSwap = rightChildInd;
                }
                this.Swap(index, indexToSwap);
                index = indexToSwap;
                leftChildInd = this.GetLeftChildInd(index);
            }
        }

        private int GetRightChildInd(int index)
        {
            return index * 2 + 2;
        }

        private bool IsLess(int index, int leftChildInd)
        {
            return this.elements[index]
                .CompareTo(this.elements[leftChildInd]) < 0;
        }

        private int GetLeftChildInd(int index)
        {
            return index * 2 + 1;
        }

        private void HeapifyUp()
        {
            var currentInd = this.Size - 1;
            var parentInd = this.GetParentInd(currentInd);
            while (currentInd>0&&IsGreater(currentInd,parentInd))
            {
                Swap(currentInd, parentInd);
                currentInd = parentInd;
                parentInd = this.GetParentInd(currentInd);
            }
        }

        private void Swap(int childInd, int parentInd)
        {
            var temp = this.elements[childInd];
            this.elements[childInd] = this.elements[parentInd];
            this.elements[parentInd] = temp;
        }

        private bool IsGreater(int childInd, int parentInd)
        {
            return this.elements[childInd]
                .CompareTo(this.elements[parentInd]) > 0;
        }

        private int GetParentInd(int childInd)
        {
            return (childInd - 1) / 2;
        }

        private void EnsureNotEmpty()
        {
            if (this.elements.Count==0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
