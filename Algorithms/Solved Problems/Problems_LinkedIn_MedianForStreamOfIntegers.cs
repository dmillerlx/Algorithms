using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_LinkedIn_MedianForStreamOfIntegers
    {
        public class Solution1
        {
            public Heap<int> minHeap = new Heap<int>(Heap<int>.heapTypeEnum.min);
            public Heap<int> maxHeap = new Heap<int>(Heap<int>.heapTypeEnum.max);

            public void AddValue(int val)
            {
                if (maxHeap.size == 0 && minHeap.size == 0)
                {
                    maxHeap.Enqueue(val);
                    return;
                }

                if (val <= GetMedian())
                {
                    maxHeap.Enqueue(val);
                }
                else
                {
                    minHeap.Enqueue(val);
                }

                while (minHeap.size > maxHeap.size )
                {
                    maxHeap.Enqueue(minHeap.Dequeue());
                }

                while ((maxHeap.size - minHeap.size) > 1)
                {
                    minHeap.Enqueue(maxHeap.Dequeue());
                }
                

            }

            public double GetMedian()
            {
                if (minHeap.size == 0 && maxHeap.size == 0)
                    return int.MinValue;
                
                if (maxHeap.size == minHeap.size)
                {
                    return ((double)maxHeap.Peek() + (double)minHeap.Peek()) / (double)2;
                }
                else
                {
                    return maxHeap.Peek();
                }

            }


        }

        public void Run()
        {
            Solution1 s1 = new Solution1();

            s1.AddValue(1);
            s1.AddValue(2);
            s1.AddValue(3);
            s1.AddValue(4);
            s1.AddValue(5);
            s1.AddValue(6);
            s1.AddValue(7);
            s1.AddValue(8);
            s1.AddValue(9);
            s1.AddValue(10);

            Console.WriteLine("Median: " + s1.GetMedian());

            s1.AddValue(11);
            Console.WriteLine("Median: " + s1.GetMedian());
            s1.AddValue(12);
            Console.WriteLine("Median: " + s1.GetMedian());



        }

    }
}
