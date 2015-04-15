using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_LinkedIn_POW_No_BuiltInFunctions
    {

        public double pow(double a, int b)
        {
            if (a == 0)
                return 0;
            else if (b == 0)
                return 1;
            else if (b > 0)
            {
                return powPos(a, b);
            }
            //else if (b < 0)
            return 1 / powPos(a, b * -1);
        }

        private double powPos(double a, int b)
        {
            if (b == 1)
                return a;
            else return a * powPos(a, b - 1);
        }


        public void Run()
        {
            Console.WriteLine(pow(5, 2));
            Console.WriteLine(pow(5, 3));
            Console.WriteLine(pow(5, 1));
            Console.WriteLine(pow(5, 0));
            Console.WriteLine(pow(5, -1));
            Console.WriteLine(pow(5, -2));


        }


    }
}
