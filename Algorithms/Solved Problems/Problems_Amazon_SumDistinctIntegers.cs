using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_Amazon_SumDistinctIntegers
    {
        /*
         * Given an array of random integers and a sum value, find two numbers from the array that sum up to the given sum. 
            eg. array = {2,5,3,7,9,8};	sum = 11 
            output -> 2,9 

            Implement in O(n) time complexity. Find all distinct pairs. (2,9) and (9,2) are not distinct.
         */


        public void FindDistinctIntegers( int []array, int sum)
        {
            Dictionary<int, bool> values = new Dictionary<int, bool>();

            for (int x=0; x < array.Length; x++)
            {
                int val = array[x];

                int needValue = sum - val;

                if (values.ContainsKey(needValue))
                {
                    Console.WriteLine("(" + val + ", " + needValue + ")");
                }

                if (values.ContainsKey(val) == false)
                    values.Add(val, true);
            }

        }


        public void Run()
        {
            FindDistinctIntegers(new int[] { 2, 5, 3, 7, 9, 8, 5 }, 10);
        }

    }
}
