using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4___Sorting_and_Searching
{
    class Program
    {
        static void Main(string[] args)
        {
            //QuestionOne();
            //KadanesAlgorithm();
            KMPAlgorithm();
        }

        /*You are given two sorted arrays, And B, where A has a large enough butter at the
         end to hold B. Wirte a method to merge B into A in sorted order.*/
        /*Hint: 
         * Start from the end of the array A
         */
        static void QuestionOne()
        {
            // Instantiate array A
            int[] A = new int[9];

            // Insert values to the array A
            for (int i = 0; i < 6; i++)
                A[i] = i * 3;

            // Instantiate array B
            int[] B = { 6, 7, 8 };

            // The pointer of array A indicating the current element
            int indexA = (A.Length - 1) - B.Length;

            int indexB = B.Length - 1;

            int pointer = A.Length - 1;

            // Start from the end of Array A
            while (pointer > 0)
            {

                //1. If indexA is negative, then insert all B elements
                if (indexA < 0)
                {
                    for (int i = pointer, j = A.Length - pointer; i < 0; i--, j--)
                    {
                        A[i] = B[j];
                    }
                    break;
                }
                //2. If indexB is negative, then insert all A elements 
                else if (indexB < 0)
                {
                    break;
                }
                else
                {
                    if (A[indexA] >= B[indexB])
                    {
                        A[pointer] = A[indexA];
                        A[indexA] = int.MinValue;
                        indexA--; pointer--;
                    }
                    else
                    {
                        A[pointer] = B[indexB];
                        indexB--; pointer--;
                    }
                }
                //3. Otherwise, we should still need comparing..
                //a.. Compare A's current element to B's current element, if A is bigger than isnert the A element to the A[]
                //b.. If B is bigger than insert the B element to the A[]
            }

            foreach (int e in A)
                Console.Write(e);
            Console.ReadLine();
        }

        /// <summary>
        /// Find the maximum sum of sub-array in the given array
        /// </summary>
        static void KadanesAlgorithm()
        {
            // By using Kadane's Algorithm, we can find the maximum in linear time complexity.
            // When the certian period's sum is negative, then we can skip that period
            // We can determine that if we should add the element by checking if it is negative
            var input = new int[] { -20, 3, 5, -10, 10, -1, 1, -4, 3 };
            int maxSum = int.MinValue, tempSum = 0;

            for (int i = 0; i < input.Length; i++)
            {
                // Get the current element and add it to the tempSum
                tempSum += input[i];

                // If tempSum is bigger than maxSum, assign it to maxSum
                // Otherwise, maxSum stays the same. (This means, we keep adding input elements to the tempSum
                maxSum = Math.Max(maxSum, tempSum);

                // If tempSum is negative, which means .. the sum of input elements' that we went through are negative and we should not add this subarray to find the max value
                // Therefore, we should set tempSum to 0 to calculate tempSmm from next input[i] again
                if (tempSum < 0)
                    tempSum = 0;
            }

            Console.WriteLine("Max sum: " + maxSum);
            Console.ReadKey();
        }

        /// <summary>
        /// Given a text txt[0..n-1] and a pattern pat[0..m-1], 
        /// write a function search(string pattern, char txt that prints all occurrences of pattern in txt.
        /// You may assume that n > m.
        /// </summary>
        static void KMPAlgorithm()
        {
            string input = "ABCDABCDABEE";
            string pattern = "ABCDABE";

            int[] lps = GetLPS(pattern);

            Search(input, pattern, lps);
        }

        private static int[] GetLPS(string pattern)
        {
            var lps = new int[pattern.Length];

            // Set the first element to 0 since lps[0] is always 0 because prefix's length is 1
            lps[0] = 0;

            for (int i = 1; i < pattern.Length; i++)
            {
                var subString = pattern.Substring(0, i + 1);

                // Medium of substring
                int medium = subString.Length / 2;

                // Check if pattern's length is odd number
                bool isOddPattern = subString.Length % 2 == 1;

                // Set first index to 0 and the second to medium + 1 or medium based on isOddPattern
                // E.g: aaba, aabaa
                int firstIndex = 0, secondIndex = isOddPattern ? medium + 1 : medium;

                // 1. Start with the full size of prefixSize(=medium).
                //   a. If they are not all same chars, Go to 1 and start with the full size - 1
                while (medium > 0)
                {
                    // If there is a same prefix and post exist with the prefixSize, then update lps and break this loop
                    if (IsFrequencyExists(firstIndex, secondIndex, medium, subString))
                    {
                        lps[i] = medium;
                        break;
                    }
                    // If no same pre-post fix exist with the prefixSize, find again with the decreased prefixSize
                    else
                        secondIndex++;

                    // Since this size is not working, we should try with the decreased size
                    medium--;
                }
            }

            return lps;
        }

        private static bool IsFrequencyExists(int firstIndex, int secondIndex, int medium, string subString)
        {
            // Loop through all char of substring until firstIndex is smaller than medium
            while (firstIndex < medium)
            {
                // If the char of first and second subarrays are different
                // (Fist substring and second substring which have been splitted by miedum)
                // Just return false
                if (subString[firstIndex] != subString[secondIndex])
                    return false;

                // If they are same, we continue comparing both chars of two sub-arrays
                firstIndex++;
                secondIndex++;
            }

            return true;
        }

        private static void Search(string input, string pattern, int[] lps)
        {
            // Start reading input's char and compare the char to pattern's char
            // If it is same, then keep comparing the next char
            // 1. If not, we should utilize lps array
            //  a. Get the failed index of pattern and get the corresponding value of lps
            //     and then we skip as many as the lps value so that we don't need to compare the already read char of input
            // 2. When comparing the pattern is done and all chars are same, printout that we have found the matching patter in input

            // Index for pattern
            int j = 0;

            // Loop through all char of input (we don't need to update i since we don't want duplicate comparing execution
            // due to setting i to backward
            for (int i = 0; i < input.Length; i++)
            {
                // If both chars are same, continue comparing and increase the pattern index
                if (input[i] == pattern[j])
                    j++;
                // If they are different, we should get the value of lps to avoid duplicate comparison
                else
                {
                    // When j is 0 we can simply continue instead of accessing the lps array
                    if (j != 0)
                    {
                        Console.WriteLine("j: " + j + " lps[j-1]: " + lps[j - 1] + " i: " + i);
                        j = lps[j - 1];
                        // We should compare the current element again..so we decrease the i here because forloop will increase i
                        i--;
                    }
                }

                // If we found the pattern, print out and update the pattern index with lps
                if (j == pattern.Length)
                {
                    Console.WriteLine("Find the pattern: " + (i - (j - 1)));
                    j = lps[j - 1];
                }
            }

            Console.ReadKey();
        }
    }
}
