using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_LinkedIn_ServerFindMissingValue
    {
        /*
You're running a pool of servers where the servers are numbered sequentially starting from 1. Over time, any given server might explode, in which case its server number is made available for reuse. When a new server is launched, it should be given the lowest available number.

Write a function which, given the list of currently allocated server numbers, returns the number of the next server to allocate.

For example:

  >> next_server_number([5, 3, 1])
  2
  >> next_server_number([5, 4, 1, 2])
  3
  >> next_server_number([3, 2, 1])
  4
  >> next_server_number([2, 3])
  1
  >> next_server_number([])
  1
*/


        int FindNextServerValue(int[] arr)
        {
            int len = arr.Length;

            //Better solution - swap number from index location into this location
            for (int i = 0; i < len; )
            {
                int index = arr[i] - 1;
                if (index == i)
                {
                    arr[index] = -1;
                    i++;
                }
                else if (index >= 0 && index < len)
                {
                    int tmp = arr[index];
                    arr[index] = -1;
                    arr[i] = tmp;
                }
                else i++;
            }


            //    //iterate from left to right
            //    for (int i = 0; i < len; i++)
            //    {
            //        int index = arr[i] - 1;
            //        if (index >= 0 && index < len)
            //            if (index <= i)
            //            {
            //                arr[index] = -1;
            //            }
            //            else
            //            {
            //                //save value in current array position before updating
            //                int tmp = arr[index];
            //                arr[index] = -1;
            //                arr[i] = tmp;
            //            }
            //    }

            ////iterate from right to left
            //for (int i = len - 1; i >= 0; i--)
            //{
            //    int index = arr[i] - 1;
            //    if (index >= 0 && index < len)
            //        if (index >= i)
            //            arr[index] = -1;

            //}


                //find lowest index not set
                for (int i = 0; i < len; i++)
                {
                    if (arr[i] >= 0)
                        return i + 1;
                }

            //No holes found in server list, so return len and next value
            return len + 1;
        }

        public void Run()
        {

            Console.WriteLine(FindNextServerValue(new int[] { 5, 3, 1 }));
            Console.WriteLine(FindNextServerValue(new int[] { 5, 4, 1, 2 }));
            Console.WriteLine(FindNextServerValue(new int[] { 3,2,1 }));
            Console.WriteLine(FindNextServerValue(new int[] { 1, 2, 3 }));
            Console.WriteLine(FindNextServerValue(new int[] { 2, 3 }));
            Console.WriteLine(FindNextServerValue(new int[] { }));



        }

    }
}
