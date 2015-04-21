using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_LinkedIn_FindMaxContigousSum
    {
        //Question Description: Write a function that, given a list of integers (both positive and negative) returns the sum
        //of the contiguous subsequence with maximum sum. Thus, given the sequence (1, 2, -4, 1, 3, -2, 3, -1) it should return 5. 

        //Approach
        //

        public int FindSum(int []arr)
        {

            int maxSum = 0;            
            int sum = 0;


            bool allNegative = true;            //Needed for case where all values are negative
            int maxNegative = int.MinValue;

            for (int x=0; x < arr.Length; x++)
            {
                //Check to see if we have a single negative value
                if (arr[x] > 0)
                    allNegative = false;

                if (arr[x] > maxNegative)
                    maxNegative = arr[x];

                //Add value to running sum
                sum += arr[x];

                
                if (sum > maxSum)
                    maxSum = sum;       //New max sum found
                else if (sum < 0)
                    sum = 0;            //Sum went negative so reset to 0
            }

            if (allNegative)
                return maxNegative;

            return maxSum;

        }

        public void Run()
        {
            Console.WriteLine("Max Sum: " + FindSum(new int[] { 1, 2, -4, 1, 3, -2, 3, -1 }));

            Console.WriteLine("Max Sum: " + FindSum(new int[] { 2, 3, -8, -1, 2, 4, -2, 3 }));

            Console.WriteLine("Max Sum: " + FindSum(new int[] { 2, -1, 3, -5, 3 }));

            Console.WriteLine("Max Sum: " + FindSum(new int[] { -2, -1, -3, -5, -3 }));

            Console.WriteLine("Max Sum: " + FindSum(new int[] { -2, -1, -3, 0, -5, -3 }));

            Console.WriteLine("Max Sum: " + FindSum(new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4}));
        
        }


    }
}
