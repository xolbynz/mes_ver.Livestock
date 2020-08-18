using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 스마트팩토리.CLS;

namespace 스마트팩토리.P50_QUA
{
    public partial class frm제품검사성적서등록 : Form
    {
        wnGConstant wConst = new wnGConstant();
        Image image;
        string path;
        int startIdx = 0; //실측치 시작 인덱스

        public frm제품검사성적서등록()
        {
            InitializeComponent();
        }

        private void frm제품검사성적서등록_Load(object sender, EventArgs e)
        {
            DataGridViewComboBoxColumn cmbColumn = new DataGridViewComboBoxColumn();

            wnDm wDm = new wnDm();
            DataTable dt = wDm.fn_query_com_code("800");

            cmbColumn.ValueMember = "코드";
            cmbColumn.DisplayMember = "명칭";
            cmbColumn.DataSource = dt;
            cmbColumn.HeaderText = "판정";
            cmbColumn.Name = "GRADE";

            dataChkGrid.Columns.Add(cmbColumn);
            dataChkGrid.Columns[dataChkGrid.Columns.Count - 1].Width = 70;

            startIdx = dataChkGrid.ColumnCount;

            chk_req_list();

            tlp_item.SetColumnSpan(label17, 2);
            tlp_item.SetColumnSpan(cmb_item_pass, 2);
            //tlp_item.SetColumnSpan(btn_pass, 2);

            init_ComboBox();
        }

        private void init_ComboBox()
        {
            ComInfo comInfo = new ComInfo();
            string sqlQuery = "";

            cmb_item_pass.ValueMember = "코드";
            cmb_item_pass.DisplayMember = "명칭";
            sqlQuery = comInfo.queryChkPass();
            wConst.ComboBox_Read_Blank(cmb_item_pass, sqlQuery);
        }

        #region button logic

        private void btnComplete_Click(object sender, EventArgs e)
        {
            if (txt_check_yn.Text.ToString().Equals("N")) //미완료
            {
                cmb_item_pass.SelectedValue = "";
                lbl_pass_lot.Text = txt_lot_no.Text.ToString() + " - " + txt_lot_sub.Text.ToString();
                lbl_pass_item_nm.Text = txt_item_nm.Text.ToString();
                tlp_item.Visible = true;
            }
            else if (txt_check_yn.Text.ToString().Equals("Y")) //완료
                MessageBox.Show("이미 공정검사가 완료되었습니다. ");
            else if (txt_check_yn.Text.ToString().Equals("S")) //대기
                MessageBox.Show("공정검사가 등록되지 않아 먼저 등록하시기 바랍니다.");
            else if (txt_check_yn.Text.ToString().Equals("O")) //생략 
                MessageBox.Show("생략된 공정검사는 검사완료 할 수 없습니다.");
            else
                MessageBox.Show("데이터가 존재하지 않습니다. ");
        }

