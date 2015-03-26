using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_LinkedList_FindNthLastItem
    {
        public void Run()
        {
            //Solution - iterate through linked list and have a tail that only starts iterating after N number of iterations


            //Create and seed list
            Data_Structures.DataStructure_LinkedList<int> list = new Data_Structures.DataStructure_LinkedList<int>();

            for (int x = 0; x < 20; x++)
                list.Add(x);
            
            Console.WriteLine("------List - Solution 1-------");
            Console.WriteLine(list.ToString());
            Console.WriteLine("-----------------");

            FindNthLastItem(list, 0);
            FindNthLastItem(list, 5);
            FindNthLastItem(list, 10);
            
        }

        public void FindNthLastItem(Data_Structures.DataStructure_LinkedList<int> list, int N)
        {
            Data_Structures.DataStructure_LinkedList<int>.Node current = list.head;
            Data_Structures.DataStructure_LinkedList<int>.Node tail = list.head;

            int count = 0;
            while (current != null && current.Next != null)
            {
                if (count >= N)
                {
                    tail = tail.Next;
                }
                count++;
                current = current.Next;
            }

            if (count >= N && tail != null)
            {
                Console.WriteLine("Found (" + N + ") last item: " + tail.Data);
            }


        }
               

    }
}
