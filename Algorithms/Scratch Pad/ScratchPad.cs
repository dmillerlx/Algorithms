using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Scratch_Pad
{
    public class ScratchPad
    {
        //////////////////////////////////////////////////////
        //
        //  Arrays and Strings


        //Assumptions
        //only lower case characters a-z
        public static bool AllUniqueChars(char[] arr)
        {
            if (arr.Length <= 1)
                return true;

            if (arr.Length > 26)
                return false;

            int[] charCount = new int[26];

            for (int index = 0; index < arr.Length; index++)
            {
                //Get character
                char c = arr[index];
                //Convert character to integer with offset starting with 'a'
                int c_value = c - 'a';

                //Return false if we have already processed this character
                if (charCount[c_value] > 0)
                    return false;

                //Increment to indicate we have processed this character
                charCount[c_value]++;
            }

            return true;
        }

        public static char[] Reverse(char[] arr)
        {
            if (arr.Length <= 1)
                return arr;

            int s = 0;
            int e = arr.Length - 1;

            char tmp;
            while (s < e)
            {
                tmp = arr[s];
                arr[s] = arr[e];
                arr[e] = tmp;
                s++;
                e--;
            }
            //abcde
            //ebcda
            //edcba

            //abcdef
            //fbcdea
            //fecdba
            //fedcba

            return arr;


        }

        //Assumptions
        //  character set is lower case letters a-z
        public static bool IsPermutation(char[] arr1, char[] arr2)
        {
            if (arr1.Length != arr2.Length)
                return false;

            int[] letterCount = new int[26];

            for (int index = 0; index < arr1.Length; index++)
            {
                char c = arr1[index];
                int offset = c - 'a';
                letterCount[offset]++;
            }

            for (int index = 0; index < arr2.Length; index++)
            {
                char c = arr2[index];
                int offset = c - 'a';
                if (letterCount[offset] <= 0)
                    return false;
                letterCount[offset]--;
            }

            return true;
        }


        //Assumptions
        //  character set is upper and lower case letters a-z
        public static string BasicStringCompression(string input)
        {
            char lastChar = (char)0;
            char currentChar = input[0];
            int charCount = 0;
            StringBuilder ret = new StringBuilder();
            for (int index = 0; index < input.Length; index++)
            {
                //Get character
                char c = input[index];

                //Increment count if character has not changed
                if (c == currentChar)
                {
                    charCount++;
                }
                else
                {
                    //character changed, so append to output
                    ret.Append(currentChar + charCount.ToString());

                    //Set currentChar to new value and charCount to 1
                    currentChar = c;
                    charCount = 1;
                }
            }

            ret.Append(currentChar + charCount.ToString());

            if (ret.Length > input.Length)
                return input;

            return ret.ToString();

        }
                
        class point
        {
            public point(int x, int y)
            {
                X = x;
                Y = y;
            }
            public int X { get; set; }
            public int Y { get; set; }

        }

        public static int [,]Matrix_MakeRowColumnZero(int [,]matrix, int m, int n)
        {
            //find current 0's

            List<int> rows = new List<int>();
            List<int> cols = new List<int>();

            List<point> points = new List<point>();


            for (int x = 0; x < m; x++)
                rows.Add(x);

            for (int y = 0; y < n; y++)
                cols.Add(y);

            
            for (int x=0; x < rows.Count;)
            {
                bool found = false;
                for (int y=0; y < cols.Count && !found;)
                {
                    if (matrix[rows[x],cols[y]] == 0)
                    {
                        found = true;
                        point p = new point(rows[x], cols[y]);
                        points.Add(p);
                        cols.RemoveAt(y);
                    }
                    else
                    {
                        y++;
                    }
                }

                if (found)
                {
                    rows.RemoveAt(x);
                }
                else
                {
                    x++;
                }
            }

            

            //Set Row/Column to zero
            foreach (point p in points)
            {
                for (int x=0; x < m; x++)
                {
                    matrix[x,p.Y] = 0;
                }

                for (int y=0; y < n; y++)
                {
                    matrix[p.X,y] = 0;
                }
            }

            return matrix;
        }


        //////////////////////////////////////////////////////
        //
        //  Linked Lists

        //Problem 2.2
        public static Data_Structures.DataStructure_LinkedList<int>.Node FindKthLastElement(Data_Structures.DataStructure_LinkedList<int> list, int k)
        {
            if (list == null)
                return null;

            Data_Structures.DataStructure_LinkedList<int>.Node node = list.head;
            Data_Structures.DataStructure_LinkedList<int>.Node tail = null;

            int count = 0;
            while (node != null)
            {
                if (count >= k)
                {
                    if (tail == null)
                        tail = list.head;
                    else tail = tail.Next;
                }
                node = node.Next;
                count++;
            }

            return tail;
        }

        //Problem 2.5
        //
        //Add data in lists stored in reverse order:
        //Input (7 -> 1 -> 6) + (5 -> 9 -> 2) = 617 + 295
        //Output (2 -> 1 -> 9) = 912
        public static Data_Structures.DataStructure_LinkedList<int> AddLinkedLists(Data_Structures.DataStructure_LinkedList<int> one, Data_Structures.DataStructure_LinkedList<int> two)
        {
            if (one == null)
                return two;

            if (two == null)
                return one;

            Data_Structures.DataStructure_LinkedList<int> output = new Data_Structures.DataStructure_LinkedList<int>();

            Data_Structures.DataStructure_LinkedList<int>.Node oneNode = one.head;
            Data_Structures.DataStructure_LinkedList<int>.Node twoNode = two.head;

            int carryOver = 0;
            while (oneNode != null || twoNode != null)
            {
                int value = -1;
                if (oneNode != null)
                {
                    value = oneNode.Data;
                }
                if (twoNode != null)
                {
                    if (value < 0)
                        value = twoNode.Data;
                    else value += twoNode.Data;
                }

                value += carryOver;
                carryOver = 0;
                while (value >= 10)
                {
                    value = value - 10;
                    carryOver++;
                }

                output.Add(value);
                if (oneNode != null)
                    oneNode = oneNode.Next;

                if (twoNode != null)
                    twoNode = twoNode.Next;
            }

            if (carryOver >0)
            {
                output.Add(carryOver);
            }

            return output;

        }


        //Problem 2.7
        //
        // 1,2,3,4,4,3,2,1
        public static bool IsLinkedListPalindrome(Data_Structures.DataStructure_LinkedList<int> list)
        {
            head = list.head;
            return isPalindromeHelper(list.head);
        }

        static Data_Structures.DataStructure_LinkedList<int>.Node head = null;

        public static bool isPalindromeHelper(Data_Structures.DataStructure_LinkedList<int>.Node current)
        {
            if (current == null)
                return true;

            if (!isPalindromeHelper(current.Next))
                return false;

            bool ret = head.Data == current.Data;

            if (!ret)
                return false;

            head = head.Next;

            return true;
        }



        // Problem 3.1 - Implement stack with a single array
        public class SingleArrayStack
        {
            int[] arr;
            int head;
            public SingleArrayStack()
            {
                arr = new int[10];
                head = -1;
            }

            public void Push(int value)
            {
                if (head > arr.Length) throw new Exception("Full");
                arr[++head] = value;
            }

            public int Pop()
            {
                if (IsEmpty) throw new Exception("Empty");

                return arr[head--];
            }

            public int Peek()
            {
                if (IsEmpty) throw new Exception("Empty");
                return arr[head];
            }

            public bool IsEmpty { get { return head < 0; } }

        }

        // Problem 3.2 - Stack that also has MIN function
        public class StackWithMin
        {
            public class StackItem
            {
                public int Value { get; set; }
                public StackItem NextMin { get; set; }
            }

            StackItem []stackItems;
            int head;
            StackItem minHead = null;

            public StackWithMin()
            {
                stackItems = new StackItem[10];
                head = -1;
            }

            public void Push(int value)
            {
                if (head > stackItems.Length) throw new Exception("Full");
                stackItems[++head] = new StackItem();
                stackItems[head].Value = value;
                if (minHead == null)
                {
                    minHead = stackItems[head];
                    minHead.NextMin = null;
                    return;
                }

                if (stackItems[head].Value < minHead.Value)
                {
                    stackItems[head].NextMin = minHead;
                    minHead = stackItems[head];
                    return;
                }

                StackItem prev = null;
                StackItem current = minHead;

                while (current != null && current.Value < value)
                {
                    prev = current;
                    current = current.NextMin;
                }

                if (prev == null)
                {
                    minHead = current;
                    minHead.NextMin = current;
                }
                else
                {
                    StackItem tmp = prev.NextMin;
                    prev.NextMin = stackItems[head];
                    stackItems[head].NextMin = tmp;                    
                }

                //StackItem node = minHead;
                //while (node != null && node.NextMin != null && node.NextMin.Value < value)
                //    node = node.NextMin;

                //if (node.NextMin == null)
                //    node.NextMin = stackItems[head];
                //else
                //{
                //    StackItem tmp = node.NextMin;
                //    node.NextMin = stackItems[head];
                //    stackItems[head].NextMin = tmp;
                //}
                //set next min
            }

            public int Pop()
            {
                if (IsEmpty) throw new Exception("Empty");

                //find previous item
                StackItem node = minHead;
                if (minHead == stackItems[head])
                {
                    minHead = minHead.NextMin;
                }
                else
                {
                    StackItem prev = null;
                    StackItem current = minHead;

                    while (current != stackItems[head])
                    {
                        prev = current;
                        current = current.NextMin;
                    }

                    if (prev == null)
                    {
                        minHead = current.NextMin;
                    }
                    else
                    {
                        prev.NextMin = current.NextMin;
                    }

                    //while (node != null && node.NextMin != stackItems[head])
                    //    node = node.NextMin;

                    //if (node != null && node.NextMin != null)
                    //    node.NextMin = node.NextMin.NextMin;
                }

                

                return stackItems[head--].Value;
            }

            public int Min()
            {
                if (minHead == null)
                    throw new Exception("Empty");

                return minHead.Value;
            }

            public int Peek()
            {
                if (IsEmpty) throw new Exception("Empty");
                return stackItems[head].Value;
            }

            public bool IsEmpty { get { return head < 0; } }

        }

        public class StackWithMin2
        {
            public class StackItem
            {
                public int Value { get; set; }
                public int LocalMin { get; set; }
            }

            StackItem[] stackItems;
            int head;

            public StackWithMin2()
            {
                stackItems = new StackItem[10];
                head = -1;
            }

            public void Push(int value)
            {
                if (head > stackItems.Length) throw new Exception("Full");

                int newMin = value;
                if (!IsEmpty)
                    newMin = Math.Min(value, stackItems[head].LocalMin);

                stackItems[++head] = new StackItem();
                stackItems[head].Value = value;
                stackItems[head].LocalMin = newMin;

            }

            public int Pop()
            {
                if (IsEmpty) throw new Exception("Empty");
                
                return stackItems[head--].Value;
            }

            public int Min()
            {
                if (IsEmpty) throw new Exception("Empty");

                return stackItems[head].LocalMin;
            }

            public int Peek()
            {
                if (IsEmpty) throw new Exception("Empty");
                return stackItems[head].Value;
            }

            public bool IsEmpty { get { return head < 0; } }

        }


        public static int TreeDepth(Node root)
        {
            if (root == null)
                return 0;

            return 1 + TreeDepth(root.left) + TreeDepth(root.right);
        }

        //Problem 4.1 - find if a binary tree is balanced
        public static bool IsBinaryTreeBalanced(Node root)
        {
            if (root == null)
                return true;

            return isBalanced(root);
        }

        public static bool isBalanced(Node root)
        {
            if (root == null)
                return true;

            int leftDepth = TreeDepth(root.left);
            int rightDepth = TreeDepth(root.right);

            if (Math.Abs(leftDepth - rightDepth) > 1)
                return false;

            return isBalanced(root.left) && isBalanced(root.right);
        }

    }
}