        private void btnOmit_Click(object sender, EventArgs e)
        {
            if (tbItemControl.SelectedIndex == 0)
            {
                if (ReqChkList.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    bool chk = false;

                    for (int i = 0; i < ReqChkList.Rows.Count; i++)
                    {
                        if ((bool)ReqChkList.Rows[i].Cells[0].Value == true)
                        {
                            if (ReqChkList.Rows[i].Cells[11].Value.ToString().Equals("S"))
                            {
                                sb.AppendLine(" update F_WORK_FLOW_DETAIL ");
                                sb.AppendLine(" set ITEM_CHECK_YN = 'O' ");
                                sb.AppendLine(" where LOT_NO = '" + ReqChkList.Rows[i].Cells[2].Value + "' ");
                                sb.AppendLine("     and LOT_SUB = '" + ReqChkList.Rows[i].Cells[3].Value + "' ");
                                sb.AppendLine("     and F_STEP = '" + ReqChkList.Rows[i].Cells[10].Value + "' ");
                                chk = true;
                            }
                        }
                    }

                    if (chk == true)
                    {
                        DataTable dt = new DataTable();
                        wnDm wDm = new wnDm();
                        int rsNum = wDm.updateChkOmit(sb);

                        if (rsNum == 0)
                        {
                            chk_req_list();
                            MessageBox.Show("생략되었습니다.");
                        }
                        else if (rsNum == 1)
                            MessageBox.Show("저장에 실패하였습니다");
                        else
                            MessageBox.Show("Exception 에러2");
                    }
                    else
                    {
                        MessageBox.Show("생략데이터가 없습니다. (대기중일 경우에만 생략 가능)");
                    }

                    for (int i = 0; i < ReqChkList.Rows.Count; i++)
                    {
                        ReqChkList.Rows[i].Cells[0].Value = false;
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dataChkGrid.Rows.Count == 0)
            {
                MessageBox.Show("검사항목이 없어 저장이 불가능합니다.");
                return;
            }
            saveLogic();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataChkGrid.Rows.Count > 0)
            {
                DialogResult msgOk = MessageBox.Show("데이터가 존재합니다. \n 창을 닫으시겠습니까?", "확인여부", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (msgOk == DialogResult.No)
                {
                    return;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (dataChkGrid.Rows.Count > 0)
            {
                DialogResult msgOk = MessageBox.Show("데이터가 존재합니다. \n 창을 닫으시겠습니까?", "확인여부", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (msgOk == DialogResult.No)
                {
                    return;
                }
            }
            this.Close();
        }

        private void btn_pass_Click(object sender, EventArgs e)
        {
            if (cmb_item_pass.SelectedValue == null) cmb_item_pass.SelectedValue = "";

            if ((string)cmb_item_pass.SelectedValue == "")
            {
                MessageBox.Show("검사결과를 선택하시기 바랍니다.");
                return;
            }

            wnDm wDm = new wnDm();
            int rsNum = wDm.updateItemChkPass(txt_lot_no.Text.ToString()
                                              , txt_lot_sub.Text.ToString()
                                              , txt_f_step.Text.ToString()
                                              , cmb_item_pass.SelectedValue.ToString());
            if (rsNum == 0)
            {
                chk_req_list();
                chk_complete_list();

                txt_check_nm.Text = "완료";
                txt_check_yn.Text = "Y";
                MessageBox.Show("검사가 완료되었습니다.");
            }
            else if (rsNum == 1)
                MessageBox.Show("저장에 실패하였습니다");
            else
                MessageBox.Show("Exception 에러2");

            tlp_item.Visible = false;
        }

        private void btn_file_up_Click(object sender, EventArgs e)
        {
            pic_logic(pic_exam);
        }

        #endregion button logic 

        #region item chk logic
        private void chk_req_list()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("and (B.ITEM_CHECK_YN = 'S' or B.ITEM_CHECK_YN = 'N') "); //검사현황 S 대기, N 검사 등록 후 미완료 , Y 검사 완료 , O 검사 생략

            item_chk_logic(ReqChkList, sb.ToString());
        }

        private void chk_complete_list()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("and B.ITEM_CHECK_YN = 'Y'  "); //검사현황 S 대기, N 검사 등록 후 미완료 , Y 검사 완료 , O 검사 패스 

            item_chk_logic(CompChkList, sb.ToString());
        }

        private void chk_omit_list()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("and B.ITEM_CHECK_YN = 'O'  "); //검사현황 S 대기, N 검사 등록 후 미완료 , Y 검사 완료 , O 검사 패스 

            item_chk_logic(omitChkList, sb.ToString());
        }

        private void item_chk_logic(DataGridView dgv, string condition)
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("and B.COMPLETE_YN = 'N' ");
                sb.AppendLine("and D.ITEM_IDEN_YN = 'Y' "); //제품식별표 검사 여부
                sb.AppendLine(condition);

                dt = wDm.fn_Item_Chk_Req_List(sb.ToString());

                if (dt != null && dt.Rows.Count > 0)
                {
                    dgv.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        dgv.Rows[i].Cells[0].Value = false;

                        dgv.Rows[i].Cells[1].Value = dt.Rows[i]["F_SUB_DATE"].ToString();
                        dgv.Rows[i].Cells[2].Value = dt.Rows[i]["LOT_NO"].ToString();
                        dgv.Rows[i].Cells[3].Value = dt.Rows[i]["LOT_SUB"].ToString();
                        dgv.Rows[i].Cells[4].Value = dt.Rows[i]["ITEM_NM"].ToString();
                        dgv.Rows[i].Cells[5].Value = dt.Rows[i]["FLOW_NM"].ToString();

                        dgv.Rows[i].Cells[6].Value = (decimal.Parse(dt.Rows[i]["F_SUB_AMT"].ToString())).ToString("#,0.######");
                        dgv.Rows[i].Cells[7].Value = dt.Rows[i]["ITEM_CHECK_NM"].ToString();
                        dgv.Rows[i].Cells[8].Value = dt.Rows[i]["ITEM_CD"].ToString();
                        dgv.Rows[i].Cells[9].Value = dt.Rows[i]["FLOW_CD"].ToString();
                        dgv.Rows[i].Cells[10].Value = dt.Rows[i]["F_STEP"].ToString();
                        dgv.Rows[i].Cells[11].Value = dt.Rows[i]["ITEM_CHECK_YN"].ToString();
                        if (dt.Rows[i]["PASS_YN"].ToString().Equals("Y"))
                        {
                            dgv.Rows[i].Cells[12].Value = "합격";
                        }
                        else if (dt.Rows[i]["PASS_YN"].ToString().Equals("N"))
                            dgv.Rows[i].Cells[12].Value = "불합격";
                        else dgv.Rows[i].Cells[12].Value = "";

                        dgv.Rows[i].Cells[13].Value = dt.Rows[i]["PASS_YN"].ToString();
                    }
                }
                else
                {
                    dgv.Rows.Clear();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("시스템 에러" + e.ToString());
            }
        }
        private void resetSetting()
        {

        }

