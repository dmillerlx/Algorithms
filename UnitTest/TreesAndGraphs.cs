using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class TreesAndGraphs: TestBase
    {
        [TestMethod]
        public void Trees_IsTreeBalanced()
        {
            Algorithms.DataStructure_Tree tree = new Algorithms.DataStructure_Tree();

            int[] vals = { 1, 5, 2, 3, 7, 3, 5, 2, 8, 10, 25, 1, 5, 2 };
            Algorithms.Node root = tree.CreateTree(vals);

            tree.PrintValues(root);
            tree.PrintValues_BFD(root);

            Assert.IsFalse(Algorithms.Scratch_Pad.ScratchPad.IsBinaryTreeBalanced(root));

            //  50
            // 3  6
            //2 4   7

            int[] vals2 = { 5, 3, 6, 2, 4, 7 };
            root = tree.CreateTree(vals2);

            Assert.IsTrue(Algorithms.Scratch_Pad.ScratchPad.IsBinaryTreeBalanced(root));

        }
    }
}
