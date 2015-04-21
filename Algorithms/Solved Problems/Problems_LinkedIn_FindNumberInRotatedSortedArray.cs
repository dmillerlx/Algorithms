using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_LinkedIn_FindNumberInRotatedSortedArray
    {


        //Find number in rotated sorted array

        //Approach
        //First find the rotation point
        //Second do binary search on side that has the element

        // 4,5,1,2,3
        // 2,3,4,5,6,7,8,9,0,1
        // 


        public int FindNumber(int []arr, int val)
        {
            //Find rotation point

            //Binary search on side that has rotation
            int rotationPoint = FindRotation_BinarySearch(arr, arr.Length);

            int start = -1;
            int end = -1;

            //Binary search to find the value
            //First get start and end, based on if the array is roated
            if (rotationPoint > 0)
            {
                //array is rotated, so set start/end to left or right segment
                if (val >= arr[rotationPoint] && val <= arr[arr.Length-1])
                {
                    //value is in right segment, after rotation point
                    start = rotationPoint;
                    end = arr.Length - 1;
                }
                else
                {
                    //value is in left segment, before (and including) roation point
                    start = 0;
                    end = rotationPoint -1;
                }
            }
            else
            {
                //array is not rotated, so use entire array
                start = 0;
                end = arr.Length - 1;
            }

            int mid = -1;// start + end - start;

            while (true)
            {
                mid = start + end - start;
                
                if (arr[mid] == val)
                    return val;
                else if (arr[start] == val)
                    return arr[start];
                else if (arr[end] == val)
                    return arr[end];

                if (end - start <= 1)
                    return int.MinValue; //value not found

                if (val < arr[mid])
                {
                    end = mid - 1;
                }
                else
                {
                    start = mid + 1;
                }                
            }
        }

        public int FindRotation_BinarySearch(int[] val, int len)
        {
            if (len == 0) return -1;
            if (len == 1) return 0;

            int newStart = 0;
            int newEnd = len - 1;

            Console.WriteLine("FindRotation - Binary Search:");
            int iterations = 0;

            try
            {

                while (true)
                {
                    iterations++;

                    int start = newStart;
                    int end = newEnd;
                    int mid = start + (end - start) / 2;

                    if (end - start == 1)
                    {
                        if (val[start] > val[end])
                            return end;
                        return start;
                    }

                    if (start == end)
                    {
                        return end;
                    }

                    if (val[start] > val[mid])
                    {
                        //Solution is in left
                        newStart = start;
                        newEnd = mid;
                    }
                    else if (val[mid + 1] > val[end])
                    {
                        //solution is in right
                        newStart = mid + 1;
                        newEnd = end;
                    }
                    else if (val[end] > val[start])
                    {
                        //Set is ordered, return start
                        return start;
                    }
                    else
                    {
                        //solution is start of right
                        return mid + 1;
                    }
                }

            }
            finally
            {
                Console.WriteLine("  Iterations to find solution: " + iterations);
            }


        }


        public void Run()
        {

            Console.WriteLine(FindNumber(new int[] { 4, 5, 1, 2, 3 }, 3));
            Console.WriteLine(FindNumber(new int[] { 4, 5, 1, 2, 3 }, 6));
            Console.WriteLine(FindNumber(new int[] { 4, 5, 1, 2, 3 }, 5));

            Console.WriteLine(FindNumber(new int[] { 1, 2, 3, 4, 5 }, 4));

        }

    }
}
