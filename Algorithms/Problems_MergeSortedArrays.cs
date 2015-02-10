using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    class Problems_MergeSortedArrays
    {
        //Assume first array has room to hold both A & B
        public void MergeSortedArrays(char[] A, int m, char[] B, int n)
        {
            for (int i = m + n -1; i >= 0; i--)
            {
                bool useA = false;
                if (m > 0 && n > 0)
                {
                    if (A[m - 1] > B[n - 1])
                        useA = true;
                }
                else if (m > 0 && !(n > 0))
                    useA = true;
                else if (!(m > 0) && n > 0)
                    useA = false;
                else throw new Exception("Should never happen");

                if (useA)
                {
                    A[i] = A[m - 1];
                    m--;
                }
                else
                {
                    A[i] = B[n - 1];
                    n--;
                }
            }

        }

        public int[] MergeKSortedArrays(int[][] data, int k)
        {
            int totalSize = 0;
            int []indexes = new int[k];

            for (int i = 0; i < k; i++)
            {
                totalSize += data[i].Length;
                indexes[i] = data[i].Length - 1;
            }

            int[] ret = new int[totalSize];

            if (k == 0)
                return ret;

            for (int i = totalSize-1; i >= 0; i--)
            {
                int maxIndex = -1;
                for (int j = 0; j < k; j++)
                {
                    if (indexes[j] >= 0)
                    {
                        if (maxIndex < 0)
                            maxIndex = j;
                        else if (data[j][indexes[j]] > data[maxIndex][indexes[maxIndex]])
                            maxIndex = j;
                    }
                }

                ret[i] = data[maxIndex][indexes[maxIndex]];
                indexes[maxIndex]--;
            }

            return ret;

        }

        public class indexItem : IComparable
        {
            public int index { get; set; }
            public int val { get; set; }

            public int CompareTo(object obj)
            {
                indexItem i = (indexItem)obj;
                return val.CompareTo(i.val);
            }
        }

        public int[] MergeKSortedArrays_UsingMaxHeap(int[][] data, int k)
        {
            int totalSize = 0;
            int[] indexes = new int[k];

            //Setup indexes that reference next largest element for each array
            for (int i = 0; i < k; i++)
            {
                totalSize += data[i].Length;
                indexes[i] = data[i].Length - 1;
            }

            //create output array
            int[] ret = new int[totalSize];

            int p = totalSize - 1; //p will point to insertion point

            if (k == 0)
                return ret;

            //Create max heap that will store the next greatest index to select
            Heap<indexItem> h = new Heap<indexItem>(Heap<indexItem>.heapTypeEnum.max);

            for (int i = 0; i < k; i++)
            {
                if (indexes[i] >= 0)
                {
                    h.Enqueue(new indexItem(){index=i, val=data[i][indexes[i]]});
                    indexes[i]--;
                }
            }

            //While the heap is not empty
            //Dequeue an item.  The max item is the indexItem that has the highest value
            //After removing the item from that index, immediatly add a new item from the index
            //we just used
            while (h.size > 0)
            {
                indexItem item = h.Dequeue();

                if (indexes[item.index] >= 0)
                {                    
                    h.Enqueue(new indexItem() { index = item.index, val = data[item.index][indexes[item.index]] });
                    indexes[item.index]--;
                }

                //finally add it to the output list
                ret[p--] = item.val;
            }

            return ret;
        }
    }
}
