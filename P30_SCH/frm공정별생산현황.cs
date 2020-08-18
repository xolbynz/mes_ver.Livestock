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
    public partial class frm공정별생산현황 : Form
    {
        private wnGConstant wConst = new wnGConstant();

        public frm공정별생산현황()
        {
            InitializeComponent();
        }

        private void frm공정별생산현황_Load(object sender, EventArgs e)
        {
            start_date.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
            
            init_ComboBox();
            flowProdLogic();
        }

        public void init_ComboBox() 
        {
            ComInfo comInfo = new ComInfo();
            string sqlQuery = "";

            cmb_flow.ValueMember = "코드";
            cmb_flow.DisplayMember = "명칭";
            sqlQuery = comInfo.queryFlow();
            wConst.ComboBox_Read_Blank(cmb_flow, sqlQuery);

            cmb_item.ValueMember = "코드";
            cmb_item.DisplayMember = "명칭";
            sqlQuery = comInfo.queryItem();
            wConst.ComboBox_Read_Blank(cmb_item, sqlQuery);


        }
        #region button logic
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            flowProdLogic();
        }

        #endregion button logic

        #region logic 
        private void flowProdLogic()
        {
            DataTable dt = new DataTable();
            wnDm wDm = new wnDm();

            if (cmb_flow.SelectedValue == null) cmb_flow.SelectedValue = "";
            if (cmb_item.SelectedValue == null) cmb_item.SelectedValue = "";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" and (C.INPUT_DATE >= '" + start_date.Text.ToString() + "' and C.INPUT_DATE <= '" + end_date.Text.ToString() + "') ");

            if (cmb_flow.SelectedValue.ToString() != "")
            {
                sb.AppendLine(" and A.FLOW_CD = '" + cmb_flow.SelectedValue + "' ");
            }

            if (cmb_item.SelectedValue.ToString() != "") 
            {
                sb.AppendLine(" and K.ITEM_CD = '" + cmb_item.SelectedValue + "' ");
            }

            dt = wDm.fn_Flow_Product_List(sb.ToString());

            flowProdGrid.RowCount = dt.Rows.Count;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    flowProdGrid.Rows[i].Cells["INPUT_DATE"].Value = dt.Rows[i]["INPUT_DATE"].ToString();
                    flowProdGrid.Rows[i].Cells["LOT_NO"].Value = dt.Rows[i]["LOT_NO"].ToString();
                    flowProdGrid.Rows[i].Cells["LOT_SUB"].Value = dt.Rows[i]["LOT_SUB"].ToString();
                    flowProdGrid.Rows[i].Cells["F_STEP"].Value = dt.Rows[i]["F_STEP"].ToString() + "차";
                    flowProdGrid.Rows[i].Cells["FLOW_NM"].Value = dt.Rows[i]["FLOW_NM"].ToString();
                    flowProdGrid.Rows[i].Cells["F_SUB_AMT"].Value = dt.Rows[i]["LOT_NO"].ToString();
                    flowProdGrid.Rows[i].Cells["POOR_AMT"].Value = dt.Rows[i]["POOR_AMT"].ToString();
                    flowProdGrid.Rows[i].Cells["LOSS"].Value = dt.Rows[i]["LOSS"].ToString();
                    flowProdGrid.Rows[i].Cells["COMMENT"].Value = dt.Rows[i]["COMMENT"].ToString();
                }
            }
        }
        #endregion logic
    }
}
