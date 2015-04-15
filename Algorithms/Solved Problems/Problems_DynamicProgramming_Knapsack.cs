using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_DynamicProgramming_Knapsack
    {
        public class Solution1
        {

            //These slides show the basic algorithm and the keep algorithm but have a flaw in the keep
            //section.  
            //
            //This youtube video shows the basic algorithm and explains how the keep algorithm works
            //to produce the final output result.
            //
            // https://www.youtube.com/watch?v=EH6h7WA7sDw&list=WL&index=36

            //
            //Overview
            //The KnapSack problem is an optimization problem with these inputs:
            //      v_arr   Value       Values assocated with each item
            //      w_arr   Weight      Weights assocated with each item
            //      n       Count       Number of items
            //      W       Max Weight  Maximum weight for all items in the knap sack
            //
            //The goal of this problem is to find these solutions
            //  1. What is the maximum value that can be placed into the knapsack with out exceeding the maximum weight?
            //  2. What items, when placed into the knap sack, will have the maximum value without exceeding the maximum weight?
            //
            //Problem defintion
            //  A knap sack can hold a maximum of W weight
            //  n items are available to place into the knapsack
            //  Each item has a value of v_arr[]
            //  Each item has a weight of w_arr[]
            //  Notes
            //      Only 1 of each item exists
            //      Items cannot be split.  They must be placed into the knap sack, or not placed into the knap sack
            //      Weight must be an integer
            //
            //Solution
            //  To solve this problem, the algorithm will iterate over each item and each weight and find the best solution
            //  for the given item and weight.  The algorithm incrementally solves the problem by solving for every item and
            //  every weight.
            //
            //  It works like this:
            //      A solution array is created where the rows are the item numbers and the columns are the weight limits.
            //          The first item is #1, but we start with an empty item #0 which has no size and no value
            //
            //              Weight ->     
            //      Items   1   2   3   4   5
            //          0
            //          1
            //          2
            //          3
            //          4

            //      It starts with 0 items and iterates over all weights.  The answer is that 0 items can fit into the bag regardless
            //          of the weight so the first row in the solution array is all 0's.
            //
            //      Next it uses item #1 and iterates over all weights.  Where the item can fit based on the weight limitation it places the
            //          value for item #1 into the table.  If there is any space remaining after that item is placed into the bag, it checks the
            //          previous row for how best to fill the remaining weight.  For this item (#1) the previous row is all 0's so nothing changes.
            //      Next it uses item #2 and iterates over all weights.  It ONLY looks at item #2 and does not consider item #1.  Where item #2 can fit
            //          into based on the weight limitation it uses the value for item #2.  This value is not guarenteed to go into the table yet.
            //          If there is any space remaining after the item is placed into the bag (for example if item #2 has weight of 2 and the weight limitation
            //          is 3, then there is 1 weight left over).  The reaining weight is looked up in the previous row for the best solution.  So we also use the value
            //          in the previous row for weight of 1.  This value is added to the value for #2.
            //          Finally, this value (item #2 value + item #1 value for remaining weight) is added together and 
            //
            //
            //Additional explaination:
            //
            //  The Rows and columns in the solution array represent the item number and the weights for the knapsack
            //
            //  1. An array is created to hold the solution values.  
            //      The rows and columns in the array represent incremental solutions to the problem
            //              Weight ->     
            //      Items   1   2   3   4   5
            //          0
            //          1
            //          2
            //          3
            //          
            //
            //  2. The first row of the array is filled with 0's
            //      The purpose of this is that since each row represents the solution for a specific item being placed into the knapsack
            //      As such, the item with index of 0 is the 'empty set' which is no item.  Regardless of the weight, it will never be used
            //          since it does not exist.  So the first row is all 0's.
            //
            //              Weight ->
            //      Items   1   2   3   4   5                   <-- Max weight of 5
            //          0   0   0   0   0   0
            //          1
            //          2
            //          3
            //             
            //
            //
            //      Consider:
            
            //          Item #      1      x2       3x
            //          Value       5      x3       4x
            //          Weight      3      x2       1x
            //                     ^^^     Do Not exist when looking at item #1
            //
            //  3.  The 2nd row, with Item index of 1, iterates across the weights from 1 through 5
            //      To fill this in we are ONLY looking at the Item #1.  The other items do not exist
            //      
            //      Iterating across the weight and find
            //        Weights
            //              1   -   1 < 3 --> will not fit                                                                          <-- a0 below
            //              2   -   2 < 3 --> will not fit                                                                          <-- b0 below
            //              3   -   3 = 3 --> will fit and item has value of 5.  5 > 0 (item above it) so set to 5                  <-- c5 below
            //              4   -   4 > 3 --> will fit and item has value of 5.                                                     
            //                      4 - 3 = 1, so we have space for 1 more item.  Find solution in previous row for weight of 1
            //                      previous row solution is row 0, weight 1.  This has a value of 0.
            //                      5 + 0 (from previous row soluton) = 5
            //                      5 > 0 (value above it) so set 5                                                                 <-- d5 below
            //              5   -   5 > 3 --> will fit and item has vlaue of 5.  
            //                      5 - 3 = 2, so we have space for 2 more item.  Find solution in previous row for weight of 2
            //                      previous row solution is row 0, weight 2.  This has a value of 0.
            //                      5 + 0 (from previous row soluton) = 5
            //                      5 > 0 (value above it) so set 5                                                                 <-- e5 below
            // 
            //              Weight ->
            //      Items   1   2   3   4   5                   <-- Max weight of 5
            //          0   0   0   0   0   0
            //          1  a0  b0  c5  d5  e5                   <--  Values set as described above for a,b,c,d,e             
            //          2
            //          3
            //              
            //
            //  4.  The 3rd row, with Item index of 2, iterates across the weights from 1 through 5
            //      To fill this in we are ONLY looking at the Item #2.  The other items do not exist
            //
            //          Item #     x1       2       3x
            //          Value      x5       3       4x
            //          Weight     x3       2       1x
            //                  Not Exist  ^^^     Not Exist
            //      
            //      Iterating across the weight and find
            //        Weights
            //              1   -   1 < 2 --> will not fit.  Set to 0                                                               <-- f0 below
            //              2   -   2 = 2 --> will fit and item has value of 3.  3 > 0 (item above it) so set to 3                  <-- g3 below
            //              3   -   3 > 2 --> will fit and item has value of 3.                                                     
            //                      3 - 2 = 1, so we have space for 1 more item.  Find solution in previous row for weight of 1
            //                      previous row solution is row 1, weight 1.  This has a value of a0.
            //                      3 + a0 (from previous row soluton) = 3
            //                      3 < c5 (value above it) so set 5                                                                <-- h5 below
            //              4   -   4 > 2 --> will fit and item has value of 3.                                                     
            //                      4 - 2 = 2, so we have space for 2 more item.  Find solution in previous row for weight of 2
            //                      previous row solution is row 1, weight 2.  This has a value of b0.
            //                      3 + b0 (from previous row soluton) = 3
            //                      3 < d5 (value above it) so set 5                                                                <-- i5 below
            //              5   -   5 > 2 --> will fit and item has vlaue of 3.  
            //                      5 - 2 = 3, so we have space for 3 more item.  Find solution in previous row for weight of 3
            //                      previous row solution is row 1, weight 3.  This has a value of c5.
            //                      3 + c5 (from previous row soluton) = 8
            //                      8 > e5 (value above it) so set 8                                                                <-- j8 below
            // 
            //              Weight ->
            //      Items   1   2   3   4   5                   <-- Max weight of 5
            //          0   0   0   0   0   0
            //          1  a0  b0  c5  d5  e5                               
            //          2  f0  g3  h5  i5  j8                   <--  Values set as described above for f,g,h,i,j
            //          3
            //              
            //  5.  The 4th row, with Item index of 3, iterates across the weights from 1 through 5
            //      To fill this in we are ONLY looking at the Item #3.  The other items do not exist
            //
            //          Item #     x1      x2       3
            //          Value      x5      x3       4
            //          Weight     x3      x2       1
            //                  Not Exist Not Exist ^^^
            //      
            //      Iterating across the weight and find
            //        Weights
            //              1   -   1 = 1 --> will fit and item has value of 4.                                                     
            //                      1 - 1 = 0, so no space left over.  It fill the bag completly
            //                      4 > f0 (value above it) so set 4                                                                <-- k4 below           
            //              2   -   2 > 1 --> will fit and item has value of 4.                                          
            //                      2 - 1 = 1, so we have space for 1 more item.  Find solution in previous row for weight of 1
            //                      previous row solution is row 2, weight 1.  This has a value of f0.
            //                      4 + f0 (from previous row soluton) = 4                                                          
            //                      4 > g3 (value above it) so set 4                                                                <-- l4 below
            //              3   -   3 > 1 --> will fit and item has value of 4.                                                     
            //                      3 - 1 = 2, so we have space for 2 more item.  Find solution in previous row for weight of 2
            //                      previous row solution is row 2, weight 2.  This has a value of g3.
            //                      4 + g3 (from previous row soluton) = 7
            //                      7 > h5 (value above it) so set 7                                                                <-- m7 below
            //              4   -   4 > 1 --> will fit and item has value of 4.                                                     
            //                      4 - 1 = 3, so we have space for 3 more item.  Find solution in previous row for weight of 3
            //                      previous row solution is row 2, weight 3.  This has a value of h5.
            //                      4 + h5 (from previous row soluton) = 9
            //                      9 > i5 (value above it) so set 9                                                                <-- n9 below
            //              5   -   5 > 1 --> will fit and item has vlaue of 4.  
            //                      5 - 1 = 4, so we have space for 4 more item.  Find solution in previous row for weight of 4
            //                      previous row solution is row 2, weight 4.  This has a value of i5.
            //                      4 + i5 (from previous row soluton) = 9
            //                      9 > j8 (value above it) so set 9                                                                <-- o9 below
            // 
            //              Weight ->
            //      Items   1   2   3   4   5                   <-- Max weight of 5
            //          0   0   0   0   0   0
            //          1  a0  b0  c5  d5  e5                   
            //          2  f0  g3  h5  i5  j8       
            //          3  k4  l4  m7  n9  o9                   <--  Values set as described above for k,l,m,n,o
            //              

            //  6. Now that all items and weights have been iterated over, the solution to the first question is just
            //      the solutions array [3,5] = o9 = 9.  The maximum value that can be placed into the bag of weight 5
            //      with items 1, 2, and 3 is 9.  But which items?
            //
            //      


            int max(int a, int b) { return (a > b) ? a : b; }

            public void KnapSack(int[] v_arr, int[] w_arr, int n, int W)
            {
                //size and value must be the same size
                if (v_arr.Length != w_arr.Length) return;

                //W is the maximum capacity of the container

                //n is the number of items (same as size.length and value.length)
                //but it is easier if size and value start at index 1

                int[,] V = new int[n + 1, W+1];     //V holds the sub solutions for each weight and items used

                int [,]keep = new int[n+1, W+1];    //keep holds if the item should be part of the solution set.  This allows us to backtrace to find the items

                int i = 0;
                int w = 0;
                for (w = 0; w < W; w++)             //Initialize the sub solutions where there are no items.  Regardless of the weight (0 < w < W)
                    V[0,w] = 0;                     //  No items can be used, since no items are available to be used


                //Starting with i=1, we solve for each weight from 0 < w <=W.  If item i will fit into the container of weight W,
                //then we check to see if any other items will fit into the remaining space.  The remaining space is the left over
                //and can be calculated with the previous row.
                //
                //


                for (i = 1; i <= n; i++)            
                {
                    for (w = 1; w <= W; w++)
                    {
                        if (w_arr[i] <= w)
                        {
                            
                            V[i, w] = max(V[i - 1, w], v_arr[i] + V[i - 1, w - w_arr[i]]);

                            if (V[i,w] > V[i-1,w])
                                keep[i, w] = 1;     //Keep the item if the new one is greater than the item above it
                            else
                                keep[i, w] = 0;
                        }
                        else
                        {
                            V[i, w] = V[i - 1, w];
                            keep[i, w] = 0;
                        }
                    }
                }

                
                //Priting matrix for debugging
                Console.WriteLine("----------------------------------");
                for (i = 0; i <= n; i++)
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


                Console.WriteLine("-------------KEEP---------------------");
                for (i = 0; i <= n; i++)
                {
                    Console.WriteLine();
                    for (int j = 1; j <= W; j++)
                    {
                        if (j != 0)
                        {
                            Console.Write(" ");
                            if (keep[i, j] < 10)
                                Console.Write(" ");
                        }
                        Console.Write(keep[i, j]);
                    }
                }
                Console.WriteLine();
                Console.WriteLine("----------------------------------");

                Console.WriteLine("Max value: " + V[n, W]);


                Console.WriteLine("----Solution Set-----");
                int K = W;
                for (i = n; i >= 1; i--)
                {
                    if (keep[i, K] == 1)
                    {
                        //Console.WriteLine(i);
                        Console.WriteLine("i:"+i+" K:"+K + " --> "+v_arr[i] + " --> " + w_arr[i]);
                        K = K - w_arr[i];
                    }
                }

            }
        }

        public void Run()
        {
            //int[] size = new int[5];
            //size[0] = 5;
            //size[1] = 3;
            //size[2] = 2;
            //size[3] = 6;
            //size[4] = 2;

            //int[] value = new int[5];
            //value[0] = 3;
            //value[1] = 5;
            //value[2] = 6;
            //value[3] = 1;
            //value[4] = 7;

            //int maxSize = 10;

            Solution1 s = new Solution1();

            //s.Run(size, value, maxSize, size.Length);


            int[] v = new int[5];
            int[] w = new int[5];
            v[1] = 10;
            v[2] = 40;
            v[3] = 30;
            v[4] = 50;

            w[1] = 5;
            w[2] = 4;
            w[3] = 6;
            w[4] = 3;
            s.KnapSack(v, w, 4, 10);

            v = new int[4];
            w = new int[4];
            v[1] = 5;
            v[2] = 3;
            v[3] = 4;

            w[1] = 3;
            w[2] = 2;
            w[3] = 1;

            s.KnapSack(v, w, 3, 5);



        }
    }
}
