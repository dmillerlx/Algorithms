using System;
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

        private void button45_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_LinkedList_RemoveDuplicates test = new Solved_Problems.Problems_LinkedList_RemoveDuplicates();
            test.RemoveDuplicates();
        }

        private void button46_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_LinkedList_FindNthLastItem test = new Solved_Problems.Problems_LinkedList_FindNthLastItem();
            test.Run();
        }

        private void button47_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_LinkedList_AddNumbersInList test = new Solved_Problems.Problems_LinkedList_AddNumbersInList();
            test.Run();
        }

        private void button48_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_LinkedList_ReverseSinglyLinkedList test = new Solved_Problems.Problems_LinkedList_ReverseSinglyLinkedList();
            test.Run();
        }

        private void button49_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_LinkedList_FindLoop test = new Solved_Problems.Problems_LinkedList_FindLoop();

            test.Run();
        }

        private void button50_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_Stacks_3Stacks_SingleArray test = new Solved_Problems.Problems_Stacks_3Stacks_SingleArray();
            test.Run();
        }

        private void button51_Click(object sender, EventArgs e)
        {
            Problems_Amazon_PrintTree test = new Problems_Amazon_PrintTree();
            test.main();
        }

        private void button52_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_Stacks_PushPopMin test = new Solved_Problems.Problems_Stacks_PushPopMin();
            test.Run();
        }

        private void button53_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_Stacks_SetOfStacks test = new Solved_Problems.Problems_Stacks_SetOfStacks();
            test.Run();
        }

    
        private void button55_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_DynamicProgramming_TextWrap test = new Solved_Problems.Problems_DynamicProgramming_TextWrap();
            test.Run();
            
        }

        private void button56_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_Stacks_TowersOfHanoi test = new Solved_Problems.Problems_Stacks_TowersOfHanoi();
            test.Run();
        }

        private void button57_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_Stacks_ImplementQueueUsingStacks test = new Solved_Problems.Problems_Stacks_ImplementQueueUsingStacks();
            test.Run();
        }

        private void button58_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_Stacks_Sort test = new Solved_Problems.Problems_Stacks_Sort();
            test.Run();
        }

        private void button59_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_Stacks_AnimalShelter test = new Solved_Problems.Problems_Stacks_AnimalShelter();

            test.Run2();
        }

        private void button60_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_Trees_DetectBalanced test = new Solved_Problems.Problems_Trees_DetectBalanced();
            test.Run();
        }

        private void button61_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_Trees_FindIfRouteExists test = new Solved_Problems.Problems_Trees_FindIfRouteExists();

            test.Run();
        }

        private void button62_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_Trees_SortedArrayToBST test = new Solved_Problems.Problems_Trees_SortedArrayToBST();
            test.Run();
        }

        private void button63_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_Trees_CheckIfTreeIsBST test = new Solved_Problems.Problems_Trees_CheckIfTreeIsBST();
            test.Run();
        }

        private void button64_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_Trees_FindNextNodeSuccessor test = new Solved_Problems.Problems_Trees_FindNextNodeSuccessor();
            test.Run();
        }

        private void button65_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_Trees_FindCommonAncestor test = new Solved_Problems.Problems_Trees_FindCommonAncestor();
            test.Run();
        }

        private void button66_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_Trees_IsSubTree test = new Solved_Problems.Problems_Trees_IsSubTree();

            test.Run();
        }

        private void button67_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_Trees_SumNodesToValue test = new Solved_Problems.Problems_Trees_SumNodesToValue();
            test.Run();
        }

        private void button55_Click_1(object sender, EventArgs e)
        {
            Solved_Problems.Problems_DynamicProgramming_TextWrap test = new Solved_Problems.Problems_DynamicProgramming_TextWrap();
            test.Run();
        }

        private void button54_Click_1(object sender, EventArgs e)
        {
            Solved_Problems.Problems_DynamicProgramming_LongestSubSequence test = new Solved_Problems.Problems_DynamicProgramming_LongestSubSequence();
            //test.Run();

            test.Run2();
        }

        private void button68_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_Amazon_StringConcatCost test = new Solved_Problems.Problems_Amazon_StringConcatCost();
            test.Run();
        }

        private void button69_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_Amazon_IntegerMaxNumberFromDigits test = new Solved_Problems.Problems_Amazon_IntegerMaxNumberFromDigits();
            test.Run();
        }

        private void button70_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_Amazon_ParityBits test = new Solved_Problems.Problems_Amazon_ParityBits();
            test.Run();
        }

        private void button71_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_Amazon_SumDistinctIntegers test = new Solved_Problems.Problems_Amazon_SumDistinctIntegers();
            test.Run();
        }

        private void button72_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_LinkedIn_DeepIterator test = new Solved_Problems.Problems_LinkedIn_DeepIterator();
            test.Run();
        }

        private void button73_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_LinkedIn_IsomorphicStrings test = new Solved_Problems.Problems_LinkedIn_IsomorphicStrings();
            test.Run();
        }

        private void button74_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_LinkedIn_StringValidNumber test = new Solved_Problems.Problems_LinkedIn_StringValidNumber();
            test.Run();
        }

        private void button75_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_LinkedIn_FindMaxContigousSum test = new Solved_Problems.Problems_LinkedIn_FindMaxContigousSum();
            test.Run();
        }

        private void button76_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_DynamicProgramming_EditDistance test = new Solved_Problems.Problems_DynamicProgramming_EditDistance();
            test.Run();
        }

        private void button77_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_DynamicProgramming_Knapsack test = new Solved_Problems.Problems_DynamicProgramming_Knapsack();
            test.Run();
        }

        private void button78_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_LinkedIn_Bipartie_Graph test = new Solved_Problems.Problems_LinkedIn_Bipartie_Graph();
            test.Run();
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        bool init = false;
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!init) return;
            Application.UserAppDataRegistry.SetValue("ActivePage", tabControl1.SelectedIndex);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int val = (int)Application.UserAppDataRegistry.GetValue("ActivePage", 0);
            tabControl1.SelectedIndex = val;
            init = true;
        }

        private void button79_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_LinkedIn_MedianForStreamOfIntegers test = new Solved_Problems.Problems_LinkedIn_MedianForStreamOfIntegers();
            test.Run();
        }

        private void button80_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_LinkedIn_LockFreeStack test = new Solved_Problems.Problems_LinkedIn_LockFreeStack();
            test.Run();
        }

        private void button81_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_LinkedIn_TextJustification test = new Solved_Problems.Problems_LinkedIn_TextJustification();
            test.Run();
        }

        private void button82_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_LinkedIn_LockFreeQueue test = new Solved_Problems.Problems_LinkedIn_LockFreeQueue();
            test.Run();
        }

        private void button83_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_LinkedIn_POW_No_BuiltInFunctions test = new Solved_Problems.Problems_LinkedIn_POW_No_BuiltInFunctions();
            test.Run();
        }

        private void button84_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_LinkedIn_Intervals test = new Solved_Problems.Problems_LinkedIn_Intervals();
            test.Run();
        }

        private void button85_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_LinkedIn_FindMinimumSubString test = new Solved_Problems.Problems_LinkedIn_FindMinimumSubString();
            test.Run();
        }

        private void button86_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_DynamicProgramming_LongestCommonSubSequence test = new Solved_Problems.Problems_DynamicProgramming_LongestCommonSubSequence();
            test.Run();
        }

        private void button87_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_LinkedList_IsPalendrome test = new Solved_Problems.Problems_LinkedList_IsPalendrome();
            test.Run();
        }

        private void button88_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_String_ReverseWords test = new Solved_Problems.Problems_String_ReverseWords();
            test.Run();
        }

        private void button89_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_Microsoft_ReverseLettersInWords test = new Solved_Problems.Problems_Microsoft_ReverseLettersInWords();
            test.Run();
        }

        private void button90_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_Moderate_SwapTwoNumbersWithoutTempData test = new Solved_Problems.Problems_Moderate_SwapTwoNumbersWithoutTempData();
            test.Run();
        }

        private void button91_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_Microsoft_Merge_and_Sort_M_N_Sorted_Array test = new Solved_Problems.Problems_Microsoft_Merge_and_Sort_M_N_Sorted_Array();
            test.Run();
        }

        private void button92_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_Microsoft_Tree_Find_Precedessor_and_Successor test = new Solved_Problems.Problems_Microsoft_Tree_Find_Precedessor_and_Successor();
            test.Run();
        }

        private void button93_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_Microsoft_Palindrome test = new Solved_Problems.Problems_Microsoft_Palindrome();
            test.Run();
        }

        private void button94_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_LinkedIn_FindNumberInRotatedSortedArray test = new Solved_Problems.Problems_LinkedIn_FindNumberInRotatedSortedArray();
            test.Run();
        }

        private void button95_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_LinkedIn_FindNumberInSortedMatrix test = new Solved_Problems.Problems_LinkedIn_FindNumberInSortedMatrix();
            test.Run();
        }

        private void button96_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_LinkedIn_ServerFindMissingValue test = new Solved_Problems.Problems_LinkedIn_ServerFindMissingValue();
            test.Run();
        }

        void ColorControls(System.Windows.Forms.Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                if (c.Controls != null && c.Controls.Count > 0)
                {
                    ColorControls(c.Controls);
                }

                if (textBox1.Text.Trim().Length > 0 && c.Text.ToUpper().Contains(textBox1.Text.ToUpper()))
                {
                    c.BackColor = Color.Orange;
                }
                else c.BackColor = Color.Transparent;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            foreach (Control C in this.Controls)
            {
                if (C.Controls != null && C.Controls.Count > 0)
                {
                    ColorControls(C.Controls);
                    
                }

            }

            //foreach (Control c in groupBox6.Controls)
            //{
            //    if (c.Text.ToUpper().Contains(textBox1.Text.ToUpper()))
            //    {
            //        c.BackColor = Color.Orange;
            //    }
            //    else c.BackColor = Color.Transparent;
            //}
        }

        private void button97_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_DynamicProgramming_LongestPaldromeSubSequence test = new Solved_Problems.Problems_DynamicProgramming_LongestPaldromeSubSequence();
            test.Run();
        }

        private void button98_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_DynamicProgramming_Staircase test = new Solved_Problems.Problems_DynamicProgramming_Staircase();
            test.Run();
        }

        private void button99_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_TopCoder_Set657 test = new Solved_Problems.Problems_TopCoder_Set657();
            test.Run();
        }

        private void button100_Click(object sender, EventArgs e)
        {
            Solved_Problems.Problems_TopCoder_CCipher test = new Solved_Problems.Problems_TopCoder_CCipher();
            test.Run();
        }
    }
}
