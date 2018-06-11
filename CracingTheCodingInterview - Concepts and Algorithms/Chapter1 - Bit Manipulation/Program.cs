using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter1___Bit_Manipulation
{
    class Program
    {
        static void Main(string[] args)
        {
            QuestionOne();
        }


        /// <summary>
        /// Insertion: You are given two 32-bit numbers, N and M, and two bit positions, i and j.
        /// Write a method to insert M into N such that M starts a bit j and ends at bit i. 
        /// You can assume that the bits j through i have enough sapce to fit all of M.
        /// That is, if M=10011, you can assume that there are at least 5 bits between j and i.
        /// You would not, for example, have j=3 and i=2, because M could not fully fit between bit 3 and bit 2.
        /// Input  N = 10000111000, M = 10011, i = 2, j = 6
        /// Output N = 10001001100
        /// </summary>
        static void QuestionOne()
        {
            int m = Convert.ToInt32("10011", 2);
            long n = Convert.ToInt32("10000111000", 2);
            Console.WriteLine("m: " + Convert.ToString(m, 2));
            Console.WriteLine("N: " + Convert.ToString(n, 2));

            // J is the start bit, i is the end bit
            int i = 2, j = 6;

            //1. We should replace bits from i to j in n with 0 bit
            long oneBits = ~0;

            // Shift one bits j+1 bits to the left 
            long leftOneBits = oneBits << j + 1;

            // Shift one bits i bit and then swap
            long rightOneBits = ~(oneBits << i);

            long mark = leftOneBits | rightOneBits;

            // Clean from i to j bits of n bits
            n = n & mark;

            //Shit m i bits to merge 
            long longM = m << i;

            // Merge n and longM
            n |= longM;

            Console.WriteLine("N: " + Convert.ToString(n, 2));
            Console.ReadLine();
        }
    }
}
