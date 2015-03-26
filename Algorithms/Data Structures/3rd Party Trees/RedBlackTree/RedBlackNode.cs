using System;

namespace DataStructureLibrary.RedBlackTree
{
    /// <summary>
    /// A single node that is encapsulated in a red black tree data structure.
    /// </summary>
    /// <typeparam name="DataType"></typeparam>
    public class RedBlackNode<KeyType, DataType> where KeyType : IComparable
    {           
        private NodeColor _color;
        private RedBlackNode<KeyType, DataType> _leftNode;
        private RedBlackNode<KeyType, DataType> _rightNode;
        private RedBlackNode<KeyType, DataType> _parentNode;
        private KeyType _key;
        private DataType _data;


        /// <summary>
        /// Creates an instance of this class.
        /// </summary>
        public RedBlackNode()
        {
            Color = NodeColor.Red;
            Left = null;
            Right = null;
            Parent = null;
        }


        ///<summary>
        /// Color - used to balance the tree.
        ///</summary>
        public NodeColor Color
        {
            get
            {
                return _color;
            }

            set
            {
                _color = value;
            }
        }

        ///<summary>
        /// Left node.
        ///</summary>
        public RedBlackNode<KeyType, DataType> Left
        {
            get
            {
                return _leftNode;
            }
            set
            {
                _leftNode = value;
            }
        }

        ///<summary>
        /// Right node.
        ///</summary>
        public RedBlackNode<KeyType, DataType> Right
        {
            get
            {
                return _rightNode;
            }
            set
            {
                _rightNode = value;
            }
        }

        /// <summary>
        /// Parent node.
        /// </summary>
        public RedBlackNode<KeyType, DataType> Parent
        {
            get
            {
                return _parentNode;
            }
            set
            {
                _parentNode = value;
            }
        }
        
        ///<summary>
        /// Key provided by the calling class.
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
        /// Data available in this node
        ///</summary>
        public DataType Data
        {
            get
            {
                return _data;
            }

            set
            {
                _data = value;
            }
        }
    }
}
