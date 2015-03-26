using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_Trees_DetectBalanced
    {

        public class Node
        {
            public object Data;
            public Node left = null;
            public Node right = null;            
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

                    Console.WriteLine("Level: "+visited[currentNode] + " Data: " + currentNode.Data);
                }

                return true;
            }


            public bool IsBalanced2()
            {
                int left = TreeHeight(root.left);
                int right = TreeHeight(root.right);

                int diff = Math.Abs(left - right);

                if (diff > 1)
                {
                    return false;
                }
                return true;

            }


            public bool IsBalanced3()
            {
                return IsBalanced3(root);
            }
            public bool IsBalanced3(Node root)
            {
                if (root == null)
                    return true;

                int left = TreeHeight(root.left);
                int right = TreeHeight(root.right);

                int diff = Math.Abs(left - right);

                if (diff > 1)
                {
                    return false;
                }

                return IsBalanced3(root.left) && IsBalanced3(root.right);
                
            }


            public int TreeHeight(Node root)
            {
                if (root == null)
                    return 0;

                return Math.Max(TreeHeight(root.left), TreeHeight(root.right)) + 1;
            }

            //Book Solutions
            public int getHeight(Node root)
            {
                if (root == null) return 0;

                return Math.Max(getHeight(root.left), getHeight(root.right)) + 1;
            }

            public bool isBalanced_Book(Node root)
            {
                if (root == null) return true;

                int heightDiff = getHeight(root.left) - getHeight(root.right);
                if (Math.Abs(heightDiff) > 1){
                    return false;
                }
                else
                {
                    return isBalanced_Book(root.left) && isBalanced_Book(root.right);
                }
            }

            public bool isBalanced_Book()
            {
                return isBalanced_Book(root);
            }

            //Book v2
            public int checkHeight(Node root)
            {
                if (root == null)
                    return 0;

                //check if left is balanced
                int leftHeight = checkHeight(root.left);
                if (leftHeight == -1)
                    return -1;

                //check if right is balanced
                int rightHeight = checkHeight(root.right);
                if (rightHeight == -1)
                    return -1;

                //check current node is balanced
                int heightDiff = leftHeight - rightHeight;
                if (Math.Abs(heightDiff) > 1)
                    return -1;
                else
                    return Math.Max(leftHeight, rightHeight) + 1;
            }

            public bool isBalanced_Book_v2()
            {
                if (checkHeight(root) == -1)
                    return false;
                return true;
            }

          


        }
               



        public void Run()
        {
            NewTree n = new NewTree();

            n.Insert(5);

            //left tree
            n.Insert(3);
            //n.Insert(4);
            n.Insert(2);
            
            //right tree
            n.Insert(10);
            n.Insert(8);
            n.Insert(12);

            //Tree is complete and balanced


            //Add 2 more nodes to right side
            n.Insert(11);
            n.Insert(13);

            //Add unbalancing node
            //n.Insert(14);


            Console.WriteLine("Is Balanced BFS: " + n.IsBalanced());
            Console.WriteLine("Is Balanced2: " + n.IsBalanced2());
            Console.WriteLine("Is Balanced3: " + n.IsBalanced3());
            Console.WriteLine("Is BalancedBook: " + n.isBalanced_Book());
            Console.WriteLine("Is BalancedBook v2: " + n.isBalanced_Book_v2());

            n.Insert(14);
            Console.WriteLine("Is Balanced BFS: " + n.IsBalanced());
            Console.WriteLine("Is Balanced2: " + n.IsBalanced2());
            Console.WriteLine("Is Balanced3: " + n.IsBalanced3());
            Console.WriteLine("Is BalancedBook: " + n.isBalanced_Book());
            Console.WriteLine("Is BalancedBook v2: " + n.isBalanced_Book_v2());
            
        }

    }
}
