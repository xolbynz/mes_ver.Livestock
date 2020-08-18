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

namespace 스마트팩토리.P50_QUA
{
    public partial class frm생산보고서조회 : Form
    {
        public frm생산보고서조회()
        {
            InitializeComponent();
        }

        private void frm생산보고서조회_Load(object sender, EventArgs e)
        {
            chk_srch_day.Checked = true;
            cmb_month.Format = DateTimePickerFormat.Custom;
            cmb_month.CustomFormat = "yyyy-MM"; 
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void ccpChkGrid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void ccpChkGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void resetSetting()
        {


            cmb_datetime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            cmb_datetime2.Text = DateTime.Now.ToString("yyyy-MM-dd");
            cmb_month.Text = DateTime.Now.ToString("yyyy-MM");

            

            //getHaccpGrid();
        }

        private void inputPlanGrid(string condition, string condition2)
        {
            wnDm wDm = new wnDm();
            DataTable dt = null;
            //"where A.INPUT_DATE = '" + txt_chk_date.Text.ToString() + "' and A.INPUT_CD = '" + txt_input_cd.Text.ToString() + "'  "
            dt = wDm.fn_GroupByPlanList(condition);
            planGridView.Rows.Clear();
            flowGridView.Rows.Clear();


            if (dt != null && dt.Rows.Count > 0)
            {
                
                planGridView.RowCount = dt.Rows.Count;
                for (int i = 0; i < planGridView.Rows.Count; i++)
                {
                    planGridView.Rows[i].Cells["ITEM_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                    planGridView.Rows[i].Cells["ITEM_NM"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                    planGridView.Rows[i].Cells["SPEC"].Value = dt.Rows[i]["SPEC"].ToString();
                    planGridView.Rows[i].Cells["UNIT_CD"].Value = dt.Rows[i]["UNIT_CD"].ToString();
                    planGridView.Rows[i].Cells["UNIT_NM"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                    planGridView.Rows[i].Cells["SUM_AMT"].Value = dt.Rows[i]["SUM_AMT"].ToString();
                    

                    
                }
            }


            dt = null;
            //"where A.INPUT_DATE = '" + txt_chk_date.Text.ToString() + "' and A.INPUT_CD = '" + txt_input_cd.Text.ToString() + "'  "
            dt = wDm.fn_GroupByFlowList(condition2);
            flowGridView.Rows.Clear();


            if (dt != null && dt.Rows.Count > 0)
            {
               
                flowGridView.RowCount = dt.Rows.Count;
                for (int i = 0; i < flowGridView.Rows.Count; i++)
                {
                   


                    flowGridView.Rows[i].Cells["F_ITEM_NM"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                    flowGridView.Rows[i].Cells["F_INST_AMT"].Value = dt.Rows[i]["TOTAL_INST_AMT"].ToString();
                    flowGridView.Rows[i].Cells["F_SPEC"].Value = dt.Rows[i]["SPEC"].ToString();
                    flowGridView.Rows[i].Cells["F_ITEM_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                    flowGridView.Rows[i].Cells["F_LOSS"].Value = dt.Rows[i]["LOSS"].ToString();
                    flowGridView.Rows[i].Cells["F_POOR_AMT"].Value = dt.Rows[i]["POOR_AMT"].ToString();
                    flowGridView.Rows[i].Cells["F_SUM_AMT"].Value = dt.Rows[i]["SUM_AMT"].ToString();



                }
            }
        }

        private void chk_srch_day_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_srch_day.Checked == true)
            {
                chk_srch_month.Checked = false;
            }
        }

        private void chk_srch_month_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_srch_month.Checked == true)
            {
                chk_srch_day.Checked = false;
            }
        }

        private void btn_srch_Click(object sender, EventArgs e)
        {
            string condition = "";
            string condition2 = "";
            if (chk_srch_day.Checked == true)
            {
                condition = "where A.PLAN_DATE >= '" + cmb_datetime.Text.ToString() + "' and A.PLAN_DATE <= '" + cmb_datetime2.Text.ToString() + "'  ";
                condition2 = "   where A.W_INST_DATE >= '" + cmb_datetime.Text.ToString() + "' and A.W_INST_DATE <= '" + cmb_datetime2.Text.ToString() + "'    ";
            }
            else 
            {
                condition = "where A.PLAN_DATE like '" + cmb_month.Text.ToString() + "%'  ";
                condition2 = "where A.W_INST_DATE like '" + cmb_month.Text.ToString() + "%'  ";
            }
                

            inputPlanGrid(condition, condition2);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

    }
}
