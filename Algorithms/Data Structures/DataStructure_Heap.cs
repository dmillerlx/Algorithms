using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    //Heap implementation for Int's
    public class HeapInt
    {
        public enum heapTypeEnum { min, max };
        public heapTypeEnum heapType = heapTypeEnum.min;

        public HeapInt(heapTypeEnum heapType)
        {
            this.heapType = heapType;
        }

        int[] data = null;
        public int size = 0;
        int headIndex = 1; //starting position for heap

        public void BuildHeap(int[] vals)
        {
            size = 0;
            data = new int[vals.Length+1];
            for (int x = 0; x < data.Length; x++)
            {
                data[x] = -1;
            }

            
            foreach (int v in vals)
            {
                Enqueue(v);
            }                       
        }

        public void Enqueue(int v)
        {
            if (size >= data.Length -1)
            {
                DoubleHeapSize();
            }

            data[headIndex + size] = v;
            PercolateUp();
            size++;
        }

        public int Dequeue()
        {
            int ret = data[headIndex];
            data[headIndex] = data[headIndex + size -1];
            data[headIndex + size - 1] = -1;
            size--;
            PercolateDown();

            return ret;
        }

        public void DoubleHeapSize()
        {
            int []newData = new int[data.Length * 2 + 1];
            for (int x = 0; x < newData.Length; x++)
            {
                newData[x] = -1;
            }

            Array.Copy(data, newData, data.Length);

            data = newData;
        }

        public void PercolateDown()
        {
            int i = headIndex; //set i to the start of the heap...default position is 1
            int left, r, max, tmp;			// declare variables
            max = 0;

            bool done = true;
            do
            {
                done = true;

                left = 2 * i;// +1;  			// left child
                r = 2 * i + 1;// +2;       			// right child

                if (heapType == heapTypeEnum.max)
                {
                    if (left <= size && data[left] > data[i])		// find smallest child
                        max = left;             	// save index of smaller child
                    else
                        max = i;

                    if (r <= size && data[r] > data[max])
                        max = r;           		// save index of smaller child
                }
                else if (heapType == heapTypeEnum.min)
                {
                    if (left <= size && data[left] < data[i])		// find smallest child
                        max = left;             	// save index of smaller child
                    else
                        max = i;

                    if (r <= size && data[r] < data[max])
                        max = r;           		// save index of smaller child
                }

                if (max != i)	 			// swap and percolate, if necessary
                {
                    tmp = data[i];      		// exchange values at two indices
                    data[i] = data[max];
                    data[max] = tmp;
                    //HeapifyMax(max); 			// call Heapify  -- not doing this since it is recursive

                    i = max;
                    done = false;

                }// end if

            } while (!done);


        }

        public void PercolateUp()
        {
            int pos = headIndex + size;
            int current = pos;
            int parent = pos / 2;

            bool done = false;
            do
            {
                done = true;
                parent = current / 2;
                if (current > headIndex)
                {
                    if (
                        (heapType == heapTypeEnum.max && data[current] > data[parent])
                        ||
                        (heapType == heapTypeEnum.min && data[current] < data[parent])
                    )
                    {
                        //swap
                        int tmp = data[current];
                        data[current] = data[parent];
                        data[parent] = tmp;
                        done = false;
                        current = parent;
                    }
                }

            } while (!done);

        }
    }


    //Heap implementation as generic, but only accepting types that implement IComparable
    public class Heap<I> where I: IComparable
    {
        public enum heapTypeEnum { min, max };
        public heapTypeEnum heapType = heapTypeEnum.min;
        
        public Heap(heapTypeEnum heapType)
        {
            this.heapType = heapType;

            data = new I[100];            
        }

        I[] data = null;        
        public int size = 0;
        int headIndex = 1; //starting position for heap

        public void BuildHeap(I[] vals)
        {
            size = 0;
            data = new I[vals.Length+1];
            for (int x = 0; x < data.Length; x++)
            {
                data[x] = default(I);//-1;
            }

            
            foreach (I v in vals)
            {
                Enqueue(v);
            }                       
        }

        public void Enqueue(I v)
        {
            if (size >= data.Length -1)
            {
                DoubleHeapSize();
            }

            data[headIndex + size] = v;
            PercolateUp();
            size++;
        }

        public I Dequeue()
        {
            I ret = data[headIndex];
            data[headIndex] = data[headIndex + size -1];
            data[headIndex + size - 1] = default(I);// -1;
            size--;
            PercolateDown();

            return ret;
        }

        public I Peek()
        {
            I ret = data[headIndex];
            
            return ret;
        }

        public void DoubleHeapSize()
        {
            I []newData = new I[data.Length * 2 + 1];
            for (int x = 0; x < newData.Length; x++)
            {
                newData[x] = default(I);//-1;
            }

            Array.Copy(data, newData, data.Length);

            data = newData;
        }

        public void PercolateDown()
        {
            int i = headIndex; //set i to the start of the heap...default position is 1
            int left, r, max;
            I tmp;			// declare variables
            max = 0;

            bool done = true;
            do
            {
                done = true;

                left = 2 * i;// +1;  			// left child
                r = 2 * i + 1;// +2;       			// right child

                if (heapType == heapTypeEnum.max)
                {
                    if (left <= size && data[left].CompareTo(data[i]) > 0)// data[left] > data[i])		// find smallest child
                        max = left;             	// save index of smaller child
                    else
                        max = i;

                    if (r <= size && data[r].CompareTo(data[max]) > 0)// data[r] > data[max])
                        max = r;           		// save index of smaller child
                }
                else if (heapType == heapTypeEnum.min)
                {
                    if (left <= size && data[left].CompareTo(data[i]) < 0)//data[left] < data[i])		// find smallest child
                        max = left;             	// save index of smaller child
                    else
                        max = i;

                    if (r <= size && data[r].CompareTo(data[max]) < 0)// data[r] < data[max])
                        max = r;           		// save index of smaller child
                }

                if (max != i)	 			// swap and percolate, if necessary
                {
                    tmp = data[i];      		// exchange values at two indices
                    data[i] = data[max];
                    data[max] = tmp;
                    //HeapifyMax(max); 			// call Heapify  -- not doing this since it is recursive

                    i = max;
                    done = false;

                }// end if

            } while (!done);


        }

        public void PercolateUp()
        {
            int pos = headIndex + size;
            int current = pos;
            int parent = pos / 2;

            bool done = false;
            do
            {
                done = true;
                parent = current / 2;
                if (current > headIndex)
                {
                    if (
                        (heapType == heapTypeEnum.max && data[current].CompareTo(data[parent]) > 0)// data[current] > data[parent])
                        ||
                        (heapType == heapTypeEnum.min && data[current].CompareTo(data[parent]) < 0)//data[current] < data[parent])
                    )
                    {
                        //swap
                        I tmp = data[current];
                        data[current] = data[parent];
                        data[parent] = tmp;
                        done = false;
                        current = parent;
                    }
                }

            } while (!done);

        }

        //Heap Sort
        //Dequeue entry and place at end of array (position size+1)
        //This is an inplace sort and so the heap will be dequeued and the sorted data will be in the array
        //Since the array can be larger than the internal size of the array, return the number of elements used
        //as sizeOfArray
        public I[] Sort(out int sizeOfArray)
        {
            sizeOfArray = size;

            while (size > 0)
            {
                I val = Dequeue();
                data[size + 1] = val;
            }

            return data;
        }
    }


}
