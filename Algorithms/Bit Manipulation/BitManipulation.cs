using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Bit_Manipulation
{
    public class BitManipulation
    {
        public static bool GetBit(int num, int i)
        {
            return ((num & (1 << i)) != 0);
        }

        public static int SetBit(int num, int i)
        {
            // 0001000
            //Set bit 3
            // 0001100

            // 1 << 3 = 0000100
            // 0001000 |
            // 0000100
            //---------
            // 0001100

            return (num | (1 << i));
        }

        public static int ClearBit(int num, int i)
        {
            //0101100
            //Clear bit 3
            //0101000

            //0101100
            //0101000
            //-------
            //0101000

            int val = SetBit(0, i);
            //0000100

            val = ~val;
            //1111011

            return num & val;

        }

    }
}
