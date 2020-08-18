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

namespace 스마트팩토리.P30_SCH
{
    public partial class frm공정별재공현황 : Form
    {
        public frm공정별재공현황()
        {
            InitializeComponent();
        }

        private void frm공정별재공현황_Load(object sender, EventArgs e)
        {
            itemFlowLogic();
        }

        #region button logic

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion button logic

        #region 공정현황 logic

        private void itemFlowLogic() 
        {
            DataTable dt = new DataTable();
            wnDm wDm = new wnDm();
            dt = wDm.fn_Item_Flow_Con_List();

            itemFlowGrid.Rows.Clear();
            if (dt != null && dt.Rows.Count > 0) 
            {
                itemFlowGrid.RowCount = dt.Rows.Count;
                for (int i = 0; i < dt.Rows.Count; i++) 
                {
                    itemFlowGrid.Rows[i].Cells["ITEM_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                    itemFlowGrid.Rows[i].Cells["ITEM_NM"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                    itemFlowGrid.Rows[i].Cells["SPEC"].Value = dt.Rows[i]["SPEC"].ToString();
                    itemFlowGrid.Rows[i].Cells["LOT_NO"].Value = dt.Rows[i]["LOT_NO"].ToString();
                    itemFlowGrid.Rows[i].Cells["W_INST_DATE"].Value = dt.Rows[i]["W_INST_DATE"].ToString();
                    itemFlowGrid.Rows[i].Cells["LOT_TOT_FLOW_NM"].Value = dt.Rows[i]["LOT_TOT_FLOW_NM"].ToString();
                }
                detailLogic(itemFlowGrid, 0);
            }
        }

        private void itemFlowGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            detailLogic(dgv,e.RowIndex);
        }

        private void detailLogic(DataGridView dgv, int idx) 
        {
            DataTable dt = new DataTable();
            wnDm wDm = new wnDm();

            dt = wDm.fn_Item_Flow_Con_Dt_List(dgv.Rows[idx].Cells["LOT_NO"].Value.ToString());
            flowDetailGrid.DataSource = dt;

            for (int i = 0; i < flowDetailGrid.Rows.Count; i++) 
            {
                if (int.Parse(flowDetailGrid.Rows[i].Cells[10].Value.ToString()) >= 7 && int.Parse(flowDetailGrid.Rows[i].Cells[10].Value.ToString()) <= 13 ) // 경과일자
                {
                    flowDetailGrid.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                } 
                else if (int.Parse(flowDetailGrid.Rows[i].Cells[10].Value.ToString()) > 14)
                {
                    flowDetailGrid.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        #endregion 공정현황 logic
    }
}
