namespace _02.LowestCommonAncestor
{
    using System;
    using System.Collections.Generic;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        public BinaryTree(
            T value,
            BinaryTree<T> leftChild,
            BinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
            if (this.RightChild != null)
            {
                this.RightChild.Parent = this;
            }
            if (this.LeftChild != null)
            {
                this.LeftChild.Parent = this;
            }
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public BinaryTree<T> Parent { get; set; }

        public T FindLowestCommonAncestor(T first, T second)
        {
            var firstList = new List<BinaryTree<T>>();
            var secondList = new List<BinaryTree<T>>();

            this.FindNodesDFS(this, first, firstList);
            this.FindNodesDFS(this, second, secondList);
            var firstNode = firstList[0];
            var secondNode = secondList[0];
            T parentToSearch = firstNode.Parent.Value;
            while (!parentToSearch.Equals(firstNode.Value)
                ||!parentToSearch.Equals(secondNode.Value))
            {
                if (!parentToSearch.Equals(firstNode.Value))
                {
                    firstNode = firstNode.Parent;
                }
                if (!parentToSearch.Equals(secondNode.Value))
                {
                    secondNode = secondNode.Parent;
                }
            }
            return firstNode.Value;
        }

        private void FindNodesDFS(BinaryTree<T> current,
            T value, List<BinaryTree<T>> list)
        {
            if (current == null)
            {
                return;
            }
            if (current.Value.Equals(value))
            {
                list.Add(current);
            }
            this.FindNodesDFS(current.LeftChild, value, list);
            this.FindNodesDFS(current.RightChild, value, list);
        }
    }
}
