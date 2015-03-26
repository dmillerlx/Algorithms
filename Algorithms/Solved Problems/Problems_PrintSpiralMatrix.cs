using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Algorithms
{
    class Problems_PrintSpiralMatrix
    {

        public void Run()
        {
            int[,] data = new int[,]{
                {1,2,3,4},
                {5,6,7,8},
                {9,10,11,12}};

            PrintSpiralMatrix(data, 3, 4);

            reverse = true;
            PrintSpiralMatrix(data, 3, 4);
            reverse = false;

            return;

            data = new int[,]{
                {1,2,3,4}
            };

            PrintSpiralMatrix(data, 1, 4);

            data = new int[,]{
                {1},
                {2},
                {3},
                {4}
            };
            PrintSpiralMatrix(data, 4, 1);

            data = new int[,]{
                {1,2,3,4},
                {5,6,7,8},
                {9,10,11,12},
                {13,14,15,16},
                {17,18,19,20}                
            };

            PrintSpiralMatrix(data, 5, 4);

            data = new int[,]{
               {1,2,3,4,5,6,7,8,9,10},
               {11,12,13,14,15,16,17,18,19,20},
               {21,22,23,24,25,26,27,28,29,30},
               {31,32,33,34,35,36,37,38,39,40},
               {41,42,43,44,45,46,47,48,49,50},
               {51,52,53,54,55,56,57,58,59,60}

            
            };

            PrintSpiralMatrix(data, 6, 10);

        }

        bool reverse = false;
        public Stack<int> intStack = new Stack<int>();
        public void outputValue(int val)
        {
            if (reverse)
            {
                intStack.Push(val);
            }
            else
            {
                Debug.WriteLine(val);
            }
        }


        public void PrintSpiralMatrix(int[,] data, int M, int N)
        {
            Debug.WriteLine("----------Start Matrix size: M = " + M + " N = " + N);
            for (int m = 0; m < M; m++)
            {
                Debug.Write("{");
                for (int n = 0; n < N; n++)
                {
                    if (n != 0)
                        Debug.Write(", ");
                    Debug.Write(data[m, n]);
                }
                Debug.Write("}");
                Debug.WriteLine("");
            }


            Debug.WriteLine("Sprial Print:");

            try
            {

                if (M == 1)
                {
                    for (int n = 0; n < N; n++)
                        outputValue(data[M - 1, n]);
                    return;
                }

                if (N == 1)
                {
                    for (int m = 0; m < M; m++)
                        outputValue(data[m, N - 1]);
                    return;
                }

                int layers = Math.Min(M / 2, N / 2);
                for (int layer = 0; layer <= layers; layer++)
                {
                    for (int x = 0 + layer; x <= N - layer - 1; x++)
                        outputValue(data[layer, x]);

                    for (int y = layer + 1; y < M - layer - 1; y++)
                        outputValue(data[y, N - layer - 1]);

                    if (M - layer - 1 > layer)
                    {
                        for (int x = N - layer - 1; x >= layer; x--)
                            outputValue(data[M - layer - 1, x]);
                    }

                    if (M - layer > layer)
                    {
                        for (int y = M - layer - 1 - 1; y > layer; y--)
                            outputValue(data[y, layer]);
                    }
                }
            }
            finally
            {
                if (reverse)
                {
                    Debug.WriteLine("----Revese---");
                    while (intStack.Count > 0)
                    {
                        Debug.WriteLine(intStack.Pop());
                    }
                }

            }
        }


    }
}
