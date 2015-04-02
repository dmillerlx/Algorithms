using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_LinkedIn_FindMaxContigousSum
    {
        public int FindSum(int []arr)
        {

            int maxSum = 0;
            int sum = 0;

            for (int x=0; x < arr.Length; x++)
            {
                sum += arr[x];

                if (sum > maxSum)
                    maxSum = sum;
                else if (sum < 0)
                    sum = 0;
            }

            return maxSum;

        }

        public void Run()
        {
            Console.WriteLine("Max Sum: " + FindSum(new int[] { 1, 2, -4, 1, 3, -2, 3, -1 }));

            Console.WriteLine("Max Sun: " + FindSum(new int[] { 2, 3, -8, -1, 2, 4, -2, 3 }));

        }


    }
}
