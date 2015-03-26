using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_LinkedList_AddNumbersInList
    {
        /*
         You have two numbers represented by a linked list, where each node contains a single digit. 
         * The digits are stored in reverse order, such that the 1’s digit is at the head of the list. 
         * Write a function that adds the two numbers and returns the sum as a linked list.
        EXAMPLE
        Input: (3 -> 1 -> 5) + (5 -> 9 -> 2)
        Output: 8 -> 0 -> 8
         */

        public void Run()
        {
            Solution1();
            Solution2();

            Solution3(); //This solution is for a linked list that is in-order (1's digit is at the end of the list)
                         //and is done with out revesing the lists
        }

        private static void Solution1()
        {
            //Create and seed list
            Data_Structures.DataStructure_LinkedList<int> first = new Data_Structures.DataStructure_LinkedList<int>();
            Data_Structures.DataStructure_LinkedList<int> second = new Data_Structures.DataStructure_LinkedList<int>();
            Data_Structures.DataStructure_LinkedList<int> output = new Data_Structures.DataStructure_LinkedList<int>();

            first.Add(3);
            first.Add(1);
            first.Add(5);
            first.Add(5);

            second.Add(5);
            second.Add(9);
            second.Add(2);

            //Convert list to integer

            int firstInt = 0;
            int count = 0;
            for (Data_Structures.DataStructure_LinkedList<int>.Node current = first.head; current != null; current = current.Next)
            {
                firstInt = firstInt + current.Data * (int)Math.Pow(10, count++);
            }

            int secondInt = 0;
            count = 0;
            for (Data_Structures.DataStructure_LinkedList<int>.Node current = second.head; current != null; current = current.Next)
            {
                secondInt = secondInt + current.Data * (int)Math.Pow(10, count++);
            }

            //Now add the integers
            int outputInt = firstInt + secondInt;

            //Finally create output linked list using the integer
            string outputString = outputInt.ToString();
            foreach (char c in outputString)
            {
                output.Add(c - '0');
            }


            Console.WriteLine("Output:");
            Console.WriteLine(output.ToString());
        }

        public void Solution2()
        {
            Console.WriteLine("----Solution 2-----");

            //Create and seed list
            Data_Structures.DataStructure_LinkedList<int> first = new Data_Structures.DataStructure_LinkedList<int>();
            Data_Structures.DataStructure_LinkedList<int> second = new Data_Structures.DataStructure_LinkedList<int>();
            Data_Structures.DataStructure_LinkedList<int> output = new Data_Structures.DataStructure_LinkedList<int>();

            first.Add(3);
            first.Add(1);
            first.Add(5);
            first.Add(5);

            second.Add(5);
            second.Add(9);
            second.Add(2);

        
            //Iterate through and add the numbers, keeping a carry over

            Data_Structures.DataStructure_LinkedList<int>.Node currentFirst = first.head;
            Data_Structures.DataStructure_LinkedList<int>.Node currentSecond = second.head;

            int carry = 0;

            while (currentFirst != null || currentSecond != null)
            {
                int value = 0;
                if (currentFirst != null && currentSecond != null)
                {
                    value = currentFirst.Data + currentSecond.Data;
                    currentFirst = currentFirst.Next;
                    currentSecond = currentSecond.Next;
                }
                else if (currentFirst!= null)
                {
                    value = currentFirst.Data;
                    currentFirst = currentFirst.Next;
                }
                else if (currentSecond != null)
                {
                    value = currentSecond.Data;
                    currentSecond = currentSecond.Next;
                }

                value += carry;
                carry = 0;

                if (value >= 10)
                {
                    value = 0;
                    carry = 1;
                }
                output.Add(value);
            }

            if (carry > 0)
            {
                output.Add(carry);
            }


            Console.WriteLine("Output:");
            Console.WriteLine(output.ToString());

        }

        public int AddHelper(
            Data_Structures.DataStructure_LinkedList<int>.Node first,
            int firstNodesToEnd,
            Data_Structures.DataStructure_LinkedList<int>.Node second,
            int secondNodesToEnd            
            )
        {
            int carryOver = 0;

            //If not at end of the list's, advance each list to the end.           
            if (first.Next != null && second.Next != null)
            {
                if (firstNodesToEnd > secondNodesToEnd)
                {
                    carryOver = AddHelper(first.Next, firstNodesToEnd - 1, second, secondNodesToEnd);
                }
                else if (secondNodesToEnd > firstNodesToEnd)
                {
                    carryOver = AddHelper(first, firstNodesToEnd, second.Next, secondNodesToEnd - 1);
                }
                else
                {
                    carryOver = AddHelper(first.Next, firstNodesToEnd - 1, second.Next, secondNodesToEnd - 1);
                }
            }

            int value = 0;
            if (firstNodesToEnd == secondNodesToEnd)
            {
                value = first.Data + second.Data + carryOver;
            }
            else if (firstNodesToEnd > secondNodesToEnd)
            {
                value = first.Data + carryOver;
            }
            else if (secondNodesToEnd > firstNodesToEnd)
            {
                value = second.Data + carryOver;
            }

            if (value >= 10)
            {
                value = value % 10;
                carryOver = 1;
            }
            else
            {
                carryOver = 0;
            }


            outputStack.Push(value);        //Put items into stack so we can make the list at the end

            return carryOver;

        }

        Data_Structures.DataStructure_LinkedList<int> output = new Data_Structures.DataStructure_LinkedList<int>();

        Stack<int> outputStack = new Stack<int>();

        public void Solution3()
        {
            Console.WriteLine("----Solution 3-----");

            //Create and seed list
            Data_Structures.DataStructure_LinkedList<int> first = new Data_Structures.DataStructure_LinkedList<int>();
            Data_Structures.DataStructure_LinkedList<int> second = new Data_Structures.DataStructure_LinkedList<int>();
            

            first.Add(3);
            first.Add(1);
            first.Add(5);
            first.Add(5);

            Console.WriteLine("Input1: ");
            Console.WriteLine(first.ToString());

            second.Add(5);
            second.Add(9);
            second.Add(2);
            second.Add(2);
            second.Add(2);
            second.Add(2);

            Console.WriteLine("Input2: ");
            Console.WriteLine(second.ToString());


            //Iterate through and add the numbers, keeping a carry over

            Data_Structures.DataStructure_LinkedList<int>.Node currentFirst = first.head;
            Data_Structures.DataStructure_LinkedList<int>.Node currentSecond = second.head;

            if (first.head != null && second.head != null)
            {


                int carryOver = AddHelper(first.head, first.Count, second.head, second.Count);
                if (carryOver > 0)
                {
                    outputStack.Push(carryOver);//.Add(carryOver);
                }

                while (outputStack.Count > 0)
                {
                    output.Add(outputStack.Pop());
                }
            
            }
            else if (first.head == null)
            {
                output = second;
            }
            else if (second.head == null)
            {
                output = first;
            }

            
            Console.WriteLine("Output:");
            Console.WriteLine(output.ToString());

        }



    }
}
