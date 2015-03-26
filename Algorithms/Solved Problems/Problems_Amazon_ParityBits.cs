using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_Amazon_ParityBits
    {
        private int Parity(int x)
        {
            //return (x == 0) ? 0 : Parity(x >> 1) + (x & 0x1);

            if (x == 0)
                return 0;
            return Parity(x >> 1) + (x & 0x1);
        }

        // 0b00 & 0b01 = 0b00 = 0
        
        // 0b11 & 0b01 = 0b01 = 1
        // 0b01 & 0b01 = 0b01 = 1


        public void Run()
        {
            //Console.WriteLine("Parity: " + Parity(0,0));    //0b00
            //Console.WriteLine("Parity: " + Parity(1,0));    //0b01
            //Console.WriteLine("Parity: " + Parity(2,0));    //0b10
            //Console.WriteLine("Parity: " + Parity(3,0));    //0b11
            //Console.WriteLine("Parity: " + Parity(4,0));    //0b100

            Console.WriteLine("Parity: " + Parity(0));    //0b00
            Console.WriteLine("Parity: " + Parity(1));    //0b01
            Console.WriteLine("Parity: " + Parity(2));    //0b10
            Console.WriteLine("Parity: " + Parity(3));    //0b11
            Console.WriteLine("Parity: " + Parity(4));    //0b100

        }

        //  0 ^ 0 = 0
        //  0 ^ 1 = 1
        //  1 ^ 1 = 0

       
    }
}
