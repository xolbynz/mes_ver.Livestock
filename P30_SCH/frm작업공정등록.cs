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
    public partial class frm작업공정등록 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private Rectangle _Retangle;

        private DateTimePicker[] dtp = new DateTimePicker[12];
        private conDataGridView[] dgv = new conDataGridView[12];
        private Panel[] pnl = new Panel[12];
        private Button[] btn = new Button[12];
        private Label[] lbl_flow_cd = new Label[12];
        private Label[] lbl_flow_nm = new Label[12];

        private Label[] lbl_flow_pr_type = new Label[12];
        private Label[] lbl_flow_seq = new Label[12]; //공정코드 순서 값
        private Label[] lbl_item_iden = new Label[12]; //제품식별표


        int flow_cnt  = 0;
        private int max_flow_cnt = 12;
        public frm작업공정등록()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm작업공정등록_Load(object sender, EventArgs e)
        {
            getSetting();
            gridList();

            workLogic();
        }
        
        private void gridList() 
        {
            workLogic(); //작업지시서 목록 
            flowLogic(); //공정 목록
        }

        private void workLogic() 
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Work_List("where ISNULL(C.COMPLETE_YN,'N') = 'N' ");

                if (dt.Rows.Count > 0)
                {
                    dataWorkGrid.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataWorkGrid.Rows[i].Cells[0].Value = dt.Rows[i]["LOT_NO"].ToString();
                        dataWorkGrid.Rows[i].Cells[1].Value = dt.Rows[i]["ITEM_NM"].ToString();
                        dataWorkGrid.Rows[i].Cells[2].Value = dt.Rows[i]["INST_AMT"].ToString();
                        dataWorkGrid.Rows[i].Cells[3].Value = dt.Rows[i]["ITEM_CD"].ToString();
                        dataWorkGrid.Rows[i].Cells[4].Value = dt.Rows[i]["SPEC"].ToString();
                        dataWorkGrid.Rows[i].Cells[5].Value = dt.Rows[i]["W_INST_DATE"].ToString();
                        dataWorkGrid.Rows[i].Cells[6].Value = dt.Rows[i]["W_INST_CD"].ToString();
                        dataWorkGrid.Rows[i].Cells[7].Value = dt.Rows[i]["CHARGE_AMT"].ToString();
                        dataWorkGrid.Rows[i].Cells[8].Value = dt.Rows[i]["PACK_AMT"].ToString();
                        dataWorkGrid.Rows[i].Cells[9].Value = dt.Rows[i]["COMPLETE_YN"].ToString();
                    }
                }
                else
                {
                    dataWorkGrid.Rows.Clear();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error" + e.ToString());
            }
        }

        private void flowLogic() 
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Work_Flow_List("and A.FLOW_DATE >= '" + start_date.Text.ToString() + "' and  A.FLOW_DATE <= '" + end_date.Text.ToString() + "'");

                dataFlowGrid.RowCount = dt.Rows.Count;

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataFlowGrid.Rows[i].Cells[0].Value = dt.Rows[i]["LOT_NO"].ToString();
                        dataFlowGrid.Rows[i].Cells[1].Value = dt.Rows[i]["ITEM_NM"].ToString();
                        dataFlowGrid.Rows[i].Cells[2].Value = dt.Rows[i]["INST_AMT"].ToString();
                        dataFlowGrid.Rows[i].Cells[3].Value = dt.Rows[i]["ITEM_CD"].ToString();
                        dataFlowGrid.Rows[i].Cells[4].Value = dt.Rows[i]["SPEC"].ToString();
                        dataFlowGrid.Rows[i].Cells[5].Value = dt.Rows[i]["W_INST_DATE"].ToString();
                        dataFlowGrid.Rows[i].Cells[6].Value = dt.Rows[i]["W_INST_CD"].ToString();
                        dataFlowGrid.Rows[i].Cells[7].Value = dt.Rows[i]["CHARGE_AMT"].ToString();
                        dataFlowGrid.Rows[i].Cells[8].Value = dt.Rows[i]["PACK_AMT"].ToString();
                        dataFlowGrid.Rows[i].Cells[10].Value = dt.Rows[i]["COMPLETE_YN"].ToString();
                        dataFlowGrid.Rows[i].Cells[9].Value = dt.Rows[i]["FLOW_DATE"].ToString();
                    }
                }
                else
                {
                    dataWorkGrid.Rows.Clear();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error" + e.ToString());
            }
        }

        private void getSetting() 
        {
            pnl[0] = panel_comp_01;
            pnl[1] = panel_comp_02;
            pnl[2] = panel_comp_03;
            pnl[3] = panel_comp_04;
            pnl[4] = panel_comp_05;
            pnl[5] = panel_comp_06;
            pnl[6] = panel_comp_07;
            pnl[7] = panel_comp_08;
            pnl[8] = panel_comp_09;
            pnl[9] = panel_comp_10;
            pnl[10] = panel_comp_11;
            pnl[11] = panel_comp_12;

            dgv[0] = workComp01;
            dgv[1] = workComp02;
            dgv[2] = workComp03;
            dgv[3] = workComp04;
            dgv[4] = workComp05;
            dgv[5] = workComp06;
            dgv[6] = workComp07;
            dgv[7] = workComp08;
            dgv[8] = workComp09;
            dgv[9] = workComp10;
            dgv[10] = workComp11;
            dgv[11] = workComp12;

            btn[0] = btn_comp_01;
            btn[1] = btn_comp_02;
            btn[2] = btn_comp_03;
            btn[3] = btn_comp_04;
            btn[4] = btn_comp_05;
            btn[5] = btn_comp_06;
            btn[6] = btn_comp_07;
            btn[7] = btn_comp_08;
            btn[8] = btn_comp_09;
            btn[9] = btn_comp_10;
            btn[10] = btn_comp_11;
            btn[11] = btn_comp_12;

            lbl_flow_cd[0] = lbl_flow_cd_01;
            lbl_flow_cd[1] = lbl_flow_cd_02;
            lbl_flow_cd[2] = lbl_flow_cd_03;
            lbl_flow_cd[3] = lbl_flow_cd_04;
            lbl_flow_cd[4] = lbl_flow_cd_05;
            lbl_flow_cd[5] = lbl_flow_cd_06;
            lbl_flow_cd[6] = lbl_flow_cd_07;
            lbl_flow_cd[7] = lbl_flow_cd_08;
            lbl_flow_cd[8] = lbl_flow_cd_09;
            lbl_flow_cd[9] = lbl_flow_cd_10;
            lbl_flow_cd[10] = lbl_flow_cd_11;
            lbl_flow_cd[11] = lbl_flow_cd_12;

            lbl_flow_nm[0] = lbl_flow_nm_01;
            lbl_flow_nm[1] = lbl_flow_nm_02;
            lbl_flow_nm[2] = lbl_flow_nm_03;
            lbl_flow_nm[3] = lbl_flow_nm_04;
            lbl_flow_nm[4] = lbl_flow_nm_05;
            lbl_flow_nm[5] = lbl_flow_nm_06;
            lbl_flow_nm[6] = lbl_flow_nm_07;
            lbl_flow_nm[7] = lbl_flow_nm_08;
            lbl_flow_nm[8] = lbl_flow_nm_09;
            lbl_flow_nm[9] = lbl_flow_nm_10;
            lbl_flow_nm[10] = lbl_flow_nm_11;
            lbl_flow_nm[11] = lbl_flow_nm_12;

            for (int i = 0; i < max_flow_cnt; i++) 
            {
                lbl_flow_seq[i] = new Label();
                lbl_flow_seq[i].Name = "lbl_flow_seq" + (i + 1).ToString();

                lbl_flow_pr_type[i] = new Label();
                lbl_flow_pr_type[i].Name = "lbl_flow_poor" + (i + 1).ToString();

                lbl_item_iden[i] = new Label();
                lbl_item_iden[i].Name = "lbl_item_iden" + (i + 1).ToString();
            }
            btn_work_srch.FlatAppearance.BorderSize = 0;

            for (int i = 0; i < max_flow_cnt; i++) 
            {
                btn[i].FlatAppearance.BorderSize = 0;
                btn[i].Image = (Image)(new Bitmap(btn[i].Image, new Size(20, 20)));

                dtp[i] = new DateTimePicker();
                this.dgv[i].Controls.Add(dtp[i]);
                dtp[i].Visible = false;
                dtp[i].Name = "FLOW_DATE" + (i + 1).ToString("00");
                dtp[i].Format = DateTimePickerFormat.Custom;
                dtp[i].TextChanged += new EventHandler(dtp_TextChange);

                //datagrid setting
                DataGridViewComboBoxColumn cmbColumn = new DataGridViewComboBoxColumn();
                DataGridViewCheckBoxColumn chkColumn = new DataGridViewCheckBoxColumn();

                dgv[i].Columns.Add("LOT_NO" + (i + 1).ToString("00"), "LOT_NO"); //index 0
                dgv[i].Columns.Add("SUB" + (i + 1).ToString("00"), "SUB"); //1
                dgv[i].Columns.Add("FLOW_DATE" + (i + 1).ToString("00"), "일자"); //2
                dgv[i].Columns.Add("INST_AMT" + (i + 1).ToString("00"), "수량"); //3
                dgv[i].Columns.Add("LOSS" + (i + 1).ToString("00"), "LOSS"); //4
                
                cmbColumn.ValueMember = "코드";
                cmbColumn.DisplayMember = "명칭";
                cmbColumn.HeaderText = "불량";
                cmbColumn.Name = "CMB_POOR" + (i + 1).ToString("00"); 

                dgv[i].Columns.Add(cmbColumn); //불량 , index 5
                dgv[i].Columns.Add("POOR_AMT" + (i + 1).ToString("00"), "갯수"); //index 6
                dgv[i].Columns.Add("F_STEP" + (i + 1).ToString("00"), "step");//7
                dgv[i].Columns.Add(chkColumn); //index 8

                chkColumn.HeaderText = "";
                chkColumn.Name = "CHK" + (i + 1).ToString("00");

                dgv[i].Columns["LOT_NO" + (i + 1).ToString("00")].Width = 70;

                dgv[i].Columns[1].Width = 35;
                dgv[i].Columns[2].Width = 78;
                dgv[i].Columns[3].Width = 50;
                dgv[i].Columns[4].Width = 50;
                dgv[i].Columns[5].Width = 80;
                dgv[i].Columns[6].Width = 35;
                dgv[i].Columns[7].Width = 30;
                dgv[i].Columns[8].Width = 30;

                //header 체크 박스 세팅 

                chkColumn.Width = 30;
                chkColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // add checkbox header
                Rectangle rect = dgv[i].GetCellDisplayRectangle(7, -1, true);
                // set checkbox header to center of header cell. +1 pixel to position correctly.
                rect.X = rect.Location.X + 8;
                rect.Y = rect.Location.Y + 2;

                CheckBox checkboxHeader = new CheckBox();
                checkboxHeader.Name = "HeaderCHK" + (i + 1).ToString("00");
                checkboxHeader.Size = new Size(30, 25);
                checkboxHeader.Location = rect.Location;
                checkboxHeader.BackColor = Color.Transparent;
                checkboxHeader.CheckedChanged += new EventHandler(checkboxHeader_CheckedChanged);

                dgv[i].Controls.Add(checkboxHeader);

                //header 체크 박스 끝
                dgv[i].Columns[7].Visible = false;

                dgv[i].Columns[0].Frozen = true;
                dgv[i].Columns[1].Frozen = true;
                dgv[i].Columns[2].Frozen = true;

                dgv[i].Columns[0].ReadOnly = true;
                dgv[i].Columns[1].ReadOnly = true;

                dgv[i].Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv[i].Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv[i].Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv[i].Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv[i].Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv[i].Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv[i].Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv[i].Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv[i].CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellClick);
                dgv[i].ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.grid_ColumnWidthChanged);
                dgv[i].Scroll += new System.Windows.Forms.ScrollEventHandler(this.grid_scroll);
                dgv[i].Leave += new EventHandler(this.grid_leave);
                dgv[i].EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grid_EditingControlShowing);
                dgv[i].CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellEndEdit);
            }
        }

        #region button logic

        private void btnFlow_Click(object sender, EventArgs e)
        {

            if (dgv[flow_cnt-1].Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                wnDm wDm = new wnDm();
                DataTable dt = new DataTable();
                sb.AppendLine("and LOT_NO = '" + txt_lot_no.Text.ToString() + "' ");
                dt = wDm.fn_Work_Flow_Cnt(sb.ToString());

                if (int.Parse(dt.Rows[0]["cnt"].ToString()) == 0) 
                {
                    MessageBox.Show("등록된 공정이력이 없습니다.");
                    return;
                }

                dt = wDm.fn_Work_Raw_Out_Yn(txt_lot_no.Text.ToString());
                if (dt.Rows[0]["RAW_OUT_YN"].ToString().Equals("N"))
                {
                    MessageBox.Show("자재 출고가 되지 않아 공정완료를 할 수 없습니다. \n 작업지시서에 자재출고를 설정하시기 바랍니다.");
                    return;
                }
                ComInfo comInfo = new ComInfo();
                DialogResult msgOk = MessageBox.Show("공정완료를 확정 하시겠습니까?", "경고여부", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (msgOk == DialogResult.No)
                {
                    return;
                }

                //남은 수량 불량작업지시서로 이관 하기 위한 작업 소스 
                double input_amt = 0.0;
                for(int i=0;i<dgv[flow_cnt-1].Rows.Count;i++)
                {
                    input_amt += double.Parse(dgv[flow_cnt - 1].Rows[i].Cells[3].Value.ToString());
                }

                double inst_amt = double.Parse(txt_inst_amt.Text.ToString());

                double poor_cnt = inst_amt - input_amt;
                if (poor_cnt > 0) 
                {
                    sb = new StringBuilder();
                    sb.AppendLine("지시수량보다 적게 등록이 되어");
                    sb.AppendLine(poor_cnt+"개는 불량 작업지시서로 자동 등록됩니다.");
                    MessageBox.Show(sb.ToString());
                }
                // 불량작업지시서 작업 소스 끝 

                wDm = new wnDm();
                int rsNum = wDm.update_Work_Flow(
                            txt_lot_no.Text.ToString()
                            , txt_item_cd.Text.ToString()
                            , dgv
                            , lbl_flow_cd
                            , lbl_flow_seq
                            , lbl_item_iden
                            , flow_cnt);

                if (rsNum == 0)
                {
                    int rsNum2 = wDm.update_Work_Flow_Complete(txt_lot_no.Text.ToString(), poor_cnt);

                    if (rsNum2 == 0)
                    {
                        wnProcCon wDmProc = new wnProcCon();
                        int rsNum3 = wDmProc.prod_item_stock_upd(txt_item_cd.Text.ToString());

                        workLogic(); //작업지시서 목록 
                        flowLogic(); //공정 목록

                        MessageBox.Show("공정 완료 되었습니다. ");

                        if (rsNum3 == -9) 
                        {
                            MessageBox.Show("제품 재고 조정 실패.");
                        }
                    }
                    else if (rsNum2 == 1)
                        MessageBox.Show("공정 완료에 실패하였습니다. ");
                    else
                        MessageBox.Show("Exception 에러");
                    return;
                }
                else if (rsNum == 1)
                    MessageBox.Show("저장에 실패하였습니다");
                else
                    MessageBox.Show("Exception 에러");
            }
            else 
            {
                MessageBox.Show("아직 공정이 끝나지 않았습니다. 다시 설정해주시기 바랍니다.");
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txt_lot_no.Text.ToString().Equals("")) 
            {
                MessageBox.Show("작업지시서를 선택하십시요.");
                return;
            }

            DataTable dt = new DataTable();
            wnDm wDm = new wnDm();
            StringBuilder sb = new StringBuilder();

            wDm = new wnDm();
            sb = new StringBuilder();

            //공정완료 체크
            sb.AppendLine("and LOT_NO = '" + txt_lot_no.Text.ToString() + "' ");
            sb.AppendLine("and COMPLETE_YN = 'Y' ");
            dt = wDm.fn_Work_Flow_Cnt(sb.ToString());
            if (int.Parse(dt.Rows[0]["cnt"].ToString()) > 0) 
            {
                MessageBox.Show("이미 공정완료된 작업공정입니다. ");
                return;
            }

            //insert & update 체크
            sb = new StringBuilder();
            sb.AppendLine("and LOT_NO = '" + txt_lot_no.Text.ToString() + "' ");
            dt = wDm.fn_Work_Flow_Cnt(sb.ToString());

            if (int.Parse(dt.Rows[0]["cnt"].ToString()) > 0) //update
            {
                int rsNum = wDm.update_Work_Flow(
                            txt_lot_no.Text.ToString()
                            , txt_item_cd.Text.ToString()
                            , dgv
                            , lbl_flow_cd
                            , lbl_flow_seq
                            , lbl_item_iden
                            , flow_cnt);

                if (rsNum == 0)
                {
                    wnProcCon wDmProc = new wnProcCon();
                    int rsNum2 = wDmProc.prod_item_stock_upd(txt_item_cd.Text.ToString());

                    flow_detail_logic(); // 데이터 갱신

                    workLogic(); //작업지시서 목록 
                    flowLogic(); //공정 목록

                    MessageBox.Show("성공적으로 수정하였습니다.");

                    if (rsNum2 == -9)
                    {
                        MessageBox.Show("제품재고조정 실패");
                    }
                }
                else if (rsNum == 1)
                    MessageBox.Show("저장에 실패하였습니다");
                else
                    MessageBox.Show("Exception 에러");

            }
            else //insert
            {
                int rsNum = wDm.insert_Work_Flow(
                            txt_lot_no.Text.ToString()
                            , txt_item_cd.Text.ToString()
                            , dgv
                            , lbl_flow_cd
                            , lbl_flow_seq
                            , lbl_item_iden
                            , flow_cnt);

                if (rsNum == 0)
                {
                    wnProcCon wDmProc = new wnProcCon();
                    int rsNum2 = wDmProc.prod_item_stock_upd(txt_item_cd.Text.ToString());

                    btnFlow.Enabled = true;
                    btnDelete.Enabled = true;

                    flow_detail_logic(); // 데이터 갱신

                    workLogic(); //작업지시서 목록 
                    flowLogic(); //공정 목록

                    MessageBox.Show("성공적으로 등록하였습니다.");

                    if (rsNum2 == -9)
                    {
                        MessageBox.Show("제품재고조정 실패");
                    }

                }
                else if (rsNum == 1)
                    MessageBox.Show("저장에 실패하였습니다");
                else
                    MessageBox.Show("Exception 에러");
            }
        }

        private void flow_detail_logic() 
        {
            DataTable dt = new DataTable();
            wnDm wDm = new wnDm();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" where A.ITEM_CD = '" + txt_item_cd.Text.ToString() + "' ");
            sb.AppendLine(" and B.FLOW_INSERT_YN = 'Y' ");
            dt = wDm.fn_Item_Flow_List(sb.ToString());

            flow_cnt = dt.Rows.Count;

            for (int i = 0; i < max_flow_cnt; i++) // 작업공정 최대 9 단계
            {
                pnl[i].Visible = true;
                dgv[i].Visible = true;
                btn[i].Visible = true;
                ((CheckBox)dgv[i].Controls.Find("HeaderCHK" + (i + 1).ToString("00"), true)[0]).Checked = false; //그리드 header cell 초기화
            }

            if (flow_cnt > 0)
            {
                for (int i = max_flow_cnt; i > flow_cnt; i--)
                {
                    pnl[i - 1].Visible = false;
                    dgv[i - 1].Visible = false;
                    //btn[i - 1].Visible = false;
                }

                for (int i = 0; i < flow_cnt; i++)
                {
                    lbl_flow_cd[i].Text = dt.Rows[i]["FLOW_CD"].ToString();
                    lbl_flow_nm[i].Text = dt.Rows[i]["FLOW_NM"].ToString();
                    lbl_flow_pr_type[i].Text = dt.Rows[i]["TYPE_CD"].ToString();
                    lbl_flow_seq[i].Text = dt.Rows[i]["SEQ"].ToString();
                    lbl_item_iden[i].Text = dt.Rows[i]["ITEM_IDEN_YN"].ToString();

                    //불량 타입에 따른 불량 유형 combobox 정의
                    wDm = new wnDm();
                    DataTable dt2 = new DataTable();
                    dt2 = wDm.fn_query_Poor(lbl_flow_pr_type[i].Text);

                    ((DataGridViewComboBoxColumn)dgv[i].Columns[5]).DataSource = dt2;

                }
                btn[flow_cnt - 1].Visible = false; //마지막 공정에는 이동 버튼 숨김
            }

            for (int i = 0; i < max_flow_cnt; i++)
            {
                dgv[i].Rows.Clear(); //GridView 초기화
            }

            wDm = new wnDm();
            sb = new StringBuilder();

            sb.AppendLine("and LOT_NO = '" + txt_lot_no.Text.ToString() + "' ");
            dt = wDm.fn_Work_Flow_Cnt(sb.ToString());

            if (int.Parse(dt.Rows[0]["cnt"].ToString()) > 0)
            {

                this.lbl_title.Text = "작업공정등록 - 수정";
                this.btnFlow.Enabled = true;
                this.btnDelete.Enabled = true;

                subLogic(); //첫번째 1공정 세팅 (sub number setting)


                StringBuilder sb2 = new StringBuilder();
                sb2.AppendLine(" and A.LOT_NO = '" + txt_lot_no.Text.ToString() + "' ");
                DataTable dt2 = wDm.fn_wf_LotNo_Sub_Status(sb2.ToString());
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    DataTable flow_dt = new DataTable();
                    wDm = new wnDm();
                    sb = new StringBuilder();
                    sb.AppendLine("and A.LOT_NO = '" + txt_lot_no.Text.ToString() + "' ");
                    flow_dt = wDm.fn_Work_Flow_Detail(sb.ToString());

                    if (flow_dt != null && flow_dt.Rows.Count > 0)
                    {

                        for (int i = 0; i < flow_dt.Rows.Count; i++)
                        {
                            int idx = int.Parse(flow_dt.Rows[i]["F_STEP"].ToString()) - 1; //현재 단계 진행 (2단계일 경우 INDEX는 1)
                            int rowIdx;
                            if (idx > 0) //두번째 공정일 경우 
                            {
                                dgv[idx].Rows.Add();
                                rowIdx = dgv[idx].Rows.Count - 1;
                            }
                            else //첫번째 공정일 경우 
                            {
                                int lot_sub = int.Parse(flow_dt.Rows[i]["LOT_SUB"].ToString()) - 1;
                                rowIdx = lot_sub;

                            }

                            dgv[idx].Rows[rowIdx].Cells[0].Value = flow_dt.Rows[i]["LOT_NO"].ToString();
                            dgv[idx].Rows[rowIdx].Cells[1].Value = flow_dt.Rows[i]["LOT_SUB"].ToString();
                            dgv[idx].Rows[rowIdx].Cells[2].Value = flow_dt.Rows[i]["F_SUB_DATE"].ToString();
                            dgv[idx].Rows[rowIdx].Cells[3].Value = (decimal.Parse(flow_dt.Rows[i]["F_SUB_AMT"].ToString())).ToString("#,0.######");
                            dgv[idx].Rows[rowIdx].Cells[4].Value = (decimal.Parse(flow_dt.Rows[i]["LOSS"].ToString())).ToString("#,0.######");
                            dgv[idx].Rows[rowIdx].Cells[5].Value = flow_dt.Rows[i]["POOR_CD"].ToString();
                            dgv[idx].Rows[rowIdx].Cells[6].Value = (decimal.Parse(flow_dt.Rows[i]["POOR_AMT"].ToString())).ToString("#,0.######");
                            dgv[idx].Rows[rowIdx].Cells[7].Value = flow_dt.Rows[i]["F_STEP"].ToString();
                            dgv[idx].Rows[rowIdx].Cells[8].Value = flow_dt.Rows[i]["INPUT_YN"].ToString();
                            dgv[idx].Rows[rowIdx].Cells[dgv[0].ColumnCount - 1].Value = false;
                        }
                    }
                }

            }
            else //공정 테이블에 LOT_NO가 없을 경우 
            {
                this.lbl_title.Text = "작업공정등록 - 신규";
                this.btnFlow.Enabled = false;
                this.btnDelete.Enabled = false;

                subLogic();
            }
        }
        //작업 지시 코드 가져 올 경우 
        private void btn_work_srch_Click(object sender, EventArgs e)
        {
            Popup.pop작업지시검색 frm = new Popup.pop작업지시검색();

            frm.ShowDialog();

            if (frm.sCode != "")
            {
                txt_lot_no.Text = frm.dgv.Rows[0].Cells["LOT_NO"].Value.ToString();
                txt_item_nm.Text = frm.dgv.Rows[0].Cells["ITEM_NM"].Value.ToString();
                txt_item_cd.Text = frm.dgv.Rows[0].Cells["ITEM_CD"].Value.ToString();
                txt_spec.Text = frm.dgv.Rows[0].Cells["SPEC"].Value.ToString();
                txt_work_date.Text = frm.dgv.Rows[0].Cells["W_INST_DATE"].Value.ToString();

                txt_inst_amt.Text = (decimal.Parse(frm.dgv.Rows[0].Cells["INST_AMT"].Value.ToString())).ToString("#,0.######");
                txt_char_amt.Text = (decimal.Parse(frm.dgv.Rows[0].Cells["CHARGE_AMT"].Value.ToString())).ToString("#,0.######");
                txt_pack_amt.Text = (decimal.Parse(frm.dgv.Rows[0].Cells["PACK_AMT"].Value.ToString())).ToString("#,0.######");

                flow_detail_logic();
            }
            else
            {
                //txt_lot_no.Text = "";   
            }

            frm.Dispose();
            frm = null;
        }

        private void btn_comp_01_Click(object sender, EventArgs e)
        {
            btn_logic(0);
        }

        private void btn_comp_02_Click(object sender, EventArgs e)
        {
            btn_logic(1);

        }

        private void btn_comp_03_Click(object sender, EventArgs e)
        {
            btn_logic(2);
        }

        private void btn_comp_04_Click(object sender, EventArgs e)
        {
            btn_logic(3);
        }

        private void btn_comp_05_Click(object sender, EventArgs e)
        {
            btn_logic(4);
        }

        private void btn_comp_06_Click(object sender, EventArgs e)
        {
            btn_logic(5);
        }

        private void btn_comp_07_Click(object sender, EventArgs e)
        {
            btn_logic(6);
        }

        private void btn_comp_08_Click(object sender, EventArgs e)
        {
            btn_logic(7);
        }

        private void btn_comp_09_Click(object sender, EventArgs e)
        {
            btn_logic(8);
        }

        private void btn_comp_10_Click(object sender, EventArgs e)
        {
            btn_logic(9);

        }

        private void btn_comp_11_Click(object sender, EventArgs e)
        {
            btn_logic(10);
        }

        private void btn_comp_12_Click(object sender, EventArgs e)
        {
            btn_logic(11);
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            ComInfo comInfo = new ComInfo();
            DialogResult msgOk = comInfo.deleteConfrim("공정등록", txt_lot_no.Text.ToString());

            if (msgOk == DialogResult.No)
            {
                return;
            }

            wnDm wDm = new wnDm();
            int rsNum = wDm.deleteWorkFlow(txt_lot_no.Text.ToString());

            if (rsNum == 0)
            {
                flow_cnt = 0; //해당 공정 카운트 리셋

                btnFlow.Enabled = false;
                btnDelete.Enabled = false;

                txt_lot_no.Text = "";
                txt_item_nm.Text = "";
                txt_item_cd.Text = "";
                txt_spec.Text = "";
                txt_work_date.Text = "";

                txt_inst_amt.Text = "0";
                txt_char_amt.Text = "0";
                txt_pack_amt.Text = "0";

                for (int i = 0; i < flow_cnt; i++)
                {
                    lbl_flow_cd[i].Text = "";
                    lbl_flow_nm[i].Text = "";
                    lbl_flow_pr_type[i].Text = "";
                    lbl_flow_seq[i].Text = "";
                }
                //btn[flow_cnt - 1].Visible = false;

                for (int i = 0; i < max_flow_cnt; i++)
                {
                    dgv[i].Rows.Clear(); //GridView 초기화
                }

                gridList();

                MessageBox.Show("성공적으로 삭제하였습니다.");
            }
        }

        #endregion button logic

        #region data logic

        private void btn_logic(int idx) 
        {
            int chk = 0;
            for (int i = 0; i < dgv[idx].Rows.Count; i++) 
            {
                bool chk2 = false; 

                int col_cnt = dgv[idx].ColumnCount - 1;
                if ((bool)dgv[idx].Rows[i].Cells[col_cnt].Value == true) 
                {
                    if (dgv[idx + 1].Rows.Count > 0) //해당 공정 테이블의 LotNo와 LotSub가 다음 공정 테이블에 Lotno와 Lotsub에 있을 경우
                    {
                        for (int j = 0; j < dgv[idx + 1].Rows.Count; j++)
                        {
                            string lot_no = (string)dgv[idx].Rows[i].Cells[0].Value;
                            string lot_sub = (string)dgv[idx].Rows[i].Cells[1].Value;

                            string nf_lot_no = (string)dgv[idx + 1].Rows[j].Cells[0].Value; //next flow 
                            string nf_lot_sub = (string)dgv[idx + 1].Rows[j].Cells[1].Value;

                            if (lot_no.ToString().Equals(nf_lot_no) && lot_sub.ToString().Equals(nf_lot_sub))
                            {
                                chk2 = true;
                                chk = 1;
                                break;
                            }
                        }
                    }

                    if (chk2 == false) //중복이 아닐 시 
                    {

                        double flow_amt = double.Parse((string)dgv[idx].Rows[i].Cells[3].Value);
                        double loss = double.Parse((string)dgv[idx].Rows[i].Cells[4].Value);
                        double poor_amt = 0.0;

                        if ((string)dgv[idx].Rows[i].Cells[5].Value != ""
                            && dgv[idx].Rows[i].Cells[5].Value != null)
                        {
                            poor_amt = double.Parse((string)dgv[idx].Rows[i].Cells[6].Value);

                        }

                        double rs_flow_amt = flow_amt - loss - poor_amt;
                        dgv[idx].Rows[i].Cells[3].Value = (decimal.Parse((rs_flow_amt).ToString())).ToString("#,0.######");
                        dgv[idx].Rows[i].Cells[4].Value = (decimal.Parse((loss).ToString())).ToString("#,0.######");

                        dgv[idx + 1].Rows.Add();
                        dgv[idx + 1].Rows[dgv[idx + 1].Rows.Count - 1].Cells[dgv[idx + 1].ColumnCount - 1].Value = false; //다음 공정 테이블 combo 초기화

                        dgv[idx + 1].Rows[dgv[idx + 1].Rows.Count - 1].Cells[0].Value = dgv[idx].Rows[i].Cells[0].Value;
                        dgv[idx + 1].Rows[dgv[idx + 1].Rows.Count - 1].Cells[1].Value = dgv[idx].Rows[i].Cells[1].Value;
                        dgv[idx + 1].Rows[dgv[idx + 1].Rows.Count - 1].Cells[2].Value = dgv[idx].Rows[i].Cells[2].Value;
                        dgv[idx + 1].Rows[dgv[idx + 1].Rows.Count - 1].Cells[3].Value = dgv[idx].Rows[i].Cells[3].Value;
                        dgv[idx + 1].Rows[dgv[idx + 1].Rows.Count - 1].Cells[4].Value = "0";
                        dgv[idx + 1].Rows[dgv[idx + 1].Rows.Count - 1].Cells[6].Value = "0";

                        chk = 1;
                    }
                }
            }

            if (chk == 0)
            {
                MessageBox.Show("체크한 데이터가 없습니다.");
            }
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            conDataGridView grd = (conDataGridView)sender;
            int idx = int.Parse(grd.Name.Substring(grd.Name.Length - 2))-1; //그리드뷰 명칭의 끝 가져오기, workComp01 ~ workComp12 까지 지정되어 있음

            switch (grd.Columns[e.ColumnIndex].HeaderText)
            {
                case "일자":

                    _Retangle = grd.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    dtp[idx].Size = new Size(_Retangle.Width, _Retangle.Height);
                    dtp[idx].Location = new Point(_Retangle.X, _Retangle.Y);
                    dtp[idx].Visible = true;

                    string flow_time = (string)grd.Rows[e.RowIndex].Cells[2].Value;
                    if (flow_time != "" && flow_time != null)
                    {
                        dtp[idx].Text = (string)grd.Rows[e.RowIndex].Cells[2].Value;
                    }
                    else
                    {
                        dtp[idx].Text = DateTime.Today.ToString("yyyy-MM-dd");
                    }
                    break;

                default:
                    dtp[idx].Visible = false;
                    break;
            }
        }

        private void grid_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            dtpVisible(sender, false);

        }

        private void grid_scroll(object sender, ScrollEventArgs e)
        {
            dtpVisible(sender, false);
        }

        private void grid_leave(object sender, EventArgs e)
        {
            dtpVisible(sender, false);
        }

        private void dtp_TextChange(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            int idx = int.Parse(dtp.Name.Substring(dtp.Name.Length - 2)) - 1; //그리드뷰 명칭의 끝 가져오기, workComp01 ~ workComp09 까지 지정되어 있음
            //MessageBox.Show(dgv[idx].Text.ToString());
            dgv[idx].CurrentCell.Value = dtp.Text.ToString();

            //grd.CurrentCell.Value = dtp[idx].Text.ToString();
        }

        private void dtpVisible(object sender, bool chk) 
        {
            conDataGridView grd = (conDataGridView)sender;
            int idx = int.Parse(grd.Name.Substring(grd.Name.Length - 2)) - 1; //그리드뷰 명칭의 끝 가져오기, workComp01 ~ workComp09 까지 지정되어 있음

            dtp[idx].Visible = chk;
        }

        #endregion data logic

        //숫자만 입력
        private void grid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            conDataGridView grd = (conDataGridView)sender;
            int idx = grd.CurrentCell.ColumnIndex;

            if (idx == 3 || idx == 4 || idx == 6)
            {
                e.Control.KeyPress += new KeyPressEventHandler(txtCheckNumeric_KeyPress);
            }
            else
            {
                e.Control.KeyPress -= new KeyPressEventHandler(txtCheckNumeric_KeyPress);
            }
        }


        private void grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //conDataGridView grd = (conDataGridView)sender;
            //DataGridViewCell cell = grd[e.ColumnIndex, e.RowIndex];

            //cell.Style.BackColor = Color.White;

            //int idx = grd.CurrentCell.ColumnIndex;

            //if (idx == 3 || idx == 4 || idx == 6)
            //{
            //    double flow_amt = double.Parse((string)grd.Rows[e.RowIndex].Cells[3].Value);
            //    double loss = double.Parse((string)grd.Rows[e.RowIndex].Cells[4].Value);
            //    double poor_amt = 0.0;

            //    MessageBox.Show((string)grd.Rows[e.RowIndex].Cells[4].Value);
            //    if ((string)grd.Rows[e.RowIndex].Cells[5].Value != ""
            //        && grd.Rows[e.RowIndex].Cells[5].Value != null)
            //    {
            //        poor_amt = double.Parse((string)grd.Rows[e.RowIndex].Cells[6].Value);

            //    }

            //    double rs_flow_amt = flow_amt - loss - poor_amt;
            //    grd.Rows[e.RowIndex].Cells[3].Value = (decimal.Parse((rs_flow_amt).ToString())).ToString("#,0.######");
            //}
        }

        #region 공통 그리드 체크

        #endregion 공통 그리드 체크
        private void txtCheckNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e);
        }

        private void dataWorkGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView grd = (DataGridView)sender;
            txt_lot_no.Text = grd.Rows[e.RowIndex].Cells[0].Value.ToString();
            txt_item_nm.Text = grd.Rows[e.RowIndex].Cells[1].Value.ToString();
            txt_item_cd.Text = grd.Rows[e.RowIndex].Cells[3].Value.ToString();
            txt_spec.Text = grd.Rows[e.RowIndex].Cells[4].Value.ToString();
            txt_work_date.Text = grd.Rows[e.RowIndex].Cells[5].Value.ToString();

            txt_inst_amt.Text = (decimal.Parse(grd.Rows[e.RowIndex].Cells[2].Value.ToString())).ToString("#,0.######");
            txt_char_amt.Text = (decimal.Parse(grd.Rows[e.RowIndex].Cells[7].Value.ToString())).ToString("#,0.######");
            txt_pack_amt.Text = (decimal.Parse(grd.Rows[e.RowIndex].Cells[8].Value.ToString())).ToString("#,0.######");

            flow_detail_logic(); //작업
            grd.CurrentCell = grd[0, e.RowIndex];
        }

        private void tbWorkControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbFlowControl.SelectedIndex == 0)
            {
                workLogic();
            }
            else
            {
                start_date.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
                flowLogic();
            }
        }

        private void dataFlowGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView grd = (DataGridView)sender;
            txt_lot_no.Text = grd.Rows[e.RowIndex].Cells[0].Value.ToString();
            txt_item_nm.Text = grd.Rows[e.RowIndex].Cells[1].Value.ToString();
            txt_item_cd.Text = grd.Rows[e.RowIndex].Cells[3].Value.ToString();
            txt_spec.Text = grd.Rows[e.RowIndex].Cells[4].Value.ToString();
            txt_work_date.Text = grd.Rows[e.RowIndex].Cells[5].Value.ToString();

            txt_inst_amt.Text = (decimal.Parse(grd.Rows[e.RowIndex].Cells[2].Value.ToString())).ToString("#,0.######");
            txt_char_amt.Text = (decimal.Parse(grd.Rows[e.RowIndex].Cells[7].Value.ToString())).ToString("#,0.######");
            txt_pack_amt.Text = (decimal.Parse(grd.Rows[e.RowIndex].Cells[8].Value.ToString())).ToString("#,0.######");

            flow_detail_logic();
            grd.CurrentCell = grd[0, e.RowIndex];

        }

        private void btnSrch_Click(object sender, EventArgs e)
        {
            flowLogic();
        }


        private void checkboxHeader_CheckedChanged(object sender, EventArgs e) {

            CheckBox chk = (CheckBox)sender;
            string str = chk.Name;
            int idx = int.Parse(chk.Name.Substring(chk.Name.Length - 2)) - 1;

            if (dgv[idx].Rows.Count > 0)
            {
                for (int i = 0; i < dgv[idx].RowCount; i++)
                {
                    dgv[idx].Rows[i].Cells[dgv[idx].ColumnCount-1].Value = chk.Checked;
                }
                dgv[idx].EndEdit();
            }
            else 
            {
                MessageBox.Show("데이터가 없습니다.");
            }
        }

        private void subLogic() 
        {
            double d_inst_amt = double.Parse(txt_inst_amt.Text.ToString());
            double d_char_amt = double.Parse(txt_char_amt.Text.ToString());

            double rs_div = 0.0; //나누기 결과 값 -> 13(수량)/2(포장수량) = 6(결과 값)
            double rs_remain = 0.0; // 나누기 나머지 값 -> 13(수량)% 6(포장수량) = 1 (나머지 값)

            if (d_char_amt == 0) //장입수량이 0일 경우 
            {
                rs_div = 0;
                rs_remain = d_inst_amt;
            }
            else
            {
                rs_div = d_inst_amt / d_char_amt;
                rs_remain = d_inst_amt % d_char_amt;
            }

            if (rs_remain > 0)
            {
                for (int i = 0; i < rs_div - 1; i++) //나누기 결과 값이 6이면 6번을 돌린다. 
                {
                    dgv[0].Rows.Add();
                    dgv[0].Rows[dgv[0].Rows.Count - 1].Cells[dgv[0].ColumnCount - 1].Value = false; //체크 값 초기화

                    dgv[0].Rows[i].Cells[0].Value = txt_lot_no.Text;
                    dgv[0].Rows[i].Cells[1].Value = (i + 1).ToString();
                    dgv[0].Rows[i].Cells[2].Value = DateTime.Today.ToString("yyyy-MM-dd");
                    dgv[0].Rows[i].Cells[3].Value = d_char_amt.ToString("#,0.######"); //장입 수량
                    dgv[0].Rows[i].Cells[4].Value = "0"; //장입 수량
                    dgv[0].Rows[i].Cells[6].Value = "0"; //장입 수량
                }
                if (rs_remain > 0)
                {
                    dgv[0].Rows.Add();
                    dgv[0].Rows[dgv[0].Rows.Count - 1].Cells[dgv[0].ColumnCount - 1].Value = false;

                    dgv[0].Rows[dgv[0].Rows.Count - 1].Cells[0].Value = txt_lot_no.Text;
                    dgv[0].Rows[dgv[0].Rows.Count - 1].Cells[1].Value = dgv[0].Rows.Count.ToString();
                    dgv[0].Rows[dgv[0].Rows.Count - 1].Cells[2].Value = DateTime.Today.ToString("yyyy-MM-dd");
                    dgv[0].Rows[dgv[0].Rows.Count - 1].Cells[3].Value = rs_remain.ToString("#,0.######"); //나머지 수량
                    dgv[0].Rows[dgv[0].Rows.Count - 1].Cells[4].Value = "0"; //나머지 수량
                    dgv[0].Rows[dgv[0].Rows.Count - 1].Cells[6].Value = "0"; //나머지 수량
                }
                else
                {
                    dgv[0].Rows.Add();
                    dgv[0].Rows[dgv[0].Rows.Count - 1].Cells[dgv[0].ColumnCount - 1].Value = false;

                    dgv[0].Rows[dgv[0].Rows.Count - 1].Cells[0].Value = txt_lot_no.Text;
                    dgv[0].Rows[dgv[0].Rows.Count - 1].Cells[1].Value = dgv[0].Rows.Count.ToString();
                    dgv[0].Rows[dgv[0].Rows.Count - 1].Cells[2].Value = DateTime.Today.ToString("yyyy-MM-dd");
                    dgv[0].Rows[dgv[0].Rows.Count - 1].Cells[3].Value = d_char_amt.ToString("#,0.######"); //장입 수량
                    dgv[0].Rows[dgv[0].Rows.Count - 1].Cells[4].Value = "0"; //나머지 수량
                    dgv[0].Rows[dgv[0].Rows.Count - 1].Cells[6].Value = "0"; //나머지 수량
                }
            }
            else
            {
                if (rs_div > 1)
                {
                    for (int i = 0; i < rs_div; i++) //나누기 결과 값이 6이면 6번을 돌린다. 
                    {
                        dgv[0].Rows.Add();
                        dgv[0].Rows[dgv[0].Rows.Count - 1].Cells[dgv[0].ColumnCount - 1].Value = false; //체크 값 초기화

                        dgv[0].Rows[i].Cells[0].Value = txt_lot_no.Text;
                        dgv[0].Rows[i].Cells[1].Value = (i + 1).ToString();
                        dgv[0].Rows[i].Cells[2].Value = DateTime.Today.ToString("yyyy-MM-dd");
                        dgv[0].Rows[i].Cells[3].Value = d_char_amt.ToString("#,0.######"); //장입 수량
                        dgv[0].Rows[i].Cells[4].Value = "0"; //장입 수량
                        dgv[0].Rows[i].Cells[6].Value = "0"; //장입 수량
                    }
                }
                else
                {
                    dgv[0].Rows.Add();
                    dgv[0].Rows[dgv[0].Rows.Count - 1].Cells[dgv[0].ColumnCount - 1].Value = false;

                    dgv[0].Rows[dgv[0].Rows.Count - 1].Cells[0].Value = txt_lot_no.Text;
                    dgv[0].Rows[dgv[0].Rows.Count - 1].Cells[1].Value = dgv[0].Rows.Count.ToString();
                    dgv[0].Rows[dgv[0].Rows.Count - 1].Cells[2].Value = DateTime.Today.ToString("yyyy-MM-dd");
                    dgv[0].Rows[dgv[0].Rows.Count - 1].Cells[3].Value = d_inst_amt.ToString("#,0.######"); //전체 수량
                    dgv[0].Rows[dgv[0].Rows.Count - 1].Cells[4].Value = "0"; //나머지 수량
                    dgv[0].Rows[dgv[0].Rows.Count - 1].Cells[6].Value = "0"; //나머지 수량
                }
            }
        }
    }
}
