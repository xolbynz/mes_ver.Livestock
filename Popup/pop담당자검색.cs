using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 스마트팩토리.CLS;

namespace 스마트팩토리.Popup
{
    public partial class pop담당자검색 : Form
    {
        private int iCnt;
        private string strCondition = "";
        public string sRetCode = "";
        public string sRetName = "";
        private bool bSearch = false;

        public pop담당자검색()
        {
            InitializeComponent();
        }

        private void pop담당자검색_Load(object sender, EventArgs e)
        {
            strCondition = this.makeSearchCondition();
            this.bindData();
            tmFocus.Enabled = true;

            // 자동 선택/닫기
            if (GridRecord.RowCount == 1)
            {
                sRetCode = "" + (string)GridRecord.Rows[0].Cells[0].Value;
                sRetName = "" + (string)GridRecord.Rows[0].Cells[1].Value;

                tmClose.Enabled = true;
            }

            if (GridRecord.RowCount == 0)
            {
                strCondition = " and a.MAN_CODE = '" + txtSrch.Text + "' ";
                this.bindData();

                if (GridRecord.RowCount == 1)
                {
                    sRetCode = "" + (string)GridRecord.Rows[0].Cells[0].Value;
                    sRetName = "" + (string)GridRecord.Rows[0].Cells[1].Value;

                    tmClose.Enabled = true;
                }
            }
        }

        private string makeSearchCondition()
        {
            StringBuilder sb = new StringBuilder();

            switch (this.txtSrch.Text)
            {
                case "":
                    sb.Append("");
                    break;
                default:
                    sb.Append(" and (a.CODE_DESC like '%" + txtSrch.Text + "%' ");
                    //sb.Append("     or a.REP_NAME like '" + txtSrch.Text + "%' ");
                    //sb.Append("     or a.COMP_NUM like '" + txtSrch.Text + "%' ");
                    sb.Append("     ) ");
                    break;
            }

            return sb.ToString();
        }

        private void bindData()
        {
            this.GridRecord.DataSource = null;
            this.GridRecord.RowCount = 0;

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_MANCODE_List_pop(strCondition);

                this.GridRecord.DataSource = dt;

                GridRecord.Columns[0].DefaultCellStyle.ForeColor = Color.Blue;
                GridRecord.Columns[1].DefaultCellStyle.ForeColor = Color.Blue;

                GridRecord.Columns[0].Frozen = true;
                GridRecord.Columns[1].Frozen = true;

                GridRecord.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GridRecord.Columns[0].Width = 80;
                //GridRecord.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GridRecord.Columns[1].Width = 150;

                GridRecord.Columns["부서명"].Width = 200;
                //GridRecord.Columns["부서명"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                GridRecord.Columns["퇴사구분"].Width = 80;
                //GridRecord.Columns["부서명"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }
            catch (Exception ex)
            {
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            bSearch = true;
            strCondition = this.makeSearchCondition();
            this.bindData();
            tmFocus.Enabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tmFocus_Tick(object sender, EventArgs e)
        {
            tmFocus.Enabled = false;
            if (GridRecord.Rows.Count > 0)
            {
                if (this.txtSrch.Text == "")
                {
                    if (bSearch == true)
                    {
                        GridRecord.Focus();
                    }
                    else
                    {
                        txtSrch.Focus();
                    }
                }
                else
                {
                    GridRecord.Focus();
                }
            }
            else
            {
                txtSrch.Focus();
            }
        }

        private void tmClose_Tick(object sender, EventArgs e)
        {
            tmClose.Enabled = false;
            this.Close();
        }

        private void GridRecord_DoubleClick(object sender, EventArgs e)
        {
            if (GridRecord.CurrentCell == null) return;

            iCnt = GridRecord.CurrentCell.RowIndex;
            sRetCode = "" + (string)GridRecord.Rows[iCnt].Cells[0].Value;
            sRetName = "" + (string)GridRecord.Rows[iCnt].Cells[1].Value;

            this.Close();
        }

        private void GridRecord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                txtSrch.Focus();
            }

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                e.Handled = true;

                if (GridRecord.RowCount == 0) return;
                if (GridRecord.CurrentCell == null) return;
                if (GridRecord.CurrentCell.RowIndex < 0) return;
                if (GridRecord.CurrentCell.ColumnIndex < 0) return;

                iCnt = GridRecord.CurrentCell.RowIndex;
                sRetCode = "" + (string)GridRecord.Rows[iCnt].Cells[0].Value;
                sRetName = "" + (string)GridRecord.Rows[iCnt].Cells[1].Value;

                this.Close();
            }
        }

        private void GridRecord_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView grd = (DataGridView)sender;
            if ("" + (string)grd.Rows[e.RowIndex].Cells["퇴사구분"].Value == "퇴사")
            {
                grd.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Gray;
                grd.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Gray;
            }
            //if ("" + (string)grd.Rows[e.RowIndex].Cells["사용여부"].Value == "중지")
            //{
            //    grd.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DarkRed;
            //    grd.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.DarkRed;
            //}
        }

        private void txtSrch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                btnExit_Click(this, null);
            }
        }

    }
}
