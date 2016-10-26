using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms.Data_Structures;
using Algorithms.Scratch_Pad;

namespace UnitTest
{
    [TestClass]
    public class LinkedLists: TestBase
    {
        [TestMethod]
        public void LinkedLists_FindKthLastElement()
        {
            DataStructure_LinkedList<int> list = new Algorithms.Data_Structures.DataStructure_LinkedList<int>();

            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.Add(6);
            list.Add(7);
            list.Add(8);
            list.Add(9);
            list.Add(10);

            Assert.AreEqual(ScratchPad.FindKthLastElement(list, 0).Data, 10);

            Assert.AreEqual(ScratchPad.FindKthLastElement(list, 11), null);

            Assert.AreEqual(ScratchPad.FindKthLastElement(list, 5).Data, 5);

            Assert.AreEqual(ScratchPad.FindKthLastElement(null, 11), null);
        }

        [TestMethod]
        public void LinkedLists_AddLinkedLists()
        {
            DataStructure_LinkedList<int> one = new DataStructure_LinkedList<int>();
            DataStructure_LinkedList<int> two = new DataStructure_LinkedList<int>();

            //617
            one.Add(7);
            one.Add(1);
            one.Add(6);

            //295
            two.Add(5);
            two.Add(9);
            two.Add(2);

            DataStructure_LinkedList<int> sol = new DataStructure_LinkedList<int>();
            //617 + 295 = 912
            sol.Add(2);
            sol.Add(1);
            sol.Add(9);

            DataStructure_LinkedList<int> output = ScratchPad.AddLinkedLists(one, two);

            Assert.IsTrue(LinkedList_Compare(output, sol));

        }

        [TestMethod]
        public void LinkedLists_AddLinkedLists2()
        {
            DataStructure_LinkedList<int> one = new DataStructure_LinkedList<int>();
            DataStructure_LinkedList<int> two = new DataStructure_LinkedList<int>();

            //6174
            one.Add(4);
            one.Add(7);
            one.Add(1);
            one.Add(6);

            //295
            two.Add(5);
            two.Add(9);
            two.Add(2);

            DataStructure_LinkedList<int> sol = new DataStructure_LinkedList<int>();
            //6174 + 295 = 6469
            sol.Add(9);
            sol.Add(6);
            sol.Add(4);
            sol.Add(6);

            DataStructure_LinkedList<int> output = ScratchPad.AddLinkedLists(one, two);

            Assert.IsTrue(LinkedList_Compare(output, sol));

        }

        [TestMethod]
        public void LinkedList_IsPalindrome()
        {
            DataStructure_LinkedList<int> one = new DataStructure_LinkedList<int>();

            one.Add(1);
            one.Add(2);
            one.Add(3);
            one.Add(4);
            one.Add(4);
            one.Add(3);
            one.Add(2);
            one.Add(1);

            Assert.IsTrue(ScratchPad.IsLinkedListPalindrome(one));

            one = new DataStructure_LinkedList<int>();

            one.Add(1);
            one.Add(2);
            one.Add(3);
            one.Add(4);
            one.Add(3);
            one.Add(2);
            one.Add(1);

            Assert.IsTrue(ScratchPad.IsLinkedListPalindrome(one));

            one = new DataStructure_LinkedList<int>();

            one.Add(1);
            one.Add(2);
            one.Add(3);
            one.Add(4);
            one.Add(4);
            one.Add(2);
            one.Add(2);
            one.Add(1);

            Assert.IsFalse(ScratchPad.IsLinkedListPalindrome(one));

        }


    }
}
