using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Data_Structures
{
    public class DataStructure_Stack<I> where I: IComparable
    {
        public class Node
        {
            public I Item { get; set; }
            public Node next;
        }

        private Node head = null;        

        public I Pop()
        {
            if (head != null)
            {
                I ret = head.Item;
                head = head.next;
                return ret;
            }

            throw new Exception("Empty");
        }

        public void Push(I item)
        {
            Node n = new Node();
            n.Item = item;
            n.next = head;
            head = n;           
        }

        public I Peek()
        {
            if (head != null)
            {
                I ret = head.Item;                
                return ret;
            }

            throw new Exception("Empty");
        }

        public bool IsEmpty { get { return head == null; } }

    }
}
