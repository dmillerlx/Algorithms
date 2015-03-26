using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_Stacks_Sort
    {

        Stack<int> stack = new Stack<int>();
        Stack<int> temp = new Stack<int>();

        public void Sort()
        {
            //Sort items in 'stack' in ascending order
            //using at most 1 other stack, and no other data structures

            //Find smallest item in stack by popping and pushing onto temp

            int smallest = int.MaxValue;
            while (stack.Count > 0)
            {
                if (stack.Peek() < smallest)
                    smallest = stack.Peek();

                temp.Push(stack.Pop());
            }

            //Now push all elements back into stack except smallest

            while (temp.Count > 0)
            {
                if (temp.Peek() != smallest)
                {
                    stack.Push(temp.Pop());
                }
                else
                {
                    temp.Pop();
                }
            }

            SortHelper(smallest);
        }

        void SortHelper(int smallest)
        {
            if (stack.Count == 0)
                return;

            int nextSmallest = int.MaxValue;
            while (stack.Count > 0)
            {
                if (stack.Peek() < nextSmallest)
                    nextSmallest = stack.Peek();

                temp.Push(stack.Pop());
            }

            //Now push all elements back into stack except smallest

            while (temp.Count > 0)
            {
                if (temp.Peek() != nextSmallest)
                {
                    stack.Push(temp.Pop());
                }
                else
                {
                    temp.Pop();
                }
            }

            SortHelper(nextSmallest);

            stack.Push(smallest);
        }


        public void Sort2()
        {
            int count = 0;
            while (stack.Count > 0)
            {
                //Find smallest value in stack
                int tmp = stack.Pop();
                while (stack.Count > 0)
                {
                    if (stack.Peek() < tmp)
                    {
                        temp.Push(tmp);
                        tmp = stack.Pop();
                    }
                    else
                    {
                        temp.Push(stack.Pop());
                    }
                }

                //Restore items into stack, except nth item
                while (temp.Count > count)
                {
                    stack.Push(temp.Pop());
                }

                temp.Push(tmp);
                count++;
            }

            //finally restore items back into stack
            while (temp.Count > 0)
            {
                stack.Push(temp.Pop());
            }


        }



        //Stradegy - Push items into temp stack in order
        //           by popping greater values off and then pushing smaller value
        //
        //Pop an item off the input stack and place into 'tmp'
        //Move items from temp over to input that are greater than the 'tmp'
        //Repeat until input stack is empty

        public Stack<int> Sort3(Stack<int> input)
        {
            int tmp;
            Stack<int> temp = new Stack<int>();

            while (input.Count > 0)
            {
                tmp = input.Pop();

                while (temp.Count > 0 && temp.Peek() > tmp)
                    input.Push(temp.Pop());

                temp.Push(tmp);
            }

            return temp;

        }

        public void Run()
        {
            stack.Push(1);
            stack.Push(5);
            stack.Push(10);
            stack.Push(15);
            stack.Push(3);
            stack.Push(8);
            stack.Push(11);
            stack.Push(7);
            stack.Push(2);
            stack.Push(0);

            //Sort3();
            stack = Sort3(stack);

            Console.WriteLine("Stack Conentets: ");
            while (stack.Count > 0)
            {
                Console.WriteLine(stack.Pop());
            }
        }
    }
}
