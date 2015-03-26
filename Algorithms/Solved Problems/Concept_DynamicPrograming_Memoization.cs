using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    class Concept_DynamicPrograming_Memoization
    {

        public class Fibonacci
        {
            Dictionary<UInt64, UInt64> memoizationTable = new Dictionary<UInt64, UInt64>();


            public int Calculate(int n)
            {
                if (n <= 2) return 1;

                int ret = Calculate(n - 1) + Calculate(n - 2);
                return ret;
            }

            public UInt64 Calculate_With_Memo(UInt64 n)
            {
                if (n <= 2) return 1;
                if (memoizationTable.ContainsKey(n)) return memoizationTable[n];

                UInt64 ret = Calculate_With_Memo(n - 1) + Calculate_With_Memo(n - 2);
                memoizationTable.Add(n, ret);
                return ret;
            }
        }

        public void Test_Fibonacci()
        {
            Fibonacci f = new Fibonacci();
            DateTime start;
            DateTime end;
            TimeSpan lapse;


            Console.WriteLine("******************************************");
            Console.WriteLine("Fibonacci Numbers Test");
            Console.WriteLine("Calcualting 40th Fibonacii number, no memoization:");
            start = DateTime.Now;
            Console.WriteLine(f.Calculate(40));
            end = DateTime.Now;
            lapse = end - start;
            Console.WriteLine("Lapse: " + lapse.TotalSeconds);


            Console.WriteLine("Calcualting 1000th Fibonacii number with memoization:");
            start = DateTime.Now;
            Console.WriteLine(f.Calculate_With_Memo(1000));
            end = DateTime.Now;
            lapse = end - start;
            Console.WriteLine("Lapse: " + lapse.TotalSeconds);
            Console.WriteLine("******************************************");
        }

        public void Run()
        {
            //MIT Course Notes
            //http://courses.csail.mit.edu/6.006/fall09/lecture_notes/lecture18.pdf


            Console.WriteLine("******************************************");
            Console.WriteLine("Dynamic Programming");
            Console.WriteLine("******************************************");

            Test_Fibonacci();

        }
    }
}
