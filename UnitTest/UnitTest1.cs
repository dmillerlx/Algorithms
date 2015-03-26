using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms.Data_Structures;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DataStructure_LinkedList()
        {


            DataStructure_LinkedList <int> list = new DataStructure_LinkedList<int>();

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
    }
}
