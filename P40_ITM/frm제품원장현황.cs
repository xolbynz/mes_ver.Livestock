using System;
using System.Collections;
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
    public partial class frm제품원장현황 : Form
    {
        Popup.frmPrint readyPrt = new Popup.frmPrint();
        private wnGConstant wConst = new wnGConstant();

        DataTable adoPrt = null;
        DataTable adoPrt2 = null;
        wnAdo wAdo = new wnAdo();
        public Popup.frmPrint frmPrt;

        public string strDay1 = "";
        public string strDay2 = "";
        public string strCondition = "";
        public string raw_mat_temp = "";

        public frm제품원장현황()
        {
            InitializeComponent();
        }

        private void frm제품원장현황_Load(object sender, EventArgs e)
        {
            ComInfo.gridHeaderSet(inputRmGrid);

            start_date.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");

            grdCellSetting();

            init_ComboBox();

            

        }

        #region button logic

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (chk_all != null && chk_all.Checked)
            {
                txt_item_cd.Text = "";
                txt_item_nm.Text = "";
                txt_chugjong_nm.Text ="";
                txt_class_nm.Text = "";
                txt_country_nm.Text ="";
                txt_type_nm.Text ="";
                txt_hamyang.Text = "";
                txt_label_nm.Text ="";
                txt_balstock.Text = "";
                txt_srch.Text = "";
                txt_srch2.Text = "";
                txt_grade1.Text = "";
                txt_grade2.Text = "";
                txt_grade3.Text = "";
                in_grid_logic();
            }
            else
            {
                if (txt_srch2.Text == null || txt_srch2.Text.Equals(""))
                {
                    MessageBox.Show("조회할 제품을 선택해주십시오");
                    return;
                }
                in_grid_logic();
                calBalstock_logic();
                in_grade_logic();
            }
            wnGConstant wng = new wnGConstant();
            wng.mergeCells(inputRmGrid, 3);
        }

        private void in_grade_logic()
        {

            txt_grade1.Text = "";
            txt_grade2.Text = "";
            txt_grade3.Text = "";

            wnDm wDm = new wnDm();
            DataTable dt = null;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" WHERE D.PRODUCT_CD = '"+txt_srch2.Text.ToString()+"' and A.SALES_DATE >= '"+start_date.Text.ToString()+"'  and A.SALES_DATE <= '"+end_date.Text.ToString()+"'  ");

            dt = wDm.fn_Sales_Detail_Order_Count_Only_Item(sb.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                ArrayList arr = new ArrayList();
                arr.Add(txt_grade1);
                arr.Add(txt_grade2);
                arr.Add(txt_grade3);


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox tempGrade = (TextBox)arr[i];
                    tempGrade.Text = dt.Rows[i]["CUST_NM"].ToString();
                    if (i == 2)
                    {
                        break;
                    }
                }
            }
        }

        

        private void calBalstock_logic()
        {
            wnDm wDm = new wnDm();
            DataTable dt = null;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("and A.ITEM_CD = '"+txt_srch2.Text.ToString()+"' and A.DATE > '"+end_date.Text.ToString()+"'   ");


            dt = wDm.fn_item_input_output_list(sb.ToString());

            decimal lastBalance = decimal.Parse(txt_balstock.Text.ToString().Replace(",", ""));
            if (dt != null && dt.Rows.Count > -1)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lastBalance = (dt.Rows[i]["IO_GUBUN"].ToString().Equals("I") ?
                        lastBalance - decimal.Parse(dt.Rows[i]["AMT"].ToString()) :
                        lastBalance + decimal.Parse(dt.Rows[i]["AMT"].ToString()));
                }


                for (int i = inputRmGrid.Rows.Count - 1; i >= 0; i--)
                {
                    inputRmGrid.Rows[i].Cells["BALSTOCK"].Value = lastBalance.ToString("#,0.######");
                    if (!(inputRmGrid.Rows[i].Cells["INPUT_DATE"].Value.ToString().Equals("--- 일계 ---") ||
                        inputRmGrid.Rows[i].Cells["INPUT_DATE"].Value.ToString().Equals("=== 월계 ===") ||
                        inputRmGrid.Rows[i].Cells["INPUT_DATE"].Value.ToString().Equals("=== 합계 ===") ||
                        inputRmGrid.Rows[i].Cells["INPUT_DATE"].Value.ToString().Equals("== 전잔고 ==")
                        ))
                    {
                        string total_input;
                        string total_output;
                        if (inputRmGrid.Rows[i].Cells["INPUT_AMT"].Value == null || inputRmGrid.Rows[i].Cells["INPUT_AMT"].Value.ToString().Equals(""))
                        {
                            total_input = "0";
                        }
                        else
                        {
                            total_input = inputRmGrid.Rows[i].Cells["INPUT_AMT"].Value.ToString();
                        }
                        if (inputRmGrid.Rows[i].Cells["OUTPUT_AMT"].Value == null || inputRmGrid.Rows[i].Cells["OUTPUT_AMT"].Value.ToString().Equals(""))
                        {
                            total_output = "0";
                        }
                        else
                        {
                            total_output = inputRmGrid.Rows[i].Cells["OUTPUT_AMT"].Value.ToString();
                        }
                        lastBalance = lastBalance - decimal.Parse(total_input);
                        lastBalance = lastBalance + decimal.Parse(total_output);
                    }

                }

            }

        }

        #endregion button logic

        private void init_ComboBox() 
        {


            //cmb_cust.ValueMember = "코드";
            //cmb_cust.DisplayMember = "명칭";
            //sqlQuery = comInfo.queryCust("2");
            //wConst.ComboBox_Read_Blank(cmb_cust, sqlQuery);

            //cmb_raw.ValueMember = "코드";
            //cmb_raw.DisplayMember = "명칭";
            //sqlQuery = comInfo.queryRaw();
            //wConst.ComboBox_Read_Blank(cmb_raw, sqlQuery);
        }
        private void in_grid_logic()
        {
            wnDm wDm = new wnDm();
            DataTable dt = null;

            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();

            if (chk_all.Checked)
            {
                sb.AppendLine("and Z.INPUT_DATE >= '" + start_date.Text.ToString() + "' and  Z.INPUT_DATE <= '" + end_date.Text.ToString() + "' ");
                sb2.AppendLine("and Z.OUTPUT_DATE >= '" + start_date.Text.ToString() + "' and  Z.OUTPUT_DATE <= '" + end_date.Text.ToString() + "' ");
            }
            else
            {
                sb.AppendLine("and Z.INPUT_DATE >= '" + start_date.Text.ToString() + "' and  Z.INPUT_DATE <= '" + end_date.Text.ToString() + "' and Z.ITEM_CD = '" + txt_srch2.Text.ToString() + "'   ");
                sb2.AppendLine("and Z.OUTPUT_DATE >= '" + start_date.Text.ToString() + "' and  Z.OUTPUT_DATE <= '" + end_date.Text.ToString() + "' and Z.ITEM_CD = '" + txt_srch2.Text.ToString() + "'   ");
            }

            dt = wDm.fn_Item_Ledger_List(sb.ToString(), sb2.ToString());

            inputRmGrid.Rows.Clear();


            if (dt != null && dt.Rows.Count > 0)
            {
                inputRmGrid.Rows.Add();
                inputRmGrid.Rows[0].Cells["INPUT_DATE"].Value = "== 전재고 ==";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //string t_amt = string.Format("{0:#.##}", 100.2);
                    inputRmGrid.Rows.Add();


                    inputRmGrid.Rows[i+1].Cells["INPUT_DATE"].Value = dt.Rows[i]["일자명칭"].ToString();
                    if (dt.Rows[i]["일자명칭"].ToString().ToString().Equals("--- 일계 ---")
                        || dt.Rows[i]["일자명칭"].ToString().ToString().Equals("=== 월계 ==="))
                    {
                        DataGridViewCellStyle style = new DataGridViewCellStyle();
                        style.BackColor = Color.Khaki;
                        style.SelectionBackColor = Color.DarkKhaki;
                        if (dt.Rows[i]["일자명칭"].ToString().ToString().Equals("=== 월계 ==="))
                        {
                            style.ForeColor = Color.Blue;
                        }
                        for (int j = 0; j < inputRmGrid.ColumnCount; j++)
                        {
                            inputRmGrid.Rows[i+1].Cells[j].Style = style;
                        }
                    }
                    if (dt.Rows[i]["INPUT_CD"].ToString().Equals("999999"))
                    {
                        inputRmGrid.Rows[i + 1].Cells["INPUT_CD"].Value = "";
                    }
                    else
                    {
                        inputRmGrid.Rows[i + 1].Cells["INPUT_CD"].Value = dt.Rows[i]["INPUT_CD"].ToString();
                    }

                    if (dt.Rows[i]["SEQ"].ToString().Equals("0"))
                    {
                        inputRmGrid.Rows[i + 1].Cells["SEQ"].Value = "";
                    }
                    else
                    {
                        inputRmGrid.Rows[i + 1].Cells["SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
                    }
                    if (dt.Rows[i]["bun"].ToString().Equals("ㅎ"))
                    {
                        inputRmGrid.Rows[i + 1].Cells["NO"].Value = "";
                    }
                    else
                    {
                        inputRmGrid.Rows[i + 1].Cells["NO"].Value = dt.Rows[i]["bun"].ToString();
                    }

                    inputRmGrid.Rows[i+1].Cells["PRODUCT_NM"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                    

                    inputRmGrid.Rows[i+1].Cells["CHUGJONG_NM"].Value = dt.Rows[i]["CHUGJONG_NM"].ToString();
                    inputRmGrid.Rows[i+1].Cells["CLASS_NM"].Value = dt.Rows[i]["CLASS_NM"].ToString();
                    inputRmGrid.Rows[i+1].Cells["COUNTRY_NM"].Value = dt.Rows[i]["COUNTRY_NM"].ToString();
                    inputRmGrid.Rows[i+1].Cells["UNION_CD"].Value = dt.Rows[i]["UNION_CD"].ToString();
                    inputRmGrid.Rows[i+1].Cells["LABEL_NM"].Value = dt.Rows[i]["LABEL_NM"].ToString();
                    inputRmGrid.Rows[i + 1].Cells["FROZEN_GUBUN"].Value = dt.Rows[i]["FROZEN_GUBUN"].ToString();
                    inputRmGrid.Rows[i + 1].Cells["UNIT_NM"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                    inputRmGrid.Rows[i + 1].Cells["INPUT_GUBUN"].Value = dt.Rows[i]["INPUT_GUBUN"].ToString();

                    if (dt.Rows[i]["bun"].ToString().Equals("출고"))
                    {
                        inputRmGrid.Rows[i + 1].Cells["OUTPUT_LOT"].Value = "(" + dt.Rows[i]["OUTPUT_LOT"].ToString() + ")";
                    }
                    else
                    {
                        inputRmGrid.Rows[i + 1].Cells["OUTPUT_LOT"].Value = dt.Rows[i]["OUTPUT_LOT"].ToString();
                    }



                    inputRmGrid.Rows[i + 1].Cells["INPUT_AMT"].Value = (decimal.Parse(dt.Rows[i]["INPUT_AMT"].ToString())).ToString("#,0.######");
                    inputRmGrid.Rows[i + 1].Cells["OUTPUT_AMT"].Value = (decimal.Parse(dt.Rows[i]["OUTPUT_AMT"].ToString())).ToString("#,0.######");

                    if (inputRmGrid.Rows[i + 1].Cells["INPUT_AMT"].Value == null || inputRmGrid.Rows[i + 1].Cells["INPUT_AMT"].Value.ToString().Equals("0"))
                    {
                        inputRmGrid.Rows[i + 1].Cells["INPUT_AMT"].Value = "";
                    }
                    if (inputRmGrid.Rows[i + 1].Cells["OUTPUT_AMT"].Value == null || inputRmGrid.Rows[i + 1].Cells["OUTPUT_AMT"].Value.ToString().Equals("0"))
                    {
                        inputRmGrid.Rows[i + 1].Cells["OUTPUT_AMT"].Value = "";
                    }
                    

                }
            }

            //데이타 끝나고 다시 copy를 써준 이유는 for중에 no에 값을 엏었기 때문에 출력물 데이타테이블(dt)를 다시 복사함
        }

        private void grdCellSetting()
        {
            ComInfo comInfo = new ComInfo();
            comInfo.grdCellSetting2(inputRmGrid);
        }

        private void btn출력_Click(object sender, EventArgs e)
        {
            btn출력.Enabled = false;

            strCondition = "";

            if (inputRmGrid.Rows.Count == 0)
            {
                MessageBox.Show("출력할 자료가 없습니다.");
                btn출력.Enabled = true;
                return;
            }
            strDay1 = start_date.Text;
            strDay2 = end_date.Text;
            strCondition = "제품원장현황";

            frmPrt = readyPrt;
            frmPrt.Show();
            frmPrt.BringToFront();
            //frmPrt.prt_원자재재고현황(adoPrt, strDay1, strDay2, strCust, strCondition);
            frmPrt.prt_원장현황(adoPrt2,inputRmGrid, strDay1, strDay2, strCondition,txt_label_nm.Text);

            btn출력.Enabled = true;
        }

        

        private void cmb_raw_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e);

            //if (cmb_raw.Text.Length >= 14)
            //{
            //    StringBuilder sb = new StringBuilder();
            //    sb.AppendLine(" and CONVERT(NVARCHAR(8),CONVERT (DATE, A.INPUT_DATE),112) ");
            //    sb.AppendLine(" +RIGHT('000'+CONVERT(varchar,A.INPUT_CD),4) ");
            //    sb.AppendLine(" +RIGHT('0'+CONVERT(varchar,A.SEQ),2) = '"+cmb_raw.Text.ToString() +"' ");

            //    in_rm_ledger_logic(sb.ToString());
            //}
        }

        private void btn_raw_srch_Click(object sender, EventArgs e)
        {
            wnDm wDm = new wnDm();
            Popup.pop_sf_제품검색 msg = new Popup.pop_sf_제품검색();
            msg.dt = wDm.fn_Item_List("where 1=1  ");

            msg.ShowDialog();

            if (msg.sCode != null && !msg.sCode.Equals(""))
            {
                txt_srch.Text = msg.sName;
                txt_srch2.Text = msg.sCode;

                input_item_list();
                inputRmGrid.Rows.Clear();
                
            }

        }

        private void input_item_list()
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = wDm.fn_Item_Stock_List(" WHERE 1=1 AND A.ITEM_CD = '"+txt_srch2.Text.ToString()+"'  ");
                adoPrt2 = dt.Copy();

                if (dt != null && dt.Rows.Count > 0)
                {
                    txt_item_cd.Text = dt.Rows[0]["ITEM_CD"].ToString();
                    txt_item_nm.Text = dt.Rows[0]["ITEM_NM"].ToString();
                    txt_chugjong_nm.Text = dt.Rows[0]["CHUGJONG_NM"].ToString();
                    txt_class_nm.Text = dt.Rows[0]["CLASS_NM"].ToString();
                    txt_country_nm.Text = dt.Rows[0]["COUNTRY_NM"].ToString();
                    txt_type_nm.Text = dt.Rows[0]["TYPE_NM"].ToString();
                    txt_hamyang.Text = dt.Rows[0]["HAMYANG"].ToString();
                    txt_label_nm.Text = dt.Rows[0]["LABEL_NM"].ToString();
                    txt_balstock.Text = decimal.Parse(dt.Rows[0]["BAL_STOCK"].ToString()).ToString("#,0.######");

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show("제품 검색 중 오류가 발생했습니다.");
                return;
            }
        }

        private void inputRmGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (inputRmGrid.Columns[e.ColumnIndex].Name.Equals("OUTPUT_LOT")
                    && inputRmGrid.Rows[e.RowIndex].Cells["OUTPUT_LOT"].Value != null
                    && !inputRmGrid.Rows[e.RowIndex].Cells["OUTPUT_LOT"].Value.ToString().Equals(""))
                {
                    if (inputRmGrid.Rows[e.RowIndex].Cells["OUTPUT_LOT"].Value.ToString().Contains("("))
                    {
                        //납품추적
                        string valueTemp = inputRmGrid.Rows[e.RowIndex].Cells["OUTPUT_LOT"].Value.ToString().Replace("(", "").Replace(")", "");
                        string date = "20" + valueTemp.Substring(0, 2) + "-" + valueTemp.Substring(2, 2) + "-" + valueTemp.Substring(4, 2);
                        string cd = int.Parse(valueTemp.Substring(6, 3)).ToString();
                        P50_QUA.frm씨지엠납품추적 form = new P50_QUA.frm씨지엠납품추적();
                        form.s_INPUT_DATE = inputRmGrid.Rows[e.RowIndex].Cells["INPUT_DATE"].Value.ToString();
                        form.s_INPUT_CD = inputRmGrid.Rows[e.RowIndex].Cells["INPUT_CD"].Value.ToString();
                        form.s_INPUT_SEQ = inputRmGrid.Rows[e.RowIndex].Cells["SEQ"].Value.ToString();
                        form.s_GUBUN = "납품";
                        form.StartPosition = FormStartPosition.CenterParent;
                        form.SrchValue = "and S.SALES_DATE = '" + date + "'   AND S.SALES_CD = '" + cd + "'   ";
                        form.txt_srch_value.Text = valueTemp;
                        form.txt_rBtn_value.Text = "납품LOT로 추적";
                        form.Show();
                        form.Srch_List();
                    }
                    else
                    {
                        P50_QUA.frm씨지엠공정추적 form = new P50_QUA.frm씨지엠공정추적();
                        form.SrchValue = inputRmGrid.Rows[e.RowIndex].Cells["OUTPUT_LOT"].Value.ToString();
                        form.s_INPUT_DATE = inputRmGrid.Rows[e.RowIndex].Cells["INPUT_DATE"].Value.ToString();
                        form.s_INPUT_CD = inputRmGrid.Rows[e.RowIndex].Cells["INPUT_CD"].Value.ToString();
                        form.s_INPUT_SEQ = inputRmGrid.Rows[e.RowIndex].Cells["SEQ"].Value.ToString();
                        form.s_GUBUN = "생산";
                        form.s_GUBUN2 = "";
                        form.StartPosition = FormStartPosition.CenterParent;
                        form.Show();
                        form.Srch_by_LotNo(form.SrchValue);
                    }
                }
                else if (inputRmGrid.Columns[e.ColumnIndex].Name.Equals("UNION_CD")
                    && inputRmGrid.Rows[e.RowIndex].Cells["UNION_CD"].Value != null
                    && !inputRmGrid.Rows[e.RowIndex].Cells["UNION_CD"].Value.ToString().Equals(""))
                {
                    P50_QUA.frm씨지엠공정추적 form = new P50_QUA.frm씨지엠공정추적();
                    form.SrchValue = inputRmGrid.Rows[e.RowIndex].Cells["UNION_CD"].Value.ToString();
                    form.s_INPUT_DATE = inputRmGrid.Rows[e.RowIndex].Cells["INPUT_DATE"].Value.ToString();
                    form.s_INPUT_CD = inputRmGrid.Rows[e.RowIndex].Cells["INPUT_CD"].Value.ToString();
                    form.s_INPUT_SEQ = inputRmGrid.Rows[e.RowIndex].Cells["SEQ"].Value.ToString();
                    form.s_GUBUN = "생산";

                    form.StartPosition = FormStartPosition.CenterParent;
                    form.Show();
                    form.Srch_by_a_union(form.SrchValue);
                    
                }
            }
        }
    }
}
