using System;
using System.Collections.Generic;
using System.Linq;


namespace Chapter4___Trees_and_Graphs
{
    class Program
    {
        static void Main(string[] args)
        {
            //var treeToPractice = PracticeTree<BST<int>>();

            //SolveNumber1();
            SolveNumber2();
            //SolveNumber4(treeToPractice);
            //SolutionNumber4(treeToPractice);
        }

        /// <summary>
        /// Tree implementations.
        /// 1. Insert
        /// 2. Remove
        /// 3. Search
        /// </summary>
        static Ttree PracticeTree<Ttree>()
        {
            var binarySearchTree = new BST<int>() { Comparer = Comparer<int>.Default };

            /*
                      10
                     /  \
                    5    20
                   / \
                  2   6
                   \
                    3
            */
            var nodeA = new TreeNode<int>() { Value = 10 };
            var nodeB = new TreeNode<int>() { Value = 20 };
            var nodeC = new TreeNode<int>() { Value = 5 };
            var nodeD = new TreeNode<int>() { Value = 6 };
            var nodeE = new TreeNode<int>() { Value = 2 };
            var nodeF = new TreeNode<int>() { Value = 3 };

            binarySearchTree.Insert(nodeA);
            binarySearchTree.Insert(nodeB);
            binarySearchTree.Insert(nodeC);
            binarySearchTree.Insert(nodeD);
            binarySearchTree.Insert(nodeE);
            binarySearchTree.Insert(nodeF);

            binarySearchTree.Search(nodeD);

            //binarySearchTree.Delete(nodeA);
            //binarySearchTree.Delete(nodeD);

            binarySearchTree.Search(nodeD);

            binarySearchTree.PreOrder();
            binarySearchTree.InOrder();
            binarySearchTree.PostOrder();

            Console.ReadLine();

            return (Ttree)(object)binarySearchTree;
        }

        /// <summary>
        /// Give a directed graph, design an algorithm to find out whether there is a route between two nodes.
        /// </summary>
        /// <param name="tree"></param>
        static void SolveNumber1()
        {
            // Make a graph with 1. two dimension arrays or 2. Linked List
            // Then we can do simple graph traversal DFS or BFS.
            var graph = new Graph<int>();

            var node1 = new GraphNode<int>(1);
            var node2 = new GraphNode<int>(2);
            var node3 = new GraphNode<int>(3);
            var node4 = new GraphNode<int>(4);
            var node5 = new GraphNode<int>(5);
            var node6 = new GraphNode<int>(6);
            var node7 = new GraphNode<int>(7);

            graph.AddNode(node1);
            graph.AddNode(node2);
            graph.AddNode(node3);
            graph.AddNode(node4);
            graph.AddNode(node5);
            graph.AddNode(node6);
            graph.AddNode(node7);

            graph.AddDirectedEdge(node1, node2, 1);
            graph.AddDirectedEdge(node1, node3, 2);
            graph.AddDirectedEdge(node2, node3, 3);
            graph.AddDirectedEdge(node2, node4, 4);
            graph.AddDirectedEdge(node3, node5, 5);
            graph.AddDirectedEdge(node3, node6, 6);

            if (BreadthFirstSearch(graph, node1, node7))
                Console.WriteLine($"BST Route from Node: {node1.Data} to Node: {node7.Data} exists.");
            else
                Console.WriteLine($"BST Route from Node: {node1.Data} to Node: {node7.Data} does NOT exist.");

            if (DepthFirstSearch(graph, node1, node4))
                Console.WriteLine($"DST Route from Node: {node1.Data} to Node: {node4.Data} exists.");
            else
                Console.WriteLine($"DST Route from Node: {node1.Data} to Node: {node4.Data} does NOT exist.");

            Console.ReadLine();
        }

        static bool BreadthFirstSearch(Graph<int> graph, GraphNode<int> start, GraphNode<int> end)
        {
            // First we should set all nodes' state to NotVisited
            graph.Nodes.ForEach(n => n.State = GraphNode<int>.VisitState.NotVisited);

            // Initiate a queue for Breadth First Search
            var nodeQueue = new Queue<GraphNode<int>>();
            nodeQueue.Enqueue(start);

            while (nodeQueue.Any())
            {
                var queuedNode = nodeQueue.Dequeue();
                queuedNode.State = GraphNode<int>.VisitState.Visited;
                Console.WriteLine($"Current Node: {queuedNode.Data}");

                foreach (var neighborNode in queuedNode.Neighbors)
                {
                    Console.WriteLine($"Current Neighbor Node: {neighborNode.Data}");
                    // If we find the end, just return true!
                    if (neighborNode.Data == end.Data)
                        return true;
                    // Otherwise, we should eunque if the neighbor node has not been visited yet.
                    else
                    {
                        if (neighborNode.State == GraphNode<int>.VisitState.NotVisited)
                            nodeQueue.Enqueue(neighborNode);
                    }
                }
            }
            // We should return false, if end has not been found
            return false;
        }

