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
    public partial class pop검사기준검색 : Form
    {
        private int iCnt;
        private string strCondition = "";
        public string sCode = "";
        public string sName = "";
        public string sOrd = "";
        private bool bSearch = false;
        private int intTotalRecords = 0;
        private int intPageSize = 0;
        private int intPageCount = 0;
        private int intCurrentPage = 1;
        public int chk_gbn = 0;

        private int nPageSize = int.Parse(Common.p_PageSize);

        public DataTable dt = new DataTable();

        public pop검사기준검색()
        {
            InitializeComponent();
        }

        private void pop수입검사기준검색_Load(object sender, EventArgs e)
        {
            bindData();
        }

        private void bindData()
        {
            this.GridRecord.DataSource = null;
            this.GridRecord.RowCount = 0;

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

                GridRecord.Columns[0].Frozen = true;
                GridRecord.Columns[1].Frozen = true;

                GridRecord.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GridRecord.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                GridRecord.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                GridRecord.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                this.lblStatus.Text = (intCurrentPage + 1).ToString() + " / " + intPageCount.ToString();

                if (dt.Rows.Count > 0)
                {
                    GridRecord.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        GridRecord.Rows[i].Cells[0].Value = dt.Rows[i]["CHK_CD"].ToString();
                        GridRecord.Rows[i].Cells[1].Value = dt.Rows[i]["CHK_ORD"].ToString();
                        GridRecord.Rows[i].Cells[2].Value = dt.Rows[i]["CHK_NM"].ToString();
                        GridRecord.Rows[i].Cells[3].Value = dt.Rows[i]["COMMENT"].ToString();
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
            sCode = GridRecord.Rows[e.RowIndex].Cells[0].Value.ToString();
            sName = GridRecord.Rows[e.RowIndex].Cells[2].Value.ToString();
            sOrd = GridRecord.Rows[e.RowIndex].Cells[1].Value.ToString();
            this.Close();
        }
    }
}
