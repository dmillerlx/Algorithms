using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_Microsoft_Palindrome
    {
        public HashSet<string> getAllPalindromes(string word)
        {

            int length = word.Length;

            bool[,] isPalindrome = new bool[length, length];

            //Initialize array 
            //for i 0..length
            //  for j 0..i
            //      isPaleindrome[i,j] = true
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    isPalindrome[i, j] = true;
                }
            }

            PrintArray(isPalindrome);


            for (int i = length - 1; i >= 0; i--)
            {
                for (int j = length - 1; j > i; j--)
                {
                    isPalindrome[i, j] = (word[i] == word[j]) ? isPalindrome[i + 1, j - 1] : false;
                }
            }

            PrintArray(isPalindrome);

            HashSet<string> s = new HashSet<string>();

            for (int i = 0; i < length-1; i++)
            {               
                s.Add(word.Substring(i, 1)); //i + 1));         
            }

            for (int i = 0; i < length; i++)
            {
                for (int j = i; j < length; j++)
                {
                    if (isPalindrome[i, j])
                        s.Add(GetSubString(word, i, j+1));
                }
            }
            return s;
        }

        public void PrintArray(int [,]V)
        {
            int n = V.GetLength(0);
            int W = V.GetLength(1);
            //Priting matrix for debugging
            Console.WriteLine("----------------------------------");
            for (int i = 0; i <= n; i++)
            {
                Console.WriteLine();
                for (int j = 1; j <= W; j++)
                {
                    if (j != 0)
                    {
                        Console.Write(" ");
                        if (V[i, j] < 10)
                            Console.Write(" ");
                    }
                    Console.Write(V[i, j]);
                }
            }
            Console.WriteLine();
            Console.WriteLine("----------------------------------");
        }
        public void PrintArray(bool[,] V)
        {
            int n = V.GetLength(1);
            int W = V.GetLength(0);
            //Priting matrix for debugging
            Console.WriteLine("----------------------------------");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < W; j++)
                {
                    if (j > 0)
                    {
                        Console.Write(" ");
                    }
                    Console.Write(V[i, j] ? "T":"-");
                }
            }
            Console.WriteLine();
            Console.WriteLine("----------------------------------");
        }


        public string GetSubString(string val, int start, int end)
        {
            // 1 2 3 4 5 6 7 8 9 
            // a b c d e f g h i
            //
            // 1 to 4
            // 1, 4
            // 5 to 8
            // 5, 3
            // 

            return val.Substring(start, end - start);
        }

        public void RunValue(string val)
        {
            Console.WriteLine("---Test---");
            Console.WriteLine(val);
            Console.WriteLine("--Palindromes:");
            HashSet<string> s = getAllPalindromes(val);
            foreach (string str in s)
            {
                Console.WriteLine(str);
            }

        }

        public void Run()
        {
            //RunValue("rohihor");
            //RunValue("test");
            //RunValue("autotua");

            //RunValue("allanece");

            //RunValue("1234");
            RunValue("1232");

        }
    }
}
