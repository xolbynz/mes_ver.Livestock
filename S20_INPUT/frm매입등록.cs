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
using System.Data.SqlClient;
using 스마트팩토리.Controls;
using 스마트팩토리.Popup;
using 스마트팩토리.Controller;

namespace 스마트팩토리.S20_INPUT
//스마트팩토리.SF_Sales.S10_SALES.frm매입등록
{
    public partial class frm매입등록 : Form
    {
        ComInfo ComInfo = new ComInfo(); 
        DataGridView del_dgv_itemOut = new DataGridView();

        Popup.frmPrint readyPrt = new Popup.frmPrint();

        DataTable adoPrt = null;
        DataTable adoPrt2 = null;
        wnAdo wAdo = new wnAdo();
        public Popup.frmPrint frmPrt;

        public string strDay1 = "";
        public string strDay2 = "";
        public string strCondition = "";
        private wnGConstant wConst = new wnGConstant();

        public frm매입등록()
        {
            InitializeComponent();
                  
        }

        private void Form매입등록_Load(object sender, EventArgs e)
        {


            ComInfo.gridHeaderSet(inputRmGrid);
            ComInfo.gridHeaderSet(inputGrid);
            init_ComboBox();
            StringBuilder sb = new StringBuilder();
            start_date.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
            end_date.Text = DateTime.Today.ToString("yyyy-MM-dd");
            sb.AppendLine("WHERE  A.BUY_DATE >= '" + start_date.Text.ToString() + "' and  A.BUY_DATE <= '" + end_date.Text.ToString() + "' ");
          
            SalesGrid(inputGrid,sb.ToString()); //날짜, 거래처명 or 상품명
            //SalesMain();
            //SalesDetail();


        }

       
        private void init_ComboBox()
        {
            string sqlQuery = "";

            //납품일 방법
            cmb_tax_cd.ValueMember = "코드";
            cmb_tax_cd.DisplayMember = "명칭";
            sqlQuery = ComInfo.queryCustTax_select();
            wConst.ComboBox_Read_NoBlank(cmb_tax_cd, sqlQuery);


        }
        private void SalesGrid(DataGridView dgv, string condition)
        {
            wnDm wDm = new wnDm();
            DataTable dt = new DataTable();
            dt = wDm.fn_BuyList(condition);
            //dt = qCtrl.fn_WorkList(" where 1=1 ");
            
            if (dt != null && dt.Rows.Count > 0)
            {
                dgv.RowCount = dt.Rows.Count;
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    dgv.Rows[i].Cells["BUY_DATE"].Value = dt.Rows[i]["BUY_DATE"].ToString();
                    dgv.Rows[i].Cells["BUY_CD"].Value = dt.Rows[i]["BUY_CD"].ToString();
                   
                    //dgv.Rows[i].Cells["BALANCE"].Value = dt.Rows[i]["BALANCE"].ToString();
                    dgv.Rows[i].Cells["RS_TOTAL_MONEY"].Value = decimal.Parse(dt.Rows[i]["ALL_TOTAL_MONEY"].ToString()).ToString("#,0.######");
                    dgv.Rows[i].Cells["SUPPLY_MONEY"].Value = decimal.Parse(dt.Rows[i]["ALL_SUPPLY_MONEY"].ToString()).ToString("#,0.######");
                    dgv.Rows[i].Cells["TAX_MONEY"].Value = decimal.Parse(dt.Rows[i]["ALL_TAX_MONEY"].ToString()).ToString("#,0.######");
                    dgv.Rows[i].Cells["CUST_CD"].Value = dt.Rows[i]["CUST_CD"].ToString();
                    dgv.Rows[i].Cells["CUST_NM"].Value = dt.Rows[i]["CUST_NM"].ToString();
                    dgv.Rows[i].Cells["ITEM_AMT"].Value = dt.Rows[i]["ITEM_AMT"].ToString();
                    dgv.Rows[i].Cells["VAT_CD"].Value = dt.Rows[i]["TAX_CD"].ToString();
                    dgv.Rows[i].Cells["BALANCE"].Value = dt.Rows[i]["BALANCE"].ToString();
                    //dgv.Rows[i].Cells["CUST_NM"].Value = dt.Rows[i]["CUST_NM"].ToString();
                      //(decimal.Parse(dt.Rows[i]["SOYO_AMT"].ToString())).ToString("#,0.######");
                    //workRsGrid.Rows[i].Cells["W_SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
                }

            }
            else
            {
                dgv.Rows.Clear();
            }
        }

     
       
        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            resetSetting();
        }
        private void resetSetting()
        {
            btnDelete.Enabled = false;
            dtp_buy_date.Text = ""; 
            txt_buy_cd.Text = "";
            txt_cust_nm.Text = "";
            txt_comment.Text = "";
            txt_totalMoney.Text = "";
            lbl_buy_gbn.Text = "";
            cmb_tax_cd.SelectedValue = "";
            inputRmGrid.Rows.Clear();



        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            save_logic();
        }
        private void save_logic()
        {

           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            del_logic();
        }
        private void del_logic()
        {
        }

        

        private void dgv_BUY_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            SaleMain(inputGrid,e);
            SaleDetail(inputRmGrid, e.RowIndex);

        }
        #region 출고제품뿌리기
        private void SaleDetail(DataGridView dgv, int p)
        {
            wnDm wDm = new wnDm();
            DataTable dt = new DataTable();
            dt = wDm.fn_Buy_Detail_List("AND A.BUY_DATE = '"+dtp_buy_date.Text+"' AND A.BUY_CD ='"+txt_buy_cd.Text+"' ");

            if (dt != null && dt.Rows.Count > 0)
            {
                dgv.RowCount = dt.Rows.Count;
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                   // dgv.Rows[i].Cells["BUY_DATE"].Value = dt.Rows[i]["BUY_DATE"].ToString();
                    //dgv.Rows[i].Cells["BUY_CD"].Value = dt.Rows[i]["BUY_CD"].ToString();

                    //dgv.Rows[i].Cells["BALANCE"].Value = dt.Rows[i]["BALANCE"].ToString();
                    dgv.Rows[i].Cells[0].Value = dt.Rows[i]["BUY_SEQ"].ToString();
                    dgv.Rows[i].Cells["ITEM_CD"].Value = dt.Rows[i]["PRODUCT_CD"].ToString();
                    dgv.Rows[i].Cells["ITEM_NM"].Value = dt.Rows[i]["PRODUCT_NM"].ToString();
                    dgv.Rows[i].Cells["CHUGJONG_NM"].Value = dt.Rows[i]["CHUGJONG_NM"].ToString();
                    dgv.Rows[i].Cells["CLASS_NM"].Value = dt.Rows[i]["CLASS_NM"].ToString();
                    dgv.Rows[i].Cells["TYPE_NM"].Value = dt.Rows[i]["TYPE_NM"].ToString();
                    dgv.Rows[i].Cells["COUNTRY_NM"].Value = dt.Rows[i]["COUNTRY_NM"].ToString();
                    dgv.Rows[i].Cells["LABEL_NM"].Value = dt.Rows[i]["LABEL_NM"].ToString();
                    dgv.Rows[i].Cells["UNION_CD"].Value = dt.Rows[i]["UNION_CD"].ToString();
                    dgv.Rows[i].Cells["SPEC"].Value = dt.Rows[i]["SPEC"].ToString();
                    if (dt.Rows[i]["FROZEN_GUBUN"].ToString().Equals("F"))
                    {
                        dgv.Rows[i].Cells["FROZEN_GUBUN"].Value = "냉동";
                    }
                    else if (dt.Rows[i]["FROZEN_GUBUN"].ToString().Equals("NF"))
                    {
                        dgv.Rows[i].Cells["FROZEN_GUBUN"].Value = "냉장";
                    }
                    else 
                    {
                        dgv.Rows[i].Cells["FROZEN_GUBUN"].Value = "";
                    }
                    //dgv.Rows[i].Cells["SPEC"].Value = dt.Rows[i]["SPEC"].ToString();
                    dgv.Rows[i].Cells["TOTAL_AMT"].Value = decimal.Parse(dt.Rows[i]["TOTAL_AMT"].ToString()).ToString("#,0.######");
                    dgv.Rows[i].Cells["UNIT_NM"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                    dgv.Rows[i].Cells["TOTAL_MONEY"].Value = decimal.Parse(dt.Rows[i]["TOTAL_MONEY"].ToString()).ToString("#,0.######");
                    //dgv.Rows[i].Cells["BUY_GUBUN"].Value = dt.Rows[i]["BUY_GUBUN_NM"].ToString();
                  
                    //dgv.Rows[i].Cells["CUST_NM"].Value = dt.Rows[i]["CUST_NM"].ToString();
                    //(decimal.Parse(dt.Rows[i]["SOYO_AMT"].ToString())).ToString("#,0.######");
                    //workRsGrid.Rows[i].Cells["W_SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
                }

            }
            else
            {
                dgv.Rows.Clear();
            }
        }
        
        #endregion


        #region 매출메인뿌리기
        private void SaleMain(DataGridView dgv_list, DataGridViewCellEventArgs e)
        {
            dtp_buy_date.Text = dgv_list.Rows[e.RowIndex].Cells["BUY_DATE"].Value.ToString();
            txt_cust_cd.Text = dgv_list.Rows[e.RowIndex].Cells["CUST_CD"].Value.ToString();
            txt_buy_cd.Text = dgv_list.Rows[e.RowIndex].Cells["BUY_CD"].Value.ToString();
            cust_nm_txt.Text = dgv_list.Rows[e.RowIndex].Cells["CUST_NM"].Value.ToString();
            txt_cust_nm.Text = dgv_list.Rows[e.RowIndex].Cells["CUST_NM"].Value.ToString();
            txt_totalMoney.Text = dgv_list.Rows[e.RowIndex].Cells["SUPPLY_MONEY"].Value.ToString();
            txt_rs_total_money.Text = dgv_list.Rows[e.RowIndex].Cells["RS_TOTAL_MONEY"].Value.ToString();
            txt_tax_money.Text = dgv_list.Rows[e.RowIndex].Cells["TAX_MONEY"].Value.ToString();
            cmb_tax_cd.SelectedValue = dgv_list.Rows[e.RowIndex].Cells["VAT_CD"].Value.ToString();
         

           
            if (!dgv_list.Rows[e.RowIndex].Cells["BALANCE"].Value.ToString().Equals(""))
            {
                txt_jango.Text = decimal.Parse(dgv_list.Rows[e.RowIndex].Cells["BALANCE"].Value.ToString()).ToString("#,0.######");
            }
            else
            {
                txt_jango.Text = "";
            }


           

            //dgv_itemOut.Rows[i].Cells[""].Value = dt;
        }
        #endregion

        private void btn_search_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("WHERE  A.BUY_DATE >= '" + start_date.Text.ToString() + "' and  A.BUY_DATE <= '" + end_date.Text.ToString() + "' ");

            if (txt_cust_nm.Text!=null && !txt_cust_nm.Text.Equals(""))
            {
                Popup.pop거래처검색 frm = new Popup.pop거래처검색();
                frm.txtSrch.Text = txt_cust_nm.Text.ToString();
                frm.sCustGbn = "2";
                frm.ShowDialog();
                if (frm.sCode != "")
                {
                    txt_cust_cd.Text = frm.sCode.Trim();
                    txt_cust_nm.Text = frm.sName.Trim();

                    sb.AppendLine("  and C.CUST_CD = '" + txt_cust_cd.Text + "'  ");
                }
                frm.Dispose();
                frm = null;
            }
            
            
            SalesGrid(inputGrid, sb.ToString());
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

            
            strCondition = "매출처원장";

            wnDm wDm = new wnDm();


            DataTable myinfo = wDm.fn_Saup_List(Common.p_saupNo);
            DataTable custInfo = wDm.fn_Cust_List("WHERE A.CUST_CD = '"+txt_cust_cd.Text+"'  ");



            DataTable dtTemp = MakeFrmPrt();

            dtTemp.Rows.Clear();
           
            for (int i = 0; i < inputRmGrid.Rows.Count; i++)
            {
                dtTemp.Rows.Add();

                dtTemp.Rows[i]["표시페이지"] = i/13+1;
                dtTemp.Rows[i]["표시순번"] = (i + 1).ToString();
                dtTemp.Rows[i]["매출일자"] = dtp_buy_date.Text;
                dtTemp.Rows[i]["전표번호"] = txt_buy_cd.Text + "-" + (i / 13 + 1).ToString();
                dtTemp.Rows[i]["거래처사업자번호"] = custInfo.Rows[0]["SAUP_NO"].ToString();
                dtTemp.Rows[i]["거래처명"] = custInfo.Rows[0]["CUST_NM"].ToString();
                dtTemp.Rows[i]["거래처대표자명"] = custInfo.Rows[0]["OWNER"].ToString();
                dtTemp.Rows[i]["주소"] = custInfo.Rows[0]["ADDR"].ToString();
                dtTemp.Rows[i]["대체사업자번호2"] = myinfo.Rows[0]["SAUP_NO"].ToString();
                dtTemp.Rows[i]["대체대표자"] = Common.p_strUserName;
                dtTemp.Rows[i]["대체상호명"] = myinfo.Rows[0]["COMPANY_NM"].ToString();
                dtTemp.Rows[i]["대체종목"] = myinfo.Rows[0]["JONGMOK"].ToString();
                dtTemp.Rows[i]["대체주소"] = myinfo.Rows[0]["ADDR"].ToString() + myinfo.Rows[0]["ADDR2"].ToString();
                dtTemp.Rows[i]["입력금액계"] = txt_totalMoney.Text;
                dtTemp.Rows[i]["부가세액계"] = txt_tax_money.Text;
                dtTemp.Rows[i]["포함금액계"] = txt_rs_total_money.Text;

                dtTemp.Rows[i]["상품명"] = inputRmGrid.Rows[i].Cells["LABEL_NM"].Value.ToString();
                dtTemp.Rows[i]["규격"] = inputRmGrid.Rows[i].Cells["SPEC"].Value.ToString();
                dtTemp.Rows[i]["낱개수량"] = inputRmGrid.Rows[i].Cells["TOTAL_AMT"].Value.ToString() + inputRmGrid.Rows[i].Cells["UNIT_NM"].Value.ToString();
                dtTemp.Rows[i]["낱개단가"] = inputRmGrid.Rows[i].Cells["PRICE"].Value.ToString();
                dtTemp.Rows[i]["포함금액"] = inputRmGrid.Rows[i].Cells["TOTAL_MONEY"].Value.ToString();
                dtTemp.Rows[i]["박스수량"] = inputRmGrid.Rows[i].Cells["PAC_AMT"].Value.ToString();
                dtTemp.Rows[i]["비고"] = inputRmGrid.Rows[i].Cells["COMMENT"].Value.ToString();
            }

            if (dtTemp.Rows.Count % 12 > 0)
            {
                int temp = dtTemp.Rows.Count;
                for (int i = 0; i < 12 - temp % 12; i++)
                {
                    dtTemp.Rows.Add();

                    dtTemp.Rows[i + temp ]["표시페이지"] = (i + temp) / 13 + 1;
                    dtTemp.Rows[i + temp ]["표시순번"] = ((i + temp) + 1).ToString();
                    dtTemp.Rows[i + temp ]["매출일자"] = dtp_buy_date.Text;
                    dtTemp.Rows[i + temp ]["전표번호"] = txt_buy_cd.Text + "-" + ((i + temp) / 13 + 1).ToString();
                    dtTemp.Rows[i + temp]["거래처사업자번호"] = custInfo.Rows[0]["SAUP_NO"].ToString();
                    dtTemp.Rows[i + temp]["거래처명"] = custInfo.Rows[0]["CUST_NM"].ToString();
                    dtTemp.Rows[i + temp]["주소"] = custInfo.Rows[0]["ADDR"].ToString();
                    dtTemp.Rows[i + temp]["대체사업자번호2"] = myinfo.Rows[0]["SAUP_NO"].ToString();
                    dtTemp.Rows[i + temp]["대체대표자"] = Common.p_strUserName;
                    dtTemp.Rows[i + temp]["대체상호명"] = myinfo.Rows[0]["COMPANY_NM"].ToString();
                    dtTemp.Rows[i + temp]["대체종목"] = myinfo.Rows[0]["JONGMOK"].ToString();
                    dtTemp.Rows[i + temp]["대체주소"] = myinfo.Rows[0]["ADDR"].ToString() + myinfo.Rows[0]["ADDR2"].ToString();
                    dtTemp.Rows[i + temp]["입력금액계"] = txt_totalMoney.Text;
                    dtTemp.Rows[i + temp]["부가세액계"] = txt_tax_money.Text;
                    dtTemp.Rows[i + temp]["포함금액계"] = txt_rs_total_money.Text;
                    dtTemp.Rows[i + temp]["상품명"] = "";
                    dtTemp.Rows[i + temp]["규격"] = "";
                    dtTemp.Rows[i + temp]["낱개수량"] = "";
                    dtTemp.Rows[i + temp]["낱개단가"] = "";
                    dtTemp.Rows[i + temp]["포함금액"] = "";
                    dtTemp.Rows[i + temp]["박스수량"] = "";
                    dtTemp.Rows[i + temp]["비고"] = "";
                }
               
            }


            frmPrt = readyPrt;
            frmPrt.Show();
            frmPrt.BringToFront();
            //frmPrt.prt_원자재재고현황(adoPrt, strDay1, strDay2, strCust, strCondition);
            frmPrt.prt_거래명세표(dtTemp, inputRmGrid, strCondition);

            btn출력.Enabled = true;
        }


        private DataTable MakeFrmPrt()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("   SELECT           ");

            sb.AppendLine(" '' AS 매출일자             ");
            sb.AppendLine(",'' AS 전표번호             ");
            sb.AppendLine(",'' AS 항목번호             ");
            sb.AppendLine(",'' AS 입력방법             ");
            sb.AppendLine(",'' AS 매출구분명           ");
            sb.AppendLine(",'' AS 매출시각             ");
            sb.AppendLine(",'' AS 서명일시             ");
            sb.AppendLine(",'' AS 거래처명             ");
            sb.AppendLine(",'' AS 거래처사업자번호     ");
            sb.AppendLine(",'' AS 거래처대표자명       ");
            sb.AppendLine(",'' AS 계좌별칭             ");
            sb.AppendLine(",'' AS 주소                 ");
            sb.AppendLine(",'' AS 전잔고               ");
            sb.AppendLine(",'' AS 당일매출액           ");
            sb.AppendLine(",'' AS 당일수금액           ");
            sb.AppendLine(",'' AS 잔고                 ");
            sb.AppendLine(",'' AS 상품코드             ");
            sb.AppendLine(",'' AS 상품명               ");
            sb.AppendLine(",'' AS 규격                 ");
            sb.AppendLine(",'' AS 낱개수량계           ");
            sb.AppendLine(",'' AS 입력금액계           ");
            sb.AppendLine(",'' AS 부가세액계           ");
            sb.AppendLine(",'' AS 총수량계             ");
            sb.AppendLine(",'' AS 포함금액계           ");
            sb.AppendLine(",'' AS 대체사업자번호       ");
            sb.AppendLine(",'' AS 대체사업자번호2      ");
            sb.AppendLine(",'' AS 대체대표자           ");
            sb.AppendLine(",'' AS 대체상호명           ");
            sb.AppendLine(",'' AS 대체연락처           ");
            sb.AppendLine(",'' AS 대체이메일           ");
            sb.AppendLine(",'' AS 대체업태             ");
            sb.AppendLine(",'' AS 대체종목             ");
            sb.AppendLine(",'' AS 대체우편번호         ");
            sb.AppendLine(",'' AS 대체주소             ");
            sb.AppendLine(",'' AS 대체직인파일         ");
            sb.AppendLine(",'' AS 대체공지컴퓨터       ");
            sb.AppendLine(",'' AS 상품폰트             ");
            sb.AppendLine(",'' AS 규격폰트             ");
            sb.AppendLine(",'' AS 상품상세명           ");
            sb.AppendLine(",'' AS 박스수량             ");
            sb.AppendLine(",'' AS 중간수량             ");
            sb.AppendLine(",'' AS 낱개수량             ");
            sb.AppendLine(",'' AS 박스표시             ");
            sb.AppendLine(",'' AS 총수량               ");
            sb.AppendLine(",'' AS 박스단가             ");
            sb.AppendLine(",'' AS 낱개단가             ");
            sb.AppendLine(",'' AS 금액                 ");
            sb.AppendLine(",'' AS 부가세액             ");
            sb.AppendLine(",'' AS 포함금액             ");
            sb.AppendLine(",'' AS 서비스박스           ");
            sb.AppendLine(",'' AS 서비스낱개           ");
            sb.AppendLine(",'' AS 배송사원명           ");
            sb.AppendLine(",'' AS 비고                 ");
            sb.AppendLine(",'' AS 표시순번             ");
            sb.AppendLine(",'' AS 페이지그룹           ");
            sb.AppendLine(",'' AS 표시페이지           ");
            sb.AppendLine(",'' AS 박스바코드           ");
            sb.AppendLine(",'' AS 중간바코드           ");
            sb.AppendLine(",'' AS 낱개바코드           ");
            sb.AppendLine(",'' AS 상품비고             ");
            sb.AppendLine(",'' AS 매출년월             ");
            sb.AppendLine(",'' AS 박스수량계           ");
            sb.AppendLine(",'' AS 중간수량계           ");
            

            SqlCommand sCommand = new SqlCommand(sb.ToString());
            if (sCommand.CommandText.Equals(null))
            {
                wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                return null;
            }
            return wAdo.SqlCommandSelect(sCommand);
        }

        private void txt_cust_nm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("WHERE  A.BUY_DATE >= '" + start_date.Text.ToString() + "' and  A.BUY_DATE <= '" + end_date.Text.ToString() + "' ");

                if (txt_cust_nm.Text != null && !txt_cust_nm.Text.Equals(""))
                {
                    Popup.pop거래처검색 frm = new Popup.pop거래처검색();
                    frm.txtSrch.Text = txt_cust_nm.Text.ToString();
                    frm.sCustGbn = "2";
                    frm.ShowDialog();
                    if (frm.sCode != "")
                    {
                        txt_cust_cd.Text = frm.sCode.Trim();
                        txt_cust_nm.Text = frm.sName.Trim();

                        sb.AppendLine("  and C.CUST_CD = '" + txt_cust_cd.Text + "'  ");
                    }
                    frm.Dispose();
                    frm = null;
                }


                SalesGrid(inputGrid, sb.ToString());
            }
        }




    }
}
