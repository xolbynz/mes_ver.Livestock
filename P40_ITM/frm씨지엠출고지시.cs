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
using 스마트팩토리.Controls;

namespace 스마트팩토리.P40_ITM
{
    public partial class frm씨지엠출고지시 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private DataTable del_inputTable = new DataTable();
        private DateTimePicker dtp = new DateTimePicker();
        private Rectangle _Retangle;

        private string old_cust_nm = "";
        private bool bHeadCheck = false;
        private ComInfo comInfo = new ComInfo();
        private DataGridView copied_dgv = new DataGridView();

        private DataTable Curr_Product_Temp = null;

        private bool isUserInput = true;

        private string currunt_column_temp = "";
        private int currunt_Row_Product = 0;


        public frm씨지엠출고지시()
        {
            

            InitializeComponent();

            this.ProductGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grid_KeyDown);




            this.ProductGrid.Controls.Add(dtp);
            dtp.Visible = false;
            dtp.Format = DateTimePickerFormat.Custom;


            
        }

        private void frm씨지엠출고지시_Load(object sender, EventArgs e)
        {
            //input_list(tdInputGrid, "where convert(varchar(10), Z.INTIME, 120) = convert(varchar(10), getDate(), 120) ");
            //input_list(inputGrid, "where Z.INPUT_DATE >= '" + start_date.Text.ToString() + "' and  Z.INPUT_DATE <= '" + end_date.Text.ToString() + "'");

            ComInfo.gridHeaderSet(inputGrid);
            ComInfo.gridHeaderSet(ProductGrid);
            ComInfo.gridHeaderSet(InputTempGrid);


            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            cellStyle.BackColor = Color.FromArgb(210, 219, 254);
            DataGridViewCellStyle cellStyle2 = new DataGridViewCellStyle();
            cellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            cellStyle2.BackColor = Color.FromArgb(227, 210, 254);

            for (int i = 0; i < 8; i++)
            {
                ProductGrid.Columns[i].HeaderCell.Style = cellStyle;
            }
            for (int i = 8; i < 11; i++)
            {
                ProductGrid.Columns[i].HeaderCell.Style = cellStyle2;
            }
            lbl_usual.BackColor = Color.FromArgb(97, 143, 253);
            lbl_plan.BackColor = Color.FromArgb(148, 97, 253);


            ProductGrid.Columns["BTN_INPUT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            input_list("WHERE 1=1 AND A.COMPLETE_YN = 'N' ");



            del_inputTable.Columns.Add("JISI_DATE");
            del_inputTable.Columns.Add("JISI_CD");
            del_inputTable.Columns.Add("JISI_SEQ");

        }



        private void grid_KeyDown(object sender, KeyEventArgs e)
        {
            // Edit 모드가 아닐때, 작동함.

            conDataGridView grd = (conDataGridView)sender;
            DataGridViewCell cell = grd[grd.CurrentCell.ColumnIndex, grd.CurrentCell.RowIndex];

            if (grd.CurrentCell == null) return;
            if (grd.CurrentCell.RowIndex < 0) return;
            if (grd.CurrentCell.ColumnIndex < 0) return;

            if (e.KeyCode == Keys.Enter)
            {

                e.Handled = true;
            }
        }

        #region button logic

        private void btnNew_Click(object sender, EventArgs e)
        {
            resetSetting();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            inputLogic();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            input_del();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion button logic

        #region input logic
        private void resetSetting()
        {
            txt_Output_cd.Text = "";
            txt_Output_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
            lbl_input_gbn.Text = "";
            btnDelete.Enabled = false;
            txt_Output_date.Enabled = true;
            btn_plan_srch.Visible = true;


            txt_plan_cd.Text = "";
            txt_plan_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txt_cust_cd.Text = "";
            txt_cust_nm.Text = "";
            txt_comment.Text = "";
            txt_tax_cd.Text = "";
            txt_tax_nm.Text = "";

            ProductGrid.Rows.Clear();
            InputTempGrid.Rows.Clear();
            TotalSumGrid.Rows.Clear();
            del_inputTable.Rows.Clear();

        }

        private void inputLogic()
        {
            try
            {
                if (lbl_input_gbn.Text.Equals(""))
                {

                    wnDm wDm = new wnDm();
                    int rsNum = wDm.insert_Out_Jisi_List(
                         txt_Output_date.Text
                         , txt_cust_cd.Text
                         , txt_plan_date.Text
                         , txt_plan_cd.Text
                         , txt_tax_cd.Text
                         , ProductGrid
                         , TotalSumGrid
                         );

                    if (rsNum == 0)
                    {

                        //input_list(tdInputGrid, "where convert(varchar(10), Z.INTIME, 120) = convert(varchar(10), getDate(), 120) ");
                        //input_list(inputGrid, "where Z.INPUT_DATE >= '" + start_date.Text.ToString() + "' and  Z.INPUT_DATE <= '" + end_date.Text.ToString() + "'");
                        input_list("WHERE 1=1 AND A.COMPLETE_YN = 'N' ");

                        resetSetting();

                        MessageBox.Show("성공적으로 등록하였습니다.");

                    }
                    else if (rsNum == 1)
                        MessageBox.Show("저장에 실패하였습니다");
                    else if (rsNum == 2)
                    {
                        MessageBox.Show("조건 검사 중 에러");
                    }
                    else if (rsNum == 3)
                    {
                        MessageBox.Show("발주수량보다 초과 입력 하셨습니다. \n 체크 후 다시 저장 하시기 바랍니다.");
                    }
                    else
                        MessageBox.Show("Exception 에러");

                }
                else
                {
                    wnDm wDm = new wnDm();
                    int rsNum = wDm.Update_Out_Jisi_List(
                         txt_Output_date.Text
                         ,txt_Output_cd.Text
                         , txt_cust_cd.Text
                         , txt_plan_date.Text
                         , txt_plan_cd.Text
                         , txt_tax_cd.Text
                         , ProductGrid
                         , TotalSumGrid
                         , del_inputTable
                         );

                    if (rsNum == 0)
                    {

                        input_list("WHERE 1=1 AND A.COMPLETE_YN = 'N' ");

                        resetSetting();
                        MessageBox.Show("성공적으로 수정하였습니다.");

                    }
                    else if (rsNum == 1)
                        MessageBox.Show("저장에 실패하였습니다");
                    else if (rsNum == 2)
                    {
                        MessageBox.Show("조건 검사 중 에러");
                    }
                    else if (rsNum == 3)
                    {
                        MessageBox.Show("발주수량보다 초과 입력 하셨습니다. \n 체크 후 다시 저장 하시기 바랍니다.");
                    }
                    else
                        MessageBox.Show("Exception 에러");
                }





            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception 에러 발생");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message + ex);
            }

        }

        private void input_list(string condition)
        {
            try
            {
                inputGrid.Rows.Clear();
                wnDm wDm = new wnDm();
                DataTable dt = wDm.fn_Out_Jisi_List(condition);
                if (dt != null && dt.Rows.Count > 0)
                {
                    
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        inputGrid.Rows.Add();
                        inputGrid.Rows[i].Cells["JISI_DATE"].Value = dt.Rows[i]["JISI_DATE"].ToString();
                        inputGrid.Rows[i].Cells["JISI_CD"].Value = dt.Rows[i]["JISI_CD"].ToString();
                        inputGrid.Rows[i].Cells["CUST_CD"].Value = dt.Rows[i]["CUST_CD"].ToString();
                        inputGrid.Rows[i].Cells["CUST_NM"].Value = dt.Rows[i]["CUST_NM"].ToString();
                        inputGrid.Rows[i].Cells["PLAN_DATE"].Value = dt.Rows[i]["PLAN_DATE"].ToString();
                        inputGrid.Rows[i].Cells["PLAN_CD"].Value = dt.Rows[i]["PLAN_CD"].ToString();
                        inputGrid.Rows[i].Cells["ALL_TOTAL_MONEY"].Value = decimal.Parse(dt.Rows[i]["ALL_TOTAL_MONEY"].ToString()).ToString("#,0.######");
                        inputGrid.Rows[i].Cells["COMMENT"].Value = dt.Rows[i]["COMMENT"].ToString();
                        inputGrid.Rows[i].Cells["I_TAX_CD"].Value = dt.Rows[i]["TAX_CD"].ToString();
                        inputGrid.Rows[i].Cells["I_TAX_NM"].Value = dt.Rows[i]["TAX_NM"].ToString();
                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void inputGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            input_detail(e);
        }

        private void input_detail(DataGridViewCellEventArgs e)
        {
            lbl_input_gbn.Text = "1";
            btnDelete.Enabled = true;
            txt_Output_date.Enabled = false;
            btn_plan_srch.Visible = false;
            txt_Output_date.Text = inputGrid.Rows[e.RowIndex].Cells["JISI_DATE"].Value.ToString();
            txt_Output_cd.Text  =  inputGrid.Rows[e.RowIndex].Cells["JISI_CD"].Value.ToString();
            txt_cust_cd.Text  =    inputGrid.Rows[e.RowIndex].Cells["CUST_CD"].Value .ToString();
            txt_cust_nm.Text   =   inputGrid.Rows[e.RowIndex].Cells["CUST_NM"].Value .ToString();
            txt_plan_date.Text =  inputGrid.Rows[e.RowIndex].Cells["PLAN_DATE"].Value.ToString();
            txt_plan_cd.Text  =   inputGrid.Rows[e.RowIndex].Cells["PLAN_CD"].Value.ToString();
            txt_comment.Text = inputGrid.Rows[e.RowIndex].Cells["COMMENT"].Value.ToString();
            txt_tax_cd.Text = inputGrid.Rows[e.RowIndex].Cells["I_TAX_CD"].Value.ToString();
            txt_tax_nm.Text = inputGrid.Rows[e.RowIndex].Cells["I_TAX_NM"].Value.ToString();


            PLANDetail();
            inputDetail2();
        }
        

        

        private void inputDetail2()
        {
            if (ProductGrid.RowCount > 0)
            {
                wnDm wDm = new wnDm();
                DataTable dt = wDm.fn_Out_Jisi_Detail_List("WHERE A.JISI_DATE = '"+txt_Output_date.Text+"' AND A.JISI_CD = '"+txt_Output_cd.Text+"'   ");

                if (dt != null && dt.Rows.Count > 0)
                {
                    for(int i = 0 ; i < dt.Rows.Count ; i++){
                        DataTable dtTemp = (DataTable)ProductGrid.Rows[int.Parse(dt.Rows[i]["PLAN_SEQ"].ToString()) - 1].Cells["INPUT_DGV"].Value;

                        dtTemp.Rows.Add();
                        dtTemp.Rows[dtTemp.Rows.Count - 1]["T_INPUT_DATE"] = dt.Rows[i]["INPUT_DATE"].ToString();
                        dtTemp.Rows[dtTemp.Rows.Count - 1]["T_INPUT_CD"] = dt.Rows[i]["INPUT_CD"].ToString();
                        dtTemp.Rows[dtTemp.Rows.Count - 1]["T_INPUT_SEQ"] = dt.Rows[i]["INPUT_SEQ"].ToString();
                        dtTemp.Rows[dtTemp.Rows.Count - 1]["T_PRODUCT_NM"] = dt.Rows[i]["PRODUCT_NM"].ToString();
                        dtTemp.Rows[dtTemp.Rows.Count - 1]["T_TOTAL_AMT"] = decimal.Parse(dt.Rows[i]["CURR_AMT"].ToString()).ToString("#,0.######");
                        dtTemp.Rows[dtTemp.Rows.Count - 1]["T_EXPRT_DATE"] = dt.Rows[i]["EXPRT_DATE"].ToString();
                        dtTemp.Rows[dtTemp.Rows.Count - 1]["T_USE_AMT"] = decimal.Parse(dt.Rows[i]["TOTAL_AMT"].ToString()).ToString("#,0.######");
                        dtTemp.Rows[dtTemp.Rows.Count - 1]["DEL_SEQ"] = dt.Rows[i]["SEQ"].ToString();
                        dtTemp.Rows[dtTemp.Rows.Count - 1]["T_LABEL_NM"] = dt.Rows[i]["LABEL_NM"].ToString();
                        dtTemp.Rows[dtTemp.Rows.Count - 1]["T_CHUGJONG_NM"] = dt.Rows[i]["CHUGJONG_NM"].ToString();
                        dtTemp.Rows[dtTemp.Rows.Count - 1]["T_CLASS_NM"] = dt.Rows[i]["CLASS_NM"].ToString();
                        dtTemp.Rows[dtTemp.Rows.Count - 1]["T_COUNTRY_NM"] = dt.Rows[i]["COUNTRY_NM"].ToString();
                        dtTemp.Rows[dtTemp.Rows.Count - 1]["T_STORE_GUBUN"] = dt.Rows[i]["STORE_GUBUN"].ToString();
                        if(dt.Rows[i]["STORE_GUBUN"].ToString().Equals("STORE_1F"))
                        {
                            dtTemp.Rows[dtTemp.Rows.Count - 1]["T_STORE_NM"] = "냉동";
                        }
                        else if (dt.Rows[i]["STORE_GUBUN"].ToString().Equals("STORE_1NF"))
                        {
                            dtTemp.Rows[dtTemp.Rows.Count - 1]["T_STORE_NM"] = "냉장";
                        }
                        else if (dt.Rows[i]["STORE_GUBUN"].ToString().Equals("STORE_UF"))
                        {
                            dtTemp.Rows[dtTemp.Rows.Count - 1]["T_STORE_NM"] = "해동";
                        }
                        else if (dt.Rows[i]["STORE_GUBUN"].ToString().Equals("REMAIN_AMT"))
                        {
                            dtTemp.Rows[dtTemp.Rows.Count - 1]["T_STORE_NM"] = "대기";
                        }
                        else 
                        {
                            dtTemp.Rows[dtTemp.Rows.Count - 1]["T_STORE_NM"] = "제품";
                        }

                        
                        if (ProductGrid.Rows[int.Parse(dt.Rows[i]["PLAN_SEQ"].ToString()) - 1].Cells["OUT_AMT"].Value == null
                            || ProductGrid.Rows[int.Parse(dt.Rows[i]["PLAN_SEQ"].ToString()) - 1].Cells["OUT_AMT"].Value.ToString().Equals("0"))
                        {
                            ProductGrid.Rows[int.Parse(dt.Rows[i]["PLAN_SEQ"].ToString()) - 1].Cells["OUT_AMT"].Value = "0";
                        }
                        ProductGrid.Rows[int.Parse(dt.Rows[i]["PLAN_SEQ"].ToString()) - 1].Cells["OUT_AMT"].Value =
                            (decimal.Parse(ProductGrid.Rows[int.Parse(dt.Rows[i]["PLAN_SEQ"].ToString()) - 1].Cells["OUT_AMT"].Value.ToString()) 
                            + decimal.Parse(dt.Rows[i]["TOTAL_AMT"].ToString())).ToString("#,0.######");

                        ProductGrid.Rows[int.Parse(dt.Rows[i]["PLAN_SEQ"].ToString()) - 1].Cells["OUT_PRICE"].Value = decimal.Parse(dt.Rows[i]["TOTAL_PRICE"].ToString()).ToString("#,0.######");
                        Change_TempGrid(dtTemp, int.Parse(dt.Rows[i]["PLAN_SEQ"].ToString()) - 1);
                        cal_tax(int.Parse(dt.Rows[i]["PLAN_SEQ"].ToString()) - 1);
                    }

                    

                }
            }
        }

        private void input_del()
        {
            wnDm wDm = new wnDm();
            try
            {
                int rsNum = wDm.delete_Out_Jisi(
                    txt_Output_date.Text.ToString()
                    , txt_Output_cd.Text.ToString()
                    , txt_plan_date.Text.ToString()
                    , txt_plan_cd.Text.ToString()
                    );


                if (rsNum == 0)
                {
                    resetSetting();

                    input_list("WHERE 1=1 AND A.COMPLETE_YN = 'N' ");

                    MessageBox.Show("성공적으로 삭제하였습니다.");
                }
                else if (rsNum == 1)
                {
                    MessageBox.Show("저장에 실패하였습니다");
                }
                else
                {
                    MessageBox.Show("Exception 에러");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }



        #endregion input logic


        #region main grid logic
 
        private void txtCheckNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e);
        }

   

        private void btn_work_inst_srch_Click(object sender, EventArgs e)
        {
            Popup.pop씨지엠주문서검색 popup = new Popup.pop씨지엠주문서검색();

            popup.ShowDialog();


            if (popup.sJumun_date != null && !popup.sJumun_date.Equals(""))
            {
                txt_plan_date.Text = popup.sJumun_date;
                txt_plan_cd.Text = popup.sJumun_cd;
                txt_cust_cd.Text = popup.sCust_cd;
                txt_cust_nm.Text = popup.sCust_nm;
                txt_comment.Text = popup.sComment;
                txt_tax_cd.Text = popup.sTax_cd;
                txt_tax_nm.Text = popup.sTax_nm;

                PLANDetail();
            }
        }

        private void PLANDetail()
        {
            try
            {
                wnDm wdm = new wnDm();
                DataTable dt = wdm.fn_Plan_Detail_List("WHERE A.PLAN_DATE = '" + txt_plan_date.Text + "' AND A.PLAN_CD = '" + txt_plan_cd.Text + "'  ");
                ProductGrid.Rows.Clear();
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProductGrid.Rows.Add();
                        ProductGrid.Rows[i].Cells[0].Value = (i + 1);
                        ProductGrid.Rows[i].Cells["PRODUCT_GUBUN"].Value = (dt.Rows[i]["RAW_ITEM_GUBUN"].ToString().Equals("1") ? "상품" : "제품");
                        ProductGrid.Rows[i].Cells["PRODUCT_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                        ProductGrid.Rows[i].Cells["PRODUCT_NM"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                        ProductGrid.Rows[i].Cells["CHUGJONG_CD"].Value = dt.Rows[i]["CHUGJONG_CD"].ToString();
                        ProductGrid.Rows[i].Cells["CHUGJONG_NM"].Value = dt.Rows[i]["CHUGJONG_NM"].ToString();
                        ProductGrid.Rows[i].Cells["CLASS_CD"].Value = dt.Rows[i]["CLASS_CD"].ToString();
                        ProductGrid.Rows[i].Cells["CLASS_NM"].Value = dt.Rows[i]["CLASS_NM"].ToString();
                        ProductGrid.Rows[i].Cells["COUNTRY_CD"].Value = dt.Rows[i]["COUNTRY_CD"].ToString();
                        ProductGrid.Rows[i].Cells["COUNTRY_NM"].Value = dt.Rows[i]["COUNTRY_NM"].ToString();
                        ProductGrid.Rows[i].Cells["UNIT_CD"].Value = dt.Rows[i]["UNIT_CD"].ToString();
                        ProductGrid.Rows[i].Cells["UNIT_NM"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                        ProductGrid.Rows[i].Cells["LABEL_NM"].Value = dt.Rows[i]["LABEL_NM"].ToString();
                        ProductGrid.Rows[i].Cells["VAT_CD"].Value = dt.Rows[i]["VAT_CD"].ToString();
                        ProductGrid.Rows[i].Cells["PLAN_AMT"].Value = decimal.Parse(dt.Rows[i]["TOTAL_AMT"].ToString()).ToString("#,0.######");
                        ProductGrid.Rows[i].Cells["PLAN_MONEY"].Value = decimal.Parse(dt.Rows[i]["TOTAL_MONEY"].ToString()).ToString("#,0.######");

                        ProductGrid.Rows[i].Cells["OUT_AMT"].Value = "0";
                        ProductGrid.Rows[i].Cells["OUT_PRICE"].Value = "0";
                        ProductGrid.Rows[i].Cells["TOTAL_MONEY"].Value = "0";
                        ProductGrid.Rows[i].Cells["OUT_MONEY"].Value = "0";
                        ProductGrid.Rows[i].Cells["TAX"].Value = "0";
                        DataTable tempDt = new DataTable();

                        tempDt.Rows.Clear();
                        tempDt.Columns.Add("T_INPUT_DATE");
                        tempDt.Columns.Add("T_INPUT_CD");
                        tempDt.Columns.Add("T_INPUT_SEQ");
                        tempDt.Columns.Add("T_PRODUCT_NM");
                        tempDt.Columns.Add("T_TOTAL_AMT");
                        tempDt.Columns.Add("T_USE_AMT");
                        tempDt.Columns.Add("T_EXPRT_DATE");
                        tempDt.Columns.Add("DEL_SEQ");
                        tempDt.Columns.Add("T_CHUGJONG_NM");
                        tempDt.Columns.Add("T_CLASS_NM");
                        tempDt.Columns.Add("T_COUNTRY_NM");
                        tempDt.Columns.Add("T_LABEL_NM");
                        tempDt.Columns.Add("T_STORE_GUBUN");
                        tempDt.Columns.Add("T_STORE_NM");

                        ProductGrid.Rows[i].Cells["INPUT_DGV"].Value = tempDt;
                    }
                    TotalSumGrid.Rows.Clear();
                    TotalSumGrid.Rows.Add();
                    TotalSumGrid.Rows[0].Cells["SUM_OUT_MONEY"].Value = "0";
                    TotalSumGrid.Rows[0].Cells["SUM_TAX"].Value = "0";
                    TotalSumGrid.Rows[0].Cells["SUM_TOTAL_MONEY"].Value = "0";


                }
                else
                {
                    MessageBox.Show("주문 제품이 조회되지 않습니다");
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message + ex);
            }
        }


        private void ProductGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                string product_cd = ProductGrid.Rows[e.RowIndex].Cells["PRODUCT_CD"].Value.ToString();
                string product_gubun = ProductGrid.Rows[e.RowIndex].Cells["PRODUCT_GUBUN"].Value.ToString();
                SearchInputDetails(product_cd, product_gubun, e.RowIndex);
            }
        }

        private void SearchInputDetails(string product_cd, string product_gubun, int e_RowIndex)
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = wDm.fn_input_Rm_Item_List(product_cd, product_gubun);
                Popup.pop_sf_상품제품입고목록 msg = new Popup.pop_sf_상품제품입고목록();
                msg.dt = dt;
                msg.sGubun = product_gubun;


                msg.ShowDialog();


                if (msg.sInputDate != null && !msg.sInputDate.Equals(""))
                {

                    DataTable tempDt = (DataTable)ProductGrid.Rows[e_RowIndex].Cells["INPUT_DGV"].Value;

                    for (int i = 0; i < tempDt.Rows.Count; i++)
                    {
                        if (tempDt.Rows[i]["T_INPUT_DATE"].ToString().Equals(msg.sInputDate)
                            && tempDt.Rows[i]["T_INPUT_CD"].ToString().Equals(msg.sInputCd)
                            && tempDt.Rows[i]["T_INPUT_SEQ"].ToString().Equals(msg.sInputSeq)
                            && tempDt.Rows[i]["T_STORE_GUBUN"].ToString().Equals(msg.sStore_gubun))
                        {
                            MessageBox.Show("이미 해당 품목이 등록되어 있습니다");
                            return;
                        }

                    }

                    tempDt.Rows.Add();
                    tempDt.Rows[tempDt.Rows.Count - 1]["T_PRODUCT_NM"] = msg.sName;
                    tempDt.Rows[tempDt.Rows.Count - 1]["T_INPUT_DATE"] = msg.sInputDate;
                    tempDt.Rows[tempDt.Rows.Count - 1]["T_INPUT_CD"] = msg.sInputCd;
                    tempDt.Rows[tempDt.Rows.Count - 1]["T_INPUT_SEQ"] = msg.sInputSeq;
                    tempDt.Rows[tempDt.Rows.Count - 1]["T_TOTAL_AMT"] = msg.sTotalAmt;
                    tempDt.Rows[tempDt.Rows.Count - 1]["T_EXPRT_DATE"] = msg.sExprt_date;
                    tempDt.Rows[tempDt.Rows.Count - 1]["T_CHUGJONG_NM"] = msg.sChugjong_nm;
                    tempDt.Rows[tempDt.Rows.Count - 1]["T_CLASS_NM"] = msg.sClass_nm;
                    tempDt.Rows[tempDt.Rows.Count - 1]["T_COUNTRY_NM"] = msg.sCountry_nm;
                    tempDt.Rows[tempDt.Rows.Count - 1]["T_LABEL_NM"] = msg.sLabel_nm;
                    tempDt.Rows[tempDt.Rows.Count - 1]["T_STORE_GUBUN"] = msg.sStore_gubun;
                    tempDt.Rows[tempDt.Rows.Count - 1]["T_STORE_NM"] = msg.sStore_nm;
                    tempDt.Rows[tempDt.Rows.Count - 1]["T_USE_AMT"] = 0;

                    tempDt.DefaultView.Sort = "T_EXPRT_DATE";
                    tempDt = tempDt.DefaultView.ToTable();

                    ProductGrid.Rows[e_RowIndex].Cells["INPUT_DGV"].Value = tempDt;

                    InputTempGrid.Rows.Clear();
                    for (int i = 0; i < tempDt.Rows.Count; i++)
                    {
                        InputTempGrid.Rows.Add();
                        InputTempGrid.Rows[i].Cells["T_INPUT_DATE"].Value = tempDt.Rows[i]["T_INPUT_DATE"].ToString();
                        InputTempGrid.Rows[i].Cells["T_INPUT_CD"].Value = tempDt.Rows[i]["T_INPUT_CD"].ToString();
                        InputTempGrid.Rows[i].Cells["T_INPUT_SEQ"].Value = tempDt.Rows[i]["T_INPUT_SEQ"].ToString();
                        InputTempGrid.Rows[i].Cells["T_PRODUCT_NM"].Value = tempDt.Rows[i]["T_PRODUCT_NM"].ToString();
                        InputTempGrid.Rows[i].Cells["T_TOTAL_AMT"].Value = tempDt.Rows[i]["T_TOTAL_AMT"].ToString();
                        InputTempGrid.Rows[i].Cells["T_USE_AMT"].Value = tempDt.Rows[i]["T_USE_AMT"].ToString();
                        InputTempGrid.Rows[i].Cells["T_EXPRT_DATE"].Value = tempDt.Rows[i]["T_EXPRT_DATE"].ToString();
                        InputTempGrid.Rows[i].Cells["DEL_SEQ"].Value = tempDt.Rows[i]["DEL_SEQ"].ToString();
                        InputTempGrid.Rows[i].Cells["T_LABEL_NM"].Value = tempDt.Rows[i]["T_LABEL_NM"].ToString();
                        InputTempGrid.Rows[i].Cells["T_CHUGJONG_NM"].Value = tempDt.Rows[i]["T_CHUGJONG_NM"].ToString();
                        InputTempGrid.Rows[i].Cells["T_CLASS_NM"].Value = tempDt.Rows[i]["T_CLASS_NM"].ToString();
                        InputTempGrid.Rows[i].Cells["T_COUNTRY_NM"].Value = tempDt.Rows[i]["T_COUNTRY_NM"].ToString();
                        InputTempGrid.Rows[i].Cells["T_STORE_GUBUN"].Value = tempDt.Rows[i]["T_STORE_GUBUN"].ToString();
                        InputTempGrid.Rows[i].Cells["T_STORE_NM"].Value = tempDt.Rows[i]["T_STORE_NM"].ToString();
                    }

                    Change_TempGrid(tempDt,e_RowIndex);


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message + ex);

            }
        }

        private void ProductGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataTable tempDt = (DataTable)ProductGrid.Rows[e.RowIndex].Cells["INPUT_DGV"].Value;

                Change_TempGrid(tempDt,e.RowIndex);

            }
        }

        private void Change_TempGrid(DataTable tempDt, int ProductRow)
        {
            Curr_Product_Temp = tempDt;
            currunt_Row_Product = ProductRow;
            InputTempGrid.Rows.Clear();
            
            for (int i = 0; i < tempDt.Rows.Count; i++)
            {
                InputTempGrid.Rows.Add();
                InputTempGrid.Rows[i].Cells["T_INPUT_DATE"].Value = tempDt.Rows[i]["T_INPUT_DATE"].ToString();
                InputTempGrid.Rows[i].Cells["T_INPUT_CD"].Value = tempDt.Rows[i]["T_INPUT_CD"].ToString();
                InputTempGrid.Rows[i].Cells["T_INPUT_SEQ"].Value = tempDt.Rows[i]["T_INPUT_SEQ"].ToString();
                InputTempGrid.Rows[i].Cells["T_PRODUCT_NM"].Value = tempDt.Rows[i]["T_PRODUCT_NM"].ToString();
                InputTempGrid.Rows[i].Cells["T_TOTAL_AMT"].Value = tempDt.Rows[i]["T_TOTAL_AMT"].ToString();
                InputTempGrid.Rows[i].Cells["T_USE_AMT"].Value = tempDt.Rows[i]["T_USE_AMT"].ToString();
                InputTempGrid.Rows[i].Cells["T_EXPRT_DATE"].Value = tempDt.Rows[i]["T_EXPRT_DATE"].ToString();
                InputTempGrid.Rows[i].Cells["DEL_SEQ"].Value = tempDt.Rows[i]["DEL_SEQ"].ToString();
                InputTempGrid.Rows[i].Cells["T_LABEL_NM"].Value = tempDt.Rows[i]["T_LABEL_NM"].ToString();
                InputTempGrid.Rows[i].Cells["T_CHUGJONG_NM"].Value = tempDt.Rows[i]["T_CHUGJONG_NM"].ToString();
                InputTempGrid.Rows[i].Cells["T_CLASS_NM"].Value = tempDt.Rows[i]["T_CLASS_NM"].ToString();
                InputTempGrid.Rows[i].Cells["T_COUNTRY_NM"].Value = tempDt.Rows[i]["T_COUNTRY_NM"].ToString();
                InputTempGrid.Rows[i].Cells["T_STORE_GUBUN"].Value = tempDt.Rows[i]["T_STORE_GUBUN"].ToString();
                InputTempGrid.Rows[i].Cells["T_STORE_NM"].Value = tempDt.Rows[i]["T_STORE_NM"].ToString();
            }
        }



        private void ProductGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && ProductGrid.Columns[e.ColumnIndex].Name.ToString().Equals("OUT_AMT"))
            {
                string use_amt = "";
                if (ProductGrid.Rows[e.RowIndex].Cells["OUT_AMT"].Value == null || ProductGrid.Rows[e.RowIndex].Cells["OUT_AMT"].Value.ToString().Equals(""))
                {
                    use_amt = "0";
                    ProductGrid.Rows[e.RowIndex].Cells["OUT_AMT"].Value = "0";
                }
                else
                {
                    use_amt = ProductGrid.Rows[e.RowIndex].Cells["OUT_AMT"].Value.ToString();
                }
                Change_TempGrid((DataTable)ProductGrid.Rows[e.RowIndex].Cells["INPUT_DGV"].Value,e.RowIndex);
                bool result = input_use_amt(e.RowIndex, use_amt, 0);
                if (!result)
                {
                    if (!ProductGrid.Rows[e.RowIndex].Cells["OUT_AMT"].Value.ToString().Equals("0"))
                    {
                        MessageBox.Show("출고 지시를 위한 충분한 재고가 없거나, 사용 품목이 등록되어 있지 않습니다.");
                    }
                    ProductGrid.Rows[e.RowIndex].Cells["OUT_AMT"].Value = "0";
                    input_use_amt(e.RowIndex, "0", 0);
                }
            }
            cal_tax(e.RowIndex);
        }

        private void cal_tax(int e_RowIndex)
        {
            if (ProductGrid.Rows[e_RowIndex].Cells["OUT_AMT"].Value == null || ProductGrid.Rows[e_RowIndex].Cells["OUT_AMT"].Value.ToString().Equals(""))
            {
                ProductGrid.Rows[e_RowIndex].Cells["OUT_AMT"].Value = "0";
            }
            if (ProductGrid.Rows[e_RowIndex].Cells["OUT_MONEY"].Value == null || ProductGrid.Rows[e_RowIndex].Cells["OUT_MONEY"].Value.ToString().Equals(""))
            {
                ProductGrid.Rows[e_RowIndex].Cells["OUT_MONEY"].Value = "0";
            }
            if (ProductGrid.Rows[e_RowIndex].Cells["TAX"].Value == null || ProductGrid.Rows[e_RowIndex].Cells["TAX"].Value.ToString().Equals(""))
            {
                ProductGrid.Rows[e_RowIndex].Cells["TAX"].Value = "0";
            }
            if (ProductGrid.Rows[e_RowIndex].Cells["TOTAL_MONEY"].Value == null || ProductGrid.Rows[e_RowIndex].Cells["TOTAL_MONEY"].Value.ToString().Equals(""))
            {
                ProductGrid.Rows[e_RowIndex].Cells["TOTAL_MONEY"].Value = "0";
            }
            if (ProductGrid.Rows[e_RowIndex].Cells["OUT_PRICE"].Value == null || ProductGrid.Rows[e_RowIndex].Cells["OUT_PRICE"].Value.ToString().Equals(""))
            {
                ProductGrid.Rows[e_RowIndex].Cells["OUT_PRICE"].Value = "0";
            }

            if (ProductGrid.Rows[e_RowIndex].Cells["VAT_CD"].Value.ToString().Equals("2")) //면세
            {
                decimal outmoney = decimal.Parse(ProductGrid.Rows[e_RowIndex].Cells["OUT_AMT"].Value.ToString()) * decimal.Parse(ProductGrid.Rows[e_RowIndex].Cells["OUT_PRICE"].Value.ToString());
                decimal tax = 0;
                decimal totalMoney = outmoney + tax;

                ProductGrid.Rows[e_RowIndex].Cells["OUT_MONEY"].Value = outmoney.ToString("#,0.######");
                ProductGrid.Rows[e_RowIndex].Cells["TAX"].Value = tax.ToString("#,0.######"); ;
                ProductGrid.Rows[e_RowIndex].Cells["TOTAL_MONEY"].Value = totalMoney.ToString("#,0.######");
            }
            else //과세일 경우 
            {
                if (txt_tax_cd.Text.Equals("0")) // 부가세 별도
                {
                    decimal outmoney = decimal.Parse(ProductGrid.Rows[e_RowIndex].Cells["OUT_AMT"].Value.ToString()) * decimal.Parse(ProductGrid.Rows[e_RowIndex].Cells["OUT_PRICE"].Value.ToString());
                    decimal tax = outmoney * decimal.Parse("0.1");
                    decimal totalMoney = outmoney + tax;

                    ProductGrid.Rows[e_RowIndex].Cells["OUT_MONEY"].Value = outmoney.ToString("#,0.######");
                    ProductGrid.Rows[e_RowIndex].Cells["TAX"].Value = tax.ToString("#,0.######"); ;
                    ProductGrid.Rows[e_RowIndex].Cells["TOTAL_MONEY"].Value = totalMoney.ToString("#,0.######");

                }
                else if (txt_tax_cd.Text.Equals("1")) // 부가세 포함
                {
                    //얘
                    decimal totalMoney = decimal.Parse(ProductGrid.Rows[e_RowIndex].Cells["OUT_AMT"].Value.ToString()) * decimal.Parse(ProductGrid.Rows[e_RowIndex].Cells["OUT_PRICE"].Value.ToString());
                    decimal outmoney = totalMoney / decimal.Parse("1.1");
                    outmoney = decimal.Round(outmoney, 0);
                    decimal tax = totalMoney - outmoney;

                    ProductGrid.Rows[e_RowIndex].Cells["OUT_MONEY"].Value = outmoney.ToString("#,0.######");
                    ProductGrid.Rows[e_RowIndex].Cells["TAX"].Value = tax.ToString("#,0.######"); ;
                    ProductGrid.Rows[e_RowIndex].Cells["TOTAL_MONEY"].Value = totalMoney.ToString("#,0.######");
                }
                else
                { //영세율
                    decimal outmoney = decimal.Parse(ProductGrid.Rows[e_RowIndex].Cells["OUT_AMT"].Value.ToString()) * decimal.Parse(ProductGrid.Rows[e_RowIndex].Cells["OUT_PRICE"].Value.ToString());
                    decimal tax = 0;
                    decimal totalMoney = outmoney + tax;

                    ProductGrid.Rows[e_RowIndex].Cells["OUT_MONEY"].Value = outmoney.ToString("#,0.######");
                    ProductGrid.Rows[e_RowIndex].Cells["TAX"].Value = tax.ToString("#,0.######"); ;
                    ProductGrid.Rows[e_RowIndex].Cells["TOTAL_MONEY"].Value = totalMoney.ToString("#,0.######");
                }
            }

            if (txt_tax_cd.Text.Equals("1"))
            {
                decimal sumTotMoney = 0;
                decimal sum과세OutMoney = 0;
                decimal sum면세OutMoney = 0;
                decimal oneDotone = decimal.Parse("1.1");

                for (int i = 0; i < ProductGrid.Rows.Count; i++)
                {
                    sumTotMoney += decimal.Parse(ProductGrid.Rows[i].Cells["TOTAL_MONEY"].Value.ToString());
                    if (ProductGrid.Rows[i].Cells["VAT_CD"].Value.ToString().Equals("1"))
                    {
                        sum과세OutMoney += decimal.Parse(ProductGrid.Rows[i].Cells["TOTAL_MONEY"].Value.ToString());
                    }
                    else
                    {
                        sum면세OutMoney += decimal.Parse(ProductGrid.Rows[i].Cells["TOTAL_MONEY"].Value.ToString());
                    }
                }
                TotalSumGrid.Rows[0].Cells["SUM_TOTAL_MONEY"].Value = sumTotMoney.ToString("#,0.######");
                TotalSumGrid.Rows[0].Cells["SUM_OUT_MONEY"].Value = (decimal.Round(((sum과세OutMoney / oneDotone) + sum면세OutMoney), 0)).ToString("#,0.######");
                TotalSumGrid.Rows[0].Cells["SUM_TAX"].Value = (decimal.Parse(TotalSumGrid.Rows[0].Cells["SUM_TOTAL_MONEY"].Value.ToString()) - decimal.Parse(TotalSumGrid.Rows[0].Cells["SUM_OUT_MONEY"].Value.ToString())).ToString("#,0.######");
            }
            else
            {
                decimal sumTotMoney = 0;
                decimal sumOutMoney = 0;
                for (int i = 0; i < ProductGrid.Rows.Count; i++)
                {
                    sumTotMoney += decimal.Parse(ProductGrid.Rows[i].Cells["TOTAL_MONEY"].Value.ToString());
                    sumOutMoney += decimal.Parse(ProductGrid.Rows[i].Cells["OUT_MONEY"].Value.ToString());
                }
                TotalSumGrid.Rows[0].Cells["SUM_TOTAL_MONEY"].Value = sumTotMoney.ToString("#,0.######");
                TotalSumGrid.Rows[0].Cells["SUM_OUT_MONEY"].Value = sumOutMoney.ToString("#,0.######");
                TotalSumGrid.Rows[0].Cells["SUM_TAX"].Value = (decimal.Parse(TotalSumGrid.Rows[0].Cells["SUM_TOTAL_MONEY"].Value.ToString()) - decimal.Parse(TotalSumGrid.Rows[0].Cells["SUM_OUT_MONEY"].Value.ToString())).ToString("#,0.######");

            }
        }



        private bool input_use_amt(int e_Rowindex, string use_amt, int TempIndex)
        {

            DataTable TempDt = (DataTable)ProductGrid.Rows[e_Rowindex].Cells["INPUT_DGV"].Value;
            bool flag = false;
            if (TempIndex > InputTempGrid.Rows.Count - 1)
            {
                return false;
            }
            else if (decimal.Parse(TempDt.Rows[TempIndex]["T_TOTAL_AMT"].ToString()) < decimal.Parse(use_amt))
            {
                use_amt = (decimal.Parse(use_amt) - decimal.Parse(TempDt.Rows[TempIndex]["T_TOTAL_AMT"].ToString())).ToString();
                flag = input_use_amt(e_Rowindex, use_amt, TempIndex + 1);
                if (flag)
                {
                    TempDt.Rows[TempIndex]["T_USE_AMT"] = TempDt.Rows[TempIndex]["T_TOTAL_AMT"].ToString();
                    InputTempGrid.Rows[TempIndex].Cells["T_USE_AMT"].Value = InputTempGrid.Rows[TempIndex].Cells["T_TOTAL_AMT"].Value.ToString();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                InputTempGrid.Rows[TempIndex].Cells["T_USE_AMT"].Value = use_amt;
                TempDt.Rows[TempIndex]["T_USE_AMT"] = use_amt;
                use_amt = "0";
                input_use_amt(e_Rowindex, use_amt, TempIndex + 1);

                return true;
            }

        }

        private void btn_minus_Click(object sender, EventArgs e)
        {
            if (Curr_Product_Temp != null && Curr_Product_Temp.Rows.Count > 0)
            {
                if (lbl_input_gbn.Text.Equals("1") && 
                    (Curr_Product_Temp.Rows[InputTempGrid.CurrentCell.RowIndex]["DEL_SEQ"] != null && !Curr_Product_Temp.Rows[InputTempGrid.CurrentCell.RowIndex]["DEL_SEQ"].ToString().Equals(""))
                    )
                {
                    del_inputTable.Rows.Add();
                    del_inputTable.Rows[del_inputTable.Rows.Count - 1]["JISI_DATE"] = txt_Output_date.Text;
                    del_inputTable.Rows[del_inputTable.Rows.Count - 1]["JISI_CD"] = txt_Output_cd.Text;
                    del_inputTable.Rows[del_inputTable.Rows.Count - 1]["JISI_SEQ"] = InputTempGrid.Rows[InputTempGrid.CurrentCell.RowIndex].Cells["DEL_SEQ"].Value.ToString();
                }

                Curr_Product_Temp.Rows.RemoveAt(InputTempGrid.CurrentCell.RowIndex);
                InputTempGrid.Rows.RemoveAt(InputTempGrid.CurrentCell.RowIndex);
                ProductGrid.Rows[currunt_Row_Product].Cells["OUT_AMT"].Value = "0";
                cal_tax(currunt_Row_Product);
            }
        }

        #endregion main grid logic

    }
}
