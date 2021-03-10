namespace _01.BSTOperations
{
    using System;
    using System.Collections.Generic;

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

        public int Count => this.Root.Count;

        public bool Contains(T element)
        {
            var current = this.Root;
            while (current != null)
            {
                if (IsLess(element, current.Value))
                {
                    current = current.LeftChild;
                }
                else if (IsGreater(element, current.Value))
                {
                    current = current.RightChild;
                }
                else if (AreEqual(element, current.Value))
                {
                    return true;
                }
            }
            return false;
        }

        public void Insert(T element)
        {
            var nodeToInsert = new Node<T>(element, null, null);
            if (this.Root == null)
            {
                this.Root = nodeToInsert;
                return;
            }

            var current = this.Root;
            this.InsertDFS(current, null, nodeToInsert);
        }


        public IAbstractBinarySearchTree<T> Search(T element)
        {
            var current = this.Root;
            while (current != null)
            {
                if (IsLess(element, current.Value))
                {
                    current = current.LeftChild;
                }
                else if (IsGreater(element, current.Value))
                {
                    current = current.RightChild;
                }
                else if (AreEqual(element, current.Value))
                {
                    break;
                }
            }
            return new BinarySearchTree<T>(current);
        }

        public void EachInOrder(Action<T> action)
        {
            this.EnsureNotEmpty();
            var current = this.Root;
            this.EachInOrderDFS(current, action);
        }

        public List<T> Range(T lower, T upper)
        {
            var result = new List<T>();
            var queue = new Queue<Node<T>>();
            queue.Enqueue(this.Root);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (IsValidInRange(lower, upper, current.Value))
                {
                    result.Add(current.Value);
                }
                if (current.LeftChild != null)
                {
                    queue.Enqueue(current.LeftChild);
                }
                if (current.RightChild != null)
                {
                    queue.Enqueue(current.RightChild);
                }
            }
            return result;
        }

        public void DeleteMin()
        {
            this.EnsureNotEmpty();
            var current = this.Root;
            Node<T> prev = null;
            if (this.Root.LeftChild == null)
            {
                this.Root = this.Root.RightChild;
            }
            else
            {
                while (current.LeftChild != null)
                {
                    //decrease down on levels
                    current.Count--;
                    prev = current;
                    current = current.LeftChild;
                }
                prev.LeftChild = current.RightChild;
            }
            //if (current.RightChild!=null)
            //{
            //    current = current.RightChild;
            //    prev.LeftChild = current;
            //    current.RightChild = null;
            //}
            //else
            //{
            //    current = null;
            //    prev.LeftChild = null;
            //}
        }

        public void DeleteMax()
        {
            this.EnsureNotEmpty();
            var current = this.Root;
            Node<T> prev = null;
            if (this.Root.RightChild == null)
            {
                this.Root = this.Root.LeftChild;
            }
            else
            {
                while (current.RightChild != null)
                {
                    current.Count--;
                    prev = current;
                    current = current.RightChild;
                }
                prev.RightChild = current.LeftChild;
            }
        }

        public int GetRank(T element)
        {
            return GetRankDFS(this.Root, element);
        }

        private int GetRankDFS(Node<T> current, T element)
        {
            if (current == null)
            {
                return 0;
            }
            if (this.IsLess(element, current.Value))
            {
                return this.GetRankDFS(current.LeftChild, element);
            }
            else if (this.AreEqual(element, current.Value))
            {
                return this.GetNodeCount(current);
            }
            return this.GetNodeCount(current.LeftChild) + 1
                + this.GetRankDFS(current.RightChild, element);
        }

        private int GetNodeCount(Node<T> current)
        {
            if (current == null)
            {
                return 0;
            }
            return current.Count;
        }
        private bool IsValidInRange(T lower, T upper, T value)
        {
            if (IsLess(value, lower))
            {
                return false;
            }
            if (IsGreater(value, upper))
            {
                return false;
            }
            return true;
        }

        private void EachInOrderDFS(Node<T> current, Action<T> action)
        {
            if (current != null)
            {
                this.EachInOrderDFS(current.LeftChild, action);
                action.Invoke(current.Value);
                this.EachInOrderDFS(current.RightChild, action);
            }
        }

        private void InsertDFS(Node<T> current,
           Node<T> prev, Node<T> nodeToInsert)
        {
            if (current == null && this.IsLess(nodeToInsert.Value, prev.Value))
            {
                prev.LeftChild = nodeToInsert;
                if (this.LeftChild == null)
                {
                    this.LeftChild = nodeToInsert;
                }
                return;
            }
            if (current == null && this.IsGreater(nodeToInsert.Value, prev.Value))
            {
                prev.RightChild = nodeToInsert;
                if (this.RightChild == null)
                {
                    this.RightChild = nodeToInsert;
                }
                return;
            }
            if (IsLess(nodeToInsert.Value, current.Value))
            {
                InsertDFS(current.LeftChild, current, nodeToInsert);
                current.Count++;
            }
            else if (IsGreater(nodeToInsert.Value, current.Value))
            {
                InsertDFS(current.RightChild, current, nodeToInsert);
                current.Count++;
            }
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

        private void EnsureNotEmpty()
        {
            if (this.Root == null)
            {
                throw new InvalidOperationException();
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