        private void saveLogic()
        {

            wnDm wDm = new wnDm();
            if (txt_check_yn.Text.ToString().Equals("S")) //대기
            {
                lblSearch.Text = "등록중";
                lblSearch.Visible = true;
                Application.DoEvents();

                byte[] flow_img;
                int flow_img_size = 0;
                if (path != null && path != "")
                {
                    flow_img = ComInfo.GetImage(path);
                    flow_img_size = flow_img.Length;
                }
                else
                {
                    flow_img = null;
                    flow_img_size = 0;
                }

                int rsNum = wDm.insertItemChkExam(txt_lot_no.Text.ToString()
                                                  , txt_lot_sub.Text.ToString()
                                                  , txt_f_step.Text.ToString()
                                                  , txt_item_cd.Text.ToString()
                                                  , txt_sub_amt.Text.ToString()
                                                  , txt_measure_cnt.Text.ToString()
                                                  , startIdx
                                                  , lblSearch
                                                  , flow_img
                                                  , flow_img_size
                                                  , dataChkGrid);
                if (rsNum == 0)
                {
                    chk_req_list();

                    txt_check_nm.Text = "미완료";
                    txt_check_yn.Text = "N";

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

                lblSearch.Visible = false;
            }
            else if (txt_check_yn.Text.ToString().Equals("N"))  // 미완료
            {
                byte[] flow_img;
                int flow_img_size = 0;

                if (path != null && path != "")
                {
                    flow_img = ComInfo.GetImage(path);
                    flow_img_size = flow_img.Length;

                    //flow_img = ImageToByte(pic_exam.Image);
                    //flow_img_size = flow_img.Length;
                }
                else
                {
                    flow_img = null;
                    flow_img_size = 0;
                }

                lblSearch.Text = "등록중";
                lblSearch.Visible = true;
                Application.DoEvents();

                int rsNum = wDm.updateItemChkExam(txt_lot_no.Text.ToString()
                                                  , txt_lot_sub.Text.ToString()
                                                  , txt_f_step.Text.ToString()
                                                  , startIdx
                                                  , lblSearch
                                                  , flow_img
                                                  , flow_img_size
                                                  , dataChkGrid);
                if (rsNum == 0)
                {
                    chk_req_list();

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

                lblSearch.Visible = false;
            }
        }
        #region 제품검사 더블클릭시 로직 (
        //제품검사 더블클릭 시 로직
        private void item_chk_data_logic(DataGridView dgv, DataGridViewCellEventArgs e, bool change_chk)
        {
            wnDm wDm = new wnDm();
            DataTable dt = new DataTable();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("     and A.LOT_NO = '" + dgv.Rows[e.RowIndex].Cells[2].Value.ToString() + "' ");
            sb.AppendLine("     and B.LOT_SUB = '" + dgv.Rows[e.RowIndex].Cells[3].Value.ToString() + "' ");
            sb.AppendLine("     and B.F_STEP = '" + dgv.Rows[e.RowIndex].Cells[10].Value.ToString() + "' ");
            //sb.AppendLine("     and B.FLOW_CD = '" + dgv.Rows[e.RowIndex].Cells[9].Value.ToString() + "' ");

            dt = wDm.fn_Item_Chk_Main_List(sb.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                txt_chk_date.Text = dt.Rows[0]["F_CHK_DATE"].ToString();
                txt_lot_no.Text = dt.Rows[0]["LOT_NO"].ToString();
                txt_lot_sub.Text = dt.Rows[0]["LOT_SUB"].ToString();
                txt_item_cd.Text = dt.Rows[0]["ITEM_CD"].ToString();
                txt_item_nm.Text = dt.Rows[0]["ITEM_NM"].ToString();
                txt_spec.Text = dt.Rows[0]["SPEC"].ToString();
                txt_flow_cd.Text = dt.Rows[0]["FLOW_CD"].ToString();
                txt_flow_nm.Text = dt.Rows[0]["FLOW_NM"].ToString();

                txt_item_gubun_nm.Text = dt.Rows[0]["ITEM_GUBUN_NM"].ToString();
                txt_sub_amt.Text = (decimal.Parse(dt.Rows[0]["F_SUB_AMT"].ToString())).ToString("#,0.######");

                txt_measure_cnt.Text = dt.Rows[0]["MEASURE_CNT"].ToString();
                //txt_eva_gubun_nm.Text = dt.Rows[0]["EVA_GUBUN_NM"].ToString();
                txt_check_nm.Text = dt.Rows[0]["ITEM_CHECK_NM"].ToString();
                txt_check_yn.Text = dt.Rows[0]["ITEM_CHECK_YN"].ToString();
                txt_f_step.Text = dt.Rows[0]["F_STEP"].ToString();

                if (dt.Rows[0]["PASS_YN"].ToString().Equals("Y"))
                {
                    txt_pass.Text = "합격";
                }
                else if (dt.Rows[0]["PASS_YN"].ToString().Equals("N"))
                {
                    txt_pass.Text = "불합격";
                }
                else
                {
                    txt_pass.Text = "";
                }

                if (int.Parse(dt.Rows[0]["MAP_SIZE"].ToString()) > 0)
                {
                    byte[] rs = (byte[])dt.Rows[0]["MAP"]; // byte to string Encoding.UTF8.GetBytes(dt.Rows[0]["MAP"].ToString());
                    MemoryStream ms = new MemoryStream(rs);
                    Image img = Image.FromStream(ms);

                    Image cus_img = ComInfo.pic_resize_logic(pic_exam, img);

                    pic_exam.BackgroundImage = cus_img;
                }
                else 
                {
                    pic_exam.Image = null;
                }

                flow_chk_detail(dataChkGrid, dgv.Rows[e.RowIndex].Cells[8].Value.ToString(), dgv.Rows[e.RowIndex].Cells[9].Value.ToString(), change_chk, 2);
            }
            else
            {
                MessageBox.Show("데이터 일시 오류 \n 다시 더블클릭 해주시기 바랍니다. ");
                return;
            }
        }

        private void flow_chk_detail(DataGridView main_dgv, string item_cd, string flow_cd, bool change_chk, int gbn)  // 1 -> 대기 , 2-> 미완료 혹은 완료
        {
            if (change_chk == true)
            {
                for (int i = 0; i < main_dgv.Rows.Count; i++)
                {
                    main_dgv.Rows[i].Cells[startIdx - 1].Value = "";
                }

                if (main_dgv.Columns.Count > startIdx)  //실측치 수 컬럼이 있을 때 제거 후 다시 생성
                {
                    for (int i = main_dgv.Columns.Count - 1; i >= startIdx; i--)
                    {
                        main_dgv.Columns.RemoveAt(main_dgv.Columns.Count - 1);
                    }
                }
                int measure_cnt = int.Parse(txt_measure_cnt.Text.ToString());
                if (measure_cnt > 0)
                {
                    for (int j = 0; j < measure_cnt; j++)
                    {
                        main_dgv.Columns.Add("CHK" + (j + 1).ToString(), (j + 1).ToString());
                        main_dgv.Columns[j + startIdx].Width = 40;
                    }
                }
            }

            if (gbn == 1) //대기
            {
                //쿼리 시작 
                wnDm wDm = new wnDm();
                DataTable dt = new DataTable();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" and A.ITEM_CD = '" + item_cd + "' ");
                dt = wDm.fn_Item_Chk_Detail_List(sb.ToString(), gbn);

                main_dgv.RowCount = dt.Rows.Count;
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        main_dgv.Rows[i].Cells["CHK_CD"].Value = dt.Rows[i]["CHK_CD"].ToString();
                        main_dgv.Rows[i].Cells["CHK_ORD"].Value = (i + 1).ToString(); // dt.Rows[i]["CHK_ORD"].ToString();
                        main_dgv.Rows[i].Cells["CHK_NM"].Value = dt.Rows[i]["CHK_NM"].ToString();
                        main_dgv.Rows[i].Cells["CHK_LOC"].Value = dt.Rows[i]["CHK_LOC"].ToString();
                        main_dgv.Rows[i].Cells["RULE_SIZE"].Value = dt.Rows[i]["RULE_SIZE"].ToString();
                        main_dgv.Rows[i].Cells["RULE_LIMIT"].Value = dt.Rows[i]["RULE_LIMIT"].ToString();
                        main_dgv.Rows[i].Cells["MEASURE_APP"].Value = dt.Rows[i]["MEASURE_APP"].ToString();
                        main_dgv.Rows[i].Cells["EVA_GUBUN_NM"].Value = dt.Rows[i]["EVA_GUBUN_NM"].ToString();

                        //main_dgv.Rows[i].Cells["UPPER_SIZE"].Value = (decimal.Parse(dt.Rows[i]["UPPER_SIZE"].ToString())).ToString("#,0.######");
                        //main_dgv.Rows[i].Cells["LOWER_SELF"].Value = (decimal.Parse(dt.Rows[i]["LOWER_SELF"].ToString())).ToString("#,0.######");
                        //main_dgv.Rows[i].Cells["UPPER_SELF"].Value = (decimal.Parse(dt.Rows[i]["UPPER_SELF"].ToString())).ToString("#,0.######");
                    }
                }
                else
                {
                    MessageBox.Show("데이터 일시 오류");
                }
            }
            else
            {
                lblSearch.Text = "Searching ...";
                lblSearch.Visible = true;
                Application.DoEvents();

                //쿼리 시작 
                wnDm wDm = new wnDm();
                DataTable dt = new DataTable();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" and A.LOT_NO = '" + txt_lot_no.Text.ToString() + "' ");
                sb.AppendLine(" and A.LOT_SUB = '" + txt_lot_sub.Text.ToString() + "' ");
                sb.AppendLine(" and A.F_STEP = '" + txt_f_step.Text.ToString() + "' ");
                dt = wDm.fn_Item_Chk_Detail_List(sb.ToString(), gbn);

