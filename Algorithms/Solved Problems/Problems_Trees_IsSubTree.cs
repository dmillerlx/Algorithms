using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_Trees_IsSubTree
    {

        public class Node
        {
            public object Data;
            public Node left = null;
            public Node right = null;

            public Node() { }

            public Node(object data)
            {
                Data = data;
            }

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


            public bool IsBST()
            {
                lastNode = null;
                return IsBST(root);
            }

            Node lastNode = null;

            //Do in order transversal
            private bool IsBST(Node root)
            {
                if (root == null)
                    return true;
                if (!IsBST(root.left))
                    return false;

                if (lastNode == null)
                {
                    lastNode = root;
                    Console.WriteLine(root.Data);
                }
                else
                {
                    if ((int)lastNode.Data > (int)root.Data)
                    {
                        Console.WriteLine("Found item out of order: " + root.Data);
                        return false;
                    }
                    lastNode = root;
                    Console.WriteLine(root.Data);
                }

                if (!IsBST(root.right))
                    return false;

                return true;
            }


        }

        Node FindStart(Node root, Node findNode)
        {
            if (root == null) return null;

            if ((int)root.Data == (int)findNode.Data)
                return root;

            Node ret = FindStart(root.left, findNode);
            if (ret == null)
                ret = FindStart(root.right, findNode);

            return ret;
        }

        bool TreesMatch(Node root, Node rootb)
        {
            if (root == null) return false;

            if ((int)root.Data != (int)rootb.Data)
                return false;

            bool ret = true;
            if (rootb.left != null)
                ret = TreesMatch(root.left, rootb.left);

            if (ret != false)
            {
                if (rootb.right != null)
                    ret = TreesMatch(root.right, rootb.right);
            }

            return ret;
        }

        public bool IsSubTree(Node roota, Node rootb)
        {
            if (roota == null || rootb == null) return false;

            Node start = FindStart(roota, rootb);

            if (start == null) return false;

            //Found start, now trace to see if the trees match

            return TreesMatch(start, rootb);

        }


        public void Run()
        {
            NewTree tree = new NewTree();
            tree.root = new Node(5);
            tree.root.left = new Node(3);
            tree.root.left.left = new Node(2);
            tree.root.left.right = new Node(4);

            tree.root.right = new Node(10);
            tree.root.right.left = new Node(8);
            tree.root.right.right = new Node(12);
            tree.root.right.right.left = new Node(11);
            tree.root.right.right.right = new Node(13);
            tree.root.right.right.right.right = new Node(14);

            NewTree tree2 = new NewTree();
            tree2.root = new Node(12);
            tree2.root.left = new Node(11);
            tree2.root.right = new Node(13);
            tree2.root.right.right = new Node(14);
            tree2.root.right.right.right = new Node(15);


            Console.WriteLine("Is tree 2 a subtree: " + IsSubTree(tree.root, tree2.root));



        }




    }
}
