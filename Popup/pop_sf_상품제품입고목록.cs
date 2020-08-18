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
    public partial class pop_sf_상품제품입고목록 : Form
    {
        private int iCnt;
        public string strCondition = "";
        public string sGubun = "";
        public string sCode = "";
        public string sInputDate = "";
        public string sInputCd = "";
        public string sInputSeq = "";
        public string sTotalAmt = "";
        public string sExprt_date = "";

        public string sLabel_nm = "";
        public string sChugjong_nm = "";
        public string sClass_nm = "";
        public string sCountry_nm = "";

        public string sStore_gubun = "";
        public string sStore_nm = "";


        
        
        public string sName = "";
        public string sSpec = "";
        
        private bool bSearch = false;
        private int intTotalRecords = 0;
        private int intPageSize = 0;
        private int intPageCount = 0;
        private int intCurrentPage = 1;
        


        private int nPageSize = int.Parse(Common.p_PageSize);

        public DataTable dt = new DataTable();
        public pop_sf_상품제품입고목록()
        {
            InitializeComponent();
        }

        private void pop_sf_상품제품입고목록_Load(object sender, EventArgs e)
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
                        if (sGubun.Equals("상품"))
                        {
                            GridRecord.Rows[i].Cells["ITEM_CD"].Value = dt.Rows[i]["RAW_MAT_CD"].ToString();
                            GridRecord.Rows[i].Cells["ITEM_NM"].Value = dt.Rows[i]["RAW_MAT_NM"].ToString();
                        }
                        else
                        {
                            GridRecord.Rows[i].Cells["ITEM_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                            GridRecord.Rows[i].Cells["ITEM_NM"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                        }
                        GridRecord.Rows[i].Cells["INPUT_DATE"].Value = dt.Rows[i]["INPUT_DATE"].ToString();
                        GridRecord.Rows[i].Cells["INPUT_CD"].Value = dt.Rows[i]["INPUT_CD"].ToString();
                        GridRecord.Rows[i].Cells["INPUT_SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
                        GridRecord.Rows[i].Cells["TYPE_CD"].Value = dt.Rows[i]["TYPE_CD"].ToString();
                        GridRecord.Rows[i].Cells["UNIT_NM"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                        GridRecord.Rows[i].Cells["UNIT_CD"].Value = dt.Rows[i]["UNIT_CD"].ToString();
                        GridRecord.Rows[i].Cells["LABEL_NM"].Value = dt.Rows[i]["LABEL_NM"].ToString();
                        GridRecord.Rows[i].Cells["CHUGJONG_CD"].Value = dt.Rows[i]["CHUGJONG_CD"].ToString();
                        GridRecord.Rows[i].Cells["CHUGJONG_NM"].Value = dt.Rows[i]["CHUGJONG_NM"].ToString();
                        GridRecord.Rows[i].Cells["CLASS_CD"].Value = dt.Rows[i]["CLASS_CD"].ToString();
                        GridRecord.Rows[i].Cells["CLASS_NM"].Value = dt.Rows[i]["CLASS_NM"].ToString();
                        GridRecord.Rows[i].Cells["COUNTRY_CD"].Value = dt.Rows[i]["COUNTRY_CD"].ToString();
                        GridRecord.Rows[i].Cells["COUNTRY_NM"].Value = dt.Rows[i]["COUNTRY_NM"].ToString();
                        GridRecord.Rows[i].Cells["HAMYANG"].Value = dt.Rows[i]["HAMYANG"].ToString();
                        GridRecord.Rows[i].Cells["CURR_AMT"].Value = decimal.Parse(dt.Rows[i]["CURR_AMT"].ToString()).ToString("#,0.######");
                        GridRecord.Rows[i].Cells["EXPRT_DATE"].Value = dt.Rows[i]["EXPRT_DATE"].ToString();
                        GridRecord.Rows[i].Cells["STORE_GUBUN"].Value = dt.Rows[i]["STORE_GUBUN"].ToString();
                        if (dt.Rows[i]["STORE_GUBUN"].ToString().Equals("STORE_1F"))
                        {
                            GridRecord.Rows[i].Cells["STORE_NM"].Value = "냉동";
                        }
                        else if (dt.Rows[i]["STORE_GUBUN"].ToString().Equals("STORE_1NF"))
                        {
                            GridRecord.Rows[i].Cells["STORE_NM"].Value = "냉장";
                        }
                        else if (dt.Rows[i]["STORE_GUBUN"].ToString().Equals("STORE_UF"))
                        {
                            GridRecord.Rows[i].Cells["STORE_NM"].Value = "해동";
                        }
                        else if (dt.Rows[i]["STORE_GUBUN"].ToString().Equals("REMAIN_AMT"))
                        {
                            GridRecord.Rows[i].Cells["STORE_NM"].Value = "대기";
                        }
                        else
                        {
                            GridRecord.Rows[i].Cells["STORE_NM"].Value = "제품";
                        }
                        
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

        

        private void GridRecord_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            sCode = GridRecord.Rows[e.RowIndex].Cells["ITEM_CD"].Value.ToString();
            sName = GridRecord.Rows[e.RowIndex].Cells["ITEM_NM"].Value.ToString();
            sInputDate = GridRecord.Rows[e.RowIndex].Cells["INPUT_DATE"].Value.ToString();
            sInputCd = GridRecord.Rows[e.RowIndex].Cells["INPUT_CD"].Value.ToString();
            sInputSeq = GridRecord.Rows[e.RowIndex].Cells["INPUT_SEQ"].Value.ToString();
            sTotalAmt = GridRecord.Rows[e.RowIndex].Cells["CURR_AMT"].Value.ToString();
            sExprt_date = GridRecord.Rows[e.RowIndex].Cells["EXPRT_DATE"].Value.ToString();
            sLabel_nm = GridRecord.Rows[e.RowIndex].Cells["LABEL_NM"].Value.ToString();
            sChugjong_nm = GridRecord.Rows[e.RowIndex].Cells["CHUGJONG_NM"].Value.ToString();
            sClass_nm = GridRecord.Rows[e.RowIndex].Cells["CLASS_NM"].Value.ToString();
            sCountry_nm = GridRecord.Rows[e.RowIndex].Cells["COUNTRY_NM"].Value.ToString();
            sStore_gubun = GridRecord.Rows[e.RowIndex].Cells["STORE_GUBUN"].Value.ToString();
            sStore_nm = GridRecord.Rows[e.RowIndex].Cells["STORE_NM"].Value.ToString();
            

            
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GridRecord_KeyDown(object sender, KeyEventArgs e)
        {
            if (GridRecord.CurrentCell == null || GridRecord.CurrentCell.RowIndex < 0)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                sCode = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["ITEM_CD"].Value.ToString();
                sName = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["ITEM_NM"].Value.ToString();
                sInputDate = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["INPUT_DATE"].Value.ToString();
                sInputCd = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["INPUT_CD"].Value.ToString();
                sInputSeq = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["INPUT_SEQ"].Value.ToString();
                sTotalAmt = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["CURR_AMT"].Value.ToString();
                sExprt_date = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["EXPRT_DATE"].Value.ToString();
                sLabel_nm = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["LABEL_NM"].Value.ToString();
                sChugjong_nm = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["CHUGJONG_NM"].Value.ToString();
                sClass_nm = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["CLASS_NM"].Value.ToString();
                sCountry_nm = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["COUNTRY_NM"].Value.ToString();
                sStore_gubun = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["STORE_GUBUN"].Value.ToString();
                sStore_nm = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["STORE_NM"].Value.ToString();



                this.Close();
            }
        }
    }
}
