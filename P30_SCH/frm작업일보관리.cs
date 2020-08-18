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
using 스마트팩토리.Controls;

namespace 스마트팩토리.P30_SCH
{
    public partial class frm작업일보관리 : Form
    {    

        public frm작업일보관리()
        {
            InitializeComponent();
        }

        private void btnitemSrch_Click(object sender, EventArgs e)
        {
            Popup.pop_sf_제품검색 frm = new Popup.pop_sf_제품검색();
            wnDm wDm = new wnDm();
            DataTable dt = new DataTable();
            dt = wDm.fn_Item_List("where ITEM_NM like '%" + txtitemSrch.Text.ToString() + "%' ");

            
            frm.dt = dt;
            frm.txtSrch.Text = txtitemSrch.Text.ToString();
            frm.ShowDialog();

            if (frm.sCode != "")
            {
                
                txtitemSrch.Text = frm.sName.Trim();           

            }
            else
            {
                //txt_item_nm.Text = old_item_nm;
            }          
        }

        private void btncustSrch_Click(object sender, EventArgs e)
        {
            Popup.pop거래처검색 frm = new Popup.pop거래처검색();

            frm.sCustGbn = "1";
            frm.sCustNm = txtcustSrch.Text.ToString();
            frm.ShowDialog();

            if (frm.sCode != "")
            {
                txtcustSrch.Text = frm.sName.Trim();                
            }
            else
            {
                //txt_cust_cd.Text = old_cust_nm;
            }

            frm.Dispose();
            frm = null;

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSrch_Click(object sender, EventArgs e)
        {
            work_logic();
        }

        private void work_logic()
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                StringBuilder sb = new StringBuilder();                

                dTP1.Text = dTP1.Value.ToString("yyyy-MM-dd");

                sb.AppendLine("where A.W_INST_DATE = '" + dTP1.Text + "' ");

                if (txtitemSrch != null)
                {
                    sb.AppendLine(" and (SELECT ITEM_NM FROM N_ITEM_CODE WHERE ITEM_CD = A.ITEM_CD) LIKE '%" + txtitemSrch.Text.ToString() + "%' ");                    
                }                
                if (txtcustSrch != null)
                {
                    sb.AppendLine(" and (SELECT CUST_NM FROM N_CUST_CODE WHERE CUST_CD = A.CUST_CD) LIKE '%" + txtcustSrch.Text.ToString() + "%' ");                    
                }
                dt = wDm.fn_work_List(sb.ToString());
                
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.workGrid.RowCount = dt.Rows.Count;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        workGrid.Rows[i].Cells["LOT_NO"].Value = dt.Rows[i]["LOT_NO"].ToString();
                        workGrid.Rows[i].Cells["W_INST_DATE"].Value = dt.Rows[i]["W_INST_DATE"].ToString();
                        workGrid.Rows[i].Cells["ITEM_NM"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                        workGrid.Rows[i].Cells["SPEC"].Value = dt.Rows[i]["SPEC"].ToString();
                        workGrid.Rows[i].Cells["CUST_NM"].Value = dt.Rows[i]["CUST_NM"].ToString();
                        workGrid.Rows[i].Cells["INST_AMT"].Value = dt.Rows[i]["INST_AMT"].ToString();
                        workGrid.Rows[i].Cells["INPUT_YN"].Value = dt.Rows[i]["RAW_OUT_YN"].ToString();
                        workGrid.Rows[i].Cells["W_STEP"].Value = dt.Rows[i]["F_STEP"].ToString();
                        workGrid.Rows[i].Cells["CHECK_YN"].Value = dt.Rows[i]["COMPLETE_YN"].ToString();
                    }
                }
                else
                {
                    workGrid.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("시스템 오류" + ex.ToString());
            }
        }

    }
}
