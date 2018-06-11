using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4___Trees_and_Graphs
{
    public class TreeNode<T>
    {
        private T _value;
        private TreeNode<T> _left;
        private TreeNode<T> _right;

        public TreeNode(T item)
        {
            this._value = item;
        }

        public TreeNode() { }

        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public TreeNode<T> Left
        {
            get { return _left; }
            set { _left = value; }
        }

        public TreeNode<T> Right
        {
            get { return _right; }
            set { _right = value; }
        }
    }
}
