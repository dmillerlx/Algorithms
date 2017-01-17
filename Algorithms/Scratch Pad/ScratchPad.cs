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
            for (int x=0; x < input.Length; x++)
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
            for (int x= startBest; x <= endBest; x++)
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
        
        public static int FindAmazingNumberOffset(int []input)
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
            for (int index=0; index < input.Length; index++)
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
            for (int i=0; i < input.Length; i++)
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
        public static char[]ReplaceSpaces(char []input, int len)
        {
            int numerOfSpaces = 0;
            for (int x=0; x < len; x++)
            {
                if (input[x] == ' ')
                    numerOfSpaces++;
            }

            //%20 is 3 chars, but only using 2 extra since the space is already using 1 char
            int end = len + numerOfSpaces * 2 -1; //13 + 2*2 -1 = 13+4-1 = 16
            
            for (int x= len-1; x >= 0; x--)
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

        public static int []SortSquaresOfIntegers(int []vals)
        {
            //-10, -5, -1, 1, 3, 5, 10

            //100, 25, 1, 1, 9, 25, 100

            //Create two lists based for positive and negative numbers
            //then merge sort the output array

            int[] one = new int[vals.Length]; //for negative squares
            int[] two = new int[vals.Length];
            int oneCount = 0;
            int twoCount = 0;
            for (int x=0; x < vals.Length; x++)
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

            int oneIndex = oneCount-1;
            int twoIndex = 0;
            for (int x=0; x < ret.Length; x++)
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

        public static bool HasContigousSubArraySum(int []arr, int target)
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



        public class MyPoint: IComparable
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

        public static MyPoint FindClosestKPoint(MyPoint []points, int k)
        {
            if (points == null)
                return null;

            if (points.Length == 0)
                return null;

            Heap<MyPoint> pointHeap = new Heap<ScratchPad.MyPoint>(Heap<ScratchPad.MyPoint>.heapTypeEnum.min);

            for(int x=0; x < points.Length; x++)
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
            for(int x=0; x < input.Length; x++)
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
            for (int x= 0; x < input.Length; x++)
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

            for(int x=0; x < val.Length; x++)
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
        public static int FindMaximumDrop(int []vals)
        {
            //1, 5, 10, 3, 7, 15, 8, 9, 4, 3, 1, 3

            int maxDrop = -1;
            int localMax = int.MinValue;
            int localMin = int.MaxValue;
            for (int x=0; x < vals.Length; x++)
            {
                if (x==0)
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

        public static void Reorder(ref char []A, ref int []B)
        {
            for (int x=0; x < A.Length; )
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

    }

}