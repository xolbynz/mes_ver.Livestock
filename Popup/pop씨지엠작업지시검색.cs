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
    public partial class pop씨지엠작업지시검색 : Form
    {
        public DataGridView dgv;

        private int iCnt;
        private string strCondition = "";

        public string sRaw_mat_cd = "";
        public string sRaw_mat_nm = "";
        public string sInst_date = "";
        public string sInst_cd = "";
        public string sDelivery_date = "";
        public string sInst_notice = "";
        public string sComplete_yn = "";
        public string sInst_Amt = "";
        public string sLot_no = "";

        private bool bSearch = false;
        private int intTotalRecords = 0;
        private int intPageSize = 0;
        private int intPageCount = 0;
        private int intCurrentPage = 1;

        private int nPageSize = int.Parse(Common.p_PageSize);

        public pop씨지엠작업지시검색()
        {
            InitializeComponent();
        }

        private void pop씨지엠작업지시검색_Load(object sender, EventArgs e)
        {
            getSetting();
            bindData("where A.W_INST_DATE >= '" + start_date.Text.ToString() + "' and  A.W_INST_DATE <= '" + end_date.Text.ToString() + "' and A.COMPLETE_YN != '3' ");
        }

        private void getSetting() 
        {
            start_date.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
        }

        private void btnSrch_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("where A.W_INST_DATE >= '" + start_date.Text.ToString() + "' and  A.W_INST_DATE <= '" + end_date.Text.ToString() + "' and A.COMPLETE_YN != '3' ");
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
            dt = wDm.fn_Work_List(condition);

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
                        GridRecord.Rows[i].Cells["W_INST_DATE"].Value = dt.Rows[i]["W_INST_DATE"].ToString();
                        GridRecord.Rows[i].Cells["W_INST_CD"].Value = dt.Rows[i]["W_INST_CD"].ToString();
                        GridRecord.Rows[i].Cells["RAW_MAT_CD"].Value = dt.Rows[i]["RAW_MAT_CD"].ToString();
                        GridRecord.Rows[i].Cells["RAW_MAT_NM"].Value = dt.Rows[i]["RAW_MAT_NM"].ToString();
                        GridRecord.Rows[i].Cells["DELIVERY_DATE"].Value = dt.Rows[i]["DELIVERY_DATE"].ToString();
                        GridRecord.Rows[i].Cells["INST_NOTICE"].Value = dt.Rows[i]["INST_NOTICE"].ToString();
                        if (dt.Rows[i]["COMPLETE_YN"].ToString().Equals("2"))
                        {
                            GridRecord.Rows[i].Cells["COMPLETE_YN"].Value = "진행중";
                        }
                        else
                        {
                            GridRecord.Rows[i].Cells["COMPLETE_YN"].Value = "대기(창고이동필요)";
                        }
                        GridRecord.Rows[i].Cells["INST_AMT"].Value = decimal.Parse(dt.Rows[i]["INST_AMT"].ToString()).ToString("#,0.######");
                        GridRecord.Rows[i].Cells["LOT_NO"].Value = dt.Rows[i]["LOT_NO"].ToString();
                        
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

            sInst_date = "" + (string)GridRecord.Rows[iCnt].Cells["W_INST_DATE"].Value;
            sInst_cd = "" + (string)GridRecord.Rows[iCnt].Cells["W_INST_CD"].Value;
            sRaw_mat_cd = "" + (string)GridRecord.Rows[iCnt].Cells["RAW_MAT_CD"].Value;
            sRaw_mat_nm = "" + (string)GridRecord.Rows[iCnt].Cells["RAW_MAT_NM"].Value;
            sDelivery_date = "" + (string)GridRecord.Rows[iCnt].Cells["DELIVERY_DATE"].Value;
            sInst_notice = "" + (string)GridRecord.Rows[iCnt].Cells["INST_NOTICE"].Value;
            sComplete_yn = "" + (string)GridRecord.Rows[iCnt].Cells["COMPLETE_YN"].Value;
            sInst_Amt = "" + (string)GridRecord.Rows[iCnt].Cells["INST_AMT"].Value;
            sLot_no = "" + (string)GridRecord.Rows[iCnt].Cells["LOT_NO"].Value;

            this.Close();
        }
    }
}
