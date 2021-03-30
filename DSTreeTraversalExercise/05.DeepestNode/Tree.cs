namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        public Tree(T key, params Tree<T>[] children)
        {
            this.Key = key;
            this._children = new List<Tree<T>>();
            foreach (var child in children)
            {
                this._children.Add(child);
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }


        public IReadOnlyCollection<Tree<T>> Children
            => this._children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            this._children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
        }

        public string GetAsString()
        {
            StringBuilder result = new StringBuilder();
            int indent = 0;
            this.FindDFS(this, indent, result);
            return result.ToString().Trim();
        }

        public Tree<T> GetDeepestLeftomostNode()
        {
            var dictionary = new Dictionary<Tree<T>, int>();
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (!dictionary.ContainsKey(node))
                {
                dictionary.Add(node, 0);
                }
                foreach (var child in node.Children)
                {
                    if (!dictionary.ContainsKey(child))
                    {
                        dictionary.Add(child, 0);
                    }
                    var ancestorsCount = dictionary[node];
                    dictionary[child]=ancestorsCount+1;
                    queue.Enqueue(child);
                }
            }
            return dictionary.OrderByDescending(x => x.Value).First().Key;
        }

        public List<T> GetLeafKeys()
        {
            var result = new List<T>();
            this.FindNodes(this, result, IsLeaf);
            result.Sort();
            return result;
        }

        public List<T> GetMiddleKeys()
        {
            var result = new List<T>();
            this.FindNodes(this, result, IsMiddle);
            result.Sort();
            return result;
        }


        public List<T> GetLongestPath()
        {
            throw new NotImplementedException();
        }

        public List<List<T>> PathsWithGivenSum(int sum)
        {
            throw new NotImplementedException();
        }

        public List<Tree<T>> SubTreesWithGivenSum(int sum)
        {
            throw new NotImplementedException();
        }

        private void FindDFS(Tree<T> subTtree, int indent,
            StringBuilder result)
        {
            result.Append(' ', indent);
            result.AppendLine(subTtree.Key.ToString());

            foreach (var child in subTtree.Children)
            {
                FindDFS(child, indent + 2, result);
            }
        }

        private void FindNodes(Tree<T> tree, List<T> result,
            Func<Tree<T>, bool> IsSearchedNode)
        {
            foreach (var child in tree._children)
            {
                if (IsSearchedNode(child))
                {
                    result.Add(child.Key);
                }
                FindNodes(child, result, IsSearchedNode);
            }
        }

        private bool IsLeaf(Tree<T> tree)
        {
            return tree.Children.Count == 0;
        }
        private bool IsRoot(Tree<T> tree)
        {
            return tree.Parent == null;
        }
        private bool IsMiddle(Tree<T> tree)
        {
            return tree.Parent != null && tree.Children.Count > 0;
        }
    }
}
