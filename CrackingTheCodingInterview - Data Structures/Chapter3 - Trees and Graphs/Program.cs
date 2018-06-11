using System;
using System.Collections.Generic;
using System.Linq;

namespace Chapter3___Trees_and_Graphs
{
    public class Program
    {
        static void Main(string[] args)
        {
            //SolveNumber1();
            //SolveNumber2();
            SolveNumber3();            
            //SolveNumber4();
        }

        /* 
         * Q: Describe how you could use a single array to implement three stacks
         */
        static void SolveNumber1()
        {
            /* Use a remainder of division. => Remainder of 10 divided by 3 is 1 
             * 1. Make a huge array with fixed size
             * 2. The first stack is corresponding to indexes of array which is index % 3 == 0
             * 3. The second stack is corresponding to indexes of array which is index % 3 == 1
             * 4. The third stack is corresponding to indexes of array which is index % 3 == 2
             * If the element of array is already full, then make a new array with doubled size
             * Push, Pop, Peek
             */

            var stacks = new int?[10];
            var stackPointer = new int[] { 0, 1, 2 };

            Push(1, ref stacks, 1, stackPointer);
            Push(1, ref stacks, 2, stackPointer);
            Push(1, ref stacks, 3, stackPointer);
            Push(1, ref stacks, 4, stackPointer);
            Push(1, ref stacks, 5, stackPointer);

            foreach (var e in stacks.ToList())
                Console.WriteLine("Stack: " + e);

            Console.WriteLine("Pop: " + Pop(1, stackPointer, stacks));

            Console.ReadLine();

        }
        
        /*
         Q: How would you design a stack which, in addition to push and pop, has a function min
            which returns the minimum element? Push, pop and min should all operate in O(1) time.
             */
        static void SolveNumber2()
        {
            var minStack = new MinStack();
            minStack.Push(3);
            minStack.Push(1);
            minStack.Push(7);
            minStack.Pop();
            minStack.Pop();

            Console.WriteLine("Min: " + minStack.Min());
            Console.ReadLine();
        }

        /// <summary>
        /// Imagine a (literal) stack of plates.
        /// </summary>
        static void SolveNumber3()
        {

        }

        /* 
         * Q: Implement a MyQueue class which implements a queue using two stacks.
         */
        public static void SolveNumber4()
        {
            var myQueue = new MyQueue();

            myQueue.Add(1);
            myQueue.Add(2);
            myQueue.Add(3);

            Console.WriteLine("Peek: " + myQueue.Peek());
            Console.WriteLine("Removed the first element." + myQueue.Remove());
            Console.WriteLine("Peek: " + myQueue.Peek());
            Console.ReadKey();
        }

        #region Private methods

        private static void Push(int stackNumber, ref int?[] stacks, int value, int[] stackPointer)
        {
            if (stackNumber == 1)
            {
                OperatePush(stackNumber, stackPointer, ref stacks, value);
            }
            else if (stackNumber == 2)
            {
                // index % 3 == 1
                OperatePush(stackNumber, stackPointer, ref stacks, value);
            }
            else if (stackNumber == 3)
            {
                OperatePush(stackNumber, stackPointer, ref stacks, value);
            }
            else
            {
                throw new Exception();
            }
        }

        private static void OperatePush(int stackNumber, int[] stackPointer, ref int?[] stacks, int value)
        {
            while (stacks[stackPointer[stackNumber - 1]].HasValue)
            {
                if (stackPointer[stackNumber - 1] + 3 > stacks.Length)
                {
                    //Create a new array and copy over
                    var stacksToCopy = new int?[stacks.Length * 2];
                    stacks.CopyTo(stacksToCopy, 0);
                    stacks = stacksToCopy;
                }

                stackPointer[stackNumber - 1] += 3;
            }

            //stackPointer[stackNumber] += 3;
            stacks[stackPointer[stackNumber - 1]] = value;
        }

        private static int Pop(int stackNumber, int[] stackPointer, int?[] stacks)
        {
            if (stackPointer[stackNumber - 1] < 0)
            {
                return 0;
            }

            var currentPointer = stackPointer[stackNumber - 1];
            stackPointer[stackNumber - 1] -= 3;
            return stacks[currentPointer].Value;
        }

        #endregion

        #region Class

        public class MinStack
        {
            private Stack<int> _minStack = new Stack<int>();
            private Stack<int> _realStack = new Stack<int>();

            public int Pop()
            {
                var lastElement = _realStack.Pop();

                if (Comparer<int>.Default.Compare(lastElement, Min()) == 0)
                    _minStack.Pop();

                return lastElement;
            }

            public void Push(int item)
            {
                if (Comparer<int>.Default.Compare(item, Min()) < 0)
                    _minStack.Push(item);

                _realStack.Push(item);
            }

            public int Min()
            {
                if (!_minStack.Any())
                    return int.MaxValue;

                return _minStack.Peek();
            }
        }

        public class MyQueue
        {
            Stack<int> stackA = new Stack<int>();
            Stack<int> stackB = new Stack<int>();
            int top;

            public void Add(int item)
            {
                if (!stackA.Any())
                    top = item;

                // Simply add all items to stackA
                stackA.Push(item);
            }

            public int Remove()
            {
                while(stackA.Any())
                {
                    stackB.Push(stackA.Pop());
                }

                var itemToRemove = stackB.Pop();
                this.top = stackB.Peek();

                while(stackB.Any())
                {
                    stackA.Push(stackB.Pop());
                }

                return itemToRemove;
            }

            public int Peek()
            {
                return top;
            }
        }

        #endregion Class
    }
}
