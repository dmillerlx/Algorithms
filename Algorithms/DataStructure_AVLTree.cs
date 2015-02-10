using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    class DataStructure_AVLTree
    {    
        
        /// <summary>
        /// This class implements an AVL Binary Search Tree
        ///     In an AVL tree, the heights of the two child subtrees of any node differ by at most one; if at any 
        ///	    time they differ by more than one, rebalancing is done to restore this property. 
        ///	    
        ///     		Average	Worst case
		///		Space	O(n)		O(n)
		///		Search	O(log n)	O(log n)
		///		Insert	O(log n)	O(log n)
		///		Delete	O(log n)	O(log n)
        /// 
        ///     Some code was ported from an example on this site:
        ///     http://www.geeksforgeeks.org/avl-tree-set-1-insertion/
        ///     http://www.geeksforgeeks.org/avl-tree-set-2-deletion/
        /// 
        ///     For verification, this class also implements Bredeath First and Depth First 
        ///     tree transversal.  As such, the AVLNode class has additional fields to support these algorithms.
        ///         bool Visited        Used to mark that a Node was visited
        ///         int Level           Used to mark the level of the Node
        ///         string Direction    Records the pointer direction (Left/Right) during the Breadith First transversal.
        ///                                 This is only used for debugging and implementation verification
        /// 
        ///     This tree does NOT store duplicate values
        /// </summary>
        /// <typeparam name="I"></typeparam>
   

        public class AVLTree<I> where I : IComparable
        {
            public class AVLNode: IComparable
            {
                public AVLNode()
                {

                }                              

                public AVLNode(I newData, AVLNode left, AVLNode right)
                {
                    this.Data = newData;
                    this.Left = left;
                    this.Right = right;

                    Height = 1;
                }

                public I Data { get; set; }             //Data field
                public int Height { get; set; }         //Height property is used to record the balancing height of the node and sub trees


                public AVLNode Left { get; set; }
                public AVLNode Right { get; set; }

                public bool Visited { get; set; }       //Marks that the node was visited during a search
                public string Direction { get; set; }   //Records the pointer direction during a Breadith First transversal for tree verification
                public int Level { get; set; }          //Records the level of the node during a Breadith First Transversal

                //Support all datatypes that implement IComparable
                public int CompareTo(object obj)
                {
                    return Data.CompareTo((I)obj);
                }
            }
            
            AVLNode root;

            public AVLTree()
            {
                root = null;    //Initalize root to NULL
            }

            
            private AVLNode findMin(AVLNode t)
            {
                if (t == null)
                    return null;
                else if (t.Left == null)
                    return t;
                return findMin(t.Left);
            }

            private AVLNode findMax(AVLNode t)
            {
                if (t != null)
                    while (t.Right!=null)
                        t = t.Right;
                return t;
            }


            public AVLNode find(I val, AVLNode t)
            {
                if (t == null)
                    return null;
                if (val.CompareTo(t.Data) < 0)
                    return find(val, t.Left);
                else if (val.CompareTo(t.Data) > 0)
                    return find(val, t.Right);
                else 
                    return t; //match
            }


            public bool find(I findData)
            {
                if (find(findData, root) != null)
                    return true;
                return false;
            }

            public bool isEmpty()
            {
                return root == null;
            }

            public void PrintTree()
            {
                if (isEmpty())
                    Console.WriteLine("Empty Tree");
                else
                    PrintTree(root);
            }

            private void PrintTree(AVLNode t)
            {                
                if (t == null) return;
                PrintTree(t.Left);
                Console.WriteLine(t.Height + ": " + t.Data.ToString());
                PrintTree(t.Right);
            }



            #region  Breadith First and Depth First functions

            /// <summary>
            /// This class wrapped the AVLNode object into an AVLNodeExt object that supports
            /// the extra data elements required for Breadth-First transversal
            /// The extra data elements are: level, visited
            /// </summary>
            public class AVLNodeExt
            {
                public AVLNodeExt(AVLNode n, int l)
                {
                    this.node = n;
                    this.level = l;
                }

                public AVLNode node;
                public int level;
            }

            //These BFD functions wrap the AVLNode in the AVLNodeExt object above
            //This AVLNodeExt object supplies the extra 
            public int FindValueDepth_BFD_WrappedAVLNode(AVLNode root, I val)
            {
                ResetVisited_BFD(root);

                Queue<AVLNodeExt> nodeQueue = new Queue<AVLNodeExt>();

                nodeQueue.Enqueue(new AVLNodeExt(root, 0));

                while (nodeQueue.Count > 0)
                {
                    AVLNodeExt currentNode = nodeQueue.Peek();

                    if (currentNode.node.Data.CompareTo(val) == 0)
                    {
                        return currentNode.level;
                    }


                    if (currentNode.node.Left != null && currentNode.node.Left.Visited == false)
                    {
                        nodeQueue.Enqueue(new AVLNodeExt(currentNode.node.Left, currentNode.level + 1));
                    }
                    if (currentNode.node.Right != null && currentNode.node.Right.Visited == false)
                    {
                        nodeQueue.Enqueue(new AVLNodeExt(currentNode.node.Right, currentNode.level + 1));
                    }

                    currentNode.node.Visited = true;

                    nodeQueue.Dequeue();
                }

                return 0;
            }

            public int FindValueDepth_BFD_WrappedAVLNode(I val)
            {
                return FindValueDepth_BFD_WrappedAVLNode(root, val);
            }

            //These BFD functions iterate over the AVL Tree in Breadth-First order
            //These functions use data elements added to the AVLNode class (visited, level)
            public int FindValueDepth_BFD(I val)
            {
                return FindValueDepth_BFD(root, val);
            }

            public int FindValueDepth_BFD(AVLNode root, I val)
            {
                ResetVisited_BFD(root);

                Queue<AVLNode> nodeQueue = new Queue<AVLNode>();

                root.Level = 0;
                nodeQueue.Enqueue(root);

                while (nodeQueue.Count > 0)
                {
                    AVLNode currentNode = nodeQueue.Peek();

                    if (currentNode.Data.CompareTo(val) == 0)
                    {
                        return currentNode.Level;
                    }


                    if (currentNode.Left != null && currentNode.Left.Visited == false)
                    {
                        currentNode.Left.Level = currentNode.Level + 1;
                        nodeQueue.Enqueue(currentNode.Left);//, currentNode.level + 1));
                    }
                    if (currentNode.Right != null && currentNode.Right.Visited == false)
                    {
                        currentNode.Right.Level = currentNode.Level + 1;
                        nodeQueue.Enqueue(currentNode.Right);//, currentNode.level + 1));
                    }

                    currentNode.Visited = true;

                    nodeQueue.Dequeue();
                }

                return 0;
            }
            
            public AVLNode FindValue_BFD(AVLNode root, object val)
            {
                ResetVisited_BFD(root);

                Queue<AVLNode> nodeQueue = new Queue<AVLNode>();

                nodeQueue.Enqueue(root);

                while (nodeQueue.Count > 0)
                {
                    AVLNode currentNode = nodeQueue.Peek();

                    if (currentNode.Data.CompareTo(val) == 0)
                    {
                        return currentNode;
                    }

                    if (currentNode.Left != null && currentNode.Left.Visited == false)
                    {
                        nodeQueue.Enqueue(currentNode.Left);
                    }
                    if (currentNode.Right != null && currentNode.Right.Visited == false)
                    {
                        nodeQueue.Enqueue(currentNode.Right);
                    }

                    //Debug.WriteLine((int)currentNode.Data);
                    currentNode.Visited = true;

                    nodeQueue.Dequeue();
                }

                return null;
            }

            /// <summary>
            /// Before doing a Breadith First transversal we need to reset the 'visited' flag on each node.
            /// This function iterates across the entire tree and resets the flag to false.
            /// </summary>
            /// <param name="root"></param>
            public void ResetVisited_BFD(AVLNode root)
            {
                if (root == null) return;

                root.Visited = false;
                ResetVisited_BFD(root.Left);
                ResetVisited_BFD(root.Right);
            }
            
            /// <summary>
            /// Prints the entire tree in Breadith First order
            /// </summary>
            /// <param name="root"></param>
            public void PrintTree_BFD(AVLNode root)
            {
                //Reset visited flag on all nodes
                ResetVisited_BFD(root);

                //Create queue to track nodes to visit next
                Queue<AVLNode> nodeQueue = new Queue<AVLNode>();

                //Initialize the root level to 0
                root.Level = 0;

                //Enqueue the root element since we only process nodes in the queue
                //We do this since we do not queue nodes that have been visited, thus preventing
                //endless loops from circular references
                nodeQueue.Enqueue(root);

                //Work until the queue is empty
                while (nodeQueue.Count > 0)
                {
                    //Get the head of the queue
                    AVLNode currentNode = nodeQueue.Peek();

                    //Check left child.  
                    //If it exists and we have NOT visited it, set the childs level to current +1
                    //and then queue the left child
                    if (currentNode.Left != null && currentNode.Left.Visited == false)
                    {
                        currentNode.Left.Direction = "L";  //Debugging only to verify AVL tree
                        currentNode.Left.Level = currentNode.Level + 1;
                        nodeQueue.Enqueue(currentNode.Left);
                    }

                    //Check right child.  
                    //If it exists and we have NOT visited it, set the childs level to current +1
                    //and then queue the right child
                    if (currentNode.Right != null && currentNode.Right.Visited == false)
                    {
                        currentNode.Right.Direction = "R";  //Debugging only to verify AVL tree
                        currentNode.Right.Level = currentNode.Level + 1;
                        nodeQueue.Enqueue(currentNode.Right);
                    }

                    //Print current Node
                    Console.WriteLine(currentNode.Direction+"/"+currentNode.Level + "/"+currentNode.Height+": " + currentNode.Data);

                    //Mark current node as visited
                    currentNode.Visited = true;

                    //Finally remove node fro queue  (could have done this first instead of using peek)
                    nodeQueue.Dequeue();

                }

            }

            public void PrintTree_BFD()
            {
                PrintTree_BFD(root);
            }


            public void PrintTree_DFD(AVLNode root)
            {
                //Reset visited flag on all nodes
                ResetVisited_BFD(root);

                //Create queue to track nodes to visit next
                Stack<AVLNode> nodeStack = new Stack<AVLNode>();

                //Initialize the root level to 0
                root.Level = 0;

                //Push root node onto the stack.
                //We will process nodes until the stack is empty                
                nodeStack.Push(root);

                //Work until the queue is empty
                while (nodeStack.Count > 0)
                {
                    //Get the head of the stack
                    AVLNode currentNode = nodeStack.Pop();


                    //We want to travel down the left side of the tree first, so we push the right
                    //side onto the stack first.  This way the left will be popped before the right
                    //and get processed first

                    //Check right child.  
                    //If it exists and we have NOT visited it, set the childs level to current +1
                    //and then push the right child onto the stack
                    if (currentNode.Right != null && currentNode.Right.Visited == false)
                    {
                        currentNode.Right.Direction = "R";  //Debugging only to verify AVL tree
                        currentNode.Right.Level = currentNode.Level + 1;
                        nodeStack.Push(currentNode.Right);
                    }

                    //Check left child.  
                    //If it exists and we have NOT visited it, set the childs level to current + 1
                    //and then push the left child onto the stack
                    if (currentNode.Left != null && currentNode.Left.Visited == false)
                    {
                        currentNode.Left.Direction = "L";  //Debugging only to verify AVL tree
                        currentNode.Left.Level = currentNode.Level + 1;
                        nodeStack.Push(currentNode.Left);
                    }


                    //Print current Node
                    Console.WriteLine(currentNode.Direction + "/" + currentNode.Level + "/" + currentNode.Height + ": " + currentNode.Data);

                    //Mark current node as visited
                    currentNode.Visited = true;


                }

            }

            public void PrintTree_DFD()
            {
                PrintTree_DFD(root);
            }



            #endregion

            #region AVL Functions

            //Helper to return the height of a node
            int height(AVLNode n)
            {
                if (n == null)
                    return 0;
                return n.Height;
            }

            //Helper to return the max of two numbers.  Used when examining the heights of trees
            int max(int a, int b)
            {
                return Math.Max(a, b);
            }

            /// <summary>
            /// Performs a left rotation 
            /// </summary>
            /// <param name="x"></param>
            /// <returns></returns>
            AVLNode leftRotate(AVLNode x)
            {
                AVLNode y = x.Right;
                AVLNode T2 = y.Left;

                //Do Rotation
                y.Left = x;
                x.Right = T2;

                //Update heights
                x.Height = max(height(x.Left), height(x.Right)) + 1;
                y.Height = max(height(y.Left), height(y.Right)) + 1;

                //Return new root
                return y;
            }

            /// <summary>
            /// Performs a right rotation
            /// </summary>
            /// <param name="y"></param>
            /// <returns></returns>
            AVLNode rightRotate(AVLNode y)
            {
                AVLNode x = y.Left;
                AVLNode T2 = x.Right;

                //Do Rotation
                x.Right = y;
                y.Left = T2;

                //Update heights
                y.Height = max(height(y.Left), height(y.Right)) + 1;
                x.Height = max(height(x.Left), height(x.Right)) + 1;

                //Return new root
                return x;
            }

            /// <summary>
            /// Returns the difference in height of the two sub nodes
            /// </summary>
            /// <param name="n"></param>
            /// <returns></returns>
            int getBalance(AVLNode n)
            {
                if (n == null)
                    return 0;
                return height(n.Left) - height(n.Right);
            }

            /// <summary>
            /// Inserts value into tree
            /// </summary>
            /// <param name="key"></param>
            public void Insert(I key)
            {
                root = Insert(root, key);
            }

            /// <summary>
            /// Internal insert function
            /// This function does a normal BST insertion followed by the AVL rebalancing based on the
            /// node heights
            /// 
            /// There are 4 cases to handle for rebalancing:
            ///     Left Left
            ///     Left Right
            ///     Right Right
            ///     Right Left
            /// 
            /// </summary>
            /// <param name="node"></param>
            /// <param name="key"></param>
            /// <returns></returns>
            private AVLNode Insert(AVLNode node, I key)
            {
                //1 - do normal BST insert
                if (node == null)
                    return new AVLNode(key, null, null);

                if (key.CompareTo(node.Data) < 0)
                    node.Left = Insert(node.Left, key);
                else if (key.CompareTo(node.Data) > 0)
                    node.Right = Insert(node.Right, key);

                if (key.CompareTo(node.Data) == 0)
                {
                    //Nothing to do, key already exists
                    //Not supporting duplicate keys

                    return node;
                }

                //2 update height of this ancestor node
                node.Height = max(height(node.Left), height(node.Right)) + 1;

                //3 Get balance factor of this ancestor node to check whether this node became unbaanced
                int balance = getBalance(node);

                //if unbalnaced, 4 cases to resolve: Left Left, Right Right, Left Right, Right Left

                //Left left case
                if (balance > 1 && key.CompareTo(node.Left.Data) < 0)
                    return rightRotate(node);

                //Right Right case
                if (balance < -1 && key.CompareTo(node.Right.Data) > 0)
                    return leftRotate(node);

                //Left Right case
                if (balance > 1 &&  key.CompareTo(node.Left.Data) > 0)
                {
                    node.Left = leftRotate(node.Left);
                    return rightRotate(node);
                }

                //Right Left case
                if (balance < -1 && key.CompareTo(node.Right.Data) < 0)
                {
                    node.Right = rightRotate(node.Right);
                    return leftRotate(node);
                }

                //Return unchanged node
                return node;

            }


            public void Delete(I key)
            {
                root = Delete(root, key);
            }

            private AVLNode minValueNode(AVLNode node)
            {
                AVLNode current = node;

                while (current.Left != null)
                    current = current.Left;

                return current;
            }

            private AVLNode Delete(AVLNode root, I key)
            {
                //Normal BST deletion
                if (root == null)
                    return root;

                //If key is smaller than current, go left
                if (key.CompareTo(root.Data) < 0)
                    root.Left = Delete(root.Left, key);

                //If key is larger than current, go right
                else if (key.CompareTo(root.Data) > 0)
                    root.Right = Delete(root.Right, key);

                //Current is same as search key, so we found the node to delete
                else
                {
                    //Case with 1 child or no children
                    if ( ( root.Left == null) || (root.Right == null) )
                    {
                        AVLNode temp = (root.Left != null) ? root.Left : root.Right;

                        if (temp == null)
                        {
                            temp = root;
                            root = null;
                        }
                        else
                        {
                            root = temp;
                        }
                    }
                    else
                    {
                        //2 children, so need to get the inorder successor (smallest in the right subtree)
                        //
                        //In this case the node to delete is not the node we found that contains the data.
                        //Instead we are swapping the inorder successor's data into the found node and then
                        //  issueing a delete agains the inorder successor's node

                        AVLNode temp = minValueNode(root.Right);

                        //Copy in order successors data to this node
                        root.Data = temp.Data;

                        //Delete inorder successor
                        root.Right = Delete(root.Right, temp.Data);
                    }
                }

                //Tree only had 1 node, so return
                if (root == null)
                    return root;

                //Update the height of the current node
                root.Height = max(height(root.Left), height(root.Right)) + 1;

                //Get the balance fator of this node to see if it is not unbalanced
                int balance = getBalance(root);

                //Left Left Case
                if (balance > 1 && getBalance(root.Left) >= 0)
                    return rightRotate(root);

                //Left Right case
                if (balance > 1 && getBalance(root.Left) < 0)
                {
                    root.Left = leftRotate(root.Left);
                    return rightRotate(root);
                }

                //Right Right case
                if (balance < -1 && getBalance(root.Right) <= 0)
                {
                    return leftRotate(root);
                }

                //Right Left case
                if (balance < -1 && getBalance(root.Right) > 0)
                {
                    root.Right = rightRotate(root.Right);
                    return leftRotate(root);
                }

                return root;
                
            }


            #endregion

        }

        public void Run()
        {
            AVLTree<int> tree = new AVLTree<int>();

            int []testVals = {1,25,9,3,13,2,18,21};

            for (int x=0; x < testVals.Length; x++)
            {
                tree.Insert(testVals[x]);
            }

            Console.WriteLine("In Order List");
            tree.PrintTree();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Breadith First");
            tree.PrintTree_BFD();

            Console.WriteLine("Depth First");
            tree.PrintTree_DFD();

            tree.Delete(13);

            Console.WriteLine("In Order List");
            tree.PrintTree();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Breadith First");
            tree.PrintTree_BFD();


            List<int> delVals = new List<int>();

            //Console.WriteLine("Finding depth value: 25 = " + tree.FindValueDepth_BFD(25));
            //Console.WriteLine("Finding depth value: 25 = " + tree.FindValueDepth_BFD2(25));

            Console.WriteLine("**********************");
            Console.WriteLine("Large Test");
            tree = new AVLTree<int>();
            Random rnd = new Random((int)DateTime.Now.Ticks);

            int findVal = 0;
            for (int x = 0; x < 50; x++)
            {
                int val = rnd.Next(10000);
                //if (x == 25)
                //    findVal = val;
                //if (!tree.find(val))
                    tree.Insert(val);

                    if (x < 46)// % 2 == 0)
                    {
                        Console.WriteLine("Adding to delete list: " + val);
                        delVals.Add(val);
                    }
            }

            Console.WriteLine("In Order List");
            tree.PrintTree();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Breadith First");
            tree.PrintTree_BFD();

            Console.WriteLine("Depth First");
            tree.PrintTree_DFD();


            //Console.WriteLine("Finding depth value: " + findVal + " = " + tree.FindValueDepth_BFD(findVal));
            //Console.WriteLine("Finding depth value: " + findVal + " = " + tree.FindValueDepth_BFD2(findVal));

            for (int x = 0; x < delVals.Count; x++)
            {
                Console.WriteLine("Deleting value: " + delVals[x]);
                tree.Delete(delVals[x]);
            }

            Console.WriteLine("In Order List");
            tree.PrintTree();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Breadith First");
            tree.PrintTree_BFD();

        }



    }
}
