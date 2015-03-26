using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_Stacks_PushPopMin
    {

        public class StackWithMin
        {
            Node top = null;

            //Keep track of current min with each node
            public class Node
            {
                public int value;
                public int min;
                public Node(int val, int m)
                {
                    value = val;
                    min = m;
                }

                public Node next;
            }

            public bool isEmpty { get { return top == null; } }

            public void Push(int val)
            {
                int minValue = Math.Min(val, Min());

                Node n = new Node(val, minValue);

                if (top == null)
                    top = n;
                else
                {
                    n.next = top;
                    top = n;
                }
            }

            public int Min()
            {
                if (top == null)
                    return int.MaxValue;
                return top.min;
            }

            public Node Pop()            
            {
                if (top == null) return null;
                Node n = top;
                top = top.next;
                return n;
            }

        }


        public void Run()
        {
            StackWithMin stack = new StackWithMin();

            stack.Push(5);
            stack.Push(2);
            stack.Push(19);
            stack.Push(1);
            stack.Push(25);
            stack.Push(7);
            stack.Push(19);
            stack.Push(18);

            
            while (stack.isEmpty == false)
            {
                Console.WriteLine("Min: " + stack.Min());
                Console.WriteLine("Pop: " + stack.Pop().value);
            }


        }

    }
}
