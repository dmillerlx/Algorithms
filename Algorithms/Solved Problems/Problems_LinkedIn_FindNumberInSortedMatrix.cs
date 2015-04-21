using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_LinkedIn_FindNumberInSortedMatrix
    {

        //Find a number in a matrix which is sorted by row and column 

        //Approach
        //
        //Iterate from the top right of the matrix      (row =0, col = lastColumn)
        //  If the [row,col] value matches, then return true - we found it!
        //  if the [row,col] value is > the value we want, reduce the column number.
        //      We reduce col since the minimum value for this column is greater than the value
        //      we want to find...so it can't possibly be in this column
        //  Otherwise increment the row because it can still be in that column
        //
        // This algorithm reduces the search a row or column at a time
        //  For a N x M matrix, the running time is O(N + M)


        public bool FindNumberInMatrix(int [,]arr, int val)
        {

            int row = 0;
            int col = arr.GetLength(0) - 1;

            while (row < arr.GetLength(0) && col >= 0)
            {
                if (arr[row, col] == val)
                {
                    return true;
                }
                else if (arr[row, col] > val)
                    col--;
                else row++;
            }

            return false;


        }


        public void Run()
        {
            int[,] arr = new int[4, 4];

            arr[0, 0] = 15;
            arr[0, 1] = 20;
            arr[0, 2] = 40;
            arr[0, 3] = 85;

            arr[1, 0] = 20;
            arr[1, 1] = 35;
            arr[1, 2] = 80;
            arr[1, 3] = 95;

            arr[2, 0] = 30;
            arr[2, 1] = 55;
            arr[2, 2] = 95;
            arr[2, 3] = 105;

            arr[3, 0] = 40;
            arr[3, 1] = 80;
            arr[3, 2] = 100;
            arr[3, 3] = 120;

            Console.WriteLine(FindNumberInMatrix(arr, 55));

            Console.WriteLine(FindNumberInMatrix(arr, 120));

            Console.WriteLine(FindNumberInMatrix(arr, 96));

            Console.WriteLine(FindNumberInMatrix(arr, 15));
            Console.WriteLine(FindNumberInMatrix(arr, 100));

        }
    }
}
