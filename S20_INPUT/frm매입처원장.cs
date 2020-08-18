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

namespace 스마트팩토리.S20_INPUT
{
    public partial class frm매입처원장 : Form
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

        public frm매입처원장()
        {
            InitializeComponent();
        }

        private void frm매입처원장_Load(object sender, EventArgs e)
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
            if (txt_srch2.Text == null || txt_srch2.Text.Equals(""))
            {
                MessageBox.Show("거래처를 먼저 선택해주세요");
                return;
            }
            in_grid_logic();
            in_grade_logic();
            calBalance_logic();
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
            sb.AppendLine(" WHERE A.CUST_CD = '"+txt_srch2.Text.ToString()+"' and A.BUY_DATE >= '"+start_date.Text.ToString()+"'  and A.BUY_DATE <= '"+end_date.Text.ToString()+"'  ");

            dt = wDm.fn_Buy_Detail_Order_Count(sb.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                ArrayList arr = new ArrayList();
                arr.Add(txt_grade1);
                arr.Add(txt_grade2);
                arr.Add(txt_grade3);


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox tempGrade = (TextBox)arr[i];
                    tempGrade.Text = dt.Rows[i]["PRODUCT_NM"].ToString();
                    if (i == 2)
                    {
                        break;
                    }
                }
            }


        }

        private void calBalance_logic()
        {
            

            decimal lastBalance = decimal.Parse(inputRmGrid.Rows[0].Cells["BALANCE"].Value.ToString().Replace(",",""));

            for (int i = 1; i < inputRmGrid.Rows.Count; i++ )
            {
                if (inputRmGrid.Rows[i].Cells["BUY_DATE"].Value.ToString().Equals("=== 월계 ===") ||
                    inputRmGrid.Rows[i].Cells["BUY_DATE"].Value.ToString().Equals("=== 합계 ==="))
                {
                    continue;
                }
                else if (inputRmGrid.Rows[i].Cells["BUY_DATE"].Value.ToString().Equals("--- 일계 ---"))
                {
                    lastBalance = decimal.Parse(inputRmGrid.Rows[i].Cells["BALANCE"].Value.ToString());
                }
                else
                {
                    string total_soo;
                    string total_money;

                    if (inputRmGrid.Rows[i].Cells["TOTAL_GIVE_MONEY"].Value == null || inputRmGrid.Rows[i].Cells["TOTAL_GIVE_MONEY"].Value.ToString().Equals(""))
                    {
                        total_soo = "0";
                    }
                    else
                    {
                        total_soo = inputRmGrid.Rows[i].Cells["TOTAL_GIVE_MONEY"].Value.ToString();
                    }

                    if (inputRmGrid.Rows[i].Cells["TOTAL_MONEY"].Value == null || inputRmGrid.Rows[i].Cells["TOTAL_MONEY"].Value.ToString().Equals(""))
                    {
                        total_money = "0";
                    }
                    else
                    {
                        total_money = inputRmGrid.Rows[i].Cells["TOTAL_MONEY"].Value.ToString();
                    }
                    lastBalance = lastBalance + decimal.Parse(total_soo);
                    lastBalance = lastBalance - decimal.Parse(total_money);
                    inputRmGrid.Rows[i].Cells["BALANCE"].Value = lastBalance.ToString("#,0.######");
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


            dt = wDm.fn_Buy_Ledger_List(start_date.Text.ToString(), end_date.Text.ToString(), txt_srch2.Text.ToString());

            inputRmGrid.Rows.Clear();


            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //string t_amt = string.Format("{0:#.##}", 100.2);
                    inputRmGrid.Rows.Add();


                    inputRmGrid.Rows[i].Cells["BUY_DATE"].Value = dt.Rows[i]["일자명칭"].ToString();
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
                            inputRmGrid.Rows[i].Cells[j].Style = style;
                        }
                    }
                    if (dt.Rows[i]["BUY_CD"].ToString().Equals("999999") || dt.Rows[i]["BUY_CD"].ToString().Equals("0"))
                    {
                        inputRmGrid.Rows[i].Cells["BUY_CD"].Value = "";
                    }
                    else
                    {
                        inputRmGrid.Rows[i].Cells["BUY_CD"].Value = dt.Rows[i]["BUY_CD"].ToString();
                    }
                    inputRmGrid.Rows[i].Cells["NO"].Value = dt.Rows[i]["bun"].ToString();
                    if (dt.Rows[i]["VAT_CD"].ToString().Equals("2"))
                    {
                        inputRmGrid.Rows[i].Cells["INPUT_GUBUN"].Value = "(" + dt.Rows[i]["INPUT_GUBUN"].ToString() + ")";
                    }
                    else
                    {
                        inputRmGrid.Rows[i].Cells["INPUT_GUBUN"].Value = dt.Rows[i]["INPUT_GUBUN"].ToString();
                    }
                    inputRmGrid.Rows[i].Cells["PRODUCT_NM"].Value = dt.Rows[i]["PRODUCT_NM"].ToString();
                    

                    inputRmGrid.Rows[i].Cells["CHUGJONG_NM"].Value = dt.Rows[i]["CHUGJONG_NM"].ToString();
                    inputRmGrid.Rows[i].Cells["CLASS_NM"].Value = dt.Rows[i]["CLASS_NM"].ToString();
                    inputRmGrid.Rows[i].Cells["COUNTRY_NM"].Value = dt.Rows[i]["COUNTRY_NM"].ToString();
                    inputRmGrid.Rows[i].Cells["UNION_CD"].Value = dt.Rows[i]["UNION_CD"].ToString();
                    inputRmGrid.Rows[i].Cells["LABEL_NM"].Value = dt.Rows[i]["LABEL_NM"].ToString();

                    inputRmGrid.Rows[i].Cells["TOTAL_AMT"].Value = (decimal.Parse(dt.Rows[i]["TOTAL_AMT"].ToString())).ToString("#,0.######");
                    inputRmGrid.Rows[i].Cells["TOTAL_PRICE"].Value = (decimal.Parse(dt.Rows[i]["TOTAL_PRICE"].ToString())).ToString("#,0.######");
                    inputRmGrid.Rows[i].Cells["TOTAL_SUPPLY_MONEY"].Value = (decimal.Parse(dt.Rows[i]["TOTAL_SUPPLY_MONEY"].ToString())).ToString("#,0.######");
                    inputRmGrid.Rows[i].Cells["TOTAL_TAX_MONEY"].Value = (decimal.Parse(dt.Rows[i]["TOTAL_TAX_MONEY"].ToString())).ToString("#,0.######");
                    inputRmGrid.Rows[i].Cells["TOTAL_MONEY"].Value = (decimal.Parse(dt.Rows[i]["TOTAL_MONEY"].ToString())).ToString("#,0.######");
                    inputRmGrid.Rows[i].Cells["GIVE_MONEY"].Value = (decimal.Parse(dt.Rows[i]["GIVE_MONEY"].ToString())).ToString("#,0.######");
                    inputRmGrid.Rows[i].Cells["DC_MONEY"].Value = (decimal.Parse(dt.Rows[i]["DC_MONEY"].ToString())).ToString("#,0.######");
                    inputRmGrid.Rows[i].Cells["TOTAL_GIVE_MONEY"].Value = (decimal.Parse(dt.Rows[i]["TOTAL_GIVE_MONEY"].ToString())).ToString("#,0.######");
                    inputRmGrid.Rows[i].Cells["BALANCE"].Value = (decimal.Parse(dt.Rows[i]["BALANCE"].ToString())).ToString("#,0.######");

                    if (inputRmGrid.Rows[i].Cells["TOTAL_AMT"].Value == null || inputRmGrid.Rows[i].Cells["TOTAL_AMT"].Value.ToString().Equals("0"))
                    {
                        inputRmGrid.Rows[i].Cells["TOTAL_AMT"].Value = "";
                    }
                    if (inputRmGrid.Rows[i].Cells["TOTAL_PRICE"].Value == null || inputRmGrid.Rows[i].Cells["TOTAL_PRICE"].Value.ToString().Equals("0"))
                    {
                        inputRmGrid.Rows[i].Cells["TOTAL_PRICE"].Value = "";
                    }
                    if (inputRmGrid.Rows[i].Cells["TOTAL_SUPPLY_MONEY"].Value == null || inputRmGrid.Rows[i].Cells["TOTAL_SUPPLY_MONEY"].Value.ToString().Equals("0"))
                    {
                        inputRmGrid.Rows[i].Cells["TOTAL_SUPPLY_MONEY"].Value = "";
                    }
                    if (inputRmGrid.Rows[i].Cells["TOTAL_TAX_MONEY"].Value == null || inputRmGrid.Rows[i].Cells["TOTAL_TAX_MONEY"].Value.ToString().Equals("0"))
                    {
                        inputRmGrid.Rows[i].Cells["TOTAL_TAX_MONEY"].Value = "";
                    }
                    if (inputRmGrid.Rows[i].Cells["TOTAL_MONEY"].Value == null || inputRmGrid.Rows[i].Cells["TOTAL_MONEY"].Value.ToString().Equals("0"))
                    {
                        inputRmGrid.Rows[i].Cells["TOTAL_MONEY"].Value = "";
                    }
                    if (inputRmGrid.Rows[i].Cells["GIVE_MONEY"].Value == null || inputRmGrid.Rows[i].Cells["GIVE_MONEY"].Value.ToString().Equals("0"))
                    {
                        inputRmGrid.Rows[i].Cells["GIVE_MONEY"].Value = "";
                    }
                    if (inputRmGrid.Rows[i].Cells["DC_MONEY"].Value == null || inputRmGrid.Rows[i].Cells["DC_MONEY"].Value.ToString().Equals("0"))
                    {
                        inputRmGrid.Rows[i].Cells["DC_MONEY"].Value = "";
                    }
                    if (inputRmGrid.Rows[i].Cells["TOTAL_GIVE_MONEY"].Value == null || inputRmGrid.Rows[i].Cells["TOTAL_GIVE_MONEY"].Value.ToString().Equals("0"))
                    {
                        inputRmGrid.Rows[i].Cells["TOTAL_GIVE_MONEY"].Value = "";
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
            strCondition = "매입처원장";

            frmPrt = readyPrt;
            frmPrt.Show();
            frmPrt.BringToFront();
            //frmPrt.prt_원자재재고현황(adoPrt, strDay1, strDay2, strCust, strCondition);
            frmPrt.prt_매입처원장(adoPrt2,inputRmGrid, strDay1, strDay2, strCondition);

            btn출력.Enabled = true;
        }

        private void btnRaw_Click(object sender, EventArgs e)
        {
            Popup.pop원부재료검색 frm = new Popup.pop원부재료검색();

            //frm.sUsedYN = sUsedYN;
            frm.ShowDialog();

            if (frm.sRetCode != "")
            {
                //cmb_raw.SelectedValue = frm.sRetCode.Trim();
            }

            frm.Dispose();
            frm = null;
        }

        private void btnCust_Click(object sender, EventArgs e)
        {
            Popup.pop거래처검색 frm = new Popup.pop거래처검색();

            frm.sCustGbn = "2";
            frm.ShowDialog();

            if (frm.sCode != "")
            {
               // cmb_cust.SelectedValue = frm.sCode.Trim();
            }

            frm.Dispose();
            frm = null;
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

        private void btn_cust_srch_Click(object sender, EventArgs e)
        {
            Popup.pop거래처검색 msg = new Popup.pop거래처검색();
            msg.sCustGbn = "2";
            msg.sCustNm = txt_srch.Text.ToString();

            msg.ShowDialog();

            if (msg.sCode != null && !msg.sCode.Equals(""))
            {
                txt_srch.Text = msg.sName;
                txt_srch2.Text = msg.sCode;

                input_cust_list();
                inputRmGrid.Rows.Clear();

                in_grid_logic();
                in_grade_logic();
                calBalance_logic();
                wnGConstant wng = new wnGConstant();
                wng.mergeCells(inputRmGrid, 3);
                
            }

        }

        private void input_cust_list()
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = wDm.fn_Cust_List("where CUST_CD = '" + txt_srch2.Text.ToString() + "'  ");
                adoPrt2 = dt.Copy();

                if (dt != null && dt.Rows.Count > 0)
                {
                    txt_cust_nm.Text = dt.Rows[0]["CUST_NM"].ToString();
                    txt_uptae.Text = dt.Rows[0]["UPTAE"].ToString();
                    txt_jongmok.Text = dt.Rows[0]["JONGMOK"].ToString();
                    txt_staff.Text = dt.Rows[0]["CUST_MANAGER"].ToString();
                    txt_saup.Text = dt.Rows[0]["SAUP_NO"].ToString();
                    txt_ceo.Text = dt.Rows[0]["OWNER"].ToString();
                    txt_phone_num.Text = dt.Rows[0]["CUST_COMP_PHONE"].ToString();
                    txt_fax.Text = dt.Rows[0]["CUST_FAX"].ToString();
                    txt_begin_date.Text = dt.Rows[0]["CUST_OPEN"].ToString();
                    txt_address.Text = dt.Rows[0]["ADDR"].ToString();
                    txt_tax.Text = dt.Rows[0]["TAX_NM"].ToString();
                    txt_balance.Text = decimal.Parse(dt.Rows[0]["BALANCE"].ToString()).ToString("#,0.######");

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show("거래처 검색 중 오류가 발생했습니다.");
                return;
            }
        }

        private void txt_srch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Popup.pop거래처검색 msg = new Popup.pop거래처검색();
                msg.sCustGbn = "2";
                msg.txtSrch.Text = txt_srch.Text.ToString();

                msg.ShowDialog();

                if (msg.sCode != null && !msg.sCode.Equals(""))
                {
                    txt_srch.Text = msg.sName;
                    txt_srch2.Text = msg.sCode;

                    input_cust_list();
                    inputRmGrid.Rows.Clear();

                    in_grid_logic();
                    in_grade_logic();
                    calBalance_logic();
                    wnGConstant wng = new wnGConstant();
                    wng.mergeCells(inputRmGrid, 3);

                }
            }
        }
    }
}