                main_dgv.RowCount = dt.Rows.Count;
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        main_dgv.Rows[i].Cells["CHK_CD"].Value = dt.Rows[i]["CHK_CD"].ToString();
                        main_dgv.Rows[i].Cells["CHK_ORD"].Value = (i + 1).ToString(); // dt.Rows[i]["CHK_ORD"].ToString();
                        main_dgv.Rows[i].Cells["CHK_NM"].Value = dt.Rows[i]["CHK_NM"].ToString();
                        main_dgv.Rows[i].Cells["CHK_LOC"].Value = dt.Rows[i]["CHK_LOC"].ToString();
                        main_dgv.Rows[i].Cells["RULE_SIZE"].Value = dt.Rows[i]["RULE_SIZE"].ToString();
                        main_dgv.Rows[i].Cells["RULE_LIMIT"].Value = dt.Rows[i]["RULE_LIMIT"].ToString();
                        main_dgv.Rows[i].Cells["MEASURE_APP"].Value = dt.Rows[i]["MEASURE_APP"].ToString();
                        main_dgv.Rows[i].Cells["EVA_GUBUN_NM"].Value = dt.Rows[i]["EVA_GUBUN_NM"].ToString();

                        main_dgv.Rows[i].Cells["GRADE"].Value = dt.Rows[i]["GRADE"].ToString();

