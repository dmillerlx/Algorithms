using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_Microsoft_Merge_and_Sort_M_N_Sorted_Array
    {
        //You have two arrays, one of length n of sorted elements, one of length m + n with the first m elements sorted and the last n empty. 
        //What's the best way to merge all elements into the m + n length array in sorted order? 

        public void MergeArrays(int []a, int []b, int M, int N)
        {
            int len2 = M + N;
            for (int len = M+N -1; len >= 0; len--)
            {                
                bool useA = false;
                if (M > 0 && N > 0)
                {
                    if (a[M-1] > b[N-1])
                        useA = true;
                    else useA = false;
                }
                else if (M > 0)
                {
                    useA = true;
                }
                else useA = false;

                if (useA)
                {
                    a[len] = a[M-1];
                    M--;
                }
                else
                {
                    a[len] = b[N-1];
                    N--;
                }
            }

            for (int x=0; x < len2; x++)
            {
                Console.WriteLine(a[x]);
            }

        }

        public void Run()
        {
            int[] a = new int[10];
            int[] b = new int[4];

            int M = 6;
            int N = 4;

            a[0] = 1;
            a[1] = 4;
            a[2] = 8;
            a[3] = 15;
            a[4] = 17;
            a[5] = 22;

            b[0] = 0;
            b[1] = 9;
            b[2] = 15;
            b[3] = 25;

            MergeArrays(a, b, M, N);





        }


    }
}
