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
    public partial class frm제품출고이력현황 : Form
    {
        public frm제품출고이력현황()
        {
            InitializeComponent();
        }

        private void frm제품출고이력현황_Load(object sender, EventArgs e)
        {
            start_date.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSrch_Click(object sender, EventArgs e)
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Item_Output_Detail_List("where A.OUTPUT_DATE >= '" + start_date.Text.ToString() + "' and  A.OUTPUT_DATE <= '" + end_date.Text.ToString() + "'");

                this.itemOutGrid.RowCount = dt.Rows.Count;
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        itemOutGrid.Rows[i].Cells["OUTPUT_DATE"].Value = dt.Rows[i]["OUTPUT_DATE"].ToString();
                        itemOutGrid.Rows[i].Cells["OUTPUT_CD"].Value = dt.Rows[i]["OUTPUT_CD"].ToString();
                        itemOutGrid.Rows[i].Cells["SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
                        itemOutGrid.Rows[i].Cells["LOT_NO"].Value = dt.Rows[i]["LOT_NO"].ToString();
                        itemOutGrid.Rows[i].Cells["CUST_NM"].Value = dt.Rows[i]["CUST_NM"].ToString();
                        itemOutGrid.Rows[i].Cells["ITEM_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                        itemOutGrid.Rows[i].Cells["ITEM_NM"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                        itemOutGrid.Rows[i].Cells["SPEC"].Value = dt.Rows[i]["SPEC"].ToString();
                        itemOutGrid.Rows[i].Cells["OUTPUT_AMT"].Value = (decimal.Parse(dt.Rows[i]["OUTPUT_AMT"].ToString())).ToString("#,0.######");
                    }
                }
                else
                {
                    itemOutGrid.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("시스템 오류" + e.ToString());
            }
        }
    }
}
