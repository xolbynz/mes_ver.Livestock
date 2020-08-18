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
    public partial class pop공정검색 : Form
    {
        private int iCnt;
        private string strCondition = "";
        public string sRetCode = "";
        public string sRetName = "";
        public string sRetFlowYN = "";
        private bool bSearch = false;
        private int intTotalRecords = 0;
        private int intPageSize = 0;
        private int intPageCount = 0;
        private int intCurrentPage = 1;
        private int nPageSize = int.Parse(Common.p_PageSize);

        public DataTable dt = new DataTable();

        public pop공정검색()
        {
            InitializeComponent();
        }
        
        private void pop공정검색_Load(object sender, EventArgs e)
        {
            this.bindData();
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

            if (dt != null)
            {
                intCount = dt.Rows.Count;
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

                //this.GridRecord.DataSource = dt;

                flow_list_rs(dt);

                GridRecord.Columns[0].DefaultCellStyle.ForeColor = Color.Blue;
                GridRecord.Columns[1].DefaultCellStyle.ForeColor = Color.Blue;

                GridRecord.Columns[0].Frozen = true;
                GridRecord.Columns[1].Frozen = true;

                GridRecord.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GridRecord.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GridRecord.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                GridRecord.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                GridRecord.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
            wnDm wDm = new wnDm();
            DataTable dt = new DataTable();

            dt = wDm.fn_Flow_List("where FLOW_NM like '%" + txtSrch.Text.ToString() + "%' ");

            flow_list_rs(dt);
        }

        private void flow_list_rs(DataTable dt)
        {
            this.GridRecord.RowCount = dt.Rows.Count;
            for (int i = 0; i < GridRecord.Rows.Count; i++)
            {
                GridRecord.Rows[i].Cells["FLOW_CD"].Value = dt.Rows[i]["FLOW_CD"].ToString();
                GridRecord.Rows[i].Cells["FLOW_NM"].Value = dt.Rows[i]["FLOW_NM"].ToString();
                GridRecord.Rows[i].Cells["STORAGE_NM"].Value = dt.Rows[i]["STORAGE_NM"].ToString();
                GridRecord.Rows[i].Cells["POOR_TYPE_NM"].Value = dt.Rows[i]["POOR_TYPE_NM"].ToString();
                GridRecord.Rows[i].Cells["STAFF_NM"].Value = dt.Rows[i]["STAFF_NM"].ToString();
                GridRecord.Rows[i].Cells["FLOW_YN"].Value = dt.Rows[i]["FLOW_INSERT_YN"].ToString();
            }
        }

        private void GridRecord_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GridRecord.CurrentCell == null) return;

            iCnt = GridRecord.CurrentCell.RowIndex;
            sRetCode = "" + (string)GridRecord.Rows[iCnt].Cells["FLOW_CD"].Value;
            sRetName = "" + (string)GridRecord.Rows[iCnt].Cells["FLOW_NM"].Value;
            sRetFlowYN = "" + (string)GridRecord.Rows[iCnt].Cells["FLOW_YN"].Value;
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
