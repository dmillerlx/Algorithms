using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_Microsoft_Tree_Find_Precedessor_and_Successor
    {
        //Given a key in a BST, find the inorder successor and predecessor


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

            public Node(object data, Node parent)
            {
                Data = data;
                this.parent = parent;
            }

            public Node parent = null;


            public int level = 0;

            public override string ToString()
            {
                return ((int)Data).ToString();
            }
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
                                currentNode.left.parent = currentNode; //add parent
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
                                currentNode.right.parent = currentNode; // add parent
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


            public void Insert(object val)
            {
                root = InsertValue(root, val);
            }


            public Node FindNextNext(Node node)
            {
                //Next node is either
                //if (node.right != null)
                //      Advance to node.right, then advance to node.left until node.left is null
                //else if (node.right == null)
                //   Advance to node.parent until node.parent is greater than current node.
                //   that node is the solution
                //   If node.parent is null before node.parent is greater than current node, no solution exists

                Node runner = null;
                if (node.right != null)
                {
                    runner = node.right;
                    while (runner.left != null)
                        runner = runner.left;

                    return runner;
                }
                else if (node.parent != null)
                {
                    Node runnerCurrent = node;
                    runner = node.parent;

                    //Run runner up the parent list while the node is on the right side                    
                    while (runner != null && runnerCurrent == runner.right)
                    {
                        runnerCurrent = runner;
                        runner = runner.parent;
                    }

                    //while (((int)runner.Data <= (int)node.Data) && runner.parent != null)
                    //{
                    //    runner = runner.parent;
                    //}
                    //if (((int)runner.Data > (int)node.Data))
                    //    return runner;

                    return runner;
                }

                return null;
            }


            public void FindSuccessorAndPredecessor(int value)
            {
                //Find in order successor and predecessor
                //without using parent pointers

                Console.WriteLine("Searching for " + value);

                //First find the node in the tree.  Do iterativly so we have a stack pointer to the parent nodes

                Stack<Node> stack = new Stack<Node>();

                Node current = root;

                while ((int)current.Data != value && current != null)
                {
                    stack.Push(current);
                    if (value < (int)current.Data)
                    {
                        current = current.left;
                    }
                    else
                    {
                        current = current.right;
                    }
                }

                if (current == null)
                {
                    Console.WriteLine("Value not found");
                    return;
                }

                //Current is now on the node to find, so find the in order successor and predecessor

                Node foundNode = current;
                
                //Predecessor
                Node pred = null;
                Node runner = foundNode;
                current = foundNode;
                if (runner.left != null)
                {
                    runner = runner.left;
                    while (runner.right != null)
                        runner = runner.right;
                    pred = runner;
                }
                else
                {
                    //copy parent stack
                    Stack<Node> predStack = new Stack<Node>(stack);

                    runner = predStack.Pop();
                    if (predStack.Count > 0)
                        runner = predStack.Pop();
                    else runner = null;

                    while (runner != null && runner.right != current)
                    {
                        current = runner;
                        if (predStack.Count > 0)
                            runner = predStack.Pop();
                        else runner = null;
                    }

                    pred = runner;  //pred could be null if no predecessor exists
                }

                //Successor
                Node succ = null;
                runner = foundNode;
                current = foundNode;
                if (runner.right != null)
                {
                    runner = runner.right;
                    while (runner.left != null)
                        runner = runner.left;
                    succ = runner;
                }
                else
                {
                    //copy parent stack
                    Stack<Node> succStack = new Stack<Node>(stack);

                    runner = succStack.Pop();
                    if (succStack.Count > 0)
                        runner = succStack.Pop();
                    else runner = null;

                    while (runner != null && runner.left != current)
                    {
                        current = runner;
                        if (succStack.Count > 0)
                            runner = succStack.Pop();
                        else runner = null;
                    }

                    succ = runner;  //succ could be null if no successor exists
                }

                Console.WriteLine("Predecessor: " + ((pred == null) ? "None" : ((int)pred.Data).ToString()));
                Console.WriteLine("Successor: " + ((succ == null) ? "None" : ((int)succ.Data).ToString()));
            
            }

            

        }

        public void Run()
        {
            NewTree tree = new NewTree();

            tree.root = new Node(5);

            tree.root.left = new Node(3, tree.root);
            tree.root.left.left = new Node(2, tree.root.left);
            tree.root.left.right = new Node(4, tree.root.left);

            tree.root.right = new Node(10, tree.root);
            tree.root.right.left = new Node(8, tree.root.right);
            tree.root.right.right = new Node(12, tree.root.right);

            tree.root.right.right.left = new Node(11, tree.root.right.right);
            tree.root.right.right.right = new Node(13, tree.root.right.right);

            tree.root.right.right.right.right = new Node(14, tree.root.right.right.right);

            tree.PrintTree();

            Console.WriteLine();
            Console.WriteLine();


            tree.FindSuccessorAndPredecessor(10);
            tree.FindSuccessorAndPredecessor(13);
            tree.FindSuccessorAndPredecessor(14);
            tree.FindSuccessorAndPredecessor(2);
            tree.FindSuccessorAndPredecessor(3);



        }
    }
}
