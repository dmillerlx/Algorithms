using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_Trees_FindCommonAncestor
    {

        //Create algorithm to find common ancestor of any two nodes in a tree.  assume not a BST and do not store addtional
        //data in the nodes 


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


        }

        //Implementation O(n Log n)
        //NewTree tree = new NewTree();
        public void InsertArray(int[] array, int start, int end)
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


        //Do in order transversal
        private void CheckAllNodes(Node root)
        {
            if (root == null)
                return;
            CheckAllNodes(root.left);

            Node nextNode = tree.FindNextNext(root);
            if (nextNode != null)
                Console.WriteLine(root.Data + "> Next: " + nextNode.Data);
            else
                Console.WriteLine(root.Data + "> Next: NULL");

            CheckAllNodes(root.right);

        }


        public void FindCommonAncestor (int val1, int val2)
        {
            Stack<Node> stack1 = null;
            Stack<Node> stack2 = null;
            //foundStack = new Stack<Node>();
            //if (FindValue(tree.root, val1))
            //    stack1 = foundStack;
            //else return;

            //foundStack = new Stack<Node>();
            //if (FindValue(tree.root, val2))
            //    stack2 = foundStack;
            //else return;

            if (!FindValue2(tree.root, val1, out stack1, val2, out stack2))
                return;

            //Reverse the stacks and pop elements off until they heights match
            Stack<Node> s1 = new Stack<Node>();
            Stack<Node> s2 = new Stack<Node>();

            //while (stack1.Count > 0)
            //    s1.Push(stack1.Pop());

            //while (stack2.Count > 0)
            //    s2.Push(stack2.Pop());

            //while (s1.Count > s2.Count)
            //    s1.Pop();

            //while (s2.Count > s1.Count)
            //    s2.Pop();

            while (stack1.Count > stack2.Count)
                stack1.Pop();

            while (stack2.Count > stack1.Count)
                stack2.Pop();


            while (stack1.Peek() != stack2.Peek())
            {
                stack1.Pop();
                stack2.Pop();
            }


            if (stack1.Count > 0)
            {
                Console.WriteLine("Common ancestor: " + stack1.Peek().Data);
            }

            

        }

        //public Stack<Node> FindValue(int val)
        //{

        //    Stack<Node>

        //    Stack<Node> stack = new Stack<Node>();
        //    stack.Push(tree.root);

        //    while (stack.Count > 0)
        //    {
        //        Node n = stack.Pop();

        //        if ((int)n.Data == val)
        //            return stack;

        //        stack.Push(n.left);
        //        stack.Push(n.right);
        //    }

        //}


        bool covers(Node root, int val)
        {
            if (root == null) return false;
            if ((int)root.Data == val) return true;
            return covers(root.left, val) || covers(root.right, val);
        }

        Node commonAncestorHelper (Node root, int val1, int val2)
        {
            if (root == null) return null;

            if ((int)root.Data == val1 || (int)root.Data == val2) return root;

            bool is_Val1_On_Left = covers(root.left, val1);
            bool is_Val2_On_Right = covers(root.right, val2);

            //val1 and val2 on different sides, so at common ancestor
            if (is_Val1_On_Left && is_Val2_On_Right) return root;

            //Not on different sides so walk down side they are on
            Node side = is_Val1_On_Left ? root.left : root.right;

            return commonAncestorHelper(side, val1, val2);
        }

        Node commonAncestor(Node root, int val1, int val2)
        {
            if (!covers(root, val1) || !covers(root, val2))
            {
                return null;
            }
            return commonAncestorHelper(root, val1, val2);
        }




        Stack<Node> foundStack = new Stack<Node>();
        public bool FindValue(Node root, int val)
        {
            if (root == null) return false;

            if ((int)root.Data == val)
            {
                foundStack.Push(root);
                return true;
            }

            if (FindValue(root.left, val))
            {
                foundStack.Push(root);
                return true;
            }
            if (FindValue(root.right, val))
            {
                foundStack.Push(root);
                return true;
            }

            return false;    
        }

        public bool FindValue2(Node root, int val1, out Stack<Node> s1, int val2, out Stack<Node> s2 )
        {
            Stack<Node> stack = new Stack<Node>();

            s1 = null;
            s2 = null;

            Dictionary<Node, bool> visited = new Dictionary<Node, bool>();

            //stack.Push(root);

            Node currentNode = root;

            bool done = false;
            do
            {

                if (visited.ContainsKey(currentNode) == false)
                    visited.Add(currentNode, true);
                
                if (s1 == null && (int)currentNode.Data == val1)
                {
                    //found val1
                    
                    //copy stack out
                    Stack<Node> tmp = new Stack<Node>();
                    while (stack.Count > 0)
                        tmp.Push(stack.Pop());

                    s1 = new Stack<Node>();
                    while (tmp.Count > 0)
                    {
                        stack.Push(tmp.Peek());
                        s1.Push(tmp.Pop());
                    }
                }

                if (s2 == null && (int)currentNode.Data == val2)
                {
                    //found val2

                    //copy stack out
                    Stack<Node> tmp = new Stack<Node>();
                    while (stack.Count > 0)
                        tmp.Push(stack.Pop());

                    s2 = new Stack<Node>();
                    while (tmp.Count > 0)
                    {
                        stack.Push(tmp.Peek());
                        s2.Push(tmp.Pop());
                    }
                }

                if (s1 != null && s2 != null)
                    return true;
                else
                {
                    if (currentNode.left != null && visited.ContainsKey(currentNode.left) == false)
                    {
                        stack.Push(currentNode);
                        currentNode = currentNode.left;
                    }
                    else if (currentNode.right != null && visited.ContainsKey(currentNode.right) == false)
                    {
                        stack.Push(currentNode);
                        currentNode = currentNode.right;
                    }
                    else if (stack.Count > 0)
                    {
                        currentNode = stack.Pop();
                    }
                    else
                    {
                        done = true;
                    }
                }

            } while (!done);

            return false;
        }


        NewTree tree = new NewTree();
        public void Run()
        {

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


            //CheckAllNodes(tree.root);
            //Console.WriteLine("Next successor of Is BST: " + tree.IsBST());
            
            //FindCommonAncestor(11, 14);

            Node ans = commonAncestor(tree.root, 10, 11);
            if (ans != null)
                Console.WriteLine("Common Ancestor: " + ans.Data);
            else Console.WriteLine("No common ancestor");


            return;
            tree = new NewTree();
            int n = 1050;
            int[] array = new int[n];
            for (int x = 0; x < n; x++)
                array[x] = x;

            Console.WriteLine("Method 1 - using insert node - O(n Log n)");
            InsertArray(array, 0, array.Length - 1);
            tree.PrintTree();
            CheckAllNodes(tree.root);

        }


    }
}
