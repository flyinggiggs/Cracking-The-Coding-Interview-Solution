using System;
using System.Collections.Generic;

namespace Chapter4___Trees_and_Graphs
{
    /// <summary>
    /// Binary Search Tree
    /// </summary>
    class BST<TNode>
    {
        /// <summary>
        /// The root node
        /// </summary>
        private TreeNode<TNode> _root;

        /// <summary>
        /// Generic Comparer
        /// </summary>
        private Comparer<TNode> _comparer;

        /// <summary>
        /// 
        /// </summary>
        public TreeNode<TNode> Root
        {
            get { return _root; }
            set { _root = value; }
        }

        /// <summary>
        /// Generic Comparer
        /// </summary>
        public Comparer<TNode> Comparer
        {
            get { return _comparer; }
            set { _comparer = value; }
        }

        /// <summary>
        /// Search for the node
        /// </summary>
        /// <param name="nodeToSearch"></param>
        /// <returns></returns>
        public void Search(TreeNode<TNode> nodeToSearch)
        {
            TreeNode<TNode> foundedNode = Search(_root, nodeToSearch);
            Console.WriteLine("Search for Node " + nodeToSearch.Value.ToString() + "!! Founded " + (foundedNode?.Value.ToString() ?? "Null"));
        }

        /// <summary>
        /// Search for the node
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="nodeToSearch"></param>
        /// <returns></returns>
        public TreeNode<TNode> Search(TreeNode<TNode> currentNode, TreeNode<TNode> nodeToSearch)
        {
            if (currentNode == null)
            {
                return currentNode;
            }

            int result = _comparer.Compare(currentNode.Value, nodeToSearch.Value);

            if (result == 0)
            {
                return currentNode;
            }
            else if (result > 0)
            {
                return Search(currentNode.Left, nodeToSearch);
            }
            else
            {
                return Search(currentNode.Right, nodeToSearch);
            }
        }

        /// <summary>
        /// Insert a new node
        /// </summary>
        /// <param name="nodeToInsert"></param>
        public void Insert(TreeNode<TNode> nodeToInsert)
        {
            _root = Insert(this._root, nodeToInsert);
        }

        /// <summary>
        /// Insert a new node
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="nodeToInsert"></param>
        /// <returns></returns>
        public TreeNode<TNode> Insert(TreeNode<TNode> currentNode, TreeNode<TNode> nodeToInsert)
        {
            // If the root is null (Which means this is child node of leaf)
            if (currentNode == null)
            {
                var newNode = new TreeNode<TNode>();
                newNode.Value = nodeToInsert.Value;
                return newNode;
            }

            int result = _comparer.Compare(currentNode.Value, nodeToInsert.Value);

            if (result == 0)
            {
                // they are equal - attempting to enter a duplicate - do nothing
                //currentNode = currentNode;
            }
            // If root > data
            else if (result > 0)
            {
                // current.Value > data, must add n to current's left subtree
                currentNode.Left = Insert(currentNode.Left, nodeToInsert);
            }
            // If root < data
            else if (result < 0)
            {
                // current.Value < data, must add n to current's right subtree
                currentNode.Right = Insert(currentNode.Right, nodeToInsert);
            }
            return currentNode;
        }

        public void Delete(TreeNode<TNode> nodeToDelete)
        {
            Delete(_root, nodeToDelete);
        }

        /// <summary>
        /// 1. First search for the node to delete
        /// 2. Once it finds the node, check
        ///   if the node has a. two child nodes -> find the nearest node in its child nodes(Use predecessor or successor because they are the nearest and they only have either one child or no child)
        ///                                          and replace the node with the nearest node
        ///                   b. one child node -> simply remove it and connect the parent to the child
        ///                   c. no child node -> simplye remove it
        /// </summary>
        /// <param name="root"></param>
        /// <param name="nodeToDelete"></param>
        public TreeNode<TNode> Delete(TreeNode<TNode> root, TreeNode<TNode> nodeToDelete)
        {
            // Base Case: If the tree is empty
            if (root == null)
            {
                return null;
            }

            var result = _comparer.Compare(root.Value, nodeToDelete.Value);

            // If this is the node to delete
            if (result == 0)
            {
                // If right node is null, set the root to root.left (If left is null as well, root will set to null)
                if (root.Right == null)
                {
                    root = root.Left;
                }
                // If left node is null, set the root to root.right
                else if (root.Left == null)
                {
                    root = root.Right;
                }
                // If there are two child nodes, then we should find the successor and replace the root with the successor
                else
                {
                    // Get the sucessor from the right subtree(The smallest node in the right subtree) 
                    var sucessor = GetSuccessor(root.Right);

                    // Delete the successor to replace the node 
                    root.Right = Delete(root.Right, sucessor);

                    // Set the root to successor
                    root.Value = sucessor.Value;
                }
            }
            // If the root node is bigger than the node to delete, go to the left child of the root
            else if (result > 0)
            {
                root.Left = Delete(root.Left, nodeToDelete);
            }
            // If the root node is less than the node to delete, go to the right child of the root
            else
            {
                root.Right = Delete(root.Right, nodeToDelete);
            }

            return root;
        }

        /// <summary>
        /// PreOrder Traversal
        /// </summary>
        public void PreOrder()
        {
            Console.WriteLine("Start PreOrder Traversal!");
            PreOrder(_root);
        }

        /// <summary>
        /// PreOrder Traversal
        /// </summary>
        /// <param name="root"></param>
        public void PreOrder(TreeNode<TNode> root)
        {
            if (root == null)
            {
                return;
            }

            Console.WriteLine("Node: " + root.Value);
            PreOrder(root.Left);
            PreOrder(root.Right);
        }

        /// <summary>
        /// PostOrder Traversal
        /// </summary>
        public void PostOrder()
        {
            Console.WriteLine("Start PostOrder Traversal!");
            PostOrder(_root);
        }

        /// <summary>
        /// PostOrder Traversal
        /// </summary>
        /// <param name="root"></param>
        public void PostOrder(TreeNode<TNode> root)
        {
            if (root == null)
            {
                return;
            }
            PostOrder(root.Left);
            PostOrder(root.Right);
            Console.WriteLine("Node: " + root.Value);
        }

        /// <summary>
        /// InOrder Traversal
        /// </summary>
        public void InOrder()
        {
            Console.WriteLine("Start InOrder Traversal!");
            InOrder(_root);
        }

        /// <summary>
        /// InOrder Traversal
        /// </summary>
        /// <param name="root"></param>
        public void InOrder(TreeNode<TNode> root)
        {
            if (root == null)
            {
                return;
            }

            InOrder(root.Left);
            Console.WriteLine("Node: " + root.Value);
            InOrder(root.Right);
        }

        /// <summary>
        /// Get the successor of the root
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private TreeNode<TNode> GetSuccessor(TreeNode<TNode> root)
        {
            while (root.Left != null)
            {
                root = root.Left;
            }

            return root;
        }
    }
}