                        sb = new StringBuilder();

                        sb.AppendLine(" and A.LOT_NO = '" + txt_lot_no.Text.ToString() + "' ");
                        sb.AppendLine(" and A.LOT_SUB = '" + txt_lot_sub.Text.ToString() + "' ");
                        sb.AppendLine(" and A.F_STEP = '" + txt_f_step.Text.ToString() + "' ");
                        sb.AppendLine(" and A.CHK_CD = '" + main_dgv.Rows[i].Cells["CHK_CD"].Value.ToString() + "' ");

                        DataTable dt3 = new DataTable();
                        dt3 = wDm.fn_Item_Chk_Exam_Value(sb.ToString());

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int k = 0;
                            for (int j = startIdx; j < main_dgv.ColumnCount; j++)
                            {
                                main_dgv.Rows[i].Cells[j].Value = dt3.Rows[k]["CHK_VALUE"].ToString();
                                k++;
                            }
                        }

                        //main_dgv.Rows[i].Cells["LOWER_SIZE"].Value = (decimal.Parse(dt.Rows[i]["LOWER_SIZE"].ToString())).ToString("#,0.######");
                        //main_dgv.Rows[i].Cells["UPPER_SIZE"].Value = (decimal.Parse(dt.Rows[i]["UPPER_SIZE"].ToString())).ToString("#,0.######");
                        //main_dgv.Rows[i].Cells["LOWER_SELF"].Value = (decimal.Parse(dt.Rows[i]["LOWER_SELF"].ToString())).ToString("#,0.######");
                        //main_dgv.Rows[i].Cells["UPPER_SELF"].Value = (decimal.Parse(dt.Rows[i]["UPPER_SELF"].ToString())).ToString("#,0.######");
                    }

                }
                else
                {
                    MessageBox.Show("데이터 일시 오류");
                }
                lblSearch.Visible = false;
            }
        }
        #endregion 공정검사 더블클릭시 로직


        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Row == 0)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.MediumSeaGreen), e.CellBounds);
            }
        }

        private void tbItemControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbItemControl.SelectedIndex == 0)
            {
                chk_req_list();
            }
            else if (tbItemControl.SelectedIndex == 1)
            {
                chk_complete_list();
            }
            else
            {
                chk_omit_list();
            }
        }
        #endregion item chk logic


        #region common logic

        private void pic_logic(PictureBox pic)
        {
            ofd.Filter = "*.png|*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //이미지 
                image = Image.FromFile(ofd.FileName);
                //이미지 경로 
                path = ofd.FileName;

                /* 이미지 리사이즈 */
                Image cus_img = ComInfo.pic_resize_logic(pic, image);
                //픽쳐박스에 이미지를 띄운다
                pic.BackgroundImage = cus_img;
            }
        }
       
        private byte[] ImageToByte(Image image) 
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        #endregion common logic

        private void ReqChkList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            try
            {
                bool change_chk = false;
                string ord_lot_no = txt_lot_no.Text.ToString();
                string ord_lot_sub = txt_lot_sub.Text.ToString();
                string ord_f_step = txt_f_step.Text.ToString();
                if (dgv.Rows[e.RowIndex].Cells[2].Value.ToString().Equals(ord_lot_no)
                    && dgv.Rows[e.RowIndex].Cells[3].Value.ToString().Equals(ord_lot_sub)
                    && dgv.Rows[e.RowIndex].Cells[10].Value.ToString().Equals(ord_f_step)) //같은 내용을 더블클릭 했을 시 
                {
                    change_chk = false;
                }
                else
                {
                    change_chk = true;
                }

                if (dgv.Rows[e.RowIndex].Cells[11].Value.ToString().Equals("S")) //대기
                {
                    wnDm wDm = new wnDm();
                    DataTable dt = new DataTable();

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(" and ITEM_CD = '" + dgv.Rows[e.RowIndex].Cells[8].Value.ToString() + "' ");

                    dt = wDm.fn_Item_Chk_Cnt(sb.ToString());

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (int.Parse(dt.Rows[0]["cnt"].ToString()) == 0)
                        {
                            MessageBox.Show("공정검사기준에 등록되지 않았습니다. \n 등록 후 다시 시도하시기 바랍니다. ");
                            return;
                        }
                        sb = new StringBuilder();
                        sb.AppendLine("     and A.LOT_NO = '" + dgv.Rows[e.RowIndex].Cells[2].Value.ToString() + "' ");
                        sb.AppendLine("     and B.LOT_SUB = '" + dgv.Rows[e.RowIndex].Cells[3].Value.ToString() + "' ");
                        sb.AppendLine("     and B.F_STEP = '" + dgv.Rows[e.RowIndex].Cells[10].Value.ToString() + "' ");
                        //sb.AppendLine("     and B.FLOW_CD = '" + dgv.Rows[e.RowIndex].Cells[9].Value.ToString() + "' ");
                        dt = wDm.fn_Item_Chk_Req_List(sb.ToString());

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            txt_lot_no.Text = dt.Rows[0]["LOT_NO"].ToString();
                            txt_lot_sub.Text = dt.Rows[0]["LOT_SUB"].ToString();
                            txt_item_cd.Text = dt.Rows[0]["ITEM_CD"].ToString();
                            txt_item_nm.Text = dt.Rows[0]["ITEM_NM"].ToString();
                            txt_spec.Text = dt.Rows[0]["SPEC"].ToString();
                            txt_flow_cd.Text = dt.Rows[0]["FLOW_CD"].ToString();
                            txt_flow_nm.Text = dt.Rows[0]["FLOW_NM"].ToString();

                            txt_item_gubun_nm.Text = dt.Rows[0]["ITEM_GUBUN_NM"].ToString();
                            txt_sub_amt.Text = (decimal.Parse(dt.Rows[0]["F_SUB_AMT"].ToString())).ToString("#,0.######");

                            txt_measure_cnt.Text = (decimal.Parse(dt.Rows[0]["F_SUB_AMT"].ToString())).ToString("#,0.######");
                            //txt_eva_gubun_nm.Text = dt.Rows[0]["EVA_GUBUN_NM"].ToString();
                            txt_check_nm.Text = dt.Rows[0]["ITEM_CHECK_NM"].ToString();
                            txt_check_yn.Text = dt.Rows[0]["CHECK_YN"].ToString();
                            txt_f_step.Text = dt.Rows[0]["F_STEP"].ToString();

                            flow_chk_detail(dataChkGrid, dgv.Rows[e.RowIndex].Cells[8].Value.ToString(), dgv.Rows[e.RowIndex].Cells[9].Value.ToString(), change_chk, 1); // 1 -> 대기 , 2-> 미완료 혹은 완료
                        }
                        else
                        {
                            MessageBox.Show("데이터 일시 오류 \n 다시 더블클릭 해주시기 바랍니다. ");
                            return;
                        }
                    }
                }
                else //미완료
                {
                    item_chk_data_logic(dgv, e, change_chk);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("시스템 에러" + ex.ToString());
            }
        }

        private void btn_pass_close_Click(object sender, EventArgs e)
        {
            tlp_item.Visible = false;
        }

        private void CompChkList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            bool change_chk = false;
            string ord_lot_no = txt_lot_no.Text.ToString();
            string ord_lot_sub = txt_lot_sub.Text.ToString();

            if (dgv.Rows[e.RowIndex].Cells[2].Value.ToString().Equals(ord_lot_no)
                && dgv.Rows[e.RowIndex].Cells[3].Value.ToString().Equals(ord_lot_sub)) //같은 내용을 더블클릭 했을 시 
            {
                change_chk = false;
            }
            else
            {
                change_chk = true;
            }

            item_chk_data_logic(dgv, e, change_chk);
        }

        #region grid logic 

        #endregion grid logic
    }
}
