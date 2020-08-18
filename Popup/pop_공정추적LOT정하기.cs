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
    public partial class pop_공정추적LOT정하기 : Form
    {
        private int iCnt;
        public string strCondition = "";
        public string sLotNo = "";
        public string sDate = "";
        public string sCd = "";
        public string sSeq = "";
        public string sGubun = "";
        public string sAmt = "";
        public string sLabel = "";
        
        
        private bool bSearch = false;
        private int intTotalRecords = 0;
        private int intPageSize = 0;
        private int intPageCount = 0;
        private int intCurrentPage = 1;
        


        private int nPageSize = int.Parse(Common.p_PageSize);

        public DataTable dt = new DataTable();
        public pop_공정추적LOT정하기()
        {
            InitializeComponent();
        }

        private void pop_공정추적LOT정하기_Load(object sender, EventArgs e)
        {
            bindData();

            MessageBox.Show("하나이상의 생산공정이 조회됩니다. 조회를 원하는 품목을 선택해주세요.");
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

                

                this.lblStatus.Text = (intCurrentPage + 1).ToString() + " / " + intPageCount.ToString();




                if (dt.Rows.Count > 0)  
                {
                    GridRecord.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        GridRecord.Rows[i].Cells["INPUT_DATE"].Value = dt.Rows[i]["INPUT_DATE"].ToString();
                        GridRecord.Rows[i].Cells["INPUT_CD"].Value = dt.Rows[i]["INPUT_CD"].ToString();
                        GridRecord.Rows[i].Cells["INPUT_SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
                        GridRecord.Rows[i].Cells["UNIT_NM"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                        GridRecord.Rows[i].Cells["ITEM_NM"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                        GridRecord.Rows[i].Cells["LABEL_NM"].Value = dt.Rows[i]["LABEL_NM"].ToString();
                        GridRecord.Rows[i].Cells["A_UNION_CD"].Value = dt.Rows[i]["A_UNION_CD"].ToString();
                        GridRecord.Rows[i].Cells["LOT_NO"].Value = dt.Rows[i]["LOT_NO"].ToString();
                        GridRecord.Rows[i].Cells["INPUT_AMT"].Value = decimal.Parse(dt.Rows[i]["INPUT_AMT"].ToString()).ToString("#,0.######");
                        GridRecord.Rows[i].Cells["EXPRT_DATE"].Value = dt.Rows[i]["EXPRT_DATE"].ToString();
                        GridRecord.Rows[i].Cells["FROZEN_GUBUN"].Value = dt.Rows[i]["FROZEN_GUBUN"].ToString();
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
            sLotNo = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["LOT_NO"].Value.ToString();
            sDate = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["INPUT_DATE"].Value.ToString();
            sCd = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["INPUT_CD"].Value.ToString();
            sSeq = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["INPUT_SEQ"].Value.ToString();
            sAmt = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["INPUT_AMT"].Value.ToString();
            sLabel = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["LABEL_NM"].Value.ToString();
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
                sLotNo = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["LOT_NO"].Value.ToString();
                sDate = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["INPUT_DATE"].Value.ToString();
                sCd = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["INPUT_CD"].Value.ToString();
                sSeq = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["INPUT_SEQ"].Value.ToString();
                sAmt = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["INPUT_AMT"].Value.ToString();
                sLabel = GridRecord.Rows[GridRecord.CurrentCell.RowIndex].Cells["LABEL_NM"].Value.ToString();
                this.Close();
            }
        }
    }
}
