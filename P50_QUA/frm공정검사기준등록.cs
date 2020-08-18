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

namespace 스마트팩토리.P50_QUA
{
    public partial class frm공정검사기준등록 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private DataGridView del_FlowGrid = new DataGridView();
        private DataGridView del_ItemGrid = new DataGridView();

        public frm공정검사기준등록()
        {
            InitializeComponent();

            this.FlowChkGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellEndEdit);
            this.FlowChkGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grid_KeyDown);
            this.FlowChkGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.grid_RowsAdded);

            this.ItemChkGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellEndEdit);
            this.ItemChkGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grid_KeyDown);
            this.ItemChkGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.grid_RowsAdded);

        }

        private void frm공정검사기준등록_Load(object sender, EventArgs e)
        {

            gridSetting(FlowChkGrid,"CHK_METHOD","검사방법",del_FlowGrid);
            gridSetting(ItemChkGrid,"CHK_INTERVAL", "검사주기",del_ItemGrid); ;

            init_ComboBox();
            item_flow_list();
            item_list();
        }

        private void init_ComboBox()
        {
            ComInfo comInfo = new ComInfo();
            string sqlQuery = "";

            //공정
            cmb_srch_gbn.ValueMember = "코드";
            cmb_srch_gbn.DisplayMember = "명칭";
            sqlQuery = comInfo.queryItemGbnAll();
            wConst.ComboBox_Read_NoBlank(cmb_srch_gbn, sqlQuery);

            //cmb_eva_gbn.ValueMember = "코드";
            //cmb_eva_gbn.DisplayMember = "명칭";
            //sqlQuery = comInfo.queryCode("620");
            //wConst.ComboBox_Read_Blank(cmb_eva_gbn, sqlQuery);

            //제품
            cmb_srch_gbn2.ValueMember = "코드";
            cmb_srch_gbn2.DisplayMember = "명칭";
            sqlQuery = comInfo.queryItemGbnAll();
            wConst.ComboBox_Read_NoBlank(cmb_srch_gbn2, sqlQuery);

        }

        private void gridSetting(conDataGridView dgv,string met_cd, string met_nm,DataGridView del_dgv) 
        {

            DataGridViewComboBoxColumn cmbColumn = new DataGridViewComboBoxColumn();

            wnDm wDm = new wnDm();
            DataTable dt = new DataTable();
            dt = wDm.fn_query_com_code("620");
            // ((DataGridViewComboBoxColumn)main_dgv.Columns["GRADE"]).DataSource = dt2;

            cmbColumn.ValueMember = "코드";
            cmbColumn.DisplayMember = "명칭";
            cmbColumn.DataSource = dt;
            cmbColumn.HeaderText = "구분";
            cmbColumn.Name = "EVA_GUBUN";

            dgv.Columns.Add(cmbColumn);

            dgv.Columns.Add("CHK_LOC", "검사위치");
            dgv.Columns.Add("RULE_SIZE", "규정치");
            dgv.Columns.Add("RULE_LIMIT", "채용한계");
            dgv.Columns.Add("MEASURE_APP", "측정기구");
            dgv.Columns.Add(met_cd, met_nm);
            dgv.Columns.Add("LOWER_SIZE", "하한치수");
            dgv.Columns.Add("UPPER_SIZE", "상한치수");
            dgv.Columns.Add("LOWER_SELF", "자체하한");
            dgv.Columns.Add("UPPER_SELF", "자체상한");
            dgv.Columns.Add("CHK_CD", "항목코드");
            dgv.Columns.Add("ITEM_CD", "ITEM_CD");
            dgv.Columns.Add("OLD_CHK_CD", "OLD_CHK_CD");
            dgv.Columns.Add("OLD_CHK_NM", "OLD_CHK_NM");
            dgv.Columns.Add("CHK_ORD", "CHK_ORD");

            dgv.Columns[1].Name = "NO";
            dgv.Columns[2].Name = "CHK_NM";
            dgv.Columns[3].Frozen = true;
            dgv.Columns[4].Frozen = true;
            dgv.Columns[5].Frozen = true;
            dgv.Columns[6].Frozen = true;
            dgv.Columns[7].Frozen = true;
            dgv.Columns[8].Frozen = true;

            dgv.Columns[3].Width = 50;
            dgv.Columns[4].Width = 75;
            dgv.Columns[5].Width = 110;
            dgv.Columns[6].Width = 80;
            dgv.Columns[7].Width = 100;
            dgv.Columns[8].Width = 55;
            dgv.Columns[9].Width = 55;
            dgv.Columns[10].Width = 55;
            dgv.Columns[11].Width = 55;
            dgv.Columns[12].Width = 55;
            dgv.Columns[14].Width = 80;
            dgv.Columns[17].Width = 55;

            dgv.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[13].Visible = false;
            dgv.Columns[14].Visible = false;
            dgv.Columns[15].Visible = false;
            dgv.Columns[16].Visible = false;
            dgv.Columns[17].Visible = false;

            del_dgv.AllowUserToAddRows = false;

            del_dgv.Columns.Add("CHK_CD", "CHK_CD");
            del_dgv.Columns.Add("ITEM_CD", "ITEM_CD");
        }

        #region button logic common

        private void btnNew_Click(object sender, EventArgs e)
        {
            resetSetting();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbStanControl.SelectedIndex == 0)
            {
                save_logic();
            }
            else 
            {
                save_item_logic();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (tbStanControl.SelectedIndex == 0)
            {
                flow_chk_del();
            }
            else
            {
                item_chk_del();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion 

        #region common logic 

        private void all_chk_logic(conDataGridView dgv,string condition, TextBox txt_item_cd) 
        {
            try
            {
                if (txt_item_cd.Text.ToString().Equals("") || txt_item_cd.Text == null)
                {
                    MessageBox.Show("제품정보가 없습니다. ");
                    return;
                }

                if (dgv.Rows.Count > 0)
                {
                    MessageBox.Show("항목 데이터가 존재합니다. ");
                    return;
                }

                wnDm wDm = new wnDm();
                DataTable dt = new DataTable();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("where 1=1 ");
                sb.AppendLine(condition); //"and A.CHK_GUBUN = '1'"

                dt = wDm.fn_Chk_List(sb.ToString());

                if (dt != null && dt.Rows.Count > 0)
                {
                    dgv.RowCount = dt.Rows.Count;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dgv.Rows[i].Cells[1].Value = (i + 1).ToString();
                        dgv.Rows[i].Cells[13].Value = dt.Rows[i]["CHK_CD"].ToString();
                        dgv.Rows[i].Cells[17].Value = dt.Rows[i]["CHK_ORD"].ToString();
                        dgv.Rows[i].Cells[2].Value = dt.Rows[i]["CHK_NM"].ToString();
                        //dgv.Rows[i].Cells[4].Value = dt.Rows[i]["CHK_LOC"].ToString();
                        //dgv.Rows[i].Cells[5].Value = dt.Rows[i]["RULE_SIZE"].ToString();
                        //dgv.Rows[i].Cells[6].Value = dt.Rows[i]["RULE_LIMIT"].ToString();
                        //dgv.Rows[i].Cells[7].Value = dt.Rows[i]["MEASURE_APP"].ToString();
                        //dgv.Rows[i].Cells[8].Value = dt.Rows[i]["CHK_INTEVAL"].ToString();
                        dgv.Rows[i].Cells[9].Value = "0";
                        dgv.Rows[i].Cells[10].Value = "0";
                        dgv.Rows[i].Cells[11].Value = "0";
                        dgv.Rows[i].Cells[12].Value = "0";
                    }
                }
                else
                {
                    MessageBox.Show("데이터 일시 오류");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("시스템 에러: " + ex.Message.ToString());
            }
        }

        private void tbStanControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbStanControl.SelectedIndex == 0)
            {
                lbl_title.Text = "공정검사기준등록";
                item_flow_list();
            }
            else 
            {
                lbl_title.Text = "제품검사기준등록";
                item_list();
            }
        }

        #endregion common logic

        #region flow chk stan
        //저장
        private void save_logic()
        {
            if (cmb_item_flow.SelectedValue == null) cmb_item_flow.SelectedValue = "";

            if (cmb_item_flow.SelectedValue.ToString().Equals(""))
            {
                MessageBox.Show("공정을 선택하시기 바랍니다.");
                return;
            }

            if (FlowChkGrid.Rows.Count == 0)
            {
                MessageBox.Show("검사항목을 등록하시기 바랍니다.");
                return;
            }

            if (lbl_flow_chk_gbn.Text != "1") //신규
            {
                wnDm wDm = new wnDm();
                int rsNum = wDm.insertFlowChk(txt_item_cd.Text.ToString()
                                              , cmb_item_flow.SelectedValue.ToString()
                                              , ""
                                              , txt_measure_cnt.Text.ToString()
                                              , FlowChkGrid);

                if (rsNum == 0)
                {
                    item_flow_list();
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
            else //수정 
            {
                wnDm wDm = new wnDm();
                int rsNum = wDm.updateFlowChk(txt_item_cd.Text.ToString()
                                                  , cmb_item_flow.SelectedValue.ToString()
                                                  , ""
                                                  , txt_measure_cnt.Text.ToString()
                                                  , FlowChkGrid
                                                  , del_FlowGrid);

                if (rsNum == 0)
                {
                    item_flow_list();
                    MessageBox.Show("성공적으로 수정하였습니다.");
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
        }

        private void item_flow_list()
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("where 1=1 ");

                if (cmb_srch_gbn.SelectedIndex > 0)
                {
                    sb.AppendLine("and ITEM_GUBUN = '" + cmb_srch_gbn.SelectedValue.ToString() + "' ");
                }

                if (!txt_srch.Text.ToString().Equals(""))
                {
                    sb.AppendLine("and ITEM_NM like '%" + txt_srch.Text.ToString() + "%' ");
                }

                dt = wDm.fn_Item_List(sb.ToString());

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.dataItemGrid.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataItemGrid.Rows[i].Cells["ITEM_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                        dataItemGrid.Rows[i].Cells["ITEM_NM"].Value = dt.Rows[i]["ITEM_NM"].ToString();
                        //dataItemGrid.Rows[i].Cells[3].Value = dt.Rows[i]["SPEC"].ToString();
                        dataItemGrid.Rows[i].Cells["ITEM_GUBUN_NM"].Value = dt.Rows[i]["ITEM_GUBUN_NM"].ToString();
                    }
                }
                else
                {
                    dataItemGrid.Rows.Clear();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("시스템 에러: " + e.Message.ToString());
            }
        }

        private void chk_list()
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("where 1=1 ");

                dt = wDm.fn_Flow_Chk_List(sb.ToString());

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.itemFlowChk.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        itemFlowChk.Rows[i].Cells[0].Value = dt.Rows[i]["ITEM_CD"].ToString();
                        itemFlowChk.Rows[i].Cells[1].Value = dt.Rows[i]["ITEM_NM"].ToString();
                        itemFlowChk.Rows[i].Cells[2].Value = dt.Rows[i]["FLOW_CD"].ToString();
                        itemFlowChk.Rows[i].Cells[3].Value = dt.Rows[i]["FLOW_NM"].ToString();
                        itemFlowChk.Rows[i].Cells[4].Value = dt.Rows[i]["SPEC"].ToString();
                        itemFlowChk.Rows[i].Cells[5].Value = dt.Rows[i]["ITEM_GUBUN_NM"].ToString();
                        itemFlowChk.Rows[i].Cells[6].Value = dt.Rows[i]["MEASURE_CNT"].ToString();
                        itemFlowChk.Rows[i].Cells[7].Value = dt.Rows[i]["EVA_GUBUN"].ToString();
                    }
                }
                else
                {
                    itemFlowChk.Rows.Clear();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("시스템 에러: " + e.Message.ToString());
            }
        }

        //오른쪽 탭 클릭 시 
        private void tbChkControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbFlowChkCon.SelectedIndex == 0)
            {
                item_list();
            }
            else
            {
                chk_list();
            }
        }

        //검색 탭의 그리드 더블클릭 시 
        private void dataItemGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lbl_flow_chk_gbn.Text = "";
            DataGridView dgv = (DataGridView)sender;
            cmb_item_flow.Enabled = true;

            FlowChkGrid.Rows.Clear();
            lbl_title.Text = "공정검사기준 - 신규 ";
            string item_cd = dgv.Rows[e.RowIndex].Cells["ITEM_CD"].Value.ToString();
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("where 1=1 ");
                sb.AppendLine(" and A.ITEM_CD = '" + item_cd + "' ");

                dt = wDm.fn_Item_List(sb.ToString());

                if (dt != null && dt.Rows.Count > 0)
                {
                    ComInfo comInfo = new ComInfo();
                    string sqlQuery = "";

                    cmb_item_flow.ValueMember = "코드";
                    cmb_item_flow.DisplayMember = "명칭";
                    sqlQuery = comInfo.queryItemFlow(item_cd);
                    wConst.ComboBox_Read_Blank(cmb_item_flow, sqlQuery);

                    txt_item_cd.Text = dt.Rows[0]["ITEM_CD"].ToString();
                    txt_item_nm.Text = dt.Rows[0]["ITEM_NM"].ToString();
                    txt_spec.Text = dt.Rows[0]["SPEC"].ToString();
                    txt_item_gbn.Text = dt.Rows[0]["ITEM_GUBUN_NM"].ToString();
                }
                else
                {
                    MessageBox.Show("데이터 일시 오류");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("시스템 에러: " + ex.Message.ToString());
            }
        }

        private void btn_all_chk_Click(object sender, EventArgs e)
        {
            conDataGridView dgv = FlowChkGrid;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("and A.CHK_GUBUN = '1' ");
            all_chk_logic(dgv,sb.ToString(),txt_item_cd);
        }

        private void itemFlowChk_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            lbl_flow_chk_gbn.Text = "1";
            DataGridView dgv = (DataGridView)sender;
            btnDelete.Enabled = true;
            cmb_item_flow.Enabled = false;

            lbl_title.Text = "공정검사기준 - 수정 ";

            string item_cd = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();
            string flow_cd = dgv.Rows[e.RowIndex].Cells[2].Value.ToString();
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("where 1=1 ");
                sb.AppendLine(" and A.ITEM_CD = '" + item_cd + "' ");

                dt = wDm.fn_Flow_Chk_List(sb.ToString());

                if (dt != null && dt.Rows.Count > 0)
                {
                    ComInfo comInfo = new ComInfo();
                    string sqlQuery = "";

                    cmb_item_flow.ValueMember = "코드";
                    cmb_item_flow.DisplayMember = "명칭";
                    sqlQuery = comInfo.queryItemFlow(item_cd);
                    wConst.ComboBox_Read_Blank(cmb_item_flow, sqlQuery);

                    cmb_item_flow.SelectedValue = flow_cd;
                    txt_item_cd.Text = dt.Rows[0]["ITEM_CD"].ToString();
                    txt_item_nm.Text = dt.Rows[0]["ITEM_NM"].ToString();
                    txt_spec.Text = dt.Rows[0]["SPEC"].ToString();
                    txt_item_gbn.Text = dt.Rows[0]["ITEM_GUBUN_NM"].ToString();
                    txt_measure_cnt.Text = dt.Rows[0]["MEASURE_CNT"].ToString();

                    sb.AppendLine(" and A.FLOW_CD = '" + flow_cd + "' ");
                    dt = wDm.fn_Flow_Chk_Detail_List(sb.ToString(), 1);

                    FlowChkGrid.RowCount = dt.Rows.Count;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            FlowChkGrid.Rows[i].Cells[1].Value = (i + 1).ToString();
                            FlowChkGrid.Rows[i].Cells["CHK_CD"].Value = dt.Rows[i]["CHK_CD"].ToString();
                            FlowChkGrid.Rows[i].Cells["CHK_ORD"].Value = dt.Rows[i]["CHK_ORD"].ToString();
                            FlowChkGrid.Rows[i].Cells["CHK_NM"].Value = dt.Rows[i]["CHK_NM"].ToString();
                            FlowChkGrid.Rows[i].Cells["EVA_GUBUN"].Value = dt.Rows[i]["EVA_GUBUN"].ToString();
                            FlowChkGrid.Rows[i].Cells["CHK_LOC"].Value = dt.Rows[i]["CHK_LOC"].ToString();
                            FlowChkGrid.Rows[i].Cells["RULE_SIZE"].Value = dt.Rows[i]["RULE_SIZE"].ToString();
                            FlowChkGrid.Rows[i].Cells["RULE_LIMIT"].Value = dt.Rows[i]["RULE_LIMIT"].ToString();
                            FlowChkGrid.Rows[i].Cells["MEASURE_APP"].Value = dt.Rows[i]["MEASURE_APP"].ToString();
                            FlowChkGrid.Rows[i].Cells["CHK_METHOD"].Value = dt.Rows[i]["CHK_METHOD"].ToString();
                            FlowChkGrid.Rows[i].Cells["LOWER_SIZE"].Value = (decimal.Parse(dt.Rows[i]["LOWER_SIZE"].ToString())).ToString("#,0.######");
                            FlowChkGrid.Rows[i].Cells["UPPER_SIZE"].Value = (decimal.Parse(dt.Rows[i]["UPPER_SIZE"].ToString())).ToString("#,0.######");
                            FlowChkGrid.Rows[i].Cells["LOWER_SELF"].Value = (decimal.Parse(dt.Rows[i]["LOWER_SELF"].ToString())).ToString("#,0.######");
                            FlowChkGrid.Rows[i].Cells["UPPER_SELF"].Value = (decimal.Parse(dt.Rows[i]["UPPER_SELF"].ToString())).ToString("#,0.######");

                            FlowChkGrid.Rows[i].Cells["ITEM_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                            FlowChkGrid.Rows[i].Cells["OLD_CHK_CD"].Value = dt.Rows[i]["CHK_CD"].ToString();
                            FlowChkGrid.Rows[i].Cells["OLD_CHK_NM"].Value = dt.Rows[i]["CHK_NM"].ToString();


                        }
                    }
                    else
                    {
                        MessageBox.Show("데이터 일시 오류2");
                    }
                }
                else
                {
                    MessageBox.Show("데이터 일시 오류");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("시스템 에러: " + ex.Message.ToString());
            }
        }

        //공정검사항목 삭제 

        private void flow_chk_del()
        {
            ComInfo comInfo = new ComInfo();
            DialogResult msgOk = comInfo.deleteConfrim("공정검사항목", txt_item_nm.Text.ToString() + "-" + cmb_item_flow.SelectedValue.ToString());

            if (msgOk == DialogResult.No)
            {
                return;
            }

            wnDm wDm = new wnDm();
            int rsNum = wDm.deleteFlowChk(txt_item_cd.Text.ToString(), cmb_item_flow.SelectedValue.ToString());
            if (rsNum == 0)
            {
                resetSetting();

                item_list();
                chk_list();
                MessageBox.Show("성공적으로 삭제하였습니다.");
            }
            else if (rsNum == 1)
            {
                MessageBox.Show("삭제에 실패하였습니다.");
            }
        }

        private void resetSetting()
        {
            lbl_title.Text = "공정검사기준 - 신규 ";
            lbl_flow_chk_gbn.Text = "";
            txt_item_cd.Text = "";
            txt_item_gbn.Text = "";
            txt_item_nm.Text = "";
            txt_spec.Text = "";
            txt_measure_cnt.Text = "0";
            cmb_item_flow.SelectedValue = "";
            cmb_item_flow.Enabled = true;
            btnDelete.Enabled = false;

            FlowChkGrid.Rows.Clear();
        }

        private void txt_measure_cnt_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e);
        }
        #endregion flow chk stan


        #region item chk stan

        #region button
        private void btnSearch2_Click(object sender, EventArgs e)
        {
            item_list();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            item_flow_list();
        }

        private void btn_item_all_chk_Click(object sender, EventArgs e)
        {
            conDataGridView dgv = ItemChkGrid;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("and A.CHK_GUBUN = '2' ");
            all_chk_logic(dgv, sb.ToString(),txt_item_cd2);
        }

        #endregion
        #region grid

        private void dataItemGrid2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            try
            {
                if (dgv.Rows[e.RowIndex].Cells["ITEM_CHK_YN"].Value.ToString().Equals("0")) //미등록
                {
                    lbl_item_chk_gbn.Text = "";

                    ItemChkGrid.Rows.Clear();
                    lbl_title.Text = "제품검사기준 - 신규 ";
                    string item_cd = dgv.Rows[e.RowIndex].Cells["ITEM_CD2"].Value.ToString();

                    wnDm wDm = new wnDm();
                    DataTable dt = null;

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("where 1=1 ");
                    sb.AppendLine(" and A.ITEM_CD = '" + item_cd + "' ");

                    dt = wDm.fn_Item_List(sb.ToString());

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ComInfo comInfo = new ComInfo();
                      
                        txt_item_cd2.Text = dt.Rows[0]["ITEM_CD"].ToString();
                        txt_item_nm2.Text = dt.Rows[0]["ITEM_NM"].ToString();
                        txt_spec2.Text = dt.Rows[0]["SPEC"].ToString();
                        txt_item_gbn2.Text = dt.Rows[0]["ITEM_GUBUN_NM"].ToString();

                        txt_measure_cnt2.Text = "0";
                    }
                    else
                    {
                        MessageBox.Show("데이터 일시 오류");
                    }
                }
                else 
                {
                    btnDelete.Enabled = true;
                    lbl_item_chk_gbn.Text = "1";

                    ItemChkGrid.Rows.Clear();
                    lbl_title.Text = "제품검사기준 - 수정 ";
                    string item_cd = dgv.Rows[e.RowIndex].Cells["ITEM_CD2"].Value.ToString();

                    wnDm wDm = new wnDm();
                    DataTable dt = null;

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("where 1=1 ");
                    sb.AppendLine(" and A.ITEM_CD = '" + item_cd + "' ");

                    dt = wDm.fn_Item_Chk_List(sb.ToString());

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ComInfo comInfo = new ComInfo();

                        txt_item_cd2.Text = dt.Rows[0]["ITEM_CD"].ToString();
                        txt_item_nm2.Text = dt.Rows[0]["ITEM_NM"].ToString();
                        txt_spec2.Text = dt.Rows[0]["SPEC"].ToString();
                        txt_item_gbn2.Text = dt.Rows[0]["ITEM_GUBUN_NM"].ToString();
                        txt_measure_cnt2.Text = dt.Rows[0]["MEASURE_CNT"].ToString();

                        dt = wDm.fn_Item_Chk_Detail_List(sb.ToString(), 1);

                        ItemChkGrid.RowCount = dt.Rows.Count;
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                ItemChkGrid.Rows[i].Cells[1].Value = (i + 1).ToString();
                                ItemChkGrid.Rows[i].Cells["CHK_CD"].Value = dt.Rows[i]["CHK_CD"].ToString();
                                ItemChkGrid.Rows[i].Cells["CHK_ORD"].Value = dt.Rows[i]["CHK_ORD"].ToString();
                                ItemChkGrid.Rows[i].Cells["CHK_NM"].Value = dt.Rows[i]["CHK_NM"].ToString();
                                ItemChkGrid.Rows[i].Cells["EVA_GUBUN"].Value = dt.Rows[i]["EVA_GUBUN"].ToString();
                                ItemChkGrid.Rows[i].Cells["CHK_LOC"].Value = dt.Rows[i]["CHK_LOC"].ToString();
                                ItemChkGrid.Rows[i].Cells["RULE_SIZE"].Value = dt.Rows[i]["RULE_SIZE"].ToString();
                                ItemChkGrid.Rows[i].Cells["RULE_LIMIT"].Value = dt.Rows[i]["RULE_LIMIT"].ToString();
                                ItemChkGrid.Rows[i].Cells["MEASURE_APP"].Value = dt.Rows[i]["MEASURE_APP"].ToString();
                                ItemChkGrid.Rows[i].Cells["CHK_INTERVAL"].Value = dt.Rows[i]["CHK_INTERVAL"].ToString();
                                ItemChkGrid.Rows[i].Cells["LOWER_SIZE"].Value = (decimal.Parse(dt.Rows[i]["LOWER_SIZE"].ToString())).ToString("#,0.######");
                                ItemChkGrid.Rows[i].Cells["UPPER_SIZE"].Value = (decimal.Parse(dt.Rows[i]["UPPER_SIZE"].ToString())).ToString("#,0.######");
                                ItemChkGrid.Rows[i].Cells["LOWER_SELF"].Value = (decimal.Parse(dt.Rows[i]["LOWER_SELF"].ToString())).ToString("#,0.######");
                                ItemChkGrid.Rows[i].Cells["UPPER_SELF"].Value = (decimal.Parse(dt.Rows[i]["UPPER_SELF"].ToString())).ToString("#,0.######");

                                ItemChkGrid.Rows[i].Cells["ITEM_CD"].Value = dt.Rows[i]["ITEM_CD"].ToString();
                                ItemChkGrid.Rows[i].Cells["OLD_CHK_CD"].Value = dt.Rows[i]["CHK_CD"].ToString();
                                ItemChkGrid.Rows[i].Cells["OLD_CHK_NM"].Value = dt.Rows[i]["CHK_NM"].ToString();

                            }
                        }
                        else
                        {
                            MessageBox.Show("데이터 일시 오류2");
                        }
                    }
                    else
                    {
                        MessageBox.Show("데이터 일시 오류");
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("시스템 에러: " + ex.Message.ToString());
            }
        }

        #endregion 
        //제품 검사 기준 목록 (검색)
        private void item_list()
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("where 1=1 ");

                if (cmb_srch_gbn2.SelectedIndex > 0)
                {
                    sb.AppendLine("and ITEM_GUBUN = '" + cmb_srch_gbn2.SelectedValue.ToString() + "' ");
                }

                if (!txt_srch2.Text.ToString().Equals(""))
                {
                    sb.AppendLine("and ITEM_NM like '%" + txt_srch2.Text.ToString() + "%' ");
                }

                dt = wDm.fn_Item_List(sb.ToString());

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.dataItemGrid2.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataItemGrid2.Rows[i].Cells[0].Value = (i + 1).ToString();
                        dataItemGrid2.Rows[i].Cells[1].Value = dt.Rows[i]["ITEM_CD"].ToString();
                        dataItemGrid2.Rows[i].Cells[2].Value = dt.Rows[i]["ITEM_NM"].ToString();
                        //dataItemGrid.Rows[i].Cells[3].Value = dt.Rows[i]["SPEC"].ToString();
                        dataItemGrid2.Rows[i].Cells[3].Value = dt.Rows[i]["ITEM_GUBUN_NM"].ToString();
                        if (dt.Rows[i]["ITEM_CHK_YN"].ToString().Equals("0"))
                        {
                            dataItemGrid2.Rows[i].Cells[4].Value = "미등록";
                        }
                        else
                        {
                            dataItemGrid2.Rows[i].Cells[4].Value = "등록";
                        }
                        dataItemGrid2.Rows[i].Cells[5].Value = dt.Rows[i]["ITEM_CHK_YN"].ToString();
                    }
                }
                else
                {
                    dataItemGrid.Rows.Clear();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("시스템 에러: " + e.Message.ToString());
            }
        }

        private void tbItemChkCon_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tbl = (TabControl)sender;

            if (tbl.SelectedIndex == 0)
            {
                item_list();
            }
        }

        private void save_item_logic()
        {

            if (ItemChkGrid.Rows.Count == 0)
            {
                MessageBox.Show("검사항목을 등록하시기 바랍니다.");
                return;
            }

            //for (int i = 0; i < ItemChkGrid.Rows.Count; i++) 
            //{
            //    if (ItemChkGrid.Rows[i].Cells["CMB_EVA_GBN"].Value == null) ItemChkGrid.Rows[i].Cells["CMB_EVA_GBN"].Value = (string)"";

            //    if ((string)ItemChkGrid.Rows[i].Cells["CMB_EVA_GBN"].Value == "") 
            //    {
            //        MessageBox.Show("' "+(string)ItemChkGrid.Rows[i].Cells["CHK_NM"].Value + "' 의 평가구분을 지정하시기 바랍니다.");
            //        return;
            //    }
            //}

            if (lbl_item_chk_gbn.Text != "1") //신규
            {
                wnDm wDm = new wnDm();
                int rsNum = wDm.insertItemChk(txt_item_cd2.Text.ToString()
                                              , ""
                                              , txt_measure_cnt2.Text.ToString()
                                              , ItemChkGrid);

                if (rsNum == 0)
                {
                    item_list();
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
            else //수정 
            {
                wnDm wDm = new wnDm();
                int rsNum = wDm.updateItemChk(txt_item_cd2.Text.ToString()
                                                  , ""
                                                  , txt_measure_cnt2.Text.ToString()
                                                  , ItemChkGrid
                                                  , del_ItemGrid);
                if (rsNum == 0)
                {
                    item_flow_list();
                    MessageBox.Show("성공적으로 수정하였습니다.");
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
        }

        private void item_chk_del()
        {
            ComInfo comInfo = new ComInfo();
            DialogResult msgOk = comInfo.deleteConfrim("제품검사항목", txt_item_nm.Text.ToString() + "-" + cmb_item_flow.SelectedValue.ToString());

            if (msgOk == DialogResult.No)
            {
                return;
            }

            wnDm wDm = new wnDm();
            int rsNum = wDm.deleteItemChk(txt_item_cd2.Text.ToString());
            if (rsNum == 0)
            {
                resetSetting2();

                item_list();
                MessageBox.Show("성공적으로 삭제하였습니다.");
            }
            else if (rsNum == 1)
            {
                MessageBox.Show("삭제에 실패하였습니다.");
            }
        }

        private void resetSetting2()
        {
            lbl_title.Text = "제품검사기준 - 신규 ";
            lbl_item_chk_gbn.Text = "";
            txt_item_cd2.Text = "";
            txt_item_gbn2.Text = "";
            txt_item_nm2.Text = "";
            txt_spec2.Text = "";
            txt_measure_cnt2.Text = "0";
            btnDelete.Enabled = false;

            ItemChkGrid.Rows.Clear();
        }
        #endregion item chk stan

        private void btn_f_chk_plus_Click(object sender, EventArgs e)
        {
            chkGridAdd(FlowChkGrid);
        }

        private void btn_f_chk_minus_Click(object sender, EventArgs e)
        {
            minus_logic(FlowChkGrid, del_FlowGrid);
        }

        private void btn_i_chk_plus_Click(object sender, EventArgs e)
        {
            chkGridAdd(ItemChkGrid);
        }

        private void btn_i_chk_minus_Click(object sender, EventArgs e)
        {
            minus_logic(ItemChkGrid, del_ItemGrid);
        }

        private void chkGridAdd(conDataGridView dgv)
        {
            dgv.Rows.Add();
            dgv.Rows[dgv.Rows.Count - 1].Cells[9].Value = "0";
            dgv.Rows[dgv.Rows.Count - 1].Cells[10].Value = "0";
            dgv.Rows[dgv.Rows.Count - 1].Cells[11].Value = "0";
            dgv.Rows[dgv.Rows.Count - 1].Cells[12].Value = "0";
        }

        private void minus_logic(conDataGridView dgv, DataGridView del_dgv)
        {
            if (dgv.Rows.Count > 1)
            {
                if ((string)dgv.SelectedRows[0].Cells["ITEM_CD"].Value != "" && dgv.SelectedRows[0].Cells["ITEM_CD"].Value != null)
                {
                    del_dgv.Rows.Add();

                    del_dgv.Rows[del_dgv.Rows.Count - 1].Cells["CHK_CD"].Value = dgv.SelectedRows[0].Cells["CHK_CD"].Value;
                    del_dgv.Rows[del_dgv.Rows.Count - 1].Cells["ITEM_CD"].Value = dgv.SelectedRows[0].Cells["ITEM_CD"].Value;

                }

                dgv.Rows.RemoveAt(dgv.SelectedRows[0].Index);
                dgv.CurrentCell = dgv[2, dgv.Rows.Count - 1];

                for (int i = 0; i < dgv.Rows.Count; i++) 
                {
                    dgv.Rows[i].Cells[1].Value = (i + 1).ToString();
                }
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

        private void grid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            conDataGridView grd = (conDataGridView)sender;

            grd.Rows[e.RowIndex].Cells[0].Value = false;

            // No.
            grd.Rows[e.RowIndex].Cells[1].Style.BackColor = Color.WhiteSmoke;
            grd.Rows[e.RowIndex].Cells[1].Style.SelectionBackColor = Color.Khaki;

        }

        private void grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            conDataGridView grd = (conDataGridView)sender;
            DataGridViewCell cell = grd[e.ColumnIndex, e.RowIndex];

            cell.Style.BackColor = Color.White;


            #region 공통 그리드 체크
            if (grd.Columns[e.ColumnIndex].ToolTipText.IndexOf("명칭") >= 0 && grd._KeyInput == "enter")
            {
                string chk_nm = (string)grd.Rows[e.RowIndex].Cells["CHK_NM"].Value;
                wnDm wDm = new wnDm();
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("where 1=1 ");
                if (chk_nm != null)
                {
                    sb.AppendLine(" and CHK_NM like '%" + chk_nm + "%' ");
                }

                int chk = 0;
                if (grd.Name.ToString().Equals("FlowChkGrid"))
                {
                    chk = 1;
                    sb.AppendLine(" and CHK_GUBUN = '1' ");
                }
                else 
                {
                    chk = 2;
                    sb.AppendLine(" and CHK_GUBUN = '2' ");
                }

                dt = wDm.fn_Chk_List(sb.ToString());

                if (dt.Rows.Count > 1)
                {
                    wConst.call_pop_chk(grd, dt, e.RowIndex, chk_nm, chk);
                }
                else if (dt.Rows.Count == 1) //row가 1일 경우 해당 row에 값을 자동 입력한다.
                {
                    grd.Rows[e.RowIndex].Cells[1].Value = (e.RowIndex + 1).ToString();
                    grd.Rows[e.RowIndex].Cells["CHK_CD"].Value = dt.Rows[0]["CHK_CD"].ToString();
                    grd.Rows[e.RowIndex].Cells["CHK_ORD"].Value = dt.Rows[0]["CHK_ORD"].ToString();
                    grd.Rows[e.RowIndex].Cells["CHK_NM"].Value = dt.Rows[0]["CHK_NM"].ToString();
                    grd.Rows[e.RowIndex].Cells["OLD_CHK_NM"].Value = dt.Rows[0]["CHK_NM"].ToString();
                    grd.Rows[e.RowIndex].Cells["OLD_CHK_CD"].Value = dt.Rows[0]["CHK_CD"].ToString();
                }
                else
                {
                    //row가 없는 경우
                    MessageBox.Show("데이터가 없습니다.");
                }

            }

            #endregion 공통 그리드 체크

            //string sSearchTxt = "" + (string)grd.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        }
    }
}
