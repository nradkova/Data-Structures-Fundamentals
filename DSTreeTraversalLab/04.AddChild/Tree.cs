namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class Tree<T> : IAbstractTree<T>
    {
        private bool IsRootDeleted = false;
        private readonly List<Tree<T>> _children;

        public Tree(T value)
        {
            this.Value = value;
            this.Parent = null;
            this._children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (var child in children)
            {
                child.Parent = this;
                this._children.Add(child);
            }
        }

        public T Value { get; private set; }

        public Tree<T> Parent { get; private set; }

        public IReadOnlyCollection<Tree<T>> Children
            => this._children.AsReadOnly();


        public ICollection<T> OrderBfs()
        {
            var result = new List<T>();
            if (IsRootDeleted)
            {
                return result;
            }
            var queue = new Queue<Tree<T>>();
            var node = this;
            queue.Enqueue(node);
            while (queue.Count > 0)
            {
                var subTree = queue.Dequeue();
                result.Add(subTree.Value);
                foreach (var child in subTree.Children)
                {
                    queue.Enqueue(child);
                }
            }
            return result;
        }

        public ICollection<T> OrderDfs()
        {
            var result = new List<T>();
            if (IsRootDeleted)
            {
                return result;
            }
            OrderDfs(this, result);

            return result;
        }

        public void AddChild(T parentKey, Tree<T> child)
        {
            var node = FindBFS(parentKey);
            if (node == null)
            {
                throw new ArgumentNullException();
            }
            node._children.Add(child);
        }
       
        public void RemoveNode(T nodeKey)
        {
            var node = FindBFS(nodeKey);
            if (node==null)
            {
                throw new ArgumentNullException();
            }

            foreach (var child in node.Children)
            {
                child.Parent = null;
            }
            Tree<T> nodeParent = node.Parent;
            if (nodeParent==null)
            {
                this.IsRootDeleted = true;
            }
            else
            {
                nodeParent._children.Remove(node);
            }
            node.Value = default;
        }

        public void Swap(T firstKey, T secondKey)
        {
            var first = FindBFS(firstKey);
            var second = FindBFS(secondKey);
            if (first==null||second==null)
            {
                throw new ArgumentNullException();
            }
            int firstInd = first.Parent._children.IndexOf(first);
            int secondInd = second.Parent._children.IndexOf(second);
            var firstParent = second.Parent;
            var secondParent = first.Parent;
            firstParent._children[firstInd] = first;
            secondParent._children[secondInd] = second;
        }

        private void OrderDfs(Tree<T> tree, List<T> result)
        {
            foreach (var child in tree.Children)
            {
                OrderDfs(child, result);
            }
                result.Add(tree.Value);
        }

        private Tree<T> FindBFS(T parentKey)
        {
            var queue = new Queue<Tree<T>>();
            var node = this;
            queue.Enqueue(node);
            while (queue.Count > 0)
            {
                var subTree = queue.Dequeue();
                if (subTree.Value.Equals(parentKey))
                {
                    return subTree;
                }
                foreach (var child in subTree.Children)
                {
                    queue.Enqueue(child);
                }
            }
            return null;
        }
    }
}
