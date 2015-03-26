using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Data_Structures
{
    public class DataStructure_LinkedList <T> where T:IComparable
    {
        public Node head = null;

        public int Count { get; set; }

        public DataStructure_LinkedList()
        {
            Count = 0;
        }

        public class Node
        {
            public Node Next { get; set; }
            public T Data { get; set; }

            public Node(T data) { this.Data = data; Next = null; }

            public void AppendToTail(T data)
            {
                Node newNode = new Node(data);

                Node end = this;
                while (end.Next != null) { end = end.Next; }

                end.Next = newNode;
                
            }
        }

        
        public void Delete(T data)
        {
            head = DeleteNode(head, data);
        }

        public void Add(T data)
        {
            if (head == null)
            {
                head = new Node(data);
            }
            else
            {
                head.AppendToTail(data);
            }
            Count++;
        }

        //Delete Node when a specific data value.  Only deltes the first occurance
        private Node DeleteNode(Node head, T data)
        {
            Node n = head;
            if (head == null) return head;
            if (n.Data.CompareTo(data) == 0)
            {
                Count--;
                return head.Next;
            }
            while (n.Next != null)
            {
                if (n.Next.Data.CompareTo(data) == 0)
                {
                    n.Next = n.Next.Next;
                    Count--;                
                    return head; //no change to head
                }
                n = n.Next;
            }
            return head;
        }

        
        //Delete a specific node
        public void DeleteNode(Node node)
        {
            if (head == null)
                return;

            if (node == null)
                return;

            //If deleting an element that is not the last element, can just skip over this element
            if (node == head || node.Next != null)
            {
                if (node == head)
                {
                    //head node so just advance the head
                    head = head.Next;
                    Count--;
                    return;
                }
                else
                {
                    //Copy data from next element into current node
                    node.Data = node.Next.Data;
                    //Set next pointer to skip over next element.
                    node.Next = node.Next.Next;
                    Count--;
                    return;
                }
            }
            else
            {
                //Element is at the end of the list, so search for it from the start
                //by advancing current until the next node is the node to delete
                Node current = head;
                while (current != null && current.Next != node) { current = current.Next; }
                
                if (current != null && current.Next == node)
                {
                    //current.next is the node to remove, so skip over that element
                    current.Next = current.Next.Next;
                    Count--;
                    return;
                }

            }
        }


        public string ToString()
        {
            StringBuilder sb = new StringBuilder();            
            for (Node current = head; current != null; current=current.Next)
            {
                sb.AppendLine(current.Data.ToString());
            }
            return sb.ToString();
        }

    }
}
