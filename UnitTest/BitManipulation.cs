using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class BitManipulation: TestBase
    {
        [TestMethod]
        public void BitManipulation_SetBits()
        {
            //0101
            int val = 5;

            Assert.AreEqual(Algorithms.Bit_Manipulation.BitManipulation.SetBit(val, 0), 5);
            Assert.AreEqual(Algorithms.Bit_Manipulation.BitManipulation.SetBit(val, 1), 7);
            Assert.AreEqual(Algorithms.Bit_Manipulation.BitManipulation.SetBit(val, 2), 5);
            Assert.AreEqual(Algorithms.Bit_Manipulation.BitManipulation.SetBit(val, 3), 13);


            Assert.AreEqual(Algorithms.Bit_Manipulation.BitManipulation.ClearBit(val, 0), 4);
            Assert.AreEqual(Algorithms.Bit_Manipulation.BitManipulation.ClearBit(val, 1), 5);
            Assert.AreEqual(Algorithms.Bit_Manipulation.BitManipulation.ClearBit(val, 2), 1);
            Assert.AreEqual(Algorithms.Bit_Manipulation.BitManipulation.ClearBit(val, 3), 5);

            Assert.AreEqual(Algorithms.Bit_Manipulation.BitManipulation.GetBit(val, 0), true);
            Assert.AreEqual(Algorithms.Bit_Manipulation.BitManipulation.GetBit(val, 1), false);
            Assert.AreEqual(Algorithms.Bit_Manipulation.BitManipulation.GetBit(val, 2), true);
            Assert.AreEqual(Algorithms.Bit_Manipulation.BitManipulation.GetBit(val, 3), false);


        }

    }
}
