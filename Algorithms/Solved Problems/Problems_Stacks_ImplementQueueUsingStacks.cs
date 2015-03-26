using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_Stacks_ImplementQueueUsingStacks
    {
        public class Implement_Queue_Using_2_Stacks
        {

            Stack<int> A = new Stack<int>();
            Stack<int> B = new Stack<int>();

            //Stack = FIFO
            //Queue = FILO
            
            public void Enqueue(int val)
            {
                A.Push(val);
            }

            public int DeQueue()
            {
                if (A.Count == 0)
                    return int.MaxValue;

                while (A.Count > 1)
                {
                    B.Push(A.Pop());
                }

                int ret = A.Pop();

                while (B.Count > 0)
                {
                    A.Push(B.Pop());
                }

                return ret;
            }

            public int Count()
            {
                return A.Count;
            }
        }

        public class Implement_Queue_Using_2_Stacks_V2_Optimal
        {

            Stack<int> A = new Stack<int>();
            Stack<int> B = new Stack<int>();

            //Stack = FIFO
            //Queue = FILO

            //Elements will be pushed onto A
            public void Enqueue(int val)
            {
                A.Push(val);
            }

            //Elements will be poped off B
            //If no elements in B, push them into B from A
            //If still no elements in B, queue is empty
            public int DeQueue()
            {
                if (B.Count() == 0)
                {
                    //Push all elements onto B

                    while (A.Count > 0)
                    {
                        B.Push(A.Pop());
                    }
                }

                if (B.Count () == 0)
                {
                    //No elements in queue
                    return int.MaxValue;
                }

                int ret = B.Pop();

                return ret;
            }

            public int Count()
            {
                return A.Count + B.Count;
            }
        }
        
        
        public void Run()
        {

            Implement_Queue_Using_2_Stacks_V2_Optimal queue = new Implement_Queue_Using_2_Stacks_V2_Optimal();

            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Enqueue(5);
            queue.Enqueue(6);
            queue.Enqueue(7);
            queue.Enqueue(8);

            Console.WriteLine(queue.DeQueue());
            Console.WriteLine(queue.DeQueue());
            Console.WriteLine(queue.DeQueue());
            Console.WriteLine(queue.DeQueue());
            Console.WriteLine(queue.DeQueue());

            queue.Enqueue(9);
            queue.Enqueue(10);
            queue.Enqueue(11);

            while (queue.Count() > 0)
            {
                Console.WriteLine(queue.DeQueue());
            }


        }


    }
}
