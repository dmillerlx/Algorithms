using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_LinkedList_RemoveDuplicates
    {
        //Write code to remove duplicates from an unsorted linked list.
        //FOLLOW UP
        //How would you solve this problem if a temporary buffer is not allowed?

        public void RemoveDuplicates()
        {
            Solution1();
            Solution2();
        }

       

        private void Solution1()
        {

            //Solution 1 - iterate through linked list, add items to a buffer, and if it is found
            //  in the buffer, remove the item.  If not found, then add to the buffer


            //Create and seed list
            Data_Structures.DataStructure_LinkedList<int> list = new Data_Structures.DataStructure_LinkedList<int>();

            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(3);
            list.Add(2);
            list.Add(1);
            list.Add(0);

            Console.WriteLine("------List - Solution 1-------");
            Console.WriteLine(list.ToString());
            Console.WriteLine("-----------------");



            Dictionary<int, int> buffer = new Dictionary<int, int>();

            Data_Structures.DataStructure_LinkedList<int>.Node prev = null;
            Data_Structures.DataStructure_LinkedList<int>.Node current = list.head;

            while (current != null)
            {
                if (buffer.ContainsKey(current.Data))
                {
                    if (prev != null)
                    {
                        prev.Next = current.Next;   //remove current
                        current = current.Next;     //advance current
                    }
                    else
                    {
                        //Removing head
                        list.head = list.head.Next;
                        current = list.head;
                    }
                }
                else
                {
                    //Not in buffer, so add it
                    buffer.Add(current.Data, current.Data);
                    prev = current;
                    current = current.Next;
                }

            }


            Console.WriteLine("------List without Dups-------");
            Console.WriteLine(list.ToString());
            Console.WriteLine("-----------------");
        }
        
        private static void Solution2()
        {
            //Solution 2 - Follow Up - without a temporary buffer

            //A - sort the list and remove elements that are the same and next to each other
            //      An efficent sort would require a buffer, but you could do an inefficent bubble sort
            //B - Brute force - iterate through list and have another iterator that removes elements after or before it that are the same
            //  would have O(N!)
            //      Book solution is using a runner that goes from the start of the list up to current and remove current if it exists in the runner


            Data_Structures.DataStructure_LinkedList<int> list = new Data_Structures.DataStructure_LinkedList<int>();

            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(3);
            list.Add(2);
            list.Add(1);
            list.Add(0);

            Console.WriteLine("------List - Solution 2-------");
            Console.WriteLine(list.ToString());
            Console.WriteLine("-----------------");


            Data_Structures.DataStructure_LinkedList<int>.Node current = null;
            Data_Structures.DataStructure_LinkedList<int>.Node prev = null;

            current = list.head;

            while (current != null)
            {
                Data_Structures.DataStructure_LinkedList<int>.Node runner = list.head;
                bool found = false;

                while (runner != current)
                {
                    if (runner.Data == current.Data)
                    {
                        //do not need to check for head since head can't be removed, since it is the only element and can't be a dup!

                        prev.Next = current.Next;       //skip over current
                        current = current.Next;         //Advance current
                        found = true;
                        break;
                    }
                    runner = runner.Next;
                }

                if (!found)
                {
                    //Advance current and prev
                    prev = current;
                    current = current.Next;
                }

                          

            }


            Console.WriteLine("------List without Dups-------");
            Console.WriteLine(list.ToString());
            Console.WriteLine("-----------------");

        }


    }
}
