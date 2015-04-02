using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_LinkedIn_StringValidNumber
    {

        public bool IsStringAValidNumber(string val)
        {
            // {+/-}0-9.0-9

            bool foundDecimalPoint = false;
            bool foundPositiveNegativeIndicator = false;
            char lastDigit = '\0';
            for (int x=0; x < val.Length; x++)
            {
                lastDigit = val[x];
                if (x == 0)
                {
                    if (val[x] == '+' || val[x] == '-')
                    {
                        foundPositiveNegativeIndicator = true;
                        continue;
                    }
                    if (val[x] >= '0' && val[x] <= '9')
                        continue;
                    return false;
                }
                else if (x == 1 && foundPositiveNegativeIndicator)
                {
                    if (val[x] >= '0' && val[x] <= '9')
                        continue;
                    return false;
                }
                else
                {
                    if (val[x] >= '0' && val[x] <= '9')
                        continue;
                    if (val[x] == '.' && foundDecimalPoint == false)
                    {
                        foundDecimalPoint = true;
                        continue;
                    }
                }
                return false;
            }

            if (lastDigit >= '0' && lastDigit <= '9')
                return true;
            return false;


        }


        public void Run()
        {

            Console.WriteLine("Checking 203948011: " + IsStringAValidNumber("203948011"));
            Console.WriteLine("Checking +2039.48011: " + IsStringAValidNumber("+2039.48011"));
            Console.WriteLine("Checking -20.3948011: " + IsStringAValidNumber("-20.3948011"));
            Console.WriteLine("Checking -.203948011: " + IsStringAValidNumber("-.203948011"));
            Console.WriteLine("Checking +.203948011: " + IsStringAValidNumber("+.203948011"));
            Console.WriteLine("Checking 203948011.: " + IsStringAValidNumber("203948011."));
            Console.WriteLine("Checking -2039.480.11: " + IsStringAValidNumber("-2039.480.11"));
        }
    }
}
