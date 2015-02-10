using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    class MergeSort<I> where I : IComparable
    {
        public MergeSort()//I[] values)
        {
        }

        
        public I[] Sort(I [] data)
        {
            return Merge_Sort(data);
        }

        //Standard recursive merge sort
        //Pass in unsorted list
        //Function will recursively call itself passing smaller subsets
        //When the subsets are only 1 in length, it will start returning and merging the subsets
        //together
        public I[] Merge_Sort(I[] data)
        {
            //Exit case where length is <= 1
            if (data.Length <= 1)
            {
                return data;
            }

            //More than 1 element, so find middle and split into left and right lists
            int middle = data.Length / 2;

            I[] left = new I[middle];
            I[] right = new I[data.Length - middle];

            for (int x = 0; x < middle; x++)
            {
                left[x] = data[x];
            }

            for (int y = middle; y < data.Length; y++)
            {
                right[y - middle] = data[y];
            }

            //Recursively split into left and right
            left = Merge_Sort(left);
            right = Merge_Sort(right);

            //Left and Right will return sorted, so now merge those sorted 
            //lists together and return
            I[] result = Merge(left, right);

            return result;

        }

        //Merge algorithm
        //Creates a new array that is able to hold all the elements from the
        //left and right lists
        //Iterates across both lists and adds the left/right elements into the
        //output list in order
        public I[] Merge(I[] left, I[] right)
        {
            //Create return array 
            I[] ret = new I[left.Length + right.Length];

            //Iterate over destination array and add elements from
            //left or right array.  Use leftIndex and rightIndex to hold
            //the offset in the left/right array of the last character inserted.
            int leftIndex = 0;
            int rightIndex = 0;
            for (int x = 0; x < ret.Length; x++)
            {
                //Use bools to indicate if a left or right item is available
                //or if we have hit the end of the list
                bool hasLeft = false;
                bool hasRight = false;
                if (leftIndex < left.Length)
                    hasLeft = true;

                if (rightIndex < right.Length)
                    hasRight = true;

                I minValue = default(I);

                //Find smallest value from left or right array based on if there are
                //elements in left or right array still available for comparison
                if (hasLeft && !hasRight)
                {
                    minValue = left[leftIndex++];
                }
                else if (hasRight && !hasLeft)
                {
                    minValue = right[rightIndex++];
                }
                else if (hasLeft && hasRight)
                {
                    if (left[leftIndex].CompareTo(right[rightIndex]) < 0)
                    {
                        minValue = left[leftIndex++];
                    }
                    else
                    {
                        minValue = right[rightIndex++];
                    }
                }
                else
                {
                    //this is bad
                    throw new Exception("End of list for left and right but not at end of result");
                }

                //Add value into return array
                ret[x] = minValue;
            }//end For

            return ret;
        }

        //This version does not use recursion
        //It iterates through breaking the array into smaller and smaller arrays
        //until each array has only 1 character, then it merges them together
        public I[] Sort_NoRecursion(I[] data)
        {

            I[] ret = null;

            Stack<I[]> stackOne = new Stack<I[]>();
            Stack<I[]> stackTwo = new Stack<I[]>();

            Stack<I[]> currentStack = null;
            Stack<I[]> nextStack = null;

            if (data.Length <= 1)
            {
                return data;
            }

            int middle = data.Length / 2;

            I[] left = new I[middle];
            I[] right = new I[data.Length - middle];

            for (int x = 0; x < middle; x++)
            {
                left[x] = data[x];
            }

            for (int y = middle; y < data.Length; y++)
            {
                right[y - middle] = data[y];
            }

            I[] item = null;

            stackOne.Push(left);
            stackOne.Push(right);

            currentStack = stackOne;
            nextStack = stackTwo;

            bool done = false;
            bool allSingle = false;
            do
            {
                allSingle = true;
                while (currentStack.Count > 0)
                {
                    item = currentStack.Pop();
                    if (item.Length <= 1)
                    {
                        nextStack.Push(item);
                    }
                    else
                    {
                        allSingle = false;
                        middle = item.Length / 2;

                        left = new I[middle];
                        right = new I[item.Length - middle];

                        for (int x = 0; x < middle; x++)
                        {
                            left[x] = item[x];
                        }

                        for (int y = middle; y < item.Length; y++)
                        {
                            right[y - middle] = item[y];
                        }


                        nextStack.Push(left);
                        nextStack.Push(right);
                    }
                }

                if (allSingle)
                {
                    done = true;
                }
                else
                {
                    Stack<I[]> tmp = currentStack;
                    currentStack = nextStack;
                    nextStack = tmp;
                }

            } while (!done);


            Stack<I[]> tmp2 = currentStack;
            currentStack = nextStack;
            nextStack = tmp2;


            done = false;
            do
            {
                while (currentStack.Count > 0)
                {
                    I[] item1 = currentStack.Pop();

                    I[] item2 = null;
                    if (currentStack.Count > 0)
                    {
                        item2 = currentStack.Pop();
                    }

                    I[] merged = null;
                    if (item2 != null)
                    {
                        merged = Merge(item1, item2);
                    }
                    else
                    {
                        merged = item1;
                    }
                    nextStack.Push(merged);

                    if (merged.Length == data.Length)
                    {
                        ret = merged;
                        done = true;
                    }
                }

                if (!done)
                {
                    Stack<I[]> tmp = currentStack;
                    currentStack = nextStack;
                    nextStack = tmp;
                }

            } while (!done);

            return ret;

        }

        //This version does not use recursion
        //It is the same as the previous exception that instead of iterating through
        //the array and breaking the arrays into smaller segments with repeative splits
        //it takes each item and places them into a stack.  So the repetive splitting
        //is replaced by just copying each individual element into a stack
        //
        //The second process of merging the arrays together is the same
        public I[] Sort_NoRecursion_v2(I[] data)
        {

            I[] ret = null;

            Stack<I[]> stackOne = new Stack<I[]>();
            Stack<I[]> stackTwo = new Stack<I[]>();

            Stack<I[]> currentStack = null;
            Stack<I[]> nextStack = null;

            I[] item = null;

            currentStack = stackOne;
            nextStack = stackTwo;

            for (int x = 0; x < data.Length; x++)
            {
                item = new I[1];
                item[0] = data[x];
                currentStack.Push(item);
            }
            

            Stack<I[]> tmp2 = currentStack;
            currentStack = nextStack;
            nextStack = tmp2;


            bool done = false;
            do
            {
                while (currentStack.Count > 0)
                {
                    I[] item1 = currentStack.Pop();

                    I[] item2 = null;
                    if (currentStack.Count > 0)
                    {
                        item2 = currentStack.Pop();
                    }

                    I[] merged = null;
                    if (item2 != null)
                    {
                        merged = Merge(item1, item2);
                    }
                    else
                    {
                        merged = item1;
                    }
                    nextStack.Push(merged);

                    if (merged.Length == data.Length)
                    {
                        ret = merged;
                        done = true;
                    }
                }

                if (!done)
                {
                    Stack<I[]> tmp = currentStack;
                    currentStack = nextStack;
                    nextStack = tmp;
                }

            } while (!done);

            return ret;

        }
    }
}
