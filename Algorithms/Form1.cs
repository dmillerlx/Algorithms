﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using DataStructureLibrary.RedBlackTree;
using SelfBalancedTree;

namespace Algorithms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataStructure_Tree t = new DataStructure_Tree();
            t.BTreeTest();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Codity c = new Codity();

            int result;
            c.solution2(0, 0, 1);
            c.solution2(0, 1, 1);
            c.solution2(0, 5, 2);

            c.solution2(6, 10, 2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Heap<int> h = new Heap<int>(Heap<int>.heapTypeEnum.max);
            int[] vals = new int[] { 0, 1, 2, 3, 4, 5, 4, 3, 2, 1, 10, 20, 30, 0, 1, 2, 3, 4, 5, 4, 3, 2, 1, 10, 20, 30 };
            h.BuildHeap(vals);

            Debug.WriteLine("------Dequeue 10------");
            for (int x = 0; x < 10; x++)
            //while (h.size > 10)
            {
                Debug.WriteLine(h.Dequeue());
            }                      

            Random rnd = new Random((int)DateTime.Now.Ticks);

            for (int x = 0; x < 100; x++)
            {
                int val = rnd.Next(100);
                
                if (val < 50)
                {
                    Debug.WriteLine("------Enqueue------");
                    for (int y = 0; y < 25; y++)
                    {
                        h.Enqueue(rnd.Next(1000));
                    }
                }
                else
                {
                    Debug.WriteLine("------Dequeue------");
                    for (int z = 0; z < 15 && h.size > 0; z++)
                    {
                        Debug.WriteLine(h.Dequeue());
                    }
                }
                
            }

            Debug.WriteLine("----Heap Sort----");

            int sizeOfSortedValues = 0;
            int[] sortedValues = h.Sort(out sizeOfSortedValues);

            for (int x = 0; x < sortedValues.Length && x < sizeOfSortedValues; x++)
            {
                Debug.WriteLine(sortedValues[x]);
            }


            //Debug.WriteLine("------Dequeue Flush------");
            //while (h.size > 0)
            //{
            //    Debug.WriteLine(h.Dequeue());
            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int[] vals = new int[] { 0, 1, 2, 3, 4, 5, 4, 3, 2, 1, 10, 20, 30, 0, 1, 2, 3, 4, 5, 4, 3, 2, 1, 10, 20, 30 };
            MergeSort<int> h = new MergeSort<int>();

            int []valsSorted = h.Sort(vals);

            Debug.WriteLine("Sorted values: ");
            for (int x = 0; x < valsSorted.Length; x++)
            {
                Debug.WriteLine(valsSorted[x]);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int[] vals = new int[] { 0, 1, 2, 3, 4, 5, 4, 3, 2, 1, 10, 20, 30, 0, 1, 2, 3, 4, 5, 4, 3, 2, 1, 10, 20, 30 };
            vals = new int[100];
            Random rnd = new Random((int)System.DateTime.Now.Ticks);
            for (int x = 0; x < vals.Length; x++)
            {
                vals[x] = rnd.Next(100);
            }
            
            MergeSort<int> h = new MergeSort<int>();

            int[] valsSorted = h.Sort_NoRecursion_v2(vals);

            Debug.WriteLine("Non Recursive Sorted values: ");
            for (int x = 0; x < valsSorted.Length; x++)
            {
                Debug.WriteLine(valsSorted[x]);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TestSingleton();
        }

        public void TestSingleton()
        {
            for (int x = 0; x < 50; x++)
            {
                System.Threading.Thread t = new System.Threading.Thread(Run);
                t.Start();
            }
        }


        public void Run()
        {
            //Singleton s = new Singleton().GetInstance();
            SingletonV2 s = SingletonV2.instance;
            Debug.WriteLine("ThreadID: "+System.Threading.Thread.CurrentThread.GetHashCode() +" Singleton Instance: " + s.myInstanceNumber);

        }

        private void Stradegy_Click(object sender, EventArgs e)
        {
            DesignPattern_Stradegy s = new DesignPattern_Stradegy();
            s.Run();

            DesignPattern_Stradegy_V2 s2 = new DesignPattern_Stradegy_V2();
            s2.Run();
        
        }

        private void button7_Click(object sender, EventArgs e)
        {
            InterfaceTest i = new InterfaceTest();

            i.Run();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Problems_MergeSortedArrays merge = new Problems_MergeSortedArrays();

            char []A = new char[10]{'1', '2', '3', '4', '5', '0', '0', '0', '0', '0'};
            char []B = new char[5]{'2', '4', '6', '8', '9'};
            int m = 5;
            int n = 5;

            merge.MergeSortedArrays(A, m, B, n);

            Debug.WriteLine("Merge Sorted Arrays");
            Debug.WriteLine("-------------------");
            foreach (char c in A)
            {
                Debug.WriteLine(c);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            int numArrays = r.Next() % 10;
            int[][] data = new int[numArrays][];
            for (int i = 0; i < numArrays; i++)
            {
                data[i] = new int[r.Next() % 10];
                int lastValue = -100;
                for (int j = 0; j < data[i].Length; j++)
                {
                    lastValue += r.Next() % 100;
                    data[i][j] = lastValue;
                }
            }

            Debug.WriteLine("Merge K sorted Arrays.  Input");
            Debug.WriteLine("----------------------");
            for (int x = 0; x < data.Length; x++)
            {
                for (int y = 0; y < data[x].Length; y++ )
                {
                    if (y > 0)
                        Debug.Write(",");
                    Debug.Write(data[x][y]);
                }
                Debug.WriteLine("");
            }

            Debug.WriteLine("----------------------");

            Debug.WriteLine("Sorting...");
            Problems_MergeSortedArrays merge = new Problems_MergeSortedArrays();
            int[] sorted = merge.MergeKSortedArrays(data, data.Length);
            Debug.WriteLine("Sorted Output");
            Debug.WriteLine("----------------------");
            for (int x = 0; x < sorted.Length; x++)
            {
                Debug.WriteLine(sorted[x]);
            }


        }

        private void button10_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            int numArrays = r.Next() % 10;
            int[][] data = new int[numArrays][];
            for (int i = 0; i < numArrays; i++)
            {
                data[i] = new int[r.Next() % 10];
                int lastValue = -100;
                for (int j = 0; j < data[i].Length; j++)
                {
                    lastValue += r.Next() % 100;
                    data[i][j] = lastValue;
                }
            }

            Debug.WriteLine("Merge K sorted Arrays -- USING MAX HEAP.  Input");
            Debug.WriteLine("----------------------");
            for (int x = 0; x < data.Length; x++)
            {
                for (int y = 0; y < data[x].Length; y++)
                {
                    if (y > 0)
                        Debug.Write(",");
                    Debug.Write(data[x][y]);
                }
                Debug.WriteLine("");
            }

            Debug.WriteLine("----------------------");

            Debug.WriteLine("Sorting...");
            Problems_MergeSortedArrays merge = new Problems_MergeSortedArrays();
            int[] sorted = merge.MergeKSortedArrays_UsingMaxHeap(data, data.Length);
            Debug.WriteLine("Sorted Output");
            Debug.WriteLine("----------------------");
            for (int x = 0; x < sorted.Length; x++)
            {
                Debug.WriteLine(sorted[x]);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DesignPattern_AbstractFactory f = new DesignPattern_AbstractFactory();

            f.RunDemo();

        }

        private void button12_Click(object sender, EventArgs e)
        {

            //DesignPattern_Builder.BuilderDemo01 f = new DesignPattern_Builder.BuilderDemo01();
            //f.RunDemo();

            DesignPattern_Builder.BuilderDemo02 f2 = new DesignPattern_Builder.BuilderDemo02();
            f2.Run();
        
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Problems_PrintSpiralMatrix p = new Problems_PrintSpiralMatrix();

            p.Run();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            DesignPattern_Flyweight_Form form = new DesignPattern_Flyweight_Form();
            form.ShowDialog();

        }

        private void button15_Click(object sender, EventArgs e)
        {
            DesignPattern_Composite composite = new DesignPattern_Composite();

            composite.Run();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            DesignPattern_Decorator decorator = new DesignPattern_Decorator();

            decorator.Run();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            DesignPattern_Composite_with_Decorator pattern = new DesignPattern_Composite_with_Decorator();
            pattern.Run();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            DesignPattern_Prototype pattern = new DesignPattern_Prototype();
            pattern.Run();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            DesignPattern_Bridge pattern = new DesignPattern_Bridge();
            pattern.Run();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            DesignPattern_Facade pattern = new DesignPattern_Facade();
            pattern.Run();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            DesignPattern_Proxy pattern = new DesignPattern_Proxy();
            pattern.Run();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            DesignPattern_ChainOfResponsibility pattern = new DesignPattern_ChainOfResponsibility();
            pattern.Run();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            DesignPattern_Command pattern = new DesignPattern_Command();
            pattern.Run();

            pattern.Run2();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            DesignPattern_Interpreter_v2 pattern = new DesignPattern_Interpreter_v2();
            pattern.Run();

            DesignPattern_Interpreter_v3 pattern_v3 = new DesignPattern_Interpreter_v3();
            pattern_v3.Run();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            DesignPattern_Iterator pattern = new DesignPattern_Iterator();
            pattern.Run();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            DesignPattern_Mediator pattern = new DesignPattern_Mediator();
            pattern.Run();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            DesignPattern_Memento pattern = new DesignPattern_Memento();
            pattern.Run();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            DesignPattern_Observer pattern = new DesignPattern_Observer();
            pattern.Run();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            DesignPattern_State pattern = new DesignPattern_State();
            pattern.Run();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            DesignPattern_Template pattern = new DesignPattern_Template();
            pattern.Run();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            DesignPattern_Visitor pattern = new DesignPattern_Visitor();
            pattern.Run();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            DesignPattern_Adapter pattern = new DesignPattern_Adapter();
            pattern.Run();
        }

        private void button33_Click(object sender, EventArgs e)
        {
            DesignPattern_Factory pattern = new DesignPattern_Factory();
            pattern.Run();
        }

        private void button34_Click(object sender, EventArgs e)
        {
            DataStructure_Tries tries = new DataStructure_Tries();

            tries.Run();
        }

        private void button35_Click(object sender, EventArgs e)
        {
            DataStructure_Tries tries = new DataStructure_Tries();

            tries.Run_Wordlist();

        }

        private void button36_Click(object sender, EventArgs e)
        {
            DataStructure_Tries tries = new DataStructure_Tries();

            tries.Run_Suffix();
        }

        private void button37_Click(object sender, EventArgs e)
        {
            RedBlackTree<int, int> myTree = new RedBlackTree<int, int>();

            try
            {

                myTree.Add(1, 1);
                myTree.Add(2, 1);
                myTree.Add(3, 1);
                myTree.Add(4, 1);
                myTree.Add(5, 1);
                myTree.Add(6, 1);
                myTree.Add(7, 1);
                myTree.Add(8, 1);
                myTree.Add(9, 1);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button38_Click(object sender, EventArgs e)
        {
            //SelfBalancedTree.AVLTree<int> myTree = new AVLTree<int>();

            //try
            //{

            //    myTree.Add(1);
            //    myTree.Add(2);
            //    myTree.Add(3);
            //    myTree.Add(4);
            //    myTree.Add(5);
            //    myTree.Add(6);
            //    myTree.Add(7);
            //    myTree.Add(8);
            //    myTree.Add(9);


            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            DataStructure_AVLTree avl = new DataStructure_AVLTree();
            avl.Run();

        }

        private void button39_Click(object sender, EventArgs e)
        {
            SplayTree.SplayTree<int, int> myTree = new SplayTree.SplayTree<int, int>();

            try
            {

                myTree.Add(1, 1);
                myTree.Add(2, 1);
                myTree.Add(3, 1);
                myTree.Add(4, 1);
                myTree.Add(5, 1);
                myTree.Add(6, 1);
                myTree.Add(7, 1);
                myTree.Add(8, 1);
                myTree.Add(9, 1);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button40_Click(object sender, EventArgs e)
        {
            Problems_FindNumberOfRotations problem = new Problems_FindNumberOfRotations();
            problem.Run();
        }

        private void button41_Click(object sender, EventArgs e)
        {
            DataStructure_Hash hash = new DataStructure_Hash();
            hash.Run();
        }

        private void button42_Click(object sender, EventArgs e)
        {
            Concept_DynamicPrograming_Memoization memo = new Concept_DynamicPrograming_Memoization();
            memo.Run();
        }

        private void button43_Click(object sender, EventArgs e)
        {
            DataStructure_QuickSort sort = new DataStructure_QuickSort();

            sort.Run();
        }

        private void button44_Click(object sender, EventArgs e)
        {

        }
    }
}