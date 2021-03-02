namespace _01.BinaryTree
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
    {
        public BinaryTree(T value
            , IAbstractBinaryTree<T> leftChild
            , IAbstractBinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
        }

        public T Value { get; private set; }

        public IAbstractBinaryTree<T> LeftChild { get; private set; }

        public IAbstractBinaryTree<T> RightChild { get; private set; }

        public string AsIndentedPreOrder(int indent)
        {
            StringBuilder result = new StringBuilder();
            this.AsIndentedPreOrder(this, result, indent);
            return result.ToString();
        }

        private void AsIndentedPreOrder(IAbstractBinaryTree<T> binaryTree,
            StringBuilder result, int indent)
        {
            result
                .AppendLine($"{new string(' ', indent)}{binaryTree.Value}");
            if (binaryTree.LeftChild != null)
            {
                this.AsIndentedPreOrder(binaryTree.LeftChild, result, indent + 2);
            }
            if (binaryTree.RightChild != null)
            {
                this.AsIndentedPreOrder(binaryTree.RightChild, result, indent + 2);
            }
        }

        public List<IAbstractBinaryTree<T>> InOrder()
        {
            var inOrdered = new List<IAbstractBinaryTree<T>>();
            if (this.LeftChild != null)
            {
                inOrdered.AddRange(this.LeftChild.InOrder());
            }
            inOrdered.Add(this);
            if (this.RightChild != null)
            {
                inOrdered.AddRange(this.RightChild.InOrder());
            }
            return inOrdered;
        }

        public List<IAbstractBinaryTree<T>> PostOrder()
        {
            var postOrdered = new List<IAbstractBinaryTree<T>>();
            if (this.LeftChild != null)
            {
                postOrdered.AddRange(this.LeftChild.PostOrder());
            }
            if (this.RightChild != null)
            {
                postOrdered.AddRange(this.RightChild.PostOrder());
            }
            postOrdered.Add(this);
            return postOrdered;
        }

        public List<IAbstractBinaryTree<T>> PreOrder()
        {
            var preOrdered = new List<IAbstractBinaryTree<T>>();
            preOrdered.Add(this);
            if (this.LeftChild != null)
            {
                preOrdered.AddRange(this.LeftChild.PreOrder());
            }
            if (this.RightChild != null)
            {
                preOrdered.AddRange(this.RightChild.PreOrder());
            }
            return preOrdered;
        }

        public void ForEachInOrder(Action<T> action)
        {
            if (this.LeftChild != null)
            {
                this.LeftChild.ForEachInOrder(action);
            }
            action.Invoke(this.Value);
            if (this.RightChild != null)
            {
                this.RightChild.ForEachInOrder(action);
            }
        }
    }
}
