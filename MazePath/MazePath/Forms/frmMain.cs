using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MazePath;

namespace MazePath.Forms
{


    public partial class frmMain : Form
    {

        #region Members

        /// <summary>
        /// The last movement
        /// </summary>
        public enum LastMove
        {
            None,
            ToRight,
            ToDown,
            ToLeft,
            ToUp
        }

        /// <summary>
        /// Depth search array: 8 x 8 - 2
        /// </summary>
        private List<MatrixNode> _depthFirst; 

        private MatrixNode[,] _Matrix1 = {
            { new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true)}
            , { new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true)}
            , { new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true)}
            , { new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true)}
            , { new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true)}
            , { new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true)}
            , { new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true)}
            , { new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true), new MatrixNode(true)}
        };

        #endregion


        public frmMain()
        {
            InitializeComponent();
            InitClass();
        }


        #region Init

        private void InitClass()
        {
            this.ControlBox = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;

            this.SetEvent();

            // set all the relationship of the matrix
            this.SetMatrixRelation();

            // declaring the list of movement
            this._depthFirst = new List<MatrixNode>();
        }

        private void SetFormToCenterScreen()
        {
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
        }

        private void SetEvent()
        {

            this.box00.MouseClick += Box_MouseClick;

            this.box01.MouseClick += Box_MouseClick;

            this.box02.MouseClick += Box_MouseClick;

            this.box03.MouseClick += Box_MouseClick;

            this.box04.MouseClick += Box_MouseClick;

            this.box05.MouseClick += Box_MouseClick;

            this.box06.MouseClick += Box_MouseClick;

            this.box07.MouseClick += Box_MouseClick;

            this.box10.MouseClick += Box_MouseClick;

            this.box11.MouseClick += Box_MouseClick;

            this.box12.MouseClick += Box_MouseClick;

            this.box13.MouseClick += Box_MouseClick;

            this.box14.MouseClick += Box_MouseClick;

            this.box15.MouseClick += Box_MouseClick;

            this.box16.MouseClick += Box_MouseClick;

            this.box17.MouseClick += Box_MouseClick;

            this.box20.MouseClick += Box_MouseClick;

            this.box21.MouseClick += Box_MouseClick;

            this.box22.MouseClick += Box_MouseClick;

            this.box23.MouseClick += Box_MouseClick;

            this.box24.MouseClick += Box_MouseClick;

            this.box25.MouseClick += Box_MouseClick;

            this.box26.MouseClick += Box_MouseClick;

            this.box27.MouseClick += Box_MouseClick;

            this.box30.MouseClick += Box_MouseClick;

            this.box31.MouseClick += Box_MouseClick;

            this.box32.MouseClick += Box_MouseClick;

            this.box33.MouseClick += Box_MouseClick;

            this.box34.MouseClick += Box_MouseClick;

            this.box35.MouseClick += Box_MouseClick;

            this.box36.MouseClick += Box_MouseClick;

            this.box37.MouseClick += Box_MouseClick;

            this.box40.MouseClick += Box_MouseClick;

            this.box41.MouseClick += Box_MouseClick;

            this.box42.MouseClick += Box_MouseClick;

            this.box43.MouseClick += Box_MouseClick;

            this.box44.MouseClick += Box_MouseClick;

            this.box45.MouseClick += Box_MouseClick;

            this.box46.MouseClick += Box_MouseClick;

            this.box47.MouseClick += Box_MouseClick;

            this.box50.MouseClick += Box_MouseClick;

            this.box51.MouseClick += Box_MouseClick;

            this.box52.MouseClick += Box_MouseClick;

            this.box53.MouseClick += Box_MouseClick;

            this.box54.MouseClick += Box_MouseClick;

            this.box55.MouseClick += Box_MouseClick;

            this.box56.MouseClick += Box_MouseClick;

            this.box57.MouseClick += Box_MouseClick;

            this.box60.MouseClick += Box_MouseClick;

            this.box61.MouseClick += Box_MouseClick;

            this.box62.MouseClick += Box_MouseClick;

            this.box63.MouseClick += Box_MouseClick;

            this.box64.MouseClick += Box_MouseClick;

            this.box65.MouseClick += Box_MouseClick;

            this.box66.MouseClick += Box_MouseClick;

            this.box67.MouseClick += Box_MouseClick;

            this.box70.MouseClick += Box_MouseClick;

            this.box71.MouseClick += Box_MouseClick;

            this.box72.MouseClick += Box_MouseClick;

            this.box73.MouseClick += Box_MouseClick;

            this.box74.MouseClick += Box_MouseClick;

            this.box75.MouseClick += Box_MouseClick;

            this.box76.MouseClick += Box_MouseClick;

            this.box77.MouseClick += Box_MouseClick;

            // Form load event
            this.Load += (object sender, EventArgs e) =>
            {
                SetFormToCenterScreen();

                this.btnIco.BackgroundImage = MazePath.Properties.Resources.Doc;
                this.btnIco.BackgroundImageLayout = ImageLayout.None;
            };


            this.btnCalPath.Click += new EventHandler(btnCalPath_Click);

        }

        

        /// <summary>
        /// Set Matrix Relationship
        /// </summary>
        private void SetMatrixRelation()
        {
            for (int ridx = 0; ridx < 8; ridx++)
            {
                for (int cidx = 0; cidx < 8; cidx++)
                {

                    // set the node surrounding
                    this._Matrix1[cidx, ridx].UpNode = (ridx - 1 >= 0) ? this._Matrix1[cidx, ridx - 1] : new MatrixNode(false);
                    this._Matrix1[cidx, ridx].LeftNode = (cidx - 1 >= 0) ? this._Matrix1[cidx - 1, ridx] : new MatrixNode(false);
                    this._Matrix1[cidx, ridx].DownNode = (ridx + 1 <= 7) ? this._Matrix1[cidx, ridx + 1] : new MatrixNode(false);
                    this._Matrix1[cidx, ridx].RightNode = (cidx + 1 <= 7) ? this._Matrix1[cidx + 1, ridx] : new MatrixNode(false);

                    // Save the current index location
                    this._Matrix1[cidx, ridx].Horizontal = cidx;
                    this._Matrix1[cidx, ridx].Vertical = ridx;

                }
            }

            // search engine array
            //_DepthSearch = new MatrixNode[62];

        }

        #endregion

        #region Matrix operating

        /// <summary>
        /// Each box clicking processing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Box_MouseClick(object sender, MouseEventArgs e)
        {
            string strBoxName = ((Label)sender).Name;
            short bytHor = Convert.ToInt16(strBoxName[4].ToString());
            short bytVer = Convert.ToInt16(strBoxName[3].ToString());

            if ((bytHor == 7 && bytVer == 7) ||
                (bytHor == 0 && bytVer == 0)) return;
            try
            {
                // get the value this time
                bool blnThisTime = !_Matrix1[bytHor, bytVer].EntryAble;

                // Set the back color
                ((Label)sender).BackColor = (blnThisTime) ? Color.White : Color.DarkGray;

                // set the EntryAble value
                _Matrix1[bytHor, bytVer].EntryAble = blnThisTime;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Set the box as a part of the path
        /// </summary>
        /// <param name="pHor">Horizontal index</param>
        /// <param name="pVer">Vertical index</param>
        /// <param name="pValue">if true set to path, if false set back to normal</param>
        private void SetBoxAsPath(int pHor, int pVer, bool pValue)
        {
            if ((pHor == 0 && pVer == 0) ||
                (pHor == 7 && pVer == 7)) return;
            


            try
            {

                Label lblObj = (Label)this.Controls.Find("box" + 
                    pVer.ToString() + pHor.ToString(), true)[0];

                if (pValue)
                {

                    lblObj.BackColor = Color.Cyan;
                }
                else lblObj.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        #endregion

        #region GetPath

        /// <summary>
        /// Find the path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCalPath_Click(object sender, EventArgs e)
        {
            // initialize position
            int iHor = 0;
            int iVer = 0;
            LastMove nextMove = LastMove.None;

            // clear the old content
            for (int idx = 0; idx <= this._depthFirst.Count - 1; idx++)
            {
                if (_depthFirst[idx].EntryAble)
                SetBoxAsPath(_depthFirst[idx].Horizontal, _depthFirst[idx].Vertical, false);
            }

            // renew the matrix node
            if (_depthFirst.Count > 0) _depthFirst = new List<MatrixNode>();

            // add the start point
            _depthFirst.Add(_Matrix1[0, 0]);

            // reset the dead end property
            for (int idxHor = 0; idxHor <= 7; idxHor++)
            {
                for (int idxVer = 0; idxVer <= 7; idxVer++)
                {
                    _Matrix1[idxHor, idxVer].IsDeadEnd = false;
                }
            }

            while (!(iHor >= 7 && iVer >= 7)) 
            {
                

                MatrixNode tmp = GetNextCell(iHor, iVer, ref nextMove);
                if (nextMove != LastMove.None)
                {
                    // If we got the initial cell from GetNextCell
                    // means that there is no way to solve the matrix
                    if (tmp.IsSame(_Matrix1[0, 0]))
                    {

                        MessageBox.Show("The matrix is un-solveable!");
                        return;
                    }

                    if (!this._depthFirst.Contains(tmp)) this._depthFirst.Add(tmp);
                }

                switch (nextMove) 
                {
                    case LastMove.ToDown:
                        iVer++;
                        break;
                    case LastMove.ToLeft:
                        iHor--;
                        break;
                    case LastMove.ToRight:
                        iHor++;
                        break;
                    case LastMove.ToUp:
                        iVer--;
                        break;
                    case LastMove.None:
                        // remove the last element
                        this._depthFirst.Remove(_depthFirst[_depthFirst.Count - 1]);
                        break;
                    default:
                        break;
                }

            }

            //_depthFirst.Add(_Matrix1[7, 7]);

            //Optimize if there is any uneccessary move
            OptimizePath();

            txtArrayList.Text = "";
            for (int idx = 0; idx <= this._depthFirst.Count - 1; idx++)
            {
                txtArrayList.Text += "(" + _depthFirst[idx].Horizontal + ","
                                    + _depthFirst[idx].Vertical + ")";

                SetBoxAsPath(_depthFirst[idx].Horizontal, _depthFirst[idx].Vertical, true);
            }         

        }

        /// <summary>
        ///  This method should return the next component
        /// </summary>
        /// <param name="pHor"></param>
        /// <param name="pVer"></param>
        /// <param name="pLastMove"></param>
        /// <returns></returns>
        private MatrixNode GetNextCell(int pHor, int pVer, ref LastMove pLastMove)
        {


            if (_Matrix1[pHor, pVer].RightNode.EntryAble && pLastMove != LastMove.ToLeft 
                && !_Matrix1[pHor, pVer].RightNode.IsDeadEnd
                && !_depthFirst.Contains(_Matrix1[pHor, pVer].RightNode))
            {
                pLastMove = LastMove.ToRight;
                return _Matrix1[pHor, pVer].RightNode;
            }
            else if (_Matrix1[pHor, pVer].DownNode.EntryAble && pLastMove != LastMove.ToUp
                && !_Matrix1[pHor, pVer].DownNode.IsDeadEnd
                && !_depthFirst.Contains(_Matrix1[pHor, pVer].DownNode))
            {
                pLastMove = LastMove.ToDown;
                return _Matrix1[pHor, pVer].DownNode;
            }
            else if (_Matrix1[pHor, pVer].UpNode.EntryAble && pLastMove != LastMove.ToDown
            && !_Matrix1[pHor, pVer].UpNode.IsDeadEnd
            && !_depthFirst.Contains(_Matrix1[pHor, pVer].UpNode))
            {
                pLastMove = LastMove.ToUp;
                return _Matrix1[pHor, pVer].UpNode;
            }
            else if (_Matrix1[pHor, pVer].LeftNode.EntryAble && pLastMove != LastMove.ToRight
                && !_Matrix1[pHor, pVer].LeftNode.IsDeadEnd
                && !_depthFirst.Contains(_Matrix1[pHor, pVer].LeftNode))
            {
                pLastMove = LastMove.ToLeft;
                return _Matrix1[pHor, pVer].LeftNode;
            }  
            else
            {
                // you need to return the last cell
                pLastMove = LastMove.None;
                _Matrix1[pHor, pVer].IsDeadEnd = true;
                return _Matrix1[pHor, pVer];
            }
        }

        /// <summary>
        /// Optimize the list of MatrixNode which contains the path
        /// </summary>
        private void OptimizePath()
        {
            List<int> intIndexes = new List<int>();

            // optimize not moving around a 4 cornered square node
            for (int idxBase = 0; idxBase <= _depthFirst.Count - 4; idxBase++)
            {
                for (int idxInspect = idxBase + 3; idxInspect <= _depthFirst.Count - 1; idxInspect++ )
                {
                    byte disHor = (byte)Math.Abs(_depthFirst[idxBase].Horizontal - _depthFirst[idxInspect].Horizontal);
                    byte disVer = (byte)Math.Abs(_depthFirst[idxBase].Vertical - _depthFirst[idxInspect].Vertical);

                    if ((disHor == 1 && disVer == 0)
                        || (disHor == 0 && disVer == 1))
                    {
                        // there are a round movement
                        for (int idx = idxBase; idx <= idxInspect; idx++)
                        {
                            intIndexes.Add(idx);
                        }
                    }

                }
            }

            for (int idxDel = intIndexes.Count - 1; idxDel >= 0; idxDel--)
            {
                intIndexes.RemoveAt(idxDel);
            }

        }

        #endregion
    }
}
