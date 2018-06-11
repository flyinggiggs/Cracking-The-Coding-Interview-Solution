using System.Collections.Generic;
using System;

namespace Chapter2___Linked_Lists
{
    // Linked Lists
    class Program
    {
        static void Main(string[] args)
        {
            var node1 = new MyNode(1);
            var node11 = new MyNode(1);
            var node2 = new MyNode(2);
            var node3 = new MyNode(3);
            var node4 = new MyNode(4);
            var node33 = new MyNode(3);

            PushNode(node1, node4);
            PushNode(node1, node11);
            PushNode(node1, node3);
            PushNode(node1, node2);
            PushNode(node1, node33);

            //QuestionRemoveNode();
            //SolveNumber1(node1);
            //SolveNumber2(node1);
            //SolveNumber3(node1, node3);
            //SolveNumber4(node1, node2);
            SolveNumber5();
        }

        static void QuestionRemoveNode()
        {
            var myNode = new MyNode(1);
            PushNode(myNode, new MyNode(2));
            PushNode(myNode, new MyNode(3));

            var tempNode = myNode;
            do
            {
                Console.WriteLine("Node: " + tempNode._value);
            } while ((tempNode = tempNode._next) != null);

            var firstNode = DeleteNode(myNode, 1);

            Console.WriteLine("After delete.");

            do
            {
                Console.WriteLine("Node: " + firstNode._value);
            } while ((firstNode = firstNode._next) != null);

            Console.ReadLine();
        }

        static MyNode DeleteNode(MyNode root, int value)
        {
            var tempNode = root;
            while (tempNode._next != null)
            {
                if (tempNode._next._value == value)
                {
                    // Set the current node's next to the next's next node
                    // And then set the current node to the updated next node
                    tempNode._next = tempNode._next._next;
                }
                else
                {
                    tempNode = tempNode._next;
                }
            }

            if (root._value == value)
            {
                root = root._next;
            }

            return root;
        }

        static private void PushNode(MyNode root, MyNode newNode)
        {
            if (root._next == null)
            {
                root._next = newNode;
                return;
            }

            while (root._next != null)
            {
                root = root._next;
            }
            root._next = newNode;
        }

        /* Write code to remove duplicates from an unsorted linked list.
         * FOLLOW UP
         * How would you solve this problem if a temporary buffer is not allowed?
         */
        static void SolveNumber1(MyNode root)
        {
            // We need two pointers. 
            // First, the pointer which will point the current node to be compared
            // Second, the pointer which will point the node to iterate to compare
            //var nodeToBeCompared = MyNode

            var tempNode = root;
            do
            {
                Console.Write("Node:" + tempNode._value + " => ");
            } while ((tempNode = tempNode._next) != null);

            Console.WriteLine("After the loop...");
            RemoveDuplicateNodes(root);
        }

        private static void RemoveDuplicateNodes(MyNode root)
        {
            var currentNode = root;
            var nodeToCompare = root;

            while (currentNode != null)
            {
                while (nodeToCompare._next != null)
                {
                    // If they are the same, we should remove nodeToCompare from the linkedlist
                    // We set the next to the next's next, then we can compare the next's next value 
                    if (currentNode._value == nodeToCompare._next._value)
                        nodeToCompare._next = nodeToCompare._next._next;
                    else
                        nodeToCompare = nodeToCompare._next;
                }

                nodeToCompare = currentNode = currentNode._next;
            }

            do
            {
                Console.Write("Node:" + root._value + " => ");
            } while ((root = root._next) != null);

            Console.ReadLine();
        }

        /*
         *  Implement an algorithm to find the kth to last element of a singly linked list.
         */
        static void SolveNumber2(MyNode root)
        {
            int k = 3;
            var kElement = FindKthNode(root, k);

            Console.WriteLine("Node: " + kElement._value);
            Console.ReadLine();
        }

        private static MyNode FindKthNode(MyNode root, int k)
        {
            int count = 1;

            while (count != k)
            {
                root = root._next;
                count++;
            }

            return root;
        }

        /// <summary>
        /// Implement an algorithm to delete a node in the middle (i.e., any node but
        /// the first and last node, not ncessarily the exact middle) of a singly linked list, 
        /// given only access to that node
        /// </summary>
        static void SolveNumber3(MyNode root, MyNode rootToDelete)
        {
            var tempNode = root;
            do
            {
                Console.Write("Node:" + tempNode._value + " => ");
            } while ((tempNode = tempNode._next) != null);

            // We should overwrite the note to delete with its nextNode
            rootToDelete._value = rootToDelete._next._value;
            rootToDelete._next = rootToDelete._next._next;

            Console.WriteLine("After deletion.");

            var tempNode2 = root;
            do
            {
                Console.Write("Node:" + tempNode2._value + " => ");
            } while ((tempNode2 = tempNode2._next) != null);

            Console.ReadLine();
        }

