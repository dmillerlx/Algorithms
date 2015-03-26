using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
/*
Please note the approved languages:
C
C++
C#
Java
Python
Ruby
PHP
JavaScript


QUESTION:

Tree Printing [Data Structures]
You are given a binary tree where each node contains an integer. Print the tree out to the console level by level, with each level on its own line and each node’s value separated by a comma. You may use the JDK or the standard template library. Your solution will be evaluated on correctness, runtime complexity (big-O), and adherence to coding best practices.  

A complete answer will include the following:

1.      Document your assumptions
2.      Explain your approach and how you intend to solve the problem
3.      Provide code comments where applicable
4.      Explain the big-O run time complexity of your solution. Justify your answer.
5.      Provide a function that calls your printTree() with the provided sample input. 
6.      List and describe test cases needed to test your solution (no need to code)


Example input:

    5
    / \
    3   1
/   / \
9   4   5
    /
    2

Example output:
5
3,1
9,4,5
2

You can use one of the following code skeletons for your solution, or feel free to use the language of your choice from the above list.  

Java:

public class Node {
    public int value;
    public Node left;
    public Node right;
}

void printTree(Node root) {

}

C++:

typedef struct tnode {
    int value;
    struct tnode* left;
    struct tnode* right;
} Node;

void printTree (Node* root) {

}
*/
    
    class Problems_Amazon_PrintTree
    {

        //Approach
        //  Doing BFS tree tansversal, storing the level in the node
        //  When the level changes, add a newline character, otherwise add a comma to the output
        //
        //Assumptions
        //  Input tree will not have any circular references
        //
        //Runtime: O(n) where n = number of items in the tree
        //  We are processing each node 1 time
        //Space complexity: O(h^2) where h = height of tree
        //  If the tree is balanced, the size of the queue can be the height ^ 2

        // Test cases
        // - Sample input (provided below)
        // - Empty tree

        public class Node
        {
            public Node(int v) { value = v; }

            public int value;
            public Node left;
            public Node right;

            public int level;   //Store the level of the node in the tree
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
                Console.Write(currentNode.value);

                lastLevel = currentNode.level;  //Update lastLevel to current level
            }
        }

        public void main()
        {
            //Create and populate tree for sample input

            //Tree is not a BST so populating manually to create sample tree
            Node head = new Node(5);

            //Left side of tree
            head.left = new Node(3);
            head.left.left = new Node(9);

            //right size of tree
            head.right = new Node(1);
            head.right.left = new Node(4);
            head.right.left.left = new Node(2);
            head.right.right = new Node(5);

            //Call printTree function
            printTree(head);

        }















    }
}
