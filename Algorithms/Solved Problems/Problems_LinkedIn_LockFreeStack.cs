using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Algorithms.Solved_Problems
{
    class Problems_LinkedIn_LockFreeStack
    {
        //Code from http://www.boyet.com/Articles/LockfreeStack.html

        //Also review: http://www.dotnetperls.com/interlocked
        //http://www.cs.rochester.edu/research/synchronization/pseudocode/queues.html
        //http://www.ibm.com/developerworks/library/j-jtp04186/

        public const int topValue = 10000;

        class PusherEngine
        {
            private SimpleStack<int> stack;
            public PusherEngine(SimpleStack<int> stack)
            {
                this.stack = stack;
            }
            public void Execute()
            {
                for (int i = 1; i <= topValue; i++)
                {
                    stack.Push(i);
                }
            }
        }

        class PopperEngine
        {
            private SimpleStack<int> stack;
            private Thread pusherThread;

            public PopperEngine(SimpleStack<int> stack, Thread pusherThread)
            {
                this.stack = stack;
                this.pusherThread = pusherThread;
            }

            public void Execute()
            {
                bool[] poppedValues = new bool[topValue + 1];
                int poppedInt;

                do
                {
                    poppedInt = stack.Pop();
                    if (poppedInt != 0)
                    {
                        if (poppedValues[poppedInt])
                            Console.WriteLine(string.Format("{0} has been popped before!", poppedInt));
                        poppedValues[poppedInt] = true;
                    }
                } while (poppedInt != topValue);

                pusherThread.Join();
                Console.WriteLine("pusher now finished");

                poppedInt = stack.Pop();
                while (poppedInt != 0)
                {
                    if (poppedValues[poppedInt])
                        Console.WriteLine(string.Format("{0} has been popped before!", poppedInt));
                    poppedValues[poppedInt] = true;
                    poppedInt = stack.Pop();
                }

                Console.WriteLine("checking output");
                for (int i = 1; i <= topValue; i++)
                {
                    if (!poppedValues[i])
                        Console.Write(string.Format("{0} is missing", poppedValues[i]));
                }
            }
        }

        public class SimpleStack<T>
        {

            private class Node<V>
            {
                public Node<V> Next;
                public V Item;
            }

            private Node<T> head;

            public SimpleStack()
            {
                head = new Node<T>();
            }

            //public void Push(T item)
            //{
            //    Node<T> node = new Node<T>();
            //    node.Item = item;
            //    node.Next = head.Next;
            //    head.Next = node;
            //}

            //public T Pop()
            //{
            //    Node<T> node = head.Next;
            //    if (node == null)
            //        return default(T);
            //    head.Next = node.Next;
            //    return node.Item;
            //}

            public void Push(T item)
            {
                Node<T> node = new Node<T>();
                node.Item = item;
                do
                {
                    node.Next = head.Next;
                } while (!CAS(ref head.Next, node.Next, node));
                //
                //  0       A       B       C       D
                //  Head    
                //  
                //  Node = 1
                //  Node.next = head.next = A
                //      if (head.next (A) == node.next(A))
                //          head.Next (A) = node (1)
                //
            }

            private static bool CAS(
                ref Node<T> location, Node<T> comparand, Node<T> newValue)
            {
                return
                  comparand == Interlocked.CompareExchange<Node<T>>(
                                 ref location, newValue, comparand);
            }

            public T Pop()
            {
                Node<T> node;
                do
                {
                    node = head.Next;
                    if (node == null)
                        return default(T);
                } while (!CAS(ref head.Next, node, node.Next));
                return node.Item;
            }

        }

        //static bool CAS(ref object destination, object currentValue, object newValue)
        //{
        //    if (destination == currentValue)
        //    {
        //        destination = newValue;
        //        return true;
        //    }
        //    return false;
        //}

        


        public void Run()//string[] args)
        {

            Console.WriteLine("create the shared stack");
            SimpleStack<int> stack = new SimpleStack<int>();

            Console.WriteLine("create the threads");
            PusherEngine pusher = new PusherEngine(stack);
            Thread pusherThread = new Thread(new ThreadStart(pusher.Execute));
            PopperEngine popper = new PopperEngine(stack, pusherThread);
            Thread popperThread = new Thread(new ThreadStart(popper.Execute));

            Console.WriteLine("start the threads");
            pusherThread.Start();
            popperThread.Start();

            popperThread.Join();

            Console.WriteLine("Done");
            Console.ReadLine();
        }


    }
}
