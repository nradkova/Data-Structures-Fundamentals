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
            throw new NotImplementedException();
        }

        public List<T> GetLeafKeys()
        {
            var result = new List<T>();
            this.FindDFSLeaves(this,result);
            result.Sort();
            return result;
        }
       
        public List<T> GetMiddleKeys()
        {
            var result = new List<T>();
            this.FindDFSMiddles(this, result);
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

        private void FindDFSLeaves(Tree<T> tree, List<T> result)
        {
            foreach (var child in tree._children)
            {
                if (child.Children.Count == 0)
                {
                    result.Add(child.Key);
                }
                FindDFSLeaves(child, result);
            }
        }

        private void FindDFSMiddles(Tree<T> tree, List<T> result)
        {
            foreach (var child in tree.Children)
            {
                if (child.Children.Count > 0)
                {
                    result.Add(child.Key);
                    FindDFSMiddles(child, result);
                }
            }
        }

    }
}
