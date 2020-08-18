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
    public partial class pop_sf_상품제품검색 : Form
    {
        private int iCnt;
        private string strCondition = "";
        public string sGubun = "";
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
        public string sChugjong_cd = "";
        public string sChugjong_NM = "";
        public string sClass_cd = "";
        public string sClass_nm = "";
        public string sCountry_cd = "";
        public string sCountry_nm = "";
        public string sType_cd = "";
        public string sType_nm = "";
        public string sLabelNM = "";
        private bool bSearch = false;
        private int intTotalRecords = 0;
        private int intPageSize = 0;
        private int intPageCount = 0;
        private int intCurrentPage = 1;


        private int nPageSize = int.Parse(Common.p_PageSize);

        public DataTable dt = new DataTable();
        public pop_sf_상품제품검색()
        {
            InitializeComponent();
        }

        private void pop_sf_상품제품검색_Load(object sender, EventArgs e)
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
                        GridRecord.Rows[i].Cells["RAW_ITEM_GUBUN"].Value = dt.Rows[i]["RAW_ITEM_GUBUN"].ToString().Equals("1")? "상품" : "제품";
                        GridRecord.Rows[i].Cells["ITEM_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                        GridRecord.Rows[i].Cells["ITEM_NM"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                        GridRecord.Rows[i].Cells["TYPE_NM"].Value = dt.Rows[i]["TYPE_NM"].ToString();
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
            dt = wDm.fn_Raw_Item_List("where A.RAW_MAT_NM like '%" + txtSrch.Text + "%'  OR  A.RAW_MAT_CD like '%" + txtSrch.Text + "%'   OR  A.LABEL_NM like '%" + txtSrch.Text + "%'  "
                , "where  B.ITEM_NM like '%" + txtSrch.Text + "%'   OR  B.ITEM_CD like '%" + txtSrch.Text + "%'   OR  B.LABEL_NM like '%" + txtSrch.Text + "%' ");

            bindData();
        }

        private void GridRecord_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            sCode = GridRecord.Rows[e.RowIndex].Cells["ITEM_CD"].Value.ToString();
            sName = GridRecord.Rows[e.RowIndex].Cells["ITEM_NM"].Value.ToString();
            sUnitCd = GridRecord.Rows[e.RowIndex].Cells["UNIT_CD"].Value.ToString();
            sUnitNm = GridRecord.Rows[e.RowIndex].Cells["UNIT_NM"].Value.ToString();
            sLabelNM = GridRecord.Rows[e.RowIndex].Cells["LABEL_NM"].Value.ToString();
            sChugjong_cd = GridRecord.Rows[e.RowIndex].Cells["CHUGJONG_CD"].Value.ToString();
            sChugjong_NM = GridRecord.Rows[e.RowIndex].Cells["CHUGJONG_NM"].Value.ToString();
            sClass_cd = GridRecord.Rows[e.RowIndex].Cells["CLASS_CD"].Value.ToString();
            sClass_nm = GridRecord.Rows[e.RowIndex].Cells["CLASS_NM"].Value.ToString();
            sCountry_cd = GridRecord.Rows[e.RowIndex].Cells["COUNTRY_CD"].Value.ToString();
            sCountry_nm = GridRecord.Rows[e.RowIndex].Cells["COUNTRY_NM"].Value.ToString();
            sGubun = GridRecord.Rows[e.RowIndex].Cells["RAW_ITEM_GUBUN"].Value.ToString();
            sType_cd = GridRecord.Rows[e.RowIndex].Cells["TYPE_CD"].Value.ToString();
            sType_nm = GridRecord.Rows[e.RowIndex].Cells["TYPE_NM"].Value.ToString();
            

            
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSrch_Leave(object sender, EventArgs e)
        {
            wnDm wDm = new wnDm();
            dt = wDm.fn_Raw_Item_List("where A.RAW_MAT_NM like '%" + txtSrch.Text + "%'  OR  A.RAW_MAT_CD like '%" + txtSrch.Text + "%'   OR  A.LABEL_NM like '%" + txtSrch.Text + "%'  "
                , "where  B.ITEM_NM like '%" + txtSrch.Text + "%'   OR  B.ITEM_CD like '%" + txtSrch.Text + "%'   OR  B.LABEL_NM like '%" + txtSrch.Text + "%' ");

            bindData();
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
                sUnitCd = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["UNIT_CD"].Value.ToString();
                sUnitNm = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["UNIT_NM"].Value.ToString();
                sLabelNM = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["LABEL_NM"].Value.ToString();
                sChugjong_cd = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["CHUGJONG_CD"].Value.ToString();
                sChugjong_NM = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["CHUGJONG_NM"].Value.ToString();
                sClass_cd = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["CLASS_CD"].Value.ToString();
                sClass_nm = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["CLASS_NM"].Value.ToString();
                sCountry_cd = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["COUNTRY_CD"].Value.ToString();
                sCountry_nm = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["COUNTRY_NM"].Value.ToString();
                sGubun = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["RAW_ITEM_GUBUN"].Value.ToString();
                sType_cd = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["TYPE_CD"].Value.ToString();
                sType_nm = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["TYPE_NM"].Value.ToString();
                this.Close();
            }
        }

    }
}
