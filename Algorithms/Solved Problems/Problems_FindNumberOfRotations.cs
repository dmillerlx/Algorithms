using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    class Problems_FindNumberOfRotations
    {
        public int FindRotation(int[] val, int len)
        {
            Console.WriteLine("FindRotation:");
            int iterations = 0;

            try
            {

                if (len == 0) return -1;
                if (len == 1) return 0;

                for (int x = 0; x < len - 1; x++)
                {
                    iterations++;

                    if (val[x] > val[x + 1])
                        return x + 1;

                }

                return 0;
            }
            finally
            {
                Console.WriteLine("  Iterations to find solution: " + iterations);
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

        public void ShowArray(int[] val)
        {
            for (int x=0; x < val.Length; x++)
            {
                if (x > 0)
                    Console.Write(", ");
                Console.Write(val[x]);
            }
            Console.WriteLine();
        }

        public void Rotate(int []vals, int NumberOfRotations)
        {
            for (int x=0; x < NumberOfRotations; x++)
            {
                int tmp = -1;
                for (int i = 1; i < vals.Length; i++)
                {           
                    

                    tmp = vals[i];

                    vals[i] = vals[i - 1];

                    //if (i == 0)
                    //{
                    //    vals[i] = vals[vals.Length - 1];
                    //}

                    //vals[i + 1] = vals[i];

                }
            }
        }

        /*Function to left rotate arr[] of size n by d*/
        void leftRotate(int[] arr, int d, int n)
        {
            int i;
            for (i = 0; i < d; i++)
                leftRotatebyOne(arr, n);
        }

        void leftRotatebyOne(int[] arr, int n)
        {
            int i, temp;
            temp = arr[0];
            for (i = 0; i < n - 1; i++)
                arr[i] = arr[i + 1];
            arr[i] = temp;
        }

        public void Run()
        {

            //
            //Small value test
            //

            int []vals = {1,2,3,4,5,6};
            ShowArray(vals);

            leftRotate(vals, 2, vals.Length);

            ShowArray(vals);

            Console.WriteLine("Rotation point: "+ FindRotation(vals, vals.Length));
            Console.WriteLine("Rotation point: " + FindRotation_BinarySearch(vals, vals.Length));

            int []vals2 = {4,5,6,1,2,3,4};
            ShowArray(vals2);
            Console.WriteLine("Rotation point: "+ FindRotation(vals2, vals2.Length));
            Console.WriteLine("Rotation point: " + FindRotation_BinarySearch(vals2, vals2.Length));

            int []vals3 = {2,3,4,5,6,1};
            ShowArray(vals3);
            Console.WriteLine("Rotation point: "+ FindRotation(vals3, vals3.Length));
            Console.WriteLine("Rotation point: " + FindRotation_BinarySearch(vals3, vals3.Length));

            int []vals4 = {5,6,1,2,3,4};
            ShowArray(vals4);
            Console.WriteLine("Rotation point: "+ FindRotation(vals4, vals4.Length));
            Console.WriteLine("Rotation point: " + FindRotation_BinarySearch(vals4, vals4.Length));


            //
            //Large value test
            //
            Random rnd = new Random((int)System.DateTime.Now.Ticks);

            int numOfVals = rnd.Next(1000);

            int[] arr = new int[numOfVals];
            for (int x = 0; x < numOfVals; x++)
            {
                arr[x] = x;
            }

            ShowArray(arr);

            int numOfRotate = rnd.Next(200);

            leftRotate(arr, numOfRotate, arr.Length);

            ShowArray(arr);

            Console.WriteLine("Rotation point: " + FindRotation(arr, arr.Length));
            Console.WriteLine("Rotation point: " + FindRotation_BinarySearch(arr, arr.Length));


        }


    }
}
