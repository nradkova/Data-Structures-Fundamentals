﻿namespace _04.BinarySearchTree
{
    using System;

    public class BinarySearchTree<T> : IAbstractBinarySearchTree<T>
        where T : IComparable<T>
    {
        public BinarySearchTree()
        {
        }

        public BinarySearchTree(Node<T> root)
        {
            this.Copy(root);
        }

        public Node<T> Root { get; private set; }

        public Node<T> LeftChild { get; private set; }

        public Node<T> RightChild { get; private set; }

        public T Value => this.Root.Value;

        public bool Contains(T element)
        {
            var current = this.Root;
            while (current != null)
            {
                if (this.IsLess(element, current.Value))
                {
                    current = current.LeftChild;
                }
                else if (this.IsGreater(element, current.Value))
                {
                    current = current.RightChild;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public void Insert(T element)
        {
            var elementToInsert 
                = new Node<T>(element, null, null);
            if (this.Root == null)
            {
                this.Root = elementToInsert;
            }
            else
            {
                var current = this.Root;
                Node<T> prev = null;
                while (current != null)
                {
                    prev = current;
                    if (this.IsLess(element, current.Value))
                    {
                        current = current.LeftChild;
                    }
                    else if (this.IsGreater(element, current.Value))
                    {
                        current = current.RightChild;
                    }
                    else if (this.AreEqual(element, current.Value))
                    {
                        return;
                    }
                }
                if (this.IsLess(element, prev.Value))
                {
                    prev.LeftChild = elementToInsert;
                    if (this.LeftChild == null)
                    {
                        this.LeftChild = elementToInsert;
                    }
                }
                else
                {
                    prev.RightChild = elementToInsert;
                    if (this.RightChild == null)
                    {
                        this.RightChild = elementToInsert;
                    }
                }
            }
        }

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            var current = this.Root;
            while (current!=null 
                &&!this.AreEqual(current.Value,element))
            {
                if (this.IsLess(element, current.Value))
                {
                    current = current.LeftChild;
                }
                else if (this.IsGreater(element, current.Value))
                {
                    current = current.RightChild;
                }
            }
            return new BinarySearchTree<T>(current);
        }

        private void Copy(Node<T> root)
        {
            if (root != null)
            {
                this.Insert(root.Value);
                this.Copy(root.LeftChild);
                this.Copy(root.RightChild);

            }
        }

        private bool IsLess(T element, T value)
        {
            return element.CompareTo(value) < 0;
        }

        private bool IsGreater(T element, T value)
        {
            return element.CompareTo(value) > 0;
        }

        private bool AreEqual(T element, T value)
        {
            return element.CompareTo(value) == 0;
        }
    }
}
