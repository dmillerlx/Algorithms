using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Algorithms
{
    class Codity
    {
        public int result_upto(int n, int k)
        {
            if (n >= 0)
                return n; //k + 1 % account for 0
            else return 0;
        }

        public int solution(int a, int b, int k)
        {

            int result = result_upto(b, k) - result_upto(a - 1, k);

            Debug.WriteLine("solution(" + a + "," + b + "," + k + ") = " + result);

            return result;

        }

        public int solution2(int a, int b, int k)
        {
            int result = ((b - a) / k) + 1;

            Debug.WriteLine("solution(" + a + "," + b + "," + k + ") = " + result);

            return result;

        }


    }
}