        /// <summary>
        /// Write code to partition a linked list around a value x, such that all nodes less than x come
        /// before all nodes greater than or equal to x. If x is contained within the list, the values of x only need
        /// to be after the elements less than x(see below). The partition element x can appear anywhere in the
        /// "right partition"; it does not need to appear between the left and right partitions.
        /// Input: 3-> 5 -> 8 -> 5 -> 10 -> 2 -> 1 [partition = 5]
        /// Output: 3 -> 1 -> 2 -> 10 -> 5 -> 5 -> 8
        /// </summary>
        static void SolveNumber4(MyNode root, MyNode partition)
        {
            var tempNode = root;
            do
            {
                Console.Write("Node:" + tempNode._value + " => ");
            } while ((tempNode = tempNode._next) != null);
            Console.WriteLine($"After partition: {partition._value}");

            var newNode = DoPartition(root, partition);

            do
            {
                Console.Write("Node:" + newNode._value + " => ");
            } while ((newNode = newNode._next) != null);

            Console.ReadLine();
        }

        static private MyNode DoPartition(MyNode root, MyNode partition)
        {
            // Rather than shifting and swapping elements, we can
            // actually create two different linked listL: one for element less than x, and one for elements greater than or equal to x
            MyNode lessNode = null;
            MyNode lessNodeStart = null;
            MyNode greaterNode = null;
            MyNode greaterNodeStart = null;

            while (root != null)
            {
                Console.WriteLine($"root: {root._value}");
                if (root._value < partition._value)
                {
                    if (lessNodeStart == null)
                        lessNodeStart = lessNode = root;
                    else
                    {
                        lessNode._next = root;
                        lessNode = lessNode._next;
                    }
                }
                else
                {
                    if (greaterNodeStart == null)
                        greaterNodeStart = greaterNode = root;
                    else
                    {
                        greaterNode._next = root;
                        greaterNode = greaterNode._next;
                    }
                }

                root = root._next;
            }

            lessNode._next = greaterNodeStart;

            return lessNodeStart;
        }

        /// <summary>
        /// You have two numbers represented by a linked list, where each node contains a single digit.
        /// The digitas are stored in reverse order, such that the 1's digit is at the head of the list
        /// Write a function that adds the two numbers and returns the sumas a linked list
        /// Input (7 -> 1 -> 6) + (5 -> 9 -> 2). That is, 617 + 295.
        /// Ooutput:2 -> 1 -> 9. That is 912.
        /// Suppose the digits are stored in forward order. Repeat the above problem.
        /// Input (6 -> 1 -> 7) + (2 -> 9 -> 5). That is, 617 + 295.
        /// Ooutput:9 -> 1 -> 2. That is 912.
        /// </summary>
        /// <param name="root"></param>
        static void SolveNumber5()
        {
            // My solution: We need another new node for output linked list.
            // First, we go through two input linked lists
            // Everytime when we read each node of the two linked lists,
            // Do calculation value % 10 and value / 10.
            // value % 10 will be the value of a new node of output linkedlist and we pass value / 10  (if it is 1) with temp variable for next node 
            // which will be read in next node
            var rootNode1 = new MyNode(7);
            PushNode(rootNode1, new MyNode(1));
            PushNode(rootNode1, new MyNode(6));

            var rootNode2 = new MyNode(5);
            PushNode(rootNode2, new MyNode(9));
            PushNode(rootNode2, new MyNode(2));

            var sum = CalculateSum(rootNode1, rootNode2);

            do
            {
                Console.Write($"Node: {sum._value} =>");
            } while ((sum = sum._next) != null);

            Console.ReadLine();
        }

        private static MyNode CalculateSum(MyNode input1, MyNode input2)
        {
            MyNode outputStartNode = null;
            MyNode outputNextNode = null;
            int carrying = 0;

            while (input1 != null || input2 != null)
            {
                int tempSum = (input1?._value ?? 0) + (input2?._value ?? 0) + carrying;
                int remainder = tempSum % 10;

                // When this is the first node
                if (outputStartNode == null)
                {
                    outputNextNode = outputStartNode = new MyNode(remainder);
                }
                // If this is not the first output node, then we should make a new next node for output linked list
                // and set outputNextNode to outputNextNode._next
                else
                {
                    outputNextNode._next = new MyNode(remainder);
                    outputNextNode = outputNextNode._next;
                }

                input1 = input1?._next ?? null;
                input2 = input2?._next ?? null;
                carrying = tempSum / 10;
            }

            return outputStartNode;
        }
    }
}
