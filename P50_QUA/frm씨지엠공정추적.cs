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
    public partial class frm씨지엠공정추적 : Form
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


        public frm씨지엠공정추적()
        {
            InitializeComponent();

        }

        private void frm씨지엠공정추적_Load(object sender, EventArgs e)
        {
            ComInfo.gridHeaderSet(inputTraceGrid);
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
                if (rBtn_lot.Checked)
                {
                    SrchValue = ComInfo.TextBoxMessage("LOT번호 입력", "입력");
                    if (!SrchValue.Equals(""))
                    {
                        ResetSetting();
                        Srch_by_LotNo(SrchValue);
                    }
                    
                }
                else if (rBtn_a_union.Checked)
                {
                    SrchValue = ComInfo.TextBoxMessage("묶음코드(제품)", "입력");
                    if (!SrchValue.Equals(""))
                    {
                        ResetSetting();
                        Srch_by_a_union(SrchValue);
                    }
                }
                else if (rBtn_union.Checked)
                {
                    SrchValue = ComInfo.TextBoxMessage("개체번호(원료육)", "입력");
                    if (!SrchValue.Equals(""))
                    {
                        ResetSetting();
                        Srch_by_union(SrchValue);
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

        public void Srch_by_a_union(string SrchValue)
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = new DataTable();
                dt = wDm.fn_flow_trace_list_By_A_Union_cd(SrchValue);
                if (dt == null || dt.Rows.Count < 1)
                {
                    MessageBox.Show("검색된 묶음번호가 없습니다. 재확인부탁드립니다.");
                    return;
                }
                else if (dt.Rows.Count > 1)
                {
                    Popup.pop_공정추적LOT정하기 reLot = new Popup.pop_공정추적LOT정하기();
                    reLot.dt = wDm.fn_flow_trace_list_By_A_Union_cd_Detail(SrchValue);
                    reLot.ShowDialog();
                    if (reLot.sLotNo != null && !reLot.sLotNo.Equals(""))
                    {
                        s_GUBUN = "생산";
                        s_INPUT_DATE = reLot.sDate;
                        s_INPUT_CD = reLot.sCd;
                        s_INPUT_SEQ = reLot.sSeq;

                        txt_amt.Text = reLot.sAmt;
                        txt_label_nm.Text = reLot.sLabel;
                        txt_date.Text = reLot.sDate;
                        txt_cd.Text = reLot.sCd;
                        txt_seq.Text = reLot.sSeq;

                        Srch_by_LotNo(reLot.sLotNo);
                    }

                }
                else
                {
                    //1개 검색
                    Srch_by_LotNo(dt.Rows[0]["LOT_NO"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("검색중 오류 발생");
                Console.WriteLine(ex);
            }
            
        }

        public void Srch_by_union(string SrchValue)
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = new DataTable();
                dt = wDm.fn_flow_trace_list_By_Union_cd(SrchValue);
                if (dt == null || dt.Rows.Count < 1)
                {
                    MessageBox.Show("검색된 개체번호가 없거나 생산공정이 실시되지 않았습니다.\n 재확인부탁드립니다.");
                    return;
                }
                else if (dt.Rows.Count > 1)
                {
                    Popup.pop_공정추적LOT정하기 reLot = new Popup.pop_공정추적LOT정하기();
                    reLot.dt = wDm.fn_flow_trace_list_By_Union_cd_Detail(SrchValue);
                    reLot.lblTitle.Text = "원료육 생산 출고 내역";
                    reLot.GridRecord.Columns["INPUT_DATE"].HeaderText = "소비일자";
                    reLot.GridRecord.Columns["A_UNION_CD"].HeaderText = "개체번호";
                    reLot.GridRecord.Columns["INPUT_AMT"].HeaderText = "소비량";
                    reLot.ShowDialog();
                    if (reLot.sLotNo != null && !reLot.sLotNo.Equals(""))
                    {
                        
                        s_GUBUN = "소비";
                        s_INPUT_DATE = reLot.sDate;
                        s_INPUT_CD = reLot.sCd;
                        s_INPUT_SEQ = reLot.sSeq;

                        txt_amt.Text = reLot.sAmt;
                        txt_label_nm.Text = reLot.sLabel;
                        txt_date.Text = reLot.sDate;
                        txt_cd.Text = reLot.sCd;
                        txt_seq.Text = reLot.sSeq;

                        Srch_by_LotNo(reLot.sLotNo);
                    }

                }
                else
                {
                    //1개 검색
                    Srch_by_LotNo(dt.Rows[0]["LOT_NO"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("검색중 오류 발생");
                Console.WriteLine(ex);
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

            lbl_amt.Text = "수량";
            lbl_date.Text = "일자";
        }

        private void ClearGrids()
        {
            txt_input_date.Text = "";
            txt_input_cd.Text = "";
            txt_lot_number.Text = "";
            txt_work_date.Text = "";
            txt_work_cd.Text = "";
            end_req_date.Text = "";
            txt_inst_notice.Text = "";
            txt_complete_yn.Text = "";

            inputTraceGrid.Rows.Clear();

        }

        public void Srch_by_LotNo(string LotNo)
        {
            try
            {
                // 생산 & 지시정보 + 지시 원료육
                wnDm wDm = new wnDm();
                DataTable dt = wDm.fn_flow_trace_list_info_By_Lot(LotNo);
                if (dt != null && dt.Rows.Count == 1)
                {
                    inputTraceGrid.Rows.Clear();
                   
                    txt_input_date.Text = dt.Rows[0]["INPUT_DATE"].ToString();
                    txt_input_cd.Text = dt.Rows[0]["INPUT_CD"].ToString();
                    txt_lot_number.Text = dt.Rows[0]["LOT_NO"].ToString();
                    txt_work_date.Text = dt.Rows[0]["W_INST_DATE"].ToString();
                    txt_work_cd.Text = dt.Rows[0]["W_INST_CD"].ToString();
                    end_req_date.Text = dt.Rows[0]["DELIVERY_DATE"].ToString();
                    txt_inst_notice.Text = dt.Rows[0]["INST_NOTICE"].ToString();
                    if (dt.Rows[0]["COMPLETE_YN"].ToString().Equals("3"))
                    {
                        txt_complete_yn.Text = "완료";
                    }
                    else if (dt.Rows[0]["INPUT_DATE"].ToString().Equals("2"))
                    {
                        txt_complete_yn.Text = "진행중";
                    }
                    else
                    {
                        txt_complete_yn.Text = "대기";
                    }

                    //TRACE GRID
                    dt = wDm.fn_flow_trace_list_By_LotNo(LotNo);

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
                            inputTraceGrid.Rows[i].Cells["CHUGJONG_NM"].Value = dt.Rows[i]["CHUGJONG_NM"].ToString();
                            inputTraceGrid.Rows[i].Cells["COUNTRY_NM"].Value = dt.Rows[i]["COUNTRY_NM"].ToString();
                            inputTraceGrid.Rows[i].Cells["CLASS_NM"].Value = dt.Rows[i]["CLASS_NM"].ToString();
                            inputTraceGrid.Rows[i].Cells["GRADE_NM"].Value = dt.Rows[i]["GRADE_NM"].ToString();
                            inputTraceGrid.Rows[i].Cells["EXPRT_DATE"].Value = dt.Rows[i]["EXPRT_DATE"].ToString();
                            inputTraceGrid.Rows[i].Cells["FROZEN_GUBUN"].Value = dt.Rows[i]["FROZEN_GUBUN"].ToString();
                            inputTraceGrid.Rows[i].Cells["UNION_CD"].Value = dt.Rows[i]["UNION_CD"].ToString();
                            inputTraceGrid.Rows[i].Cells["A_UNION_CD"].Value = dt.Rows[i]["A_UNION_CD"].ToString();
                            inputTraceGrid.Rows[i].Cells["UNIT_NM"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                            if (dt.Rows[i]["TOTAL_AMT"].ToString().Contains("/"))
                            {
                                string[] sTemp = dt.Rows[i]["TOTAL_AMT"].ToString().Split('/');
                                string sTemp2 = decimal.Parse(sTemp[0]).ToString("#,0.######");
                                sTemp2 += "/" + decimal.Parse(sTemp[1]).ToString("#,0.######");
                                inputTraceGrid.Rows[i].Cells["TOTAL_AMT"].Value = sTemp2;

                            }
                            else
                            {
                                inputTraceGrid.Rows[i].Cells["TOTAL_AMT"].Value = decimal.Parse(dt.Rows[i]["TOTAL_AMT"].ToString()).ToString("#,0.######");
                            }
                            inputTraceGrid.Rows[i].Cells["LOSS_AMT"].Value = decimal.Parse(dt.Rows[i]["LOSS_AMT"].ToString()).ToString("#,0.######");
                            if (inputTraceGrid.Rows[i].Cells["LOSS_AMT"].Value.Equals("0"))
                            {
                                inputTraceGrid.Rows[i].Cells["LOSS_AMT"].Value = "";
                            }

                            if (dt.Rows[i]["INPUT_DATE"].ToString().Equals("===합계==="))
                            {
                                DataGridViewCellStyle style = new DataGridViewCellStyle();
                                style.BackColor = Color.Khaki;
                                style.SelectionBackColor = Color.DarkKhaki;
                                for (int j = 0; j < inputTraceGrid.ColumnCount; j++)
                                {
                                    inputTraceGrid.Rows[i].Cells[j].Style = style;
                                }
                            }

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
                        MessageBox.Show("조회중 오류가 발생하였습니다");
                        return;
                    }

                   
                }
                else
                {
                    if (dt == null || dt.Rows.Count <= 0)
                    {
                        MessageBox.Show("입력된 LOT번호가 존재하지 않습니다. 재확인 부탁드립니다.");
                        ClearGrids();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("하나 이상의 LOT번호가 검색됩니다. 개발자에게 문의하세요 01044314551 ");
                        ClearGrids();
                        return;
                    }
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

                    if (inputTraceGrid.Rows[e.RowIndex].Cells["GUBUN"].Value.ToString().Equals("지시"))
                    {
                        txt_amt.Text = inputTraceGrid.Rows[e.RowIndex].Cells["TOTAL_AMT"].Value.ToString();
                        txt_date.Text = inputTraceGrid.Rows[e.RowIndex].Cells["INPUT_DATE"].Value.ToString();
                        txt_cd.Text = inputTraceGrid.Rows[e.RowIndex].Cells["INPUT_CD"].Value.ToString();
                        txt_label_nm.Text = inputTraceGrid.Rows[e.RowIndex].Cells["LABEL_NM"].Value.ToString();

                        lbl_amt.Text = inputTraceGrid.Rows[e.RowIndex].Cells["GUBUN"].Value.ToString() + "수량";
                        lbl_date.Text = inputTraceGrid.Rows[e.RowIndex].Cells["GUBUN"].Value.ToString() + "일자";
                    }
                    else
                    {
                        txt_amt.Text = inputTraceGrid.Rows[e.RowIndex].Cells["TOTAL_AMT"].Value.ToString();
                        txt_seq.Text = inputTraceGrid.Rows[e.RowIndex].Cells["INPUT_SEQ"].Value.ToString();
                        txt_date.Text = inputTraceGrid.Rows[e.RowIndex].Cells["INPUT_DATE"].Value.ToString();
                        txt_cd.Text = inputTraceGrid.Rows[e.RowIndex].Cells["INPUT_CD"].Value.ToString();
                        txt_label_nm.Text = inputTraceGrid.Rows[e.RowIndex].Cells["LABEL_NM"].Value.ToString();

                        lbl_amt.Text = inputTraceGrid.Rows[e.RowIndex].Cells["GUBUN"].Value.ToString() + "수량";
                        lbl_date.Text = inputTraceGrid.Rows[e.RowIndex].Cells["GUBUN"].Value.ToString() + "일자";
                    }
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