        static bool DepthFirstSearch(Graph<int> graph, GraphNode<int> start, GraphNode<int> end)
        {
            // Fist of all, we should set State of graphNode to NotVisited
            graph.Nodes.ForEach(n => n.State = GraphNode<int>.VisitState.NotVisited);

            // Instantiate a stack for DFS and push the start node
            var nodeStack = new Stack<GraphNode<int>>();
            nodeStack.Push(start);

            // If stack has any nodes
            while (nodeStack.Any())
            {
                //Pop the node and set it to Visited
                var popedNode = nodeStack.Pop();
                popedNode.State = GraphNode<int>.VisitState.Visited;
                Console.WriteLine($"Current Node: {popedNode.Data}.");

                // Loop through neighbor nodes
                foreach (var neighborNode in popedNode.Neighbors)
                {
                    Console.WriteLine($"Neighbor Node: {neighborNode.Data}.");
                    if (neighborNode.Data == end.Data)
                        return true;
                    // If this neighbor node is not the end, we should push this neighbor node if this has not been visitedf.
                    else
                    {
                        if (neighborNode.State == GraphNode<int>.VisitState.NotVisited)
                            nodeStack.Push(neighborNode);
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Given a sorted (increasing order) array with unique integer elements, write an algorithm
        /// to create a binary search tree with minimal height
        /// </summary>
        static void SolveNumber2()
        {
            int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            //int medium = array.Length % 2 == 0 ? array.Length / 2 : (array.Length / 2) + 1;

            var tree = new BST<int>() { Comparer = Comparer<int>.Default };

            // 1. The medium should be the root so that a tree can have two branches. Each branch will have a splitted array
            // 2. With generated two branches(two splitted arrays), we should find a medium of each branch and make them as left and right child nodes
            // 3. Repeat find the minimum until medium is the only element of given array

            AddNode(array, 0, array.Length - 1);
            Console.ReadKey();
        }

        static void AddNode(int[] array, int start, int end)
        {

        }

        /// <summary>
        /// Imiplement a function to check if a binary tree is balanced. For the purposes of this question,
        /// a balanced tree is defined to be a tree such that the heights of the two subtress of any node never differ
        /// by more than one.
        /// </summary>
        static void SolveNumber4(BST<int> tree)
        {
            // Treverse to all leaves and check the difference between their heights
            // Going to use Dictionary<int (node id), int (height)>
            // When it arrvies a leaf, check if values of dictionary and if there is any difference Math.Abs(value - (leaf.value)) > 1, 
            // Yes, then return false and terminate traversing
            // No, then return true and continue traversing.

            Search(tree.Root, new Dictionary<int, int>(), 1);
            Console.ReadLine();

        }

        private static TreeNode<int> Search(TreeNode<int> root, Dictionary<int, int> dictionary, int currentHeight)
        {
            // This is the leaf
            if (root == null)
            {
                return null;
            }

            var leftReturnedTree = Search(root.Left, dictionary, currentHeight + 1);
            var rightReturnedTree = Search(root.Right, dictionary, currentHeight + 1);

            if (leftReturnedTree == null && rightReturnedTree == null)
            {
                if (IsMoreThanOne(dictionary, currentHeight))
                {
                    Console.WriteLine("Unbalanced detected!");
                }

                if (!dictionary.ContainsKey(root.Value))
                {
                    dictionary[root.Value] = currentHeight;
                }
            }

            return new TreeNode<int>();
        }

        private static bool IsMoreThanOne(Dictionary<int, int> dictionary, int valueToCompare)
        {
            var differences = dictionary.Values.FirstOrDefault(a => Math.Abs(a - valueToCompare) > 1);

            if (differences != 0)
            {
                Console.WriteLine("Unbalanced: " + differences + " with the height: " + valueToCompare);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 1. Briefly, we can get the height of each node from leaves and compare them.
        /// </summary>
        /// <param name="tree"></param>
        static void SolutionNumber4(BST<int> tree)
        {
            if (IsBalanced(tree.Root))
                Console.WriteLine("Balanced!");
            else
                Console.WriteLine("Unbalanced!");

            Console.ReadLine();
        }

        private static bool IsBalanced(TreeNode<int> root)
        {
            return CheckHeight(root) != int.MinValue;
        }

        private static int CheckHeight(TreeNode<int> root)
        {
            if (root == null)
            {
                return -1;
            }

            int leftHeight = CheckHeight(root.Left);
            if (leftHeight == int.MinValue)
            {
                return int.MinValue;
            }

            int rightHeight = CheckHeight(root.Right);
            if (rightHeight == int.MinValue)
            {
                return int.MinValue;
            }

            int heightDiff = leftHeight - rightHeight;

            if (Math.Abs(heightDiff) > 1)
            {
                return int.MinValue;
            }
            else
            {
                return Math.Max(leftHeight, rightHeight) + 1;
            }
        }
    }
}
