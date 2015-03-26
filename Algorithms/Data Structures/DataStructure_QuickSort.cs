using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    class DataStructure_QuickSort
    {
        //This quick sort implementation is based on the the code from Geeks For Geeks
        //http://www.geeksforgeeks.org/iterative-quick-sort/
        //

        public class QuickSort<I> where I : IComparable
        {

            #region Geeks for Geeks Implementation

            // A typical recursive implementation of quick sort 

            // This function takes last element as pivot, places the pivot element at its
            //   correct position in sorted array, and places all smaller (smaller than pivot)
            //   to left of pivot and all greater elements to right of pivot 
            private int partition(I[] arr, int l, int h)
            {
                I x = arr[h];
                int i = (l - 1);

                for (int j = l; j <= h - 1; j++)
                {
                    if (arr[j].CompareTo(x) <= 0) //arr[j] <= x)
                    {
                        i++;
                        swap(ref arr[i], ref arr[j]);
                    }
                }
                swap(ref arr[i + 1], ref arr[h]);
                return (i + 1);
            }

           

            //QuickSort - Recursive quick sort algorithm
            //Parameters: A[] --> Array to be sorted, l  --> Starting index, h  --> Ending index
            private void quickSort(I[] A, int l, int h)
            {
                if (l < h)
                {
                    int p = partition(A, l, h); /* Partitioning index */
                    quickSort(A, l, p - 1);
                    quickSort(A, p + 1, h);
                }
            }

            //QuickSort - Iterative quick sort algorithm
            //Parameters: A[] --> Array to be sorted, l  --> Starting index, h  --> Ending index
            void quickSortIterative(I[] arr, int l, int h)
            {
                // Create an auxiliary stack
                int[] stack = new int[h - l + 1];

                // initialize top of stack
                int top = -1;

                // push initial values of l and h to stack
                stack[++top] = l;
                stack[++top] = h;

                // Keep popping from stack while is not empty
                while (top >= 0)
                {
                    // Pop h and l
                    h = stack[top--];
                    l = stack[top--];

                    // Set pivot element at its correct position in sorted array
                    int p = partition(arr, l, h);

                    // If there are elements on left side of pivot, then push left
                    // side to stack
                    if (p - 1 > l)
                    {
                        stack[++top] = l;
                        stack[++top] = p - 1;
                    }

                    // If there are elements on right side of pivot, then push right
                    // side to stack
                    if (p + 1 < h)
                    {
                        stack[++top] = p + 1;
                        stack[++top] = h;
                    }
                }
            }


            /// <summary>
            /// Performs an inplace quick sort on the array data
            /// </summary>
            /// <param name="?"></param>
            public void Sort(I []data, bool useIterative) 
            {
                if (useIterative)
                    quickSortIterative(data, 0, data.Length-1);
                else
                    quickSort(data, 0, data.Length - 1);
            }

            #endregion


            #region My Implementation

            public void mySort(I[] array)
            {

                sortSection(array, 0, array.Length - 1);

            }

            bool showDebugginMessages = false;

            private void sortSection(I[] array, int startIndex, int endIndex)
            {
                if (showDebugginMessages) Console.WriteLine("  sortSelection " + startIndex + " , " + endIndex);
                if (endIndex <= startIndex) return;

                //Pick right most character as pivot character.  This element will be in the correct position
                //and all elements less than this element will be to the left and all elements greater
                //will be to the right

                //So, given that we chose the right most character, we will iterate through the entire list from left to right
                //and any items with a value less than this value will be placed towards the left side of the list.
                //We will run 'x' from 0 to the endIndex
                //pivotIndex will start at 0, and every time we find a value less than the selected element we will swap it into
                //this 'pivotIndex' position and increment the 'pivotIndex' value.  
                //
                //At the end, we will swap element from the end into the 'pivotIndex' field as all values to the left of that position
                //will be lower than the pivotValue, and all elements to the right will be greater than or equal to the pivotValue
                //
                //Next we will recursivly check the left and right sections of the array and do the same process.  The pivotIndex
                //does not get checked again since it is guarenteed to be in the correct position.
                //
                //
                //Running time:  Average    O(n logn)
                //               Worse Case O (n^2)

                int pivotIndex = startIndex;
                I pivotValue = array[endIndex];

                //Loop over items between start and end index
                for (int x = startIndex; x < endIndex; x++)
                {
                    //if item at position x is less than pivotItem
                    //swap it into the pivotIndex spot and increment the pivot index
                    if (array[x].CompareTo(pivotValue) < 0)
                    {
                        swap(ref array[x], ref array[pivotIndex]);
                        pivotIndex++;
                    }
                }
                
                //Finally swap the pivot element to its correct sorted location in the list                
                swap(ref array[endIndex], ref array[pivotIndex]);//+1]);

                //Now recursivly sort the left and right partitions based on the location
                //the pivot element was swapped into
                sortSection(array, startIndex, pivotIndex-1);// - 1);
                sortSection(array, pivotIndex+1, endIndex);// + 1, endIndex);

            }


            //Swaps 2 values by reference
            private void swap(ref I a, ref I b)
            {
                I temp = b;
                b = a;
                a = temp;
            }

            #endregion


        }


        // A utility function to print contents of arr
        void printArr(int[] arr)
        {
            int i;
            for (i = 0; i < arr.Length; ++i)
                Console.WriteLine(arr[i]);
        }

        

 

        public void Run()
        {
            //int[] tst = { 100, 1, 9, 10, 23, 4, 8, 25, 6, 5 };
            //QuickSort<int> newTest = new QuickSort<int>();
            //printArr(tst);
            //newTest.mySort(tst);
            ////newTest.Sort(tst, false);
            //Console.WriteLine("New sort vals:");
            //printArr(tst);

            //return;

            Random rnd = new Random((int)DateTime.Now.Ticks);
            int[] vals;

            //Create random unsorted list
            //vals = new int[rnd.Next(1000)];//rnd.Next(10)];
            //Console.WriteLine("Sort list length: " + vals.Length);
            //for (int x = 0; x < vals.Length; x++)
            //{
            //    vals[x] = rnd.Next(1000);
            //}

            QuickSort<int> quickSort = new QuickSort<int>();

            //Console.WriteLine("Unsorted Random Values:");
            //printArr(vals);
            //Console.WriteLine("Performing quick sort using recursive algorithm...");
            //quickSort.Sort(vals, false);
            //Console.WriteLine("Sorted Vals:");
            //printArr(vals);

            //Console.WriteLine("------------------------");

            ////Create random unsorted list
            //vals = new int[rnd.Next(rnd.Next(100))];

            //for (int x = 0; x < vals.Length; x++)
            //{
            //    vals[x] = rnd.Next(1000);
            //}

            ////QuickSort<int> quickSort = new QuickSort<int>();

            //Console.WriteLine("Unsorted Random Values:");
            //printArr(vals);
            //Console.WriteLine("Performing quick sort using iterative algorithm...");
            //quickSort.Sort(vals, true);
            //Console.WriteLine("Sorted Vals:");
            //printArr(vals);



            //Create random unsorted list
            vals = new int[rnd.Next(rnd.Next(5000000))];
            Console.WriteLine("Sort list length: " + vals.Length);

            for (int x = 0; x < vals.Length; x++)
            {
                vals[x] = rnd.Next(10000);
            }

            Console.WriteLine("Unsorted Random Values:");
            //printArr(vals);
            Console.WriteLine("Performing quick sort using recursive algorithm...");
            DateTime startTime = DateTime.Now;
            quickSort.mySort(vals);
            DateTime endTime = DateTime.Now;
            Console.WriteLine("Sorted Vals:");
            //printArr(vals);

            bool sortFailed = false;
            for (int x = 1; x < vals.Length; x++)
            {
                if (vals[x] < vals[x - 1])
                {
                    Console.WriteLine("Sort failed.  Index [" + x + "] = " + vals[x] + "  Index[" + (x - 1) + "] = " + vals[x - 1]);
                    sortFailed = true;
                }
            }

            if (sortFailed == false)
                Console.WriteLine("Sort Verified to be correct");
            Console.WriteLine("Done");

            Console.WriteLine("Sort time: " + (endTime - startTime).TotalSeconds);
        }

    }
}
