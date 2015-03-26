using System;
using System.Collections.Generic;

namespace DataStructureLibrary.RedBlackTree
{
    /// <summary>
    /// The RedBlackEnumerator class returns the keys or data objects of the treap in sorted order. 
    /// </summary>
    public class RedBlackEnumerator<KeyType, DataType> where KeyType : IComparable
    {
        // The treap uses the stack to order the nodes.
        private Stack<RedBlackNode<KeyType, DataType>> _stack;
        
        // Return in ascending order (true) or descending (false).
        private bool _ascending;

        private KeyType _key;
        private DataType _value;
        private RedBlackNode<KeyType, DataType> _sentinelNode;

        /// <summary>
        /// Creates an instance of this class.
        /// </summary>
        public RedBlackEnumerator() { }

        ///<summary>
        /// Determine order, walk the tree and push the nodes onto the stack
        ///</summary>
        /// <param name="node"></param>
        /// <param name="ascending"></param>
        /// <param name="sentinelNode"></param>
        public RedBlackEnumerator(RedBlackNode<KeyType, DataType> node, bool ascending, RedBlackNode<KeyType, DataType> sentinelNode)
        {
            _stack = new Stack<RedBlackNode<KeyType, DataType>>();
            _ascending = ascending;
            _sentinelNode = sentinelNode;

            insertNewNode(node);
        }


        ///<summary>
        /// Key.
        ///</summary>
        public KeyType Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
            }
        }

        ///<summary>
        /// The data or value associated with the key.
        ///</summary>
        public DataType Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
            }
        }


        /// <summary>
        /// Use depth-first traversal to push nodes into stack the lowest node will be at the top of the stack.
        /// </summary>
        /// <param name="node"></param>
        private void insertNewNode(RedBlackNode<KeyType, DataType> node)
        {
            if (_ascending)
            {   // find the lowest node
                while (node != _sentinelNode)
                {
                    _stack.Push(node);
                    node = node.Left;
                }
            }
            else
            {
                // the highest node will be at top of stack
                while (node != _sentinelNode)
                {
                    _stack.Push(node);
                    node = node.Right;
                }
            }
        }


        ///<summary>
        /// Next element.
        ///</summary>
        public DataType NextElement()
        {
            if (_stack.Count == 0)
                throw new DataStructureException("Element not found");

            // the top of stack will always have the next item
            // get top of stack but don't remove it as the next nodes in sequence
            // may be pushed onto the top
            // the stack will be popped after all the nodes have been returned
            RedBlackNode<KeyType, DataType> node = _stack.Peek();	// next node in sequence

            if (_ascending)
            {
                if (node.Right == _sentinelNode)
                {
                    // yes, top node is lowest node in subtree - pop node off stack 
                    RedBlackNode<KeyType, DataType> tn = _stack.Pop();

                    // peek at right node's parent 
                    // get rid of it if it has already been used
                    while (HasMoreElements() && _stack.Peek().Right == tn)
                    {
                        tn = _stack.Pop();
                    }
                }
                else
                {
                    // find the next items in the sequence
                    // traverse to left; find lowest and push onto stack
                    RedBlackNode<KeyType, DataType> tn = node.Right;

                    while (tn != _sentinelNode)
                    {
                        _stack.Push(tn);
                        tn = tn.Left;
                    }
                }
            }
            else            // descending, same comments as above apply
            {
                if (node.Left == _sentinelNode)
                {
                    // walk the tree
                    RedBlackNode<KeyType, DataType> tn = _stack.Pop();
                    while (HasMoreElements() && _stack.Peek().Left == tn)
                    {
                        tn = _stack.Pop();
                    }
                }
                else
                {
                    // determine next node in sequence
                    // traverse to left subtree and find greatest node - push onto stack
                    RedBlackNode<KeyType, DataType> tn = node.Left;
                    while (tn != _sentinelNode)
                    {
                        _stack.Push(tn);
                        tn = tn.Right;
                    }
                }
            }

            _value = node.Data;
            _key = node.Key;
            return node.Data;
        }
        
        ///<summary>
        /// Has more elements.
        ///</summary>
        public bool HasMoreElements()
        {
            return (_stack.Count > 0);
        }

        ///<summary>
        /// Move next.
        ///</summary>
        public bool MoveNext()
        {
            if (HasMoreElements())
            {
                NextElement();
                return true;
            }

            return false;
        }
    }
}
