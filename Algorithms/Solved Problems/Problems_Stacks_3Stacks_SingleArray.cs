using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_Stacks_3Stacks_SingleArray
    {
        //Implement 3 stacks using a single array

        public class StackElement
        {
            public int Data { get; set; }
            public int StackID { get; set; }
            public int Prev { get; set; }
        }

        public StackElement[] stack = new StackElement[100];

        public int []stackPointers = new int[3];
        public int top = -1;
        public int emptyPointer = 0;

        public void Run()        
        {
            //INitalize the pointers to -1
            for(int x=0; x < stackPointers.Length; x++)
            {
                stackPointers[x] = -1;
            }

            //INitalize the stack to be all free entries
            for (int x = stack.Length-1; x >= 0; x--)
            {
                stack[x] = new StackElement() { Prev = x + 1 };
                if (x == (stack.Length - 1))
                    stack[x].Prev = -1; //end of free items
                    
            }


            Push(0, 1);
            Push(1, 2);
            Push(2, 3);
            Push(2, 4);
            Push(1, 5);
            Push(2, 6);
            Push(0, 7);
            Push(1, 8);
            Push(2, 9);

            int data;
            while (Pop(0, out data))
            {
                Console.WriteLine("Stack: 0> " + data);
            }

            Push(0, 10);
            Push(0, 11);
            Push(0, 12);
            Push(0, 13);
            Push(0, 14);
            while (Pop(0, out data))
            {
                Console.WriteLine("Stack: 0> " + data);
            }

            while (Pop(1, out data))
            {
                Console.WriteLine("Stack: 1> " + data);
            }

            while (Pop(2, out data))
            {
                Console.WriteLine("Stack: 2> " + data);
            }


        }

        bool Push(int stackID, int data)
        {
            //Find next open location
            //Open location on stack will be either an empty item or a null item

            int nextLocation = emptyPointer;

            if (nextLocation < 0)
            {
                //Stack is full
                return false;

            }

            emptyPointer = stack[emptyPointer].Prev;    //Advance emptyPointer to next free item

            stack[nextLocation] = new StackElement(){Data = data, StackID = stackID, Prev=stackPointers[stackID]};

            stackPointers[stackID] = nextLocation;

            return true;
        }



        bool Pop(int stackID, out int Data)
        {
            Data = 0;
            int location = stackPointers[stackID];
            
            //Check to see if anything is in this stack, return false if not
            if (location < 0)
                return false;

            Data = stack[location].Data;
            int prevLocation = stack[location].Prev;

            //Add back to empty list
            stack[location].Prev = emptyPointer;
            emptyPointer = location;
            
            stackPointers[stackID] = prevLocation;

            return true;
        }





    }
}
