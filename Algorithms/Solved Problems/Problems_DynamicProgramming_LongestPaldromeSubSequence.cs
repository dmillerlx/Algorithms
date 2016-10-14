using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_DynamicProgramming_LongestPaldromeSubSequence
    {

        public void PrintArray(int[,] V)
        {
            int n = V.GetLength(0);
            int W = V.GetLength(1);
            //Priting matrix for debugging
            Console.WriteLine("----------------------------------");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < W; j++)
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
        
        public class SolutionItem
        {
            public int CurrentX { get; set; }
            public int CurrentY { get; set; }
            public bool Include { get; set; }

            public int PreviousItem1 { get; set; }
            public int PreviousItem2 { get; set; }

            public SolutionItem (int x, int y, bool include, int prevItem1, int prevItem2)
            {
                CurrentX = x;
                CurrentY = y;
                PreviousItem1 = prevItem1;
                PreviousItem2 = prevItem2;
                Include = include;
            }

            public override string ToString()
            {
                return "Curr: " + CurrentX + ", " + CurrentY + ", Prev: " + PreviousItem1 + "," + PreviousItem2 + "," + Include;
            }

        }

        //Find the longest palindromic sub sequence
        //
        //  This problem is to find the longest palindromic sub sequence, not the longest continous palidrome
        //
        //  So, this string:   agbdba
        //      Has the longest palindromic sub sequence of: abdba
        //
        //
        //Approach
        //  Using dynamic programming we will iterate across the entier string using a sliding window with an increasing size
        //  The results will be stored in a matrix of size NxN
        //  Each position in the matrix will represent the size of the palindrome between the start and end indexes
        //
        //  A second solution matrix will keep pointers to allow us to back track to the solution
        //   
        //
        //Algorithm
        //  First, initalize the diagonal of the matrix with 1 since single characters are palindromes of length 1
        //  
        //  Next, use a sliding window of increasing size to find the palindromes of increasing size
        //  Start with a length of 1, and slide the window from the left to the right
        //  On each iteration, i = starting letter and j = ending letter
        //  If the start and end characters match, we check to see if the sub sequence between the characters is a palindrome sub sequence
        //  We do this by add 2 to [i+1,j-1].
        //
        //  When viewed in the matrix, when the start and end characters match, the DP[i,j] will be 2 + the diagonal value DP[i+1,j-1]
        //
        //      if word[i] == word[j]
        //          DP[i,j] = DP[i+1, j-1] + 2      <-- we are adding 2 since each palindrome will increase by 2 characters (the 2 end chars)
        //          
        //
        //  If items at i and j do not match, then we propagate the longest sub sequence from either the DP[i+1,j] or DP[i,j-1] locations
        //  This will propagate the longest subsequence between i and j
        //                    
        //      if word[i] != word[j]
        //           DP[i, j] = Math.Max(DP[i + 1, j], DP[i, j - 1]);
        //
        //
        //  At the end, the length of the longest palindromic sub sequence will be DP[0, len -1]
        //
        // Example  a  g  b  d  b  a     
        //          0  1  2  3  4  5
        //
        //      0   1  1  1  1  3  5   <-- length of longest palindromic sub sequence
        //      1   0  1  1  1  3  3
        //      2   0  0  1  1  3  3
        //      3   0  0  0  1  1  1
        //      4   0  0  0  0  1  1
        //      5   0  0  0  0  0  1
        //
        //  As seen in the matrix, the lower left of the matrix is ignored and the diagonal starts with characters of length 1.
        //  Propagating to the upper right corner, when two charaters match, the solution in incremented by 2.  <-- DP[i,j] = DP[i+1, j-1] + 2
        //      Where the characters do not match, the longest palindromic subsequence is carried over.         <-- DP[i, j] = Math.Max(DP[i + 1, j], DP[i, j - 1]);
        //  
        //  This just gives us a solution value of: 5
        //  How do we get the actual solution?
        //
        //  We must keep track of the start/end letters that match and back track to the begining
        //  We will use a class that holds the 
        //      current i,j value                       <-- tells us the start/end characters for the word
        //      previous i,j                            <-- which location this item is built from
        //      flag to include the item in the results <-- If we are to include this in the final solution
        //
        //  The current and previous pointers are obvious but the include flag may not be
        //  When we fill in the matrix, we will be setting the i, j values based on other fields
        //      If we are setting the value because the word[i] == word[j] then we need to include that in the output
        //      But when word[i] != word[j] we do not want to include it in the output...but we do need to keep track of where
        //      the value was built from.  So we keep it in the solution matrix and instruct it to not include it
        //
        //  Once we have the solution matrix filled in, we start backtracking at the last matching word[i] == word[j]
        //  This location may not be the matrix[0, len-1] so we must keep track of that location in 'lastSolution'
        //  We then follow these back pointers until the pointers are -1 (which is what they are set to when the length is 1 for single chars)
        //
        //  This back tracking gives us the coorinates (i, j) of the included letters in the input string, but we need to get all of those letters
        //  into a string, in the correct order.  To convert the coordinates to the actual letters we create a boolean array of length N
        //  and set the value to 'true' when it is found in the back tracking list
        //  Finally we iterate across the boolean array and append each item marked as 'true' to the output       
        //
        //  This produces the output string of:
        //      abdba
        //

        public string LongestPalindromeSubSequence(string word)
        {

            int len = word.Length;
            int[,] DP = new int[len, len];
            SolutionItem[,] solution = new SolutionItem[len, len];

            int i;
            int j;
            int l;
            SolutionItem lastSolution = null;

            for (i=0; i < len; i++)
            {
                DP[i, i] = 1;       //Each single letter is a palindrome of length 1
                lastSolution = solution[i, i] = new SolutionItem(i, i, true, -1, -1);                
            }


            //Use a sliding window of increasing size to iterate over the string            
            for (l = 1; l <= len; l++ )         //Run length from 1 to len
            {
                for (i = 0; i < len - l; i++)   //Run i from 0 to len -1
                {
                    j = i + l;

                    if (word[i] == word[j])
                    {
                        DP[i, j] = DP[i + 1, j - 1] + 2;

                        //Update solution matrix
                        solution[i, j] = new SolutionItem(i, j, true, i + 1, j - 1);
                        lastSolution = new SolutionItem(i, j, false, i, j);
                    }
                    else
                    {
                        DP[i, j] = Math.Max(DP[i + 1, j], DP[i, j - 1]);

                        if (DP[i+1, j] > DP[i,j-1])
                            solution[i, j] = new SolutionItem(i, j, false, i + 1, j);
                        else solution[i, j] = new SolutionItem(i, j, false, i, j - 1);                        
                    }
                }
            }
           
             
            PrintArray(DP);

            //Construct solution using solution matrix
            //
            //Starting with the lastSolution item, back track until we have no previous item (previousItem1 < 0)
            //On each item that has 'include' set to true, mark the CurrenyX and CurrentY offsets to 'true'
            //Finally iterate across the includeChars array and add all offsets marked as 'true' to the output
            //
            StringBuilder sb = new StringBuilder();
            SolutionItem val;
            val = lastSolution;
            bool[] includeChars = new bool[len];
            while (val != null)
            {
                if (val.Include)
                {
                    //sb.Append(val.CurrentX + "," + val.CurrentY + " -> " + word[val.CurrentX] + " " + word[val.CurrentY]);
                    includeChars[val.CurrentX] = true;
                    includeChars[val.CurrentY] = true;
                }
                if (val.PreviousItem1 < 0 || val.PreviousItem2 < 0)     //single characters will have a previous point of -1, so no previous item
                    val = null;
                else
                    val = solution[val.PreviousItem1, val.PreviousItem2];
            }

            StringBuilder output = new StringBuilder();
            for (int x = 0; x < len; x++)
            {
                if (includeChars[x])
                    output.Append(word[x]);
            }

            return output.ToString();

        }

        public void Run()
        {

            Console.WriteLine(LongestPalindromeSubSequence("b"));

            Console.WriteLine(LongestPalindromeSubSequence("agbdba"));

            Console.WriteLine(LongestPalindromeSubSequence("agbd03u2osdflkjlaagafdjbacvjsalfkjslfkjslafdjba"));
            Console.WriteLine(LongestPalindromeSubSequence("ALGORITHMSISAGREATCLASS"));

        }


    }
}
