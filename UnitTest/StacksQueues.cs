using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithms.Data_Structures;
using Algorithms.Scratch_Pad;

namespace UnitTest
{
    [TestClass]
    public class StacksQueues
    {
        [TestMethod]
        public void StacksQueues_StackTest()
        {
            DataStructure_Stack<int> stack = new DataStructure_Stack<int>();

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);

            Assert.IsFalse(stack.IsEmpty);
            Assert.AreEqual(stack.Pop(), 5);
            Assert.AreEqual(stack.Pop(), 4);
            Assert.AreEqual(stack.Peek(), 3);
            Assert.AreEqual(stack.Pop(), 3);
            Assert.AreEqual(stack.Pop(), 2);
            Assert.AreEqual(stack.Pop(), 1);
            Assert.IsTrue(stack.IsEmpty);

        }

        [TestMethod]
        public void StacksQueues_QueueTest()
        {
            DataStructure_Queue<int> queue = new DataStructure_Queue<int>();

            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Enqueue(5);

            Assert.IsFalse(queue.IsEmpty);
            Assert.AreEqual(queue.Dequeue(), 1);
            Assert.AreEqual(queue.Dequeue(), 2);
            Assert.AreEqual(queue.Peek(), 3);
            Assert.AreEqual(queue.Dequeue(), 3);
            Assert.AreEqual(queue.Dequeue(), 4);
            Assert.AreEqual(queue.Dequeue(), 5);
            Assert.IsTrue(queue.IsEmpty);
        }


        [TestMethod]
        public void StacksQueues_SingleArrayStack()
        {
            Algorithms.Scratch_Pad.ScratchPad.SingleArrayStack stack = new ScratchPad.SingleArrayStack();

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);

            Assert.IsFalse(stack.IsEmpty);
            Assert.AreEqual(stack.Pop(), 5);
            Assert.AreEqual(stack.Pop(), 4);
            Assert.AreEqual(stack.Peek(), 3);
            Assert.AreEqual(stack.Pop(), 3);
            Assert.AreEqual(stack.Pop(), 2);
            Assert.AreEqual(stack.Pop(), 1);
            Assert.IsTrue(stack.IsEmpty);

        }


        [TestMethod]
        public void StacksQueues_StackWithMin()
        {
            Algorithms.Scratch_Pad.ScratchPad.StackWithMin stack = new ScratchPad.StackWithMin();

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);

            Assert.IsFalse(stack.IsEmpty);
            Assert.AreEqual(stack.Min(), 1);
            Assert.AreEqual(stack.Pop(), 5);
            Assert.AreEqual(stack.Pop(), 4);
            Assert.AreEqual(stack.Min(), 1);
            Assert.AreEqual(stack.Peek(), 3);
            Assert.AreEqual(stack.Pop(), 3);
            Assert.AreEqual(stack.Min(), 1);
            Assert.AreEqual(stack.Pop(), 2);
            Assert.AreEqual(stack.Pop(), 1);
            Assert.IsTrue(stack.IsEmpty);

            stack.Push(4);
            stack.Push(8);
            stack.Push(7);
            stack.Push(1);
            stack.Push(3);

            Assert.IsFalse(stack.IsEmpty);
            Assert.AreEqual(stack.Min(), 1);
            Assert.AreEqual(stack.Pop(), 3);
            Assert.AreEqual(stack.Pop(), 1);
            Assert.AreEqual(stack.Min(), 4);
            Assert.AreEqual(stack.Peek(), 7);
            Assert.AreEqual(stack.Pop(), 7);
            Assert.AreEqual(stack.Min(), 4);
            Assert.AreEqual(stack.Pop(), 8);
            Assert.AreEqual(stack.Pop(), 4);
            Assert.IsTrue(stack.IsEmpty);
        }

        [TestMethod]
        public void StacksQueues_StackWithMin2()
        {
            Algorithms.Scratch_Pad.ScratchPad.StackWithMin2 stack = new ScratchPad.StackWithMin2();

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);

            Assert.IsFalse(stack.IsEmpty);
            Assert.AreEqual(stack.Min(), 1);
            Assert.AreEqual(stack.Pop(), 5);
            Assert.AreEqual(stack.Pop(), 4);
            Assert.AreEqual(stack.Min(), 1);
            Assert.AreEqual(stack.Peek(), 3);
            Assert.AreEqual(stack.Pop(), 3);
            Assert.AreEqual(stack.Min(), 1);
            Assert.AreEqual(stack.Pop(), 2);
            Assert.AreEqual(stack.Pop(), 1);
            Assert.IsTrue(stack.IsEmpty);

            stack.Push(4);
            stack.Push(8);
            stack.Push(7);
            stack.Push(1);
            stack.Push(3);

            Assert.IsFalse(stack.IsEmpty);
            Assert.AreEqual(stack.Min(), 1);
            Assert.AreEqual(stack.Pop(), 3);
            Assert.AreEqual(stack.Pop(), 1);
            Assert.AreEqual(stack.Min(), 4);
            Assert.AreEqual(stack.Peek(), 7);
            Assert.AreEqual(stack.Pop(), 7);
            Assert.AreEqual(stack.Min(), 4);
            Assert.AreEqual(stack.Pop(), 8);
            Assert.AreEqual(stack.Pop(), 4);
            Assert.IsTrue(stack.IsEmpty);
        }


        

    }
}
