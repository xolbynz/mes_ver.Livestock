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
    public partial class frm씨지엠작업일보 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private DataGridView del_inputGrid = new DataGridView();
        private DateTimePicker dtp = new DateTimePicker();
        private Rectangle _Retangle;

        private string old_cust_nm = "";
        private bool bHeadCheck = false;
        private ComInfo comInfo = new ComInfo();
        private DataGridView copied_dgv = new DataGridView();

        private bool isUserInput = true;

        private bool first_touch = false;

        private string currunt_column_temp = "";


        public frm씨지엠작업일보()
        {
            InitializeComponent();

            this.inputRmGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grid_KeyDown);
            this.inputRmGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grid_ColumnHeaderMouseClick);

            this.inputRmGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.grid_RowsAdded);
            this.inputRmGrid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.grid_RowsRemoved);
            this.inputRmGrid.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grid_EditingControlShowing);

            this.inputRmGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellClick);
            this.ItemGrid.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.grid_ColumnWidthChanged);
            this.ItemGrid.Scroll += new System.Windows.Forms.ScrollEventHandler(this.grid_scroll);

            this.ItemGrid.Controls.Add(dtp);
            dtp.Visible = false;
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.TextChanged += new EventHandler(dtp_TextChange);
        }

        private void frm씨지엠작업일보_Load(object sender, EventArgs e)
        {

            ComInfo.gridHeaderSet(inputRmGrid);
            ComInfo.gridHeaderSet(inputRmSoyoGrid);
            ComInfo.gridHeaderSet(ItemGrid);
            ComInfo.gridHeaderSet(tdInputGrid);
            ComInfo.gridHeaderSet(inputGrid);


            input_list(tdInputGrid, "where convert(varchar(10), Z.INTIME, 120) = convert(varchar(10), getDate(), 120) ");
            input_list(inputGrid, "where Z.INPUT_DATE >= '" + start_date.Text.ToString() + "' and  Z.INPUT_DATE <= '" + end_date.Text.ToString() + "'");

            n_in_ord_grid.Columns["CHK"].ReadOnly = false;

            //for (int i = 0; i < n_in_ord_grid.ColumnCount; i++) {
            //    n_in_ord_grid.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //}

            tab_input.TabPages.Remove(tabPage2);

            del_inputGrid.AllowUserToAddRows = false;

            del_inputGrid.Columns.Add("INPUT_DATE", "INPUT_DATE");
            del_inputGrid.Columns.Add("INPUT_CD", "INPUT_CD");
            del_inputGrid.Columns.Add("SEQ", "SEQ");
            del_inputGrid.Columns.Add("ORDER_DATE", "ORDER_SEQ");
            del_inputGrid.Columns.Add("ORDER_CD", "ORDER_CD");
            del_inputGrid.Columns.Add("ORDER_SEQ", "ORDER_SEQ");
            del_inputGrid.Columns.Add("RAW_MAT_CD", "RAW_MAT_CD");
            del_inputGrid.Columns.Add("OLD_TOTAL_AMT", "OLD_TOTAL_AMT");

            ComInfo comInfo = new ComInfo();
            string sqlQuery = "";

            Init_Combobox();
            inputRmGrid.Rows.Add();
            inputRmGrid.Rows[inputRmGrid.Rows.Count - 1].Cells["TOTAL_AMT"].Value = "0";
            inputRmGrid.Rows[inputRmGrid.Rows.Count - 1].Cells["PRICE"].Value = "0";
            inputRmGrid.Rows[inputRmGrid.Rows.Count - 1].Cells["TOTAL_MONEY"].Value = "0";



            for (int j = 0; j < inputRmSoyoGrid.Columns.Count; j++)
            {
                copied_dgv.Columns.Add(inputRmSoyoGrid.Columns[j].Name, inputRmSoyoGrid.Columns[j].Name);
            }


        }


        public void Init_Combobox()
        {
            ComInfo comInfo = new ComInfo();
            string sqlQuery = "";


            cmb_frozen_gubun.ValueMember = "코드";
            cmb_frozen_gubun.DisplayMember = "명칭";
            sqlQuery = comInfo.queryFrozenGubun();
            wConst.ComboBox_Read_NoBlank(cmb_frozen_gubun, sqlQuery);

            cmb_exprt_auto.ValueMember = "코드";
            cmb_exprt_auto.DisplayMember = "명칭";
            sqlQuery = comInfo.queryAuto();
            wConst.ComboBox_Read_NoBlank(cmb_exprt_auto, sqlQuery);

            cmb_exprt_count.ValueMember = "코드";
            cmb_exprt_count.DisplayMember = "명칭";
            sqlQuery = comInfo.queryExprt2();
            wConst.ComboBox_Read_NoBlank(cmb_exprt_count, sqlQuery);

            DataGridViewComboBoxColumn cmbTemp = (DataGridViewComboBoxColumn)inputRmGrid.Columns["GRADE_NM"];

            cmbTemp.ValueMember = "코드";
            cmbTemp.DisplayMember = "명칭";
            sqlQuery = comInfo.queryGradeAll();
            wConst.GridComboBox_Read_Blank(cmbTemp, sqlQuery);

            cmbTemp = (DataGridViewComboBoxColumn)ItemGrid.Columns["IT_FROZEN_GUBUN"];


            cmbTemp.ValueMember = "코드";
            cmbTemp.DisplayMember = "명칭";
            sqlQuery = comInfo.queryFrozenGubun_With_HF();
            wConst.GridComboBox_Read_Blank(cmbTemp, sqlQuery);



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

        private void btnSrch_Click(object sender, EventArgs e)
        {
            input_list(inputGrid, "where Z.INPUT_DATE >= '" + start_date.Text.ToString() + "' and  Z.INPUT_DATE <= '" + end_date.Text.ToString() + "'");
        }

        private void btn_move_Click(object sender, EventArgs e)
        {
            if (n_in_ord_grid.Rows.Count > 0)
            {
                int chk = 0;
                int seq = 1;
                for (int i = 0; i < n_in_ord_grid.Rows.Count; i++)
                {
                    if ((bool)n_in_ord_grid.Rows[i].Cells["CHK"].Value == true)
                    {
                        for (int j = 0; j < inputRmGrid.Rows.Count; j++)
                        {
                            if ((string)n_in_ord_grid.Rows[i].Cells["NI_ORDER_DATE"].Value == (string)inputRmGrid.Rows[j].Cells["ORDER_DATE"].Value
                                && (string)n_in_ord_grid.Rows[i].Cells["NI_ORDER_CD"].Value == (string)inputRmGrid.Rows[j].Cells["ORDER_CD"].Value
                                && (string)n_in_ord_grid.Rows[i].Cells["NI_SEQ"].Value == (string)inputRmGrid.Rows[j].Cells["ORDER_SEQ"].Value)
                            {
                                MessageBox.Show("해당 발주의 원부재료가 이미 등록되어있습니다.");
                                return;
                            }
                        }

                        for (int k = 0; k < del_inputGrid.Rows.Count; k++)
                        {
                            if ((string)n_in_ord_grid.Rows[i].Cells["NI_ORDER_DATE"].Value == (string)del_inputGrid.Rows[k].Cells["ORDER_DATE"].Value
                                && (string)n_in_ord_grid.Rows[i].Cells["NI_ORDER_CD"].Value == (string)del_inputGrid.Rows[k].Cells["ORDER_CD"].Value
                                && (string)n_in_ord_grid.Rows[i].Cells["NI_SEQ"].Value == (string)del_inputGrid.Rows[k].Cells["ORDER_SEQ"].Value)
                            {
                                MessageBox.Show("해당 발주의 삭제데이터가 있어서 등록이 불가합니다.");
                                return;
                            }
                        }
                        double totalAmtTemp = double.Parse(n_in_ord_grid.Rows[i].Cells["NO_INPUT_AMT"].Value.ToString());
                        double boxAmtTemp = double.Parse(n_in_ord_grid.Rows[i].Cells["NI_BOX_AMT"].Value.ToString());
                        int totalBoxAmtTmp = (int)(totalAmtTemp / boxAmtTemp);
                        if (totalBoxAmtTmp * boxAmtTemp < totalAmtTemp)
                        {
                            totalBoxAmtTmp++;
                        }
                        for (int k = 0; k < totalBoxAmtTmp; k++)
                        {

                            //inputRmGrid.Rows.Add();
                            int rowNum = inputRmGrid.Rows.Count - 1;
                            inputRmGrid.Rows[rowNum].Cells["ORDER_DATE"].Value = n_in_ord_grid.Rows[i].Cells["NI_ORDER_DATE"].Value;
                            inputRmGrid.Rows[rowNum].Cells["ORDER_CD"].Value = n_in_ord_grid.Rows[i].Cells["NI_ORDER_CD"].Value;
                            inputRmGrid.Rows[rowNum].Cells["ORDER_SEQ"].Value = n_in_ord_grid.Rows[i].Cells["NI_SEQ"].Value;
                            inputRmGrid.Rows[rowNum].Cells["RAW_MAT_NM"].Value = n_in_ord_grid.Rows[i].Cells["NI_RAW_MAT_NM"].Value;
                            inputRmGrid.Rows[rowNum].Cells["RAW_MAT_CD"].Value = n_in_ord_grid.Rows[i].Cells["NI_RAW_MAT_CD"].Value;
                            inputRmGrid.Rows[rowNum].Cells["SPEC"].Value = n_in_ord_grid.Rows[i].Cells["NI_SPEC"].Value;
                            inputRmGrid.Rows[rowNum].Cells["UNIT_CD"].Value = n_in_ord_grid.Rows[i].Cells["NI_UNIT_CD"].Value;
                            inputRmGrid.Rows[rowNum].Cells["UNIT_NM"].Value = n_in_ord_grid.Rows[i].Cells["NI_UNIT_NM"].Value;
                            inputRmGrid.Rows[rowNum].Cells["RAW_MAT_GUBUN"].Value = n_in_ord_grid.Rows[i].Cells["NI_RAW_MAT_GUBUN"].Value;
                            inputRmGrid.Rows[rowNum].Cells["RAW_MAT_GUBUN_NM"].Value = n_in_ord_grid.Rows[i].Cells["NI_RAW_MAT_GUBUN_NM"].Value;

                            if (totalAmtTemp / boxAmtTemp >= 1)
                            {
                                inputRmGrid.Rows[rowNum].Cells["TOTAL_AMT"].Value = boxAmtTemp.ToString();
                                totalAmtTemp -= boxAmtTemp;
                            }
                            else
                            {
                                inputRmGrid.Rows[rowNum].Cells["TOTAL_AMT"].Value = totalAmtTemp.ToString();
                            }
                            inputRmGrid.Rows[rowNum].Cells["OLD_TOTAL_AMT"].Value = n_in_ord_grid.Rows[i].Cells["NO_INPUT_AMT"].Value;
                            inputRmGrid.Rows[rowNum].Cells["PRICE"].Value = n_in_ord_grid.Rows[i].Cells["NI_PRICE"].Value;
                            inputRmGrid.Rows[rowNum].Cells["TOTAL_MONEY"].Value = n_in_ord_grid.Rows[i].Cells["NI_TOTAL_MONEY"].Value;
                            inputRmGrid.Rows[rowNum].Cells["CLASS_NM"].Value = n_in_ord_grid.Rows[i].Cells["NI_CLASS_NM"].Value;
                            inputRmGrid.Rows[rowNum].Cells["CLASS_CD"].Value = n_in_ord_grid.Rows[i].Cells["NI_CLASS_CD"].Value;
                            inputRmGrid.Rows[rowNum].Cells["COUNTRY"].Value = n_in_ord_grid.Rows[i].Cells["NI_COUNTRY_NM"].Value;
                            inputRmGrid.Rows[rowNum].Cells["COUNTRY_CD"].Value = n_in_ord_grid.Rows[i].Cells["NI_COUNTRY_CD"].Value;
                            inputRmGrid.Rows[rowNum].Cells["GRADE_NM"].Value = n_in_ord_grid.Rows[i].Cells["NI_GRADE_NM"].Value;
                            inputRmGrid.Rows[rowNum].Cells["GRADE_CD"].Value = n_in_ord_grid.Rows[i].Cells["NI_GRADE_CD"].Value;
                            inputRmGrid.Rows[rowNum].Cells["TOTAL_BOX_AMT"].Value = n_in_ord_grid.Rows[i].Cells["NI_TOTAL_BOX_AMT"].Value;
                            inputRmGrid.Rows[rowNum].Cells["CHUGJONG_CD"].Value = n_in_ord_grid.Rows[i].Cells["NI_CHUGJONG_CD"].Value;
                            inputRmGrid.Rows[rowNum].Cells["CHUGJONG_NM"].Value = n_in_ord_grid.Rows[i].Cells["NI_CHUGJONG_NM"].Value;
                            inputRmGrid.Rows[rowNum].Cells["EXPRT_COUNT"].Value = n_in_ord_grid.Rows[i].Cells["NI_EXPRT_COUNT"].Value;
                            inputRmGrid.Rows[rowNum].Cells["LABEL_NM"].Value = n_in_ord_grid.Rows[i].Cells["NI_LABEL_NM"].Value;



                            chk = 1;
                            wConst.f_Calc_Price(inputRmGrid, rowNum, "TOTAL_AMT", "PRICE");
                        }

                    }
                }
                if (chk == 0)
                {
                    MessageBox.Show("발주서의 원부재료를 선택하십시기 바랍니다.");
                }
            }
            else
            {
                MessageBox.Show("발주서 데이터가 없습니다.");
            }
        }









        private void btn_minus_Click(object sender, EventArgs e)
        {
            minus_logic(ItemGrid);
        }

        #endregion button logic

        #region input logic
        private void resetSetting()
        {
            lbl_input_gbn.Text = "";
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btn_work_inst_srch.Enabled = true;

            txt_input_date.Enabled = true;


            txt_work_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txt_input_date.Text = DateTime.Now.ToString("yyyy-MM-dd");

            txt_work_cd.Text = "";
            txt_input_cd.Text = "";
            txt_lot_number.Text = "";
            end_req_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txt_complete_yn.Text = "";
            txt_inst_notice.Text = "";




            end_req_date.Text = "";
            txt_work_cd.Text = "";
            txt_inst_notice.Text = "";

            inputRmGrid.Rows.Clear();
            inputRmGrid.Rows.Add();
            ItemGrid.Rows.Clear();
            inputRmSoyoGrid.Rows.Clear();
            dtp.Visible = false;
            inputRmGrid.ReadOnly = false;
            ItemGrid.ReadOnly = false;
            inputRmSoyoGrid.ReadOnly = false;

            btn_plus.Visible = false;
            btn_minus.Visible = false;
            lbl_plus.Visible = false;
            lbl_minus.Visible = false;


            first_touch = false;





            //del_orderGrid.Rows.Clear();
        }

        private void inputLogic()
        {

            int cnt = 0;
            int grid_cnt = inputRmGrid.Rows.Count;


            if (inputRmGrid.Rows.Count == 0)
            {
                MessageBox.Show("먼저 작업지시를 검색하여주십시오");
                return;
            }


            for (int i = 0; i < ItemGrid.Rows.Count; i++)
            {
                if (ItemGrid.Rows[i].Cells["IT_TOTAL_AMT"].Value == null || ItemGrid.Rows[i].Cells["IT_TOTAL_AMT"].Value.ToString().Equals("") ||
                    ItemGrid.Rows[i].Cells["IT_TOTAL_AMT"].Value.ToString().Equals("0"))
                {
                    ItemGrid.Rows.RemoveAt(i);
                    i = -1;
                }
            }

            if (ItemGrid.Rows.Count < 1)
            {
                MessageBox.Show("생산 제품을 최소 하나이상 입력해주십시오");
                return;
            }

            //string input_yn = comInfo.resultYn(chk_input_yn);

            

            if (lbl_input_gbn.Text != "1")
            {



                wnDm wDm = new wnDm();
                int rsNum = wDm.insert_Item_Input(txt_work_date.Text
                    , txt_work_cd.Text
                    , txt_input_date.Text
                    , txt_lot_number.Text
                    , ItemGrid
                    , inputRmSoyoGrid
                    );

                if (rsNum == 0)
                {

                    resetSetting();
                    input_list(tdInputGrid, "where convert(varchar(10), Z.INTIME, 120) = convert(varchar(10), getDate(), 120) ");
                    input_list(inputGrid, "where Z.INPUT_DATE >= '" + start_date.Text.ToString() + "' and  Z.INPUT_DATE <= '" + end_date.Text.ToString() + "'");
                    MessageBox.Show("성공적으로 등록하였습니다.");
                }
                else if (rsNum == 1)
                {
                    MessageBox.Show("저장에 실패하였습니다");
                }
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

                if (first_touch)
                {
                    for (int i = 0; i < ItemGrid.Rows.Count; i++)
                    {
                        Cal_And_Increase_SoyoAmt(i);
                    }
                }

                wnDm wDm = new wnDm();
                DataTable dt = wDm.isItemOut(txt_input_date.Text.ToString(), txt_input_cd.Text.ToString());
                if (dt.Rows.Count > 0)
                {

                    MessageBox.Show("이미 출고된 제품이 있기 때문에 수정할 수 없습니다.");
                    return;

                }

                DialogResult msgOk = comInfo.warningMessage("기존 항목 삭제 후 재등록 합니다. 정말로 수정하시겠습니까?");
                if (msgOk == DialogResult.No)
                {
                    return;
                }
                else
                {
                    int rsNum = wDm.UpdateItemInput(
                txt_input_date.Text.ToString()
                , txt_input_cd.Text.ToString()
                , txt_work_date.Text.ToString()
                , txt_work_cd.Text.ToString()
                , txt_lot_number.Text.ToString()
                , ItemGrid
                , inputRmSoyoGrid
                );
                    if (rsNum == 0)
                    {
                        MessageBox.Show("성공적으로 수정되었습니다.");
                        resetSetting();
                        input_list(tdInputGrid, "where convert(varchar(10), Z.INTIME, 120) = convert(varchar(10), getDate(), 120) ");
                        input_list(inputGrid, "where Z.INPUT_DATE >= '" + start_date.Text.ToString() + "' and  Z.INPUT_DATE <= '" + end_date.Text.ToString() + "'");
                    }
                    else if (rsNum == 1)
                    {
                        MessageBox.Show("수정에 실패하였습니다.");
                    }


                }
            }
        }

        private void input_list(DataGridView dgv, string condition)
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Item_Input_List_CZM(condition);

                if (dt != null && dt.Rows.Count > 0)
                {
                    dgv.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dgv.Rows[i].Cells[0].Value = dt.Rows[i]["INPUT_DATE"].ToString();
                        dgv.Rows[i].Cells[1].Value = dt.Rows[i]["INPUT_CD"].ToString();

                        dgv.Rows[i].Cells[2].Value = dt.Rows[i]["LOT_NO"].ToString();
                        dgv.Rows[i].Cells[3].Value = dt.Rows[i]["W_INST_DATE"].ToString();
                        dgv.Rows[i].Cells[4].Value = dt.Rows[i]["W_INST_CD"].ToString();
                        dgv.Rows[i].Cells[5].Value = dt.Rows[i]["DELIVERY_DATE"].ToString();
                        dgv.Rows[i].Cells[6].Value = dt.Rows[i]["COMPLETE_YN"].ToString();
                        dgv.Rows[i].Cells[7].Value = dt.Rows[i]["INST_NOTICE"].ToString();
                        dgv.Rows[i].Cells[8].Value = dt.Rows[i]["RAW_MAT_CD"].ToString();
                        dgv.Rows[i].Cells[9].Value = dt.Rows[i]["INST_AMT"].ToString();
                    }
                }
                else
                {
                    dgv.Rows.Clear();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("시스템 오류" + e.ToString());
            }
        }

        private void inputGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ComInfo.grdHeaderNoAction(e))
            {
                input_detail(inputGrid, e);
            }
        }

        private void input_detail(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            btnDelete.Enabled = true;
            btnSave.Enabled = false ;
            btn_work_inst_srch.Enabled = false;

            btn_minus.Visible = true;
            btn_plus.Visible = true;
            lbl_minus.Visible = true;
            lbl_plus.Visible = true;



            lbl_input_gbn.Text = "1";
            first_touch = true;
            txt_input_date.Enabled = false;


            txt_input_date.Text = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();
            txt_input_cd.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
            txt_lot_number.Text = dgv.Rows[e.RowIndex].Cells[2].Value.ToString();
            //체크 = dgv.Rows[e.RowIndex].Cells[2].Value.ToString();\
            txt_work_date.Text = dgv.Rows[e.RowIndex].Cells[3].Value.ToString();
            txt_work_cd.Text = dgv.Rows[e.RowIndex].Cells[4].Value.ToString();
            end_req_date.Text = dgv.Rows[e.RowIndex].Cells[5].Value.ToString();
            txt_inst_notice.Text = dgv.Rows[e.RowIndex].Cells[7].Value.ToString();
            if (dgv.Rows[e.RowIndex].Cells[6].Value.ToString().Equals("1"))
            {
                txt_complete_yn.Text = "대기";
            }
            else if (dgv.Rows[e.RowIndex].Cells[2].Value.ToString().Equals("2"))
            {
                txt_complete_yn.Text = "진행중";
            }
            else
            {
                txt_complete_yn.Text = "완료";
            }
            lbl_raw_mat_cd.Text = dgv.Rows[e.RowIndex].Cells[8].Value.ToString();
            lbl_total_amt.Text = dgv.Rows[e.RowIndex].Cells[9].Value.ToString();

            inputDetail();
            inputDetail2();
            inputDetail3();

            isUserInput = true;

        }

        private void inputDetail()
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Raw_List("where RAW_MAT_CD = '" + lbl_raw_mat_cd.Text + "'  ");

                if (dt != null && dt.Rows.Count > 0)
                {
                    inputRmGrid.Rows[0].Cells[0].Value = "1";
                    inputRmGrid.Rows[0].Cells["RAW_MAT_CD"].Value = dt.Rows[0]["RAW_MAT_CD"].ToString();
                    inputRmGrid.Rows[0].Cells["RAW_MAT_NM"].Value = dt.Rows[0]["RAW_MAT_NM"].ToString();
                    inputRmGrid.Rows[0].Cells["TOTAL_AMT"].Value = (decimal.Parse(lbl_total_amt.Text)).ToString("#,0.######");
                    inputRmGrid.Rows[0].Cells["UNIT_NM"].Value = dt.Rows[0]["INPUT_UNIT_NM"].ToString();
                    inputRmGrid.Rows[0].Cells["CHUGJONG_NM"].Value = dt.Rows[0]["CHUGJONG_NM"].ToString();
                    inputRmGrid.Rows[0].Cells["COUNTRY"].Value = dt.Rows[0]["COUNTRY_NM"].ToString();
                    inputRmGrid.Rows[0].Cells["CLASS_NM"].Value = dt.Rows[0]["CLASS_NM"].ToString();
                    inputRmGrid.Rows[0].Cells["TYPE_NM"].Value = dt.Rows[0]["TYPE_NM"].ToString();
                    inputRmGrid.Rows[0].Cells["LABEL_NM"].Value = dt.Rows[0]["LABEL_NM"].ToString();
                    txt_raw_mat_cd.Text = dt.Rows[0]["RAW_MAT_CD"].ToString();

                }
            }
            catch (Exception e)
            {
                MessageBox.Show("시스템 오류" + e.ToString());
            }

        }


        private void inputDetail2()
        {
            wnDm wDm = new wnDm();
            DataTable dt = null;

            dt = wDm.fn_Raw_Output_List_CZM("WHERE A.LOT_NO = '" + txt_lot_number.Text + "'  ");

            dtp.Visible = false;
            inputRmSoyoGrid.Rows.Clear();

            if (dt != null && dt.Rows.Count > 0)
            {
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    inputRmSoyoGrid.Rows.Add();
                    //string t_amt = string.Format("{0:#.##}", 100.2);
                    inputRmSoyoGrid.Rows[i].Cells["s_NO"].Value = dt.Rows[i]["SEQ"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
                    //inputRmGrid.Rows[i].Cells["ORDER_DATE"].Value = dt.Rows[i]["ORDER_DATE"].ToString();
                    //inputRmGrid.Rows[i].Cells["ORDER_CD"].Value = dt.Rows[i]["ORDER_CD"].ToString();
                    //inputRmGrid.Rows[i].Cells["ORDER_SEQ"].Value = dt.Rows[i]["ORDER_SEQ"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_INPUT_DATE"].Value = dt.Rows[i]["INPUT_DATE"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_INPUT_CD"].Value = dt.Rows[i]["INPUT_CD"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_INPUT_SEQ"].Value = dt.Rows[i]["INPUT_SEQ"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_RAW_MAT_NM"].Value = dt.Rows[i]["RAW_MAT_NM"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_RAW_MAT_CD"].Value = dt.Rows[i]["RAW_MAT_CD"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_RAW_MAT_GUBUN"].Value = dt.Rows[i]["RAW_MAT_GUBUN"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_UNIT_CD"].Value = dt.Rows[i]["OUTPUT_UNIT"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_UNIT_NM"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_SOYO_AMT"].Value = (decimal.Parse(dt.Rows[i]["TOTAL_AMT"].ToString())).ToString("#,0.######");
                    inputRmSoyoGrid.Rows[i].Cells["s_EXPRT_DATE"].Value = dt.Rows[i]["EXPRT_DATE"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_UNION_CD"].Value = dt.Rows[i]["UNION_CD"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_CHUGJONG_CD"].Value = dt.Rows[i]["CHUGJONG_CD"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_CLASS_CD"].Value = dt.Rows[i]["CLASS_CD"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_COUNTRY_CD"].Value = dt.Rows[i]["COUNTRY_CD"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_CHUGJONG_NM"].Value = dt.Rows[i]["CHUGJONG_NM"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_CLASS_NM"].Value = dt.Rows[i]["CLASS_NM"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_COUNTRY_NM"].Value = dt.Rows[i]["COUNTRY_NM"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_LABEL_NM"].Value = dt.Rows[i]["LABEL_NM"].ToString();
                    if (dt.Rows[i]["DIRECTION"].ToString().Equals(""))
                    {
                        inputRmSoyoGrid.Rows[i].Cells["s_OUT_LOC"].Value = "STORE_2F";
                    }
                    else
                    {
                        inputRmSoyoGrid.Rows[i].Cells["s_OUT_LOC"].Value = dt.Rows[i]["DIRECTION"].ToString();
                    }
                    inputRmSoyoGrid.Rows[i].Cells["s_USE_AMT"].Value = (decimal.Parse(dt.Rows[i]["OUTPUT_AMT"].ToString())).ToString("#,0.######");
                    inputRmSoyoGrid.Rows[i].Cells["s_LOSS_AMT"].Value = (decimal.Parse(dt.Rows[i]["LOSS_AMT"].ToString())).ToString("#,0.######");



                }
            }
            else
            {
                MessageBox.Show("dt is null");
            }

            

        }

        private void inputDetail3()
        {
            wnDm wDm = new wnDm();
            DataTable dt = null;

            isUserInput = false;

            dt = wDm.fn_Item_Input_Detail_CZM("where A.INPUT_DATE = '" + txt_input_date.Text.ToString() + "' and A.INPUT_CD = '" + txt_input_cd.Text.ToString() + "' ");

            dtp.Visible = false;
            ItemGrid.Rows.Clear();

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ItemGrid.Rows.Add();
                    //string t_amt = string.Format("{0:#.##}", 100.2);
                    ItemGrid.Rows[i].Cells["NO"].Value = dt.Rows[i]["SEQ"].ToString();
                    ItemGrid.Rows[i].Cells["IT_SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
                    ItemGrid.Rows[i].Cells["IT_ITEM_NM"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                    ItemGrid.Rows[i].Cells["IT_ITEM_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                    ItemGrid.Rows[i].Cells["IT_EXPRT_DATE"].Value = dt.Rows[i]["EXPRT_DATE"].ToString();
                    ItemGrid.Rows[i].Cells["IT_UNION_CD"].Value = dt.Rows[i]["B_UNION_CD"].ToString();
                    ItemGrid.Rows[i].Cells["IT_UNION_CD_AFTER"].Value = dt.Rows[i]["A_UNION_CD"].ToString();
                    ItemGrid.Rows[i].Cells["IT_TOTAL_AMT"].Value = (decimal.Parse(dt.Rows[i]["INPUT_AMT"].ToString())).ToString("#,0.######");
                    ItemGrid.Rows[i].Cells["IT_UNIT_NM"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                    ItemGrid.Rows[i].Cells["IT_LABEL_NM"].Value = dt.Rows[i]["LABEL_NM"].ToString();
                    ItemGrid.Rows[i].Cells["IT_TYPE_NM"].Value = dt.Rows[i]["TYPE_NM"].ToString();
                    ItemGrid.Rows[i].Cells["IT_FROZEN_GUBUN"].Value = dt.Rows[i]["FROZEN_GUBUN"].ToString();

                }
            }
            isUserInput = true;

        }

        private void input_del()
        {

            ComInfo comInfo = new ComInfo();
            Console.WriteLine(txt_input_date.Text.ToString());
            Console.WriteLine(txt_input_cd.Text.ToString());

            DialogResult msgOk = comInfo.deleteConfrim("작업일보", txt_input_date.Text.ToString() + " - " + txt_input_cd.Text.ToString());

            if (msgOk == DialogResult.No)
            {
                return;
            }

            wnDm wDm = new wnDm();
            DataTable dt = wDm.isItemOut(txt_input_date.Text.ToString(), txt_input_cd.Text.ToString());
            if (dt.Rows.Count > 0)
            {

                MessageBox.Show("이미 출고된 제품이 있기 때문에 삭제할 수 없습니다.");
                return;

            }

            int rsNum = wDm.deleteItemInput(
                txt_input_date.Text.ToString()
                , txt_input_cd.Text.ToString()
                , txt_work_date.Text.ToString()
                , txt_work_cd.Text.ToString()
                , txt_lot_number.Text.ToString());
            if (rsNum == 0)
            {
                resetSetting();

                input_list(tdInputGrid, "where convert(varchar(10), Z.INTIME, 120) = convert(varchar(10), getDate(), 120) ");
                input_list(inputGrid, "where Z.INPUT_DATE >= '" + start_date.Text.ToString() + "' and  Z.INPUT_DATE <= '" + end_date.Text.ToString() + "'");

                MessageBox.Show("성공적으로 삭제하였습니다.");

            }
            else if (rsNum == 1)
            {
                MessageBox.Show("삭제에 실패하였습니다.");
            }
        }

        private void ni_detail()
        {
            wnDm wDm = new wnDm();
            DataTable dt = null;

            string condition = "where A.CUST_CD = '" + txt_cust_cd.Text.ToString() + "' and C.NO_INPUT_AMT > 0 ";
            dt = wDm.fn_Input_Order_Detail_List(condition);

            this.n_in_ord_grid.RowCount = dt.Rows.Count;
            if (dt != null && dt.Rows.Count > 0)
            {
                // n_in_ord_grid.RowCount = dt.Rows.Count;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    n_in_ord_grid.Rows[i].Cells["NI_ORDER_DATE"].Value = dt.Rows[i]["ORDER_DATE"].ToString();
                    n_in_ord_grid.Rows[i].Cells["NI_ORDER_CD"].Value = dt.Rows[i]["ORDER_CD"].ToString();
                    n_in_ord_grid.Rows[i].Cells["NI_SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
                    n_in_ord_grid.Rows[i].Cells["NI_RAW_MAT_NM"].Value = dt.Rows[i]["RAW_MAT_NM"].ToString();
                    n_in_ord_grid.Rows[i].Cells["NI_RAW_MAT_CD"].Value = dt.Rows[i]["RAW_MAT_CD"].ToString();
                    n_in_ord_grid.Rows[i].Cells["NI_SPEC"].Value = dt.Rows[i]["SPEC"].ToString();
                    n_in_ord_grid.Rows[i].Cells["NI_RAW_MAT_GUBUN"].Value = dt.Rows[i]["RAW_MAT_GUBUN"].ToString();
                    n_in_ord_grid.Rows[i].Cells["NI_RAW_MAT_GUBUN_NM"].Value = dt.Rows[i]["RAW_MAT_GUBUN_NM"].ToString();
                    n_in_ord_grid.Rows[i].Cells["NI_ORDER_AMT"].Value = (decimal.Parse(dt.Rows[i]["ORDER_AMT"].ToString())).ToString("#,0.######");
                    n_in_ord_grid.Rows[i].Cells["NI_UNIT_CD"].Value = dt.Rows[i]["UNIT_CD"].ToString();
                    n_in_ord_grid.Rows[i].Cells["NI_UNIT_NM"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                    n_in_ord_grid.Rows[i].Cells["INPUT_AMT"].Value = (decimal.Parse(dt.Rows[i]["INPUT_AMT"].ToString())).ToString("#,0.######");
                    n_in_ord_grid.Rows[i].Cells["NO_INPUT_AMT"].Value = (decimal.Parse(dt.Rows[i]["NO_INPUT_AMT"].ToString())).ToString("#,0.######");
                    n_in_ord_grid.Rows[i].Cells["NI_PRICE"].Value = (decimal.Parse(dt.Rows[i]["PRICE"].ToString())).ToString("#,0.######");
                    n_in_ord_grid.Rows[i].Cells["NI_TOTAL_MONEY"].Value = (decimal.Parse(dt.Rows[i]["TOTAL_MONEY"].ToString())).ToString("#,0.######");
                    n_in_ord_grid.Rows[i].Cells["CHK"].Value = false;
                    n_in_ord_grid.Rows[i].Cells["NI_CLASS_NM"].Value = dt.Rows[i]["CLASS_NM"].ToString();
                    n_in_ord_grid.Rows[i].Cells["NI_CLASS_CD"].Value = dt.Rows[i]["CLASS_CD"].ToString();
                    n_in_ord_grid.Rows[i].Cells["NI_COUNTRY_NM"].Value = dt.Rows[i]["COUNTRY_NM"].ToString();
                    n_in_ord_grid.Rows[i].Cells["NI_COUNTRY_CD"].Value = dt.Rows[i]["COUNTRY_CD"].ToString();
                    n_in_ord_grid.Rows[i].Cells["NI_GRADE_NM"].Value = dt.Rows[i]["GRADE_NM"].ToString();
                    n_in_ord_grid.Rows[i].Cells["NI_GRADE_CD"].Value = dt.Rows[i]["GRADE_CD"].ToString();
                    n_in_ord_grid.Rows[i].Cells["NI_CHUGJONG_CD"].Value = dt.Rows[i]["CHUGJONG_CD"].ToString();
                    n_in_ord_grid.Rows[i].Cells["NI_CHUGJONG_NM"].Value = dt.Rows[i]["CHUGJONG_NM"].ToString();
                    n_in_ord_grid.Rows[i].Cells["NI_EXPRT_COUNT"].Value = dt.Rows[i]["EXPRT_COUNT"].ToString();
                    n_in_ord_grid.Rows[i].Cells["NI_TOTAL_BOX_AMT"].Value = dt.Rows[i]["TOTAL_BOX_AMT"].ToString();
                    n_in_ord_grid.Rows[i].Cells["NI_BOX_AMT"].Value = dt.Rows[i]["BOX_AMT"].ToString();
                    n_in_ord_grid.Rows[i].Cells["NI_LABEL_NM"].Value = dt.Rows[i]["LABEL_NM"].ToString();


                }
            }
        }

        private void in_grid_detail()
        {
            wnDm wDm = new wnDm();
            DataTable dt = null;

            string condition = "where A.CUST_CD = '" + txt_cust_cd.Text.ToString() + "' and C.INPUT_AMT > 0 ";
            dt = wDm.fn_Input_Order_Detail_List(condition);

            in_ord_gird.RowCount = dt.Rows.Count;
            if (dt != null && dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    in_ord_gird.Rows[i].Cells["IN_ORDER_DATE"].Value = dt.Rows[i]["ORDER_DATE"].ToString();
                    in_ord_gird.Rows[i].Cells["IN_ORDER_CD"].Value = dt.Rows[i]["ORDER_CD"].ToString();
                    in_ord_gird.Rows[i].Cells["IN_RAW_MAT_NM"].Value = dt.Rows[i]["RAW_MAT_NM"].ToString();
                    //in_ord_gird.Rows[i].Cells["IN_RAW_MAT_CD"].Value = dt.Rows[i]["RAW_MAT_CD"].ToString();
                    in_ord_gird.Rows[i].Cells["IN_SPEC"].Value = dt.Rows[i]["SPEC"].ToString();
                    //n_in_ord_grid.Rows[i].Cells["IN_RAW_MAT_GUBUN"].Value = dt.Rows[i]["RAW_MAT_GUBUN"].ToString();
                    in_ord_gird.Rows[i].Cells["IN_RAW_MAT_GUBUN_NM"].Value = dt.Rows[i]["RAW_MAT_GUBUN_NM"].ToString();
                    in_ord_gird.Rows[i].Cells["IN_ORDER_AMT"].Value = dt.Rows[i]["ORDER_AMT"].ToString();
                    //in_ord_gird.Rows[i].Cells["IN_UNIT_CD"].Value = dt.Rows[i]["UNIT_CD"].ToString();
                    //in_ord_gird.Rows[i].Cells["IN_UNIT_NM"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                    in_ord_gird.Rows[i].Cells["IN_INPUT_AMT"].Value = dt.Rows[i]["INPUT_AMT"].ToString();
                    //in_ord_gird.Rows[i].Cells["NO_INPUT_AMT"].Value = dt.Rows[i]["NO_INPUT_AMT"].ToString();
                    in_ord_gird.Rows[i].Cells["IN_PRICE"].Value = dt.Rows[i]["PRICE"].ToString();
                    in_ord_gird.Rows[i].Cells["IN_TOTAL_MONEY"].Value = dt.Rows[i]["TOTAL_MONEY"].ToString();
                }
            }
        }
        #endregion input logic


        #region main grid logic
        private void tab_input_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tab_input.SelectedIndex == 0) //미입고
            {
                btn_move.Visible = true;
            }
            else //입고
            {
                btn_move.Visible = false;
            }
        }

        private void itemGridAdd()
        {
            ItemGrid.Rows.Add();
            ItemGrid.Rows[ItemGrid.Rows.Count - 1].Cells["No"].Value = ItemGrid.Rows.Count;
            ItemGrid.Rows[ItemGrid.Rows.Count - 1].Cells["IT_TOTAL_AMT"].Value = "0";
            ItemGrid.Rows[ItemGrid.Rows.Count - 1].Cells["IT_PRICE"].Value = "0";
            ItemGrid.Rows[ItemGrid.Rows.Count - 1].Cells["IT_TOTAL_PRICE"].Value = "0";
        }

        private void inputSoyoGridAdd()
        {
            inputRmSoyoGrid.Rows.Add();
            inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_NO"].Value = inputRmSoyoGrid.Rows.Count;

        }

        private void grid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            conDataGridView grd = (conDataGridView)sender;

            grd.Rows[e.RowIndex].Cells[0].Value = false;

            //wConst.init_RowText(grd, e.RowIndex);

            grd.Rows[e.RowIndex].Cells["TOTAL_MONEY"].Value = 0;
            grd.Rows[e.RowIndex].Cells["TOTAL_AMT"].Value = 0;
            grd.Rows[e.RowIndex].Cells["UNION_CD"].ReadOnly = true;
            grd.Rows[e.RowIndex].Cells["GRADE_NM"].ReadOnly = true;
            grd.Rows[e.RowIndex].Cells["TOTAL_AMT"].ReadOnly = true;
            grd.Rows[e.RowIndex].Cells["TOTAL_MONEY"].ReadOnly = true;

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.BackColor = Color.WhiteSmoke;
            style.SelectionBackColor = Color.NavajoWhite;

            grd.Rows[e.RowIndex].Cells["UNION_CD"].Style = style;
            grd.Rows[e.RowIndex].Cells["GRADE_NM"].Style = style;
            grd.Rows[e.RowIndex].Cells["MF_DATE"].Style = style;
            grd.Rows[e.RowIndex].Cells["EXPRT_DATE"].Style = style;
            grd.Rows[e.RowIndex].Cells["TOTAL_MONEY"].Style = style;
            grd.Rows[e.RowIndex].Cells["TOTAL_AMT"].Style = style;
            grd.Rows[e.RowIndex].Cells["UNIT_NM"].Style = style;
            grd.Rows[e.RowIndex].Cells["CHUGJONG_NM"].Style = style;
            grd.Rows[e.RowIndex].Cells["CLASS_NM"].Style = style;
            grd.Rows[e.RowIndex].Cells["COUNTRY"].Style = style;
            grd.Rows[e.RowIndex].Cells["TYPE_NM"].Style = style;
            grd.Rows[e.RowIndex].Cells["LABEL_NM"].Style = style;
            grd.Rows[e.RowIndex].Cells["RAW_MAT_GUBUN_NM"].Style = style;

            for (int kk = 0; kk < grd.Rows.Count; kk++)
            {
                grd.Rows[kk].Cells[1].Value = (kk + 1).ToString();
            }

        }

        private void grid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            conDataGridView grd = (conDataGridView)sender;

            for (int kk = 0; kk < grd.Rows.Count; kk++)
            {
                grd.Rows[kk].Cells[1].Value = (kk + 1).ToString();
            }
        }



        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void grid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            conDataGridView grd = (conDataGridView)sender;

            // 헤더 첫 컬럼 클릭시
            if (e.ColumnIndex != 0) return;

            if (bHeadCheck == false)
            {
                grd.Columns[0].HeaderText = "[v]";
                bHeadCheck = true;
                select_Check(grd, bHeadCheck);
            }
            else
            {
                grd.Columns[0].HeaderText = "[ ]";
                bHeadCheck = false;
                select_Check(grd, bHeadCheck);
            }
            grd.RefreshEdit();
            grd.Refresh();
        }

        private void select_Check(conDataGridView grd, bool bCheck)
        {
            for (int kk = 0; kk < grd.Rows.Count; kk++)
            {
                if (bCheck == true)
                {
                    grd.Rows[kk].Cells[0].Value = true;
                }
                else
                {
                    grd.Rows[kk].Cells[0].Value = false;
                }
            }
        }

        private void grid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            string name = inputRmGrid.CurrentCell.OwningColumn.Name;

            if (name == "TOTAL_AMT")
            {
                e.Control.KeyPress += new KeyPressEventHandler(txtCheckNumeric_KeyPress);
            }
            else
            {
                e.Control.KeyPress -= new KeyPressEventHandler(txtCheckNumeric_KeyPress);
            }
        }

        private void txtCheckNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e);
        }

        private void grid_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            dtp.Visible = false;
        }

        private void grid_scroll(object sender, ScrollEventArgs e)
        {
            dtp.Visible = false;
        }
        #endregion main grid logic

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                input_list(tdInputGrid, "where convert(varchar(10), Z.INTIME, 120) = convert(varchar(10), getDate(), 120) ");
            }
            else
            {
                start_date.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
                input_list(inputGrid, "where Z.INPUT_DATE >= '" + start_date.Text.ToString() + "' and  Z.INPUT_DATE <= '" + end_date.Text.ToString() + "'");
            }
        }

        private void tdInputGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ComInfo.grdHeaderNoAction(e))
            {
                input_detail(tdInputGrid, e);
            }
        }

        private void minus_logic(conDataGridView dgv)
        {
            if (dgv.Rows.Count > 0)
            {
                dgv.Rows[dgv.CurrentCell.RowIndex].Cells["IT_TOTAL_AMT"].Value = 0;
                dgv.Rows.RemoveAt(dgv.CurrentCell.RowIndex);
            }

            for (int i = 0; i < dgv.RowCount; i++)
            {
                dgv.Rows[i].Cells["NO"].Value = (i + 1);
            }


        }

        private void dtp_TextChange(object sender, EventArgs e)
        {
            DateTime dateTemp = DateTime.Today;
            ItemGrid.Rows[ItemGrid.CurrentCell.RowIndex].Cells[currunt_column_temp].Value = dtp.Text.ToString();
        }

        private void btn_plus_Click(object sender, EventArgs e)
        {
            itemGridAdd();
        }



        private void inputRmGrid_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cmb_exprt_auto_TextChanged(object sender, EventArgs e)
        {
            if (cmb_exprt_auto.SelectedValue.Equals("Y"))
            {
                lbl_exprt_count.Visible = true;
                cmb_exprt_count.Visible = true;



                if (cmb_exprt_count.SelectedValue != null)
                {
                    string exprtTemp = cmb_exprt_count.SelectedValue.ToString();
                    DateTime dateTemp = DateTime.Today;

                    for (int i = 0; i < inputRmGrid.RowCount; i++)
                    {
                        if (inputRmGrid.Rows[i].Cells["MF_DATE"].Value != null && !inputRmGrid.Rows[i].Cells["MF_DATE"].Value.Equals(""))
                        {
                            if (exprtTemp.Contains("M"))
                            {
                                dateTemp = DateTime.Parse(inputRmGrid.Rows[i].Cells["MF_DATE"].Value.ToString()).AddMonths(int.Parse(exprtTemp.Replace("M", "")));

                            }
                            else
                            {
                                dateTemp = DateTime.Parse(inputRmGrid.Rows[i].Cells["MF_DATE"].Value.ToString()).AddDays(int.Parse(exprtTemp.Replace("D", "")));
                            }
                            inputRmGrid.Rows[i].Cells["EXPRT_DATE"].Value = dateTemp.ToString("yyyy-MM-dd");
                        }
                    }
                }


            }
            else
            {
                lbl_exprt_count.Visible = false;
                cmb_exprt_count.Visible = false;

            }
        }

        private void cmb_exprt_count_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_exprt_count.SelectedValue != null)
            {
                string exprtTemp = cmb_exprt_count.SelectedValue.ToString();
                DateTime dateTemp = DateTime.Today;

                for (int i = 0; i < inputRmGrid.RowCount; i++)
                {
                    if (inputRmGrid.Rows[i].Cells["MF_DATE"].Value != null && !inputRmGrid.Rows[i].Cells["MF_DATE"].Value.Equals(""))
                    {
                        if (exprtTemp.Contains("M"))
                        {
                            dateTemp = DateTime.Parse(inputRmGrid.Rows[i].Cells["MF_DATE"].Value.ToString()).AddMonths(int.Parse(exprtTemp.Replace("M", "")));

                        }
                        else
                        {
                            dateTemp = DateTime.Parse(inputRmGrid.Rows[i].Cells["MF_DATE"].Value.ToString()).AddDays(int.Parse(exprtTemp.Replace("D", "")));
                        }
                        inputRmGrid.Rows[i].Cells["EXPRT_DATE"].Value = dateTemp.ToString("yyyy-MM-dd");
                    }
                }
            }
        }

        private void btn_work_inst_srch_Click(object sender, EventArgs e)
        {
            Popup.pop씨지엠작업지시검색 popup = new Popup.pop씨지엠작업지시검색();

            popup.ShowDialog();


            if (popup.sInst_date != null && !popup.sInst_date.Equals(""))
            {
                txt_work_date.Text = popup.sInst_date;
                txt_work_cd.Text = popup.sInst_cd;
                txt_inst_notice.Text = popup.sInst_notice;
                txt_raw_mat_cd.Text = popup.sRaw_mat_cd;
                end_req_date.Text = popup.sDelivery_date;
                txt_lot_number.Text = popup.sLot_no;
                txt_complete_yn.Text = popup.sComplete_yn;



                wnDm wDm = new wnDm();
                DataTable dt = wDm.fn_Raw_List("where A.RAW_MAT_CD = '" + txt_raw_mat_cd.Text + "'   ");

                if (dt.Rows.Count > 0)
                {
                    inputRmGrid.Rows.Clear();
                    inputRmGrid.Rows.Add();
                    inputRmGrid.Rows[0].Cells["RAW_MAT_CD"].Value = dt.Rows[0]["RAW_MAT_CD"].ToString();
                    inputRmGrid.Rows[0].Cells["RAW_MAT_NM"].Value = dt.Rows[0]["RAW_MAT_NM"].ToString();
                    inputRmGrid.Rows[0].Cells["TOTAL_AMT"].Value = decimal.Parse(popup.sInst_Amt).ToString("#,0.######");
                    inputRmGrid.Rows[0].Cells["UNIT_NM"].Value = dt.Rows[0]["INPUT_UNIT_NM"].ToString();
                    inputRmGrid.Rows[0].Cells["CHUGJONG_NM"].Value = dt.Rows[0]["CHUGJONG_NM"].ToString();
                    inputRmGrid.Rows[0].Cells["COUNTRY"].Value = dt.Rows[0]["COUNTRY_NM"].ToString();
                    inputRmGrid.Rows[0].Cells["CLASS_NM"].Value = dt.Rows[0]["CLASS_NM"].ToString();
                    inputRmGrid.Rows[0].Cells["TYPE_NM"].Value = dt.Rows[0]["TYPE_NM"].ToString();
                    inputRmGrid.Rows[0].Cells["LABEL_NM"].Value = dt.Rows[0]["LABEL_NM"].ToString();

                    inputRmSoyoGrid.Rows.Clear();

                    DataTable dt2 = wDm.fn_Work_Inst_detail("WHERE W_INST_DATE = '" + txt_work_date.Text + "'  AND W_INST_CD = '" + txt_work_cd.Text + "'   ");

                    if (dt2.Rows.Count > 0)
                    {
                        decimal SumSoyo = 0;
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            inputSoyoGridAdd();
                            inputRmSoyoGrid.Rows[i].Cells["s_INPUT_DATE"].Value = dt2.Rows[i]["INPUT_DATE"].ToString();
                            inputRmSoyoGrid.Rows[i].Cells["s_INPUT_CD"].Value = dt2.Rows[i]["INPUT_CD"].ToString();
                            inputRmSoyoGrid.Rows[i].Cells["s_INPUT_SEQ"].Value = dt2.Rows[i]["INPUT_SEQ"].ToString();
                            inputRmSoyoGrid.Rows[i].Cells["s_RAW_MAT_CD"].Value = dt2.Rows[i]["RAW_MAT_CD"].ToString();
                            inputRmSoyoGrid.Rows[i].Cells["s_RAW_MAT_NM"].Value = dt2.Rows[i]["RAW_MAT_NM"].ToString();
                            inputRmSoyoGrid.Rows[i].Cells["s_RAW_MAT_GUBUN"].Value = dt2.Rows[i]["RAW_MAT_GUBUN"].ToString();
                            inputRmSoyoGrid.Rows[i].Cells["s_EXPRT_DATE"].Value = dt2.Rows[i]["EXPRT_DATE"].ToString();
                            inputRmSoyoGrid.Rows[i].Cells["s_UNION_CD"].Value = dt2.Rows[i]["UNION_CD"].ToString();
                            inputRmSoyoGrid.Rows[i].Cells["s_SOYO_AMT"].Value = decimal.Parse(dt2.Rows[i]["SOYO_AMT"].ToString()).ToString("#,0.######");
                            SumSoyo += decimal.Parse(dt2.Rows[i]["SOYO_AMT"].ToString());
                            inputRmSoyoGrid.Rows[i].Cells["s_UNIT_CD"].Value = dt2.Rows[i]["UNIT_CD"].ToString();
                            inputRmSoyoGrid.Rows[i].Cells["s_UNIT_NM"].Value = dt2.Rows[i]["UNIT_NM"].ToString();
                            inputRmSoyoGrid.Rows[i].Cells["s_CHUGJONG_CD"].Value = dt2.Rows[i]["CHUGJONG_CD"].ToString();
                            inputRmSoyoGrid.Rows[i].Cells["s_CHUGJONG_NM"].Value = dt2.Rows[i]["CHUGJONG_NM"].ToString();
                            inputRmSoyoGrid.Rows[i].Cells["s_COUNTRY_CD"].Value = dt2.Rows[i]["COUNTRY_CD"].ToString();
                            inputRmSoyoGrid.Rows[i].Cells["s_COUNTRY_NM"].Value = dt2.Rows[i]["COUNTRY_NM"].ToString();
                            inputRmSoyoGrid.Rows[i].Cells["s_CLASS_CD"].Value = dt2.Rows[i]["CLASS_CD"].ToString();
                            inputRmSoyoGrid.Rows[i].Cells["s_CLASS_NM"].Value = dt2.Rows[i]["CLASS_NM"].ToString();
                            inputRmSoyoGrid.Rows[i].Cells["s_TYPE_CD"].Value = dt2.Rows[i]["TYPE_CD"].ToString();
                            inputRmSoyoGrid.Rows[i].Cells["s_TYPE_NM"].Value = dt2.Rows[i]["TYPE_NM"].ToString();
                            inputRmSoyoGrid.Rows[i].Cells["s_LABEL_NM"].Value = dt2.Rows[i]["LABEL_NM"].ToString();
                            inputRmSoyoGrid.Rows[i].Cells["s_LOSS_AMT"].ReadOnly = true;
                            inputRmSoyoGrid.Rows[i].Cells["s_OUT_LOC"].Value = dt2.Rows[i]["DIRECTION"].ToString();
                        }
                        inputSoyoGridAdd();
                        inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_INPUT_DATE"].Value = "";
                        inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_INPUT_CD"].Value = "";
                        inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_INPUT_SEQ"].Value = "";
                        inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_EXPRT_DATE"].Value = "";
                        inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_CHUGJONG_NM"].Value = "";
                        inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_COUNTRY_NM"].Value = "";
                        inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_CLASS_NM"].Value = "";
                        inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_TYPE_NM"].Value = "";
                        inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_LABEL_NM"].Value = "합계";
                        inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_UNION_CD"].Value = "합계";
                        inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_UNIT_NM"].Value = dt2.Rows[0]["UNIT_NM"].ToString();
                        inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_SOYO_AMT"].Value = decimal.Parse(SumSoyo.ToString()).ToString("#,0.######");
                        inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_RAW_MAT_NM"].Value = inputRmSoyoGrid.Rows[0].Cells["s_RAW_MAT_NM"].Value + " - " + (inputRmSoyoGrid.Rows.Count - 1) + "항목";
                        inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_RAW_MAT_CD"].Value = "ALL_RAW_MAT_SUM!";
                        inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_RAW_MAT_GUBUN"].Value = "1";
                        inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_OUT_LOC"].Value = inputRmSoyoGrid.Rows[0].Cells["s_OUT_LOC"].Value.ToString();
                        inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_LOSS_AMT"].ReadOnly = true;
                    }
                    else
                    {
                        MessageBox.Show("사용되기 위해 이동된 원자재가 없습니다.\n현재 상태 : 대기중");
                        resetSetting();
                        ItemGrid.Rows.Clear();
                        return;
                    }
                    ItemGrid.Rows.Clear();
                    itemGridAdd();
                    btn_minus.Visible = true;
                    btn_plus.Visible = true;
                    lbl_minus.Visible = true;
                    lbl_plus.Visible = true;




                }
            }
        }

        private void ItemGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            conDataGridView grd = (conDataGridView)sender;
            DataGridViewCell cell = grd[e.ColumnIndex, e.RowIndex];

            cell.Style.BackColor = Color.White;

            /*if (grd.Rows[e.RowIndex].Cells["TOTAL_PRICE"].Value == null || grd.Rows[e.RowIndex].Cells["TOTAL_PRICE"].Value.Equals(""))
            {
                grd.Rows[e.RowIndex].Cells["TOTAL_PRICE"].Value = "0";
            }
            if (grd.Rows[e.RowIndex].Cells["TOTAL_AMT"].Value == null || grd.Rows[e.RowIndex].Cells["TOTAL_AMT"].Value.Equals(""))
            {
                grd.Rows[e.RowIndex].Cells["TOTAL_AMT"].Value = "0";
            }
            if (grd.Rows[e.RowIndex].Cells["PRICE"].Value == null || grd.Rows[e.RowIndex].Cells["PRICE"].Value.Equals(""))
            {
                grd.Rows[e.RowIndex].Cells["PRICE"].Value = "0";
            }*/

            #region 공통 그리드 체크
            if (grd.Columns[e.ColumnIndex].ToolTipText.IndexOf("명칭") >= 0 && grd._KeyInput == "enter")
            {
                //MessageBox.Show("hit");
                string item_nm = (string)grd.Rows[e.RowIndex].Cells["IT_ITEM_NM"].Value;
                wnDm wDm = new wnDm();
                DataTable dt = new DataTable();
                dt = wDm.fn_Item_List_Search_by_Comp("where ITEM_NM like '%" + item_nm + "%' " , txt_raw_mat_cd.Text);

                if (dt.Rows.Count > 0)
                { //row가 2줄이 넘을 경우 팝업으로 넘어간다.

                    string scode = wConst.CZM_call_pop_item(grd, dt, e.RowIndex, item_nm);

                    if (scode != null && !scode.Equals(""))
                    {
                        wDm = new wnDm();
                        dt = wDm.fn_Item_Comp_List("WHERE ITEM_CD = '" + scode + "'   ");

                        if (dt.Rows.Count > 0)
                        {
                            DataTable dtTemp = new DataTable();
                            dtTemp.Columns.Add("INPUT_DATE");
                            dtTemp.Columns.Add("INPUT_CD");
                            dtTemp.Columns.Add("SEQ");
                            dtTemp.Columns.Add("EXPRT_DATE");
                            dtTemp.Columns.Add("CURR_AMT");
                            dtTemp.Columns.Add("RAW_MAT_CD");
                            dtTemp.Columns.Add("RAW_MAT_NM");
                            dtTemp.Columns.Add("OUTPUT_UNIT_NM");
                            dtTemp.Columns.Add("OUTPUT_UNIT_CD");
                            dtTemp.Columns.Add("RAW_MAT_GUBUN");
                            dtTemp.Columns.Add("OUT_LOC");
                            //dtTemp.Columns.Add("RAW_MAT_GUBUN");

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                int Soyo_RowIndex = -1;

                                for (int j = 0; j < inputRmSoyoGrid.Rows.Count; j++)
                                {
                                    if (inputRmSoyoGrid.Rows[j].Cells["s_RAW_MAT_CD"].Value.ToString().Equals(dt.Rows[i]["RAW_MAT_CD"].ToString()))
                                    {
                                        Soyo_RowIndex = j;
                                        break;
                                    }
                                }
                                if (Soyo_RowIndex == -1)
                                {
                                    if (dt.Rows[i]["RAW_MAT_GUBUN"].ToString().Equals("1"))
                                    {
                                        MessageBox.Show("이동 원료육을 제외한 다른 원자재가 포함되어 있는 제품입니다.");
                                        ItemGrid.Rows.RemoveAt(e.RowIndex);
                                        ItemGrid.Rows.Add();
                                        return;
                                    }
                                    else
                                    {
                                        DataTable dt2 = wDm.fn_Input_Detail_List("WHERE A.RAW_MAT_CD = '" + dt.Rows[i]["RAW_MAT_CD"].ToString() + "'  AND A.CURR_AMT > 0  ");
                                        if (dt2.Rows.Count > 0)
                                        {
                                            dtTemp.Rows.Add();
                                            dtTemp.Rows[dtTemp.Rows.Count - 1]["INPUT_DATE"] = dt2.Rows[dt2.Rows.Count - 1]["INPUT_DATE"].ToString();
                                            dtTemp.Rows[dtTemp.Rows.Count - 1]["INPUT_CD"] = dt2.Rows[dt2.Rows.Count - 1]["INPUT_CD"].ToString();
                                            dtTemp.Rows[dtTemp.Rows.Count - 1]["SEQ"] = dt2.Rows[dt2.Rows.Count - 1]["SEQ"].ToString();
                                            dtTemp.Rows[dtTemp.Rows.Count - 1]["EXPRT_DATE"] = dt2.Rows[dt2.Rows.Count - 1]["EXPRT_DATE"].ToString();
                                            dtTemp.Rows[dtTemp.Rows.Count - 1]["CURR_AMT"] = dt2.Rows[dt2.Rows.Count - 1]["CURR_AMT"].ToString();
                                            dtTemp.Rows[dtTemp.Rows.Count - 1]["RAW_MAT_CD"] = dt2.Rows[dt2.Rows.Count - 1]["RAW_MAT_CD"].ToString();
                                            dtTemp.Rows[dtTemp.Rows.Count - 1]["RAW_MAT_NM"] = dt2.Rows[dt2.Rows.Count - 1]["RAW_MAT_NM"].ToString();
                                            dtTemp.Rows[dtTemp.Rows.Count - 1]["RAW_MAT_GUBUN"] = dt2.Rows[dt2.Rows.Count - 1]["RAW_MAT_GUBUN"].ToString();
                                            dtTemp.Rows[dtTemp.Rows.Count - 1]["OUTPUT_UNIT_NM"] = dt2.Rows[dt2.Rows.Count - 1]["OUTPUT_UNIT_NM"].ToString();
                                            dtTemp.Rows[dtTemp.Rows.Count - 1]["OUTPUT_UNIT_CD"] = dt2.Rows[dt2.Rows.Count - 1]["OUTPUT_UNIT_CD"].ToString();
                                            dtTemp.Rows[dtTemp.Rows.Count - 1]["OUT_LOC"] = "STORE_2F";
                                        }
                                        else
                                        {
                                            MessageBox.Show("해당 제품의 생산을 위한 원부자재가 입고되지 않았습니다");
                                            ItemGrid.Rows.RemoveAt(e.RowIndex);
                                            ItemGrid.Rows.Add();
                                            return;
                                        }
                                    }
                                }
                            }
                            for (int k = 0; k < dtTemp.Rows.Count; k++)
                            {
                                inputSoyoGridAdd();
                                inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_INPUT_DATE"].Value = dtTemp.Rows[k]["INPUT_DATE"];
                                inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_INPUT_CD"].Value = dtTemp.Rows[k]["INPUT_CD"];
                                inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_INPUT_SEQ"].Value = dtTemp.Rows[k]["SEQ"];
                                inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_EXPRT_DATE"].Value = dtTemp.Rows[k]["EXPRT_DATE"];
                                inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_USABLE_AMT"].Value = dtTemp.Rows[k]["CURR_AMT"];
                                inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_SOYO_AMT"].Value = decimal.Parse(dtTemp.Rows[k]["CURR_AMT"].ToString()).ToString("#,0.######");
                                inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_RAW_MAT_CD"].Value = dtTemp.Rows[k]["RAW_MAT_CD"];
                                inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_RAW_MAT_NM"].Value = dtTemp.Rows[k]["RAW_MAT_NM"];
                                inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_RAW_MAT_GUBUN"].Value = dtTemp.Rows[k]["RAW_MAT_GUBUN"];
                                inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_UNIT_NM"].Value = dtTemp.Rows[k]["OUTPUT_UNIT_NM"];
                                inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_UNIT_CD"].Value = dtTemp.Rows[k]["OUTPUT_UNIT_CD"];
                                inputRmSoyoGrid.Rows[inputRmSoyoGrid.Rows.Count - 1].Cells["s_OUT_LOC"].Value = dtTemp.Rows[k]["OUT_LOC"];
                            }
                        }
                        else
                        {
                            MessageBox.Show("BOM이 등록되어 있지 않은 제품입니다. BOM을 먼저 등록해주세요");
                            ItemGrid.Rows.RemoveAt(e.RowIndex);
                            ItemGrid.Rows.Add();
                            return;
                        }





                        //=========================================================================================================

                        DataTable dt3 = wDm.fn_Work_Inst_detail("WHERE W_INST_DATE = '" + txt_work_date.Text + "' AND W_INST_CD = '" + txt_work_cd.Text + "'   ");
                        string union_cd = "";
                        for (int i = 0; i < dt3.Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                union_cd += dt3.Rows[i]["UNION_CD"].ToString();
                            }
                            else
                            {
                                if (!union_cd.Contains(dt3.Rows[i]["UNION_CD"].ToString()))
                                    union_cd += "," + dt3.Rows[i]["UNION_CD"].ToString();
                            }
                        }
                        grd.Rows[e.RowIndex].Cells["IT_UNION_CD"].Value = union_cd;

                        if (!union_cd.Contains(","))
                        {
                            grd.Rows[e.RowIndex].Cells["IT_UNION_CD_AFTER"].Value = union_cd;
                        }


                    }

                    //orderGridAdd();
                }
                else
                { //row가 없는 경우
                    MessageBox.Show("제품 데이터가 없습니다.");
                }

                //wConst.f_Calc_Price(grd, e.RowIndex, "TOTAL_AMT", "PRICE");
                //wConst.f_Calc_PriceAndBox(grd, e.RowIndex, "TOTAL_AMT", "PRICE", "BOX_AMT");

                //wConst.f_Calc_Box(grd, e.RowIndex, "TOTAL_AMT", "BOX_AMT");
            }


            #endregion 공통 그리드 체크

            //string sSearchTxt = "" + (string)grd.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        }

        private void ItemGrid_KeyDown(object sender, KeyEventArgs e)
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

        private void inputRmSoyoGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            e.Cancel = false;
        }

        private void Cal_And_Increase_SoyoAmt(int e_Rowindex)
        {
            first_touch = false;
            btnSave.Enabled = true;
            try
            {
                wnDm wDm = new wnDm();

                for (int p = 0; p < inputRmSoyoGrid.Rows.Count; p++)
                {
                    inputRmSoyoGrid.Rows[p].Cells["s_USE_AMT"].Value = "";
                }

                for (int k = 0; k < ItemGrid.Rows.Count; k++)
                {
                    DataTable dt = wDm.fn_Item_Comp_List("where A.ITEM_CD = '" + ItemGrid.Rows[k].Cells["IT_ITEM_CD"].Value.ToString() + "'   ");
                    if (dt.Rows.Count > 0)
                    {
                        int isOnSoyoGrid = -1;
                        int dtRowsCount = -1;
                        decimal minusTemp = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            for (int j = 0; j < inputRmSoyoGrid.Rows.Count; j++)
                            {
                                if (inputRmSoyoGrid.Rows[j].Cells["s_RAW_MAT_CD"].Value.ToString().Equals(dt.Rows[i]["RAW_MAT_CD"].ToString()) //일단 원자재코드값 같은것 찾고
                                    &&
                                    (!inputRmSoyoGrid.Rows[j].Cells["s_SOYO_AMT"].Value.ToString().Equals(inputRmSoyoGrid.Rows[j].Cells["s_USE_AMT"].Value.ToString()) //SOYOAMT와 USEAMT가 같지 않거나 (해당원자재 마지막)
                                    ||
                                    (j + 1 >= inputRmSoyoGrid.RowCount //다음행이 마지막행이거나 (GRID마지막)
                                    ||
                                    !inputRmSoyoGrid.Rows[j + 1].Cells["s_RAW_MAT_CD"].Value.ToString().Equals(dt.Rows[i]["RAW_MAT_CD"].ToString()) //다음행의 RAW_MAT_CD가 지금과 다르거나 (해당원자재 마지막)
                                    )
                                    )
                                    )
                                {
                                    isOnSoyoGrid = j;
                                    dtRowsCount = i;
                                    break;
                                }
                            }
                            if (isOnSoyoGrid == -1)
                            {
                                MessageBox.Show("오류");

                                return;
                            }
                            else
                            {
                                if (inputRmSoyoGrid.Rows[isOnSoyoGrid].Cells["s_USE_AMT"].Value == null
                                    || inputRmSoyoGrid.Rows[isOnSoyoGrid].Cells["s_USE_AMT"].Value.ToString().Equals(""))
                                {
                                    inputRmSoyoGrid.Rows[isOnSoyoGrid].Cells["s_USE_AMT"].Value = 0;
                                    inputRmSoyoGrid.Rows[isOnSoyoGrid].Cells["s_USETEMP"].Value = 0;

                                }
                                decimal dcTemp = decimal.Parse(inputRmSoyoGrid.Rows[isOnSoyoGrid].Cells["s_USE_AMT"].Value.ToString())
                                    + (decimal.Parse(ItemGrid.Rows[k].Cells["IT_TOTAL_AMT"].Value.ToString())
                                    * decimal.Parse(dt.Rows[dtRowsCount]["TOTAL_AMT"].ToString()));
                                dcTemp -= minusTemp;
                                if (decimal.Parse(inputRmSoyoGrid.Rows[isOnSoyoGrid].Cells["s_SOYO_AMT"].Value.ToString()) < dcTemp
                                   && isOnSoyoGrid + 1 < inputRmSoyoGrid.RowCount
                                    && inputRmSoyoGrid.Rows[isOnSoyoGrid].Cells["s_RAW_MAT_CD"].Value.ToString().Equals(inputRmSoyoGrid.Rows[isOnSoyoGrid + 1].Cells["s_RAW_MAT_CD"].Value.ToString()))
                                {
                                    inputRmSoyoGrid.Rows[isOnSoyoGrid].Cells["s_USE_AMT"].Value = inputRmSoyoGrid.Rows[isOnSoyoGrid].Cells["s_SOYO_AMT"].Value;
                                    inputRmSoyoGrid.Rows[isOnSoyoGrid].Cells["s_USETEMP"].Value = inputRmSoyoGrid.Rows[isOnSoyoGrid].Cells["s_SOYO_AMT"].Value;
                                    minusTemp += decimal.Parse(inputRmSoyoGrid.Rows[isOnSoyoGrid].Cells["s_SOYO_AMT"].Value.ToString());
                                    i--;
                                }
                                else
                                {
                                    inputRmSoyoGrid.Rows[isOnSoyoGrid].Cells["s_USE_AMT"].Value = dcTemp.ToString("#,0.######");
                                    inputRmSoyoGrid.Rows[isOnSoyoGrid].Cells["s_USETEMP"].Value = dcTemp.ToString("#,0.######");

                                    minusTemp = 0;
                                }
                            }
                        }
                    }

                } // end insert for 

                decimal soyoSumTemp = 0;
                for (int i = 0; i < inputRmSoyoGrid.RowCount; i++)
                {
                    if (inputRmSoyoGrid.Rows[i].Cells["s_RAW_MAT_CD"].Value.ToString().Equals("ALL_RAW_MAT_SUM!"))
                    {
                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (!inputRmSoyoGrid.Rows[j].Cells["s_USE_AMT"].Value.ToString().Equals(""))
                            {
                                soyoSumTemp += decimal.Parse(inputRmSoyoGrid.Rows[j].Cells["s_USE_AMT"].Value.ToString());
                            }
                        }
                        inputRmSoyoGrid.Rows[i].Cells["s_USE_AMT"].Value = soyoSumTemp.ToString("#,0.######");
                        inputRmSoyoGrid.Rows[i].Cells["s_USETEMP"].Value = soyoSumTemp.ToString("#,0.######");
                    }
                }

                removeRow_What_Null_Value();
            }
            catch (Exception E)
            {
                Console.WriteLine(E + "/" + E.StackTrace);
            }
            inputRmSoyoGrid.Refresh();
            CheckAmt(e_Rowindex);
        }

        private void removeRow_What_Null_Value()
        {
            for (int i = 0; i < inputRmSoyoGrid.RowCount; i++)
            {
                if (!(inputRmSoyoGrid.Rows[i].Cells["s_RAW_MAT_GUBUN"].Value.ToString().Equals("1")
                    || inputRmSoyoGrid.Rows[i].Cells["s_RAW_MAT_CD"].Value.ToString().Equals("ALL_RAW_MAT_SUM!"))
                    &&
                    (inputRmSoyoGrid.Rows[i].Cells["s_USE_AMT"].Value == null
                    || inputRmSoyoGrid.Rows[i].Cells["s_USE_AMT"].Value.ToString().Equals("")
                    ))
                {
                    inputRmSoyoGrid.Rows.RemoveAt(i--);
                }
            }
            for (int i = 0; i < inputRmSoyoGrid.RowCount; i++)
            {
                inputRmSoyoGrid.Rows[i].Cells["s_NO"].Value = i + 1;
                inputRmSoyoGrid.Rows[i].Cells["s_LOSS_AMT"].Value = "";
            }
        }

        private void removeRow_What_Zero_Value()
        {
            for (int i = 0; i < inputRmSoyoGrid.RowCount; i++)
            {
                if (!(inputRmSoyoGrid.Rows[i].Cells["s_RAW_MAT_GUBUN"].Value.ToString().Equals("1")
                    || inputRmSoyoGrid.Rows[i].Cells["s_RAW_MAT_CD"].Value.ToString().Equals("ALL_RAW_MAT_SUM!"))
                    &&
                    (inputRmSoyoGrid.Rows[i].Cells["s_USE_AMT"].Value == null
                    || inputRmSoyoGrid.Rows[i].Cells["s_USE_AMT"].Value.ToString().Equals("")
                    || inputRmSoyoGrid.Rows[i].Cells["s_USE_AMT"].Value.ToString().Equals("0")
                    ))
                {
                    inputRmSoyoGrid.Rows.RemoveAt(i--);
                }
            }
            for (int i = 0; i < inputRmSoyoGrid.RowCount; i++)
            {
                inputRmSoyoGrid.Rows[i].Cells["s_NO"].Value = i + 1;
                inputRmSoyoGrid.Rows[i].Cells["s_LOSS_AMT"].Value = "";
            }
        }

        private void CheckAmt(int e_Rowindex)
        {
            decimal totAmtTemp = 0;
            decimal useTemp = 0;

            for (int i = 0; i < inputRmSoyoGrid.Rows.Count; i++)
            {
                if (inputRmSoyoGrid.Rows[i].Cells["s_USE_AMT"].Value.ToString().Equals(""))
                {
                    continue;
                }
                totAmtTemp = decimal.Parse(inputRmSoyoGrid.Rows[i].Cells["s_SOYO_AMT"].Value.ToString());
                useTemp = decimal.Parse(inputRmSoyoGrid.Rows[i].Cells["s_USE_AMT"].Value.ToString());

                if (totAmtTemp < useTemp)
                {
                    if (inputRmSoyoGrid.Rows[i].Cells["s_RAW_MAT_GUBUN"].Value.ToString().Equals("1"))
                    {
                        MessageBox.Show(inputRmSoyoGrid.Rows[i].Cells["s_RAW_MAT_NM"].Value.ToString() + "에 대한 소요량이 부족합니다 ! ");
                        ItemGrid.Rows[e_Rowindex].Cells["IT_TOTAL_AMT"].Value = "0";
                        Cal_And_Increase_SoyoAmt(e_Rowindex);
                        return;
                    }
                    else
                    {
                        DialogResult msgOk = comInfo.warningMessage(inputRmSoyoGrid.Rows[i].Cells["s_RAW_MAT_NM"].Value.ToString() + "에 대한 소요량 부족\n이어서 계속 추가합니까?");
                        if (msgOk == DialogResult.No)
                        {
                            ItemGrid.Rows[e_Rowindex].Cells["IT_TOTAL_AMT"].Value = "0";
                            Cal_And_Increase_SoyoAmt(e_Rowindex);
                            return;
                        }
                        else
                        {
                            wnDm wDm = new wnDm();
                            DataTable dt = wDm.fn_Input_Detail_List2("WHERE A.RAW_MAT_CD = '" + inputRmSoyoGrid.Rows[i].Cells["s_RAW_MAT_CD"].Value.ToString() + "'  AND A.CURR_AMT > 0 "
                            , "WHERE A.RAW_MAT_CD = '" + inputRmSoyoGrid.Rows[i].Cells["s_RAW_MAT_CD"].Value.ToString() + "'  AND A.CURR_AMT > 0 "
                            + "AND ( (A.INPUT_DATE > '" + inputRmSoyoGrid.Rows[i].Cells["s_INPUT_DATE"].Value.ToString() + "' ) OR (A.INPUT_DATE >= '"
                            + inputRmSoyoGrid.Rows[i].Cells["s_INPUT_DATE"].Value.ToString() + "' AND A.INPUT_CD > '" + inputRmSoyoGrid.Rows[i].Cells["s_INPUT_CD"].Value.ToString() + "' ))  ");

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                inputRmSoyoGrid.Rows.Add();
                                DataGridView dgvTemp = new DataGridView();

                                for (int j = 0; j < inputRmSoyoGrid.Columns.Count; j++)
                                {
                                    dgvTemp.Columns.Add(inputRmSoyoGrid.Columns[j].Name, inputRmSoyoGrid.Columns[j].Name);
                                }
                                dgvTemp.Rows.Add();
                                for (int j = inputRmSoyoGrid.Rows.Count - 1; j > i + 1; j--)
                                {
                                    for (int k = 0; k < inputRmSoyoGrid.Columns.Count; k++)
                                    {
                                        dgvTemp.Rows[0].Cells[k].Value = inputRmSoyoGrid.Rows[j - 1].Cells[k].Value;
                                        inputRmSoyoGrid.Rows[j].Cells[k].Value = dgvTemp.Rows[0].Cells[k].Value;
                                    }
                                    inputRmSoyoGrid.Rows[j].Cells["s_NO"].Value = int.Parse(inputRmSoyoGrid.Rows[j].Cells["s_NO"].Value.ToString()) + 1;
                                }
                                for (int j = 0; j < inputRmSoyoGrid.Columns.Count; j++)
                                {
                                    inputRmSoyoGrid.Rows[i + 1].Cells[j].Value = "";
                                }

                                inputRmSoyoGrid.Rows[i + 1].Cells["s_NO"].Value = i + 2;
                                inputRmSoyoGrid.Rows[i + 1].Cells["s_INPUT_DATE"].Value = dt.Rows[dt.Rows.Count - 1]["INPUT_DATE"];
                                inputRmSoyoGrid.Rows[i + 1].Cells["s_INPUT_CD"].Value = dt.Rows[dt.Rows.Count - 1]["INPUT_CD"];
                                inputRmSoyoGrid.Rows[i + 1].Cells["s_INPUT_SEQ"].Value = dt.Rows[dt.Rows.Count - 1]["SEQ"];
                                inputRmSoyoGrid.Rows[i + 1].Cells["s_EXPRT_DATE"].Value = dt.Rows[dt.Rows.Count - 1]["EXPRT_DATE"];
                                inputRmSoyoGrid.Rows[i + 1].Cells["s_USABLE_AMT"].Value = dt.Rows[dt.Rows.Count - 1]["CURR_AMT"];
                                inputRmSoyoGrid.Rows[i + 1].Cells["s_SOYO_AMT"].Value = decimal.Parse(dt.Rows[dt.Rows.Count - 1]["CURR_AMT"].ToString()).ToString("#,0.######");
                                inputRmSoyoGrid.Rows[i + 1].Cells["s_RAW_MAT_CD"].Value = dt.Rows[dt.Rows.Count - 1]["RAW_MAT_CD"];
                                inputRmSoyoGrid.Rows[i + 1].Cells["s_RAW_MAT_NM"].Value = dt.Rows[dt.Rows.Count - 1]["RAW_MAT_NM"];
                                inputRmSoyoGrid.Rows[i + 1].Cells["s_RAW_MAT_GUBUN"].Value = dt.Rows[dt.Rows.Count - 1]["RAW_MAT_GUBUN"];
                                inputRmSoyoGrid.Rows[i + 1].Cells["s_UNIT_NM"].Value = dt.Rows[dt.Rows.Count - 1]["OUTPUT_UNIT_NM"];
                                inputRmSoyoGrid.Rows[i + 1].Cells["s_UNIT_CD"].Value = dt.Rows[dt.Rows.Count - 1]["OUTPUT_UNIT_CD"];
                                inputRmSoyoGrid.Rows[i + 1].Cells["s_OUT_LOC"].Value = "STORE_2F";


                                inputRmSoyoGrid.Rows[i + 1].Cells["s_USE_AMT"].Value = (decimal.Parse(inputRmSoyoGrid.Rows[i].Cells["s_USE_AMT"].Value.ToString()) - decimal.Parse(inputRmSoyoGrid.Rows[i].Cells["s_SOYO_AMT"].Value.ToString())).ToString("#,0.######");
                                inputRmSoyoGrid.Rows[i].Cells["s_USE_AMT"].Value = inputRmSoyoGrid.Rows[i].Cells["s_SOYO_AMT"].Value.ToString();

                                inputRmSoyoGrid.Rows[i + 1].Cells["s_USETEMP"].Value = (decimal.Parse(inputRmSoyoGrid.Rows[i].Cells["s_USETEMP"].Value.ToString()) - decimal.Parse(inputRmSoyoGrid.Rows[i].Cells["s_SOYO_AMT"].Value.ToString())).ToString("#,0.######");
                                inputRmSoyoGrid.Rows[i].Cells["s_USETEMP"].Value = inputRmSoyoGrid.Rows[i].Cells["s_SOYO_AMT"].Value.ToString();
                            }
                            else
                            {
                                MessageBox.Show("재고량부족");

                                inputRmSoyoGrid.Rows.Clear();
                                for (int j = 0; j < copied_dgv.RowCount; j++)
                                {
                                    inputRmSoyoGrid.Rows.Add();
                                    for (int k = 0; k < copied_dgv.ColumnCount; k++)
                                    {
                                        inputRmSoyoGrid.Rows[j].Cells[k].Value = copied_dgv.Rows[j].Cells[k].Value;
                                    }

                                }
                                for (int j = 0; j < inputRmSoyoGrid.Columns.Count; j++)
                                {
                                    if (inputRmSoyoGrid.Rows[j].Cells["s_RAW_MAT_CD"].Value == null || inputRmSoyoGrid.Rows[j].Cells["s_RAW_MAT_CD"].Value.ToString().Equals(""))
                                    {
                                        inputRmSoyoGrid.Rows.RemoveAt(j);
                                    }
                                }
                                ItemGrid.Rows[e_Rowindex].Cells["IT_TOTAL_AMT"].Value = "0";
                                Cal_And_Increase_SoyoAmt(e_Rowindex);
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void ItemGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (isUserInput && e.RowIndex > -1 && dgv.Columns[e.ColumnIndex].Name.ToString().Equals("IT_TOTAL_AMT") && dgv.Rows[e.RowIndex].Cells[1].Value != null && !dgv.Rows[e.RowIndex].Cells[2].Value.Equals(""))
            {


                copied_dgv.Rows.Clear();
                for (int i = 0; i < inputRmSoyoGrid.RowCount; i++)
                {
                    copied_dgv.Rows.Add();
                    for (int j = 0; j < inputRmSoyoGrid.Columns.Count; j++)
                    {
                        copied_dgv.Rows[i].Cells[j].Value = inputRmSoyoGrid.Rows[i].Cells[j].Value;
                    }
                }
                if (first_touch)
                {
                    MessageBox.Show("수정모드로 진입합니다. LOSS가 초기화됩니다.");
                }
                Cal_And_Increase_SoyoAmt(e.RowIndex);
            }
        }


        public conDataGridView CloneDataGrid(DataGridView mainDataGridView)
        {
            conDataGridView cloneDataGridView = new conDataGridView();

            if (cloneDataGridView.Columns.Count == 0)
            {
                foreach (DataGridViewColumn datagrid in mainDataGridView.Columns)
                {
                    cloneDataGridView.Columns.Add(datagrid.Clone() as DataGridViewColumn);
                }
            }

            DataGridViewRow dataRow = new DataGridViewRow();

            for (int i = 0; i < mainDataGridView.Rows.Count; i++)
            {
                dataRow = (DataGridViewRow)mainDataGridView.Rows[i].Clone();
                int Index = 0;
                foreach (DataGridViewCell cell in mainDataGridView.Rows[i].Cells)
                {
                    dataRow.Cells[Index].Value = cell.Value;
                    Index++;
                }
                cloneDataGridView.Rows.Add(dataRow);
            }
            cloneDataGridView.AllowUserToAddRows = false;
            cloneDataGridView.Refresh();


            return cloneDataGridView;
        }

        private void inputRmSoyoGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex == 20)
                {
                    if (inputRmSoyoGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null || inputRmSoyoGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Equals("") || inputRmSoyoGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Equals("0"))
                    {
                        inputRmSoyoGrid.Rows[e.RowIndex].Cells["s_USE_AMT"].Value = inputRmSoyoGrid.Rows[e.RowIndex].Cells["s_USETEMP"].Value.ToString();
                        return;
                    }
                    decimal sumTemp = decimal.Parse(inputRmSoyoGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) + decimal.Parse(inputRmSoyoGrid.Rows[e.RowIndex].Cells["s_USETEMP"].Value.ToString());
                    if (sumTemp > decimal.Parse(inputRmSoyoGrid.Rows[e.RowIndex].Cells["s_SOYO_AMT"].Value.ToString()))
                    {
                        MessageBox.Show("재고량보다 많이 소비할 수 없습니다");
                        inputRmSoyoGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                    }
                    else
                    {
                        inputRmSoyoGrid.Rows[e.RowIndex].Cells["s_USE_AMT"].Value = (decimal.Parse(inputRmSoyoGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) + decimal.Parse(inputRmSoyoGrid.Rows[e.RowIndex].Cells["s_USETEMP"].Value.ToString())).ToString("#,0.######");
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("올바른 숫자를 입력해주십시오");
                inputRmSoyoGrid.Rows[e.RowIndex].Cells["s_USE_AMT"].Value = inputRmSoyoGrid.Rows[e.RowIndex].Cells["s_USETEMP"].Value;
                inputRmSoyoGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
            }

        }

        private void ItemGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (ItemGrid.Columns[e.ColumnIndex].Name)
            {
                case "IT_EXPRT_DATE":


                    currunt_column_temp = "IT_EXPRT_DATE";
                    _Retangle = ItemGrid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    ItemGrid.CurrentCell = ItemGrid.Rows[e.RowIndex].Cells["IT_EXPRT_DATE"];
                    dtp.Size = new Size(_Retangle.Width, _Retangle.Height);
                    dtp.Location = new Point(_Retangle.X, _Retangle.Y);
                    dtp.Visible = true;

                    string mf_date = (string)ItemGrid.Rows[e.RowIndex].Cells["IT_EXPRT_DATE"].Value;
                    if (mf_date != "" && mf_date != null)
                    {
                        dtp.Text = (string)ItemGrid.Rows[e.RowIndex].Cells["IT_EXPRT_DATE"].Value;
                    }
                    else
                    {
                        dtp.Text = DateTime.Today.ToString("yyyy-MM-dd");
                    }

                    break;

                default:
                    dtp.Visible = false;
                    break;

            }

        }

        private void inputRmSoyoGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (first_touch)
            {
                MessageBox.Show("수정모드로 진입합니다. LOSS가 초기화됩니다.");
                for (int i = 0; i < ItemGrid.Rows.Count; i++)
                {
                    Cal_And_Increase_SoyoAmt(i);
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
