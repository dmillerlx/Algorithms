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
        /*
        public static int FindAmazingNumberOffset(int []input)
        {
            Dictionary<int, int> amazingRotations = new Dictionary<int, int>();

            for (int x=0; x < input.Length; x++)
            {
                if (input[x] >= input.Length)
                {
                    //Number can never be amazing, so ignore
                    continue;
                }
                int start = 0;
                int end = 0;
                if (input[x] <= x)
                {
                    start = x;
                    
                    if (input[x] < x)
                    {
                        start = input[x];
                    }


                    end = input.Length;
                }
                else
                {
                    start = 
                }


                int start = (x + 1) % nameof;// input[x]-1;
                int end = 

                int diff = x - input[x];

                if (amazingRotations.ContainsKey(diff))
                {
                    amazingRotations[diff] = amazingRotations[diff] + 1;
                }
            }

        }
        */

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
    }

}