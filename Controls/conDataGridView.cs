using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 스마트팩토리.Controls
{
    [ToolboxBitmap(typeof(TextBox))]
    public partial class conDataGridView : DataGridView
    {
        #region Property

        private Color borderColor = Color.White;
        [Browsable(true)]
        [Category(conDefaults.CatAppearance)]
        [Description("테두리 색을 지정하세요.")]
        public Color _BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }

        private string keyboardCmd = "0";
        [Browsable(true)]
        [Category(conDefaults.CatAppearance)]
        [Description("Row 추가 가능여부를 입력하세요. (0=불가, 1=가능)")]
        public string _KeyboardCmd
        {
            get { return keyboardCmd; }
            set { keyboardCmd = value; }
        }

        private string dirKey = "R";
        [Browsable(true)]
        [Category(conDefaults.CatAppearance)]
        [Description("입력된 방향키의 값이 저장됩니다.")]
        public string _DirKey
        {
            get { return dirKey; }
            set { dirKey = value; }
        }

        private string keyInput = "";
        [Browsable(true)]
        [Category(conDefaults.CatAppearance)]
        [Description("입력된 키보드의 값이 저장됩니다.")]
        public string _KeyInput
        {
            get { return keyInput; }
            set { keyInput = value; }
        }

        private int lastCol = -1;
        [Browsable(true)]
        [Category(conDefaults.CatAppearance)]
        [Description("마지막 작업 셀의 column index 값 저장.")]
        public int _LastCol
        {
            get { return lastCol; }
            set { lastCol = value; }
        }

        private int lastRow = -1;
        [Browsable(true)]
        [Category(conDefaults.CatAppearance)]
        [Description("마지막 작업 셀의 row index 값 저장.")]
        public int _LastRow
        {
            get { return lastRow; }
            set { lastRow = value; }
        }

        #endregion

        public conDataGridView()
        {

        }

        private ButtonBorderStyle _borderStyle = ButtonBorderStyle.Solid;
        private static int WM_PAINT = 0x000F;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_PAINT)
            {
                Graphics g = Graphics.FromHwnd(Handle);
                Rectangle bounds = new Rectangle(0, 0, Width, Height);
                ControlPaint.DrawBorder(g, bounds, borderColor, _borderStyle);
                g.Dispose();
                m.Result = IntPtr.Zero;
                return;
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {
                int irow = this.CurrentCell.RowIndex;
                int icolumn = this.CurrentCell.ColumnIndex;

                if (keyData == Keys.Enter)
                {
                    dirKey = "";
                    keyInput = "enter";

                    int nCurCol = 0;
                    int nCurRow = 0;

                    int nRealCol = -1;
                    int nRealRow = -1;

                    if (icolumn == this.Columns.Count - 1)
                    {
                        try
                        {
                            this.CurrentCell = this[0, irow + 1];
                            nCurCol = 0;
                            nCurRow = irow + 1;
                        }
                        catch
                        {
                            nCurCol = icolumn;
                            nCurRow = irow + 1;
                        }
                    }
                    else
                    {
                        //this.CurrentCell = this[icolumn + 1, irow];
                        nCurCol = icolumn; //원본 icolumn + 1;
                        nCurRow = irow;
                    }

                    // edit 셀 찾기---
                    for (int row = nCurRow; row < this.Rows.Count; row++)
                    {
                        if (this.Rows[row].Visible == true)
                        {
                            for (int col = nCurCol; col < this.Columns.Count; col++)
                            {
                                if (this.Columns[col].ReadOnly == true || this.Columns[col].Visible == false || this.Rows[row].Cells[col].ReadOnly == true)
                                {
                                }
                                else
                                {
                                    nRealRow = row;
                                    nRealCol = col;
                                    break;
                                }
                            }
                            nCurCol = 0;
                            if (nRealRow != -1)
                            {
                                break;
                            }
                        }
                    }

                    if (nRealRow != -1 && nRealCol != -1)
                    {
                        //커서가 오른쪽컬럼으로 넘어가서 실행되도록 설계되어있었는데 다음 컬럼이 visible되면서 textbox가 아닌 체크박스라
                        //팝업창이 뜨지 않음. 다음컬럼으로 넘기지 말고 현재 컬럼에 유지하기 위해 nRealCol-1을 해줌. 
                        //

                        this.CurrentCell = this[nRealCol - 1, nRealRow]; //원본 nRearCol
                    }
                    else
                    {
                        // 제자리 지킴
                        this.CurrentCell = this[icolumn, irow];
                    }
                    return true;
                }
                else if (keyData == Keys.Insert || keyData == Keys.F12)
                {
                    // row 추가
                    if (keyboardCmd == "1")
                    {
                        string sNum = "" + (string)this[1, irow].Value;
                        int nRow = this.CurrentCell.ColumnIndex;
                        int nCol = this.CurrentCell.ColumnIndex;

                        // 순번 컬럼 = 1 로 가정됨.
                        DataGridViewColumn colNum = this.Columns[1];
                        ListSortDirection direction = ListSortDirection.Ascending;
                        this.Sort(colNum, direction);

                        for (int kk = 0; kk < this.Rows.Count; kk++)
                        {
                            if ("" + (string)this[1, kk].Value == sNum)
                            {
                                this.CurrentCell = this[nCol, kk];
                                nRow = kk;
                                break;
                            }
                        }
                        this.Rows.Insert(nRow, 1);

                        int nRealCol = -1;
                        for (int col = 0; col < this.Columns.Count; col++)
                        {
                            if (this.Columns[col].ReadOnly == true || this.Columns[col].Visible == false || this.Rows[nRow].Cells[col].ReadOnly == true)
                            {
                            }
                            else
                            {
                                nRealCol = col;
                                break;
                            }
                        }
                        this.CurrentCell = this[nRealCol, nRow];
                    }
                    return true;
                }
                else if ((keyData & Keys.Control) == Keys.Control && (keyData & Keys.Delete) == Keys.Delete)
                {
                    //// row 삭제
                    //if (keyboardCmd == "1")
                    //{
                    //    if (this.Rows.Count > 1)
                    //    {
                    //        if (this.Columns[0].Visible == true)
                    //        {
                    //            this.Columns[0].HeaderText = "[ ]";
                    //            for (int row = this.Rows.Count - 1; row >= 0; row--)
                    //            {
                    //                if ((bool)this.Rows[row].Cells[0].Value == true)
                    //                {
                    //                    this.Rows.RemoveAt(row);
                    //                }
                    //            }
                    //        }
                    //        else
                    //        {
                    //            this.Rows.RemoveAt(irow);
                    //        }
                    //    }
                    //}
                    return true;
                }
                else if (keyData == Keys.F1)
                {
                    // 체크박스 보이기/숨기기
                    if (keyboardCmd == "1")
                    {
                        if (this.Columns[0].Visible == false)
                        {
                            this.Columns[0].Visible = true;
                        }
                        else
                        {
                            this.Columns[0].HeaderText = "[ ]";
                            this.Columns[0].Visible = false;

                            for (int row = 0; row < this.Rows.Count; row++)
                            {
                                this.Rows[row].Cells[0].Value = false;
                            }
                            for (int col = 1; col < this.Columns.Count; col++)
                            {
                                if (this.Columns[col].ReadOnly == true || this.Columns[col].Visible == false || this.Rows[irow].Cells[col].ReadOnly == true)
                                {
                                }
                                else
                                {
                                    this.CurrentCell = this[col, irow];
                                    break;
                                }
                            }
                        }
                    }
                    //return true;
                }
                else if (keyData == Keys.F4)
                {
                    if (keyboardCmd == "1")
                    {
                        keyInput = "f4";
                        this.CurrentCell.ReadOnly = true;
                    }
                    return true;
                }
                else if (keyData == Keys.F6)
                {
                    if (keyboardCmd == "1")
                    {
                        keyInput = "f6";
                    }
                    return true;
                }
                else if (keyData == Keys.F7)
                {
                    if (keyboardCmd == "1")
                    {
                        keyInput = "f7";
                    }
                    return true;
                }
                else if (keyData == Keys.F8)
                {
                    if (keyboardCmd == "1")
                    {
                        keyInput = "f8";
                    }
                    return true;
                }
                else if (keyData == Keys.F11)
                {
                    if (keyboardCmd == "1")
                    {
                        keyInput = "f11";
                    }
                    return true;
                }
                else if (keyData == Keys.Escape)
                {
                    dirKey = "";
                    int nRealCol = -1;
                    for (int col = 0; col < this.Columns.Count - 1; col++)
                    {
                        if (this.Columns[col].Visible == true)
                        {
                            nRealCol = col;
                            break;
                        }
                    }
                    if (nRealCol != -1)
                    {
                        this.CurrentCell = this[nRealCol, 0];
                        SendKeys.Send("+{TAB}");
                    }
                    return true;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e + "/\n" + e.StackTrace.ToString());
            }
            return base.ProcessDialogKey(keyData);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                int irow = this.CurrentCell.RowIndex;
                int icolumn = this.CurrentCell.ColumnIndex;

                //if (keyData == Keys.Tab)
                //{
                //    //todo special handling
                //    return true;
                //}
                if (keyData == (Keys.Control | Keys.Delete))
                {
                    // row 삭제
                    if (keyboardCmd == "1")
                    {
                        if (this.Rows.Count > 1)
                        {
                            if (this.Columns[0].Visible == true)
                            {
                                this.Columns[0].HeaderText = "[ ]";
                                for (int row = this.Rows.Count - 1; row >= 0; row--)
                                {
                                    if ((bool)this.Rows[row].Cells[0].Value == true)
                                    {
                                        this.Rows.RemoveAt(row);
                                    }
                                }
                            }
                            else
                            {
                                this.Rows.RemoveAt(irow);
                            }
                        }
                    }
                }
                //if (keyData == Keys.F4)
                //{
                //    if (keyboardCmd == "1")
                //    {
                //        keyInput = "normal_f4";
                //    }
                //}

                if (keyData == Keys.Right)
                {
                    if (this.Columns[icolumn].GetType() == typeof(DataGridViewComboBoxColumn))
                    {
                        dirKey = "R";
                        SendKeys.Send("{TAB}");
                        return true;
                    }
                    else
                    {
                        dirKey = "R";
                    }
                    //return true;
                }

                if (keyData == Keys.Left)
                {
                    if (this.Columns[icolumn].GetType() == typeof(DataGridViewComboBoxColumn))
                    {
                        dirKey = "L";
                        SendKeys.Send("+{TAB}");
                        return true;
                    }
                    else
                    {
                        dirKey = "L";
                    }
                    //return true;
                }

                //if (keyData == Keys.Up)
                //{
                //    // 콤보박스의 셀에서는 작동 안함.
                //    if (this.Columns[icolumn].GetType() != typeof(DataGridViewComboBoxColumn))
                //    {
                //        // 첫 row에서 그리드 위로 나가기
                //        if (irow == 0)
                //        {
                //            dirKey = "";
                //            int nRealCol = -1;
                //            for (int col = 0; col < this.Columns.Count - 1; col++)
                //            {
                //                if (this.Columns[col].Visible == true)
                //                {
                //                    nRealCol = col;
                //                    break;
                //                }
                //            }
                //            if (nRealCol != -1)
                //            {
                //                this.CurrentCell = this[nRealCol, irow];
                //                SendKeys.Send("+{TAB}");
                //            }
                //            return true;
                //        }
                //    }
                //}

                if (keyData == Keys.Down)
                {
                    // 콤보박스의 셀에서는 작동 안함.
                    if (this.Columns[icolumn].GetType() != typeof(DataGridViewComboBoxColumn))
                    {
                        //// 마지막 row에서 그리드 아래로 나가기
                        //if (irow == this.Rows.Count - 1)
                        //{
                        //    dirKey = "";
                        //    int nRealCol = -1;
                        //    for (int col = this.Columns.Count - 1; col >= 0; col--)
                        //    {
                        //        if (this.Columns[col].Visible == true)
                        //        {
                        //            nRealCol = col;
                        //            break;
                        //        }
                        //    }
                        //    if (nRealCol != -1)
                        //    {
                        //        this.CurrentCell = this[nRealCol, irow];
                        //        SendKeys.Send("{TAB}");
                        //    }
                        //    return true;
                        //}

                        // 다음 row 첫 셀로 이동 시키기
                        if (keyboardCmd == "1")
                        {
                            int nCurCol = 0;
                            int nCurRow = 0;

                            int nRealCol = -1;
                            int nRealRow = -1;

                            nCurCol = 0;
                            nCurRow = irow + 1;

                            // edit 셀 찾기---
                            for (int row = nCurRow; row < this.Rows.Count; row++)
                            {
                                if (this.Rows[row].Visible == true)
                                {
                                    for (int col = nCurCol; col < this.Columns.Count; col++)
                                    {
                                        if (this.Columns[col].ReadOnly == true || this.Columns[col].Visible == false || this.Rows[row].Cells[col].ReadOnly == true)
                                        {
                                        }
                                        else
                                        {
                                            nRealRow = row;
                                            nRealCol = col;
                                            break;
                                        }
                                    }
                                    nCurCol = 0;
                                    if (nRealRow != -1)
                                    {
                                        break;
                                    }
                                }
                            }

                            if (nRealRow != -1 && nRealCol != -1)
                            {
                                this.CurrentCell = this[nRealCol, nRealRow];
                            }
                            //else
                            //{
                            //    // 제자리 지킴
                            //}
                            return true;
                        }
                    }
                }
            }
            catch
            {
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnEnter(EventArgs e)
        {
            try
            {
                bool bExists = false;

                if (lastCol != -1 && lastRow != -1)
                {
                    try
                    {
                        this.CurrentCell = this[lastCol, lastRow];
                        this.BeginEdit(true);
                        bExists = true;
                    }
                    catch
                    {
                        lastCol = -1;
                        lastRow = -1;
                    }
                }

                if (bExists == false)
                {
                    int nCurCol = 0;
                    int nCurRow = 0;

                    int nRealCol = -1;
                    int nRealRow = -1;

                    // edit 셀 찾기---
                    for (int row = nCurRow; row < this.Rows.Count; row++)
                    {
                        if (this.Rows[row].Visible == true)
                        {
                            for (int col = nCurCol; col < this.Columns.Count; col++)
                            {
                                if (this.Columns[col].ReadOnly == true || this.Columns[col].Visible == false || this.Rows[row].Cells[col].ReadOnly == true)
                                {
                                }
                                else
                                {
                                    nRealRow = row;
                                    nRealCol = col;
                                    break;
                                }
                            }
                            nCurCol = 0;
                            if (nRealRow != -1)
                            {
                                break;
                            }
                        }
                    }

                    if (nRealRow != -1 && nRealCol != -1)
                    {
                        this.CurrentCell = this[nRealCol, nRealRow];
                        this.BeginEdit(true);
                    }
                }
            }
            catch
            {
            }
        }

        protected override void OnCellEnter(DataGridViewCellEventArgs e)
        {
            if (this.Columns[e.ColumnIndex].ReadOnly == true || this.Columns[e.ColumnIndex].Visible == false || this.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly == true)
            {
                if (this._DirKey == "R")
                {
                    // 오른쪽으로 이동 가능한 셀 존재여부 예측
                    bool bAvail = false;

                    int nCurCol = 0;
                    int nCurRow = 0;

                    int nRealCol = -1;
                    int nRealRow = -1;

                    nCurCol = e.ColumnIndex + 1;
                    nCurRow = e.RowIndex;

                    //if (nCurCol > this.Columns.Count - 1)
                    //{
                    //    nCurCol = 0;
                    //    nCurRow = e.RowIndex + 1;
                    //}

                    //// edit 셀 찾기---
                    //for (int row = nCurRow; row < this.Rows.Count; row++)
                    //{
                    //    if (this.Rows[row].Visible == true)
                    //    {
                    //        for (int col = nCurCol; col < this.Columns.Count; col++)
                    //        {
                    //            if (this.Columns[col].ReadOnly == true || this.Columns[col].Visible == false || this.Rows[row].Cells[col].ReadOnly == true)
                    //            {
                    //            }
                    //            else
                    //            {
                    //                nRealRow = row;
                    //                nRealCol = col;
                    //                break;
                    //            }
                    //        }
                    //        nCurCol = 0;
                    //        if (nRealRow != -1)
                    //        {
                    //            break;
                    //        }
                    //    }
                    //}

                    //if (nRealRow != -1 && nRealCol != -1)
                    //{
                    //    bAvail = true;
                    //}

                    for (int col = nCurCol; col < this.Columns.Count; col++)
                    {
                        if (this.Columns[col].ReadOnly == true || this.Columns[col].Visible == false || this.Rows[nCurRow].Cells[col].ReadOnly == true)
                        {
                        }
                        else
                        {
                            nRealCol = col;
                            break;
                        }
                    }
                    if (nRealCol != -1)
                    {
                        bAvail = true;
                    }

                    if (bAvail == false)
                    {
                        // 맨 끝이면, 원래 위치로 되돌림.
                        this._DirKey = "";
                        SendKeys.Send("+{TAB}");
                    }
                    else
                    {
                        SendKeys.Send("{TAB}");
                    }
                }
                else if (this._DirKey == "L")
                {
                    // 왼쪽으로 이동 가능한 셀 존재여부 예측
                    bool bAvail = false;

                    int nCurCol = 0;
                    int nCurRow = 0;

                    int nRealCol = -1;
                    int nRealRow = -1;

                    nCurCol = e.ColumnIndex - 1;
                    nCurRow = e.RowIndex;

                    //if (nCurCol < 0)
                    //{
                    //    nCurCol = this.Columns.Count - 1;
                    //    nCurRow = e.RowIndex - 1;
                    //}

                    //// edit 셀 찾기---
                    //for (int row = nCurRow; row >= 0; row--)
                    //{
                    //    if (this.Rows[row].Visible == true)
                    //    {
                    //        for (int col = nCurCol; col >= 0; col--)
                    //        {
                    //            if (this.Columns[col].ReadOnly == true || this.Columns[col].Visible == false || this.Rows[row].Cells[col].ReadOnly == true)
                    //            {
                    //            }
                    //            else
                    //            {
                    //                nRealRow = row;
                    //                nRealCol = col;
                    //                break;
                    //            }
                    //        }
                    //        nCurCol = this.Columns.Count - 1;
                    //        if (nRealRow != -1)
                    //        {
                    //            break;
                    //        }
                    //    }
                    //}

                    //if (nRealRow != -1 && nRealCol != -1)
                    //{
                    //    bAvail = true;
                    //}

                    for (int col = nCurCol; col >= 0; col--)
                    {
                        if (this.Columns[col].ReadOnly == true || this.Columns[col].Visible == false || this.Rows[nCurRow].Cells[col].ReadOnly == true)
                        {
                        }
                        else
                        {
                            nRealCol = col;
                            break;
                        }
                    }
                    if (nRealCol != -1)
                    {
                        bAvail = true;
                    }

                    if (bAvail == false)
                    {
                        // 맨 처음이면, 원래 위치로 되돌림.
                        this._DirKey = "";
                        SendKeys.Send("{TAB}");
                    }
                    else
                    {
                        SendKeys.Send("+{TAB}");
                    }
                }
            }
            else
            {
                this._DirKey = "";
            }
        }

        protected override void OnCellBeginEdit(DataGridViewCellCancelEventArgs e)
        {
            DataGridViewCell cell = this[e.ColumnIndex, e.RowIndex];
            cell.Style.BackColor = Color.LightCyan;
            keyInput = "";

            lastCol = e.ColumnIndex;
            lastRow = e.RowIndex;
        }

        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    // Edit 모드가 아닐때, 작동함.
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        SendKeys.Send("{TAB}");
        //        e.Handled = true;
        //    }

        //    //if (e.KeyData == Keys.Enter)
        //    //{
        //    //    int col = CurrentCell.ColumnIndex;
        //    //    int row = CurrentCell.RowIndex;

        //    //    if (row != this.NewRowIndex)
        //    //    {
        //    //        if (col == (base.Columns.Count - 1))
        //    //        {
        //    //            col = -1;
        //    //            row += 1;
        //    //        }
        //    //        try
        //    //        {
        //    //            CurrentCell = this[col + 1, row];
        //    //        }
        //    //        catch { }
        //    //    }
        //    //    e.Handled = true;
        //    //}
        //    base.OnKeyDown(e);
        //}

    }
}
