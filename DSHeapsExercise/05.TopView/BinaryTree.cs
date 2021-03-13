namespace _05.TopView
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        public BinaryTree(T value, BinaryTree<T> left, BinaryTree<T> right)
        {
            this.Value = value;
            this.LeftChild = left;
            this.RightChild = right;
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public List<T> TopView()
        {
            var topView = new SortedDictionary<int, KeyValuePair<T, int>>();
            this.FindTopViewDFS(this, 0, 1, topView);
            return topView.Values.Select(x => x.Key).ToList();
        }

        private void FindTopViewDFS(BinaryTree<T> current,
            int offset, int level, 
            SortedDictionary<int, KeyValuePair<T, int>> topView)
        {
            if (current==null)
            {
                return;
            }
            if (!topView.ContainsKey(offset))
            {
                topView
                    .Add(offset, new KeyValuePair<T, int>(current.Value, level));
            }
            if (level<topView[offset].Value)
            {
                topView[offset] = new KeyValuePair<T, int>(current.Value, level);
            }
            this.FindTopViewDFS(current.LeftChild, offset - 1, level + 1,topView);
            this.FindTopViewDFS(current.RightChild, offset + 1, level + 1, topView);

        }
    }
}
