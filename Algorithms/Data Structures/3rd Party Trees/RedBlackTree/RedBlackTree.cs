using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructureLibrary.RedBlackTree
{
    ///<summary>
    ///A red-black tree must satisfy these properties:
    ///1. The root is black. 
    ///2. All leaves are black. 
    ///3. Red nodes can only have black children. 
    ///4. All paths from a node to its leaves contain the same number of black nodes.
    ///</summary>
    public class RedBlackTree<KeyType, DataType> where KeyType : IComparable
    {
        private string _identifier;
        private Random _rand = new Random();
        private int _count;

        // a simple randomized hash code. The hash code could be used as a key
        // if it is "unique" enough. Note: The IComparable interface would need to 
        // be replaced with int.
        private int _hashCode;

        // the actual tree that holds all the data
        private RedBlackNode<KeyType, DataType> _redBlackTree;

        // the node that was last found; used to optimize searches
        private RedBlackNode<KeyType, DataType> _lastNodeFound;
        
        // sentinelNode is convenient way of indicating a leaf node.
        private RedBlackNode<KeyType, DataType> _sentinelNode;


        /// <summary>
        /// Creates an instance of this class.
        /// </summary>
        public RedBlackTree()
        {
            _hashCode = _rand.Next();
            _identifier = base.ToString() + _rand.Next();
            _count = 0;

            // set up the sentinel node. the sentinel node is the key to a successfull
            // implementation and for understanding the red-black tree properties.
            _sentinelNode = new RedBlackNode<KeyType, DataType>();
            _sentinelNode.Left = null;
            _sentinelNode.Right = null;
            _sentinelNode.Parent = null;
            _sentinelNode.Color = NodeColor.Black;
            _redBlackTree = _sentinelNode;
            _lastNodeFound = _sentinelNode;
        }

        /// <summary>
        /// Creates an instance of this class.
        /// </summary>
        /// <param name="identifier"></param>
        public RedBlackTree(string identifier)
        {
            _hashCode = _rand.Next();
            _identifier = identifier;
            _count = 0;
        }


        /// <summary>
        /// The number of nodes contained in the tree.
        /// </summary>
        public int Size
        {
            get
            {
                return _count;
            }
        }


        ///<summary>
        /// Equals.
        ///<summary>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is RedBlackNode<KeyType, DataType>))
                return false;

            if (this == obj)
                return true;

            return (ToString().Equals(((RedBlackNode<KeyType, DataType>)(obj)).ToString()));

        }
        
        /// <summary>
        /// Returns the hashcode.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _hashCode;
        }
        
        ///<summary>
        /// To string.
        ///<summary>
        public override string ToString()
        {
            return _identifier.ToString();
        }


        /// <summary>
        /// Clears the tree.
        /// </summary>
        public void Clear()
        {
            _redBlackTree = _sentinelNode;
            _count = 0;
        }

        /// <summary>
        /// Returns true if the tree is empty.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            if ((_redBlackTree == null) || (Size == 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Removes the node with the minimum key.
        /// </summary>
        public void RemoveMin()
        {
            if (_redBlackTree == null)
                throw new DataStructureException("Node Null");

            Remove(GetMinKey());
        }

        /// <summary>
        /// Removes the node with the maximum key.
        /// </summary>
        public void RemoveMax()
        {
            if (_redBlackTree == null)
                throw new DataStructureException("Red Black Node Null");

            Remove(GetMaxKey());
        }

        /// <summary>
        /// Returns the minimum key value.
        /// </summary>
        /// <returns></returns>
        public KeyType GetMinKey()
        {
            RedBlackNode<KeyType, DataType> treeNode = _redBlackTree;

            if (treeNode == null || treeNode == _sentinelNode)
                throw new DataStructureException("Tree Empty");

            // traverse to the extreme left to find the smallest key
            while (treeNode.Left != _sentinelNode)
            {
                treeNode = treeNode.Left;
            }

            _lastNodeFound = treeNode;

            return treeNode.Key;

        }

        /// <summary>
        /// Returns the maximum key value.
        /// </summary>
        /// <returns></returns>
        public KeyType GetMaxKey()
        {
            RedBlackNode<KeyType, DataType> treeNode = _redBlackTree;

            if (treeNode == null || treeNode == _sentinelNode)
                throw new DataStructureException("Tree Empty");

            // traverse to the extreme right to find the largest key
            while (treeNode.Right != _sentinelNode)
            {
                treeNode = treeNode.Right;
            }

            _lastNodeFound = treeNode;

            return treeNode.Key;

        }

        /// <summary>
        /// Returns the object having the minimum key value.
        /// </summary>
        /// <returns></returns>
        public DataType GetMinValue()
        {
            return GetData(GetMinKey());
        }

        /// <summary>
        /// Returns the object having the maximum key.
        /// </summary>
        /// <returns></returns>
        public DataType GetMaxValue()
        {
            return GetData(GetMaxKey());
        }

        /// <summary>
        /// Gets the data object associated with the specified key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public DataType GetData(KeyType key)
        {
            int result;

            // begin at root
            RedBlackNode<KeyType, DataType> treeNode = _redBlackTree;

            // traverse tree until node is found
            while (treeNode != _sentinelNode)
            {
                result = key.CompareTo(treeNode.Key);
                if (result == 0)
                {
                    _lastNodeFound = treeNode;
                    return treeNode.Data;
                }

                if (result < 0)
                {
                    treeNode = treeNode.Left;
                }
                else
                {
                    treeNode = treeNode.Right;
                }
            }

            throw new DataStructureException("Node Not Found: "+key.ToString());
        }

        ///<summary>
        /// Adds data to the tree.
        ///</summary>
        public void Add(KeyType key, DataType data)
        {
            if ((key == null) || (data == null))
            {
                throw new DataStructureException("Node Add Failed");
            }

            // traverse tree - find where node belongs
            int result = 0;

            // create new node
            RedBlackNode<KeyType, DataType> node = new RedBlackNode<KeyType, DataType>();
            RedBlackNode<KeyType, DataType> temp = _redBlackTree;

            while (temp != _sentinelNode)
            {	// find Parent
                node.Parent = temp;
                result = key.CompareTo(temp.Key);
                if (result == 0)
                {
                    throw new DataStructureException("Duplicate Key");
                }
                if (result > 0)
                {
                    temp = temp.Right;
                }
                else
                {
                    temp = temp.Left;
                }
            }

            // setup node
            node.Key = key;
            node.Data = data;
            node.Left = _sentinelNode;
            node.Right = _sentinelNode;

            // insert node into tree starting at parent's location
            if (node.Parent != null)
            {
                result = node.Key.CompareTo(node.Parent.Key);

                if (result > 0)
                {
                    node.Parent.Right = node;
                }
                else
                {
                    node.Parent.Left = node;
                }
            }
            else
            {
                // first node added
                _redBlackTree = node;
            }

            // restore red-black properities
            restoreAfterInsert(node);
            _lastNodeFound = node;
            _count++;
        }
        

        ///<summary>
        /// Rebalance the tree by rotating the nodes to the left.
        ///</summary>        
        protected void RotateLeft(RedBlackNode<KeyType, DataType> x)
        {
            // pushing node x down and to the Left to balance the tree. x's Right child (y)
            // replaces x (since y > x), and y's Left child becomes x's Right child 
            // (since it's < y but > x).

            // get x's Right node, this becomes y
            RedBlackNode<KeyType, DataType> y = x.Right;			

            // set x's Right link
            // y's Left child's becomes x's Right child
            x.Right = y.Left;

            // modify parents
            if (y.Left != _sentinelNode)
            {
                // sets y's Left Parent to x
                y.Left.Parent = x;
            }

            if (y != _sentinelNode)
            {
                // set y's Parent to x's Parent
                y.Parent = x.Parent;
            }

            if (x.Parent != null)
            {
                // determine which side of it's Parent x was on
                if (x == x.Parent.Left)
                {
                    // set Left Parent to y
                    x.Parent.Left = y;
                }
                else
                {
                    // set Right Parent to y
                    x.Parent.Right = y;
                }
            }
            else
            {
                // at rbTree, set it to y
                _redBlackTree = y;
            }

            // link x and y
            // put x on y's Left
            y.Left = x;							
            if (x != _sentinelNode)
            {
                // set y as x's Parent
                x.Parent = y;
            }
        }
        
        ///<summary>
        /// Rebalance the tree by rotating the nodes to the right.
        ///</summary>
        protected void RotateRight(RedBlackNode<KeyType, DataType> x)
        {
            // pushing node x down and to the Right to balance the tree. x's Left child (y)
            // replaces x (since x < y), and y's Right child becomes x's Left child 
            // (since it's < x but > y).

            // get x's Left node, this becomes y
            RedBlackNode<KeyType, DataType> y = x.Left;

            // set x's Right link
            // y's Right child becomes x's Left child
            x.Left = y.Right;

            // modify parents
            if (y.Right != _sentinelNode)
            {
                // sets y's Right Parent to x
                y.Right.Parent = x;
            }

            if (y != _sentinelNode)
            {
                // set y's Parent to x's Parent
                y.Parent = x.Parent;
            }

            // null=rbTree, could also have used rbTree
            if (x.Parent != null)
            {
                // determine which side of it's Parent x was on
                if (x == x.Parent.Right)
                {
                    // set Right Parent to y
                    x.Parent.Right = y;
                }
                else
                {
                    // set Left Parent to y
                    x.Parent.Left = y;
                }
            }
            else
            {
                // at rbTree, set it to y
                _redBlackTree = y;
            }

            // link x and y
            // put x on y's Right
            y.Right = x;
            if (x != _sentinelNode)
            {
                // set y as x's Parent
                x.Parent = y;
            }
        }
        

        /// <summary>
        /// Removes the key and data object (delete).
        /// </summary>
        /// <param name="key"></param>
        public void Remove(KeyType key)
        {
            if (key == null)
            {
                throw new DataStructureException("Null Key");
            }

            // find node
            int result=-1;
            RedBlackNode<KeyType, DataType> node;

            // see if node to be deleted was the last one found
            if (_lastNodeFound.Key != null)
            {
                result = key.CompareTo(_lastNodeFound.Key);
            }
            if ((_lastNodeFound.Key != null) && (result == 0))
            {
                node = _lastNodeFound;
            }
            else
            {	// not found, must search		
                node = _redBlackTree;
                while (node != _sentinelNode)
                {
                    result = key.CompareTo(node.Key);
                    if (result == 0)
                    {
                        break;
                    }
                    if (result < 0)
                    {
                        node = node.Left;
                    }
                    else
                    {
                        node = node.Right;
                    }
                }

                if (node == _sentinelNode)
                {
                    // key not found
                    return;
                }
            }

            delete(node);

            _count--;
        }

        /// <summary>
        /// Returns an enumerator that returns the tree nodes in order.
        /// </summary>
        /// <returns></returns>
        public RedBlackEnumerator<KeyType, DataType> GetEnumerator()
        {
            // elements is simply a generic name to refer to the 
            // data objects the nodes contain
            return elements(true);
        }

        /// <summary>
        /// Returns keys in ascending order.
        /// </summary>
        /// <returns></returns>
        public RedBlackEnumerator<KeyType, DataType> Keys()
        {
            return Keys(true);
        }

        /// <summary>
        /// Returns keys in order as specified in parameter.
        /// </summary>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public RedBlackEnumerator<KeyType, DataType> Keys(bool ascending)
        {
            return new RedBlackEnumerator<KeyType, DataType>(_redBlackTree, ascending, _sentinelNode);
        }

        /// <summary>
        /// Returns all the values in the tree in ascending order.
        /// </summary>
        /// <returns></returns>
        public RedBlackEnumerator<KeyType, DataType> Values()
        {
            return Values(true);
        }

        /// <summary>
        /// Returns all the values in the tree in order as specifed in the parameter.
        /// </summary>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public RedBlackEnumerator<KeyType, DataType> Values(bool ascending)
        {
            return elements(ascending);
        }


        /// <summary>
        /// Returns an enumeration of the data objects in ascending order.
        /// </summary>
        /// <returns></returns>
        private RedBlackEnumerator<KeyType, DataType> elements()
        {
            return elements(true);
        }

        /// <summary>
        /// Returns an enumeration of the data objects in order as specifed in the parameter.
        /// </summary>
        /// <param name="ascending"></param>
        /// <returns></returns>
        private RedBlackEnumerator<KeyType, DataType> elements(bool ascending)
        {
            return new RedBlackEnumerator<KeyType, DataType>(_redBlackTree, ascending, _sentinelNode);
        }

        /// <summary>
        /// RestoreAfterInsert
        /// Additions to red-black trees usually destroy the red-black 
        /// properties. Examine the tree and restore. Rotations are normally 
        /// required to restore it
        /// </summary>
        /// <param name="x"></param>
        private void restoreAfterInsert(RedBlackNode<KeyType, DataType> x)
        {
            // x and y are used as variable names for brevity, in a more formal
            // implementation, you should probably change the names

            RedBlackNode<KeyType, DataType> y;

            // maintain red-black tree properties after adding x
            while (x != _redBlackTree && x.Parent.Color == NodeColor.Red)
            {
                // determine traversal path
                // is it on the Left or Right subtree?
                if (x.Parent == x.Parent.Parent.Left)
                {
                    // get uncle
                    y = x.Parent.Parent.Right;
                    if (y != null && y.Color == NodeColor.Red)
                    {
                        // uncle is red; change x's Parent and uncle to black
                        x.Parent.Color = NodeColor.Black;
                        y.Color = NodeColor.Black;

                        // grandparent must be red. Why? Every red node that is not 
                        // a leaf has only black children 
                        x.Parent.Parent.Color = NodeColor.Red;

                        // continue loop with grandparent
                        x = x.Parent.Parent;
                    }
                    else
                    {
                        // uncle is black; determine if x is greater than Parent
                        if (x == x.Parent.Right)
                        {
                            // yes, x is greater than Parent; rotate Left
                            // make x a Left child
                            x = x.Parent;
                            RotateLeft(x);
                        }

                        // no, x is less than Parent:
                        // make Parent black
                        // make grandparent black
                        // rotate right
                        x.Parent.Color = NodeColor.Black;
                        x.Parent.Parent.Color = NodeColor.Red;
                        RotateRight(x.Parent.Parent);
                    }
                }
                else
                {
                    // x's Parent is on the Right subtree
                    // this code is the same as above with "Left" and "Right" swapped
                    y = x.Parent.Parent.Left;
                    if (y != null && y.Color == NodeColor.Red)
                    {
                        x.Parent.Color = NodeColor.Black;
                        y.Color = NodeColor.Black;
                        x.Parent.Parent.Color = NodeColor.Red;
                        x = x.Parent.Parent;
                    }
                    else
                    {
                        if (x == x.Parent.Left)
                        {
                            x = x.Parent;
                            RotateRight(x);
                        }

                        x.Parent.Color = NodeColor.Black;
                        x.Parent.Parent.Color = NodeColor.Red;
                        RotateLeft(x.Parent.Parent);
                    }
                }
            }

            // tree should always be black
            _redBlackTree.Color = NodeColor.Black;
        }

        /// <summary>
        /// Deletions from red-black trees may destroy the red-black 
        /// properties. Examine the tree and restore. Rotations are normally 
        /// required to restore it.
        /// </summary>
        /// <param name="x"></param>
        private void restoreAfterDelete(RedBlackNode<KeyType, DataType> x)
        {
            // maintain Red-Black tree balance after deleting node 			

            RedBlackNode<KeyType, DataType> y;

            while (x != _redBlackTree && x.Color == NodeColor.Black)
            {
                // determine sub tree from parent
                if (x == x.Parent.Left)
                {
                    // y is x's sibling 
                    y = x.Parent.Right;
                    if (y.Color == NodeColor.Red)
                    {
                        // x is black, y is red - make both black and rotate
                        y.Color = NodeColor.Black;
                        x.Parent.Color = NodeColor.Red;
                        RotateLeft(x.Parent);
                        y = x.Parent.Right;
                    }
                    if ((y.Left.Color == NodeColor.Black) && (y.Right.Color == NodeColor.Black))
                    {
                        // children are both black

                        // change parent to red
                        y.Color = NodeColor.Red;
                        // move up the tree
                        x = x.Parent;
                    }
                    else
                    {
                        if (y.Right.Color == NodeColor.Black)
                        {
                            y.Left.Color = NodeColor.Black;
                            y.Color = NodeColor.Red;
                            RotateRight(y);
                            y = x.Parent.Right;
                        }

                        y.Color = x.Parent.Color;
                        x.Parent.Color = NodeColor.Black;
                        y.Right.Color = NodeColor.Black;
                        RotateLeft(x.Parent);
                        x = _redBlackTree;
                    }
                }
                else
                {
                    // right subtree - same as code above with right and left swapped
                    y = x.Parent.Left;
                    if (y.Color == NodeColor.Red)
                    {
                        y.Color = NodeColor.Black;
                        x.Parent.Color = NodeColor.Red;
                        RotateRight(x.Parent);
                        y = x.Parent.Left;
                    }

                    if ((y.Right.Color == NodeColor.Black) && (y.Left.Color == NodeColor.Black))
                    {
                        y.Color = NodeColor.Red;
                        x = x.Parent;
                    }
                    else
                    {
                        if (y.Left.Color == NodeColor.Black)
                        {
                            y.Right.Color = NodeColor.Black;
                            y.Color = NodeColor.Red;
                            RotateLeft(y);
                            y = x.Parent.Left;
                        }

                        y.Color = x.Parent.Color;
                        x.Parent.Color = NodeColor.Black;
                        y.Left.Color = NodeColor.Black;
                        RotateRight(x.Parent);
                        x = _redBlackTree;
                    }
                }
            }

            x.Color = NodeColor.Black;
        }

        /// <summary>
        /// Deletes a node from the tree and restores red black properties.
        /// </summary>
        /// <param name="node"></param>
        private void delete(RedBlackNode<KeyType, DataType> node)
        {
            // A node to be deleted will be: 
            //		1. a leaf with no children
            //		2. have one child
            //		3. have two children
            // If the deleted node is red, the red black properties still hold.
            // If the deleted node is black, the tree needs rebalancing

            // work node to contain the replacement node
            RedBlackNode<KeyType, DataType> x = new RedBlackNode<KeyType, DataType>();
            // work node 
            RedBlackNode<KeyType, DataType> y;

            // find the replacement node (the successor to x) - the node one with 
            // at *most* one child. 
            if (node.Left == _sentinelNode || node.Right == _sentinelNode)
            {
                // node has sentinel as a child
                y = node;
            }
            else
            {
                // node to be deleted has two children, find replacement node which will 
                // be the leftmost node greater than node to be deleted

                // traverse right subtree	
                y = node.Right;
                // to find next node in sequence
                while (y.Left != _sentinelNode)
                {
                    y = y.Left;
                }
            }

            // at this point, y contains the replacement node. it's content will be copied 
            // to the valules in the node to be deleted

            // x (y's only child) is the node that will be linked to y's old parent. 
            if (y.Left != _sentinelNode)
            {
                x = y.Left;
            }
            else
            {
                x = y.Right;
            }

            // replace x's parent with y's parent and
            // link x to proper subtree in parent
            // this removes y from the chain
            x.Parent = y.Parent;
            if (y.Parent != null)
            {
                if (y == y.Parent.Left)
                {
                    y.Parent.Left = x;
                }
                else
                {
                    y.Parent.Right = x;
                }
            }
            else
            {
                // make x the root node
                _redBlackTree = x;
            }

            // copy the values from y (the replacement node) to the node being deleted.
            // note: this effectively deletes the node. 
            if (y != node)
            {
                node.Key = y.Key;
                node.Data = y.Data;
            }

            if (y.Color == NodeColor.Black)
            {
                restoreAfterDelete(x);
            }

            _lastNodeFound = _sentinelNode;
        }
    }
}
