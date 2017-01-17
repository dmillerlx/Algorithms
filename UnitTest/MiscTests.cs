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


        [TestMethod]
        public void FindClosestSearchValue()
        {
            int[,] matrix = new int[7, 7]
           {
                {0,0,1,0,0,1,0 },
                {1,0,1,0,1,0,1 },
                {1,1,1,1,1,1,1 },
                {0,0,1,5,0,0,0 },
                {0,0,0,0,5,0,0 },
                {0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0 }
           };

            int searchX;
            int searchY;
            bool ret = ScratchPad.FindClosestValue(matrix, 0, 0, 5, out searchX, out searchY);

            Assert.AreEqual(ret, true);
            Assert.AreEqual(searchX, 3);
            Assert.AreEqual(searchY, 3);

        }

        [TestMethod]
        public void FindLargestSum()
        {

            int[] vals = { -2, -5, 6, -2, -3, 1, 5, -6 };

            int val = ScratchPad.SubArrayLargestSum(vals);

            Assert.AreEqual(val, 7);

            int[] vals2 = { -1, -5, -2, -3, -5, -10 };

            val = ScratchPad.SubArrayLargestSum(vals2);

            Assert.AreEqual(val, -1);

            int[] vals3 = { -10, -5, -2, -3, -5, -10 };

            val = ScratchPad.SubArrayLargestSum(vals3);

            Assert.AreEqual(val, -2);

        }

        [TestMethod]
        public void FindSmallestSortIndices()
        {

            // 1,2,4,7,10,11,7,12,6,7,16,18,19
            int[] vals = { 1, 2, 4, 7, 10, 11, 7, 12, 6, 7, 16, 18, 19 };

            int start;
            int end;
            bool ret = ScratchPad.FindSmallestSortIndicies(vals, out start, out end);

            Assert.IsTrue(ret);
            Assert.AreEqual(start, 3);
            Assert.AreEqual(end, 9);

            vals = new[] { 1, 2, 16, 3, 6, 10, 11, 4, 6, 19, 20 };
            ret = ScratchPad.FindSmallestSortIndicies(vals, out start, out end);

            Assert.IsTrue(ret);
            Assert.AreEqual(start, 2);
            Assert.AreEqual(end, 8);

        }


        [TestMethod]
        public void FindLongestWordMadeOfOtherWords()
        {
            string[] words = { "cat", "banana", "dog", "nana", "walk", "walker", "dogwalker" };

            string result = ScratchPad.FindLongestWordMadeOfOtherWords(words);
        }

        [TestMethod]
        public void ReverseStringExcludingSpecialChars()
        {
            string input = "a,b$c";
            string specialChars = "$,!";
            string output = new string(ScratchPad.ReverseStringExcludingSpecialChars(input.ToArray(), specialChars.ToArray()));

            Assert.AreEqual(output, "c,b$a");

            input = "Ab,c,de!$";
            output = new string(ScratchPad.ReverseStringExcludingSpecialChars(input.ToArray(), specialChars.ToArray()));
            Assert.AreEqual(output, "ed,c,bA!$");

        }


        [TestMethod]
        public void FindPalindromicPartitions()
        {
            string input = "nitin";

            List<string> ret = ScratchPad.FindPalindromicPartitions(input.ToArray());
            Assert.AreEqual(ret.Count, 3);
            Assert.AreEqual(ret[0], "");

        }

        [TestMethod]
        public void FindLongestSubstringWithoutRepeatChars()
        {
            string input = "abccdefgh";

            string output = ScratchPad.FindLongestSubstringWithoutRepeatCharsV2(input.ToArray());

            Assert.AreEqual(output, "cdefgh");

        }

        [TestMethod]
        public void ReorderSingleLinkedList()
        {
            ScratchPad.LinkedListNode root = new ScratchPad.LinkedListNode(1);
            root.Next = new ScratchPad.LinkedListNode(2);
            root.Next.Next = new ScratchPad.LinkedListNode(3);
            root.Next.Next.Next = new ScratchPad.LinkedListNode(4);
            root.Next.Next.Next.Next = new ScratchPad.LinkedListNode(5);


            //Input  1 -> 2 -> 3 -> 4 -> 5
            //Output 1 -> 5 -> 2 -> 4 -> 3

            ScratchPad.LinkedListNode result = ScratchPad.ReorderSingleLinkedList(root);
            Assert.AreEqual(root.Data, 1);
            Assert.AreEqual(root.Next.Data, 5);
            Assert.AreEqual(root.Next.Next.Data, 2);
            Assert.AreEqual(root.Next.Next.Next.Data, 4);
            Assert.AreEqual(root.Next.Next.Next.Next.Data, 3);

        }
        [TestMethod]
        public void ReplaceSpaces()
        {
            string input = "Mr John Smith    ";

            string output = new string (ScratchPad.ReplaceSpaces(input.ToArray(), 13));

            Assert.AreEqual(output, "Mr%20John%20Smith");

        }

        [TestMethod]
        public void RotateMatrix()
        {
            // 11112222333344445555
            // aaaabbbbccccddddeeee
            // 66667777888899990000
            // ffffgggghhhhiiiijjjj

            // ffff6666aaaa1111
            // gggg7777bbbb2222
            // hhhh8888cccc3333
            // iiii9999dddd4444
            // jjjj0000eeee5555

            char[,] input = { 
                            { '1','1','1','1','2','2','2','2','3','3','3','3','4','4','4','4','5','5','5','5' },
                            { 'a','a','a','a','b','b','b','b','c','c','c','c','d','d','d','d','e','e','e','e' },
                            { '6','6','6','6','7','7','7','7','8','8','8','8','9','9','9','9','0','0','0','0' },
                            { 'f','f','f','f','g','g','g','g','h','h','h','h','i','i','i','i','j','j','j','j' }
                            };


            char [,]output = ScratchPad.RotateMatrix(input);

            output = ScratchPad.RotateMatrix(output);

            output = ScratchPad.RotateMatrix(output);

            output = ScratchPad.RotateMatrix(output);

            for (int y=0; y < input.GetLength(0); y++)
            {
                for (int x=0; x < input.GetLength(1); x++)
                {
                    System.Diagnostics.Debug.Write(input[y, x]);
                }
                System.Diagnostics.Debug.WriteLine(string.Empty);
            }

            Console.WriteLine("-------------------");

            for (int y = 0; y < output.GetLength(0); y++)
            {
                for (int x = 0; x < output.GetLength(1); x++)
                {
                    System.Diagnostics.Debug.Write(output[y, x]);
                }
                System.Diagnostics.Debug.WriteLine(string.Empty);
            }

        }

        [TestMethod]
        public void FindAmazingRotation()
        {
            int[] input = { 0, 1, 2, 3, 4, 5, 6 };
            int ret = ScratchPad.FindAmazingNumberOffset(input);

            Assert.AreEqual(ret, 0);

            input = new int []{ 4, 2, 8, 2, 4, 5, 3 };
            ret = ScratchPad.FindAmazingNumberOffset(input);
            Assert.AreEqual(ret, 0);


            input = new int []{ 1, 2, 3, 4, 5, 1, 2, 9, 10, 11, 1, 2, 3, 4, 5, 6 };
            ret = ScratchPad.FindAmazingNumberOffset(input);
            Assert.AreEqual(ret, 9);
            

        }

        [TestMethod]
        public void SortArraySquares()
        {
            //-10, -5, -1, 1, 3, 5, 10

            //100, 25, 1, 1, 9, 25, 100

            int[] input = { -10, -5, -1, 1, 3, 5, 10 };

            int[] expectedOutput = { 1, 1, 9, 25, 25, 100, 100 };

            int[] ret = ScratchPad.SortSquaresOfIntegers(input);

            for (int x=0; x < ret.Length; x++)
            {
                Assert.AreEqual(ret[x], expectedOutput[x]);
            }
        }

        [TestMethod]
        public void FindContingousSum()
        {
            // 1, 3, 5, 18      x = 8

            Assert.IsTrue(ScratchPad.HasContigousSubArraySum(new int[] { 1, 3, 5, 18 }, 8));
            Assert.IsTrue(ScratchPad.HasContigousSubArraySum(new int[] { 1, 3, 5, 18 }, 9));
            Assert.IsFalse(ScratchPad.HasContigousSubArraySum(new int[] { 1, 3, 5, 18 }, 10));
            Assert.IsFalse(ScratchPad.HasContigousSubArraySum(new int[] { 1, 3, 5, 18 }, 40));


            Assert.IsTrue(ScratchPad.HasContigousSubArraySum(new int[] { 10, 1, 2, 3, 5, 18 }, 8));
            Assert.IsTrue(ScratchPad.HasContigousSubArraySum(new int[] { 10, 1, 2, 3, 5, 18 }, 18));
            Assert.IsTrue(ScratchPad.HasContigousSubArraySum(new int[] { 10, 1, 2, 3, 5, 18 }, 3));

        }

        [TestMethod]
        public void FindKClosestPoint()
        {
            List<ScratchPad.MyPoint> points = new List<ScratchPad.MyPoint>();

            points.Add(new ScratchPad.MyPoint() { X = 5, Y = 5 });
            points.Add(new ScratchPad.MyPoint() { X = 6, Y = 6 });
            points.Add(new ScratchPad.MyPoint() { X = 2, Y = 4 });
            points.Add(new ScratchPad.MyPoint() { X = 1, Y = 5 });
            points.Add(new ScratchPad.MyPoint() { X = 3, Y = 7 });
            points.Add(new ScratchPad.MyPoint() { X = 8, Y = 2 });

            int k = 2;
            ScratchPad.MyPoint kthItem = ScratchPad.FindClosestKPoint(points.ToArray(), k);

            System.Diagnostics.Debug.WriteLine(string.Format("Kth ({0}) Item.X({1}) Item.Y({2}) Item.Distance({3})", k, kthItem.X, kthItem.Y, kthItem.Distance));


        }


        [TestMethod]
        public void RemoveInvalidParentheses()
        {
            string input = "(ab(c)";
            string expectedOutput = "ab(c)";

            string ret = ScratchPad.RemoveInvalidParentheses(input);
            System.Diagnostics.Debug.WriteLine(string.Format("Input: {0}\nOutput: {1}", input, ret));

            Assert.AreEqual(ret, expectedOutput);


            input = "(ab(cd)(e(df(ea(sdf)qe)e))";
            ret = ScratchPad.RemoveInvalidParentheses(input);
            System.Diagnostics.Debug.WriteLine(string.Format("Input: {0}\nOutput: {1}", input, ret));

        }

        [TestMethod]
        public void EnsureNDashesBetweenTwoChars()
        {
            //Ensure that there are a minimum of n dashes between any two of the same characters of a string.
            //Example: n = 2, string = "ab-bcdecca" -> "ab--bcdec--ca"

            string input = "---ab-bcdecca---";
            string expectedOutput = "---ab--bcdec--ca---";

            string ret = ScratchPad.EnsureNDashesBetweenTwoChars(input, 2);

            Assert.AreEqual(ret, expectedOutput);


            input = "A-ABCCD-F-F-G--G--H";
            expectedOutput = "A----ABC----CD-F----F-G----G--H";
            ret = ScratchPad.EnsureNDashesBetweenTwoChars(input, 4);

            Assert.AreEqual(ret, expectedOutput);

        }

        [TestMethod]
        public void CompressString()
        {
            string input = "aabccccaaa";
            string expectedOutput = "a2b1c4a3";

            string ret = ScratchPad.StringCompression(input);

            Assert.AreEqual(ret, expectedOutput);


        }

        [TestMethod]
        public void FindMaxDrop()
        {
            int[] input = { 1, 5, 10, 3, 7, 15, 8, 9, 4, 3, 1, 3 };
            int expectedOutput = 14;

            int ret = ScratchPad.FindMaximumDrop(input);

            Assert.AreEqual(ret, expectedOutput);

        }

        [TestMethod]
        public void ReorderArray()
        {
            char[] A = { 'C', 'D', 'E', 'F', 'G' };
            int[] B = { 3, 0, 4, 1, 2 };

            ScratchPad.Reorder(ref A, ref B);

            char[] C = { 'D', 'F', 'G', 'C', 'E' };
            for (int x = 0; x < C.Length; x++)
            {
                Assert.AreEqual(A[x], C[x]);
            }

        }          


    }
}