using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_Microsoft_Palindrome
    {

        //Find longest palindrome
        //
        //Approach is to use dynamic programming and generate a table indicating which
        //letters are palindromes
        //
        //

        public HashSet<string> getAllPalindromes(string word)
        {
            
            int length = word.Length;

            //Create NxN array to hold palindrome search results
            //Each position will indicate the start and end of a substring
            //0,0   -> first character
            //3,5   -> chracters 3, 4, and 5
            //
            //
            //We will iterate over the solution set using larger and larger substrings
            //If the edges of the substrings match, the solution set will be checked for the inner substring
            //to see if it is a palindrome.
            //
            //For example, consider this word: 1232
            //
            //We know that each individual letter (1, 2, 3, 2) are palindromes, but need to find longer strings that are
            //palindromes
            //
            //So start from the end and work backwards
            //  3, 2        <-- start/end do not match, so false
            //  2,3,2       <-- start/end (value of 2) match, so check the inner case for '3'
            //                  '3' is a palindrome so '2,3,2' is also a palindrome.  Mark as true
            //  2,3         <-- start/end do not match, so false
            //  1,2,3,2     <-- start/end do not match, so false
            //  1,2,3       <-- start/end do not match, so false
            //  1,2         <-- start/end do not match, so false
            //
            //  Solutions: 1, 2, 3, 232


            //Another example: 2,1,2,3,2
            //  3, 2        <-- start/end do not match, so false
            //  2,3,2       <-- start/end (value of 2) match, so check the inner case for '3'
            //                  '3' is a palindrome so '2,3,2' is also a palindrome.  Mark as TRUE
            //  2,3         <-- start/end do not match, so false
            //  1,2,3,2     <-- start/end do not match, so false
            //  1,2,3       <-- start/end do not match, so false
            //  1,2         <-- start/end do not match, so false
            //  2,1,2,3,2   <-- start/end (value of 2) match, so check the inner case for '1,2,3'
            //                  We calculated '1,2,3' above and found it to be false, so this is FALSE
            //  2,1,2,3     <-- start/end do not match, so false
            //  2,1,2       <-- start/end match (value of 2), so check inner case for '1'
            //                  '1' is a palindrome, so '2,1,2' is also a palindrome. Mark as TRUE
            //  2,1         <-- start/end do not match, so false
            //
            //  Solutions: 2, 1, 3, 232, 212
            //

            //  To visualize the values being checked, consider i and j
            //          <-- i   <-- j
            //      2   1   2   3   2
            //
            //  We have two iterators, i and j.  They are iterating from the end of the word to the front
            //  j is always behind i, and the inner solutions are calculated before the outter solutions
            //  are needed
            //
            //  Suppose we change the example so the entire string is a palindrome:
            //          <-- i   <-- j
            //      2   3   2   3   2
            //
            //      Here we have i advancing towards the front and j always trying to catch up
            //      When i is on the first position and j is on the last (string: 23232)
            //      we need to know the value for the inner string (323)
            //      This was already calculated when i was on the 2nd character (2) and j was
            //      on the 4th character (2).
            //      So the solution to the inner problem is always calculated and available before the outter
            //      problem needs it


            bool[,] isPalindrome = new bool[length, length];

            //Running example for word: 1232
            //Word  1   2   3   2
            //      0   1   2   3 
            //  0   T
            //  1       T
            //  2           T
            //  3               T

            //Initialize the diagonal of the array to true because in all cases
            //a single letter is considered a palindrome. 
            for (int i = 0; i < length; i++)
            {
                isPalindrome[i, i] = true;
            }

            PrintArray(isPalindrome);


            //Next iterate across the matrix from the lower right to the upper left
            //  for i in length-1 .. 0      <-- i is going from right side to left
            //      for j in length-1 ..i   <-- j is going from right side to left, but strictly greater than i
            //                                  So we are only iterating over the top right half of the matrix where j > i                                              

            //                                 Word 1   2   3   2
            //                                      0   1   2   3 
            //                                  0   T   .   .   .       <-- iterating over item marked with '.'
            //                                  1       T   .   .
            //                                  2           T   .     
            //                                  3               T
            //      
            //      At each iteration we use this recurance relationship
            //
            //      if (word[i] == word[j])     <-- start and end letters match
            //          isPalindrome[i,j] = isPalindrome[i+1, j-1];         <-- set i,j based on result of inner characters (i+1, j-1)
            //      else isPalindrome[i,j] = false;

            for (int i = length - 1; i >= 0; i--)
            {
                for (int j = length - 1; j > i; j--)
                {
                    if (word[i] == word[j])                                 //  <-- outside letters match
                        isPalindrome[i, j] = isPalindrome[i + 1, j - 1];    //  <-- check inner solution (i+1, j-1) to see if it is a palindrome
                    else isPalindrome[i, j] = false;                        
                }                                                           //Word  1   2   3   2
            }                                                               //      0   1   2   3 
                                                                            //  0   T   .   .   . 
            PrintArray(isPalindrome);                                       //  1       T   .   T   <-- here i=1, j=3.  [1]=2 == [3]=2, so check inner
                                                                            //  2           T   .       and find i+1 = 1+1= 2, j-1 = 3-1=2.  [2]=3 == [2]=3
                                                                            //  3               T       So mark as true for '232'

            
            //Finally create solution set (using HashSet since it automatically removes duplicates)
            HashSet<string> s = new HashSet<string>();

            //Iterate over diagonal to include all single digit characters
            for (int i = 0; i < length-1; i++)
            {               
                s.Add(word.Substring(i, 1)); //i + 1));         
            }

            //Iterate over top right section of matric and find all values with a 'true'
            //Add their substring representations to the solution set
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

            RunValue("2321");

            RunValue("23232");

            RunValue("90sofjlskmjdfkljskdlfjs12321lkdfjlskdjflskjdflskdjflksdjflksjdf");
        }
    }
}
