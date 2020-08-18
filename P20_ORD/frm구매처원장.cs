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

namespace 스마트팩토리.P20_ORD
{
    public partial class frm구매처원장 : Form
    {
        private wnGConstant wConst = new wnGConstant();

        public frm구매처원장()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            cust_Grid.Rows.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm구매처원장_Load(object sender, EventArgs e)
        {
            start_date.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
        }

        private void btnSrch_Click(object sender, EventArgs e)
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
                


        //private void init_ComboBox()
        //{
        //    ComInfo comInfo = new ComInfo();
        //    string sqlQuery = "";

        //    cmb.ValueMember = "코드";
        //    cmb.DisplayMember = "명칭";
        //    sqlQuery = comInfo.queryCustUsed("1");
        //    wConst.ComboBox_Read_Blank(cmb, sqlQuery);
        //}

        private void cust_list(string condition)
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;       
                StringBuilder sb = new StringBuilder();

                if (txtcustSrch != null)
                {
                    sb.AppendLine(" and (SELECT CUST_NM FROM N_CUST_CODE WHERE CUST_CD = A.CUST_CD) LIKE '%" + txtcustSrch.Text.ToString() + "%' ");
                    //sb.AppendLine(" and C.CUST_CD = cmb.SelectedValue");
                }

                sb.AppendLine(" and C.CUST_CD = cmb.SelectedValue");
                sb.AppendLine(" and A.ORDER_DATE >= '" + start_date.Text.ToString() + "' and  A.ORDER_DATE <= '" + end_date.Text.ToString() + "'");

                dt = wDm.cust_Grid_List(sb.ToString());
                //if (!txtSrch.Text.ToString().Equals(""))
                //{
                //    sb.AppendLine("and CHK_NM like '%" + txtSrch.Text.ToString() + "%' ");
                //}



                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        custGrid.Rows[i].Cells[0].Value = dt.Rows[i]["ORDER_DATE"].ToString();
                        custGrid.Rows[i].Cells[1].Value = dt.Rows[i]["ORDER_CD"].ToString();
                        custGrid.Rows[i].Cells[2].Value = dt.Rows[i]["SEQ"].ToString();
                        custGrid.Rows[i].Cells[3].Value = dt.Rows[i]["RAW_MAT_CD"].ToString();
                        custGrid.Rows[i].Cells[4].Value = dt.Rows[i]["SPEC"].ToString();
                        custGrid.Rows[i].Cells[5].Value = dt.Rows[i]["UNIT_CD"].ToString();
                        custGrid.Rows[i].Cells[6].Value = dt.Rows[i]["TOTAL_AMT"].ToString();
                        custGrid.Rows[i].Cells[7].Value = dt.Rows[i]["PRICE"].ToString();
                        custGrid.Rows[i].Cells[8].Value = dt.Rows[i]["COMPLETE_YN"].ToString();
                    }
                }
                else
                {
                    custGrid.Rows.Clear();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("시스템 에러: " + e.Message.ToString());
            }
        }

               

        private void save_logic(object sender, EventArgs e)
        {
            if (custGrid.Rows.Count <= 0)
            {
                MessageBox.Show("거래처를 선택하시기 바랍니다.");
                return;
            }

            if (custGrid.Rows.Count > 0)
            {
                int cnt = 0;
                int grid_cnt = custGrid.Rows.Count;
                for (int i = 0; i < grid_cnt; i++)
                {
                    string txt_item_cd = (string)custGrid.Rows[i - cnt].Cells["RAW_MAT_CD"].Value;

                    if (txt_item_cd == "" || txt_item_cd == null)  //마지막 행에 원부재료코드가 없을 경우 제거
                    {
                        custGrid.Rows.RemoveAt(i - cnt);
                        cnt++;
                    }
                }
            }

            //string pur_yn = comInfo.resultYn(chk_pur_yn);
            //if (lbl_order_gbn.Text != "1")
            //{
            //    wnDm wDm = new wnDm();
            //    int rsNum = wDm.insertOrder(
            //            txt_order_date.Text.ToString(),
            //            txt_cust_cd.Text.ToString(),
            //            in_req_date.Text.ToString(),
            //            pur_yn,
            //            txt_comment.Text.ToString(),
            //            rmOrderGrid);

            //    if (rsNum == 0)
            //    {
            //        resetSetting();

            //        StringBuilder sb = new StringBuilder();
            //        sb.AppendLine("and A.ORDER_DATE >= '" + start_date.Text.ToString() + "' and  A.ORDER_DATE <= '" + end_date.Text.ToString() + "'");

            //        string str = queryStr(sb.ToString());
            //        order_list(orderGrid, str);

            //        MessageBox.Show("성공적으로 등록하였습니다.");
            //    }
            //    else if (rsNum == 1)
            //        MessageBox.Show("저장에 실패하였습니다");
            //    else
            //        MessageBox.Show("Exception 에러");
            //}
            //else
            //{
            //    wnDm wDm = new wnDm();
            //    int rsNum = wDm.updateOrder(
            //            txt_order_date.Text.ToString(),
            //            txt_order_cd.Text.ToString(),
            //            txt_cust_cd.Text.ToString(),
            //            in_req_date.Text.ToString(),
            //            txt_comment.Text.ToString(),
            //            pur_yn,
            //            rmOrderGrid,
            //            del_orderGrid);

            //    if (rsNum == 0)
            //    {
            //        del_orderGrid.Rows.Clear();
            //        StringBuilder sb = new StringBuilder();
            //        sb.AppendLine("and A.ORDER_DATE >= '" + start_date.Text.ToString() + "' and  A.ORDER_DATE <= '" + end_date.Text.ToString() + "'");

            //        string str = queryStr(sb.ToString());
            //        order_list(orderGrid, str);

            //        orderDetail2();
            //        MessageBox.Show("성공적으로 수정하였습니다.");
            //    }
            //    else if (rsNum == 1)
            //        MessageBox.Show("저장에 실패하였습니다");
            //    else
            //        MessageBox.Show("Exception 에러");
            //}
        }

        


           
    }
}
