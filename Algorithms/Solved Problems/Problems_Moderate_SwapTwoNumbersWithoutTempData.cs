using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_Moderate_SwapTwoNumbersWithoutTempData
    {
        //Write a function to swap two numbers with out using any temporary data

        //Book solution
        public void swap(int a, int b)
        {   
            //a = 9, b = 4
            a = a - b;  //a = 9 - 4 = 5
            b = a + b;  //b = 5 + 4 = 9
            a = b - a;  //a = 9 - 5 = 4

            Console.WriteLine("A: " + a);
            Console.WriteLine("B: " + b);

        }

        public void swap_opt(int a, int b)
        {
            // a = 101, b = 110

            a = a ^ b;  // a = 101 ^ 110 = 011
            b = a ^ b;  // b = 011 ^ 110 = 101
            a = a ^ b;  // a = 011 ^ 101 = 110

            Console.WriteLine("A: " + a);
            Console.WriteLine("B: " + b);

        }

        public void Run()
        {
            swap(10, 25);

            swap_opt(50, 6);
        }


    }
}
