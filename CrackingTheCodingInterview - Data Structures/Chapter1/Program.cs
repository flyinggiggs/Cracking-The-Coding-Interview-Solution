using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter1___Arrays_and_Strings
{
    // Arrays and Strings
    class Program
    {
        static void Main(string[] args)
        {
            //SolveNumber1();
            //SolveNumber2_a();
            //SolveNumber2_b();
            //SolveNumber3();
            //SolveNumber4();
            //SolveNumber5();
            SolveNumber6();
        }

        // Implement an algorithm to determine if a string has all unique characters.
        // What if you cannot use additional data structures?
        static void SolveNumber1()
        {
            // Input string value
            string input = "abcdefggh";

            // Loop through each char of string
            for (int i = 0; i < input.Length; i++)
            {
                Console.WriteLine("First for loop.");
                for (int j = i + 1; j < input.Length; j++)
                {
                    if (input[i] == input[j])
                    {
                        Console.WriteLine("Non unique value");
                        Console.ReadLine();
                        return;
                    }
                }
            }

            Console.WriteLine("Unique value");
            Console.ReadLine();
        }

        // Implment a function void reverse() which reverses a null-terminated string.
        static void SolveNumber2_a()
        {
            string s = "abcdefg";
            string result = string.Empty;

            for (int i = s.Length - 1; i >= 0; i--)
            {
                result += s[i];
            }

            Console.WriteLine("Result: " + result);
            Console.ReadLine();
        }

        // Given two strings, write a method to decide if one is a permutation of the other
        static void SolveNumber2_b()
        {
            string a = "abcdef";
            string b = "defabc";

            if (IsPermutation(a, b))
                Console.WriteLine("B is a permutation of A");
            else
                Console.WriteLine("B is not a permutation of A");

            Console.ReadLine();
        }

        static bool IsPermutation(string a, string b)
        {
            // 1. we could simply check the number of char in the fist string and we save each char as key and the number of the char as value in dictionary
            // 2. After that, we loop through all chars in the second input and deduct the number of each char in the charMap
            var charMap = new Dictionary<char, int>();

            // Loop through all chars in the first string
            foreach (char c in a)
            {
                if (!charMap.ContainsKey(c))
                    charMap[c] = 1;
                else
                    charMap[c] += 1;
            }

            foreach (char c in b)
            {
                if (!charMap.ContainsKey(c))
                    charMap[c] = -1;
                else
                    charMap[c] -= 1;
            }

            return charMap.Values.All(v => v == 0);
        }

        // Write a method to replace all spaces in a string with '%20'. You may assume that the string
        // has sufficient space at the end of the string to hold the additional characters, and that
        // you are given the "true" length of the string. 
        // (Note: if implementing in Java, please use a character array so that you can perform this operation
        //        in place) 
        // Input: "Mr John Smith   "
        // Output: "Mr%20John%20Smith" 
        static void SolveNumber3()
        {
            string input = "Mr John Smith   ";

            char whiteSpace = ' ';
            string stringToReplace = "%20";
            bool isPassedTail = false;
            string result = string.Empty;

            for (int i = input.Length - 1; i >= 0; i--)
            {
                if (!isPassedTail && input[i] != whiteSpace)
                {
                    isPassedTail = true;
                    result += input[i];
                }
                else if (isPassedTail && input[i] == whiteSpace)
                {
                    result = stringToReplace + result;
                }
                else if (isPassedTail && input[i] != whiteSpace)
                {
                    result = input[i] + result;
                }
            }

            Console.WriteLine("Input: " + input);
            Console.WriteLine("Result: " + result);
            Console.ReadLine();
        }

        /* Given a string, wrte a function to chcek if it is a permutation of a palindrome. 
         * A palindrome is a word or phrases that is the same forwards and backwards. A permutation is a
         * rearrangement of letters. The palindrome does not need to be limited to just diciontary words.
             */
        static void SolveNumber4()
        {
            string s = "Tact Coa";
            if (IsPlaindromePermutation(s))
                Console.WriteLine("It is a permutation fo palidrome");
            else
                Console.WriteLine("It's not a permutation of palidrome");
            Console.ReadKey();
        }

        static bool IsPlaindromePermutation(string input)
        {
            // Plaidrome always have the even number of each char in the string
            // We can simply sort the string or use dictionary to save the number of char in the string
            var charMap = new Dictionary<char, int>();

            foreach (char c in input.Replace(" ", string.Empty).ToLower())
            {
                if (!charMap.ContainsKey(c))
                    charMap[c] = 1;
                else
                    charMap[c] += 1;
            }

            return charMap.Values.Where(v => v % 2 != 0).Select(s => s).Count() == 1;
        }

        /* There are three types of edits that can be performed on strings: insert a character, remove a character,
         * or replace a character. Given two strings, write a function to check if they are one edit (or zero edits) away.
           pale, ple => true , pale, bale => true
           pales, pale => true, pale, bake => false
             */
        static void SolveNumber5()
        {
            string a = "pale";
            string b = "ple";

            if (IsEditable(a, b))
                Console.WriteLine("They are editable.");
            else
                Console.WriteLine("They are not editable.");

            Console.ReadKey();
        }

        // This is good, but if we use two pointers for two string inputs, we can reduce the line of codes
        static bool IsEditable(string a, string b)
        {
            // If their length are same, simply check each char by using for loop
            if (a.Length == b.Length)
            {
                return IsReplaceAble(a, b, 0);
            }
            // If not, we should insert or delete one of A or B's char and continue checking if next chars are all same
            else
            {
                // When a is bigger than b
                if (a.Length - 1 == b.Length)
                {
                    // We should remove one char from a or insert a new char to b
                    for (int i = 0; i < b.Length; i++)
                    {
                        // 1. Check if the current chars are different, then Remove or Insert one char
                        if (a[i] != b[i])
                        {
                            // 2. Check the rest substrings are replaceable
                            return IsReplaceAble(a.Substring(i + 1, a.Length - (i + 1)), b.Substring(i, b.Length - i), 1);
                        }
                    }
                    // Return true since the last element can be simply removed so we don't need to compare
                    return true;
                }
                // When b is bigger than a
                else if (a.Length + 1 == b.Length)
                {
                    // We should remove one char from b or insert a new char to a
                    for (int i = 0; i < a.Length; i++)
                    {
                        // 1. Check if the current chars are different, then Remove or Insert one char
                        if (a[i] != b[i])
                        {
                            // 2. Check the rest substrings are replaceable
                            return IsReplaceAble(a.Substring(i, a.Length - i), b.Substring(i + 1, b.Length - (i + 1)), 1);
                        }

                    }
                    // Return true since the last element can be simply removed so we don't need to compare
                    return true;
                }
                else
                    return false;
            }

        }

        static bool IsReplaceAble(string a, string b, int allowedDifference)
        {
            int differentChar = allowedDifference;

            for (int i = 0; i < a.Length; i++)
            {
                if (differentChar > 1)
                    return false;
                // If they are different, we should replace char of A or B. Then, increase the differentChar 
                if (a[i] != b[i])
                    differentChar++;
            }

            return true;
        }

        /// <summary>
        /// String Compression: Implement a method to perform basic string compression using the counts of
        /// repeated characters. For example, the string aabcccccaaa would become a2b1c5a3.
        /// If the "compressed" string would not become smaller than the original string, your method should return
        /// the original string. You can assume the string has only uppercase and lowercase letters (a-z).
        /// </summary>
        static void SolveNumber6()
        {
            string input = "aabcccccaaa";
            var output = new StringBuilder();
            int count = 0;
            char previousChar = ' ';

            // Read char of the input
            // When this char is the first char, add it to stringBuilder and start counting
            // a. If the current char is the same with the previous char, then count the number
            // b. Otherwise, we should put the number of char to string builder and set count variable to 0
            for (int i = 0; i < input.Length; i++)
            {
                if (i == 0)
                {
                    output.Append(input[i]);
                    count++;
                    previousChar = input[i];
                }
                else
                {
                    if (previousChar == input[i])
                    {
                        count++;
                        // When this is the last char, then we should put the count to the end of string builder
                        if (i == input.Length - 1)
                            output.Append(count);
                    }
                    else
                    {
                        output.Append(count);
                        output.Append(input[i]);
                        previousChar = input[i];
                        count = 1;
                    }
                }
            }

            string result = output.ToString().Length > input.Length ? input : output.ToString();

            Console.WriteLine($"Compressed string: {result}");
            Console.ReadLine();
        }

        /// <summary>
        /// Rotate Martix: Given an image represented by an NxN matrix, where each pixel in the image is 4 bytes,
        /// write a method to rotate the image by 90 degress. Can you do this in place?
        /// </summary>
        static void SolveNumber7()
        {
            var image = new int[5][5];
        }
    }
}
