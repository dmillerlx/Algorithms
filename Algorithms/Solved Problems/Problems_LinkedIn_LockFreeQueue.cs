using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Algorithms.Solved_Problems
{
    class Problems_LinkedIn_LockFreeQueue
    {

        public class MyQueue<T>
        {
            public class NodeQ<T>
            {
                public T Data { get; set; }
                public NodeQ<T> Next = null;
                public NodeQ(T data)
                {
                    this.Data = data;
                    Next = null;
                }

                public override string ToString()
                {
                    return Data.ToString();
                }
            }

            private NodeQ<T> head = null;//new NodeQ<T>(default(T));
            private NodeQ<T> tail = null;//head;

            public MyQueue()
            {
                NodeQ<T> n = new NodeQ<T>(default(T));
                this.head = n;
                this.tail = n;
            }

            private static bool CAS(ref NodeQ<T> location, NodeQ<T> comparand, NodeQ<T> newValue)
            {
                return comparand == Interlocked.CompareExchange<NodeQ<T>>(ref location, newValue, comparand);
            }

            

            
            public void Enqueue(T item)
            {
                NodeQ<T> newNode = new NodeQ<T>(item);


                bool done = false;
                NodeQ<T> currentTail = null;
                NodeQ<T> next = null;
                do
                {
                    currentTail = tail;
                    next = currentTail.Next;

                    if (tail == currentTail)    //Tail and next consistent?
                    {
                        if (next == null)   //Tail on last node?
                        {
                            if (CAS(ref currentTail.Next, next, newNode))
                                done = true;
                        }
                        else  //Tail not pointing to last node, so advance tail towards the tail
                        {
                            CAS(ref this.tail, currentTail, next);
                        }
                    }
                } while (!done);//CAS(ref tail, tail, tail.Next = newNode));

                CAS(ref this.tail, currentTail, newNode);
            }

            //      0
            //      Head
            //      Tail
            //      if (head.next == null)
            //          head.next = newNode
            //
            //

            //      0       A       B       C       D
            //      Head                            Tail
            //
            //      t = Tail
            //      if (t.next == null)
            //          t.next = newNode
            //          tail = newNode

            public bool DeQueue(out T dataOut)
            {
                NodeQ<T> currentHead = null;
                NodeQ<T> currentTail = null;
                NodeQ<T> next = null;
                dataOut = default(T);
                do
                {
                    currentHead = this.head;
                    currentTail = this.tail;
                    next = head.Next;

                    if (currentHead == this.head)
                    {
                        if (currentHead == currentTail) //head == tail, so if next is empty, queue is empty
                        {
                            if (next == null)
                            {
                                return false;       //Queue is empty
                            }

                            CAS(ref this.tail, currentTail, next);  //Head == Tail, but next not empty.  Advance tail to next to catchup
                        }
                        else
                        {
                            //Head != Tail, so try to advance head
                            dataOut = next.Data;    //Assign value to next, not head since we start with a dummy node
                                                    //and because if we do it after we assign, it could be gone already
                            if (CAS(ref this.head, currentHead, next))
                            {
                                //Removed head so we can return this value
                                //dataOut = this.head.Data;
                                return true;
                            }

                        }

                    }
                } while (true);               
            }

        }



        ////////////////////////////////////////////////////////////////////////
        //Threaded Test Functions        
        public void threadEnqueue()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);
            for (int x=0; x < 100; x++)
            {
                int val = x;// rnd.Next(1000);
                myQueue.Enqueue(val);
               // Console.WriteLine("Enqueue: " + val);
            }
        }

        bool exit = false;
        public void threadDequeue()
        {
            int val;
            bool empty = false;
            do
            {
                if (myQueue.DeQueue(out val))
                {
                    Console.WriteLine(val);
                }
                else
                {
                    //Console.WriteLine("empty");
                    empty = true;
                }

            } while (!exit || !empty);
        }



        MyQueue<int> myQueue = new MyQueue<int>();

        public void Run()
        {
            

            //myQueue.Enqueue(1);
            //myQueue.Enqueue(2);
            //myQueue.Enqueue(3);
            //myQueue.Enqueue(4);
            //myQueue.Enqueue(5);
            //myQueue.Enqueue(6);

            //int val;
            //while (myQueue.DeQueue(out val))
            //{
            //    Console.WriteLine(val);
            //}

            Thread enqueue1 = new Thread(threadEnqueue);
            Thread enqueue2 = new Thread(threadEnqueue);

            Thread dequeue1 = new Thread(threadDequeue);

            enqueue1.Start();
            enqueue2.Start();
            dequeue1.Start();

            while (enqueue1.IsAlive || enqueue2.IsAlive)
            {
                System.Threading.Thread.Sleep(100);
            }

            exit = true;
            Console.WriteLine("Exit");

        }

    }
}
