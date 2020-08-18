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
    public partial class pop작업지시검색 : Form
    {
        public DataGridView dgv;

        private int iCnt;
        private string strCondition = "";

        public string sCode = "";
        public string sName = "";

        private bool bSearch = false;
        private int intTotalRecords = 0;
        private int intPageSize = 0;
        private int intPageCount = 0;
        private int intCurrentPage = 1;

        private int nPageSize = int.Parse(Common.p_PageSize);

        public pop작업지시검색()
        {
            InitializeComponent();
        }

        private void pop작업지시검색_Load(object sender, EventArgs e)
        {
            getSetting();
        }

        private void getSetting() 
        {
            start_date.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
        }

        private void btnSrch_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("where A.W_INST_DATE >= '" + start_date.Text.ToString() + "' and  A.W_INST_DATE <= '" + end_date.Text.ToString() + "'");
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

                GridRecord.Columns[0].DefaultCellStyle.ForeColor = Color.Blue;
                GridRecord.Columns[1].DefaultCellStyle.ForeColor = Color.Blue;
                GridRecord.Columns[2].DefaultCellStyle.ForeColor = Color.Blue;

                GridRecord.Columns[0].Frozen = true;
                GridRecord.Columns[1].Frozen = true;
                GridRecord.Columns[2].Frozen = true;

                GridRecord.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GridRecord.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                GridRecord.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                GridRecord.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //GridRecord.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                this.dgv = GridRecord;
                this.lblStatus.Text = (intCurrentPage + 1).ToString() + " / " + intPageCount.ToString();

                if (dt.Rows.Count > 0)
                {
                    GridRecord.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        GridRecord.Rows[i].Cells[0].Value = dt.Rows[i]["LOT_NO"].ToString();
                        GridRecord.Rows[i].Cells[1].Value = dt.Rows[i]["ITEM_NM"].ToString();
                        GridRecord.Rows[i].Cells[2].Value = dt.Rows[i]["INST_AMT"].ToString();
                        GridRecord.Rows[i].Cells[3].Value = dt.Rows[i]["ITEM_CD"].ToString();
                        GridRecord.Rows[i].Cells[4].Value = dt.Rows[i]["SPEC"].ToString();
                        GridRecord.Rows[i].Cells[5].Value = dt.Rows[i]["W_INST_DATE"].ToString();
                        GridRecord.Rows[i].Cells[6].Value = dt.Rows[i]["W_INST_CD"].ToString();
                        GridRecord.Rows[i].Cells[7].Value = dt.Rows[i]["CHARGE_AMT"].ToString();
                        GridRecord.Rows[i].Cells[8].Value = dt.Rows[i]["PACK_AMT"].ToString();
                        GridRecord.Rows[i].Cells[9].Value = dt.Rows[i]["COMPLETE_YN"].ToString();
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

            dgv.Rows.Add();

            for (int j = 0; j < dgv.ColumnCount; j++)
            {
                dgv.Rows[0].Cells[j].Value = GridRecord.Rows[iCnt].Cells[j].Value;
            }

            sCode = "" + (string)GridRecord.Rows[iCnt].Cells[0].Value;

            this.Close();
        }
    }
}
