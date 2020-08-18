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

namespace 스마트팩토리.P40_ITM
{
    public partial class frmLOTNO추적조회 : Form
    {
        public frmLOTNO추적조회()
        {
            InitializeComponent();
        }

        #region button logic

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSrch_Click(object sender, EventArgs e)
        {
            lot_logic();
        }
        #endregion button logic

        private void lot_logic()
        {
            try
            {
                if (txt_lot_bar.Text.ToString().Equals(""))
                {
                    MessageBox.Show("LOT_NO를 입력하시기 바랍니다.");
                    return;
                }

                DataTable dt = new DataTable();
                wnDm wDm = new wnDm();
                dt = wDm.fn_Lot_Item_Srch_List(txt_lot_bar.Text.ToString());

                if (dt != null && dt.Rows.Count > 0)
                {
                    txt_item_cd.Text = dt.Rows[0]["ITEM_CD"].ToString();
                    txt_item_nm.Text = dt.Rows[0]["ITEM_NM"].ToString();
                    txt_spec.Text = dt.Rows[0]["SPEC"].ToString();
                    txt_work_date.Text = dt.Rows[0]["W_INST_DATE"].ToString();
                    txt_inst_amt.Text = (decimal.Parse(dt.Rows[0]["INST_AMT"].ToString())).ToString("#,0.######");
                    lot_detail();
                }
                else
                {
                    MessageBox.Show("데이터가 없습니다.");
                    return;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("시스템 에러: " + e.Message.ToString());
            }
        }

        private void lot_detail()
        {
            try
            {
                DataTable dt = new DataTable();
                wnDm wDm = new wnDm();
                dt = wDm.fn_Lot_Detail(txt_lot_bar.Text.ToString());

                lotGrid.Rows.Clear();
                if (dt != null && dt.Rows.Count > 0)
                {
                   lotGrid.RowCount = dt.Rows.Count;
                   for (int i = 0; i < dt.Rows.Count; i++) 
                   {
                       lotGrid.Rows[i].Cells["F_SUB_DATE"].Value = dt.Rows[i]["F_SUB_DATE"].ToString();
                       lotGrid.Rows[i].Cells["RAW_MAT_NM"].Value = dt.Rows[i]["RAW_MAT_NM"].ToString();
                       lotGrid.Rows[i].Cells["TOTAL_AMT"].Value = (decimal.Parse(dt.Rows[i]["TOTAL_AMT"].ToString())).ToString("#,0.######");
                       lotGrid.Rows[i].Cells["W_INST_DATE"].Value = dt.Rows[i]["W_INST_DATE"].ToString();
                       lotGrid.Rows[i].Cells["W_INST_CD"].Value = dt.Rows[i]["W_INST_CD"].ToString();
                       //lotGrid.Rows[i].Cells["FLOW_CD"].Value = dt.Rows[i]["FLOW_CD"].ToString();
                       lotGrid.Rows[i].Cells["FLOW_NM"].Value = dt.Rows[i]["FLOW_NM"].ToString();
                       lotGrid.Rows[i].Cells["F_SUB_AMT"].Value = (decimal.Parse(dt.Rows[i]["F_SUB_AMT"].ToString())).ToString("#,0.######");
                       lotGrid.Rows[i].Cells["LOSS"].Value = dt.Rows[i]["LOSS"].ToString();
                       lotGrid.Rows[i].Cells["POOR_AMT"].Value = (decimal.Parse(dt.Rows[i]["POOR_AMT"].ToString())).ToString("#,0.######");
                       //lotGrid.Rows[i].Cells["LOT_NO"].Value = dt.Rows[i]["LOT_NO"].ToString();
                       //lotGrid.Rows[i].Cells["ITEM_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                       lotGrid.Rows[i].Cells["SALES_CUST_CD"].Value = dt.Rows[i]["SALES_CUST_CD"].ToString();
                       lotGrid.Rows[i].Cells["SALES_CUST_NM"].Value = dt.Rows[i]["SALES_CUST_NM"].ToString();
                       lotGrid.Rows[i].Cells["F_STEP"].Value = dt.Rows[i]["F_STEP"].ToString();
                       lotGrid.Rows[i].Cells["OUTPUT_DATE"].Value = dt.Rows[i]["OUTPUT_DATE"].ToString();
                       lotGrid.Rows[i].Cells["INPUT_DATE"].Value = dt.Rows[i]["INPUT_DATE"].ToString();
                       lotGrid.Rows[i].Cells["PUR_CUST_CD"].Value = dt.Rows[i]["PUR_CUST_CD"].ToString();
                       lotGrid.Rows[i].Cells["PUR_CUST_NM"].Value = dt.Rows[i]["PUR_CUST_NM"].ToString();

                   }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("lot 조회 상세 에러: " + e.Message.ToString());
            }
        }
    }
}
