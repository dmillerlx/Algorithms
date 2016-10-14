using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_DynamicProgramming_Staircase
    {
        //Problem:
        //A child is runningup a staricase with n steps and can hop 1, 2, or 3 steps
        //Implement a method to count how many ways the child can run up the stairs


        //Approach
        //  This is a fibonacci type problem
        //  Each stair is can be reached by coming from the n-1, n-2, or n-3 stair.
        //  So, if the staircase has n stairs, then the answer is the recurance
        //      Staircase(n-1) + Staircase(n-2) + Staircase(n-3)
        //

        Dictionary<int, UInt64> cache = new Dictionary<int, UInt64>();

        public UInt64 Staircase(int n)
        {
            if (n < 0)
                return 0;
            if (n == 1)         
                return 1;

            if (cache.ContainsKey(n))
                return cache[n];

            UInt64 value = Staircase(n - 1) + Staircase(n - 2) + Staircase(n - 3);

            cache.Add(n, value);
            return value;
        }

        public void Run()
        {

            Console.WriteLine(Staircase(20));
            Console.WriteLine(Staircase(37));

            Console.WriteLine(Staircase(45));

        }

    }
}
