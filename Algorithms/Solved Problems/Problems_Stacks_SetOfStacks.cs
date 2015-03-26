using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_Stacks_SetOfStacks
    {
        public class SetOfStacks
        {

            int maxStackSize = 0;

            public SetOfStacks(int maxStackSize)
            {
                this.maxStackSize = maxStackSize;
            }

            Stack<Stack<int>> stackSet = new Stack<Stack<int>>();

            //Have a stack that holds the set of stacks
            //When a stack exceeds the maxStackSize, push it onto the stack and start a new stack
            //alternativly
            //Create a new stack on the stack, and use Peek to access it
            //When the size gets too large, just push another stack onto the top of the stack

            //For implementing a popAt function, pop the stacks off onto another temporary stack, do the pop, and then
            //push them back onto the main stack
            

            //
            //Optimization - use List instead of stack of stacks
            //          When doing popAt, may need to pull items from lower stacks to fill in


            public void Push(int value)
            {
                if (stackSet.Count == 0)
                {
                    stackSet.Push(new Stack<int>());
                }
                else if (stackSet.Peek().Count >= maxStackSize)
                {
                    stackSet.Push(new Stack<int>());
                }

                stackSet.Peek().Push(value);
            }

            public int Pop()
            {
                if (stackSet.Count == 0)
                    return int.MaxValue;

                if (stackSet.Peek().Count == 0)
                {
                    stackSet.Pop();
                    return Pop();
                }

                return stackSet.Peek().Pop();
            }

            public int PopAt(int index)
            {
                if (index < 0 || index > stackSet.Count)
                    return int.MaxValue;

                Stack<Stack<int>> tempStack = new Stack<Stack<int>>();

                while (index > 0)
                {
                    tempStack.Push(stackSet.Pop());
                    index--;
                }

                int ret  = stackSet.Peek().Pop();

                if (stackSet.Peek().Count == 0)
                    stackSet.Pop();

                while (tempStack.Count > 0)
                    stackSet.Push(tempStack.Pop());


                return ret;
            }

            public bool isEmpty
            {
                get
                {
                    if (stackSet.Count == 0)
                        return true;

                    return false;
                }
            }
        }

        public class SetOfStacks2
        {

            int maxStackSize = 0;

            public SetOfStacks2(int maxStackSize)
            {
                this.maxStackSize = maxStackSize;
            }

            List<Stack<int>> stackSet = new List<Stack<int>>();

            //Have a stack that holds the set of stacks
            //When a stack exceeds the maxStackSize, push it onto the stack and start a new stack
            //alternativly
            //Create a new stack on the stack, and use Peek to access it
            //When the size gets too large, just push another stack onto the top of the stack

            //For implementing a popAt function, pop the stacks off onto another temporary stack, do the pop, and then
            //push them back onto the main stack


            //
            //Optimization - use List instead of stack of stacks
            //          When doing popAt, may need to pull items from lower stacks to fill in


            public void Push(int value)
            {
                if (stackSet.Count == 0)
                {
                    stackSet.Insert(0, new Stack<int>());
                }
                else if (stackSet[0].Count >= maxStackSize)
                {
                    stackSet.Insert(0, new Stack<int>());
                }

                stackSet[0].Push(value);
            }

            public int Pop()
            {
                if (stackSet.Count == 0)
                    return int.MaxValue;

                int ret = stackSet[0].Pop();
                
                if (stackSet[0].Count == 0)
                {
                    stackSet.RemoveAt(0);
                }

                return ret;
            }

            public int PopAt(int index)
            {
                if (index < 0 || index > stackSet.Count)
                    return int.MaxValue;

                int ret = stackSet[index].Pop();
                
                //Poped last item off the stack, so null it out
                if (stackSet[index].Count == 0)
                    stackSet.RemoveAt(index);
                else
                {
                    int i = index;
                    while (i < stackSet.Count - 1)
                    {                        
                        //Grab item from next stack
                        int item = stackSet[i + 1].Pop();

                        //Place item on end of current stack
                        Stack<int> tempStack = new Stack<int>();
                        while (stackSet[i].Count > 0)
                        {
                            tempStack.Push(stackSet[i].Pop());
                        }

                        stackSet[i].Push(item);

                        while (tempStack.Count > 0)
                        {
                            stackSet[i].Push(tempStack.Pop());
                        }

                        //Check to see if next stack is now empty, if so remove it fro the list
                        if (stackSet[i + 1].Count == 0)
                            stackSet.RemoveAt(i + 1);

                        //Advance to next index
                        i++;
                    }
                }

                return ret;
            }

            public bool isEmpty
            {
                get
                {
                    if (stackSet.Count == 0)
                        return true;

                    return false;
                }
            }
        }


        public void Run()
        {
            SetOfStacks2 stacks = new SetOfStacks2(5);

            stacks.Push(1);
            stacks.Push(2);
            stacks.Push(3);
            stacks.Push(4);
            stacks.Push(5);
            stacks.Push(6);
            stacks.Push(7);
            stacks.Push(8);
            stacks.Push(9);
            stacks.Push(10);
            stacks.Push(11);
            stacks.Push(12);
            stacks.Push(13);
            stacks.Push(14);
            stacks.Push(15);

            stacks.PopAt(1);
            stacks.PopAt(1);
            stacks.PopAt(1);
            stacks.PopAt(1);
            stacks.PopAt(1);

            while (!stacks.isEmpty)
            {
                Console.WriteLine(stacks.Pop());
            }


        }


        //public class Stack<T>
        //{
        //    Node<T> top = null;

        //    int Count = 0;

           

        //    public bool isEmpty { get { return top == null; } }

        //    public void Push(int val)
        //    {
        //        Node n = new Node(val);

        //        if (top == null)
        //            top = n;
        //        else
        //        {
        //            n.next = top;
        //            top = n;
        //        }
        //        Count++;
        //    }

        //    public Node Pop()
        //    {
        //        if (top == null) return null;
        //        Node n = top;
        //        top = top.next;

        //        Count--;
        //        return n;
        //    }

        //    public Node Peek()
        //    {
        //        if (top == null) return null;

        //        return top;
        //    }
        //}


    }
}
