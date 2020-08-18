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
    public partial class frm씨지엠작업지시서등록 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private DataGridView del_inputGrid = new DataGridView();
        private DateTimePicker dtp = new DateTimePicker();
        private Rectangle _Retangle;

        private string old_cust_nm = "";
        private bool bHeadCheck = false;
        private ComInfo comInfo = new ComInfo();

        private string currunt_column_temp = "";


        public frm씨지엠작업지시서등록()
        {
            InitializeComponent();

            this.inputRmGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellEndEdit);
            this.inputRmGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grid_KeyDown);
            this.inputRmGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grid_ColumnHeaderMouseClick);

            this.inputRmGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.grid_RowsAdded);
            this.inputRmGrid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.grid_RowsRemoved);
            this.inputRmGrid.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grid_EditingControlShowing);

            this.inputRmGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellClick);
            this.inputRmGrid.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.grid_ColumnWidthChanged);
            this.inputRmGrid.Scroll += new System.Windows.Forms.ScrollEventHandler(this.grid_scroll);

            this.inputRmGrid.Controls.Add(dtp);
            dtp.Visible = false;
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.TextChanged += new EventHandler(dtp_TextChange);
        }

        private void frm씨지엠작업지시서등록_Load(object sender, EventArgs e)
        {
            input_list(tdInputGrid, "where convert(varchar(10), INTIME, 120) = convert(varchar(10), getDate(), 120) ");
            
            n_in_ord_grid.Columns["CHK"].ReadOnly = false;

            //for (int i = 0; i < n_in_ord_grid.ColumnCount; i++) {
            //    n_in_ord_grid.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //}

            ComInfo.gridHeaderSet(inputRmGrid);
            ComInfo.gridHeaderSet(inputRmSoyoGrid);
            ComInfo.gridHeaderSet(inputItemGrid);
            ComInfo.gridHeaderSet(tdInputGrid);
            ComInfo.gridHeaderSet(inputGrid);


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
            input_list(inputGrid, "where W_INST_DATE >= '" + start_date.Text.ToString() + "' and  W_INST_DATE <= '" + end_date.Text.ToString() + "'");
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
                            
                            if(totalAmtTemp / boxAmtTemp >= 1){
                                inputRmGrid.Rows[rowNum].Cells["TOTAL_AMT"].Value = boxAmtTemp.ToString();
                                totalAmtTemp -= boxAmtTemp;
                            }else{
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


        private void grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            conDataGridView grd = (conDataGridView)sender;
            DataGridViewCell cell = grd[e.ColumnIndex, e.RowIndex];

            cell.Style.BackColor = Color.White;

            if (grd.Rows[e.RowIndex].Cells["TOTAL_MONEY"].Value == null || grd.Rows[e.RowIndex].Cells["TOTAL_MONEY"].Value.Equals(""))
            {
                grd.Rows[e.RowIndex].Cells["TOTAL_MONEY"].Value = "0";
            }
            if (grd.Rows[e.RowIndex].Cells["TOTAL_AMT"].Value == null || grd.Rows[e.RowIndex].Cells["TOTAL_AMT"].Value.Equals(""))
            {
                grd.Rows[e.RowIndex].Cells["TOTAL_AMT"].Value = "0";
            }
            if (grd.Rows[e.RowIndex].Cells["PRICE"].Value == null || grd.Rows[e.RowIndex].Cells["PRICE"].Value.Equals(""))
            {
                grd.Rows[e.RowIndex].Cells["PRICE"].Value = "0";
            }

            #region 공통 그리드 체크
            if (grd.Columns[e.ColumnIndex].ToolTipText.IndexOf("명칭") >= 0 && grd._KeyInput == "enter")
            {
                string rat_mat_nm = (string)grd.Rows[e.RowIndex].Cells["RAW_MAT_NM"].Value;
                wnDm wDm = new wnDm();
                DataTable dt = new DataTable();
                dt = wDm.fn_Raw_List("where RAW_MAT_NM like '%" + rat_mat_nm + "%' ");

                if (dt.Rows.Count > 1)
                { //row가 2줄이 넘을 경우 팝업으로 넘어간다.

                    wConst.call_pop_raw_mat(grd, dt, e.RowIndex, rat_mat_nm, 2);
                    //orderGridAdd();
                    
                }
                else if (dt.Rows.Count == 1) //row가 1일 경우 해당 row에 값을 자동 입력한다.
                {

                    grd.Rows[e.RowIndex].Cells["RAW_MAT_CD"].Value = dt.Rows[0]["RAW_MAT_CD"].ToString();
                    grd.Rows[e.RowIndex].Cells["RAW_MAT_NM"].Value = dt.Rows[0]["RAW_MAT_NM"].ToString();
                    grd.Rows[e.RowIndex].Cells["OLD_RAW_MAT_NM"].Value = dt.Rows[0]["RAW_MAT_NM"].ToString();
                    grd.Rows[e.RowIndex].Cells["SPEC"].Value = dt.Rows[0]["SPEC"].ToString();
                    grd.Rows[e.RowIndex].Cells["UNIT_CD"].Value = dt.Rows[0]["INPUT_UNIT"].ToString();
                    grd.Rows[e.RowIndex].Cells["UNIT_NM"].Value = dt.Rows[0]["INPUT_UNIT_NM"].ToString();
                    grd.Rows[e.RowIndex].Cells["PRICE"].Value = dt.Rows[0]["INPUT_PRICE"].ToString();
                    grd.Rows[e.RowIndex].Cells["CHUGJONG_CD"].Value = dt.Rows[0]["CHUGJONG_CD"].ToString();
                    grd.Rows[e.RowIndex].Cells["CHUGJONG_NM"].Value = dt.Rows[0]["CHUGJONG_NM"].ToString();
                    grd.Rows[e.RowIndex].Cells["CLASS_NM"].Value = dt.Rows[0]["CLASS_NM"].ToString();
                    grd.Rows[e.RowIndex].Cells["CLASS_CD"].Value = dt.Rows[0]["CLASS_CD"].ToString();
                    grd.Rows[e.RowIndex].Cells["COUNTRY"].Value = dt.Rows[0]["COUNTRY_NM"].ToString();
                    grd.Rows[e.RowIndex].Cells["COUNTRY_CD"].Value = dt.Rows[0]["COUNTRY_CD"].ToString();
                    grd.Rows[e.RowIndex].Cells["GRADE_NM"].Value = dt.Rows[0]["GRADE_NM"].ToString();
                    grd.Rows[e.RowIndex].Cells["GRADE_CD"].Value = dt.Rows[0]["GRADE_CD"].ToString();
                    grd.Rows[e.RowIndex].Cells["EXPRT_COUNT"].Value = dt.Rows[0]["EXPRT_COUNT"].ToString();
                    grd.Rows[e.RowIndex].Cells["RAW_MAT_GUBUN_NM"].Value = dt.Rows[0]["RAW_MAT_GUBUN_NM"].ToString();
                    grd.Rows[e.RowIndex].Cells["RAW_MAT_GUBUN"].Value = dt.Rows[0]["RAW_MAT_GUBUN"].ToString();
                    grd.Rows[e.RowIndex].Cells["TYPE_NM"].Value = dt.Rows[0]["TYPE_NM"].ToString();
                    grd.Rows[e.RowIndex].Cells["TYPE_CD"].Value = dt.Rows[0]["TYPE_CD"].ToString();
                    grd.Rows[e.RowIndex].Cells["BOX_AMT"].Value = dt.Rows[0]["BOX_AMT"].ToString();
                    grd.Rows[e.RowIndex].Cells["LABEL_NM"].Value = dt.Rows[0]["LABEL_NM"].ToString();
                    DataGridViewComboBoxCell cmbTemp = (DataGridViewComboBoxCell)grd.Rows[e.RowIndex].Cells["GRADE_NM"];
                    cmbTemp.Value = dt.Rows[0]["GRADE_CD"].ToString();

                    if (dt.Rows[0]["RAW_MAT_GUBUN"].ToString().Equals("1"))
                    {
                        DataGridViewCellStyle style = new DataGridViewCellStyle();
                        style.BackColor = Color.White;

                        grd.Rows[e.RowIndex].Cells["UNION_CD"].Style = style;
                        grd.Rows[e.RowIndex].Cells["GRADE_NM"].Style = style;
                        grd.Rows[e.RowIndex].Cells["MF_DATE"].Style = style;
                        grd.Rows[e.RowIndex].Cells["EXPRT_DATE"].Style = style;
                        grd.Rows[e.RowIndex].Cells["GRADE_NM"].Style = style;
                        grd.Rows[e.RowIndex].Cells["TOTAL_MONEY"].Style = style;
                        grd.Rows[e.RowIndex].Cells["UNIT_NM"].Style = style;
                        grd.Rows[e.RowIndex].Cells["CHUGJONG_NM"].Style = style;
                        grd.Rows[e.RowIndex].Cells["CLASS_NM"].Style = style;
                        grd.Rows[e.RowIndex].Cells["COUNTRY"].Style = style;
                        grd.Rows[e.RowIndex].Cells["TYPE_NM"].Style = style;
                        grd.Rows[e.RowIndex].Cells["LABEL_NM"].Style = style;
                        grd.Rows[e.RowIndex].Cells["RAW_MAT_GUBUN_NM"].Style = style;
                        grd.Rows[e.RowIndex].Cells["EXPRT_DATE"].Style = style;
                        grd.Rows[e.RowIndex].Cells["TOTAL_AMT"].Style = style;
                        grd.Rows[e.RowIndex].Cells["TOTAL_MONEY"].Style = style;


                        grd.Rows[e.RowIndex].Cells["UNION_CD"].ReadOnly = false;
                        grd.Rows[e.RowIndex].Cells["GRADE_NM"].ReadOnly = false;
                        grd.Rows[e.RowIndex].Cells["TOTAL_AMT"].ReadOnly = false;
                        grd.Rows[e.RowIndex].Cells["TOTAL_MONEY"].ReadOnly = false;
                    }
                    else
                    {
                        DataGridViewCellStyle style = new DataGridViewCellStyle();
                        style.BackColor = Color.White;

                        grd.Rows[e.RowIndex].Cells["TOTAL_AMT"].Style = style;
                        grd.Rows[e.RowIndex].Cells["TOTAL_MONEY"].Style = style;
                        grd.Rows[e.RowIndex].Cells["UNIT_NM"].Style = style;
                        grd.Rows[e.RowIndex].Cells["RAW_MAT_GUBUN_NM"].Style = style;
                        grd.Rows[e.RowIndex].Cells["TOTAL_AMT"].Style = style;
                        grd.Rows[e.RowIndex].Cells["TOTAL_MONEY"].Style = style;
                        
                        grd.Rows[e.RowIndex].Cells["UNION_CD"].ReadOnly = true;
                        grd.Rows[e.RowIndex].Cells["GRADE_NM"].ReadOnly = true;
                        grd.Rows[e.RowIndex].Cells["TOTAL_AMT"].ReadOnly = false;
                        grd.Rows[e.RowIndex].Cells["TOTAL_MONEY"].ReadOnly = false;

                    }
                    
                    //inputGridAdd();
                }
                else
                { //row가 없는 경우
                    MessageBox.Show("데이터가 없습니다.");
                }

                //wConst.f_Calc_Price(grd, e.RowIndex, "TOTAL_AMT", "PRICE");
                wConst.f_Calc_PriceAndBox(grd, e.RowIndex, "TOTAL_AMT", "PRICE", "BOX_AMT");
                if (grd.RowCount > 0)
                    grd.Rows.RemoveAt(1);
                //wConst.f_Calc_Box(grd, e.RowIndex, "TOTAL_AMT", "BOX_AMT");
                
            }


            if (grd.Columns[e.ColumnIndex].ToolTipText.IndexOf("수량") >= 0
                || grd.Columns[e.ColumnIndex].ToolTipText.IndexOf("단가") >= 0
                || grd.Columns[e.ColumnIndex].ToolTipText.IndexOf("금액") >= 0)
            {

                string total_amt = (string)grd.Rows[e.RowIndex].Cells["TOTAL_AMT"].Value;
                string input_price = (string)grd.Rows[e.RowIndex].Cells["PRICE"].Value;
                string box_amt = (string)grd.Rows[e.RowIndex].Cells["BOX_AMT"].Value;

                if (total_amt != null)
                {
                    total_amt = total_amt.ToString().Replace(" ", "");
                    if (total_amt == "")
                    {
                        grd.Rows[e.RowIndex].Cells["TOTAL_AMT"].Value = "0";
                    }
                }
                else
                {
                    grd.Rows[e.RowIndex].Cells["TOTAL_AMT"].Value = "0";
                }


                if (input_price != null)
                {
                    input_price = input_price.ToString().Replace(" ", "");
                    if (input_price == "")
                    {
                        grd.Rows[e.RowIndex].Cells["PRICE"].Value = "0";
                    }
                }

                if (box_amt != null)
                {
                    box_amt = box_amt.ToString().Replace(" ", "");
                    if (box_amt == "")
                    {
                        grd.Rows[e.RowIndex].Cells["BOX_AMT"].Value = "0";
                    }
                }
                else
                {
                    grd.Rows[e.RowIndex].Cells["BOX_AMT"].Value = "0";
                }

                //wConst.f_Calc_Price(grd, e.RowIndex, "TOTAL_AMT", "PRICE");
                wConst.f_Calc_PriceAndBox(grd, e.RowIndex, "TOTAL_AMT", "PRICE", "BOX_AMT");
            }
            #endregion 공통 그리드 체크

            //string sSearchTxt = "" + (string)grd.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            
        }

        private void btnCustSrch_Click(object sender, EventArgs e)
        {
            Popup.pop거래처검색 frm = new Popup.pop거래처검색();

            frm.sCustGbn = "2";
            frm.sCustNm = txt_cust_nm.Text.ToString();
            frm.ShowDialog();

            if (frm.sCode != "")
            {
                txt_cust_cd.Text = frm.sCode.Trim();
                txt_cust_nm.Text = frm.sName.Trim();
                old_cust_nm = frm.sCode.Trim();
                ni_detail();
                in_grid_detail();
            }
            else
            {
                txt_cust_cd.Text = old_cust_nm;
            }

            frm.Dispose();
            frm = null;
        }

        private void btn_minus_Click(object sender, EventArgs e)
        {
            minus_logic(inputRmGrid);
        }

        #endregion button logic

        #region input logic
        private void resetSetting()
        {
            lbl_input_gbn.Text = "";
            btnDelete.Enabled = false;

            txt_work_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txt_work_date.Enabled = true;
            end_req_date.Text = "";
            txt_work_cd.Text = "";
            txt_inst_notice.Text = "";

            inputRmGrid.Rows.Clear();
            inputGridAdd();

            inputRmSoyoGrid.Rows.Clear();
            inputItemGrid.Rows.Clear();

            //del_orderGrid.Rows.Clear();
        }

        private void inputLogic() 
        {

            int cnt = 0;
            int grid_cnt = inputRmGrid.Rows.Count;
            

            if (inputRmGrid.Rows.Count == 0) 
            {
                MessageBox.Show("입고에 들어갈 원부재료가 1개 이상은 있어야 합니다.");
                return;
            }

            

            for (int i = 0; i < inputRmGrid.RowCount; i++)
            {
                if (inputRmGrid.Rows[i].Cells["TOTAL_AMT"].Value == null || inputRmGrid.Rows[i].Cells["TOTAL_AMT"].Value.ToString().Equals("") || double.Parse(inputRmGrid.Rows[i].Cells["TOTAL_AMT"].Value.ToString()) <=0)
                {
                    MessageBox.Show("사용 원재료 수량은 0 이하일 수 없습니다.");
                    return;
                }

            }

            

            //string input_yn = comInfo.resultYn(chk_input_yn);
            if (lbl_input_gbn.Text != "1")
            {
                


                wnDm wDm = new wnDm();
                int rsNum = wDm.insert_Work_Inst(
                        txt_work_date.Text.ToString(),
                        end_req_date.Text.ToString(),
                        txt_inst_notice.Text.ToString(),
                        inputRmGrid);

                if (rsNum == 0)
                {
                        resetSetting();
                        input_list(tdInputGrid, "where convert(varchar(10),INTIME, 120) = convert(varchar(10), getDate(), 120) ");
                        input_list(inputGrid, "where W_INST_DATE >= '" + start_date.Text.ToString() + "' and  W_INST_DATE <= '" + end_date.Text.ToString() + "'");
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
                if (txt_complete_yn.Text.Equals("대기"))
                {
                    wnDm wDm = new wnDm();
                    int rsNum = wDm.update_Work_Inst(
                            txt_work_date.Text.ToString(),
                            txt_work_cd.Text.ToString(),
                            end_req_date.Text.ToString(),
                            txt_inst_notice.Text.ToString(),
                            inputRmGrid);

                    if (rsNum == 0)
                    {

                        input_list(tdInputGrid, "where convert(varchar(10),INTIME, 120) = convert(varchar(10), getDate(), 120) ");
                        input_list(inputGrid, "where W_INST_DATE >= '" + start_date.Text.ToString() + "' and  W_INST_DATE <= '" + end_date.Text.ToString() + "'");

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
                else
                {
                    MessageBox.Show("이미 작업중 혹은 완료된 작업지시는 수정할 수 없습니다.");
                    return;
                }
            }
        }

        private void input_list(DataGridView dgv, string condition)
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Inst_list(condition);

                if (dt != null && dt.Rows.Count > 0)
                {
                    dgv.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if(dt.Rows[i]["COMPLETE_YN"].ToString().Equals("3")){
                            dgv.Rows[i].Cells[2].Value = "완료";
                        }
                        else if (dt.Rows[i]["COMPLETE_YN"].ToString().Equals("2"))
                        {
                            dgv.Rows[i].Cells[2].Value = "진행중";
                        }
                        else
                        {
                            dgv.Rows[i].Cells[2].Value = "대기";
                        }
                        dgv.Rows[i].Cells[0].Value = dt.Rows[i]["W_INST_DATE"].ToString();
                        dgv.Rows[i].Cells[1].Value = dt.Rows[i]["W_INST_CD"].ToString();
                        
                        dgv.Rows[i].Cells[3].Value = dt.Rows[i]["INST_NOTICE"].ToString();
                        dgv.Rows[i].Cells[4].Value = dt.Rows[i]["DELIVERY_DATE"].ToString();
                        dgv.Rows[i].Cells[5].Value = dt.Rows[i]["RAW_MAT_CD"].ToString();
                        dgv.Rows[i].Cells[6].Value = dt.Rows[i]["INST_AMT"].ToString();
                        dgv.Rows[i].Cells[7].Value = dt.Rows[i]["LOT_NO"].ToString();
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
            lbl_input_gbn.Text = "1";
            txt_work_date.Enabled = false;
            

            txt_work_date.Text = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();

            txt_work_cd.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
            //체크 = dgv.Rows[e.RowIndex].Cells[2].Value.ToString();\
            txt_inst_notice.Text = dgv.Rows[e.RowIndex].Cells[3].Value.ToString();
            lbl_raw_mat_cd.Text = dgv.Rows[e.RowIndex].Cells[5].Value.ToString();
            lbl_total_amt.Text = dgv.Rows[e.RowIndex].Cells[6].Value.ToString();
            end_req_date.Text = dgv.Rows[e.RowIndex].Cells[4].Value.ToString();
            txt_complete_yn.Text = dgv.Rows[e.RowIndex].Cells[2].Value.ToString();
            txt_lot_no.Text = dgv.Rows[e.RowIndex].Cells[7].Value.ToString();
            
            inputDetail();
            inputDetail2();
            inputDetail3();
            
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

                    if (txt_complete_yn.Text.Equals("대기"))
                    {
                        inputRmGrid.Rows[0].Cells["RAW_MAT_NM"].ReadOnly = false;
                        inputRmGrid.Rows[0].Cells["TOTAL_AMT"].ReadOnly = false;
                    }
                    else
                    {
                        inputRmGrid.Rows[0].Cells["RAW_MAT_NM"].ReadOnly = true;
                        inputRmGrid.Rows[0].Cells["TOTAL_AMT"].ReadOnly = true;
                    }
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

            dt = wDm.fn_Work_Inst_detail("where A.W_INST_DATE = '" + txt_work_date.Text.ToString() + "' and A.W_INST_CD = '" + txt_work_cd.Text.ToString() + "' ");

            dtp.Visible = false;
            inputRmSoyoGrid.Rows.Clear();
            inputRmSoyoGrid.RowCount = dt.Rows.Count;
            //this.inputRmGrid.RowCount = dt.Rows.Count;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //string t_amt = string.Format("{0:#.##}", 100.2);
                    inputRmSoyoGrid.Rows[i].Cells["s_SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
                    //inputRmGrid.Rows[i].Cells["ORDER_DATE"].Value = dt.Rows[i]["ORDER_DATE"].ToString();
                    //inputRmGrid.Rows[i].Cells["ORDER_CD"].Value = dt.Rows[i]["ORDER_CD"].ToString();
                    //inputRmGrid.Rows[i].Cells["ORDER_SEQ"].Value = dt.Rows[i]["ORDER_SEQ"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_INPUT_DATE"].Value = dt.Rows[i]["INPUT_DATE"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_INPUT_CD"].Value = dt.Rows[i]["INPUT_CD"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_RAW_MAT_NM"].Value = dt.Rows[i]["RAW_MAT_NM"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_RAW_MAT_CD"].Value = dt.Rows[i]["RAW_MAT_CD"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_UNIT_CD"].Value = dt.Rows[i]["UNIT_CD"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_UNIT_NM"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_TOTAL_AMT"].Value = (decimal.Parse(dt.Rows[i]["SOYO_AMT"].ToString())).ToString("#,0.######");
                    inputRmSoyoGrid.Rows[i].Cells["s_MF_DATE"].Value = dt.Rows[i]["MF_DATE"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_EXPRT_DATE"].Value = dt.Rows[i]["EXPRT_DATE"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_UNION_CD"].Value = dt.Rows[i]["UNION_CD"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_CHUGJONG_CD"].Value = dt.Rows[i]["CHUGJONG_CD"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_CLASS_CD"].Value = dt.Rows[i]["CLASS_CD"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_COUNTRY_CD"].Value = dt.Rows[i]["COUNTRY_CD"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_GRADE_CD"].Value = dt.Rows[i]["GRADE_CD"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_CHUGJONG_NM"].Value = dt.Rows[i]["CHUGJONG_NM"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_CLASS_NM"].Value = dt.Rows[i]["CLASS_NM"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_COUNTRY_NM"].Value = dt.Rows[i]["COUNTRY_NM"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_GRADE_NM"].Value = dt.Rows[i]["GRADE_CD"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_LABEL_NM"].Value = dt.Rows[i]["LABEL_NM"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_TYPE_CD"].Value = dt.Rows[i]["TYPE_CD"].ToString();
                    inputRmSoyoGrid.Rows[i].Cells["s_TYPE_NM"].Value = dt.Rows[i]["TYPE_NM"].ToString();
                }
            }
        }

        private void inputDetail3()
        {
            wnDm wDm = new wnDm();
            DataTable dt = null;

            dt = wDm.fn_Item_Input_Detail_CZM("where C.LOT_NO = '" + txt_lot_no.Text.ToString() + "' ");

            dtp.Visible = false;
            inputItemGrid.Rows.Clear();
            inputItemGrid.RowCount = dt.Rows.Count;
            //this.inputRmGrid.RowCount = dt.Rows.Count;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //string t_amt = string.Format("{0:#.##}", 100.2);
                    inputItemGrid.Rows[i].Cells["IT_SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
                    //inputRmGrid.Rows[i].Cells["ORDER_DATE"].Value = dt.Rows[i]["ORDER_DATE"].ToString();
                    //inputRmGrid.Rows[i].Cells["ORDER_CD"].Value = dt.Rows[i]["ORDER_CD"].ToString();
                    //inputRmGrid.Rows[i].Cells["ORDER_SEQ"].Value = dt.Rows[i]["ORDER_SEQ"].ToString();
                    inputItemGrid.Rows[i].Cells["IT_INPUT_DATE"].Value = dt.Rows[i]["INPUT_DATE"].ToString();
                    inputItemGrid.Rows[i].Cells["IT_INPUT_CD"].Value = dt.Rows[i]["INPUT_CD"].ToString();
                    inputItemGrid.Rows[i].Cells["IT_ITEM_NM"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                    inputItemGrid.Rows[i].Cells["IT_UNIT_NM"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                    inputItemGrid.Rows[i].Cells["IT_EXPRT_DATE"].Value = dt.Rows[i]["EXPRT_DATE"].ToString();
                    inputItemGrid.Rows[i].Cells["IT_A_UNION_CD"].Value = dt.Rows[i]["A_UNION_CD"].ToString();
                    inputItemGrid.Rows[i].Cells["IT_B_UNION_CD"].Value = dt.Rows[i]["B_UNION_CD"].ToString();
                    inputItemGrid.Rows[i].Cells["IT_CHUGJONG_NM"].Value = dt.Rows[i]["CHUGJONG_NM"].ToString();
                    inputItemGrid.Rows[i].Cells["IT_CLASS_NM"].Value = dt.Rows[i]["CLASS_NM"].ToString();
                    inputItemGrid.Rows[i].Cells["IT_INPUT_AMT"].Value = decimal.Parse(dt.Rows[i]["INPUT_AMT"].ToString()).ToString("#,0.######");
                    inputItemGrid.Rows[i].Cells["IT_COUNTRY_NM"].Value = dt.Rows[i]["COUNTRY_NM"].ToString();
                    inputItemGrid.Rows[i].Cells["IT_LABEL_NM"].Value = dt.Rows[i]["LABEL_NM"].ToString();
                    inputItemGrid.Rows[i].Cells["IT_TYPE_NM"].Value = dt.Rows[i]["TYPE_NM"].ToString();
                }
            }
        }

        private void input_del() 
        {

            ComInfo comInfo = new ComInfo();

            bool isRawMoved = false;


            DialogResult msgOk = comInfo.deleteConfrim("작업지시", txt_work_date.Text.ToString() + " - " + txt_work_cd.Text.ToString());

            if (msgOk == DialogResult.No)
            {
                return;
            }

            if (txt_complete_yn.Text.Equals("완료"))
            {
                MessageBox.Show("이미 완료된 작업은 삭제할 수 없습니다.");
                return;
            }


            wnDm wDm = new wnDm();
            DataTable dt = wDm.fn_Work_Inst_detail("WHERE W_INST_DATE = '" + txt_work_date.Text + "'  AND W_INST_CD = '" + txt_work_cd.Text + "'   ");
            if (dt != null && dt.Rows.Count > 0)
            {
                MessageBox.Show("이미 이동된 원재료가 있습니다. 이동 수량은 기존 위치로 돌아갑니다. ");
                isRawMoved = true;
            }





            int rsNum = wDm.deleteWork(txt_work_date.Text.ToString(), txt_work_cd.Text.ToString(), isRawMoved);


            if (rsNum == 0)
            {
                MessageBox.Show("삭제에 성공하였습니다.");
                resetSetting();

                input_list(tdInputGrid, "where convert(varchar(10), INTIME, 120) = convert(varchar(10), getDate(), 120) ");
                input_list(inputGrid, "where W_INST_DATE >= '" + start_date.Text.ToString() + "' and  W_INST_DATE <= '" + end_date.Text.ToString() + "'");    
            }
            else if (rsNum == 1)
            {
                MessageBox.Show("삭제에 실패하였습니다.");
            }
            else if (rsNum == 9)
            {
                MessageBox.Show("Exception Error");
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

            if (name == "TOTAL_AMT" )
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
                input_list(tdInputGrid, "where convert(varchar(10), INTIME, 120) = convert(varchar(10), getDate(), 120) ");
            }
            else 
            {
                start_date.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
                input_list(inputGrid, "where W_INST_DATE >= '" + start_date.Text.ToString() + "' and  W_INST_DATE <= '" + end_date.Text.ToString() + "'");
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
                
                if ((!(dgv.Rows[dgv.CurrentCell.RowIndex].ToString()).Equals("")) && (dgv.Rows[dgv.CurrentCell.RowIndex] != null))
                {
                    del_inputGrid.Rows.Add();

                    del_inputGrid.Rows[del_inputGrid.Rows.Count - 1].Cells["INPUT_DATE"].Value = del_inputGrid.Text.ToString();
                    del_inputGrid.Rows[del_inputGrid.Rows.Count - 1].Cells["INPUT_CD"].Value = del_inputGrid.Text.ToString();
                    del_inputGrid.Rows[del_inputGrid.Rows.Count - 1].Cells["SEQ"].Value = dgv.SelectedRows[0].Cells["SEQ"].Value;
                    del_inputGrid.Rows[del_inputGrid.Rows.Count - 1].Cells["ORDER_DATE"].Value = dgv.SelectedRows[0].Cells["ORDER_DATE"].Value;
                    del_inputGrid.Rows[del_inputGrid.Rows.Count - 1].Cells["ORDER_CD"].Value = dgv.SelectedRows[0].Cells["ORDER_CD"].Value;
                    del_inputGrid.Rows[del_inputGrid.Rows.Count - 1].Cells["ORDER_SEQ"].Value = dgv.SelectedRows[0].Cells["ORDER_SEQ"].Value;
                    del_inputGrid.Rows[del_inputGrid.Rows.Count - 1].Cells["RAW_MAT_CD"].Value = dgv.SelectedRows[0].Cells["RAW_MAT_CD"].Value;
                    del_inputGrid.Rows[del_inputGrid.Rows.Count - 1].Cells["OLD_TOTAL_AMT"].Value = dgv.SelectedRows[0].Cells["OLD_TOTAL_AMT"].Value;

                }

                dgv.Rows.RemoveAt(dgv.SelectedRows[0].Index);
                if (dgv.Rows.Count > 0) {
                    dgv.CurrentCell = dgv[2, dgv.Rows.Count - 1];
                }
            }

        }

        private void dtp_TextChange(object sender, EventArgs e) 
        {
            DateTime dateTemp = DateTime.Today;
            inputRmGrid.Rows[inputRmGrid.CurrentCell.RowIndex].Cells[currunt_column_temp].Value = dtp.Text.ToString();
            if (cmb_exprt_auto.SelectedValue.Equals("Y") && currunt_column_temp.Equals("MF_DATE"))
            {
                string exprtTemp = cmb_exprt_count.SelectedValue.ToString();
                Console.WriteLine(exprtTemp);
                if (exprtTemp.Contains("M"))
                {
                    dateTemp = DateTime.Parse(dtp.Text.ToString()).AddMonths(int.Parse(exprtTemp.Replace("M", "")));
                }
                else
                {
                    dateTemp = DateTime.Parse(dtp.Text.ToString()).AddDays(int.Parse(exprtTemp.Replace("D", "")));
                }
                inputRmGrid.Rows[inputRmGrid.CurrentCell.RowIndex].Cells["EXPRT_DATE"].Value = dateTemp.ToString("yyyy-MM-dd");
            }
            
        }

        private void btn_plus_Click(object sender, EventArgs e)
        {
            //inputGridAdd();
        }

        private void inputGridAdd()
        {
            
                inputRmGrid.Rows.Add();
                inputRmGrid.Rows[inputRmGrid.Rows.Count - 1].Cells["TOTAL_AMT"].Value = "0";
                inputRmGrid.Rows[inputRmGrid.Rows.Count - 1].Cells["PRICE"].Value = "0";
                inputRmGrid.Rows[inputRmGrid.Rows.Count - 1].Cells["TOTAL_MONEY"].Value = "0";
          
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
