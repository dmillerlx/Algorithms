using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithms.Scratch_Pad;
using Algorithms;

namespace UnitTest
{
    [TestClass]
    public class MiscTests : TestBase
    {
        [TestMethod]
        public void QuickSort()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);

            int size = rnd.Next(10000000);
            int[] vals = new int[size];

            for (int x = 0; x < size; x++)
            {
                vals[x] = rnd.Next(100000000);
            }

            //int[] vals = new int[] { 1,2,3,4,5,5,5,5,5,5,5,5,5,5,5,7,8,9,10 };


            ScratchPad.QuickSort(ref vals);

            for (int x = 0; x < vals.Length; x++)
            {
                if (x > 0)
                {
                    Assert.IsTrue(vals[x] >= vals[x - 1]);
                }
            }



        }

        [TestMethod]
        public void MergeSort()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);

            int size = rnd.Next(10000000);
            int[] vals = new int[size];

            for (int x = 0; x < size; x++)
            {
                vals[x] = rnd.Next(100000000);
            }

            //int[] vals = new int[] { 1,2,3,4,5,5,5,5,5,5,5,5,5,5,5,7,8,9,10 };
            //int[] vals = new int[] { 10, 5, 1, 8, 2, 5, 1, 23, 1, 4, 2, 568, 12, 12345, 2, 3 };


            int[] sorted = ScratchPad.MergeSort(vals);

            for (int x = 0; x < sorted.Length; x++)
            {
                if (x > 0)
                {
                    Assert.IsTrue(sorted[x] >= sorted[x - 1]);
                }
            }



        }

        [TestMethod]
        public void PrintBinaryTreeTopToBottom()
        {
            //Random rnd = new Random((int)DateTime.Now.Ticks);

            //int size = rnd.Next(10000000);
            //int[] vals = new int[size];

            //for (int x = 0; x < size; x++)
            //{
            //    vals[x] = rnd.Next(100000000);
            //}

            //int[] vals = new int[] { 1,2,3,4,5,5,5,5,5,5,5,5,5,5,5,7,8,9,10 };
            //int[] vals = new int[] { 10, 5, 1, 8, 2, 5, 1, 23, 1, 4, 2, 568, 12, 12345, 2, 3 };


            //Algorithms.DataStructure_Tree tree = new Algorithms.DataStructure_Tree();

            //tree.CreateTree(new int[] { })
            //Node root = new Algorithms.Node();

            Node root = new Node(6);

            Node tmp = root;
            tmp.left = new Node(3,
                new Node(5, new Node(9), new Node(2, null, new Node(7))),
                new Node(1));

            tmp.right = new Node(4, null, new Node(0, new Node(8), null));


            int[] vals = ScratchPad.PrintBinaryTreeTopToBottom(root);

            foreach (int val in vals)
            {
                Console.Write(val + " ");
            }
            Console.WriteLine();

        }


        [TestMethod]
        public void FindBiggestPlusSign()
        {
            //int[,] matrix = new int[7, 7];
            //matrix[0] = new int[] { 0, 0, 1, 0, 0, 1, 0 };

            int[,] matrix = new int[7, 7]
            {
                {0,0,1,0,0,1,0 },
                {1,0,1,0,1,0,1 },
                {1,1,1,1,1,1,1 },
                {0,0,1,0,0,0,0 },
                {0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0 }
            };

            int solX;
            int solY;
            int solSize;
            ScratchPad.FindBiggestPlusSign(matrix, out solX, out solY, out solSize);

            Assert.AreEqual(solX, 2);
            Assert.AreEqual(solY, 2);
            Assert.AreEqual(solSize, 2);

        }


        [TestMethod]
        public void FlattenLinkedList()
        {
            ScratchPad.FNode root = new ScratchPad.FNode(10, null, null);

            ScratchPad.FNode[] arr = new ScratchPad.FNode[21];
            for (int x = 0; x < arr.Length; x++)
            {
                arr[x] = new ScratchPad.FNode(x, null, null);
            }

            arr[10].Next = arr[5];
            arr[5].Next = arr[12];
            arr[12].Next = arr[7];
            arr[7].Next = arr[11];

            arr[10].Child = arr[4];
            arr[4].Next = arr[20];
            arr[20].Next = arr[13];
            arr[20].Child = arr[2];
            arr[13].Child = arr[16];

            arr[16].Child = arr[3];

            arr[7].Child = arr[17];
            arr[17].Next = arr[6];

            arr[17].Child = arr[9];
            arr[9].Next = arr[8];

            arr[9].Child = arr[19];
            arr[19].Next = arr[15];

            root = arr[10];


            root = ScratchPad.FlattenList(root);

            ScratchPad.FNode current = root;
            while (current != null)
            {
                if (current != root)
                    Console.Write("->" + current.Value);
                else Console.Write(current.Value);
                current = current.Next;
            }

        }
    }
}
