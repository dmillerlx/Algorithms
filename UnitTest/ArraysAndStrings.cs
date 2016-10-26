using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms.Data_Structures;
using Algorithms.Scratch_Pad;
using System.Text;


namespace UnitTest
{
    [TestClass]
    public class ArraysAndStrings: TestBase
    {      

        [TestMethod]
        public void DataStructure_LinkedList()
        {
            DataStructure_LinkedList<int> list = new DataStructure_LinkedList<int>();

            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.Add(6);
            list.Add(7);

            Assert.AreEqual(list.Count, 7);

            list.Delete(4);

            Assert.AreEqual(list.Count, 6);
        }

        [TestMethod]
        public void ArrayString_AllUniqueChars()
        {
            string test1 = "abcdefg";

            Assert.IsTrue(ScratchPad.AllUniqueChars(test1.ToCharArray()));

            string test2 = "abccdefg";
            Assert.IsFalse(ScratchPad.AllUniqueChars(test2.ToCharArray()));
        }

        [TestMethod]
        public void ArrayString_Reverse()
        {
            string test1 = "abcdef";
            Assert.AreEqual(ToString(ScratchPad.Reverse(test1.ToCharArray())), "fedcba");

            string test2 = "abcdefg";
            Assert.AreEqual(ToString(ScratchPad.Reverse(test2.ToCharArray())), "gfedcba");
        }

        [TestMethod]
        public void ArrayString_IsPermutation()
        {
            string test1 = "abcdefg";
            string test1b = "bcdefag";

            Assert.IsTrue(ScratchPad.IsPermutation(test1.ToCharArray(), test1b.ToCharArray()));

            string test2 = "abcdefg";
            string test2b = "abcdefgh";
            Assert.IsFalse(ScratchPad.IsPermutation(test2.ToCharArray(), test2b.ToCharArray()));

            string test3 = "abcdefg";
            string test3b = "abcdeff";
            Assert.IsFalse(ScratchPad.IsPermutation(test3.ToCharArray(), test3b.ToCharArray()));


        }

        [TestMethod]
        public void ArrayString_BasicCompression()
        {
            Assert.AreEqual(ScratchPad.BasicStringCompression("aabcccccaaa"), "a2b1c5a3");


            Assert.AreEqual(ScratchPad.BasicStringCompression("abcdefgh"), "abcdefgh");


        }

        [TestMethod]
        public void ArrayString_Matrix_MakeRowColumnZero()
        {
            int[,] matrix1 =
            {
                {1,1,1,1},
                {1,0,1,1},
                {1,1,0,1},
                {1,1,1,1}
            };

            int[,] matrix1_solution =
            {
                {1,0,0,1},
                {0,0,0,0},
                {0,0,0,0},
                {1,0,0,1}
            };


            int[,] result = ScratchPad.Matrix_MakeRowColumnZero(matrix1, 4, 4);

            MatrixHelper_Print(result);

            Assert.IsTrue(MatrixHelper_Compare(matrix1, matrix1_solution));
            
        }

       

        
    }

}
