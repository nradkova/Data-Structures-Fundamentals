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
                    dictionary[child] = ancestorsCount + 1;
                    queue.Enqueue(child);
                }
            }
            return dictionary.OrderByDescending(x => x.Value).First().Key;
        }

        public List<T> GetLeafKeys()
        {
            var result = new List<T>();
            this.FindCertainNodes(this, result, IsLeaf);
            result.Sort();
            return result;
        }

        public List<T> GetMiddleKeys()
        {
            var result = new List<T>();
            this.FindCertainNodes(this, result, IsMiddle);
            result.Sort();
            return result;
        }


        public List<T> GetLongestPath()
        {
            var node = this.GetDeepestLeftomostNode();
            return FindAncestors(node);
        }

        public List<List<T>> PathsWithGivenSum(int sum)
        {
            var result = new List<List<T>>();
            var path = new List<T>();
            path.Add(this.Key);
            int currentSum = Convert.ToInt32(this.Key);
            this.FindPathsWithGivenSum(sum, result, path, this, ref currentSum);
            return result;
        }

        public List<Tree<T>> SubTreesWithGivenSum(int sum)
        {
            var nodes = new List<Tree<T>>();
            var allNodes = new List<Tree<T>>();
            allNodes.Add(this);
            allNodes = this.FindAllNodes(this, nodes);
            var result = new List<Tree<T>>();
            foreach (var node in allNodes)
            {
                var currentSum = Convert.ToInt32(node.Key)
                    + this.GetSubtreeSum(node, 0);
                if (currentSum == sum)
                {
                    result.Add(node);
                }
            }
            return result;
        }

        private int GetSubtreeSum(Tree<T> node,int sum)
        {
            foreach (var child in node.Children)
            {
                sum += Convert.ToInt32(child.Key);
                GetSubtreeSum(child, sum);
            }
            return sum;
        }

        private List<Tree<T>> FindAllNodes(Tree<T> tree, List<Tree<T>> nodes)
        {
            foreach (var child in tree.Children)
            {
                nodes.Add(child);
                FindAllNodes(child, nodes);
            }
            return nodes;
        }

        private void FindPathsWithGivenSum(int sum, List<List<T>> result,
           List<T> path, Tree<T> tree, ref int currentSum)
        {
            foreach (var child in tree.Children)
            {
                path.Add(child.Key);
                currentSum += Convert.ToInt32(child.Key);
                FindPathsWithGivenSum(sum, result, path, child, ref currentSum);
            }

            if (sum == currentSum)
            {
                result.Add(new List<T>(path));
            }

            path.Remove(tree.Key);
            currentSum -= Convert.ToInt32(tree.Key);
        }

        private List<T> FindAncestors(Tree<T> tree)
        {
            var result = new Stack<T>();
            result.Push(tree.Key);
            var parent = tree.Parent;
            while (parent != null)
            {
                result.Push(parent.Key);
                parent = parent.Parent;
            }

            return result.ToList();
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

        private void FindCertainNodes(Tree<T> tree, List<T> result,
            Func<Tree<T>, bool> IsSearchedNode)
        {
            foreach (var child in tree.Children)
            {
                if (IsSearchedNode(child))
                {
                    result.Add(child.Key);
                }
                FindCertainNodes(child, result, IsSearchedNode);
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
