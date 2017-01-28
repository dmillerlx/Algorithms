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

        public static int[,] Matrix_MakeRowColumnZero(int[,] matrix, int m, int n)
        {
            //find current 0's

            List<int> rows = new List<int>();
            List<int> cols = new List<int>();

            List<point> points = new List<point>();


            for (int x = 0; x < m; x++)
                rows.Add(x);

            for (int y = 0; y < n; y++)
                cols.Add(y);


            for (int x = 0; x < rows.Count;)
            {
                bool found = false;
                for (int y = 0; y < cols.Count && !found;)
                {
                    if (matrix[rows[x], cols[y]] == 0)
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
                for (int x = 0; x < m; x++)
                {
                    matrix[x, p.Y] = 0;
                }

                for (int y = 0; y < n; y++)
                {
                    matrix[p.X, y] = 0;
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

            if (carryOver > 0)
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

            StackItem[] stackItems;
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

        public static int[] QuickSort(ref int[] vals)
        {
            if (vals.Length <= 1)
                return vals;

            int pivot = vals.Length / 2;
            int left = 0;
            int right = vals.Length - 1;

            QuickSortHelper(ref vals, left, right, (right - left) / 2);

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
            for (int x = left; x < pivot;)
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
            for (int x = right; x > pivot;)
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
            QuickSortHelper(ref vals, pivot + 1, right, pivot + 1);
        }

        public static int[] MergeSort(int[] vals)
        {
            return MergeSortHelper(vals);
        }

        public static int[] Sub(int[] vals, int start, int end)
        {
            int[] ret = new int[end - start + 1];
            for (int x = start; x <= end; x++)
            {
                ret[x - start] = vals[x];
            }
            return ret;
        }

        public static int[] Merge(int[] one, int[] two)
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

        public static int[] MergeSortHelper(int[] vals)
        {
            //While length > 2, break into smaller pieces
            if (vals.Length > 2)
            {
                int mid = vals.Length / 2;
                //Make left and right segments
                int[] left = Sub(vals, 0, mid);
                int[] right = Sub(vals, mid + 1, vals.Length - 1);
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


        public class TreeNode : IComparable
        {
            public TreeNode(int value, int x, int y)
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
        public static int[] PrintBinaryTreeTopToBottom(Node root)
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
        public static void FindBiggestPlusSign(int[,] matrix, out int solX, out int solY, out int solSize)
        {
            Memoized = new Dictionary<string, int>();

            solX = -1;
            solY = -1;
            solSize = 0;

            int width = matrix.GetLength(0);
            int height = matrix.GetLength(1);

            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
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
        public static int FindBiggestPlusSignHelperMemorized(int[,] matrix, int x, int y, Direction direction)
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

        public static int FindBiggestPlusSignHelper2(int[,] matrix, int testX, int testY)
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


        public static string MakeKey(int x, int y)
        {
            return x + "_" + y;
        }

        //Given an NxN matrix, and starting point x,y find the closest value of 'searchValue'
        public static bool FindClosestValue(int[,] matrix, int x, int y, int searchValue, out int searchX, out int searchY)
        {
            searchX = 0;
            searchY = 0;

            Queue<point> queue = new Queue<point>();

            queue.Enqueue(new point(x, y));
            Dictionary<string, bool> visitList = new Dictionary<string, bool>();
            while (queue.Count > 0)
            {
                point item = queue.Dequeue();

                if (visitList.ContainsKey(MakeKey(item.X, item.Y)))
                    continue;

                if (matrix[item.X, item.Y] == searchValue)
                {
                    //Found search item, return
                    searchX = item.X;
                    searchY = item.Y;
                    return true;
                }

                visitList.Add(MakeKey(item.X, item.Y), true);


                //Not found, enqueue neighbors that have not been searched

                if (item.X > 0 && !visitList.ContainsKey(MakeKey(item.X - 1, item.Y)))
                {
                    queue.Enqueue(new point(item.X - 1, item.Y));
                }
                if (item.Y > 0 && !visitList.ContainsKey(MakeKey(item.X, item.Y - 1)))
                {
                    queue.Enqueue(new point(item.X, item.Y - 1));
                }
                if (item.X < matrix.GetLength(0) - 1 && !visitList.ContainsKey(MakeKey(item.X + 1, item.Y)))
                {
                    queue.Enqueue(new point(item.X + 1, item.Y));
                }
                if (item.Y < matrix.GetLength(1) - 1 && !visitList.ContainsKey(MakeKey(item.X, item.Y + 1)))
                {
                    queue.Enqueue(new point(item.X, item.Y + 1));
                }
            }

            return false;

        }


        public static int SubArrayLargestSum(int[] arr)
        {
            int maxSum = int.MinValue;
            bool allNegative = true;
            int sum = arr[0];
            int maxNeg = int.MinValue;

            if (arr[0] < 0)
                maxNeg = arr[0];

            for (int x = 1; x < arr.Length; x++)
            {

                if (arr[x] < 0 && arr[x] > maxNeg)
                    maxNeg = arr[x];

                if (arr[x] >= 0)
                {
                    allNegative = false;
                }

                sum += arr[x];
                if (sum < 0)
                    sum = 0;

                if (sum > maxSum)
                {
                    maxSum = sum;
                }
            }

            if (allNegative)
                return maxNeg;

            return maxSum;
        }


        public static bool FindSmallestSortIndicies(int[] vals, out int start, out int end)
        {
            start = 0;
            end = vals.Length - 1;

            if (vals.Length == 0)
                return false;

            // 1,2,4,7,10,11,7,12,6,7,16,18,19

            int lowestStartValue = -1;
            for (int index = 0; index < vals.Length - 1; index++)
            {
                if (vals[index] > vals[index + 1])
                {
                    lowestStartValue = vals[index + 1];
                    break;
                }
            }

            if (lowestStartValue == -1)
                return false;

            while (vals[start] < lowestStartValue)
                start++;


            Heap<int> h = new Heap<int>(Heap<int>.heapTypeEnum.max);

            for (int index = start; index < vals.Length; index++)
            {
                h.Enqueue(vals[index]);
            }




            //int highestEndValue = -1;
            //for (int index = vals.Length-1; index > 0; index--)
            //{
            //    if (vals[index-1] > vals[index] || vals[index] <= lowestStartValue)
            //    {
            //        highestEndValue = vals[index];
            //        break;
            //    }
            //}

            //if (highestEndValue == -1)
            //    return false;

            while (vals[end] >= h.Peek())
            {
                end--;
                h.Dequeue();
            }

            return true;
        }


        public static string FindLongestWordMadeOfOtherWords(string[] words)
        {
            DataStructure_Tries.Tries tri = new DataStructure_Tries.Tries();

            foreach (string word in words)
            {
                tri.AddWord(word);
            }

            string longestWordMadeOfOtherWords = string.Empty;
            foreach (string word in words)
            {
                int count = tri.CountWords(word);

                if (word.Length > longestWordMadeOfOtherWords.Length && count > 1)
                {
                    longestWordMadeOfOtherWords = word;
                }
            }


            return longestWordMadeOfOtherWords;
        }

        static bool isSpecialChar(char[] specialChars, char val)
        {
            foreach (char c in specialChars)
            {
                if (c == val)
                    return true;
            }

            return false;
        }

        public static char[] ReverseStringExcludingSpecialChars(char[] input, char[] specialChars)
        {
            int start = 0;
            int end = input.Length - 1;

            if (end <= start)
                return input;

            while (start < end)
            {
                if (isSpecialChar(specialChars, input[start]) == false)
                {
                    //Found non-special char to reverse, so find end point to reverse it with

                    while (start < end && isSpecialChar(specialChars, input[end]))
                    {
                        //decrement end until we find a non-special char
                        end--;
                    }

                    //check to see if we crossed start and end
                    if (end <= start)
                        break;

                    char tmp = input[start];
                    input[start] = input[end];
                    input[end] = tmp;

                    //Increment start
                    start++;

                    //decrement end
                    end--;
                }
                else
                {
                    start++;
                }
            }

            return input;
        }

        public static bool isPalindrome(char[] input, int start, int end)
        {
            while (start <= end)
            {
                if (input[start] != input[end])
                    return false;
                start++;
                end--;
            }

            return true;
        }

        public static List<string> FindPalindromicPartitions(char[] input)
        {
            int maxWindow = input.Length;
            List<string> ret = new List<string>();
            //Create sliding window of increasing side from 1 to size or input
            for (int window = 1; window < maxWindow; window++)
            {
                //Start window at index 0 and increment until edge of window exceeds input.Length

                for (int start = 0; start + window < input.Length; start++)
                {
                    if (isPalindrome(input, start, start + window))
                    {
                        //Found a Palindrome so append
                        StringBuilder sb = new StringBuilder();
                        for (int x = start; x < start + window; x++)
                            sb.Append(input[x]);

                        ret.Add(sb.ToString());
                    }
                }

            }

            return ret;
        }

        public static int getCharOffset(char val)
        {
            return (int)val - (int)'a';
        }

        public static string FindLongestSubstringWithoutRepeatChars(char[] input)
        {
            int[] arr = new int[26];

            int window = input.Length;

            //start with window of size input.Lenght and decrease each time a solution is not found
            for (window = input.Length; window > 0; window--)
            {

                //Move window from left to rigth 
                for (int start = 0; start + window <= input.Length; start++)
                {
                    arr = new int[26];
                    //Iterate over each char in the window to see if it is a solution
                    bool failed = false;
                    for (int x = start; x < start + window; x++)
                    {
                        int val = getCharOffset(input[x]);
                        if (arr[val] > 0)
                        {
                            //failure
                            failed = true;
                            break;
                        }
                        else
                        {
                            arr[val]++;
                        }
                    }

                    if (!failed)
                    {
                        //soution found
                        StringBuilder sb = new StringBuilder();
                        for (int x = start; x < start + window; x++)
                        {
                            sb.Append(input[x]);
                        }
                        return sb.ToString();
                    }
                }

            }
            return string.Empty;

        }

        public static string FindLongestSubstringWithoutRepeatCharsV2(char[] input)
        {
            int[] arr = new int[26];

            int startBest = -1;
            int endBest = -1;
            int lenBest = -1;

            int currStart = -1;
            int currEnd = -1;
            int currLen = -1;
            for (int x = 0; x < input.Length; x++)
            {
                if (currStart < 0)
                {
                    currStart = x;
                    currEnd = x;
                    currLen = 1;
                }

                int val = getCharOffset(input[x]);
                if (arr[val] > 0)
                {
                    //duplicate found
                    currStart = x;
                    currEnd = x;
                    currLen = 1;
                    arr = new int[26];
                    continue;
                }

                currEnd = x;
                currLen = currEnd - currStart;
                arr[val]++;

                if (currLen > lenBest)
                {
                    startBest = currStart;
                    endBest = currEnd;
                    lenBest = currLen;
                }
            }

            StringBuilder sb = new StringBuilder();
            for (int x = startBest; x <= endBest; x++)
            {
                sb.Append(input[x]);
            }

            return sb.ToString();
        }


        public class LinkedListNode
        {
            public LinkedListNode(int data)
            {
                Data = data;
                Next = null;
            }

            public int Data { get; set; }
            public LinkedListNode Next { get; set; }

            public string ToString()
            {
                return Data.ToString();
            }
        }


        // 1 -> 2 -> 3 -> 4 -> 5

        // 1 -> 5 -> 2 -> 3 -> 4

        // 1 -> 5 -> 2 -> 4 -> 3

        // O(1) space
        public static LinkedListNode ReorderSingleLinkedList(LinkedListNode root)
        {
            if (root == null) return null;

            //1 -> 5 -> 2 -> 4 -> 3

            LinkedListNode currentNode = root;

            //bool done = false;
            while (currentNode != null && currentNode.Next != null)
            {
                //Find last element in the list
                LinkedListNode prev = null;
                LinkedListNode lastElement = currentNode;
                while (lastElement.Next != null)
                {
                    prev = lastElement;
                    lastElement = lastElement.Next;
                }

                if (prev == null || prev == currentNode)
                {
                    //Already on last element, so quit
                    break;
                }

                //Link 5 -> 2
                lastElement.Next = currentNode.Next;
                //Link 1 -> 5
                currentNode.Next = lastElement;
                //Link 4 -> NULL
                prev.Next = null;

                //Advance currentNode to 2
                currentNode = lastElement.Next;
            }


            return root;
        }

        //0, 1, 2, 3, 4
        //
        //diff = 0
        //      0   1
        //      0   2
        //      0   3
        //      0   4
        // 
        //
        //2,3,4,0,1
        //
        //diff = -2
        //      -2  1
        //      -2  2
        //      -2  3
        //      3   1
        //      3   2

        //2     start       end (start + input.length-1-start)
        //      2           2+5-1-2 = 4
        //3     3           3+5-1-3 = 4
        //4     4           4+5-1-4 = 4
        //0     2           2+5-1-2 = 4
        //1     2           2+5-1-2 = 4


        //2,3,4,0,1
        //      Rotate      Rotate
        //      start       end
        //2     2           4
        //3     2           3
        //4     2           2
        //0     0           4
        //1     2           4


        //0,1,2,1,3,4
        //index = 3
        //val = 1
        //good starting = 1

        //0,1,2,3,4,5
        //  0,1,2,3,4,5
        //    0,1,2,3,4,5
        //      0,1,2,3,4,5
        //        0,1,2,3,4,5
        //          0,1,2,3,4,5
        //0,1,2,1,3,4|0,1,2,1,3
        //^ ^ ^ ^ ^ ^               = 5
        //  ^ ^ ^ ^ ^ ^             = 4
        //    ^ ^ ^ ^ ^ ^           = 3
        //      ^ ^ ^ ^ ^ ^         = 3
        //        ^ ^ ^ ^ ^ ^       = 4
        //          ^ ^ ^ ^ ^ ^     = 5

        public static int FindAmazingNumberOffset(int[] input)
        {

            //Amazing number: its value is less than or equal to its index
            //  Given a circular array, find the starting position such that the total
            //  number of amazing numbers is maximized
            //Solution should be less than O(N^2)



            ////////////////////////////////////////////////////////////
            //Brute Force
            //
            //Iterate over each offset from 0 to input.length
            //Count the number of amazing numbers in the list starting at each offset
            //Runtime: O(n^2)

            //int offsetMax = -1;
            //int offsetBest = -1;

            //for (int offset=0; offset < input.Length; offset++)
            //{
            //    int count = 0;
            //    for (int i=0; i < input.Length; i++)
            //    {
            //        int index = (offset + i) % input.Length;
            //        if (input[index] <= i)
            //        {
            //            count++;
            //        }
            //    }

            //    if (count > offsetMax)
            //    {
            //        offsetBest = offset;
            //        offsetMax = count;
            //    }
            //}

            //return offsetBest;

            ///////////////////////////////////////////////
            //Interval based solution
            //https://www.careercup.com/question?id=6018738030641152
            //
            //Iterate over each offset from 0 to input.length
            //Calculate the amazing number interval for each number using this formula
            //
            //      Valid interval is defined as:
            //          <index> + 1 ... n + <index> - a[<index>]

            //Example 1:
            //index: 0, 1, 2, 3, 4, 5, 6, 
            //value: 4, 2, 8, 2, 4, 5, 3, 
            //n = 7
            //
            //value 4 at index 0: can be used if start index is between 1 and 3
            //    because there must be at least 4 elements before a[0] to satisfy
            //    a[0] >= index.
            //    that is 0 + 1..n + 0 - a[0]
            //
            //    Why?
            //  
            //       
            //      Index 0     index: 0, 1, 2, 3, 4, 5, 6      4 > 0 bad
            //      Index 1     index: 6, 0, 1, 2, 3, 4, 5      4 <= 6 amazing with index 1
            //      Index 2     index: 5, 6, 0, 1, 2, 3, 4      4 <= 5 amazing with index 2
            //      Index 3     index: 4, 5, 6, 0, 1, 2, 3      4 <= 4 amazing with index 3
            //      Index 4     index: 3, 4, 5, 6, 0, 1, 2      4 > 3 bad
            //      Index 5     index: 2, 3, 4, 5, 6, 0, 1      4 > 2 bad
            //      Index 6     index: 1, 2, 3, 4, 5, 6, 0      4 > 1 bad
            //                  value: 4, 2, 8, 2, 4, 5, 3,     
            //
            //
            //value 2 at index 1: can be used if start index is between 2 and 6
            //    that is 1 + 1..n + 1 - a[1]
            //...
            //
            //Example 2:
            //0,1,2,1,3,4|0,1,2,1,3         <-- Here the array is 0,1,2,1,3,4 and | denotes the circular restart
            //
            //0 -> 0 .. 6
            //0 -> 0+1 .. 6 + 1 - a[0](0) = 1 .. 7  (7 is 0, so it actually covers 0 to 6 in a circular reference way)
            //1 -> 1+1 .. 6 + 1 - a[1](1) = 2 .. 6
            //2 -> 2+1 .. 6 + 2 - a[2](2) = 3 .. 6
            //1 -> 3+1 .. 6 + 3 - a[3](1) = 4 .. 8
            //3 -> 4+1 .. 6 + 4 - a[4](3) = 5 .. 7
            //4 -> 5+1 .. 6 + 5 - a[5](4) = 6 .. 7
            //
            //
            //Once the intervals are created, the solution is to find the greatest occurance inside the intervals


            ///////////////////////////
            //Conceptual solution, not implemented below
            //
            //Once we have the intervals, we can sort by the start and end values and merge into a single list keeping tack of
            //the start and end points
            //We can then scan the list and increment on the start and decrement on the ends
            //That info can use used to find the local min and max values
            //1 .. 7 -> 1S, 7E
            //2 .. 6 -> 2S, 6E
            //3 .. 6 -> 3S, 6E
            //4 .. 8 -> 4S, 8E
            //5 .. 7 -> 5S, 7E
            //6 .. 7 -> 6S, 7E

            //List
            //1S, 2S, 3S, 4S, 5S, 6S, 6E, 6E, 7E, 7E, 7E, 8E
            //Each S increments and each E decrements
            //1   2   3   4   5   6   5   4   3   2   1   0
            //                    ^
            //Best starting offset is 6
            //Number of elements i 

            //End conceptual solution
            /////////////////////////////////////

            /////////////////////
            //Solution implemented below
            //Alternate finding of index, which basically does the solution above, but is less intutive
            //
            //In this solution, we iterate from i to n and increment 'k' when a start is found
            //and decrement 'k' when an end is found
            //
            //
            //Place starting indexes and ending indexes in seperates lists
            //Sort lists
            //
            //Finding the index works as follows:
            //let k be the number of intervals covered by the current index
            //int k = 0;
            //int maxk = 0;
            //int maxki = 0;
            //int s = 0;
            //int e = 0;
            //for (int i = 0; i < n; i++)
            //{
            //    while (s < start.size() && start[s] == i) { s++; k++; }
            //    if (k > maxk) {// new found { ... } }
            //    while (e < end.size() && end[e] == i) { e++; k--; }
            //}
            //
            //


            //Create intervals
            List<int> start = new List<int>();
            List<int> end = new List<int>();
            for (int index = 0; index < input.Length; index++)
            {
                // START           END
                // <index> + 1 ... n + <index> - a[<index>]
                int _start = index + 1;
                int _end = input.Length + index - input[index];

                //if _end >= _start, interval not valid.  Could easily also check input[index] >= input.Length
                if (_end >= _start)
                {
                    //Add start
                    start.Add(_start);

                    //if end is less than input.length, just add it
                    if (_end < input.Length)
                    {
                        end.Add(_end);
                    }
                    else
                    {
                        //Otherwise, split into two intervals since it wraps at the end of the array
                        //First interval ends at the end of the array (input.Length -1)                        
                        end.Add(input.Length - 1);

                        //Second interval starts at 0 and goes to _end % input.length
                        start.Add(0);
                        end.Add(_end % input.Length);
                    }

                }
            }

            //Sort the start and end lists
            start.Sort();
            end.Sort();

            //1.    Iterate from i to input.Length
            //2.    While i == start[s], increment k
            //          This means for each inerval that starts at i, increment k
            //          Conceptually, If start and end are thought of as parentheses, then k is the number of nested parenthese we are in
            //3.    Then check to see if we are in a local maximum by comparing against maxk
            //          If we are in a local max,
            //              a.  Set maxk = k
            //              b.  Set maxki = i % input.Length        <-- This will be the return value for the local maximum
            //4.    While i == end[e], decrement k
            //          This means for each interval that ends on i, decrement k
            //          Conceptually, this would be the closing parentheses
            //
            int k = 0;
            int maxk = 0;
            int maxki = 0; //return value
            int s = 0;
            int e = 0;

            //1.    Iterate from i to input.Length
            for (int i = 0; i < input.Length; i++)
            {
                //2.    Check for starting interval at i
                while (s < start.Count() && start[s] == i)
                {
                    //Found starting interval, so increment k and repeat while the starting interval is still 'i'
                    s++;
                    k++;
                }

                //3.    Check for a local maximum
                if (k > maxk)
                {
                    //New max found
                    maxki = i % input.Length;
                    maxk = k;
                }

                //3.    Check for ending interval at i
                while (e < end.Count() && end[e] == i)
                {
                    //Found ending interval at i, so decrement k and repeat while the ending interval is still 'i'
                    e++;
                    k--;
                }
            }

            //Finally return the solution
            return maxki;




        }


        //Problem 1.4 - Replace all white spaces in a string with %20.
        //Provided true length of string
        //Char array has enough space at end to hold all chars

        //Input:  "Mr John Smith    ", 13
        //Output: "Mr%20John%20Smith"
        public static char[] ReplaceSpaces(char[] input, int len)
        {
            int numerOfSpaces = 0;
            for (int x = 0; x < len; x++)
            {
                if (input[x] == ' ')
                    numerOfSpaces++;
            }

            //%20 is 3 chars, but only using 2 extra since the space is already using 1 char
            int end = len + numerOfSpaces * 2 - 1; //13 + 2*2 -1 = 13+4-1 = 16

            for (int x = len - 1; x >= 0; x--)
            {
                if (input[x] == ' ')
                {
                    input[end--] = '0';
                    input[end--] = '2';
                    input[end--] = '%';
                }
                else
                {
                    input[end] = input[x];
                    end--;
                }
            }

            return input;

        }

        //Rotate NxM image 90 degrees
        //Each pixle is 4 bytes

        // 11112222333344445555
        // aaaabbbbccccddddeeee
        // 66667777888899990000
        // ffffgggghhhhiiiijjjj

        // ffff6666aaaa1111
        // gggg7777bbbb2222
        // hhhh8888cccc3333
        // iiii9999dddd4444
        // jjjj0000eeee5555

        public static char[,] RotateMatrix(char[,] input)
        {
            int width = input.GetLength(0);
            int height = input.GetLength(1);

            char[,] output = new char[height / 4, width * 4]; // [5, 16]

            //int pixelWidth = width / 4;
            int pixelHeight = height / 4;


            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < pixelHeight; x++)
                {
                    //output[0, 12-15]
                    //output[1, 12-15]
                    output[x, (width - 1 - y) * 4] = input[y, x * 4];
                    output[x, (width - 1 - y) * 4 + 1] = input[y, x * 4 + 1];
                    output[x, (width - 1 - y) * 4 + 2] = input[y, x * 4 + 2];
                    output[x, (width - 1 - y) * 4 + 3] = input[y, x * 4 + 3];


                    //output[pixelHeight - y * 4, x] = input[x * 4, y];
                    //output[pixelHeight - y * 4 + 1, x] = input[x * 4 + 1, y];
                    //output[pixelHeight - y * 4 + 2, x] = input[x * 4 + 2, y];
                    //output[pixelHeight - y * 4 + 3, x] = input[x * 4 + 3, y];
                }
            }

            return output;
        }

        public static int[] SortSquaresOfIntegers(int[] vals)
        {
            //-10, -5, -1, 1, 3, 5, 10

            //100, 25, 1, 1, 9, 25, 100

            //Create two lists based for positive and negative numbers
            //then merge sort the output array

            int[] one = new int[vals.Length]; //for negative squares
            int[] two = new int[vals.Length];
            int oneCount = 0;
            int twoCount = 0;
            for (int x = 0; x < vals.Length; x++)
            {
                int val = vals[x] * vals[x];
                if (vals[x] < 0)
                    one[oneCount++] = val;
                else two[twoCount++] = val;
            }



            //Check if there are only positive or negative values.  If so, return solution
            if (oneCount == 0)
                return two;

            if (twoCount == 0)
                return one;

            //merge sort the arrays
            int[] ret = new int[vals.Length];

            int oneIndex = oneCount - 1;
            int twoIndex = 0;
            for (int x = 0; x < ret.Length; x++)
            {
                bool useOne = false;
                if (oneIndex >= 0 && twoIndex < twoCount)
                {
                    if (one[oneIndex] < two[twoIndex])
                        useOne = true;
                }
                else if (oneIndex >= 0)
                {
                    useOne = true;
                }

                if (useOne)
                {
                    ret[x] = one[oneIndex--];
                }
                else
                {
                    ret[x] = two[twoIndex++];
                }
            }

            return ret;

        }

        // 1, 3, 5, 18      x = 8

        public static bool HasContigousSubArraySum(int[] arr, int target)
        {
            if (arr == null)
                return false;
            if (arr.Length == 0)
                return false;

            int start = 0;
            int end = 0;
            int sum = arr[0];

            while (start < arr.Length && end < arr.Length)
            {
                //sum == target, return true
                if (sum == target)
                {
                    return true;
                }
                else if (sum > target && start == end)
                {
                    //Only using 1 value and start == end, so increment both
                    start++;
                    end++;
                    if (start < arr.Length)
                    {
                        sum = arr[start];
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (sum < target)
                {
                    //sum is less than target so add more numbers to the end
                    end++;
                    if (end < arr.Length)
                        sum += arr[end];
                    else return false;
                }
                else if (sum > target)
                {
                    //Sum exceeded target, so remove starting value
                    sum -= arr[start];
                    start++;
                }
            }

            return false;



        }

        //        1) Given a list of(x, y) coordinates, return k nearest points to(0, 0)



        public class MyPoint : IComparable
        {
            public int X { get; set; }
            public int Y { get; set; }
            public double Distance { get; set; }

            public int CompareTo(object obj)
            {
                MyPoint other = (MyPoint)obj;

                return Distance.CompareTo(other.Distance);
            }
        }

        public static MyPoint FindClosestKPoint(MyPoint[] points, int k)
        {
            if (points == null)
                return null;

            if (points.Length == 0)
                return null;

            Heap<MyPoint> pointHeap = new Heap<ScratchPad.MyPoint>(Heap<ScratchPad.MyPoint>.heapTypeEnum.min);

            for (int x = 0; x < points.Length; x++)
            {
                double distance = Math.Sqrt(Math.Pow(points[x].X - 0, 2) + Math.Pow(points[x].Y - 0, 2));

                //MyPoint p = new MyPoint() { X = points[x].X, Y = points[x].Y, Distance = distance}
                points[x].Distance = distance;

                pointHeap.Enqueue(points[x]);
            }

            while (k > 1 && pointHeap.size > 0)
            {
                pointHeap.Dequeue();
                k--;
            }

            if (pointHeap.size == 0)
                return null;

            return pointHeap.Dequeue();


        }

        //2) Given a string of parenthesis and characters, remove the invalid parentheses.
        //eg. "(ab(a)" => "ab(a)"  

        public static string RemoveInvalidParentheses(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            //Scan string and make list of open and close parenteses by their index

            //List<int> opening = new List<int>();
            //List<int> closing = new List<int>();
            Stack<int> opening = new Stack<int>();
            Stack<int> closing = new Stack<int>();
            for (int x = 0; x < input.Length; x++)
            {
                if (input[x] == '(')
                {
                    opening.Push(x);
                }
                else if (input[x] == ')')
                {
                    closing.Push(x);
                }
            }

            //List<int> remove = new List<int>();
            Heap<int> remove = new Heap<int>(Heap<int>.heapTypeEnum.max);
            while (opening.Count > 0 || closing.Count > 0)
            {
                if (opening.Count > 0 && closing.Count == 0)
                {
                    remove.Enqueue(opening.Pop());
                }
                else if (opening.Count == 0 && closing.Count > 0)
                {
                    remove.Enqueue(closing.Pop());
                }
                else
                {
                    closing.Pop();
                    opening.Pop();
                }
            }

            while (remove.size > 0)
            {
                input = input.Remove(remove.Dequeue(), 1);
            }


            return input;

        }

        //Ensure that there are a minimum of n dashes between any two of the same characters of a string.
        //Example: n = 2, string = "ab-bcdecca" -> "ab--bcdec--ca"

        public static string EnsureNDashesBetweenTwoChars(string input, int n)
        {
            //Find first starting char
            StringBuilder sb = new StringBuilder();
            char lastChar = input[0];
            int dashCount = 0;
            for (int x = 0; x < input.Length; x++)
            {
                if (x == 0 || lastChar == '-')
                {
                    lastChar = input[x];
                    sb.Append(input[x]);
                }
                else
                {
                    if (input[x] == '-')
                    {
                        dashCount++;
                        sb.Append(input[x]);
                    }
                    else if (input[x] == lastChar)
                    {
                        while (dashCount < n)
                        {
                            sb.Append('-');
                            dashCount++;
                        }
                        sb.Append(input[x]);
                        dashCount = 0;
                    }
                    else
                    {
                        lastChar = input[x];
                        sb.Append(input[x]);
                        dashCount = 0;
                    }
                }
            }

            return sb.ToString();
        }


        public static string StringCompression(string val)
        {
            if (string.IsNullOrEmpty(val))
                return string.Empty;

            if (val.Length <= 2)
                return val;

            StringBuilder sb = new StringBuilder();

            char lastChar = '0';
            int count = 0;

            //aabccccaaa
            //lastChar = a  count=1 x=0
            //          a       2     1
            //          b       1     2 --> a2
            //          c       1     3 --> b1
            //          c       2     4
            //          ...

            for (int x = 0; x < val.Length; x++)
            {
                if (x == 0)
                {
                    lastChar = val[0];
                    count = 1;
                }
                else if (val[x] == lastChar)
                {
                    count++;
                }
                else
                {
                    sb.Append(lastChar);
                    sb.Append(count);

                    lastChar = val[x];
                    count = 1;
                }
            }

            sb.Append(lastChar);
            sb.Append(count);

            if (sb.Length < val.Length)
                return sb.ToString();

            return val;

        }

        //Given an array of integer, find the maximum drop between two array elements, given that second element comes after the first one.
        public static int FindMaximumDrop(int[] vals)
        {
            //1, 5, 10, 3, 7, 15, 8, 9, 4, 3, 1, 3

            int maxDrop = -1;
            int localMax = int.MinValue;
            int localMin = int.MaxValue;
            for (int x = 0; x < vals.Length; x++)
            {
                if (x == 0)
                {
                    localMax = vals[0];
                    localMin = vals[0];
                    maxDrop = 0;
                }
                else if (vals[x] > localMax)
                {
                    localMax = vals[x];
                    localMin = vals[x];
                }
                else if (vals[x] < localMin)
                {
                    localMin = vals[x];
                    int drop = localMax - localMin;
                    if (drop > maxDrop)
                        maxDrop = drop;
                }
                else
                {
                    //do nothing
                }
            }

            return maxDrop;
        }


        //We have an array of objects A and an array of indexes B.Reorder objects in array A with given indexes in array B.Do not change array A's length. 
        //example:


        //var A = [C, D, E, F, G];
        //var B = [3, 0, 4, 1, 2];

        //sort(A, B);
        // A is now [D, F, G, C, E];

        public static void Reorder(ref char[] A, ref int[] B)
        {
            for (int x = 0; x < A.Length;)
            {
                if (B[x] == x)
                {
                    x++;
                }
                else
                {
                    //swap B[x] with B[B[x]]
                    int tmpInt = B[x];
                    char tmpChar = A[x];

                    A[x] = A[B[x]];
                    B[x] = B[B[x]];

                    A[tmpInt] = tmpChar;
                    B[tmpInt] = tmpInt;
                }
            }

        }


        public static Wrapper FindNext(int[] preorder, Stack<int> stack, int index)
        {
            while (index < preorder.Length)
            {
                if (stack.Count == 0 || preorder[stack.Peek()] > preorder[index])
                {
                    stack.Push(index);  //push the index
                    index++;
                }
                else
                {
                    return new Wrapper(index, stack.Pop());
                }
            }
            if (stack.Count == 0) return new Wrapper(index, null);
            return new Wrapper(index, stack.Pop());
        }

        public class Wrapper
        {
            public int index;  //index of currently traversed node in the array (as in pre-order)
            public int? c;  //index of the next leave (as in in-order)

            public Wrapper(int index, int? c)
            {
                this.index = index;
                this.c = c;
            }
        }

        //compare the if the two leaves are equal
        public static void FirstNonMathcingLeaves(int[] o1, int[] o2)
        {
            Stack<int> s1 = new Stack<int>(), s2 = new Stack<int>();

            Wrapper w1 = new Wrapper(0, 0), w2 = new Wrapper(0, 0);
            while (w1.c != null && w2.c != null && o1[w1.c.Value] == o2[w2.c.Value])
            {
                w1 = FindNext(o1, s1, w1.index);
                w2 = FindNext(o2, s2, w2.index);
            }

            if (w1.c == null && w2.c == null)
            {
                System.Diagnostics.Debug.WriteLine("same"); return;
            }
            if (w1.c == null)
            {
                System.Diagnostics.Debug.WriteLine(w1.c + " " + o2[w2.c.Value]); return;
            }
            if (w2.c == null)
            {
                System.Diagnostics.Debug.WriteLine(o1[w1.c.Value] + " " + w2.c); return;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(o1[w1.c.Value] + " " + o2[w2.c.Value]);
            }
        }


        public static int FindBestBuySellStock(int[] stockPrice)
        {
            //Find greatest difference between n and n+1 where (n+m) - (n) > 0
            //stock_prices_yesterday = [10, 7, 5, 8, 11, 9, 8, 4, 3, 6, 5, 11, 12, 10]
            //get_max_profit(stock_prices_yesterday)
            //# returns 6 (buying for $5 and selling for $11)

            //v2: buy 4, sell 12

            //min, max
            //challengeMin, challengeMax


            //foreach (int n in stockPrice)
            //{

            //}
            bool haveMinMax = false;
            int min = int.MaxValue;
            int max = int.MinValue;

            bool haveChallenge = false;
            int challengeMin = int.MaxValue;
            int challengeMax = int.MinValue;
            int buyIndex = -1;
            int sellIndex = -1;

            for (int x = 0; x < stockPrice.Length; x++)
            {
                if (!haveMinMax)
                {
                    //Look for first buy sell that is profitable
                    if (stockPrice[x] < min)
                        min = stockPrice[x];
                    else if (stockPrice[x] - min > 0)
                    {
                        max = stockPrice[x];
                        haveMinMax = true;
                    }
                }
                else
                {
                    //have min and max positive values
                    //look for challenging min and max values

                    if (stockPrice[x] > max)
                    {
                        max = stockPrice[x];
                    }


                    if (stockPrice[x] < min)
                    {
                        if (challengeMin == int.MaxValue)
                        {
                            challengeMin = stockPrice[x];
                            challengeMax = int.MinValue;
                        }
                        else if (stockPrice[x] < challengeMin)
                        {
                            challengeMin = stockPrice[x];
                            challengeMax = int.MinValue;
                        }
                    }
                    else if ((challengeMin != int.MaxValue) && stockPrice[x] > challengeMax && stockPrice[x] > challengeMin)
                    {
                        challengeMax = stockPrice[x];

                        if (challengeMax - challengeMin > max - min)
                        {
                            max = challengeMax;
                            min = challengeMin;
                            challengeMin = int.MaxValue;
                            challengeMax = int.MinValue;
                        }
                    }

                }

            }


            if (min != int.MaxValue && max != int.MinValue)
            {
                System.Diagnostics.Debug.WriteLine("Best Min (" + min + ") Best Max (" + max + ") Profit (" + (max - min) + ")");
                return max - min;
            }

            return -1;
        }


        public static int[] GetProductsOfAllIntsExceptAtIndex(int[] vals)
        {
            int[] ret = new int[vals.Length];
            //[1, 7, 3, 4]
            //[84, 12, 28, 21]
            //[7*3*4, 1*3*4, 1*7*4, 1*7*3]

            int prev = 1;

            //Create stack of multipled values on the right side
            Stack<int> right = new Stack<int>();
            Queue<int> left = new Queue<int>();
            Stack<int> leftStack = new Stack<int>();
            for (int z = vals.Length - 1; z > 0; z--)
            {
                if (right.Count == 0)
                {
                    right.Push(vals[z]);
                }
                else
                {
                    right.Push(vals[z] * right.Peek());
                }

                int leftIndex = (vals.Length - 1) - z;
                if (left.Count == 0)
                {
                    left.Enqueue(vals[leftIndex]);
                }
                else
                {
                    int last = left.Peek();
                    int last2 = left.Dequeue();
                    left.Enqueue(last2);
                    left.Enqueue(vals[leftIndex] * last2);// left.Peek());
                }

                //leftStack
                if (leftStack.Count == 0)
                {
                    leftStack.Push(vals[leftIndex]);
                }
                else
                {
                    int last = leftStack.Peek();
                    leftStack.Push(vals[leftIndex] * last);
                }
            }

            //reverse stack
            Stack<int> leftStack2 = new Stack<int>();
            while (leftStack.Count > 0)
                leftStack2.Push(leftStack.Pop());

            for (int j = 0; j < vals.Length; j++)
            {
                if (j == 0)
                {
                    ret[j] = right.Pop();
                }
                else if (j == vals.Length - 1)
                {
                    ret[j] = leftStack2.Pop();
                    //ret[j] = left.Dequeue();
                }
                else
                {
                    ret[j] = leftStack2.Pop() * right.Pop();
                    //ret[j] = left.Dequeue() * right.Pop();
                }
            }





            ////Use prev multipled values to 
            //for (int x=0; x < vals.Length; x++)
            //{
            //    int val = prev;

            //    //for (int y=x+1; y < vals.Length; y++)
            //    //{
            //    //    val = val * vals[y];
            //    //}
            //    if (right.Count > 0)
            //        val = val * right.Pop();
            //    ret[x] = val;

            //    prev = prev * vals[x];
            //}

            for (int i = 0; i < ret.Length; i++)
            {
                System.Diagnostics.Debug.WriteLine(ret[i]);
            }

            return ret;

        }

        public class MeetingTime : IComparable
        {
            public int Start { get; set; }
            public int End { get; set; }

            public int CompareTo(object obj)
            {
                return this.Start.CompareTo(((MeetingTime)obj).Start);
            }
        }

        //O(nLogn)
        //n for pass through list
        //n Logn for sort
        public static MeetingTime[] MergeMeetingTimes(MeetingTime[] vals)
        {
            List<MeetingTime> list = new List<MeetingTime>(vals);
            list.Sort();

            for (int x = 1; x < list.Count;)
            {
                if (list[x - 1].End >= list[x].Start)
                {
                    list[x - 1].End = Math.Max(list[x - 1].End, list[x].End);
                    list.RemoveAt(x);
                }
                else
                {
                    x++;
                }
            }

            return list.ToArray();

        }


        //Example: for amount=4  and denominations=[1,2,3] (1¢, 2¢ and 3¢), your program would output 4, the number of ways to make 4¢ with those denominations:
        //  1¢, 1¢, 1¢, 1¢
        //  1¢, 1¢, 2¢
        //  1¢, 3¢
        //  2¢, 2¢
        //Permutations of 1, 2, 3
        //  1
        //  1   2
        //  1   2   3
        //      2   3
        //  1       3
        //      2
        //          3

        //Given 1
        //      Amount = 4
        //      Currency = 1
        //          use 1
        //          Make 3 using 1
        //              use 1
        //              Make 2 using 1
        //                  use 1
        //                  Make 1 using 1
        //                      use 1
        //                      Have solution


        //Given 1 2
        //      Amount = 4
        //      Use 2
        //          Amount=2
        //          Solve for 2 with currency 2 -> use 2
        //          Solve for 2 with currency 1 -> use 1, use 1

        //Given 1 2 3
        //      Amount = 4
        //      Use 

        //Given       


        public static int ChangePossibilitiesBottomUp(int amount, int[] denominations)
        {
            int[] waysOfDoingNCents = new int[amount + 1]; // Array of zeros from 0..amount
            waysOfDoingNCents[0] = 1;

            foreach (int coin in denominations)
            {
                for (int higherAmount = coin; higherAmount < amount + 1; higherAmount++)
                {
                    int higherAmountRemainder = higherAmount - coin;
                    waysOfDoingNCents[higherAmount] += waysOfDoingNCents[higherAmountRemainder];
                }
            }

            return waysOfDoingNCents[amount];
        }


        //        I like parentheticals(a lot).
        //"Sometimes (when I nest them (my parentheticals) too much (like this (and this))) they get confusing."

        //Write a function that, given a sentence like the one above, along with the position of an opening parenthesis, finds the corresponding closing parenthesis.

        //Example: if the example string above is input with the number 10 (position of the first parenthesis), the output should be 79 (position of the last parenthesis).

        public class Paren
        {
            public int Position { get; set; }
            public char Val { get; set; }
            public int NestingLevel { get; set; }
        }

        //Can refactor to do this in O(1) space by using the nesting levels and scanning the string once

        public static int FindMatchingParentheses(string input, int val)
        {
            //  slkfjlskaf ( aklsdjfslkdfj( sd(k)fj) dfgfg (slk)jdf)
            //             1s            10s  12s 13e     15e  16s  18e  20e

            // 1s 10s 12s 13e 15e 16s 18e 20e

            //Create array for Start and End tags and associate nesting level with each tag
            //For requested start, find next end with same level

            List<Paren> parenList = new List<Paren>();

            int nestingLevel = 0;
            for (int x = 0; x < input.Length; x++)
            {
                if (input[x] == '(')
                {
                    parenList.Add(new Paren() { Position = x, Val = '(', NestingLevel = nestingLevel });
                    nestingLevel++;
                }
                else if (input[x] == ')')
                {
                    nestingLevel--;
                    parenList.Add(new Paren() { Position = x, Val = ')', NestingLevel = nestingLevel });
                }
            }

            int findClosingWithNestingLevel = -1;
            for (int i = 0; i < parenList.Count; i++)
            {
                if (parenList[i].Position == val)
                {
                    findClosingWithNestingLevel = parenList[i].NestingLevel;
                }
                else if (parenList[i].Val == ')' && parenList[i].NestingLevel == findClosingWithNestingLevel)
                {
                    return parenList[i].Position;
                }
            }

            return -1;

        }

        //I have a list where every number in the range 1...n1...n appears once except for one number which appears twice.
        //Write a function for finding the number that appears twice.

        public static int FindAppearingTwice(int[] arr)
        {
            Dictionary<int, int> values = new Dictionary<int, int>();

            for (int x = 0; x < arr.Length; x++)
            {
                if (values.ContainsKey(arr[x]))
                    return arr[x];
                values.Add(arr[x], 0);
            }

            return -1;
        }

        //Assume short list and small numbers
        public static int FindAppearingTwiceV2(int[] arr)
        {
            int sum = 0;
            for (int x = 0; x < arr.Length; x++)
            {
                //Don't sum the last place since we are looking for a dup
                if (x < arr.Length - 1)
                    sum = sum + (x + 1);        //use x+1 since rance is 1..n not 0..n
                sum = sum - arr[x];
            }

            return Math.Abs(sum);

        }

        public static void MoveZerosToEnd(ref int []arr)
        {

            int i = 0;
            int j = arr.Length - 1;

            while (i < j)
            {
                if (arr[i] == 0)
                {
                    while (arr[j] == 0 && i < j)
                    {
                        j--;
                    }

                    if (j <= i)
                        break;
                    int tmp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = tmp;
                }
                else i++;
            }


        }

        //        Given an input string "aabbccba", find the shortest substring from the alphabet "abc". 

        //In the above example, there are these substrings "aabbc", "aabbcc", "ccba" and "cba". 
        //However the shortest substring that contains all the characters in the alphabet is "cba", so "cba" must be the output.

        //Output doesnt need to maintain the ordering as in the alphabet. 

        //Other examples: 
        //input = "abbcac", alphabet= "abc" Output : shortest substring = "bca".

        //Create window size of the string
        //Count number of occurance of each letter needed in alphabet
        //Shrink left until condition no longer met
        //shrink right until condition no longer met


        //Window size alphabet left to right and check for a solution
        //Then window alphabet + 1 left to right anc check for a solution
        // ...
        //window size of array and check for a solution

        public static bool CountAlphabet(char []arr, char []alphabet, int start, int length)
        {
            //int[] count = new int[alphabet.Length];
            Dictionary<char, int> counts = new Dictionary<char, int>();
            for (int index= start; index < start+ length; index++)
            {
                char c = arr[index];
                if (counts.ContainsKey(c))
                    counts[arr[index]]++;
                else counts.Add(c, 1);
            }
            if (counts.Keys.Count >= alphabet.Length)
                return true;

            return false;
        }

        public static string FindShortestSubstringWithAlphabet(char []arr, char []alphabet)
        {            
            for (int windowSize = alphabet.Length; windowSize <= arr.Length; windowSize++)
            {
                for (int start = 0; start + windowSize <= arr.Length; start++)
                {
                    if (CountAlphabet(arr, alphabet, start, windowSize))
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int index = start; index < start + windowSize; index++)
                        {
                            sb.Append(arr[index]);
                        }
                        return sb.ToString();
                    }
                }
            }

            return string.Empty;
        }

        static Dictionary<char, int> counts = new Dictionary<char, int>();
        public static void AlphabetAddChar(char addChar)
        {
            if (counts.ContainsKey(addChar))
                counts[addChar]++;
            else counts.Add(addChar, 1);
        }

        public static void AlphabetRemoveChar(char removeChar)
        {
            if (counts.ContainsKey(removeChar))
            {
                counts[removeChar]--;
                if (counts[removeChar] <= 0)
                    counts.Remove(removeChar);
            }
        }

        public static bool AlphabetFoundSolution(int alphabetLength)
        {
            return counts.Keys.Count == alphabetLength;
        }

        public static string AlphabetMakeSolution(char[] arr, int start, int length)
        {
            StringBuilder sb = new StringBuilder();
            for (int index = start; index < start + length; index++)
            {
                sb.Append(arr[index]);
            }
            return sb.ToString();
        }
        
        public static string FindShortestSubstringWithAlphabetV2(char[] arr, char[] alphabet)
        {
            Dictionary<char, int> alphabetMap = new Dictionary<char, int>();
            for (int x=0; x < alphabet.Length; x++)
            {
                alphabetMap.Add(alphabet[x], 0);
            }

            counts = new Dictionary<char, int>();
            //prime with first chars
            for (int start=0; start < alphabet.Length; start++)
            {
                if (alphabetMap.ContainsKey(arr[start]))
                    AlphabetAddChar(arr[start]);
            }

            if (AlphabetFoundSolution(alphabet.Length))
            {
                return AlphabetMakeSolution(arr, 0, alphabet.Length);
            }

            //Use increasing window size moving left to right then right to left
            bool movingLeftToRight = true;
            for (int windowSize = alphabet.Length; windowSize <= arr.Length; windowSize++)
            {
                char prevChar = 'z';
                if (movingLeftToRight)
                {
                    for (int start = 0; start + windowSize <= arr.Length; start++)
                    {
                        //First start left to right using primed values
                        if (start == 0 && windowSize == alphabet.Length)
                        {
                            //Check for solution.  May have it from last return trip when the prevChar was added back on
                            if (AlphabetFoundSolution(alphabet.Length))
                                return AlphabetMakeSolution(arr, start, windowSize);

                            //do nothing but record first char so we can remove it.
                            prevChar = arr[start];
                        }
                        else if (start == 0)
                        {
                            //nth trip from left to right

                            //Add last char in window size
                            if (alphabetMap.ContainsKey(arr[start + windowSize - 1]))
                            {
                                AlphabetAddChar(arr[start + windowSize - 1]);
                                if (AlphabetFoundSolution(alphabet.Length))
                                    return AlphabetMakeSolution(arr, start, windowSize);
                            }

                            prevChar = arr[start];
                        }
                        else
                        {
                            if (alphabetMap.ContainsKey(prevChar))
                                AlphabetRemoveChar(prevChar);

                            if (alphabetMap.ContainsKey(arr[start + windowSize - 1]))
                            {
                                AlphabetAddChar(arr[start + windowSize - 1]); //Add the new char that was just included in the window
                                if (AlphabetFoundSolution(alphabet.Length))
                                    return AlphabetMakeSolution(arr, start, windowSize);
                            }
                                                      
                            prevChar = arr[start];
                        }
                    }
                    movingLeftToRight = false;
                    //Add prevChar back to counts for return trip
                    //AlphabetAddChar(prevChar);
                }
                else
                {
                    for (int start = arr.Length - windowSize; start >= 0; start--)
                    {
                        if (start == arr.Length - windowSize)
                        {
                            if (alphabetMap.ContainsKey(arr[start]))
                            {
                                //add starting char
                                AlphabetAddChar(arr[start]);

                                //check for solution
                                if (AlphabetFoundSolution(alphabet.Length))
                                    return AlphabetMakeSolution(arr, start, windowSize);
                            }

                            //prev char is now last char in string
                            prevChar = arr[start + windowSize - 1];
                        }
                        else
                        {
                            if (alphabetMap.ContainsKey(prevChar))
                                AlphabetRemoveChar(prevChar);

                            if (alphabetMap.ContainsKey(arr[start]))
                            {
                                AlphabetAddChar(arr[start]);
                                if (AlphabetFoundSolution(alphabet.Length))
                                    return AlphabetMakeSolution(arr, start, windowSize);
                            }

                            prevChar = arr[start + windowSize - 1];
                        }
                    }

                    movingLeftToRight = true;
                    //Add prevChar back to counts for return trip
                    //AlphabetAddChar(prevChar);
                }
            }

            return string.Empty;
        }

        public class BasicTreeNode
        {
            public BasicTreeNode(int val)
            {
                Value = val;
                Left = null;
                Right = null;
            }
            public int Value { get; set; }
            public BasicTreeNode Left { get; set; }
            public BasicTreeNode Right { get; set; }
        }

        //Add elements to a BST
        public static void AddToBST(BasicTreeNode root, int val)
        {
            if (val < root.Value)
            {
                if (root.Left == null)
                {
                    root.Left = new BasicTreeNode(val);
                    return;
                }
                AddToBST(root.Left, val);
                return;
            }

            if (root.Right == null)
            {
                root.Right = new BasicTreeNode(val);
                return;
            }
            AddToBST(root.Right, val);
        }

        public static void PrintTreeInLevelOrder(BasicTreeNode root)
        {
            //Level order so do BSF transversal

            Queue<BasicTreeNode> queue = new Queue<BasicTreeNode>();
            Queue<BasicTreeNode> tmpQueue = new Queue<BasicTreeNode>();

            queue.Enqueue(root);
            int level = 0;
            System.Diagnostics.Debug.Write("Level " + level + ": ");
            while (queue.Count > 0)
            {
                //Dequeue element
                BasicTreeNode current = queue.Dequeue();

                //print current element
                System.Diagnostics.Debug.Write(current.Value + " ");

                //Enqueue children onto tmpQueue
                if (current.Left != null)
                    tmpQueue.Enqueue(current.Left);
                if (current.Right != null)
                    tmpQueue.Enqueue(current.Right);

                if (queue.Count == 0)
                {
                    //no elements left on the queue, so going down next level
                    //Move all elements from tmpQueue to queue
                    while (tmpQueue.Count > 0)
                        queue.Enqueue(tmpQueue.Dequeue());

                    //If we have elements for the next level, write the leve header
                    if (queue.Count > 0)
                    {
                        System.Diagnostics.Debug.WriteLine("");
                        level++;
                        System.Diagnostics.Debug.Write("Level " + level + ": ");
                    }
                }
            }

        }

        /*********************************
        
        // Given an array with N elements, find two elements with the given sum K. 
        // Return a boolean for whether the array contains that the given sum K.
        //
        // For example:
        //   Input: [5, 8, 1, 6, 2], 3
        //   Output: true
        */

        public static bool FindSumToK(int[] arr, int k)
        {

            if (arr.Length < 2)
                return false;

            //arr.length = 5
            // i 0 -> 
            // 5, 8, 1, 6, 2
            // 0, 1, 2, 3, 4
            // 0        i     <- 5-2 = 3
            // 0           j  <- 5-1 = 4

            for (int i = 0; i <= arr.Length - 2; i++)
            {
                for (int j = i + 1; i <= arr.Length - 1; j++)
                {
                    if (arr[i] + arr[j] == k)
                        return true;
                }
            }
            return false;
        }


        public bool FindSumToK_sort(int[] arr, int k)
        {

            if (arr.Length < 2)
                return false;

            List<int> arrList = new List<int>();
            foreach (int i in arrList)
                arrList.Add(i);

            arrList.Sort();


            for (int i = 0; i <= arrList.Count - 2 && arrList[i] < k; i++)
            {
                for (int j = i + 1; i <= arr.Length - 1; j++)
                {
                    if (arr[i] + arr[j] == k)
                        return true;
                }
            }
            return false;
        }


        // O(n) time  space O(n) 
        public static bool FindSumToK_hash(int[] arr, int k)
        {
            //Input: [5, 8, 1, 6, 2], 3

            if (arr.Length < 2)
                return false;

            Dictionary<int, int> map = new Dictionary<int, int>();

            for (int i = 0; i < arr.Length; i++)
            {
                map.Add(arr[i], 0);
            }

            for (int j = 0; j < arr.Length; j++)
            {
                int valueWeNeed = k - arr[j];  // 3 - 5 = -2

                if (map.ContainsKey(valueWeNeed))
                    return true;
            }

            return false;
        }



        // Your code is stored in a revision control system, let's say something 
        // like svn, which numbers all versions sequentially.  You see a bug in 
        // your code, and you know it wasn't there before.  Write a function to 
        // find the revision that introduced the bug.
        // 
        // For example:
        //   revision 123 <-- good
        //   revision 124 <--  also good
        //   ...
        //   revision ??? <-- introduced the bug
        //   ...
        //   revision 544<-- also bad
        //   revision 545 <-- bad

        // bool isBad(int revision)


        //input good and bad revision
        //return first bad revision

        static int badRevision = 0;
        static bool isBad(int revision)
        {
            return revision >= badRevision;

        }

        public static int FindBadReivision(int start, int end, int badNumber)
        {
            badRevision = badNumber;
            //if (isBad(end))
            //    return 

            //given the start and end
            //  if isBad(end) == true
            //    FindMiddle and search again
            //  else this segment is ok


            // 1, 2, 3, 4, 5, 6, 7, 8, 9, 10
            //          ^
            //             ^
            // 1 --------  5
            //       ^
            //       -------
            //          ^
            //       +  -  return 4


            bool done = false;
            while (!done)
            {
                int middle = start + (end - start) / 2;

                if (isBad(middle))
                {
                    end = middle;
                }
                else
                {
                    //answer is between middle and end
                    start = middle;
                }

                if (start == end)
                {
                    if (isBad(end))
                        return end;
                    done = true;
                }

                if (isBad(end) == false)
                    return end + 1;

                if (end - start == 1)
                {
                    if (isBad(start) == false && isBad(end) == true)
                        return end;
                    else if (isBad(start))
                        return start;
                    done = true;
                }

            }

            return -1;
        }


        //Find shortest string with all chars in alphabet
        //O(b*a)
        public static string FindShortestStringWithAlphabet(string input, string alphabet)
        {

            //  ADOBECODEBANC
            //  0123456789111
            //            012
            //  ABC

            //  A   0, 10
            //  B   3, 9
            //  C   5, 12


            //Find minimum distance between A, B, C
            //
            // For each character in the alphabet, we need to find the nearest char for every other item in the alphabet
            // The we find the difference between the minimum and maximum index for each item in the alphabet
            //
            // For example, the index of the alphabet input chars are below
            //
            //  0   3   5   9   10  12
            //  A   B   C   B   A   C
            //  |
            //  A   nearest B = 3 (index)
            //      nearest C = 5 (index)
            //      current A = 0
            //      total = 5-0 = 5     <-- Max(A, B, C) = Max (0, 3, 5) = 5
            //                              Min(A, B, C) = Min(0, 3, 5) = 0
            //                              = Abs(0 - 5) = 5
            //      |
            //      B   nearest A = 0
            //          nearest C = 5
            //          current B = 3
            //          total = 5-0 = 5
            //
            //          |
            //          C   nearest A = 0, 10
            //              nearest B = 3
            //              current C = 5
            //              total = 0 -> 3 -> 5 = 5 || 3 -> 5 -> 10 = 7 --> 5
            //
            //              |
            //              B  nearest A = 10
            //                 nearest C = 12
            //                 current B = 9
            //                 total = 9 - 12 = 3                           <--- Solution
            //
            //                  |              
            //                  A   nearest B = 9
            //                      nearest C = 12
            //                      current A = 10
            //                      total = 9 - 12 = 3                      <--- Solution  All 3 are the solution since they are different letters of the alphabet
            //
            //                      |
            //                      C   nearest A = 10
            //                          nearest B = 9
            //                          current C = 12
            //                          total = 9 - 12 = 3                  <--- Solution
            //
            //  Minimum substring length = 3
            //  Starting = 9
            //  Ending = 12
            //  return: BANC

            //charMap stores the index of the items in the alphabet
            Dictionary<int, Item> charMap = new Dictionary<int, Item>();

            //alphabetMap stores the characters in the alphabet for O(1) lookup to find if a character is in the alphabet
            //It also stores the last index of the character
            Dictionary<char, int> alphabetMap = new Dictionary<char, int>();

            //charMapStack stores the order we find the items in the input so we can re-process them in reverse to find the closest
            //character that may appear after the character
            Stack<int> charMapStack = new Stack<int>();

            //For BigO notation, these values are used
            //a = length of alphabet
            //b = length of input string
            //c = number of alphabet letters in input string.  max is 'b' if only using alphabet chars

            // O(a)
            //Initalize the alphabetMap and set chars to -1 indicating no values have been seen
            for (int i = 0; i < alphabet.Length; i++)
            {
                if (alphabetMap.ContainsKey((alphabet[i])))
                    continue;
                alphabetMap.Add(alphabet[i], -1);
            }


            //Itterate over the input from left to right
            //If the character is in the alphabet
            //  1.  add this index position to the charMap hash
            //  2.  set the alphabetMap last seen location for this character as the current index
            //  3.  copy the last location for each character into the charMap.nearestChar hash map
            //  4.  push the current index number onto the charMapStack for the return trip in processing right to left
            //      we use this so we don't have to process every character on the return trip.  we only have to process
            //      those that are in the alphabet

            //O(b)
            for (int x = 0; x < input.Length; x++)
            {
                if (alphabetMap.ContainsKey(input[x]))
                {
                    charMap.Add(x, new Item(input[x], x));
                    alphabetMap[input[x]] = x; //set this char as nearest

                    //O(a)
                    foreach (var i in alphabetMap.Keys)
                    {
                        //Add all keys, even if it is -1
                        charMap[x].nearestChar.Add(i, alphabetMap[i]);
                    }

                    charMapStack.Push(x);
                }
            }

            //First pass is complete and the charMap.nearestChar is populated with the nearest character that occured before that
            //value in the index.  The next step is to go from right to left and populate the nearest characters going this direction

            //reset alphabetMap to -1 for return trip
            alphabetMap = new Dictionary<char, int>();
            //O(a)
            for (int i = 0; i < alphabet.Length; i++)
            {
                if (alphabetMap.ContainsKey((alphabet[i])))
                    continue;
                alphabetMap.Add(alphabet[i], -1);
            }

            //Store solution index as minIndex
            int minIndex = -1;

            //O(c)
            //Pop an item off the stack and process it until the stack is empty
            while (charMapStack.Count > 0)
            {
                //x is the key value inside charMap.  It is not the index of the value in the input string unless
                //the input string only contains characters from the alphabet
                int x = charMapStack.Pop();
                char c = charMap[x].C;
                int index = charMap[x].Index;   //index is the actual index value inside the input string

                //Initialize charMap
                charMap[x].Size = 0;    //stores the final size (max index - min index)
                charMap[x].MinChar = index; //init minChar to current index
                charMap[x].MaxChar = index; //init maxChar to current index

                //update alphabetMap with current character and index position
                alphabetMap[c] = index;

                //O(a)
                foreach (char v in alphabetMap.Keys)
                {
                    //Check to see if we have a value in the map
                    if (alphabetMap.ContainsKey(v) && alphabetMap[v] != -1)
                    {
                        //if our current nearest is -1, or the new value is < current value, update it
                        if (charMap[x].nearestChar[v] == -1 ||
                            alphabetMap[v] < charMap[x].nearestChar[v])
                        {
                            charMap[x].nearestChar[v] = alphabetMap[v];
                        }
                    }

                    //Check to see if this character is still -1
                    //If so, then this alphabet character was not found in the forward or reverse
                    //processing of the input, so the character does not exist.
                    //Return empty string for failure
                    if (charMap[x].nearestChar[v] == -1)
                    {
                        return "no solution";// string.Empty;
                    }

                    //At this point charMap.nearestChar[v] has been processed forwards and backwards and contains the closest
                    //character for character 'v'
                    //So, check to see if its index is the Min or Max for the nearest alphabet characters
                    charMap[x].MinChar = Math.Min(charMap[x].MinChar, charMap[x].nearestChar[v]);
                    charMap[x].MaxChar = Math.Max(charMap[x].MaxChar, charMap[x].nearestChar[v]);
                }

                //All chracters have been processed forwards and backwards and the Min and Max character is know
                //We can now calculate the size of the window using the Min and Max values
                charMap[x].Size = charMap[x].MaxChar - charMap[x].MinChar;

                //Check to see if we found a new minimum
                if (minIndex < 0)
                    minIndex = x;
                else if (charMap[x].Size < charMap[minIndex].Size)
                    minIndex = x;
            }

            //Finally find the min and max indexes in the solution set to get the start and end indexes for the answer
            int start = int.MaxValue;
            int end = int.MinValue;
            //O(a)
            foreach (var key in charMap[minIndex].nearestChar.Keys)
            {
                start = Math.Min(start, charMap[minIndex].nearestChar[key]);
                end = Math.Max(end, charMap[minIndex].nearestChar[key]);
            }

            string ret = input.Substring(start, end - start + 1);

            return ret;
        }


        public class Item
        {
            public char C { get; set; }
            public int Index { get; set; }

            public Dictionary<char, int> nearestChar { get; set; }

            public int MinChar { get; set; }
            public int MaxChar { get; set; }
            public int Size { get; set; }


            public Item(char c, int index)
            {
                C = c;
                Index = index;
                nearestChar = new Dictionary<char, int>();
            }

        }


    }
}