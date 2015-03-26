using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_LinkedList_FindLoop
    {
        public void Run()
        {
            Data_Structures.DataStructure_LinkedList<string> list = new Data_Structures.DataStructure_LinkedList<string>();
            list.Add("A");
            list.Add("B");
            list.Add("C");
            list.Add("D");
            list.Add("E");
            list.Add("F");
            list.Add("G");
            list.Add("H");
            list.Add("I");
            list.Add("J");
            list.Add("K");
            list.Add("L");
            list.Add("M");
            list.Add("N");

            //Add loop back to C

            Data_Structures.DataStructure_LinkedList<string>.Node C_Node = null;

            Data_Structures.DataStructure_LinkedList<string>.Node iterator = list.head;

            while (iterator.Next != null)
            {
                if (iterator.Data == "C")
                {
                    C_Node = iterator;
                }
                iterator = iterator.Next;
            }

            if (iterator != null && C_Node != null)
            {
                iterator.Next = C_Node;
            }


            Console.WriteLine("-----Problem - Linked List - find loop - Solution 1");

            Data_Structures.DataStructure_LinkedList<string>.Node loopNode = Solution1(list);

            if (loopNode != null)
                Console.WriteLine("Found loop start at node: " + loopNode.Data);
            else
                Console.WriteLine("Loop not found");

            Console.WriteLine("-----Problem - Linked List - find loop - Solution 2");

            loopNode = Solution2(list);

            if (loopNode != null)
                Console.WriteLine("Found loop start at node: " + loopNode.Data);
            else
                Console.WriteLine("Loop not found");


        }

        public Data_Structures.DataStructure_LinkedList<string>.Node Solution1(Data_Structures.DataStructure_LinkedList<string> list)
        {
            Dictionary<Data_Structures.DataStructure_LinkedList<string>.Node, Data_Structures.DataStructure_LinkedList<string>.Node> visitedNodes = new Dictionary<Data_Structures.DataStructure_LinkedList<string>.Node, Data_Structures.DataStructure_LinkedList<string>.Node>();

            Data_Structures.DataStructure_LinkedList<string>.Node iterator = list.head;

            while (iterator != null)
            {
                if (visitedNodes.ContainsKey(iterator))
                {
                    //found loop start
                    return iterator;
                }
                else
                {
                    visitedNodes.Add(iterator, iterator);
                    iterator = iterator.Next;
                }
            }

            //Reached end of the list, so no loop found
            return null;

        }

        public Data_Structures.DataStructure_LinkedList<string>.Node Solution2(Data_Structures.DataStructure_LinkedList<string> list)
        {
            //Have 2 iterators: n1, n2
            //  Both start at the head of the list
            //  n1 iterates at 1 step at a time
            //  n2 iterates at 2 steps at a time
            //  They will meet in the loop
            //  After they meet, reset n1 to the head of the list and iterate one at a time
            //  They will now meat at the start of the loop
            //
            //  n1 advances x positions
            //  n2 advances 2x positions
            //  n1 and n2 meet in the loop, and will be k nodes from the start
            //  So, reset n1 to the head and advance each iterator (n1 and n2) by 1 step until they meet
            //  Where they meet will be the start of the loop
            //


            Data_Structures.DataStructure_LinkedList<string>.Node n1 = list.head;
            Data_Structures.DataStructure_LinkedList<string>.Node n2 = list.head;

            //Advance n1 by 1 step and and n2 by 2 steps until they meet in the loop
            while (n2 != null && n2.Next != null)
            {
                n1 = n1.Next;
                n2 = n2.Next.Next;
                if (n1 == n2)
                    break;
            }

            //Not a circular list
            if (n1 == null || n2 == null)
                return null;


            //Reset n1 to the head and advance n1 and n2 by 1 step until they meet
            //They must meet at the start of the loop
            n1 = list.head;
            while (n1 != n2)
            {
                n1 = n1.Next;
                n2 = n2.Next;
            }

            //n1 should be at start of loop
            return n1;

        }

    }
}
