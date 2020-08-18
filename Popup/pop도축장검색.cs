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
    public partial class pop도축장검색 : Form
    {
        private int iCnt;
        private string strCondition = "";
        
        public string sCode = "";
        public string sName = "";
        public string sSlauHouseNm = "";
        

        private bool bSearch = false;
        private int intTotalRecords = 0;
        private int intPageSize = 0;
        private int intPageCount = 0;
        private int intCurrentPage = 1;

        private int nPageSize = int.Parse(Common.p_PageSize);
        public pop도축장검색()
        {
            InitializeComponent();
        }

        private void pop도축장검색_Load(object sender, EventArgs e)
        {
            bindData("where SLAUHOUSE_NM LIKE '%" + sSlauHouseNm + "%' ");
        }

        private void bindData(string condition)
        {
            this.GridRecord.DataSource = null;
            this.GridRecord.RowCount = 0;

            wnDm wDm = new wnDm();
            DataTable dt = null;
            dt = wDm.fn_SlauHouse_List(condition);

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

                
                this.lblStatus.Text = (intCurrentPage + 1).ToString() + " / " + intPageCount.ToString();

                if (dt.Rows.Count > 0)
                {
                    GridRecord.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        GridRecord.Rows[i].Cells[0].Value = dt.Rows[i]["SLAUHOUSE_CD"].ToString();
                        GridRecord.Rows[i].Cells[1].Value = dt.Rows[i]["SLAUHOUSE_NM"].ToString();
                        GridRecord.Rows[i].Cells[2].Value = dt.Rows[i]["COMMENT"].ToString();
                        
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
            bindData("where SLAUHOUSE_NM like '%" + txtSrch.Text.ToString() + "%' ");
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GridRecord_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            sCode = GridRecord.Rows[e.RowIndex].Cells[0].Value.ToString();
            sName = GridRecord.Rows[e.RowIndex].Cells[1].Value.ToString();

            this.Close();
        }

        private void txtSrch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bindData("where SLAUHOUSE_NM like '%" + txtSrch.Text.ToString() + "%' ");
            }
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
                sName = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells[1].Value.ToString();

                this.Close();
            }
        }
    }
}
