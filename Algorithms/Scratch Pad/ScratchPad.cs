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

        public static int [] QuickSort(ref int []vals)
        {
            if (vals.Length <= 1)
                return vals;

            int pivot = vals.Length / 2;
            int left = 0;
            int right = vals.Length - 1;

            QuickSortHelper(ref vals, left, right, (right- left)/2);

            return vals;

        }

        private static void QuickSortHelper(ref int[] vals, int left, int right, int pivot)
        {
            int tmp = 0;
            if (left >= right)
                return;

            //Get the pivot value
            int pivotVal = vals[pivot];

            //Example:
            //Consider 
            //  right segment: 9, 15, 7, 1
            //  pivotVal of 9 
            //  pivot = 0
            //  right = 3
            //  
            //  Everything < 9 has to be to left of 9 and > 9 has to be to the right of 9
            //      
            //      9, 15, 7, 1
            //      ^         ^
            //1.    1 < 9 => 
            //          Set pivot index to 1
            //          New pivot index is now 1
            //              Move value (15) at new index (1) to the end, where 1 used to be
            //      
            //      9, 15, 7, 1 ==> 1, 9, 7, 15
            //      ^         ^        ^     ^
            //      p         right    p     right
            //
            //      So 1 replaced 9, 15 replace 1, and 9 replace 15

            //2.    15 > 9  => no change, decrement right
            //      1, 9, 7, 15 ==> 1, 9, 7, 15
            //         ^     ^         ^  ^
            //         p     right     p  right
            //      
            //3.    9 > 7 ==>
            //      Set value at pivot index to 7
            //          Increment pivot index to 2
            //          Move value at new index (2) to end, where 7 used to be
            //              This is basically setting 7 = 7
            //          Finally set pivot value
            //
            //      1, 9, 7, 15 ==> 1, 7, 9, 15
            //         ^  ^               ^
            //         p  right           p 
            //                            right
            //
            //      So 7 replaced 9, 7 replace 7, and 9 replace 7
            //
            //4.    Exit since right <= p

            //Make every item to the left of pivotVal smaller than pivotVal
            for (int x = left; x < pivot; )
            {
                if (vals[x] > pivotVal)
                {
                    vals[pivot] = vals[x];
                    vals[x] = vals[pivot - 1];
                    vals[pivot - 1] = pivotVal; 
                    pivot--;
                }
                else
                {
                    x++;
                }
            }

            //Make every item to the right of pivotVal greater than pivotVal
            for (int x= right; x > pivot;)
            {
                if (vals[x] < pivotVal)
                {
                    vals[pivot] = vals[x];
                    vals[x] = vals[pivot + 1];
                    vals[pivot + 1] = pivotVal;
                    pivot++;
                }
                else
                {
                    x--;
                }
            }

            ////////
            //Pivot position does not matter

            //Can use the midpoint of the segment as is done here:
            //QuickSortHelper(ref vals, left, pivot, left + (pivot-1-left)/2);
            //QuickSortHelper(ref vals, pivot + 1, right, pivot + (right-pivot+1) / 2);
            
            //Can use the left most position as is done here
            QuickSortHelper(ref vals, left, pivot, left);
            QuickSortHelper(ref vals, pivot + 1, right, pivot+1);
        }

        public static int [] MergeSort(int []vals)
        {
            return MergeSortHelper(vals);
        }

        public static int []Sub(int []vals, int start, int end)
        {
            int[] ret = new int[end - start + 1];
            for (int x= start; x <= end; x++)
            {
                ret[x - start] = vals[x];
            }
            return ret;
        }

        public static int[]  Merge(int []one, int []two)
        {
            int len = one.Length + two.Length;
            int[] ret = new int[len];

            int oneIndex = 0;
            int twoIndex = 0;
            bool chooseOne = false;
            for (int x = 0; x < len; x++)
            {
                if (oneIndex >= one.Length)
                {
                    chooseOne = false;
                }
                else if (twoIndex >= two.Length)
                {
                    chooseOne = true;
                }
                else if (one[oneIndex] < two[twoIndex])
                {
                    chooseOne = true;
                }
                else
                {
                    chooseOne = false;
                }

                if (chooseOne)
                {
                    ret[x] = one[oneIndex++];
                }
                else
                {
                    ret[x] = two[twoIndex++];
                }
                
            }//end for

            return ret;
        }

        public static int[]  MergeSortHelper(int []vals)
        {
            //While length > 2, break into smaller pieces
            if (vals.Length > 2)
            {
                int mid = vals.Length / 2;
                //Make left and right segments
                int[] left = Sub(vals, 0, mid);
                int[] right = Sub(vals, mid + 1, vals.Length-1);
                //Merge the segments after they are broken into smaller pieces
                return Merge(MergeSortHelper(left), MergeSortHelper(right));
            }

            //Length of 2, so sort the two items
            if (vals.Length == 2)
            {                
                if (vals[0] > vals[1])
                {
                    int tmp = vals[0];
                    vals[0] = vals[1];
                    vals[1] = tmp;
                }
            }

            return vals;
        }


        public class TreeNode: IComparable
        {
            public TreeNode (int value, int x, int y)
            {
                Value = value;
                X = x;
                Y = y;
            }
            public int Value { get; set; }
            public int X { get; set; }
            public int Y { get; set; }

            public int CompareTo(object obj)
            {
                TreeNode other = (TreeNode)obj;
                if (other.X != X)
                    return X.CompareTo(other.X);

                return Y.CompareTo(other.Y);
            }
        }
        public static int [] PrintBinaryTreeTopToBottom(Node root)
        {
            if (root == null)
                return null;

            heap = new Heap<TreeNode>(Heap<TreeNode>.heapTypeEnum.min);

            PrintBrinaryTreeTopToBottomHelper(root, 0, 0);

            int[] ret = new int[heap.size];

            int index = 0;
            while (heap.size > 0)
            {
                TreeNode node = heap.Dequeue();
                ret[index++] = node.Value;
            }

            return ret;

        }

        public static Heap<TreeNode> heap = new Heap<TreeNode>(Heap<TreeNode>.heapTypeEnum.min);

        public static void PrintBrinaryTreeTopToBottomHelper(Node root, int x, int y)
        {
            if (root == null)
                return;
            heap.Enqueue(new TreeNode((int)root.Data, x, y));
            PrintBrinaryTreeTopToBottomHelper(root.left, x - 1, y + 1);
            PrintBrinaryTreeTopToBottomHelper(root.right, x + 1, y + 1);
        }

        //https://www.careercup.com/question?id=5653583535013888
        public static void FindBiggestPlusSign(int [,]matrix, out int solX, out int solY, out int solSize)
        {
            Memoized = new Dictionary<string, int>();

            solX = -1;
            solY = -1;
            solSize = 0;

            int width = matrix.GetLength(0);
            int height = matrix.GetLength(1);

            for (int x=1; x < width-1; x++)
            {
                for (int y=1; y < height - 1; y++)
                {
                    if (matrix[x, y] == 1)
                    {
                        int size = FindBiggestPlusSignHelper(matrix, x, y);
                        if (size > solSize)
                        {
                            solX = x;
                            solY = y;
                            solSize = size;
                        }
                    }
                }
            }
        }
        
        public static string MakeKey(int x, int y, Direction direction)
        {
            int directionNum = 0;
            switch (direction)
            {
                case Direction.up: directionNum = 0; break;
                case Direction.down: directionNum = 1; break;
                case Direction.left: directionNum = 2; break;
                case Direction.right: directionNum = 3; break;
            }

            return x + "_" + y + "_" + directionNum;
        }

        public static Dictionary<string, int> Memoized;

        public enum Direction { up, down, left, right };
        public static int FindBiggestPlusSignHelperMemorized(int [,]matrix, int x, int y, Direction direction)
        {
            if (x < 0 || x >= matrix.GetLength(0) || y < 0 || y >= matrix.GetLength(1))
            {
                return 0;
            }
            
            if (matrix[x, y] == 0)
                return 0;

            if (Memoized.ContainsKey(MakeKey(x, y, direction)))
                return Memoized[MakeKey(x, y, direction)];

            int newX = x;
            int newY = y;
            switch (direction)
            {
                case Direction.up: newY = y - 1; break;
                case Direction.down: newY = y + 1; break;
                case Direction.left: newX = x - 1; break;
                case Direction.right: newX = x + 1; break;
            }

            Memoized.Add(MakeKey(x, y, direction), FindBiggestPlusSignHelperMemorized(matrix, newX, newY, direction) + 1);

            return Memoized[MakeKey(x, y, direction)];
        }

        public static int FindBiggestPlusSignHelper(int[,] matrix, int testX, int testY)
        {
            int up = FindBiggestPlusSignHelperMemorized(matrix, testX, testY, Direction.up);
            int down = FindBiggestPlusSignHelperMemorized(matrix, testX, testY, Direction.down);
            int left = FindBiggestPlusSignHelperMemorized(matrix, testX, testY, Direction.left);
            int right = FindBiggestPlusSignHelperMemorized(matrix, testX, testY, Direction.right);

            return Math.Min(Math.Min(up, down), Math.Min(left, right));
            
        }

        public static int FindBiggestPlusSignHelper2(int [,]matrix, int testX, int testY)
        {
            int size = 0;
            int width = matrix.GetLength(0);
            int height = matrix.GetLength(1);

            int increment = 0;
            while (true)
            {
                if (testX + increment < width &&
                    testX - increment >= 0 &&
                    testY + increment < height &&
                    testY - increment >= 0 &&
                    matrix[testX + increment, testY] == 1 &&
                    matrix[testX - increment, testY] == 1 &&
                    matrix[testX, testY - increment] == 1 &&
                    matrix[testX, testY - increment] == 1
                    )
                {
                    size++;
                    increment++;
                }
                else
                {
                    return size;
                }
            }
        }


        public class FNode
        {
            public FNode(int value, FNode next, FNode down)
            {
                Value = value;
                Next = next;
                Child = down;
            }
            public int Value { get; set; }
            public FNode Next { get; set; }
            public FNode Child { get; set; }
        }

        public static FNode FlattenList(FNode root)
        {
            
            Queue<FNode> queue = new Queue<FNode>();

            FNode current = root;

            if (current == null)
                return root;

            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                FNode tmp = queue.Dequeue();
                if (current != null && current != root)
                    current.Next = tmp;

                do
                {
                    if (current.Child != null)
                    {
                        queue.Enqueue(current.Child);
                        current.Child = null;
                    }
                    if (current.Next != null)
                        current = current.Next;
                } while (current.Next != null);
            }


            return root;

        }

    }
}
