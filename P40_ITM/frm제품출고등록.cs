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

namespace 스마트팩토리.P40_ITM
{
    public partial class frm제품출고등록 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private DataGridView del_outGrid = new DataGridView();

        private bool bHeadCheck = false;
        private string old_cust_nm = "";

        public frm제품출고등록()
        {
            InitializeComponent();

            this.itemOutGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellEndEdit);
            this.itemOutGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grid_KeyDown);
            this.itemOutGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grid_ColumnHeaderMouseClick);

            this.itemOutGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.grid_RowsAdded);
            this.itemOutGrid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.grid_RowsRemoved);
            this.itemOutGrid.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grid_EditingControlShowing);
            this.itemOutGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellContent);

        }

        private void frm제품출고등록_Load(object sender, EventArgs e)
        {
            init_ComboBox();

            itemStockGrid.Columns["CHK"].ReadOnly = false;

            for (int i = 0; i < itemStockGrid.ColumnCount; i++)
            {
                itemStockGrid.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            del_outGrid.AllowUserToAddRows = false;

            del_outGrid.Columns.Add("LOT_NO", "LOT_NO");
            del_outGrid.Columns.Add("SEQ", "SEQ");
            del_outGrid.Columns.Add("OLD_TOTAL_AMT", "OLD_TOTAL_AMT");

            output_list(tdOutGrid, "where convert(varchar(10), A.INTIME, 120) = convert(varchar(10), getDate(), 120) ","");


            item_out_detail("");
        }

        #region item out button logic 

        private void btnNew_Click(object sender, EventArgs e)
        {
            resetSetting();
            item_out_detail(""); //제품재고 재정의
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            outputLogic();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            output_del();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCustSrch_Click(object sender, EventArgs e)
        {
            Popup.pop거래처검색 frm = new Popup.pop거래처검색();

            frm.sCustGbn = "1";
            frm.sCustNm = txt_cust_nm.Text.ToString();
            frm.ShowDialog();

            if (frm.sCode != "")
            {
                txt_cust_cd.Text = frm.sCode.Trim();
                txt_cust_nm.Text = frm.sName.Trim();
                old_cust_nm = frm.sCode.Trim();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" and D.CUST_CD = '" + txt_cust_cd.Text.ToString() + "' ");
                
                item_out_detail(sb.ToString());
            }
            else
            {
                txt_cust_cd.Text = old_cust_nm;
            }

            frm.Dispose();
            frm = null;
        }

        private void btn_move_Click(object sender, EventArgs e)
        {
            if (itemStockGrid.Rows.Count > 0)
            {
                if (chk_self_yn.Checked == true) 
                {
                    MessageBox.Show("자체출고가 체크되어 출고를 할 수 없습니다.");
                    return;
                }
                int chk = 0;
                for (int i = 0; i < itemStockGrid.Rows.Count; i++)
                {
                    if ((bool)itemStockGrid.Rows[i].Cells["CHK"].Value == true)
                    {
                        for (int j = 0; j < itemOutGrid.Rows.Count; j++) 
                        {
                            if ((string)itemStockGrid.Rows[i].Cells["LOT_NO"].Value == (string)itemOutGrid.Rows[j].Cells["O_LOT_NO"].Value 
                                && (string)itemStockGrid.Rows[i].Cells["LOT_SUB"].Value == (string)itemOutGrid.Rows[j].Cells["O_LOT_SUB"].Value)
                            {
                                MessageBox.Show("해당 출고할 제품이 이미 등록되어있습니다.");
                                return;
                            }
                        }

                        for (int k = 0; k < del_outGrid.Rows.Count; k++)
                        {
                            if ((string)itemStockGrid.Rows[i].Cells["LOT_NO"].Value == (string)del_outGrid.Rows[k].Cells["O_LOT_NO"].Value
                                && (string)itemStockGrid.Rows[i].Cells["LOT_SUB"].Value == (string)del_outGrid.Rows[k].Cells["O_LOT_SUB"].Value)
                            {
                                MessageBox.Show("해당 제품출고의 삭제데이터가 있어서 등록이 불가합니다.");
                                return;
                            }
                        }

                        itemOutGrid.Rows.Add();
                        int rowNum = itemOutGrid.Rows.Count-1;
                        itemOutGrid.Rows[rowNum].Cells["O_LOT_NO"].Value = itemStockGrid.Rows[i].Cells["LOT_NO"].Value;
                        itemOutGrid.Rows[rowNum].Cells["O_LOT_SUB"].Value = itemStockGrid.Rows[i].Cells["LOT_SUB"].Value;
                        itemOutGrid.Rows[rowNum].Cells["O_ITEM_CD"].Value = itemStockGrid.Rows[i].Cells["ITEM_CD"].Value;
                        itemOutGrid.Rows[rowNum].Cells["O_ITEM_NM"].Value = itemStockGrid.Rows[i].Cells["ITEM_NM"].Value;
                        itemOutGrid.Rows[rowNum].Cells["OUTPUT_AMT"].Value = itemStockGrid.Rows[i].Cells["STOCK_AMT"].Value;
                        itemOutGrid.Rows[rowNum].Cells["O_SPEC"].Value = itemStockGrid.Rows[i].Cells["SPEC"].Value;
                        itemOutGrid.Rows[rowNum].Cells["PRICE"].Value = itemStockGrid.Rows[i].Cells["OUTPUT_PRICE"].Value;
                        itemOutGrid.Rows[rowNum].Cells["O_UNIT_CD"].Value = itemStockGrid.Rows[i].Cells["UNIT_CD"].Value;
                        itemOutGrid.Rows[rowNum].Cells["O_UNIT_NM"].Value = itemStockGrid.Rows[i].Cells["UNIT_NM"].Value;
                        itemOutGrid.Rows[rowNum].Cells["OLD_OUT_AMT"].Value = itemStockGrid.Rows[i].Cells["STOCK_AMT"].Value;
                        double total_money = double.Parse(itemStockGrid.Rows[i].Cells["STOCK_AMT"].Value.ToString()) * double.Parse(itemStockGrid.Rows[i].Cells["OUTPUT_PRICE"].Value.ToString());
                        itemOutGrid.Rows[rowNum].Cells["TOTAL_MONEY"].Value = total_money.ToString("#,0.######");
                        itemOutGrid.Rows[rowNum].Cells["O_INPUT_DATE"].Value = itemStockGrid.Rows[i].Cells["INPUT_DATE"].Value;
                        itemOutGrid.Rows[rowNum].Cells["O_INPUT_CD"].Value = itemStockGrid.Rows[i].Cells["INPUT_CD"].Value;

                        itemOutGrid.Rows[rowNum].Cells["O_CUST_CD"].Value = itemStockGrid.Rows[i].Cells["CUST_CD"].Value;
                        itemOutGrid.Rows[rowNum].Cells["O_CUST_NM"].Value = itemStockGrid.Rows[i].Cells["CUST_NM"].Value;
                        itemOutGrid.Rows[rowNum].Cells["O_BTN_OUT"].Value = "등록";
                        

                        chk = 1;
                        wConst.f_Calc_Price(itemOutGrid, rowNum, "OUTPUT_AMT", "PRICE");
                        
                    }
                }
                if (chk == 0) 
                {
                    MessageBox.Show("발주서의 원부재료를 선택하십시기 바랍니다.");
                }
            }
            else 
            {
                MessageBox.Show("입고 데이터가 없습니다.");
            }
        }

        private void btnSrch_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            
            sb.AppendLine("where A.OUTPUT_DATE >= '" + txt_start_date.Text.ToString() + "' and  A.OUTPUT_DATE <= '" + txt_end_date.Text.ToString() + "'");

            if (cmb_cd_srch.SelectedIndex == 0)
            {
                sb2.AppendLine("");
            }
            else if (cmb_cd_srch.SelectedIndex == 1)
            {
                if (txt_srch.Text.ToString().Equals(""))
                {
                    MessageBox.Show("거래처명을  입력하시기 바랍니다.");
                    return;
                }
                sb2.AppendLine("and C.CUST_NM like '%" + txt_srch.Text.ToString() + "%' ");
            }
            else if (cmb_cd_srch.SelectedIndex == 2)
            {
                if (txt_srch.Text.ToString().Equals(""))
                {
                    MessageBox.Show("제품명을 입력하시기 바랍니다.");
                    return;
                }
                sb2.AppendLine("and B.ITEM_NM like '%" + txt_srch.Text.ToString() + "%' ");
            }
            output_list(outputGrid,sb.ToString(),sb2.ToString());
        }

        #endregion item out button logic

        #region item out local logic

        private void init_ComboBox()
        {
            ComInfo comInfo = new ComInfo();
            string sqlQuery = "";

            //창고 
            cmb_stor.ValueMember = "코드";
            cmb_stor.DisplayMember = "명칭";
            sqlQuery = comInfo.queryStorage();
            wConst.ComboBox_Read_Blank(cmb_stor, sqlQuery);

            cmb_cd_srch.Items.Add("전체 검색");
            cmb_cd_srch.Items.Add("거래처명 검색");
            cmb_cd_srch.Items.Add("제품명 검색");
            cmb_cd_srch.SelectedIndex = 0;
        }

        private void item_out_detail(string condition)
        {
            wnDm wDm = new wnDm();
            DataTable dt = null;

            dt = wDm.fn_Item_In_Stock_List(condition);

            itemStockGrid.RowCount = dt.Rows.Count;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    itemStockGrid.Rows[i].Cells["LOT_NO"].Value = dt.Rows[i]["LOT_NO"].ToString();
                    itemStockGrid.Rows[i].Cells["LOT_SUB"].Value = dt.Rows[i]["LOT_SUB"].ToString();
                    itemStockGrid.Rows[i].Cells["INPUT_DATE"].Value = dt.Rows[i]["INPUT_DATE"].ToString();
                    itemStockGrid.Rows[i].Cells["INPUT_CD"].Value = dt.Rows[i]["INPUT_CD"].ToString();
                    itemStockGrid.Rows[i].Cells["ITEM_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                    itemStockGrid.Rows[i].Cells["ITEM_NM"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                    itemStockGrid.Rows[i].Cells["INPUT_AMT"].Value = dt.Rows[i]["INPUT_AMT"].ToString();
                    itemStockGrid.Rows[i].Cells["OUT_AMT"].Value = (decimal.Parse(dt.Rows[i]["OUTPUT_AMT"].ToString())).ToString("#,0.######");
                    itemStockGrid.Rows[i].Cells["STOCK_AMT"].Value = (decimal.Parse(dt.Rows[i]["STOCK_AMT"].ToString())).ToString("#,0.######");
                    itemStockGrid.Rows[i].Cells["SPEC"].Value = dt.Rows[i]["SPEC"].ToString();
                    itemStockGrid.Rows[i].Cells["OUTPUT_PRICE"].Value = (decimal.Parse(dt.Rows[i]["OUTPUT_PRICE"].ToString())).ToString("#,0.######");
                    itemStockGrid.Rows[i].Cells["UNIT_CD"].Value = dt.Rows[i]["UNIT_CD"].ToString();
                    itemStockGrid.Rows[i].Cells["UNIT_NM"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                    itemStockGrid.Rows[i].Cells["CHK"].Value = false;

                    itemStockGrid.Rows[i].Cells["CUST_CD"].Value = dt.Rows[i]["CUST_CD"].ToString();
                    itemStockGrid.Rows[i].Cells["CUST_NM"].Value = dt.Rows[i]["CUST_NM"].ToString();

                }
            }
        }

        private void outputLogic()
        {
            try
            {
                if (cmb_stor.SelectedValue == null) cmb_stor.SelectedValue = "";

                if (chk_self_yn.Checked == false) 
                {
                    //if (txt_cust_cd.Text.ToString().Equals(""))
                    //{
                    //    MessageBox.Show("거래처를 선택하시기 바랍니다.");
                    //    return;
                    //}
                }
                
                //if (cmb_stor.SelectedValue.ToString().Equals("")) 
                //{
                //    MessageBox.Show("창고를 선택하시기 바랍니다.");
                //    return;
                //}

                if (itemOutGrid.Rows.Count == 0)
                {
                    MessageBox.Show("출고에 들어갈 제품이 1개 이상은 있어야 합니다.");
                    return;
                }

                if (lbl_out_gbn.Text != "1")
                {
                    wnDm wDm = new wnDm();
                    int rsNum = wDm.insertItemOut(
                            txt_out_date.Text.ToString(),
                            txt_cust_cd.Text.ToString(),
                            cmb_stor.SelectedValue.ToString(),
                            "N",
                            itemOutGrid);

                    if (rsNum == 0)
                    {
                        for (int i = 0; i < itemOutGrid.Rows.Count; i++) 
                        {
                            wnProcCon wDmProc = new wnProcCon();
                            int rsNum2 = wDmProc.prod_item_stock_upd((string)itemOutGrid.Rows[i].Cells["O_ITEM_CD"].Value);

                            if (rsNum2 == -9)
                            {
                                MessageBox.Show("재고 조정 실패: " + (string)itemOutGrid.Rows[i].Cells["O_ITEM_NM"].Value + " ");
                            }
                        }

                        resetSetting();
                        output_list(tdOutGrid, "where convert(varchar(10), A.INTIME, 120) = convert(varchar(10), getDate(), 120) ","");
                        output_list(outputGrid, "where A.OUTPUT_DATE >= '" + txt_start_date.Text.ToString() + "' and  A.OUTPUT_DATE <= '" + txt_end_date.Text.ToString() + "'","");

                        MessageBox.Show("저장에 성공하였습니다.");

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
                        MessageBox.Show("재고수량 초과 입력 하셨습니다. \n 체크 후 다시 저장 하시기 바랍니다.");
                    }
                    else
                        MessageBox.Show("Exception 에러");
                }
                else
                {
                    wnDm wDm = new wnDm();
                    int rsNum = wDm.updateItemOut(
                            txt_out_date.Text.ToString(),
                            txt_out_cd.Text.ToString(),
                            itemOutGrid,
                            del_outGrid);

                    if (rsNum == 0)
                    {

                        for (int i = 0; i < itemOutGrid.Rows.Count; i++)
                        {
                            wnProcCon wDmProc = new wnProcCon();
                            int rsNum2 = wDmProc.prod_item_stock_upd((string)itemOutGrid.Rows[i].Cells["O_ITEM_CD"].Value);

                            if (rsNum2 == -9)
                            {
                                MessageBox.Show("재고 조정 실패: " + (string)itemOutGrid.Rows[i].Cells["O_ITEM_NM"].Value + " ");
                            }
                        }

                        del_outGrid.Rows.Clear();
                        output_list(tdOutGrid, "where convert(varchar(10), A.INTIME, 120) = convert(varchar(10), getDate(), 120) ","");
                        output_list(outputGrid, "where A.OUTPUT_DATE >= '" + txt_start_date.Text.ToString() + "' and  A.OUTPUT_DATE <= '" + txt_end_date.Text.ToString() + "'","");

                        outputDetail2();
                        item_out_detail("");

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
            catch (Exception e) 
            {
            
            }
        }

        private void resetSetting()
        {
            lbl_out_gbn.Text = "";
            btnDelete.Enabled = false;

            txt_out_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txt_out_date.Enabled = true;
            btnCustSrch.Enabled = true;
            txt_out_cd.Text = "";
            txt_cust_cd.Text = "";
            txt_cust_nm.Text = "";
            old_cust_nm = "";

            chk_self_yn.Checked = false;

            itemOutGrid.Rows.Clear();
            itemStockGrid.Rows.Clear();
            del_outGrid.Rows.Clear();
        }

        private void output_list(DataGridView dgv, string condition,string condition2)
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Item_Output_List(condition,condition2);

                if (dt != null && dt.Rows.Count > 0)
                {
                    dgv.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dgv.Rows[i].Cells[0].Value = dt.Rows[i]["OUTPUT_DATE"].ToString();
                        dgv.Rows[i].Cells[1].Value = dt.Rows[i]["OUTPUT_CD"].ToString();
                        dgv.Rows[i].Cells[4].Value = dt.Rows[i]["CUST_CD"].ToString();
                        dgv.Rows[i].Cells[2].Value = dt.Rows[i]["CUST_NM"].ToString();
                        dgv.Rows[i].Cells[3].Value = dt.Rows[i]["ITEM_CNT"].ToString();
                        dgv.Rows[i].Cells[5].Value = dt.Rows[i]["STORAGE_CD"].ToString();
                        dgv.Rows[i].Cells[6].Value = dt.Rows[i]["STORAGE_NM"].ToString();
                        dgv.Rows[i].Cells[7].Value = dt.Rows[i]["SELF_YN"].ToString();
                        //dgv.Rows[i].Cells[4].Value = dt.Rows[i]["COMPLETE_YN"].ToString();
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                output_list(tdOutGrid, "where convert(varchar(10), A.INTIME, 120) = convert(varchar(10), getDate(), 120) ","");
            }
            else
            {
                txt_start_date.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
                output_list(outputGrid, "where A.OUTPUT_DATE >= '" + txt_start_date.Text.ToString() + "' and  A.OUTPUT_DATE <= '" + txt_end_date.Text.ToString() + "'","");
            }
        }

        private void tdOutGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ComInfo.grdHeaderNoAction(e))
            {
                out_detail(tdOutGrid, e);
            }
        }

        private void outputGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ComInfo.grdHeaderNoAction(e))
            {
                out_detail(outputGrid, e);
            }
        }

        private void out_detail(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            btnDelete.Enabled = true;
            lbl_out_gbn.Text = "1";
            txt_out_date.Enabled = false;
            txt_out_date.Text = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();

            txt_out_cd.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
            txt_cust_cd.Text = dgv.Rows[e.RowIndex].Cells[4].Value.ToString();
            txt_cust_nm.Text = dgv.Rows[e.RowIndex].Cells[2].Value.ToString();
            old_cust_nm = dgv.Rows[e.RowIndex].Cells[4].Value.ToString();
            cmb_stor.SelectedValue = dgv.Rows[e.RowIndex].Cells[5].Value.ToString();

            if (dgv.Rows[e.RowIndex].Cells[7].Value.ToString().Equals("Y"))
            {
                chk_self_yn.Checked = true;
            }
            else 
            {
                chk_self_yn.Checked = false;
            }

            btnCustSrch.Enabled = false;

            //if (dgv.Rows[e.RowIndex].Cells[4].Value.ToString().Equals("Y"))
            //{
            //    chk_input_yn.Checked = true;
            //}
            //else
            //{
            //    chk_input_yn.Checked = false;
            //}
            //inputDetail2();
            //ni_detail();
            //in_grid_detail();

            outputDetail2();
            item_out_detail("");
        }

        private void outputDetail2()
        {
            wnDm wDm = new wnDm();
            DataTable dt = null;
            dt = wDm.fn_Item_Output_Detail_List("where A.OUTPUT_DATE = '" + txt_out_date.Text.ToString() + "' and A.OUTPUT_CD = '" + txt_out_cd.Text.ToString() + "' ");

            this.itemOutGrid.RowCount = dt.Rows.Count;
            if (dt != null && dt.Rows.Count > 0)
            {
                string out_str = "";
                if (chk_self_yn.Checked == true)
                {
                    out_str = "불가";
                    //((DataGridViewButtonColumn)itemOutGrid.Columns["O_BTN_OUT"]).ReadOnly = true;

                    itemOutGrid.Columns["O_BTN_OUT"].ReadOnly = true;
                    //((DataGridViewButtonCell)itemOutGrid.Rows[i].Cells["O_BTN_OUT"]).ReadOnly = true;
                }
                else 
                {
                    itemOutGrid.Columns["O_BTN_OUT"].ReadOnly = false;
                    out_str = "등록";
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    itemOutGrid.Rows[i].Cells["SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
                    itemOutGrid.Rows[i].Cells["O_LOT_NO"].Value = dt.Rows[i]["LOT_NO"].ToString();
                    itemOutGrid.Rows[i].Cells["O_LOT_SUB"].Value = dt.Rows[i]["LOT_SUB"].ToString();
                    itemOutGrid.Rows[i].Cells["O_ITEM_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                    itemOutGrid.Rows[i].Cells["O_ITEM_NM"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                    itemOutGrid.Rows[i].Cells["OUTPUT_AMT"].Value = (decimal.Parse(dt.Rows[i]["OUTPUT_AMT"].ToString())).ToString("#,0.######");
                    itemOutGrid.Rows[i].Cells["O_SPEC"].Value = dt.Rows[i]["SPEC"].ToString();
                    itemOutGrid.Rows[i].Cells["PRICE"].Value = (decimal.Parse(dt.Rows[i]["PRICE"].ToString())).ToString("#,0.######");
                    itemOutGrid.Rows[i].Cells["O_UNIT_CD"].Value = dt.Rows[i]["UNIT_CD"].ToString();
                    itemOutGrid.Rows[i].Cells["O_UNIT_NM"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                    itemOutGrid.Rows[i].Cells["OLD_OUT_AMT"].Value = dt.Rows[i]["OUTPUT_AMT"].ToString();
                    itemOutGrid.Rows[i].Cells["TOTAL_MONEY"].Value = (decimal.Parse(dt.Rows[i]["TOTAL_MONEY"].ToString())).ToString("#,0.######");

                    itemOutGrid.Rows[i].Cells["O_INPUT_DATE"].Value = dt.Rows[i]["INPUT_DATE"].ToString();
                    itemOutGrid.Rows[i].Cells["O_INPUT_CD"].Value = dt.Rows[i]["INPUT_CD"].ToString();

                    itemOutGrid.Rows[i].Cells["O_CUST_CD"].Value = dt.Rows[i]["CUST_CD2"].ToString();
                    itemOutGrid.Rows[i].Cells["O_CUST_NM"].Value = dt.Rows[i]["CUST_NM2"].ToString();
                    itemOutGrid.Rows[i].Cells["OUT_INST_YN"].Value = dt.Rows[i]["OUT_INST_YN"].ToString();
                    if (chk_self_yn.Checked == true)
                    {
                        itemOutGrid.Rows[i].Cells["O_BTN_OUT"].Value = out_str;
                        
                    }
                    else 
                    {
                        if (dt.Rows[i]["OUT_INST_YN"].ToString().Equals("Y"))
                        {
                            itemOutGrid.Rows[i].Cells["O_BTN_OUT"].Value = "완료";
                        }
                        else 
                        {
                            itemOutGrid.Rows[i].Cells["O_BTN_OUT"].Value = out_str;
                        }
                    }
                }
            }
        }
        public static bool grdHeaderNoAction(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return false;
            }
            else return true;
        }

        private void output_del()
        {

            ComInfo comInfo = new ComInfo();
            DialogResult msgOk = comInfo.deleteConfrim("원자재입고", txt_out_date.Text.ToString() + " - " + txt_out_cd.Text.ToString());

            if (msgOk == DialogResult.No)
            {
                return;
            }

            wnDm wDm = new wnDm();
            int rsNum = wDm.deleteItemOut(txt_out_date.Text.ToString(), txt_out_cd.Text.ToString());
            if (rsNum == 0)
            {
                resetSetting();
                output_list(tdOutGrid, "where convert(varchar(10), A.INTIME, 120) = convert(varchar(10), getDate(), 120) ","");
                output_list(outputGrid, "where A.OUTPUT_DATE >= '" + txt_start_date.Text.ToString() + "' and  A.OUTPUT_DATE <= '" + txt_end_date.Text.ToString() + "'","");

                item_out_detail(""); //제품재고 재정의
                MessageBox.Show("성공적으로 삭제하였습니다.");

                //int rsNum2 = wDm.updateStRaw(inputRmGrid);

                //if (rsNum2 == 0)
                //{
                //    resetSetting();

                //    input_list(tdInputGrid, "where convert(varchar(10), A.INTIME, 120) = convert(varchar(10), getDate(), 120) ");
                //    input_list(inputGrid, "where A.INPUT_DATE >= '" + start_date.Text.ToString() + "' and  A.INPUT_DATE <= '" + end_date.Text.ToString() + "'");


                //}
                //else if (rsNum == 1)
                //{
                //    MessageBox.Show("저장에 실패하였습니다");
                //}
                //else
                //{
                //    MessageBox.Show("Exception 에러");

                //}
            }
            else if (rsNum == 1)
            {
                MessageBox.Show("삭제에 실패하였습니다.");
            }
        }
        #endregion item out local logic


        #region grid control 

        private void grid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            conDataGridView grd = (conDataGridView)sender;

            grd.Rows[e.RowIndex].Cells[0].Value = false;

            //wConst.init_RowText(grd, e.RowIndex);

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
                SendKeys.Send("{TAB}");
                e.Handled = true;
            }
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

        private void grid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            string name = itemOutGrid.CurrentCell.OwningColumn.Name;

            if (name == "OUTPUT_AMT" || name == "PRICE" || name == "TOTAL_MONEY")
            {
                e.Control.KeyPress += new KeyPressEventHandler(txtCheckNumeric_KeyPress);
            }
            else
            {
                e.Control.KeyPress -= new KeyPressEventHandler(txtCheckNumeric_KeyPress);
            }
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

        private void txtCheckNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e);
        }

        private void grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            conDataGridView grd = (conDataGridView)sender;
            DataGridViewCell cell = grd[e.ColumnIndex, e.RowIndex];

            //cell.Style.BackColor = Color.White;

            if (grd.Columns[e.ColumnIndex].ToolTipText.IndexOf("수량") >= 0
                || grd.Columns[e.ColumnIndex].ToolTipText.IndexOf("단가") >= 0
                || grd.Columns[e.ColumnIndex].ToolTipText.IndexOf("금액") >= 0)
            {

                string total_amt = (string)grd.Rows[e.RowIndex].Cells["OUTPUT_AMT"].Value;
                string price = (string)grd.Rows[e.RowIndex].Cells["PRICE"].Value;

                if (total_amt != null)
                {
                    total_amt = total_amt.ToString().Replace(" ", "");
                    if (total_amt == "")
                    {
                        grd.Rows[e.RowIndex].Cells["OUTPUT_AMT"].Value = "0";
                    }
                }
                else
                {
                    grd.Rows[e.RowIndex].Cells["OUTPUT_AMT"].Value = "0";
                }

                if (price != null)
                {
                    price = price.ToString().Replace(" ", "");
                    if (price == "")
                    {
                        grd.Rows[e.RowIndex].Cells["PRICE"].Value = "0";
                    }
                }
                else
                {
                    grd.Rows[e.RowIndex].Cells["PRICE"].Value = "0";
                }

                wConst.f_Calc_Price(grd, e.RowIndex, "OUTPUT_AMT", "PRICE");
            }
        }

        //버튼 출력 관련
        private void grid_CellContent(object sender, DataGridViewCellEventArgs e)
        {
            conDataGridView grd = (conDataGridView)sender;
            if (grd.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0) 
            {
                if (((DataGridViewButtonColumn)grd.Columns["O_BTN_OUT"]).ReadOnly == true)
                {
                    MessageBox.Show("자체 출고로 인해 출하 불가");
                }
                else 
                {
                    if ((string)grd.Rows[e.RowIndex].Cells["OUT_INST_YN"].Value != "Y") //출하 지시 내리지 않았을 경우 
                    {
                        if ((string)grd.Rows[e.RowIndex].Cells["O_INPUT_DATE"].Value != "" && (string)grd.Rows[e.RowIndex].Cells["O_INPUT_CD"].Value != "") //출고 번호가 있을 경우에만 출하지시 등록 
                        {
                            wnDm wDm = new wnDm();
                            int rsNum = wDm.insertItemOutInst(
                                    txt_out_date.Text.ToString(),
                                    txt_out_cd.Text.ToString(),
                                    (string)itemOutGrid.Rows[e.RowIndex].Cells["SEQ"].Value);

                            if (rsNum == 0)
                            {

                                itemOutGrid.Rows[e.RowIndex].Cells["O_BTN_OUT"].Value = "완료";
                                itemOutGrid.Rows[e.RowIndex].Cells["OUT_INST_YN"].Value = "Y";
                                output_list(tdOutGrid, "where convert(varchar(10), A.INTIME, 120) = convert(varchar(10), getDate(), 120) ","");
                                output_list(outputGrid, "where A.OUTPUT_DATE >= '" + txt_start_date.Text.ToString() + "' and  A.OUTPUT_DATE <= '" + txt_end_date.Text.ToString() + "'","");

                                MessageBox.Show("출하지시 완료 하였습니다.");

                            }
                        }
                        else
                        {
                            MessageBox.Show("출고번호가 없어 등록이 불가능합니다.");
                        }
                    }
                    else 
                    {
                        MessageBox.Show("출고 이미 완료");
                        
                    }
                }
            }
        }

        #endregion grid control 

        private void txt_bar_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender,e);

            if (txt_bar.Text.Length >= 10)
            {
                if (itemStockGrid.Rows.Count > 0)
                {
                    for (int i = 0; i < itemStockGrid.Rows.Count; i++)
                    {
                        string lotno = itemStockGrid.Rows[i].Cells["LOT_NO"].Value.ToString();
                        string lotsub = int.Parse(itemStockGrid.Rows[i].Cells["LOT_SUB"].Value.ToString()).ToString("000");

                        if (txt_bar.Text.ToString().Equals(lotno + lotsub))
                        {
                            for (int j = 0; j < itemOutGrid.Rows.Count; j++)
                            {
                                if ((string)itemStockGrid.Rows[i].Cells["LOT_NO"].Value == (string)itemOutGrid.Rows[j].Cells["O_LOT_NO"].Value
                                    && (string)itemStockGrid.Rows[i].Cells["LOT_SUB"].Value == (string)itemOutGrid.Rows[j].Cells["O_LOT_SUB"].Value)
                                {
                                    MessageBox.Show("해당 출고할 제품이 이미 등록되어있습니다.");
                                    return;
                                }
                            }

                            for (int k = 0; k < del_outGrid.Rows.Count; k++)
                            {
                                if ((string)itemStockGrid.Rows[i].Cells["LOT_NO"].Value == (string)del_outGrid.Rows[k].Cells["O_LOT_NO"].Value
                                    && (string)itemStockGrid.Rows[i].Cells["LOT_SUB"].Value == (string)del_outGrid.Rows[k].Cells["O_LOT_SUB"].Value)
                                {
                                    MessageBox.Show("해당 제품출고의 삭제데이터가 있어서 등록이 불가합니다.");
                                    return;
                                }
                            }

                            itemOutGrid.Rows.Add();
                            int rowNum = itemOutGrid.Rows.Count - 1;
                            itemOutGrid.Rows[rowNum].Cells["O_LOT_NO"].Value = itemStockGrid.Rows[i].Cells["LOT_NO"].Value;
                            itemOutGrid.Rows[rowNum].Cells["O_LOT_SUB"].Value = itemStockGrid.Rows[i].Cells["LOT_SUB"].Value;
                            itemOutGrid.Rows[rowNum].Cells["O_ITEM_CD"].Value = itemStockGrid.Rows[i].Cells["ITEM_CD"].Value;
                            itemOutGrid.Rows[rowNum].Cells["O_ITEM_NM"].Value = itemStockGrid.Rows[i].Cells["ITEM_NM"].Value;
                            itemOutGrid.Rows[rowNum].Cells["OUTPUT_AMT"].Value = itemStockGrid.Rows[i].Cells["STOCK_AMT"].Value;
                            itemOutGrid.Rows[rowNum].Cells["O_SPEC"].Value = itemStockGrid.Rows[i].Cells["SPEC"].Value;
                            itemOutGrid.Rows[rowNum].Cells["PRICE"].Value = itemStockGrid.Rows[i].Cells["OUTPUT_PRICE"].Value;
                            itemOutGrid.Rows[rowNum].Cells["O_UNIT_CD"].Value = itemStockGrid.Rows[i].Cells["UNIT_CD"].Value;
                            itemOutGrid.Rows[rowNum].Cells["O_UNIT_NM"].Value = itemStockGrid.Rows[i].Cells["UNIT_NM"].Value;
                            itemOutGrid.Rows[rowNum].Cells["OLD_OUT_AMT"].Value = itemStockGrid.Rows[i].Cells["STOCK_AMT"].Value;
                            double total_money = double.Parse(itemStockGrid.Rows[i].Cells["STOCK_AMT"].Value.ToString()) * double.Parse(itemStockGrid.Rows[i].Cells["OUTPUT_PRICE"].Value.ToString());
                            itemOutGrid.Rows[rowNum].Cells["TOTAL_MONEY"].Value = total_money.ToString("#,0.######");

                            itemOutGrid.Rows[rowNum].Cells["O_INPUT_DATE"].Value = itemStockGrid.Rows[i].Cells["INPUT_DATE"].Value;
                            itemOutGrid.Rows[rowNum].Cells["O_INPUT_CD"].Value = itemStockGrid.Rows[i].Cells["INPUT_CD"].Value;
                            itemOutGrid.Rows[rowNum].Cells["O_CUST_CD"].Value = itemStockGrid.Rows[i].Cells["CUST_CD"].Value;
                            itemOutGrid.Rows[rowNum].Cells["O_CUST_NM"].Value = itemStockGrid.Rows[i].Cells["CUST_NM"].Value;
                            // chk = 1;
                            wConst.f_Calc_Price(itemOutGrid, rowNum, "OUTPUT_AMT", "PRICE");

                            txt_bar.Text = "";
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("입고 데이터가 없습니다.");
                }
            }
        }

        private void btnCustSrch2_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" and E.CUST_NM LIKE '%" + txt_cust_nm2.Text.ToString() + "%' ");

            item_out_detail(sb.ToString());
        }

    }
}