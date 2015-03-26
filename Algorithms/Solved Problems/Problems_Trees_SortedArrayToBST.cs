using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_Trees_SortedArrayToBST
    {
        //Given a sorted (increasing order) array of unique integer elements, write an algorithm to create a BST with minimal height

        public class Node
        {
            public object Data;
            public Node left = null;
            public Node right = null;

            public int level = 0;
        }


        public class NewTree
        {
            public Node root = null;

            public Node InsertValue(Node root, object val)
            {
                Node currentNode = null;
                bool foundInsertionLocation = false;
                if (root == null)
                {
                    root = new Node();
                    currentNode = root;
                }
                else
                {
                    currentNode = root;
                    do
                    {
                        if ((int)val <= (int)(currentNode.Data))
                        {
                            if (currentNode.left != null)
                            {
                                currentNode = currentNode.left;
                            }
                            else
                            {
                                currentNode.left = new Node();
                                currentNode = currentNode.left;
                                foundInsertionLocation = true;
                            }
                        }
                        else
                        {
                            if (currentNode.right != null)
                            {
                                currentNode = currentNode.right;
                            }
                            else
                            {
                                currentNode.right = new Node();
                                currentNode = currentNode.right;
                                foundInsertionLocation = true;
                            }
                        }
                    }
                    while (!foundInsertionLocation);
                }

                currentNode.Data = val;

                return root;
            }

            public void Insert(object val)
            {
                root = InsertValue(root, val);
            }

            Dictionary<Node, int> visited = new Dictionary<Node, int>();

            public bool IsBalanced()
            {
                visited = new Dictionary<Node, int>();
                //BFS Transversal to check each level for balanced.

                //Queue nodes for BFS transversal and when detecting
                //a level change, inspect each element on the queue to see if there is
                //a null leaf.  If so, mark the first level to have a null as the queued node + 1.
                //Next continue the BFS transfersal and fail out if you encounter a leaf a node that is
                //greater than the node that had a null.

                Queue<Node> queue = new Queue<Node>();

                visited.Add(root, 0);

                queue.Enqueue(root);

                int lastLevel = -1;
                //bool foundNullLevel = false;
                int nullLevel = -1;

                while (queue.Count > 0)
                {
                    Node currentNode = queue.Peek();
                    int currentLevel = visited[currentNode];

                    if (currentLevel > lastLevel)
                    {

                        //Check to see if we have a null leaf
                        if (nullLevel >= 0 && currentLevel > nullLevel)
                        {
                            //Check to see if the current level is beyond the null leaf level
                            if (nullLevel < currentLevel)//visited[queuedNode])
                            {
                                Console.WriteLine("Unbalanced at level (" + visited[currentNode] + ") with data (" + currentNode.Data + ")");
                                return false;
                            }
                        }


                        //Check all elements in the queue to see if their children have any null flags
                        //If so, mark that l
                        Queue<Node> tempQueue = new Queue<Node>();

                        while (queue.Count > 0)
                        {
                            Node queuedNode = queue.Dequeue();

                            if (queuedNode.left == null || queuedNode.right == null)
                            {
                                //foundNullLevel = true;
                                if (nullLevel < 0)
                                    nullLevel = visited[queuedNode] + 1;
                            }

                            tempQueue.Enqueue(queuedNode);
                        }

                        queue = tempQueue;
                    }

                    //Remove item from queue
                    queue.Dequeue();

                    if (currentNode.left != null)
                    {
                        visited.Add(currentNode.left, currentLevel + 1);
                        queue.Enqueue(currentNode.left);
                    }

                    if (currentNode.right != null)
                    {
                        visited.Add(currentNode.right, currentLevel + 1);
                        queue.Enqueue(currentNode.right);
                    }


                    lastLevel = currentLevel;

                    //Console.WriteLine("Level: " + visited[currentNode] + " Data: " + currentNode.Data);
                }

                return true;
            }


            public void PrintTree()
            {
                printTree(root);
            }

            void printTree(Node root)
            {
                //Print nothing if the tree is empty
                if (root == null)
                    return;

                //Doing BFS tree transversal
                Queue<Node> nodeQueue = new Queue<Node>();

                //Enqueue the root node, then process until queue is empty
                nodeQueue.Enqueue(root);

                //Record the last level processed so we know when the level has changed
                //and a new line char needs to be inserted
                int lastLevel = 0;

                //Add check for first item to prevent printing a comma before the first item
                bool firstItem = true;

                while (nodeQueue.Count > 0)
                {
                    Node currentNode = nodeQueue.Dequeue();

                    //Set child levels to current level + 1
                    //Enqueue the children nodes
                    if (currentNode.left != null)
                    {
                        currentNode.left.level = currentNode.level + 1;
                        nodeQueue.Enqueue(currentNode.left);
                    }

                    if (currentNode.right != null)
                    {
                        currentNode.right.level = currentNode.level + 1;
                        nodeQueue.Enqueue(currentNode.right);
                    }


                    if (currentNode.level != lastLevel)
                        Console.WriteLine();    //Level has changed so add new line char
                    else
                    {
                        if (!firstItem)             //If this is the first item, don't print a comma
                            Console.Write(",");     //Same level, so add a comma deliminter
                    }
                    firstItem = false;

                    //Now write out the node information
                    Console.Write(currentNode.Data);

                    lastLevel = currentNode.level;  //Update lastLevel to current level
                }
            }

        }

        //Implementation O(n Log n)
        NewTree tree = new NewTree();
        public void InsertArray(int []array, int start, int end)
        {
            if (start == end)
            {
                tree.Insert(array[start]);
            }
            else if ((end - start) == 1)
            {
                tree.Insert(array[start]);
                tree.Insert(array[end]);
            }
            else
            {
                int mid = start + ((end - start) / 2);
                tree.Insert(array[mid]);
                InsertArray(array, start, mid - 1);
                InsertArray(array, mid + 1, end);
            }
        }

        //Book implementation - O(n)
        NewTree tree2 = new NewTree();
        public Node CreateMinimalBST(int []array, int start, int end)
        {
            if (end < start)
                return null;

            int mid = (start + end)/2;
            Node n = new Node() { Data = array[mid] };
            n.left = CreateMinimalBST(array, start, mid - 1);
            n.right = CreateMinimalBST(array, mid + 1, end);

            return n;
        }


        public void Run()
        {
            int[] array = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

            int n = 1050;
            array = new int[n];
            for (int x = 0; x < n; x++)
                array[x] = x;

            Console.WriteLine("Method 1 - using insert node - O(n Log n)");
            InsertArray(array, 0, array.Length - 1);
            tree.PrintTree();

            Console.WriteLine();
            Console.WriteLine("Tree is balanced: " + tree.IsBalanced());


            Console.WriteLine("Method 2 - creating small tree nodes - O(n)");
            tree2.root = CreateMinimalBST(array, 0, array.Length - 1);

            tree2.PrintTree();

            Console.WriteLine();
            Console.WriteLine("Tree is balanced: " + tree2.IsBalanced());


        }

    }
}
