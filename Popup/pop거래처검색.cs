using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 스마트팩토리.CLS;

namespace 스마트팩토리.Popup
{
    public partial class pop거래처검색 : Form
    {
        private int iCnt;
        private string strCondition = "";
        
        public string sCode = "";
        public string sName = "";
        public string sInUnit = "";
        public string sOutUnit = "";
        public string sInUnitNm = "";
        public string sOutUnitNm = "";
        public string sCustGbn = "";
        public string sCustNm = "";
        public string sTaxCd = "";


        private bool bSearch = false;
        private int intTotalRecords = 0;
        private int intPageSize = 0;
        private int intPageCount = 0;
        private int intCurrentPage = 1;

        private int nPageSize = int.Parse(Common.p_PageSize);
        public pop거래처검색()
        {
            InitializeComponent();
        }

        private void pop거래처검색_Load(object sender, EventArgs e)
        {

            bindData("where CUST_GUBUN = '" + sCustGbn + "' and  ( CUST_NM like '%" + txtSrch.Text.ToString() + "%' OR CUST_CD like '%" + txtSrch.Text.ToString() + "%' ) ");

        }

        private void bindData(string condition)
        {
            this.GridRecord.DataSource = null;
            this.GridRecord.RowCount = 0;

            wnDm wDm = new wnDm();
            DataTable dt = null;
            dt = wDm.fn_Cust_List(condition);

            // For Page view.
            intPageSize = nPageSize;
            intTotalRecords = getCount(dt);
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

            loadPage(dt);
        }

        private int getCount(DataTable dt)
        {
            int intCount = 0;

            if (dt != null)
            {
                intCount = dt.Rows.Count;
            }

            return intCount;
        }

        private void loadPage(DataTable dt)
        {
            this.lblStatus.Text = "- / -";

            try
            {
                int intSkip = 0;
                intSkip = (intCurrentPage * intPageSize);

                GridRecord.Columns[0].DefaultCellStyle.ForeColor = Color.Blue;
                GridRecord.Columns[1].DefaultCellStyle.ForeColor = Color.Blue;
                GridRecord.Columns[2].DefaultCellStyle.ForeColor = Color.Blue;

                GridRecord.Columns[0].Frozen = true;
                GridRecord.Columns[1].Frozen = true;
                GridRecord.Columns[2].Frozen = true;

                GridRecord.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GridRecord.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GridRecord.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                GridRecord.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GridRecord.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GridRecord.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GridRecord.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GridRecord.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GridRecord.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                this.lblStatus.Text = (intCurrentPage + 1).ToString() + " / " + intPageCount.ToString();

                if (dt.Rows.Count > 0)
                {
                    GridRecord.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (dt.Rows[i]["USED_CD"].ToString().Equals("2"))
                        {
                            GridRecord.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        }
                        else if (dt.Rows[i]["USED_CD"].ToString().Equals("3"))
                        {
                            GridRecord.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                        else if (dt.Rows[i]["USED_CD"].ToString().Equals("1"))
                        {
                            GridRecord.Rows[i].DefaultCellStyle.BackColor = Color.Empty;
                        }

                        GridRecord.Rows[i].Cells[0].Value = dt.Rows[i]["CUST_CD"].ToString();
                        GridRecord.Rows[i].Cells[1].Value = dt.Rows[i]["CUST_GUBUN_NM"].ToString();
                        GridRecord.Rows[i].Cells[2].Value = dt.Rows[i]["CUST_NM"].ToString();
                        GridRecord.Rows[i].Cells[3].Value = dt.Rows[i]["SAUP_NO"].ToString();
                        GridRecord.Rows[i].Cells[4].Value = dt.Rows[i]["UPTAE"].ToString();
                        GridRecord.Rows[i].Cells[5].Value = dt.Rows[i]["JONGMOK"].ToString();
                        GridRecord.Rows[i].Cells[6].Value = dt.Rows[i]["CUST_MANAGER"].ToString();
                        GridRecord.Rows[i].Cells[7].Value = dt.Rows[i]["CUST_COMP_PHONE"].ToString();
                        GridRecord.Rows[i].Cells[8].Value = dt.Rows[i]["STAFF_NM"].ToString();
                        GridRecord.Rows[i].Cells[9].Value = dt.Rows[i]["TAX_CD"].ToString();
                    }
                }
                else 
                {
                    GridRecord.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("검색중에 오류가 발생했습니다.");
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            bindData("where CUST_GUBUN = '" + sCustGbn + "' and  ( CUST_NM like '%" + txtSrch.Text.ToString() + "%' OR CUST_CD like '%" + txtSrch.Text.ToString() + "%' ) ");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GridRecord_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            sCode = GridRecord.Rows[e.RowIndex].Cells[0].Value.ToString();
            sName = GridRecord.Rows[e.RowIndex].Cells[2].Value.ToString();
            sTaxCd = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells[9].Value.ToString();

            this.Close();
        }

        private void txtSrch_Leave(object sender, EventArgs e)
        {
            bindData("where CUST_GUBUN = '" + sCustGbn + "' and ( CUST_NM like '%" + txtSrch.Text.ToString() + "%' OR CUST_CD like '%" + txtSrch.Text.ToString() + "%' ) ");
        }

        private void GridRecord_KeyDown(object sender, KeyEventArgs e)
        {
            if (GridRecord.CurrentCell == null || GridRecord.CurrentCell.RowIndex < 0)
            {
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                sCode = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells[0].Value.ToString();
                sName = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells[2].Value.ToString();
                sTaxCd = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells[9].Value.ToString();

                this.Close();
            }
        }
    }
}
