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

namespace 스마트팩토리.P10_PLN
{
    public partial class frm생산계획등록 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private DataGridView del_planGrid = new DataGridView();
        private DataGridView del_HalfGrid = new DataGridView();
        private ComInfo comInfo = new ComInfo();
        private string old_cust_nm = "";

        private DataGridView pHalfGrid = new DataGridView();

        public frm생산계획등록()
        {
            InitializeComponent();

            this.itemPlanGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellEndEdit);
            this.itemPlanGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grid_KeyDown);
            this.itemPlanGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grid_ColumnHeaderMouseClick);
            this.itemPlanGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.grid_RowsAdded);
            this.itemPlanGrid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.grid_RowsRemoved);
            this.itemPlanGrid.CellValueChanged += new DataGridViewCellEventHandler(grid_CellValueChanged);
            this.itemPlanGrid.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.grid_ColumnWidthChanged);
            this.itemPlanGrid.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grid_EditingControlShowing);
        }

        private void frm생산계획등록_Load(object sender, EventArgs e)
        {
            //DateTime today = DateTime.Today.AddMonths(-1);

            start_date.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");

            itemPlanGridAdd();

            plan_list(planGrid, "where A.PLAN_DATE >= '" + start_date.Text.ToString() + "' and  A.PLAN_DATE <= '" + end_date.Text.ToString() + "'");

            del_planGrid.AllowUserToAddRows = false;

            del_planGrid.Columns.Add("PLAN_DATE", "PLAN_DATE");
            del_planGrid.Columns.Add("PLAN_CD", "PLAN_CD");
            del_planGrid.Columns.Add("SEQ", "SEQ");

            del_HalfGrid.AllowUserToAddRows = false;

            del_HalfGrid.Columns.Add("PLAN_DATE", "PLAN_DATE");
            del_HalfGrid.Columns.Add("PLAN_CD", "PLAN_CD");
            del_HalfGrid.Columns.Add("SEQ", "SEQ");

            pHalfGrid.AllowUserToAddRows = false;
            for (int i = 0; i < itemHalfGrid.Columns.Count; i++) 
            {
                pHalfGrid.Columns.Add(itemHalfGrid.Columns[i].Name.ToString(), itemHalfGrid.Columns[i].Name.ToString());
            }
            pHalfGrid.Columns.Add("F_LEVEL", "F_LEVEL");
            pHalfGrid.Columns.Add("TOP_ITEM_CD", "TOP_ITEM_CD");

            ComInfo.gridHeaderSet(itemPlanGrid);
            ComInfo.gridHeaderSet(planGrid);


        }

        #region btn click

        //private void dataGridView1_AllowUserToAddRowsChanged(object sender, EventArgs e)
        //{

        //}

        private void btnNew_Click(object sender, EventArgs e)
        {
            resetSetting();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            plan_logic();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            plan_del();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCustSrch_Click(object sender, EventArgs e)
        {
            Popup.pop거래처검색 frm = new Popup.pop거래처검색();

            frm.sCustGbn = "1"; //매출처
            frm.sCustNm = txt_cust_nm.Text.ToString();
            frm.ShowDialog();

            if (frm.sCode != "")
            {
                txt_cust_cd.Text = frm.sCode.Trim(); 
                txt_cust_nm.Text = frm.sName.Trim();
                old_cust_nm = frm.sCode.Trim();
            }
            else 
            {
                txt_cust_cd.Text = old_cust_nm;
            }
           
            frm.Dispose();
            frm = null;
        }

        private void btnSrch_Click(object sender, EventArgs e)
        {
            plan_list(planGrid, "where A.PLAN_DATE >= '" + start_date.Text.ToString() + "' and  A.PLAN_DATE <= '" + end_date.Text.ToString() + "'");
        }
        #endregion btn click


        #region prod plan logic

        private void resetSetting()
        {
            lbl_plan_gbn.Text = "";
            btnDelete.Enabled = false;

            txt_plan_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txt_plan_date.Enabled = true;
            txt_plan_cd.Text = "";
            txt_cust_cd.Text = "";
            txt_cust_nm.Text = "";
            txt_comment.Text = "";
            old_cust_nm = "";
            chk_order_yn.Checked = false;
            req_date.Text = DateTime.Now.ToString("yyyy-MM-dd");

            itemPlanGrid.Rows.Clear();
            itemHalfGrid.Rows.Clear();
            pHalfGrid.Rows.Clear();

            itemPlanGridAdd();
            del_planGrid.Rows.Clear();
            del_HalfGrid.Rows.Clear();
        }

        private void plan_logic()
        {
            try
            {
                if (txt_cust_cd.Text.ToString().Equals("")) 
                {
                    MessageBox.Show("거래처를 선택하시기 바랍니다.");
                    return;
                }

                if (itemPlanGrid.Rows.Count > 0)
                {
                    int cnt = 0;
                    int grid_cnt = itemPlanGrid.Rows.Count;
                    for (int i = 0; i < grid_cnt; i++)
                    {
                        string txt_item_cd = (string)itemPlanGrid.Rows[i - cnt].Cells["ITEM_CD"].Value;

                        if (txt_item_cd == "" || txt_item_cd == null)  //마지막 행에 원부재료코드가 없을 경우 제거
                        {
                            itemPlanGrid.Rows.RemoveAt(i-cnt);
                            cnt++;
                        }
                    }
                }

                string order_yn = comInfo.resultYn(chk_order_yn);
                if (lbl_plan_gbn.Text != "1")
                {
                    string plan_num = txt_plan_date.Text.ToString().Replace("-", "");
                    plan_num = plan_num.Substring(2).ToString();

                    wnDm wDm = new wnDm();
                    int rsNum = wDm.insertPlan(
                        txt_plan_date.Text.ToString(),
                        txt_cust_cd.Text.ToString(),
                        req_date.Text.ToString(),
                        order_yn,
                        plan_num,
                        txt_comment.Text.ToString(),
                        itemPlanGrid
                        );

                    if (rsNum == 0)
                    {
                        //wnProcCon wDmProc = new wnProcCon();
                        //int rsNum2 = wDmProc.prod_plan_group(txt_plan_date.Text.ToString(), txt_plan_cd.Text.ToString(), Common.p_strStaffNo);

                        //if (rsNum2 == -9)
                        //{
                        //    wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                        //}

                        resetSetting();
                        plan_list(planGrid, "where A.PLAN_DATE >= '" + start_date.Text.ToString() + "' and  A.PLAN_DATE <= '" + end_date.Text.ToString() + "'");

                        MessageBox.Show("성공적으로 등록하였습니다.");
                    }
                    else if (rsNum == 1)
                        MessageBox.Show("저장에 실패하였습니다");
                    else if (rsNum == 2)
                        MessageBox.Show("SQL COMMAND 에러");
                    else if (rsNum == 3)
                        MessageBox.Show("기존 코드가 있으니 \n 다른 코드로 입력 바랍니다.");
                    else
                        MessageBox.Show("Exception 에러");
                }
                else 
                {
                    wnDm wDm = new wnDm();
                    int rsNum = wDm.updatePlan(
                        txt_plan_date.Text.ToString(),
                        txt_plan_cd.Text.ToString(),
                        txt_cust_cd.Text.ToString(),
                        req_date.Text.ToString(),
                        order_yn,
                        txt_comment.Text.ToString(),
                        itemPlanGrid,
                        itemHalfGrid,
                        del_planGrid,
                        del_HalfGrid);

                    if (rsNum == 0)
                    {
                        wnProcCon wDmProc = new wnProcCon();
                        int rsNum2 = wDmProc.prod_plan_group(txt_plan_date.Text.ToString(), txt_plan_cd.Text.ToString(), Common.p_strStaffNo);

                        if (rsNum2 == -9)
                        {
                            wnLog.writeLog(wnLog.LOG_ERROR, wnLog.LOGSTRING_NO_QUERY);
                        }

                        del_planGrid.Rows.Clear();
                        del_HalfGrid.Rows.Clear();
                        plan_list(planGrid, "where A.PLAN_DATE >= '" + start_date.Text.ToString() + "' and  A.PLAN_DATE <= '" + end_date.Text.ToString() + "'");
                        planDetail2();

                        MessageBox.Show("성공적으로 수정하였습니다.");
                    }
                    else if (rsNum == 1)
                        MessageBox.Show("저장에 실패하였습니다");
                    else
                        MessageBox.Show("Exception 에러");
                }
            }
            catch (Exception e) 
            {
                MessageBox.Show("시스템 오류" + e.ToString());
            }
        }

        private void plan_list(DataGridView dgv,string condition) 
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Plan_List(condition);

                lbl_cnt.Text = dt.Rows.Count.ToString();

                if (dt != null && dt.Rows.Count > 0)
                {
                    dgv.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dgv.Rows[i].Cells[0].Value = dt.Rows[i]["PLAN_DATE"].ToString();
                        dgv.Rows[i].Cells[1].Value = dt.Rows[i]["PLAN_CD"].ToString();
                        dgv.Rows[i].Cells[5].Value = dt.Rows[i]["CUST_CD"].ToString();
                        dgv.Rows[i].Cells[2].Value = dt.Rows[i]["CUST_NM"].ToString();
                        dgv.Rows[i].Cells[3].Value = dt.Rows[i]["DELIVER_REQ_DATE"].ToString();
                        dgv.Rows[i].Cells[6].Value = dt.Rows[i]["ORDER_YN"].ToString();
                        dgv.Rows[i].Cells[7].Value = dt.Rows[i]["PLAN_NUM"].ToString();
                        dgv.Rows[i].Cells[8].Value = dt.Rows[i]["ITEM_CNT"].ToString();
                        dgv.Rows[i].Cells["COMMENT"].Value = dt.Rows[i]["COMMENT"].ToString();
                    }
                }
                else
                {
                    planGrid.Rows.Clear();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("시스템 오류" + e.ToString());
            }
        }
        #endregion prod plan logic


        private void plan_del() 
        {
            ComInfo comInfo = new ComInfo();
            DialogResult msgOk =  comInfo.deleteConfrim("계획/수주", txt_plan_date.Text.ToString()+" - "+txt_plan_cd.Text.ToString());

            if (msgOk == DialogResult.No)
            {
                return;
            }

            wnDm wDm = new wnDm();
            int rsNum = wDm.deletePlan(txt_plan_date.Text.ToString(), txt_plan_cd.Text.ToString() );
            if (rsNum == 0)
            {
                resetSetting();

                plan_list(planGrid, "where A.PLAN_DATE >= '" + start_date.Text.ToString() + "' and  A.PLAN_DATE <= '" + end_date.Text.ToString() + "'");

                MessageBox.Show("성공적으로 삭제하였습니다.");
            }
            else if (rsNum == 1)
            {
                MessageBox.Show("삭제에 실패하였습니다.");
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

        private void grid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            conDataGridView grd = (conDataGridView)sender;

            for (int kk = 0; kk < grd.Rows.Count; kk++)
            {
                grd.Rows[kk].Cells[1].Value = (kk + 1).ToString();
            }
        }

        private void grid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            conDataGridView grd = (conDataGridView)sender;

            grd.Rows[e.RowIndex].Cells[0].Value = false;

            // No.
            grd.Rows[e.RowIndex].Cells[1].Style.BackColor = Color.WhiteSmoke;
            grd.Rows[e.RowIndex].Cells[1].Style.SelectionBackColor = Color.Khaki;

            // 코드

            for (int kk = 0; kk < grd.Rows.Count; kk++)
            {
                grd.Rows[kk].Cells[1].Value = (kk + 1).ToString();
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
            string name = itemPlanGrid.CurrentCell.OwningColumn.Name;

            if (name == "TOTAL_AMT" || name == "PRICE" || name == "TOTAL_MONEY")
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
            //if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            //    e.Handled = true;
        }

        private bool bHeadCheck = false;
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

        private void grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex < 0) return;

            conDataGridView grd = (conDataGridView)sender;

            // 수량, 금액 = 0 자료 구분
            grd.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Gray;
            grd.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Gray;

            //// 수량, 금액 != 0 자료 구분
            //if (grd.Rows[e.RowIndex].Cells[7].Value != null && grd.Rows[e.RowIndex].Cells[9].Value != null)
            //{
            //    if (decimal.Parse("" + (string)grd.Rows[e.RowIndex].Cells[7].Value) > 0 && decimal.Parse("" + (string)grd.Rows[e.RowIndex].Cells[9].Value) > 0)
            //    {
            //        grd.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            //        grd.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
            //    }
            //}
        }

        private void grid_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
           
        }

        private void grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            conDataGridView grd = (conDataGridView)sender;
            DataGridViewCell cell = grd[e.ColumnIndex, e.RowIndex];

            cell.Style.BackColor = Color.White;

            
            #region 공통 그리드 체크
            if (grd.Columns[e.ColumnIndex].ToolTipText.IndexOf("명칭") >= 0 && grd._KeyInput == "enter")
            {
                string item_nm = (string)grd.Rows[e.RowIndex].Cells["ITEM_NM"].Value;
                wnDm wDm = new wnDm();
                DataTable dt = new DataTable();
                dt = wDm.fn_Raw_Item_List("where A.RAW_MAT_NM like '%" + item_nm + "%'  ","where  B.ITEM_NM like '%" + item_nm + "%'  ");

                if (dt.Rows.Count > 0)
                { //row가 2줄이 넘을 경우 팝업으로 넘어간다.
                    
                    string sResult = wConst.CZM_call_pop_raw_and_item(grd, dt, e.RowIndex, item_nm);

                }
                
                else
                { //row가 없는 경우
                    string sResult = wConst.CZM_call_pop_raw_and_item(grd, dt, e.RowIndex, item_nm);
                }

                wConst.f_Calc_Price(grd, e.RowIndex, "TOTAL_AMT","PRICE");
            }

            if (grd.Columns[e.ColumnIndex].ToolTipText.IndexOf("수량") >= 0
                || grd.Columns[e.ColumnIndex].ToolTipText.IndexOf("단가") >= 0
                || grd.Columns[e.ColumnIndex].ToolTipText.IndexOf("금액") >= 0)
            {

                string total_amt = (string)grd.Rows[e.RowIndex].Cells["TOTAL_AMT"].Value;
                string price = (string)grd.Rows[e.RowIndex].Cells["PRICE"].Value;

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

                //if (total_amt == "" || total_amt == null)
                //{
                //    grd.Rows[e.RowIndex].Cells["TOTAL_AMT"].Value = "0";
                //}
                //if (price == "" || price == null)
                //{
                //    grd.Rows[e.RowIndex].Cells["PRICE"].Value = "0";
                //}

                wConst.f_Calc_Price(grd, e.RowIndex, "TOTAL_AMT", "PRICE");

                string item_cd_chk = (string)grd.Rows[e.RowIndex].Cells["ITEM_CD"].Value;
                
            }
            #endregion 공통 그리드 체크

            //string sSearchTxt = "" + (string)grd.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

        }

        private void btn_plus_Click(object sender, EventArgs e)
        {
            itemPlanGridAdd();
        }

        private void btn_minus_Click(object sender, EventArgs e)
        {
            minus_logic(itemPlanGrid);
        }

        private void itemPlanGridAdd()
        {
            itemPlanGrid.Rows.Add();
            itemPlanGrid.Rows[itemPlanGrid.Rows.Count - 1].Cells["TOTAL_AMT"].Value = "0";
            itemPlanGrid.Rows[itemPlanGrid.Rows.Count - 1].Cells["PRICE"].Value = "0";
            itemPlanGrid.Rows[itemPlanGrid.Rows.Count - 1].Cells["TOTAL_MONEY"].Value = "0";
        }

        private void minus_logic(conDataGridView dgv) 
        {
            if (dgv.Rows.Count > 1)
            {
                if ((string)dgv.SelectedRows[0].Cells["SEQ"].Value != "" && dgv.SelectedRows[0].Cells["SEQ"].Value != null)
                {
                    
                    int cnt = del_planGrid.Rows.Count;
                    del_planGrid.Rows.Add();

                    del_planGrid.Rows[del_planGrid.Rows.Count - 1].Cells["PLAN_DATE"].Value = txt_plan_date.Text.ToString();
                    del_planGrid.Rows[del_planGrid.Rows.Count - 1].Cells["PLAN_CD"].Value = txt_plan_cd.Text.ToString();
                    del_planGrid.Rows[del_planGrid.Rows.Count - 1].Cells["SEQ"].Value = dgv.SelectedRows[0].Cells["SEQ"].Value;

                    
                }

                

                dgv.Rows.RemoveAt(dgv.SelectedRows[0].Index);
                dgv.CurrentCell = dgv[2, dgv.Rows.Count - 1];
            }
        }

        private void planGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ComInfo.grdHeaderNoAction(e)) 
            {
                planDetail(planGrid, e);
            }

        }
        private void planDetail(DataGridView dgv, DataGridViewCellEventArgs e) 
        {
            btnDelete.Enabled = true;
            lbl_plan_gbn.Text = "1";
            txt_plan_date.Enabled = false;
            txt_plan_date.Text = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();

            txt_plan_cd.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
            txt_cust_cd.Text = dgv.Rows[e.RowIndex].Cells[5].Value.ToString();
            txt_cust_nm.Text = dgv.Rows[e.RowIndex].Cells[2].Value.ToString();
            req_date.Text = dgv.Rows[e.RowIndex].Cells[3].Value.ToString();
            txt_comment.Text = dgv.Rows[e.RowIndex].Cells["COMMENT"].Value.ToString();


            if (dgv.Rows[e.RowIndex].Cells["ORDER_YN"].Value.ToString().Equals("Y"))
            {
                chk_order_yn.Checked = true;
            }
            else
            {
                chk_order_yn.Checked = false;
            }

            planDetail2();
        }

        private void planDetail2() 
        {
            wnDm wDm = new wnDm();
            DataTable dt = null;

            dt = wDm.fn_Plan_Detail_List("where A.PLAN_DATE = '" + txt_plan_date.Text.ToString() + "' and A.PLAN_CD = '" + txt_plan_cd.Text.ToString() + "' ");

            if (dt != null && dt.Rows.Count > 0)
            {
                int itemChk = 0;
                int halfChk = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["F_LEVEL"].ToString().Equals("1"))
                    {
                        itemChk++;
                    }
                    
                }

                itemPlanGrid.RowCount = itemChk;
                itemHalfGrid.RowCount = halfChk;


                for (int i = 0; i < itemPlanGrid.Rows.Count; i++) 
                {
                    //itemPlanGrid.Rows.Add();
                    itemPlanGrid.Rows[i].Cells["P_PLAN_DATE"].Value = dt.Rows[i]["PLAN_DATE"].ToString();
                    itemPlanGrid.Rows[i].Cells["P_PLAN_CD"].Value = dt.Rows[i]["PLAN_CD"].ToString();
                    itemPlanGrid.Rows[i].Cells["SPEC"].Value = dt.Rows[i]["SPEC"].ToString();
                    itemPlanGrid.Rows[i].Cells["SEQ"].Value = dt.Rows[i]["SEQ"].ToString();
                    itemPlanGrid.Rows[i].Cells["ITEM_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                    itemPlanGrid.Rows[i].Cells["ITEM_NM"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                    itemPlanGrid.Rows[i].Cells["UNIT_CD"].Value = dt.Rows[i]["UNIT_CD"].ToString();
                    itemPlanGrid.Rows[i].Cells["UNIT_NM"].Value = dt.Rows[i]["UNIT_NM"].ToString();
                    itemPlanGrid.Rows[i].Cells["CLASS_CD"].Value = dt.Rows[i]["CLASS_CD"].ToString();
                    itemPlanGrid.Rows[i].Cells["CLASS_NM"].Value = dt.Rows[i]["CLASS_NM"].ToString();
                    itemPlanGrid.Rows[i].Cells["COUNTRY_CD"].Value = dt.Rows[i]["COUNTRY_CD"].ToString();
                    itemPlanGrid.Rows[i].Cells["COUNTRY_NM"].Value = dt.Rows[i]["COUNTRY_NM"].ToString();
                    itemPlanGrid.Rows[i].Cells["CHUGJONG_CD"].Value = dt.Rows[i]["CHUGJONG_CD"].ToString();
                    itemPlanGrid.Rows[i].Cells["CHUGJONG_NM"].Value = dt.Rows[i]["CHUGJONG_NM"].ToString();
                    itemPlanGrid.Rows[i].Cells["TYPE_CD"].Value = dt.Rows[i]["TYPE_CD"].ToString();
                    itemPlanGrid.Rows[i].Cells["TYPE_NM"].Value = dt.Rows[i]["TYPE_NM"].ToString();
                    itemPlanGrid.Rows[i].Cells["LABEL_NM"].Value = dt.Rows[i]["LABEL_NM"].ToString();
                    itemPlanGrid.Rows[i].Cells["RAW_ITEM_GUBUN"].Value = dt.Rows[i]["RAW_ITEM_GUBUN"].ToString().Equals("1") ? "상품" : "제품";

                    if (dt.Rows[i]["WORK_YN"].ToString().Equals("Y"))
                    {
                        itemPlanGrid.Rows[i].Cells["WORK_YN"].Value = true;
                    }
                    else
                    {
                        itemPlanGrid.Rows[i].Cells["WORK_YN"].Value = false;
                    }

                    itemPlanGrid.Rows[i].Cells["TOTAL_AMT"].Value = (decimal.Parse(dt.Rows[i]["TOTAL_AMT"].ToString())).ToString("#,0.######");
                    itemPlanGrid.Rows[i].Cells["PRICE"].Value = (decimal.Parse(dt.Rows[i]["PRICE"].ToString())).ToString("#,0.######");
                    itemPlanGrid.Rows[i].Cells["TOTAL_MONEY"].Value = (decimal.Parse(dt.Rows[i]["TOTAL_MONEY"].ToString())).ToString("#,0.######");
                }

                int j = 0;
                
            }
            else
            {
                itemPlanGrid.RowCount = 0;
                itemPlanGridAdd(); //데이터가 없을 경우 빈 행 생성
            }
        }

       
        private void txt_cust_nm_Leave(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Popup.pop거래처검색 frm = new Popup.pop거래처검색();

                frm.sCustGbn = "1"; //매출처
                frm.sCustNm = txt_cust_nm.Text.ToString();
                frm.ShowDialog();

                if (frm.sCode != "")
                {
                    txt_cust_cd.Text = frm.sCode.Trim();
                    txt_cust_nm.Text = frm.sName.Trim();
                    old_cust_nm = frm.sCode.Trim();
                }
                else
                {
                    txt_cust_cd.Text = old_cust_nm;
                }


                frm.Dispose();
                frm = null;
            }
        }

        

        
       
    }
}
