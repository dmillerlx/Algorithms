using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_Trees_FindCommonAncestor
    {

        //Create algorithm to find common ancestor of any two nodes in a tree.  assume not a BST and do not store addtional
        //data in the nodes (no parent pointers or other data)
        NewTree tree = new NewTree();

        #region Tree Elements
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
        #endregion

        #region Unused - Find single item in stack and get node list
        Stack<Node> foundStack = new Stack<Node>();
        //Finds a single item in the tree and populates 'foundStack' with result
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
        #endregion


        #region Book Solution
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

#endregion


        #region My Solution
        //Approach
        //1. Find parent stacks for both val1 and val2 in the tree
        //2. Reverse the stacks so the top element is at the search node
        //3. Adjust the levels of the stacks to be the same length by poping nodes off each stack
        //      While popping nodes off the stack, if the value for the other search item is found, we have found the common ancestor
        //      A node can be its own descedent, which means that if we are popping elements off stack2 and find search item val1,
        //      then val1 is in the parent chain for stack2...which means that the node for val1 is the common ancestor for both
        //      val1 and val2
        //
        //4.    If val1 or val2 is not in the parent stack for each others stack, then we pop nodes off each stack until the top nodes
        //      match.  This top matching node is the common ancestor
        public Node FindCommonAncestor(int val1, int val2)
        {
            Stack<Node> stack1 = null;
            Stack<Node> stack2 = null;

            //1. Get stack trace for each value in the tree
            if (!FindValuesInTree(tree.root, val1, out stack1, val2, out stack2))
                return null;

            //2. Reverse the stacks and pop elements off until they heights match
            Stack<Node> s1 = new Stack<Node>();
            Stack<Node> s2 = new Stack<Node>();

            while (stack1.Count > stack2.Count)
            {
                if ((int)stack1.Peek().Data == val2)        // <--  3. Check to see if val2 is a parent of val1 (in stack1).  If so, solution found
                    return stack1.Peek();
                stack1.Pop();
            }

            while (stack2.Count > stack1.Count)
            {
                if ((int)stack2.Peek().Data == val1)        // <--  3. Check to see if val1 is a parent of val2 (in stack2).  If so, solution found
                    return stack2.Peek();
                stack2.Pop();
            }

            //4. Solution not found yet, which means val1 and val2 are not parents of each other
            //Pop elements off each stack until we find a common ancestor
            //The matching top element is the common ancestor
            while (stack1.Peek() != stack2.Peek() && stack1.Count > 0 && stack2.Count > 0)
            {
                stack1.Pop();
                stack2.Pop();
            }


            if (stack1.Count > 0)
            {
                return stack1.Peek();                
            }

            //No solution found.  Should never happen until different tree's are searched
            return null;
        }

        //Performs in-order iterative transfersal of tree until it finds both 'val1' and 'val2'
        //Upon finding 'val1' or 'val2', the parent stack is coppied to 's1' and 's2' respectivly
        //When both values are found, the parent stack for each item is passed out
        public bool FindValuesInTree(Node root, int val1, out Stack<Node> s1, int val2, out Stack<Node> s2 )
        {
            Stack<Node> stack = new Stack<Node>();

            s1 = null;
            s2 = null;

            //Use hash table to track which nodes have been visited
            Dictionary<Node, bool> visited = new Dictionary<Node, bool>();

            Node currentNode = root;

            bool done = false;
            do
            {

                //Mark current node as visited
                if (visited.ContainsKey(currentNode) == false)
                    visited.Add(currentNode, true);
                
                //Check to see if current node is sitting on 'val1'
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

                //Check to see if current node is sitting on 'val2'
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

                //If parent stacks 's1' and 's2' are populated, exit
                if (s1 != null && s2 != null)
                    return true;
                else
                {
                    //Not done, so check to see if we have explored left or right.
                    //If we have not gone left or right, push the current node onto the stack and
                    //step left/right.
                    //
                    //When we can not go left or right, pop a value off the stack to go to the parent
                    //
                    //If we can't pop a parent off the stack, and can't go left/right, we are at the root
                    //and have failed to find both 'val1' and 'val2'.  Exit the loop and return false. 

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

        #endregion

        
        public void Run()
        {

            //                  5
            //          3               10
            //      2       4       8            12
            //                               11      13
            //                                           14


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


            Node ans = null;
            
            ans = FindCommonAncestor(11, 14);
            //ans = FindCommonAncestor(10, 11);
            //ans = FindCommonAncestor(8, 12);
            if (ans != null)
                Console.WriteLine("Common ancestor: " + ans.Data);
            else
                Console.WriteLine("No common ancestor");

            ans = commonAncestor(tree.root, 11, 14);
            //ans = commonAncestor(tree.root, 10, 11);
            //ans = commonAncestor(tree.root, 8, 12);
            if (ans != null)
                Console.WriteLine("Common Ancestor: " + ans.Data);
            else Console.WriteLine("No common ancestor");

           
        }


    }
}
