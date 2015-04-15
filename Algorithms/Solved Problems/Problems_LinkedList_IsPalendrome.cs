using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_LinkedList_IsPalendrome
    {

        //Implement a function to check if a linked list is a palindrome

        public class Node
        {
            public int Data { get; set; }
            public Node Next { get; set; }
            public Node (int data)
            {
                Data = data;
                Next = null;
            }
            public override string ToString()
            {
                return Data.ToString();
            }
        }

        //Approach
        //Recurse through the list until the end is found
        //When the end is found, set another variable (outside the function - 'tmp') to the head of the list and advance it will 
        //returning from the end of the list
        //If the data values match, the linked list is a palindrome


        public Node Head = null;        //Head of list
        public Node tmp = null;         //runner that starts at Head when the recursive function hits the end of the list

        public bool helper(Node current)
        {            
            //Set tmp to 'Head' of list when at end of list
            if (current == null)
            {
                tmp = Head;
                return true;
            }
            else
            {
                //Not at end of list, recurse
                //If false is returned a found a non-match, so stop processing and bubble the false up to the top
                if (helper(current.Next) == false)
                    return false;
            }

            //Check to see if current node matches the tmp value
            //If it matches, advance tmp and return true
            if (tmp.Data == current.Data)
            {
                tmp = tmp.Next;
                return true;
            }

            //Did not match, return false
            return false;
        }

        bool isPalendrome ()
        {
            return helper(Head);
        }

        public void Run()
        {
            //Test cases
            // 0, 1, 2, 1, 0
            Head = new Node(0);
            Node n = new Node(1);
            Head.Next = n;
            n.Next = new Node(2);
            n = n.Next;
            n.Next = new Node(1);
            n = n.Next;
            n.Next = new Node(0);

            Console.WriteLine("IsPalendrome: " + isPalendrome());

            //Append -1
            // 0, 1, 2, 1, 0, -1
            n = n.Next;
            n.Next = new Node(-1);
            Console.WriteLine("IsPalendrome: " + isPalendrome());

            //Shorter
            //5, 1, 5
            Head = new Node(5);
            n = new Node(1);
            Head.Next = n;
            n.Next = new Node(5);
            Console.WriteLine("IsPalendrome: " + isPalendrome());
            

        }


    }
}
