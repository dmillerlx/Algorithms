using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_LinkedList_ReverseSinglyLinkedList
    {
        Data_Structures.DataStructure_LinkedList<int> list = new Data_Structures.DataStructure_LinkedList<int>();

        public void Run()
        {
            //Solution - iterate through linked list and have a tail that only starts iterating after N number of iterations


            //Create and seed list
           

            for (int x = 0; x < 20; x++)
                list.Add(x);

            ReveseList(list);

        }

        public void ReveseList(Data_Structures.DataStructure_LinkedList<int> list)
        {
            if (list.head == null)
                return;

            Data_Structures.DataStructure_LinkedList<int>.Node lastNode = ReveseHelper(list.head);
            lastNode.Next = null;

            Console.WriteLine("------List Reverse - Solution 1-------");
            Console.WriteLine(list.ToString());
            Console.WriteLine("-----------------");

        }

        public Data_Structures.DataStructure_LinkedList<int>.Node ReveseHelper(Data_Structures.DataStructure_LinkedList<int>.Node node)
        {
           
            if (node.Next == null)
            {
                list.head = node;
                return node;
            }

            Data_Structures.DataStructure_LinkedList<int>.Node retNode = ReveseHelper(node.Next);

            retNode.Next = node;
            return node;

        }


    }
}
