using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazePath
{
    /// <summary>
    /// Nodes in Matrix Structure
    /// </summary>
    public class MatrixNode
    {

        private MatrixNode _Left;
        private MatrixNode _Right;
        private MatrixNode _Up;
        private MatrixNode _Down;

        private int _Horizontal;
        private int _Vertical;

        /// <summary>
        /// define the state of the node
        /// </summary>
        private bool _isEntryAble;

        private bool _inDeadEnd;

        /// <summary>
        /// Declaring every side node to be null
        /// </summary>
        /// <param name="pSetNull">set the entry able property</param>
        public MatrixNode(bool pSetNull)
        {

            _Left = null;
            _Right = null;
            _Up = null;
            _Down = null;

            _Horizontal = 0;
            _Vertical = 0;

            _isEntryAble = pSetNull;
            _inDeadEnd = false;
        }

        /// <summary>
        /// get or set the property of whether the cell is in the dead end path
        /// </summary>
        public bool IsDeadEnd
        {
            get
            {
                return this._inDeadEnd;
            }
            set
            {
                this._inDeadEnd = value;
            }
        }

        /// <summary>
        /// get or set the property of EntryAble
        /// </summary>
        public bool EntryAble
        {
            get
            {
                return this._isEntryAble;
            }
            set
            {
                this._isEntryAble = value;
            }
        }

        /// <summary>
        /// Assign or Retrieve the Left Node
        /// </summary>
        public MatrixNode LeftNode
        {
            get
            {
                return this._Left;
            }
            set
            {
                this._Left = value;
            }
        }

        /// <summary>
        /// Assign or Retrieve the Right Node
        /// </summary>
        public MatrixNode RightNode
        {
            get
            {
                return this._Right;
            }
            set
            {
                this._Right = value;
            }
        }
        /// <summary>
        /// Assign or Retrieve the Upper Node
        /// </summary>
        public MatrixNode UpNode
        {
            get
            {
                return this._Up;
            }
            set
            {
                this._Up = value;
            }
        }

        /// <summary>
        /// Assign or Retrieve the Lower Node
        /// </summary>
        public MatrixNode DownNode
        {
            get
            {
                return this._Down;
            }
            set
            {
                this._Down = value;
            }
        }

        /// <summary>
        /// get or set vertical index
        /// </summary>
        public int Vertical
        {
            get
            {
                return this._Vertical;
            }
            set
            {
                this._Vertical = value;
            }
        }

        /// <summary>
        /// get or set horizontal index
        /// </summary>
        public int Horizontal
        {
            get
            {
                return this._Horizontal;
            }
            set
            {
                this._Horizontal = value;
            }
        }

        /// <summary>
        /// Check if two Nodes are the same
        /// </summary>
        /// <param name="pNode"></param>
        /// <returns></returns>
        public bool IsSame(MatrixNode pNode)
        {
            if (pNode == null) return false;

            if (this._Horizontal == pNode.Horizontal &&
                this._Vertical == pNode.Vertical) return true;

            return false;
        }

    }

}
