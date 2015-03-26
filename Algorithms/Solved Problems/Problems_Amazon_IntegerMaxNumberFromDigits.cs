using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_Amazon_IntegerMaxNumberFromDigits
    {
        /* Given an integer find the maximum value that can be formed from the digits.
         * Input: 8754365
         * Output: 8765543
         * 
         */

        public int Process(int val)
        {
            bool negative = (val < 0);

            //First get individual digits from the number

            if (negative)
                val = val * -1;

            int[] digits = new int[10];

            while (val > 10)
            {
                int val2 = val / 10;
                int digit2 = val - val2 * 10;

                val = val2;

                digits[digit2]++;
            }

            digits[val]++;


            bool firstDigit = true;
            int ret = 0;
            if (!negative)
            {
                for (int x=9; x >= 0; x--)
                {
                    while (digits[x] > 0)
                    {
                        if (firstDigit)
                        {
                            ret = x;
                            firstDigit = false;
                        }
                        else
                        {
                            ret = ret * 10 + x;
                        }
                        digits[x]--;
                    }
                }
            }
            else
            {
                for (int x = 0; x <= 9; x++)
                {
                    while (digits[x] > 0)
                    {
                        if (firstDigit)
                        {
                            ret = x;
                            firstDigit = false;
                        }
                        else
                        {
                            ret = ret * 10 + x;
                        }
                        digits[x]--;
                    }
                }

                ret = ret * -1;
            }

            return ret;
        }

        public void Run()
        {
            Console.WriteLine(Process(87543659));
            Console.WriteLine(Process(-87543659));
        }

    }
}
