using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
namespace Algorithms
{
    public class DataStructure_Tree
    {
        //Binary Tree Functions
        
        public int []dataPoints = {1, 9, 5, 21, 66, 11, 44, 3, 10, 9};

        public void BTreeTest()
        {
            Node rootNode = CreateTree(dataPoints);

            PrintValues(rootNode);

            PrintValues_BFD(rootNode);

            ResetVisited_BFD(rootNode);

            Node v = FindValue_BFD(rootNode, 5);

            Debug.WriteLine("-------------Searching for node----------------");

            if (v == null)
                Debug.WriteLine("Not Found");
            else
                Debug.WriteLine("Node Found");


            Debug.WriteLine("-------------Show nodes and depths----------------");
            
            foreach (int x in dataPoints)
            {
                int depth = FindValueDepth(rootNode, x, 0);
                Debug.WriteLine("Value: "+x + " Depth: " + depth);
            }


            Debug.WriteLine("-------------Show nodes in BFD----------------");            

            foreach (int x in dataPoints)
            {
                ResetVisited_BFD(rootNode);
                int depth = FindValueDepth_BFD(rootNode, x);
                Debug.WriteLine("Value: " + x + " Depth: " + depth);
            }


        }
        
        public Node CreateTree(int []vals)
        {
            Node rootNode = null;

            if (vals == null) return null;

            foreach (int v in vals)
            {
                rootNode = InsertValue(rootNode, v);
            }

            return rootNode;
        }

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

        public Node FindValue(Node root, object val)
        {
            if (root == null) return null;

            if (root.Data == val)
            {
                return root;
            }

            if ((int)val < (int)root.Data)
            {
                return FindValue(root.left, val);
            }
            else
            {
                return FindValue(root.right, val);
            }
        }

        public void PrintValues(Node root)
        {
            if (root == null) return;

            PrintValues(root.left);
            Debug.WriteLine((int)root.Data);
            PrintValues(root.right);
        }

        public int FindValueDepth(Node root, int val, int level)
        {
            if (root == null) return level;

            if ((int)root.Data == val)
                return level + 1;

            if (val <= (int)root.Data)
                return FindValueDepth(root.left, val, level + 1);
            else
                return FindValueDepth(root.right, val, level + 1);

        }

        //BFD Functions

        public class NodeExt
        {
            public NodeExt(Node n, int l)
            {
                this.node = n;
                this.level = l;
            }

            public Node node;
            public int level;
        }

        public int FindValueDepth_BFD(Node root, int val)
        {
            Queue<NodeExt> nodeQueue = new Queue<NodeExt>();

            nodeQueue.Enqueue(new NodeExt(root, 1));

            while (nodeQueue.Count > 0)
            {
                NodeExt currentNode = nodeQueue.Peek();                
                
                if ((int)currentNode.node.Data == (int)val)
                {
                    return currentNode.level;
                }


                if (currentNode.node.left != null && currentNode.node.left.visited == false)
                {
                    nodeQueue.Enqueue(new NodeExt(currentNode.node.left, currentNode.level + 1));
                }
                if (currentNode.node.right != null && currentNode.node.right.visited == false)
                {
                    nodeQueue.Enqueue(new NodeExt(currentNode.node.right, currentNode.level + 1));
                }
                
                currentNode.node.visited = true;

                nodeQueue.Dequeue();
            }

            return 0;
        }

        public Node FindValue_BFD(Node root, object val)
        {
            Queue<Node> nodeQueue = new Queue<Node>();

            nodeQueue.Enqueue(root);

            while (nodeQueue.Count > 0)
            {
                Node currentNode = nodeQueue.Peek();

                if ((int)currentNode.Data == (int)val)
                {
                    return currentNode;
                }

                if (currentNode.left != null && currentNode.left.visited == false)
                {
                    nodeQueue.Enqueue(currentNode.left);
                }
                if (currentNode.right != null && currentNode.right.visited == false)
                {
                    nodeQueue.Enqueue(currentNode.right);
                }

                //Debug.WriteLine((int)currentNode.Data);
                currentNode.visited = true;

                nodeQueue.Dequeue();
            }

            return null;
        }

        public void ResetVisited_BFD(Node root)
        {
            if (root == null) return;

            root.visited = false;
            ResetVisited_BFD(root.left);
            ResetVisited_BFD(root.right);            
        }

        public void PrintValues_BFD(Node root)
        {
            Queue<Node> nodeQueue = new Queue<Node>();

            nodeQueue.Enqueue(root);

            while (nodeQueue.Count > 0)
            {
                Node currentNode = nodeQueue.Peek();
                if (currentNode.left != null && currentNode.left.visited == false)
                {
                    nodeQueue.Enqueue(currentNode.left);
                }
                if (currentNode.right != null && currentNode.right.visited == false)
                {
                    nodeQueue.Enqueue(currentNode.right);
                }

                Debug.WriteLine((int)currentNode.Data);
                currentNode.visited = true;

                nodeQueue.Dequeue();
                
            }

        }
    }   


    public class Node
    {
        public Node()
        { }

        public Node(object data)
        {
            Data = data;
            left = null;
            right = null;
        }

        public Node(object data, Node left, Node right)
        {
            Data = data;
            this.left = left;
            this.right = right;
        }


        public object Data;
        public Node left = null;
        public Node right = null;

        public bool visited = false;
    }


}
