using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter3___Dynamic_Programming
{
    class Program
    {
        static void Main(string[] args)
        {
            //QuestionOne();
            //QuestionSeven(); // Solve it again

            //QuestionIntPermutation();
            QuestionFibonacci();
        }

        static void QuestionIntPermutation()
        {
            string input = "123";

            StartPermutation(input, 0, input.Length-1);
            Console.ReadLine();
        }

        static void QuestionFibonacci()
        {
            int input = 45;
            var cachedValues = new int[input];

            Console.WriteLine(StartFibonacci(input-1, cachedValues));
            Console.ReadLine();
        }

        static int StartFibonacci(int input, int[] cachedValues)
        {
            if (input == 0 || input == 1)
                return 1;


            if (cachedValues[input] == 0)
                cachedValues[input] = StartFibonacci(input - 1, cachedValues) + StartFibonacci(input - 2, cachedValues);

            return cachedValues[input];

            //return StartFibonacci(input - 1, cachedValues) + StartFibonacci(input - 2, cachedValues);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="l">Start index</param>
        /// <param name="r"></param>
        /// <returns></returns>
        static void StartPermutation(string input, int l, int r)
        {                 
            if(l==r)
            {
                Console.WriteLine(input);
            }
            for (int i = l; i <= r; i++)
            {
                input = Swap(input, l, i);
                StartPermutation(input, l + 1, r);
                input = Swap(input, l, i);
            }
        }
        static private string Swap(string input, int index1, int index2)
        {
            var charArray = input.ToCharArray();

            char temp = charArray[index1];
            charArray[index1] = charArray[index2];
            charArray[index2] = temp;

            return new string(charArray);
        }

        /// <summary>
        /// A child is running up a staircase with n steps and can hop either 1 step, 2 steps, or 3 steps at a time.
        /// Implement a method to count how many possible ways the child can run up the stairs.
        /// Solution page: 355
        /// </summary>
        static void QuestionOne()
        {
            // Base case: when only 3 steps left. There are 3 ways to hop 3 steps.
            // 1step * 3, 1 step * 1 + 2 step * 1, 3 step * 1
            Console.WriteLine("Possible ways: "+ FindPossibleWays(4, Enumerable.Repeat<int>(-1, 4).ToArray()));
            //Console.WriteLine("Possible ways: " + BrutalForceFindWays(50));
            Console.ReadLine();

        }

        /// <summary>
        /// Write a method to return all subsets of a set.
        /// </summary>
        static void QuestionFour()
        {
            var input = new int[] { 0, 1, 2, 3, 4, 5 };
            
             
        }


        /// <summary>
        /// Write a method to compute all permutations of a string of unique characters. 
        /// </summary>
        static void QuestionSeven()
        {
            string input = "123";

            // Get all unique charcaters
            var charMap = new Dictionary<char, bool>();
            foreach(char c in input)
            {
                if(!charMap.ContainsKey(c))
                {
                    charMap[c] = true;
                }
            }

            string uniqueString = string.Join("", charMap.Keys);

            GetPermutations(uniqueString);
            
            Console.ReadLine();
        }

        private static void GetPermutations(string input)
        {
            //oreach (char c in input)
                Console.WriteLine(FindPermutation(input));
        }

        private static string FindPermutation(string input)
        {
            // Base case
            if (input.Length == 1)
                return input;

            string result = string.Empty;

            foreach(char c in input)
            {
                result = c + FindPermutation(input.Substring(input.IndexOf(c) + 1));
            }

            return result;
        }

        #region PrivateMethods

        static private int BrutalForceFindWays(int leftSteps)
        {
            if (leftSteps == 0)
                return 1;
            else if (leftSteps < 0)
                return 0;
            else
                return BrutalForceFindWays(leftSteps - 1) + BrutalForceFindWays(leftSteps - 2) + BrutalForceFindWays(leftSteps - 3);
        }

        static private int FindPossibleWays(int leftSteps, int[] memo)
        {
            if (leftSteps == 0)
                return 1;
            else if (leftSteps < 0)
                return 0;
            else if(memo[leftSteps-1] > -1)
            {
                return memo[leftSteps-1];
            }
            else
            {
                memo[leftSteps-1] = FindPossibleWays(leftSteps - 1 , memo) + FindPossibleWays(leftSteps - 2, memo) + FindPossibleWays(leftSteps - 3, memo);
                return memo[leftSteps-1]; 
            }

        }

        #endregion PrivateMethods
    }
}
