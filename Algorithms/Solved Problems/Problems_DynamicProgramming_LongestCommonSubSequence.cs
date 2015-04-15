using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_DynamicProgramming_LongestCommonSubSequence
    {

        public class Solution1
        {

            int[,] matrix = null;

            /*********Sample Run showing the matrix that is built
            ----------START--------------
            Longest Common Sub Sequence for:
            HEIROGLYPHOLOGY --> MICHAELANGELO

            Value: 5
            Common Sub Sequence: HELLO
            ----------------------------------

            5  5  5  5  4  4  3  3  3  3  3  2  1  0
            4  4  4  4  4  4  3  3  3  3  3  2  1  0
            4  4  3  3  3  3  3  3  3  3  2  2  1  0
            3  3  3  3  3  3  3  3  3  3  2  2  1  0
            3  3  3  3  3  3  3  3  3  3  2  2  1  0
            3  3  3  3  3  3  3  3  3  3  2  2  1  0
            3  3  3  3  3  3  3  2  2  2  2  2  1  0
            3  3  3  3  2  2  2  2  2  2  2  2  1  0
            3  3  3  3  2  2  2  2  2  2  2  2  1  0
            3  3  3  3  2  2  2  2  2  2  2  2  1  0
            2  2  2  2  2  2  2  2  2  2  2  2  1  0
            2  2  2  2  2  2  2  2  2  2  2  2  1  0
            1  1  1  1  1  1  1  1  1  1  1  1  1  0
            1  1  1  1  1  1  1  1  1  1  0  0  0  0
            0  0  0  0  0  0  0  0  0  0  0  0  0  0
            0  0  0  0  0  0  0  0  0  0  0  0  0  0
            ----------------------------------
            ----------END--------------
            */

            public void Run(string a, string b)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("----------START--------------");

                Console.WriteLine("Longest Common Sub Sequence for:");                                  
                Console.WriteLine(a + " --> " + b);
                Console.WriteLine();
                
                //M = length of A
                //N = length of B
                int M = a.Length;
                int N = b.Length;
                matrix = new int[M + 1, N + 1];

                int i = 0;
                int j = 0;

                //Iterate across entire matrix, starting at the right bottom most element
                //The recurance will reference the i+1 and j+1 elements, so we are working backwords from the lower right to the upper left part of the matrix
                
                //            j ------->
                //  offset    0  1  2  3  4  5  6  7  8  9  10 11 12 13                 <-- Offsets
                //  
                //            M  I  C  H  A  E  L  A  N  G  E  L  O                     <-- String characters
                //  
                //i 0    H    5  5  5 H5  4  4  3  3  3  3  3  2  1  0                  <-- Items with a character are part of common longest common subsequence: HELLO
                //  1    E    4  4  4  4  4 E4  3  3  3  3  3  2  1  0                  
                //  2    I    4  4  3  3  3  3  3  3  3  3  2  2  1  0
                //  3    R    3  3  3  3  3  3  3  3  3  3  2  2  1  0
                //  4    O    3  3  3  3  3  3  3  3  3  3  2  2  1  0
                //  5    G    3  3  3  3  3  3  3  3  3  3  2  2  1  0
                //  6    L    3  3  3  3  3  3 L3  2  2  2  2  2  1  0
                //  7    Y    3  3  3  3  2  2  2  2  2  2  2  2  1  0
                //  8    P    3  3  3  3  2  2  2  2  2  2  2  2  1  0
                //  9    H    3  3  3  3  2  2  2  2  2  2  2  2  1  0
                //  10   O    2  2  2  2  2  2  2  2  2  2  2  2  1  0
                //  11   L    2  2  2  2  2  2  2  2  2  2  2 L2  1  0
                //  12   O    1  1  1  1  1  1  1  1  1  1  1  1 O1  0
                //  13   G    1  1  1  1  1  1  1  1  1  1  0  0  0  0
                //  14   Y    0  0  0  0  0  0  0  0  0  0  0  0  0  0
                //  15        0  0  0  0  0  0  0  0  0  0  0  0  0  0  <-- Iterator starts here and works right to left, bottom to top
                //                                                   ^      Matrix is [a.Length+1 x b.Length +1] so outter edge on bottom and right 
                //                                                          can be set to 0
                //                                                          This way when iterating from right to left, bottom to top we can reference
                //                                                          the previous element as [i+1,j+1] or [i+1,j] or [i,j+1]
                //
                //    What values are we putting into the matrix?
                //    1. If on the edge of the matrix, set [i,j] to 0 so we have an initial value
                //    2. If a[i] == b[j] then set [i,j] to 1 + [i+1,j+1]
                //          Example: When iterating over the matrix, the first 2 items to reach an equal value are
                //              a[12] == b[12] == 'O'  --> matrix[12,12] = 1 + matrix[13,13] = 1 + 0 = 1
                //              a[13] == b[9] == 'G' --> matrix[13,9] = 1 + matrix[14, 10] = 1 + 0 = 1
                //
                //          The next one to match is the last 'L' in both words so 
                //              a[11] == b[11] = 'L' --> matrix[11,11] = 1 + matrix[12,12] = 1 + 1 = 2      <-- matrix[12,12] calculated above
                //
                //          
                //    3.  If a[i] != b[j] then set [i,j] to the Max of (matrix[i+1, j], matrix[i,j+1])
                //        This basically carries over the largest of the values to the right or below the current item
                //          Example: The last iteration is when i = 0 and j = 0
                //              a[0] = 'H'
                //              b[0] = 'M'
                //              a[0] != b[0] --> matrix[0,0] = max (matrix[0+1, 0], matrix[0, 0+1]) = max(matrix[1,0], matrix[0,1]) = max (5, 4) = 5
                //              So the last iteration sets the array for [0,0] to 5
                //
                //   4. Answers to the original problem
                //          Question: How long is the longest common sub sequence?
                //          Answer: matrix[0,0] = 5.  The longest common sub sequence is 5 characters
                //
                //          Question: What is the longest common sub sequence?
                //          Answer: HELLO
                //
                
                //Longest Common Subsequence defined:

                //LCS (Xi, Yj) =    {   0                                           if i = 0 or j = 0       }   #1
                //                  {   LCS (Xi-1, Yj-1)                            if Xi == Yj             }   #2
                //                  {   longest (LCS (Xi, Yj-1, LCS(Xi-1, Yj))     if Xi != Yj              }   #3

                //The iterative algorithm below does the above formula

                for (i = M; i >= 0; i--)
                    for (j = N; j >= 0; j--)
                    {
                        if (i == M || j == N) matrix[i, j] = 0;                         //If i or j is at right or bottom edge, set to 0            <-- See 1 above
                        else if (a[i] == b[j]) matrix[i, j] = 1 + matrix[i + 1, j + 1]; //If (a[i] == b[i]) set matrix[i,j] to 1 + the diagonal matrix[i+1,j+1] <-- See #2 above
                        else matrix[i, j] = Math.Max(matrix[i + 1, j], matrix[i, j + 1]);//Else set matrix[i,j] to the max of either the item to the right (i+1) or the item below (j+1)    <-- See #3 above
                    }

                //Solution for longest subsequence value is now at matrix[0,0]
                Console.WriteLine("Value: " + matrix[0, 0]);

                StringBuilder S = new StringBuilder();
                i = 0;
                j = 0;
                while (i < M && j < N)
                {
                    if (a[i] == b[j])
                    {
                        S.Append(a[i]);             //Values match, add to answer set and advance both i and j
                        i++;
                        j++;
                    }
                    //Check matrix values to the right and below the current item to find the largest item
                    //and travel towards the largest item.
                    //If they match, default to traveling down (i++)
                    else if (matrix[i + 1, j] >= matrix[i, j + 1])
                        i++;
                    else j++;
                }

                Console.WriteLine("Common Sub Sequence: " + S.ToString());

                //Priting matrix for debugging
                Console.WriteLine("----------------------------------");
                for (i = 0; i <= M; i++)
                {
                    Console.WriteLine();
                    for (j = 0; j <= N; j++)
                    {
                        if (j != 0)
                        {
                            Console.Write(" ");
                            if (matrix[i, j] < 10)
                                Console.Write(" ");
                        }
                        Console.Write(matrix[i, j]);
                    }
                }
                Console.WriteLine();
                Console.WriteLine("----------------------------------");

                //To review the edit changes, use Dijkstra to find shortest path through the array
                //Back pointers and direction show the type of operation
                                
                Console.WriteLine("----------END--------------");
            }

        }

        public void Run()
        {
            Solution1 s1 = new Solution1();

            //s1.Run("INTENTION", "EXECUTION");

            //s1.Run("MOP", "AMOP");

            //s1.Run("EXECUTION", "INTENTION");

            s1.Run("INTENTION", "INTENTIOP");

            //s1.Run("Saturday", "Sunday");

            s1.Run("empty bottle", "nematode knowledge");

            s1.Run("HEIROGLYPHOLOGY", "MICHAELANGELO");


        }
    }
}
