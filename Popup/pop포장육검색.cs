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
    public partial class pop포장육검색 : Form
    {
        private int iCnt;
        private string strCondition = "";
        public string sRetCode = "";

        public DataGridView dgv;
        private bool bSearch = false;
        private int intTotalRecords = 0;
        private int intPageSize = 0;
        private int intPageCount = 0;
        private int intCurrentPage = 1;
        private int nPageSize = int.Parse(Common.p_PageSize);

        public DataTable dt = new DataTable();

        public pop포장육검색()
        {
            InitializeComponent();
        }

        private void pop포장육검색_Load(object sender, EventArgs e)
        {
           // strCondition = this.makeSearchCondition();
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
                Console.WriteLine("1");
                int intSkip = 0;
                intSkip = (intCurrentPage * intPageSize);
                Console.WriteLine("12");
                //this.GridRecord.DataSource = dt;

                raw_list_rs(dt);
                Console.WriteLine("13");
                GridRecord.Columns[0].DefaultCellStyle.ForeColor = Color.Blue;
                GridRecord.Columns[1].DefaultCellStyle.ForeColor = Color.Blue;
                Console.WriteLine("14");
                GridRecord.Columns[0].Frozen = true;
                GridRecord.Columns[1].Frozen = true;
                Console.WriteLine("15");
                GridRecord.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GridRecord.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                GridRecord.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                GridRecord.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                GridRecord.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                GridRecord.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                GridRecord.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                GridRecord.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                GridRecord.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                Console.WriteLine("16");
                GridRecord.Columns["INPUT_PRICE"].DefaultCellStyle.Format = "#,0";
                GridRecord.Columns["OUTPUT_PRICE"].DefaultCellStyle.Format = "#,0";
                Console.WriteLine("17");
                this.dgv = GridRecord;
                Console.WriteLine("18");
                this.lblStatus.Text = (intCurrentPage + 1).ToString() + " / " + intPageCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("검색중에 오류가 발생했습니다.");
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
                Console.WriteLine(ex);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            wnDm wDm = new wnDm();
            DataTable dt = new DataTable();

            dt = wDm.fn_Raw_List("where RAW_MAT_NM like '%" + txtSrch.Text.ToString() + "%' where RAW_MAT_GUBUN = '1' ");

            raw_list_rs(dt);
        }

        private void raw_list_rs(DataTable dt) 
        {
            this.GridRecord.RowCount = dt.Rows.Count;
            for (int i = 0; i < GridRecord.Rows.Count; i++)
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
                GridRecord.Rows[i].Cells["RAW_MAT_CD"].Value = dt.Rows[i]["RAW_MAT_CD"].ToString();
                GridRecord.Rows[i].Cells["RAW_MAT_NM"].Value = dt.Rows[i]["RAW_MAT_NM"].ToString();
                GridRecord.Rows[i].Cells["SPEC"].Value = dt.Rows[i]["SPEC"].ToString();
                GridRecord.Rows[i].Cells["RAW_MAT_GUBUN_NM"].Value = dt.Rows[i]["RAW_MAT_GUBUN_NM"].ToString();
                GridRecord.Rows[i].Cells["RAW_MAT_GUBUN_CD"].Value = dt.Rows[i]["RAW_MAT_GUBUN"].ToString();
                GridRecord.Rows[i].Cells["TYPE_NM"].Value = dt.Rows[i]["TYPE_NM"].ToString();
                GridRecord.Rows[i].Cells["INPUT_UNIT"].Value = dt.Rows[i]["INPUT_UNIT"].ToString();
                GridRecord.Rows[i].Cells["OUTPUT_UNIT"].Value = dt.Rows[i]["OUTPUT_UNIT"].ToString();

                GridRecord.Rows[i].Cells["INPUT_UNIT_NM"].Value = dt.Rows[i]["INPUT_UNIT_NM"].ToString();
                GridRecord.Rows[i].Cells["OUTPUT_UNIT_NM"].Value = dt.Rows[i]["OUTPUT_UNIT_NM"].ToString();

                GridRecord.Rows[i].Cells["INPUT_PRICE"].Value = decimal.Parse(dt.Rows[i]["INPUT_PRICE"].ToString()).ToString("#,0.######");

                GridRecord.Rows[i].Cells["OUTPUT_PRICE"].Value = decimal.Parse(dt.Rows[i]["OUTPUT_PRICE"].ToString()).ToString("#,0.######");
                if (dt.Rows[i]["BAL_STOCK"].ToString().Equals(""))
                {
                    dt.Rows[i]["BAL_STOCK"] = 0.000000;
                }
                GridRecord.Rows[i].Cells["BAL_STOCK"].Value = decimal.Parse(dt.Rows[i]["BAL_STOCK"].ToString()).ToString("#,0.######"); ;

                GridRecord.Rows[i].Cells["CUST_CD"].Value = dt.Rows[i]["CUST_CD"].ToString();
                GridRecord.Rows[i].Cells["CUST_NM"].Value = dt.Rows[i]["CUST_NM"].ToString();

                GridRecord.Rows[i].Cells["CVR_RATIO"].Value = dt.Rows[i]["CVR_RATIO"].ToString();
                GridRecord.Rows[i].Cells["CHUGJONG_CD"].Value = dt.Rows[i]["CHUGJONG_CD"].ToString();
                GridRecord.Rows[i].Cells["CHUGJONG_NM"].Value = dt.Rows[i]["CHUGJONG_NM"].ToString();
                GridRecord.Rows[i].Cells["CLASS_CD"].Value = dt.Rows[i]["CLASS_CD"].ToString();
                GridRecord.Rows[i].Cells["CLASS_NM"].Value = dt.Rows[i]["CLASS_NM"].ToString();
                GridRecord.Rows[i].Cells["GRADE_CD"].Value = dt.Rows[i]["GRADE_CD"].ToString();
                GridRecord.Rows[i].Cells["GRADE_NM"].Value = dt.Rows[i]["GRADE_NM"].ToString();
                GridRecord.Rows[i].Cells["COUNTRY_CD"].Value = dt.Rows[i]["COUNTRY_CD"].ToString();
                GridRecord.Rows[i].Cells["COUNTRY_NM"].Value = dt.Rows[i]["COUNTRY_NM"].ToString();
                GridRecord.Rows[i].Cells["LABEL_NM"].Value = dt.Rows[i]["LABEL_NM"].ToString();
                GridRecord.Rows[i].Cells["BOX_AMT"].Value = dt.Rows[i]["BOX_AMT"].ToString();
                GridRecord.Rows[i].Cells["TYPE_CD"].Value = dt.Rows[i]["TYPE_CD"].ToString();


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

            sRetCode = "" + (string)GridRecord.Rows[iCnt].Cells["RAW_MAT_CD"].Value;





            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
