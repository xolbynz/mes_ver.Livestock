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
    public partial class pop제품검색 : Form
    {
        private int iCnt;
        private string strCondition = "";
        public string sRetCode = "";
        public string sRetName = "";
        private bool bSearch = false;
        private int intTotalRecords = 0;
        private int intPageSize = 0;
        private int intPageCount = 0;
        private int intCurrentPage = 1;
        private int nPageSize = int.Parse(Common.p_PageSize);

        public pop제품검색()
        {
            InitializeComponent();
        }

        private void pop제품검색_Load(object sender, EventArgs e)
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
                strCondition = " and a.PRODUCT_CODE = '" + txtSrch.Text + "' ";
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
                    sb.Append(" and (a.PRODUCT_NAME like '%" + txtSrch.Text + "%' ");
                    sb.Append("     or a.PRODUCT_SPEC like '" + txtSrch.Text + "%' ");
                    sb.Append("     or a.PRODUCT_NAME_SHORT like '" + txtSrch.Text + "%' ");
                    sb.Append("     or a.의약품제품명 like '" + txtSrch.Text + "%' ");
                    sb.Append("     ) ");
                    break;
            }

            return sb.ToString();
        }

        private void bindData()
        {
            this.GridRecord.DataSource = null;
            this.GridRecord.RowCount = 0;

            // For Page view.
            intPageSize = nPageSize;
            intTotalRecords = getCount(strCondition);
            intPageCount = intTotalRecords / intPageSize;

            // Adjust page count if the last page contains partial page.
            if (intTotalRecords % intPageSize > 0)
                intPageCount++;

            intCurrentPage = 0;

            cmbPage.Items.Clear();
            if (intTotalRecords == 0)
            {
                cmbPage.Items.Add("1");
            }
            else
            {
                for (int kk = 0; kk < intPageCount; kk++)
                {
                    cmbPage.Items.Add((kk + 1).ToString());
                }
            }
            cmbPage.SelectedIndex = 0;

            loadPage(strCondition);
        }

        private int getCount(string condition)
        {
            int intCount = 0;

            wnDm wDm = new wnDm();
            DataTable dt = null;
            dt = wDm.fn_PRODUCT_List_Count(condition);

            if (dt != null)
            {
                intCount = int.Parse(dt.Rows[0]["CNT"].ToString());
            }

            return intCount;
        }

        private void loadPage(string condition)
        {
            this.lblStatus.Text = "- / -";

            try
            {
                int intSkip = 0;
                intSkip = (intCurrentPage * intPageSize);

                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_PRODUCT_List_pop(condition, intPageSize, intSkip);

                this.GridRecord.DataSource = dt;

                GridRecord.Columns[0].DefaultCellStyle.ForeColor = Color.Blue;
                GridRecord.Columns[1].DefaultCellStyle.ForeColor = Color.Blue;

                GridRecord.Columns[0].Frozen = true;
                GridRecord.Columns[1].Frozen = true;

                GridRecord.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GridRecord.Columns[0].Width = 80;
                //GridRecord.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GridRecord.Columns[1].Width = 200;

                GridRecord.Columns["유효기간"].Width = 60;
                GridRecord.Columns["유효기간"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                GridRecord.Columns["표준원가"].Width = 80;
                GridRecord.Columns["표준원가"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                GridRecord.Columns["표준원가"].DefaultCellStyle.Format = "#,0";

                GridRecord.Columns["기준약가"].Width = 80;
                GridRecord.Columns["기준약가"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                GridRecord.Columns["기준약가"].DefaultCellStyle.Format = "#,0";

                GridRecord.Columns["도매단가"].Width = 80;
                GridRecord.Columns["도매단가"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                GridRecord.Columns["도매단가"].DefaultCellStyle.Format = "#,0";

                GridRecord.Columns["약국단가"].Width = 80;
                GridRecord.Columns["약국단가"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                GridRecord.Columns["약국단가"].DefaultCellStyle.Format = "#,0";

                GridRecord.Columns["의원단가"].Width = 80;
                GridRecord.Columns["의원단가"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                GridRecord.Columns["의원단가"].DefaultCellStyle.Format = "#,0";

                GridRecord.Columns["병원단가"].Width = 80;
                GridRecord.Columns["병원단가"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                GridRecord.Columns["병원단가"].DefaultCellStyle.Format = "#,0";

                GridRecord.Columns["기타단가"].Width = 80;
                GridRecord.Columns["기타단가"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                GridRecord.Columns["기타단가"].DefaultCellStyle.Format = "#,0";

                GridRecord.Columns["표준단가"].Width = 80;
                GridRecord.Columns["표준단가"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                GridRecord.Columns["표준단가"].DefaultCellStyle.Format = "#,0";

                this.lblStatus.Text = (intCurrentPage + 1).ToString() + " / " + intPageCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("검색중에 오류가 발생했습니다.");
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSrch.Text.Trim() == "" && int.Parse(Common.p_PageSize) > 0)
            {
                MessageBox.Show("검색어를 입력하세요.");
                txtSrch.Focus();
                return;
            }

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

        private void GridRecord_DoubleClick(object sender, EventArgs e)
        {
            if (GridRecord.CurrentCell == null) return;

            iCnt = GridRecord.CurrentCell.RowIndex;
            sRetCode = "" + (string)GridRecord.Rows[iCnt].Cells[0].Value;
            sRetName = "" + (string)GridRecord.Rows[iCnt].Cells[1].Value;

            this.Close();
        }

        private void GridRecord_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //DataGridView grd = (DataGridView)sender;
            //if ("" + (string)grd.Rows[e.RowIndex].Cells["사용여부"].Value == "삭제")
            //{
            //    grd.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Gray;
            //    grd.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Gray;
            //}
            //if ("" + (string)grd.Rows[e.RowIndex].Cells["사용여부"].Value == "중지")
            //{
            //    grd.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DarkRed;
            //    grd.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.DarkRed;
            //}
        }

        private void btnFirst_Click(object sender, System.EventArgs e)
        {
            this.goFirst();
        }

        private void btnPrevious_Click(object sender, System.EventArgs e)
        {
            this.goPrevious();
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            this.goNext();
        }

        private void btnLast_Click(object sender, System.EventArgs e)
        {
            this.goLast();
        }

        private void goFirst()
        {
            intCurrentPage = 0;

            //loadPage();

            cmbPage.SelectedIndex = intCurrentPage;
        }

        private void goPrevious()
        {
            if (intCurrentPage == intPageCount)
                intCurrentPage = intPageCount - 1;

            intCurrentPage--;

            if (intCurrentPage < 1)
                intCurrentPage = 0;

            //loadPage();

            cmbPage.SelectedIndex = intCurrentPage;
        }

        private void goNext()
        {
            intCurrentPage++;

            if (intCurrentPage > (intPageCount - 1))
                intCurrentPage = intPageCount - 1;

            //loadPage();

            cmbPage.SelectedIndex = intCurrentPage;
        }

        private void goLast()
        {
            intCurrentPage = intPageCount - 1;

            //loadPage();

            cmbPage.SelectedIndex = intCurrentPage;
        }

        private void cmbPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (intTotalRecords > 0)
            {
                intCurrentPage = cmbPage.SelectedIndex;

                loadPage(strCondition);
            }
            GridRecord.Focus();
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
