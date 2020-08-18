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
    public partial class pop씨지엠주문서검색 : Form
    {
        public DataGridView dgv;

        private int iCnt;
        private string strCondition = "";

        public string sJumun_date = "";
        public string sJumun_cd = "";
        public string sDelivery_date = "";
        public string sComment = "";
        public string sCust_nm = "";
        public string sCust_cd = "";
        public string sTax_cd = "";
        public string sTax_nm = "";

        private bool bSearch = false;
        private int intTotalRecords = 0;
        private int intPageSize = 0;
        private int intPageCount = 0;
        private int intCurrentPage = 1;

        private int nPageSize = int.Parse(Common.p_PageSize);

        public pop씨지엠주문서검색()
        {
            InitializeComponent();
        }

        private void pop씨지엠주문서검색_Load(object sender, EventArgs e)
        {
            getSetting();
            bindData("where A.PLAN_DATE >= '" + start_date.Text.ToString() + "' and  A.PLAN_DATE <= '" + end_date.Text.ToString() + "' AND WORK_YN = 'N' ");
        }

        private void getSetting() 
        {
            start_date.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
        }

        private void btnSrch_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("where A.PLAN_DATE >= '" + start_date.Text.ToString() + "' and  A.PLAN_DATE <= '" + end_date.Text.ToString() + "' AND WORK_YN = 'N' ");
            bindData(sb.ToString());
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bindData(string condition)
        {
            this.GridRecord.DataSource = null;
            this.GridRecord.RowCount = 0;

            wnDm wDm = new wnDm();
            DataTable dt = null;
            dt = wDm.fn_Plan_List(condition);

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

                this.dgv = GridRecord;
                this.lblStatus.Text = (intCurrentPage + 1).ToString() + " / " + intPageCount.ToString();

                if (dt.Rows.Count > 0)
                {
                    GridRecord.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        GridRecord.Rows[i].Cells["PLAN_DATE"].Value = dt.Rows[i]["PLAN_DATE"].ToString();
                        GridRecord.Rows[i].Cells["PLAN_CD"].Value = dt.Rows[i]["PLAN_CD"].ToString();
                        GridRecord.Rows[i].Cells["PLAN_PRODUCTS"].Value = dt.Rows[i]["ITEM_CNT"].ToString();
                        GridRecord.Rows[i].Cells["DELIVERY_DATE"].Value = dt.Rows[i]["DELIVER_REQ_DATE"].ToString();
                        GridRecord.Rows[i].Cells["COMMENT"].Value = dt.Rows[i]["COMMENT"].ToString();
                        GridRecord.Rows[i].Cells["WORK_YN"].Value = dt.Rows[i]["WORK_YN"].ToString();
                        GridRecord.Rows[i].Cells["CUST_CD"].Value = dt.Rows[i]["CUST_CD"].ToString();
                        GridRecord.Rows[i].Cells["CUST_NM"].Value = dt.Rows[i]["CUST_NM"].ToString();
                        GridRecord.Rows[i].Cells["TAX_CD"].Value = dt.Rows[i]["TAX_CD"].ToString();
                        GridRecord.Rows[i].Cells["TAX_NM"].Value = dt.Rows[i]["TAX_NM"].ToString();
                        
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

        private void GridRecord_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GridRecord.CurrentCell == null) return;

            iCnt = GridRecord.CurrentCell.RowIndex;

            sJumun_date = "" + (string)GridRecord.Rows[iCnt].Cells["PLAN_DATE"].Value;
            sJumun_cd = "" + (string)GridRecord.Rows[iCnt].Cells["PLAN_CD"].Value;
            sDelivery_date = "" + (string)GridRecord.Rows[iCnt].Cells["DELIVERY_DATE"].Value;
            sComment = "" + (string)GridRecord.Rows[iCnt].Cells["COMMENT"].Value;
            sCust_nm = "" + (string)GridRecord.Rows[iCnt].Cells["CUST_NM"].Value;
            sCust_cd = "" + (string)GridRecord.Rows[iCnt].Cells["CUST_CD"].Value;
            sTax_cd = "" + (string)GridRecord.Rows[iCnt].Cells["TAX_CD"].Value;
            sTax_nm = "" + (string)GridRecord.Rows[iCnt].Cells["TAX_NM"].Value;

            this.Close();
        }
    }
}
