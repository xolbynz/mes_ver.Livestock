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
    public partial class frm제품재고현황 : Form
    {
        public frm제품재고현황()
        {
            InitializeComponent();
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
                if (chk_stock.Checked == true)
                {
                    dt = wDm.fn_Item_Stock_List("where ( A.ITEM_NM LIKE '%" + txt_srch.Text.ToString() + "%' OR A.LABEL_NM LIKE '%" + txt_srch.Text.ToString() + "%' )  and (ISNULL(B.INPUT_AMT,0) - ISNULL(C.OUTPUT_AMT,0) > 0) ");
                }
                else
                {
                    dt = wDm.fn_Item_Stock_List("where ( A.ITEM_NM LIKE '%" + txt_srch.Text.ToString() + "%' OR A.LABEL_NM LIKE '%" + txt_srch.Text.ToString() + "%' ) ");
                }
                this.dataItemGrid.RowCount = dt.Rows.Count;
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        dataItemGrid.Rows[i].Cells["ITEM_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                        dataItemGrid.Rows[i].Cells["ITEM_NM"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                        dataItemGrid.Rows[i].Cells["BAL_STOCK"].Value = decimal.Parse(dt.Rows[i]["BAL_STOCK"].ToString()).ToString("#,0.######");
                        dataItemGrid.Rows[i].Cells["INPUT_AMT"].Value = decimal.Parse(dt.Rows[i]["INPUT_AMT"].ToString()).ToString("#,0.######");
                        dataItemGrid.Rows[i].Cells["OUTPUT_AMT"].Value = decimal.Parse(dt.Rows[i]["OUTPUT_AMT"].ToString()).ToString("#,0.######");
                        dataItemGrid.Rows[i].Cells["CHUGJONG_NM"].Value = dt.Rows[i]["CHUGJONG_NM"].ToString();
                        dataItemGrid.Rows[i].Cells["COUNTRY_NM"].Value = dt.Rows[i]["COUNTRY_NM"].ToString();
                        dataItemGrid.Rows[i].Cells["CLASS_NM"].Value = dt.Rows[i]["CLASS_NM"].ToString();
                        dataItemGrid.Rows[i].Cells["LABEL_NM"].Value = dt.Rows[i]["LABEL_NM"].ToString();
                    }
                }
                else
                {
                    dataItemGrid.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("시스템 오류" + ex.ToString());
            }
        }

        private void dataItemGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0)
            {
                return;
            }
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = wDm.fn_Item_Stock_List_Detail("WHERE A.ITEM_CD = '"+dataItemGrid.Rows[e.RowIndex].Cells["ITEM_CD"].Value.ToString()+"'   ");
                ItemDetailGrid.Rows.Clear();
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ItemDetailGrid.Rows.Add();
                        ItemDetailGrid.Rows[i].Cells["INPUT_DATE"].Value = dt.Rows[i]["INPUT_DATE"].ToString();
                        ItemDetailGrid.Rows[i].Cells["INPUT_CD"].Value = dt.Rows[i]["INPUT_CD"].ToString();
                        ItemDetailGrid.Rows[i].Cells["INPUT_SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
                        ItemDetailGrid.Rows[i].Cells["EXPRT_DATE"].Value = dt.Rows[i]["EXPRT_DATE"].ToString();
                        ItemDetailGrid.Rows[i].Cells["R_INPUT_AMT"].Value = decimal.Parse(dt.Rows[i]["INPUT_AMT"].ToString()).ToString("#,0.######");
                        ItemDetailGrid.Rows[i].Cells["R_OUTPUT_AMT"].Value = decimal.Parse(dt.Rows[i]["OUTPUT_AMT"].ToString()).ToString("#,0.######");
                        ItemDetailGrid.Rows[i].Cells["R_STOCK_AMT"].Value = decimal.Parse(dt.Rows[i]["BAL_STOCK"].ToString()).ToString("#,0.######");
                        ItemDetailGrid.Rows[i].Cells["B_UNION_CD"].Value = dt.Rows[i]["B_UNION_CD"].ToString();
                        ItemDetailGrid.Rows[i].Cells["A_UNION_CD"].Value = dt.Rows[i]["A_UNION_CD"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void frm제품재고현황_Load(object sender, EventArgs e)
        {
            ComInfo.gridHeaderSet(dataItemGrid);
            ComInfo.gridHeaderSet(ItemDetailGrid);
        }

        private void txt_srch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    wnDm wDm = new wnDm();
                    DataTable dt = null;
                    if (chk_stock.Checked == true)
                    {
                        dt = wDm.fn_Item_Stock_List("where ( A.ITEM_NM LIKE '%" + txt_srch.Text.ToString() + "%' OR A.LABEL_NM LIKE '%" + txt_srch.Text.ToString() + "%' )  and (ISNULL(B.INPUT_AMT,0) - ISNULL(C.OUTPUT_AMT,0) > 0) ");
                    }
                    else
                    {
                        dt = wDm.fn_Item_Stock_List("where ( A.ITEM_NM LIKE '%" + txt_srch.Text.ToString() + "%' OR A.LABEL_NM LIKE '%" + txt_srch.Text.ToString() + "%' ) ");
                    }
                    this.dataItemGrid.RowCount = dt.Rows.Count;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            dataItemGrid.Rows[i].Cells["ITEM_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                            dataItemGrid.Rows[i].Cells["ITEM_NM"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                            dataItemGrid.Rows[i].Cells["BAL_STOCK"].Value = decimal.Parse(dt.Rows[i]["BAL_STOCK"].ToString()).ToString("#,0.######");
                            dataItemGrid.Rows[i].Cells["INPUT_AMT"].Value = decimal.Parse(dt.Rows[i]["INPUT_AMT"].ToString()).ToString("#,0.######");
                            dataItemGrid.Rows[i].Cells["OUTPUT_AMT"].Value = decimal.Parse(dt.Rows[i]["OUTPUT_AMT"].ToString()).ToString("#,0.######");
                            dataItemGrid.Rows[i].Cells["CHUGJONG_NM"].Value = dt.Rows[i]["CHUGJONG_NM"].ToString();
                            dataItemGrid.Rows[i].Cells["COUNTRY_NM"].Value = dt.Rows[i]["COUNTRY_NM"].ToString();
                            dataItemGrid.Rows[i].Cells["CLASS_NM"].Value = dt.Rows[i]["CLASS_NM"].ToString();
                            dataItemGrid.Rows[i].Cells["LABEL_NM"].Value = dt.Rows[i]["LABEL_NM"].ToString();
                        }
                    }
                    else
                    {
                        dataItemGrid.Rows.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("시스템 오류" + ex.ToString());
                }
            }
        }
    }
}
