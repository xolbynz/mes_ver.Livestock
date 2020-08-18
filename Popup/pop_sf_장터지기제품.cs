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
    public partial class pop_sf_장터지기제품 : Form
    {
        private int iCnt;
        private string strCondition = "";
        public string sCode = "";
        public string sName = "";
        public string sSpec = "";
        public string sUnitCd = "";
        public string sUnitNm = "";
        public string sCharge = "";
        public string sPack = "";
        public string sInputPrice = "";
        public string sOutputPrice = "";
        public string sRetFlowYN = "";
        private bool bSearch = false;
        private int intTotalRecords = 0;
        private int intPageSize = 0;
        private int intPageCount = 0;
        private int intCurrentPage = 1;

        private int nPageSize = int.Parse(Common.p_PageSize);

        public DataTable dt = new DataTable();
        public pop_sf_장터지기제품()
        {
            InitializeComponent();
        }

        private void pop_sf_장터지기제품_Load(object sender, EventArgs e)
        {

            wnDm wDm = new wnDm();
            string condition = "where 사업자번호 = '" + Common.p_saupNo + "' ";
            //DataTable dt = null;



            dt = wDm.fn_Jang_Item_List(condition);

            bindData(condition);
        }

        private void bindData(string condition)
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
                GridRecord.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
                        if (dt.Rows[i]["사용여부"].ToString().Equals("1"))
                        {
                            GridRecord.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        }
                        else if (dt.Rows[i]["사용여부"].ToString().Equals("2"))
                        {
                            GridRecord.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                        else if (dt.Rows[i]["사용여부"].ToString().Equals("0"))
                        {
                            GridRecord.Rows[i].DefaultCellStyle.BackColor = Color.Empty;
                        }
                        GridRecord.Rows[i].Cells[0].Value = dt.Rows[i]["상품코드"].ToString();
                        GridRecord.Rows[i].Cells[1].Value = dt.Rows[i]["상품명"].ToString();
                        GridRecord.Rows[i].Cells[2].Value = //dt.Rows[i]["ITEM_GUBUN_NM"].ToString();
                            "ITEM_GUBUN_NM";
                        GridRecord.Rows[i].Cells[3].Value = dt.Rows[i]["규격"].ToString();
                        GridRecord.Rows[i].Cells[4].Value = //dt.Rows[i]["TYPE_NM"].ToString();
                            "TYPE_NM";
                        GridRecord.Rows[i].Cells[5].Value = //dt.Rows[i]["UNIT_NM"].ToString();
                            "UNIT_NM";
                        GridRecord.Rows[i].Cells[6].Value = //dt.Rows[i]["LINE_NM"].ToString();
                            "LINE_NM";
                        GridRecord.Rows[i].Cells[7].Value = decimal.Parse(dt.Rows[i]["낱개입고단가"].ToString()).ToString("#,0.######");
                        GridRecord.Rows[i].Cells[8].Value = decimal.Parse(dt.Rows[i]["낱개판매단가"].ToString()).ToString("#,0.######");
                        GridRecord.Rows[i].Cells[9].Value = dt.Rows[i]["현재재고"].ToString();
                        //GridRecord.Rows[i].Cells[10].Value = dt.Rows[i]["UNIT_CD"].ToString();
                        //GridRecord.Rows[i].Cells[11].Value = dt.Rows[i]["CHARGE_AMT"].ToString();
                        //GridRecord.Rows[i].Cells[12].Value = dt.Rows[i]["PACK_AMT"].ToString();
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
                Console.WriteLine(ex);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            wnDm wDm = new wnDm();
            //DataTable dt = null;
            string condition = "where 사업자번호 = '" + Common.p_saupNo + "' and 상품명 like  '%" + txtSrch.Text.ToString() + "%'";
            dt = wDm.fn_Jang_Item_List(condition);

            bindData(condition);
        }

        private void GridRecord_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            sCode = GridRecord.Rows[e.RowIndex].Cells["ITEM_CD"].Value.ToString();
            //sName = GridRecord.Rows[e.RowIndex].Cells["ITEM_NM"].Value.ToString();
            //sSpec = GridRecord.Rows[e.RowIndex].Cells["SPEC"].Value.ToString();
            //sUnitCd = GridRecord.Rows[e.RowIndex].Cells["UNIT_CD"].Value.ToString();
            //sUnitNm = GridRecord.Rows[e.RowIndex].Cells["UNIT_NM"].Value.ToString();
            //sOutputPrice = GridRecord.Rows[e.RowIndex].Cells["OUTPUT_PRICE"].Value.ToString();
            //sCharge = GridRecord.Rows[e.RowIndex].Cells["CHARGE_AMT"].Value.ToString();
            //sPack = GridRecord.Rows[e.RowIndex].Cells["PACK_AMT"].Value.ToString();
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
