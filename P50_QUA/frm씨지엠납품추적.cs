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

namespace 스마트팩토리.P50_QUA
{
    public partial class frm씨지엠납품추적 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private DataGridView del_inputGrid = new DataGridView();
        private DateTimePicker dtp = new DateTimePicker();
        private Rectangle _Retangle;

        private string old_cust_nm = "";
        private bool bHeadCheck = false;
        private ComInfo comInfo = new ComInfo();
        private DataGridView copied_dgv = new DataGridView();

        public string SrchValue = "";
        public string s_GUBUN = "";
        public string s_GUBUN2 = "";
        public string s_INPUT_DATE = "";
        public string s_INPUT_CD = "";
        public string s_INPUT_SEQ = "";

        private bool isUserInput = true;

        private bool first_touch = false;

        private string currunt_column_temp = "";


        public frm씨지엠납품추적()
        {
            InitializeComponent();

        }

        private void frm씨지엠납품추적_Load(object sender, EventArgs e)
        {
            ComInfo.gridHeaderSet(inputTraceGrid);
            txt_start_date.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
            txt_end_date.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }

        private void inputRmSoyoGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_work_inst_srch_Click(object sender, EventArgs e)
        {
            try
            {
                if (rBtn_s_lot.Checked)
                {
                    SrchValue = ComInfo.TextBoxMessage("납품LOT번호", "입력");
                    if (!SrchValue.Equals(""))
                    {
                        string valueTemp = SrchValue;
                        txt_srch_value.Text = SrchValue;
                        txt_rBtn_value.Text = rBtn_s_lot.Text;

                        if (valueTemp.Length == 9) //200103001
                        {
                            string date = "";
                            string cd = "";
                            try
                            {
                                decimal.Parse(valueTemp); // 양식 체크용
                                date = "20" + valueTemp.Substring(0, 2) + "-" + valueTemp.Substring(2, 2) + "-" + valueTemp.Substring(4, 2);
                                cd = int.Parse(valueTemp.Substring(6, 3)).ToString();
                                ResetSetting();
                                SrchValue = "and S.SALES_DATE = '" + date + "'   AND S.SALES_CD = '" + cd + "'   ";
                                Srch_List();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("입력 양식이 올바르지 않습니다 ( 9자리 )");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("입력 양식이 올바르지 않습니다 ( 9자리 )");
                        }
                    }
                    
                }
                else if (rBtn_lot.Checked)
                {
                    SrchValue = ComInfo.TextBoxMessage("공정LOT번호", "입력");
                    if (!SrchValue.Equals(""))
                    {
                        txt_srch_value.Text = SrchValue;
                        txt_rBtn_value.Text = rBtn_lot.Text;
                        ResetSetting();
                        SrchValue = "and II.LOT_NO = '" + SrchValue + "' ";
                        Srch_List();
                    }

                }
                else if (rBtn_a_union.Checked)
                {
                    SrchValue = ComInfo.TextBoxMessage("묶음코드(제품)", "입력");
                    if (!SrchValue.Equals(""))
                    {
                        txt_srch_value.Text = SrchValue;
                        txt_rBtn_value.Text = rBtn_a_union.Text;
                        ResetSetting();
                        SrchValue = "and A_UNION_CD = '" + SrchValue + "' ";
                        Srch_List();
                    }
                }
                else if (rBtn_srch_item.Checked)
                {
                    Popup.pop_sf_제품검색 msg = new Popup.pop_sf_제품검색();
                    msg.ShowDialog();

                    if (msg.sCode != null && !msg.sCode.Equals(""))
                    {
                        SrchValue = msg.sCode;
                        txt_srch_value.Text = msg.sLabelNM;
                        txt_rBtn_value.Text = rBtn_srch_item.Text;
                        ResetSetting();
                        SrchValue = "and ID.ITEM_CD  = '" + SrchValue + "' and S.SALES_DATE >= '" + txt_start_date.Text + "'  and S.SALES_DATE <= '" + txt_end_date.Text + "'   ";
                        Srch_List();
                    }
                }
                else if (rBtn_srch_cust.Checked)
                {
                    Popup.pop거래처검색 msg = new Popup.pop거래처검색();
                    msg.sCustGbn = "1";
                    msg.ShowDialog();

                    if (msg.sCode != null && !msg.sCode.Equals(""))
                    {
                        SrchValue = msg.sCode;
                        txt_srch_value.Text = msg.sName;
                        txt_rBtn_value.Text = rBtn_srch_cust.Text;
                        ResetSetting();
                        SrchValue = "and CN.CUST_CD = '" + SrchValue + "' and S.SALES_DATE >= '" + txt_start_date.Text + "'  and S.SALES_DATE <= '" + txt_end_date.Text + "'   ";
                        Srch_List();
                    }
                }
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("검색중 오류가 발생했습니다");
                Console.WriteLine(ex);
                return;
            }
        }

       

        private void ResetSetting()
        {
            s_GUBUN = "";
            s_GUBUN2 = "";
            s_INPUT_DATE = "";
            s_INPUT_CD = "";
            s_INPUT_SEQ = "";

            txt_amt.Text = "";
            txt_seq.Text = "";
            txt_date.Text = "";
            txt_cd.Text = "";
            txt_label_nm.Text = "";
            txt_supply.Text = "";
            txt_tax.Text = "";
            txt_money.Text = "";
            txt_cust_nm.Text = "";

            lbl_amt.Text = "수량";
            lbl_date.Text = "일자";
        }

        private void ClearGrids()
        {
            

            inputTraceGrid.Rows.Clear();

        }

        public void Srch_List()
        {
            try
            {
                // 생산 & 지시정보 + 지시 원료육
                wnDm wDm = new wnDm();
                DataTable dt = new DataTable();
                
                inputTraceGrid.Rows.Clear();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" WHERE 1=1 ");

                sb.AppendLine(SrchValue);

                dt = wDm.fn_sales_trace_list(sb.ToString());

                if (dt != null && dt.Rows.Count > 0)
                {
                    inputTraceGrid.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        inputTraceGrid.Rows[i].Cells["GUBUN"].Value = dt.Rows[i]["GUBUN"].ToString();
                        inputTraceGrid.Rows[i].Cells["INPUT_DATE"].Value = dt.Rows[i]["INPUT_DATE"].ToString();
                        inputTraceGrid.Rows[i].Cells["INPUT_CD"].Value = dt.Rows[i]["INPUT_CD"].ToString();
                        inputTraceGrid.Rows[i].Cells["INPUT_SEQ"].Value = dt.Rows[i]["INPUT_SEQ"].ToString();
                        inputTraceGrid.Rows[i].Cells["CUST_NM"].Value = dt.Rows[i]["CUST_NM"].ToString();
                        inputTraceGrid.Rows[i].Cells["RAW_MAT_NM"].Value = dt.Rows[i]["RAW_MAT_NM"].ToString();
                        inputTraceGrid.Rows[i].Cells["LABEL_NM"].Value = dt.Rows[i]["LABEL_NM"].ToString();
                        inputTraceGrid.Rows[i].Cells["EXPRT_DATE"].Value = dt.Rows[i]["EXPRT_DATE"].ToString();
                        inputTraceGrid.Rows[i].Cells["FROZEN_GUBUN"].Value = dt.Rows[i]["FROZEN_GUBUN"].ToString();
                        inputTraceGrid.Rows[i].Cells["A_UNION_CD"].Value = dt.Rows[i]["A_UNION_CD"].ToString();
                        inputTraceGrid.Rows[i].Cells["UNIT_NM"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                        inputTraceGrid.Rows[i].Cells["TOTAL_AMT"].Value = decimal.Parse(dt.Rows[i]["TOTAL_AMT"].ToString()).ToString("#,0.######");
                        inputTraceGrid.Rows[i].Cells["TOTAL_PRICE"].Value = decimal.Parse(dt.Rows[i]["TOTAL_PRICE"].ToString()).ToString("#,0.######");
                        inputTraceGrid.Rows[i].Cells["TOTAL_SUPPLY_MONEY"].Value = decimal.Parse(dt.Rows[i]["TOTAL_SUPPLY_MONEY"].ToString()).ToString("#,0.######");
                        inputTraceGrid.Rows[i].Cells["TOTAL_TAX_MONEY"].Value = decimal.Parse(dt.Rows[i]["TOTAL_TAX_MONEY"].ToString()).ToString("#,0.######");
                        inputTraceGrid.Rows[i].Cells["TOTAL_MONEY"].Value = decimal.Parse(dt.Rows[i]["TOTAL_MONEY"].ToString()).ToString("#,0.######");
                        
                        

                        if ((dt.Rows[i]["GUBUN"].ToString().Equals(s_GUBUN) || dt.Rows[i]["GUBUN"].ToString().Equals(s_GUBUN2))
                            && dt.Rows[i]["INPUT_DATE"].ToString().Equals(s_INPUT_DATE)
                            && dt.Rows[i]["INPUT_CD"].ToString().Equals(s_INPUT_CD)
                            && dt.Rows[i]["INPUT_SEQ"].ToString().Equals(s_INPUT_SEQ))
                        {

                            txt_amt.Text = inputTraceGrid.Rows[i].Cells["TOTAL_AMT"].Value.ToString();
                            txt_seq.Text = inputTraceGrid.Rows[i].Cells["INPUT_SEQ"].Value.ToString();
                            txt_date.Text = inputTraceGrid.Rows[i].Cells["INPUT_DATE"].Value.ToString();
                            txt_cd.Text = inputTraceGrid.Rows[i].Cells["INPUT_CD"].Value.ToString();
                            txt_label_nm.Text = inputTraceGrid.Rows[i].Cells["LABEL_NM"].Value.ToString();
                            txt_supply.Text = inputTraceGrid.Rows[i].Cells["TOTAL_SUPPLY_MONEY"].Value.ToString();
                            txt_tax.Text = inputTraceGrid.Rows[i].Cells["TOTAL_TAX_MONEY"].Value.ToString();
                            txt_money.Text = inputTraceGrid.Rows[i].Cells["TOTAL_MONEY"].Value.ToString();
                            txt_cust_nm.Text = inputTraceGrid.Rows[i].Cells["CUST_NM"].Value.ToString();

                            lbl_amt.Text = inputTraceGrid.Rows[i].Cells["GUBUN"].Value.ToString() + "수량";
                            lbl_date.Text = inputTraceGrid.Rows[i].Cells["GUBUN"].Value.ToString() + "일자";

                            DataGridViewCellStyle style = new DataGridViewCellStyle();
                            style.BackColor = Color.Firebrick;
                            style.SelectionBackColor = Color.DarkRed;
                            style.ForeColor = Color.White;
                            style.SelectionForeColor = Color.White;
                            for (int j = 3; j < inputTraceGrid.ColumnCount; j++)
                            {
                                inputTraceGrid.Rows[i].Cells[j].Style = style;
                            }
                        }


                    }
                    wnGConstant wng = new wnGConstant();
                    wng.mergeCells(inputTraceGrid, 3);
                }
                else
                {
                    MessageBox.Show("납품 내역이 없거나 검색값이 잘못되었습니다.");
                    txt_rBtn_value.Text = "";
                    txt_srch_value.Text = "";
                    return;
                }

                   
                
                


                //// 소비원부자재
                //dt = wDm.fn_flow_trace_list_Sobi_By_Lot(LotNo);
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    //inputSobiGrid.Rows.Clear();
                //    //inputSobiGrid.RowCount = dt.Rows.Count;

                //    //for (int i = 0; i < dt.Rows.Count; i++)
                //    //{
                //    //    inputSobiGrid.Rows[i].Cells["SO_INPUT_DATE"].Value = dt.Rows[i]["INPUT_DATE"].ToString();
                //    //    inputSobiGrid.Rows[i].Cells["SO_INPUT_CD"].Value = dt.Rows[i]["INPUT_CD"].ToString();
                //    //    inputSobiGrid.Rows[i].Cells["SO_INPUT_SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
                //    //    inputSobiGrid.Rows[i].Cells["SO_RAW_MAT_NM"].Value = dt.Rows[i]["RAW_MAT_NM"].ToString();
                //    //    inputSobiGrid.Rows[i].Cells["SO_LABEL_NM"].Value = dt.Rows[i]["LABEL_NM"].ToString();
                //    //    inputSobiGrid.Rows[i].Cells["SO_EXPRT_DATE"].Value = dt.Rows[i]["EXPRT_DATE"].ToString();
                //    //    inputSobiGrid.Rows[i].Cells["SO_UNION_CD"].Value = dt.Rows[i]["UNION_CD"].ToString();
                //    //    inputSobiGrid.Rows[i].Cells["SO_TOTAL_AMT"].Value = decimal.Parse(dt.Rows[i]["OUTPUT_AMT"].ToString()).ToString("#,0.######");
                //    //    inputSobiGrid.Rows[i].Cells["SO_LOSS_AMT"].Value = decimal.Parse(dt.Rows[i]["LOSS_AMT"].ToString()).ToString("#,0.######");
                //    //    inputSobiGrid.Rows[i].Cells["SO_UNIT_NM"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                //    //}
                //}
                //else
                //{
                //    MessageBox.Show("입력된 LOT번호가 존재하지 않습니다. 재확인 부탁드립니다.");
                //    ClearGrids();
                //    return;
                //}

                //// 생산제품
                //dt = wDm.fn_flow_trace_list_Item_By_Lot(LotNo);
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    //inputItemGrid.Rows.Clear();
                //    //inputItemGrid.RowCount = dt.Rows.Count;

                //    //for (int i = 0; i < dt.Rows.Count; i++)
                //    //{
                //    //    inputItemGrid.Rows[i].Cells["IT_INPUT_DATE"].Value = dt.Rows[i]["INPUT_DATE"].ToString();
                //    //    inputItemGrid.Rows[i].Cells["IT_INPUT_CD"].Value = dt.Rows[i]["INPUT_CD"].ToString();
                //    //    inputItemGrid.Rows[i].Cells["IT_LABEL_NM"].Value = dt.Rows[i]["LABEL_NM"].ToString();
                //    //    inputItemGrid.Rows[i].Cells["IT_EXPRT_DATE"].Value = dt.Rows[i]["EXPRT_DATE"].ToString();
                //    //    inputItemGrid.Rows[i].Cells["IT_FROZEN_GUBUN"].Value = dt.Rows[i]["FROZEN_GUBUN"].ToString();
                //    //    inputItemGrid.Rows[i].Cells["IT_UNION_CD"].Value = dt.Rows[i]["A_UNION_CD"].ToString();
                //    //    inputItemGrid.Rows[i].Cells["IT_TOTAL_AMT"].Value = decimal.Parse(dt.Rows[i]["INPUT_AMT"].ToString()).ToString("#,0.######");
                //    //    inputItemGrid.Rows[i].Cells["IT_UNIT_NM"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                //    //}
                //}
                //else
                //{
                //    MessageBox.Show("입력된 LOT번호가 존재하지 않습니다. 재확인 부탁드립니다.");
                //    ClearGrids();
                //    return;
                //}

                //// 납품내역
                //dt = wDm.fn_flow_trace_list_Sales_By_Lot(LotNo);
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    //inputSalesGrid.Rows.Clear();
                //    //inputSalesGrid.RowCount = dt.Rows.Count;

                //    //for (int i = 0; i < dt.Rows.Count; i++)
                //    //{
                //    //    inputSalesGrid.Rows[i].Cells["SA_INPUT_DATE"].Value = dt.Rows[i]["SALES_DATE"].ToString();
                //    //    inputSalesGrid.Rows[i].Cells["SA_INPUT_CD"].Value = dt.Rows[i]["SALES_CD"].ToString();
                //    //    inputSalesGrid.Rows[i].Cells["SA_CUST_NM"].Value = dt.Rows[i]["CUST_NM"].ToString();
                //    //    inputSalesGrid.Rows[i].Cells["SA_LABEL_NM"].Value = dt.Rows[i]["LABEL_NM"].ToString();
                //    //    inputSalesGrid.Rows[i].Cells["SA_EXPRT_DATE"].Value = dt.Rows[i]["EXPRT_DATE"].ToString();
                //    //    inputSalesGrid.Rows[i].Cells["SA_FROZEN_GUBUN"].Value = dt.Rows[i]["FROZEN_GUBUN"].ToString();
                //    //    inputSalesGrid.Rows[i].Cells["SA_UNION_CD"].Value = dt.Rows[i]["A_UNION_CD"].ToString();
                //    //    inputSalesGrid.Rows[i].Cells["SA_TOTAL_AMT"].Value = decimal.Parse(dt.Rows[i]["TOTAL_AMT"].ToString()).ToString("#,0.######");
                //    //    inputSalesGrid.Rows[i].Cells["SA_UNIT_NM"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                //    //}
                //}
                //else
                //{
                //    MessageBox.Show("입력된 LOT번호가 존재하지 않습니다. 재확인 부탁드립니다.");
                //    ClearGrids();
                //    return;
                //}


            }
            catch (Exception ex)
            {
                MessageBox.Show("검색중 오류가 발생했습니다");
                Console.WriteLine(ex);
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void inputTraceGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (!inputTraceGrid.Rows[e.RowIndex].Cells["INPUT_DATE"].Value.ToString().Equals("===합계==="))
                {
                    txt_amt.Text = "";
                    txt_date.Text = "";
                    txt_cd.Text = "";
                    txt_seq.Text = "";
                    txt_label_nm.Text = "";

                    
                    txt_amt.Text = inputTraceGrid.Rows[e.RowIndex].Cells["TOTAL_AMT"].Value.ToString();
                    txt_seq.Text = inputTraceGrid.Rows[e.RowIndex].Cells["INPUT_SEQ"].Value.ToString();
                    txt_date.Text = inputTraceGrid.Rows[e.RowIndex].Cells["INPUT_DATE"].Value.ToString();
                    txt_cd.Text = inputTraceGrid.Rows[e.RowIndex].Cells["INPUT_CD"].Value.ToString();
                    txt_label_nm.Text = inputTraceGrid.Rows[e.RowIndex].Cells["LABEL_NM"].Value.ToString();
                    txt_supply.Text = inputTraceGrid.Rows[e.RowIndex].Cells["TOTAL_SUPPLY_MONEY"].Value.ToString();
                    txt_tax.Text = inputTraceGrid.Rows[e.RowIndex].Cells["TOTAL_TAX_MONEY"].Value.ToString();
                    txt_money.Text = inputTraceGrid.Rows[e.RowIndex].Cells["TOTAL_MONEY"].Value.ToString();
                    txt_cust_nm.Text = inputTraceGrid.Rows[e.RowIndex].Cells["CUST_NM"].Value.ToString();

                    lbl_amt.Text = inputTraceGrid.Rows[e.RowIndex].Cells["GUBUN"].Value.ToString() + "수량";
                    lbl_date.Text = inputTraceGrid.Rows[e.RowIndex].Cells["GUBUN"].Value.ToString() + "일자";
                    
                }
            }
        }






        //private void inputRmGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{

        //    conDataGridView grd = (conDataGridView)sender;

        //    grd.EndEdit();
        //    //DataGridViewRow Row = grd.SelectedCells[17].OwningRow;
        //    int col = grd.Columns.Count - 1;
        //    if (grd.SelectedCells[col].ColumnIndex == col && grd.SelectedCells[col].Value as bool? == true)
        //    {
        //        grd.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.Red;
        //    }
        //    else
        //    {
        //        grd.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;

        //    }
        //}
    }
}
