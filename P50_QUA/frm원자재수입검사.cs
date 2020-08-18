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
using 스마트팩토리.Popup;

namespace 스마트팩토리.P50_QUA
{
    public partial class frm원자재수입검사 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private ComInfo comInfo = new ComInfo();
        private int p_type_idx = 0;
        private int p_num = 0;
        public frm원자재수입검사()
        {
            InitializeComponent();
        }

        private void frm원자재수입검사_Load(object sender, EventArgs e)
        {
            start_date.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
            init_ComboBox();
            chk_req_list();

            gridSetting();

            tlp_raw.SetColumnSpan(label36, 2);
            tlp_raw.SetColumnSpan(cmb_raw_pass, 2);
            //startIdx = dataChkGrid.ColumnCount;
        }

        private void gridSetting() 
        {
            DataGridViewComboBoxColumn cmbColumn = new DataGridViewComboBoxColumn();

            wnDm wDm = new wnDm();
            DataTable dt = wDm.fn_query_Type();
            //((DataGridViewComboBoxColumn)dataChkGrid.Columns["GRADE"]).DataSource = dt2;

            int num = dt.Rows.Count;
            cmbColumn.ValueMember = "코드";
            cmbColumn.DisplayMember = "명칭";
            cmbColumn.DataSource = dt;
            cmbColumn.HeaderText = "불량유형";
            cmbColumn.Name = "POOR_TYPE";

            rawPoorGrid.Columns.Add(cmbColumn);
            rawPoorGrid.Columns[rawPoorGrid.Columns.Count - 1].Width = 100;

            //cmbColumn = new DataGridViewComboBoxColumn();
            ////dt = wDm.fn_query_Poor();
            //cmbColumn.ValueMember = "코드";
            //cmbColumn.DisplayMember = "명칭";
            ////cmbColumn.DataSource = dt;
            //cmbColumn.HeaderText = "불량내역";
            //cmbColumn.Name = "POOR_CD";

            //rawPoorGrid.Columns.Add(cmbColumn);
            rawPoorGrid.Columns.Add("POOR_NM", "불량사유"); //index 1
            rawPoorGrid.Columns[rawPoorGrid.Columns.Count - 1].Width = 120;

            rawPoorGrid.Columns.Add("PRI_NON_PASS_AMT", "부적합량"); //index 2
            rawPoorGrid.Columns.Add("UPD_DETAIL", "수정내역"); //index 3 
            rawPoorGrid.Columns.Add("UPD_PASS_AMT", "수정합량"); //index 4 
            rawPoorGrid.Columns.Add("COMMENT", "비고"); //index 5
            rawPoorGrid.Columns.Add("POOR_SEQ", "SEQ"); //index 6

            rawPoorGrid.Columns[2].Width = 60;
            rawPoorGrid.Columns[3].Width = 150;
            rawPoorGrid.Columns[4].Width = 60;
            rawPoorGrid.Columns[5].Width = 110;
            rawPoorGrid.Columns[6].Width = 50;
            rawPoorGrid.Columns[6].Visible = false;

        }
        private void init_ComboBox()
        {
            string sqlQuery = "";

            cmb_raw.ValueMember = "코드";
            cmb_raw.DisplayMember = "명칭";
            sqlQuery = comInfo.queryRaw();
            wConst.ComboBox_Read_Blank(cmb_raw, sqlQuery);

            cmb_cust.ValueMember = "코드";
            cmb_cust.DisplayMember = "명칭";
            sqlQuery = comInfo.queryCust("2");
            wConst.ComboBox_Read_Blank(cmb_cust, sqlQuery);

            cmb_raw_pass.ValueMember = "코드";
            cmb_raw_pass.DisplayMember = "명칭";
            sqlQuery = comInfo.queryChkPass();
            wConst.ComboBox_Read_Blank(cmb_raw_pass, sqlQuery);

        }
        #region button logic 
        
        private void btnSave_Click(object sender, EventArgs e)
        {
       
            if (txt_input_date.Text.ToString().Equals("")) 
            {
                MessageBox.Show("원자재 입고번호를 선택하셔야 합니다. ");
                return;
            }

            if (txt_control_no.Text.ToString().Equals("")) 
            {
                MessageBox.Show("관리 No.를 입력하시기 바랍니다. ");
                return;
            }
            
            int chk_total_amt = int.Parse(this.txt_chk_total_amt.Text.ToString().Replace(",","")); //검사수량
            int pass_amt = int.Parse(this.txt_pass_amt.Text.ToString().Replace(",", "")); //합격수량
            int p_non_pass_amt = int.Parse(this.pri_non_pass_amt.Text.ToString().Replace(",", "")); //1차 부적합량
            int u_com_amt = int.Parse(this.upd_com_amt.Text.ToString().Replace(",", "")); //수정완료
            int f_non_pass_amt = int.Parse(this.final_non_pass_amt.Text.ToString().Replace(",", "")); //최종 부적합량
            int f_pass_amt = int.Parse(this.final_pass_amt.Text.ToString().Replace(",", "")); //최종 합격량

            if (chk_total_amt != pass_amt+p_non_pass_amt) 
            {
                MessageBox.Show("합격수량과 1차부적합량의 합이 검사총량과 같아야 합니다.");
                return;
            }
            if (p_non_pass_amt != u_com_amt) 
            {
                MessageBox.Show("1차 부적합량과 수정완료 갯수가 같아야 합니다.");
                return;
            }

            //if (u_com_amt != f_non_pass_amt) 
            //{
            //    MessageBox.Show("수정완료 갯수와 최종 부적합량이 같아야 합니다.");
            //    return;
            //}
            if (f_pass_amt + f_non_pass_amt != chk_total_amt) 
            {
                MessageBox.Show("최종부적합량과 최종합격량의 차가 검사수량과 같아야 합니다. ");
                return;
            }

            if (rawPoorGrid.Rows.Count > 0)
            {
                for (int i = 0; i < rawPoorGrid.Rows.Count; i++)
                {
                    if (rawPoorGrid.Rows[i].Cells["POOR_TYPE"].Value == null)
                    {
                        MessageBox.Show((i + 1).ToString() + "번째 불량유형을 선택하지 않아서 저장이 불가능합니다.");
                        return;
                    }
                }
            }

            saveLogic();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_plus_Click(object sender, EventArgs e)
        {
            rawPoorGrid.Rows.Add();

            rawPoorGrid.Rows[rawPoorGrid.Rows.Count - 1].Cells["PRI_NON_PASS_AMT"].Value = 0;
            rawPoorGrid.Rows[rawPoorGrid.Rows.Count - 1].Cells["UPD_PASS_AMT"].Value = 0;
        }

        private void btn_minus_Click(object sender, EventArgs e)
        {

        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            if (txt_check_yn.Text.ToString().Equals("N")) //미완료
            {
                cmb_raw_pass.SelectedValue = "";
                lbl_pass_raw.Text = txt_raw_mat_nm.Text.ToString();
                lbl_pass_spec.Text = txt_spec.Text.ToString();
                lbl_pass_cust.Text = txt_cust_nm.Text.ToString();
                tlp_raw.Visible = true;
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

        private void btn_pass_Click(object sender, EventArgs e)
        {
            if (!txt_chk_date.Text.ToString().Equals("")) 
            {
                if (txt_check_yn.Text.ToString().Equals("N")) 
                {
                    if (cmb_raw_pass.SelectedValue == null) cmb_raw_pass.SelectedValue = "";

                    if ((string)cmb_raw_pass.SelectedValue == "")
                    {
                        MessageBox.Show("검사결과를 선택하시기 바랍니다.");
                        return;
                    }

                    wnDm wDm = new wnDm();
                    int rsNum = wDm.updateRawChkPass(txt_input_date.Text.ToString()
                                                      , txt_input_cd.Text.ToString()
                                                      , txt_seq.Text.ToString()
                                                      , cmb_raw_pass.SelectedValue.ToString()
                                                      , decimal.Parse(final_pass_amt.Text.ToString().Replace(",","")));
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
                    tlp_raw.Visible = false;
                }
            }   
        }

        private void btn_pass_close_Click(object sender, EventArgs e)
        {
            tlp_raw.Visible = false;
        }

        #endregion button logic

        #region logic 

        private void saveLogic()
        {
            wnDm wDm = new wnDm();
            Console.WriteLine(txt_check_yn.Text.ToString());
            if (txt_check_yn.Text.ToString().Equals("S")) //대기
            {
                lblSearch.Text = "등록중";
                lblSearch.Visible = true;
                Application.DoEvents();

                /*txt_input_date.Text = txt_input_date.Text == null ? "" : txt_input_date.Text;
                txt_input_cd.Text = txt_input_cd.Text == null ? "" : txt_input_cd.Text;
                txt_seq.Text = txt_seq.Text == null ? "" : txt_seq.Text;
                txt_raw_mat_cd.Text = txt_raw_mat_cd.Text == null ? "" : txt_raw_mat_cd.Text;
                txt_control_no.Text = txt_control_no.Text == null ? "" : txt_control_no.Text;
                txt_part_no.Text = txt_part_no.Text == null ? "" : txt_part_no.Text;
                txt_part_no.Text = txt_part_no.Text == null ? "" : txt_part_no.Text;
                txt_input_date.Text = txt_input_date.Text == null ? "" : txt_input_date.Text;
                txt_input_date.Text = txt_input_date.Text == null ? "" : txt_input_date.Text;
                txt_input_date.Text = txt_input_date.Text == null ? "" : txt_input_date.Text;
                txt_input_date.Text = txt_input_date.Text == null ? "" : txt_input_date.Text;
                txt_input_date.Text = txt_input_date.Text == null ? "" : txt_input_date.Text;
                txt_input_date.Text = txt_input_date.Text == null ? "" : txt_input_date.Text;
                txt_input_date.Text = txt_input_date.Text == null ? "" : txt_input_date.Text;
                txt_input_date.Text = txt_input_date.Text == null ? "" : txt_input_date.Text;
                txt_input_date.Text = txt_input_date.Text == null ? "" : txt_input_date.Text;
                txt_input_date.Text = txt_input_date.Text == null ? "" : txt_input_date.Text;*/

                Console.WriteLine(txt_input_date.Text.ToString());
                Console.WriteLine(txt_input_cd.Text.ToString());
                Console.WriteLine(txt_seq.Text.ToString());
                Console.WriteLine(txt_raw_mat_cd.Text.ToString());
                Console.WriteLine(txt_control_no.Text.ToString());
                Console.WriteLine(txt_part_no.Text.ToString());
                Console.WriteLine(txt_chk_total_amt.Text.ToString());
                Console.WriteLine(txt_pass_amt.Text.ToString());
                Console.WriteLine(pri_non_pass_amt.Text.ToString());
                Console.WriteLine(upd_com_amt.Text.ToString());
                Console.WriteLine(final_non_pass_amt.Text.ToString());
                Console.WriteLine(final_pass_amt.Text.ToString());
                Console.WriteLine(txt_comment.Text.ToString());
                

                int rsNum = wDm.insertRawChkExam(txt_input_date.Text.ToString()
                                                  , txt_input_cd.Text.ToString()
                                                  , txt_seq.Text.ToString()
                                                  , txt_raw_mat_cd.Text.ToString()
                                                  , txt_control_no.Text.ToString()
                                                  , txt_part_no.Text.ToString()
                                                  , txt_chk_total_amt.Text.ToString()
                                                  , txt_pass_amt.Text.ToString()
                                                  , pri_non_pass_amt.Text.ToString()
                                                  , upd_com_amt.Text.ToString()
                                                  , final_non_pass_amt.Text.ToString()
                                                  , final_pass_amt.Text.ToString()
                                                  , txt_comment.Text.ToString()
                                                  , rawStanGrid
                                                  , rawPoorGrid);

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
                lblSearch.Text = "등록중";
                lblSearch.Visible = true;
                Application.DoEvents();

                int rsNum = wDm.updateRawChkExam(txt_input_date.Text.ToString()
                                                  , txt_input_cd.Text.ToString()
                                                  , txt_seq.Text.ToString()
                                                  , txt_raw_mat_cd.Text.ToString()
                                                  , txt_control_no.Text.ToString()
                                                  , txt_part_no.Text.ToString()
                                                  , txt_chk_total_amt.Text.ToString()
                                                  , txt_pass_amt.Text.ToString()
                                                  , pri_non_pass_amt.Text.ToString()
                                                  , upd_com_amt.Text.ToString()
                                                  , final_non_pass_amt.Text.ToString()
                                                  , final_pass_amt.Text.ToString()
                                                  , txt_comment.Text.ToString()
                                                  , rawStanGrid
                                                  , rawPoorGrid);
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

        private void raw_chk_logic(DataGridView dgv, string condition, int chk)
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("where 1=1 ");
                sb.AppendLine(condition);
                dt = wDm.fn_Input_Chk_List(sb.ToString());

                if (dt != null && dt.Rows.Count > 0)
                {

                    dgv.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dgv.Rows[i].Cells[0].Value = false;

                        dgv.Rows[i].Cells[1].Value = dt.Rows[i]["INPUT_DATE"].ToString();
                        dgv.Rows[i].Cells[2].Value = dt.Rows[i]["INPUT_CD"].ToString();
                        dgv.Rows[i].Cells[3].Value = dt.Rows[i]["RAW_MAT_NM"].ToString();
                        if(dt.Rows[i]["RAW_MAT_GUBUN"].ToString().Equals("9")){
                            dgv.Rows[i].Cells[3].Value += "("+dt.Rows[i]["RAW_HST_CD"].ToString()+")";
                        }
                        
                        dgv.Rows[i].Cells[4].Value = dt.Rows[i]["CUST_NM"].ToString();
                        dgv.Rows[i].Cells[5].Value = dt.Rows[i]["SPEC"].ToString();
                        if (chk == 1) //대기, 미완료
                        {
                            dgv.Rows[i].Cells[6].Value = decimal.Parse(dt.Rows[i]["TEMP_AMT"].ToString()).ToString("#,0.######"); //가입고수량
                        }
                        else //완료, 생략
                        {
                            dgv.Rows[i].Cells[6].Value = decimal.Parse(dt.Rows[i]["TOTAL_AMT"].ToString()).ToString("#,0.######"); //최종수량
                        }
                        dgv.Rows[i].Cells[7].Value = decimal.Parse(dt.Rows[i]["TOTAL_MONEY"].ToString()).ToString("#,0.######");
                        dgv.Rows[i].Cells[8].Value = dt.Rows[i]["HEAT_NO"].ToString();
                        dgv.Rows[i].Cells[13].Value = dt.Rows[i]["CHECK_NM"].ToString();
                        dgv.Rows[i].Cells[14].Value = dt.Rows[i]["RAW_MAT_CD"].ToString();
                        dgv.Rows[i].Cells[15].Value = dt.Rows[i]["CUST_CD"].ToString();
                        dgv.Rows[i].Cells[16].Value = dt.Rows[i]["CHECK_YN"].ToString();
                        dgv.Rows[i].Cells[17].Value = dt.Rows[i]["SEQ"].ToString();
                        dgv.Rows[i].Cells[18].Value = dt.Rows[i]["CHK_DATE"].ToString();
                        dgv.Rows[i].Cells[19].Value = dt.Rows[i]["PASS_YN"].ToString();

                        if (dt.Rows[0]["PASS_YN"].ToString().Equals("Y"))
                        {
                            dgv.Rows[i].Cells[19].Value = "합격";
                        }
                        else if (dt.Rows[0]["PASS_YN"].ToString().Equals("N"))
                        {
                            dgv.Rows[i].Cells[19].Value = "불합격";
                        }
                        else
                        {
                            dgv.Rows[i].Cells[19].Value = "";
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        private void chk_req_list() //검사요청 탭
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("and (A.CHECK_YN = 'S' or A.CHECK_YN = 'N') "); //검사현황 S 대기, N 검사 등록 후 미완료 , Y 검사 완료 , O 검사 생략

            raw_chk_logic(rawChkGrid, sb.ToString(),1); 
        }

        private void chk_complete_list() //완료된 검사현황
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("and A.CHECK_YN = 'Y'  "); //검사현황 S 대기, N 검사 등록 후 미완료 , Y 검사 완료 , O 검사 패스 

            raw_chk_logic(compChkList, sb.ToString(),2); 
        }

        private void chk_omit_list() //생략된 자료
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("and A.CHECK_YN = 'O'  "); //검사현황 S 대기, N 검사 등록 후 미완료 , Y 검사 완료 , O 검사 패스 

            raw_chk_logic(omitChkList, sb.ToString(),3);
        }

        #endregion logic

        #region grid logic
        private void rawChkGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            try
            {
                bool change_chk = false;
                string ord_input_date = txt_input_date.Text.ToString();
                string ord_input_cd = txt_input_cd.Text.ToString();

                if (dgv.Rows[e.RowIndex].Cells[2].Value.ToString().Equals(ord_input_date)
                    && dgv.Rows[e.RowIndex].Cells[3].Value.ToString().Equals(ord_input_cd)) //같은 내용을 더블클릭 했을 시 
                {
                    change_chk = false;
                }
                else
                {
                    change_chk = true;
                }

                txt_input_date.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
                txt_input_cd.Text = dgv.Rows[e.RowIndex].Cells[2].Value.ToString();
                txt_raw_mat_cd.Text = dgv.Rows[e.RowIndex].Cells[14].Value.ToString();
                txt_raw_mat_nm.Text = dgv.Rows[e.RowIndex].Cells[3].Value.ToString();
                txt_cust_cd.Text = dgv.Rows[e.RowIndex].Cells[15].Value.ToString();
                txt_cust_nm.Text = dgv.Rows[e.RowIndex].Cells[4].Value.ToString();
                txt_spec.Text = dgv.Rows[e.RowIndex].Cells[5].Value.ToString();
                txt_total_amt.Text = dgv.Rows[e.RowIndex].Cells[6].Value.ToString();
                txt_check_nm.Text = dgv.Rows[e.RowIndex].Cells[13].Value.ToString();
                txt_check_yn.Text = dgv.Rows[e.RowIndex].Cells[16].Value.ToString();
                txt_seq.Text = dgv.Rows[e.RowIndex].Cells[17].Value.ToString();

                txt_chk_total_amt.Text = txt_total_amt.Text.ToString();
                txt_chk_date.Text = dgv.Rows[e.RowIndex].Cells[18].Value.ToString();

                txt_pass.Text = dgv.Rows[e.RowIndex].Cells[19].Value.ToString();

                wnDm wDm = new wnDm();
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" and A.RAW_MAT_CD = '" + txt_raw_mat_cd.Text.ToString() + "' ");

                dt = wDm.fn_Raw_Chk_Rst_List(sb.ToString());

                if (dt != null && dt.Rows.Count > 0)
                {
                    rawStanGrid.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++) 
                    {
                        rawStanGrid.Rows[i].Cells["CHK_CD"].Value =  dt.Rows[i]["CHK_CD"].ToString();// dt.Rows[i]["CHK_CD"].ToString();
                        rawStanGrid.Rows[i].Cells["CHK_ORD"].Value = (i + 1).ToString();
                        rawStanGrid.Rows[i].Cells["CHK_NM"].Value = dt.Rows[i]["CHK_NM"].ToString();
                        rawStanGrid.Rows[i].Cells["CHK_STAN_VALUE"].Value = dt.Rows[i]["CHK_STAN_VALUE"].ToString();
                        rawStanGrid.Rows[i].Cells["CHK_VALUE"].Value = dt.Rows[i]["CHK_VALUE"].ToString();
                    }
                }
                else 
                {
                    rawStanGrid.Rows.Clear();
                }


                if (dgv.Rows[e.RowIndex].Cells[16].Value.ToString().Equals("N")) //미완료
                {
                    btnComplete.Enabled = true;
                    row_chk_detail();
                }
                else 
                {
                    txt_pass_amt.Text = "0";
                    pri_non_pass_amt.Text = "0";
                    upd_com_amt.Text = "0";
                    final_non_pass_amt.Text = "0";
                    final_pass_amt.Text = "0";
                    txt_comment.Text = "";

                    btnComplete.Enabled = false;

                    rawPoorGrid.Rows.Clear();
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("시스템 에러" + ex.ToString());
            }
        }

        private void compChkList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            btnComplete.Enabled = false;

            txt_input_date.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
            txt_input_cd.Text = dgv.Rows[e.RowIndex].Cells[2].Value.ToString();
            txt_raw_mat_cd.Text = dgv.Rows[e.RowIndex].Cells[14].Value.ToString();
            txt_raw_mat_nm.Text = dgv.Rows[e.RowIndex].Cells[3].Value.ToString();
            txt_cust_cd.Text = dgv.Rows[e.RowIndex].Cells[15].Value.ToString();
            txt_cust_nm.Text = dgv.Rows[e.RowIndex].Cells[4].Value.ToString();
            txt_spec.Text = dgv.Rows[e.RowIndex].Cells[5].Value.ToString();
            txt_total_amt.Text = dgv.Rows[e.RowIndex].Cells[6].Value.ToString();
            txt_check_nm.Text = dgv.Rows[e.RowIndex].Cells[13].Value.ToString();
            txt_check_yn.Text = dgv.Rows[e.RowIndex].Cells[16].Value.ToString();
            txt_seq.Text = dgv.Rows[e.RowIndex].Cells[17].Value.ToString();

            txt_chk_total_amt.Text = txt_total_amt.Text.ToString();
            txt_chk_date.Text = dgv.Rows[e.RowIndex].Cells[18].Value.ToString();

            txt_pass.Text = dgv.Rows[e.RowIndex].Cells[19].Value.ToString();

            row_chk_detail();
        }

        #endregion grid logic

        private void row_chk_detail() 
        {
            wnDm wDm = new wnDm();
            DataTable dt = null;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("     and A.INPUT_DATE = '" + txt_input_date.Text.ToString() + "' ");
            sb.AppendLine("     and A.INPUT_CD = '" + txt_input_cd.Text.ToString() + "' ");
            sb.AppendLine("     and A.SEQ = '" + txt_seq.Text.ToString() + "' ");

            dt = wDm.fn_Input_Chk_Detail_List(sb.ToString());

            if (dt != null && dt.Rows.Count > 0) 
            {
                txt_control_no.Text = dt.Rows[0]["CONTROL_NO"].ToString();
                txt_part_no.Text = dt.Rows[0]["PART_NO"].ToString();
                txt_chk_total_amt.Text = dt.Rows[0]["CHK_TOTAL_AMT"].ToString();
                txt_pass_amt.Text = dt.Rows[0]["PASS_AMT"].ToString();
                pri_non_pass_amt.Text = dt.Rows[0]["PRI_NON_PASS_AMT"].ToString();
                upd_com_amt.Text = dt.Rows[0]["UPD_COM_AMT"].ToString();
                final_non_pass_amt.Text = dt.Rows[0]["FINAL_NON_PASS_AMT"].ToString();
                final_pass_amt.Text = dt.Rows[0]["FINAL_PASS_AMT"].ToString();
                txt_comment.Text = dt.Rows[0]["COMMENT"].ToString();
            }

            dt = wDm.fn_Input_Chk_Poor_List(sb.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                rawPoorGrid.RowCount = dt.Rows.Count;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rawPoorGrid.Rows[i].Cells["POOR_TYPE"].Value = dt.Rows[i]["TYPE_CD"].ToString();
                    rawPoorGrid.Rows[i].Cells["POOR_NM"].Value = dt.Rows[i]["POOR_NM"].ToString();
                    rawPoorGrid.Rows[i].Cells["POOR_SEQ"].Value = dt.Rows[i]["POOR_SEQ"].ToString();
                    rawPoorGrid.Rows[i].Cells["PRI_NON_PASS_AMT"].Value = dt.Rows[i]["PRI_NON_PASS_AMT"].ToString();
                    rawPoorGrid.Rows[i].Cells["UPD_DETAIL"].Value = dt.Rows[i]["UPD_DETAIL"].ToString();
                    rawPoorGrid.Rows[i].Cells["UPD_PASS_AMT"].Value = dt.Rows[i]["UPD_PASS_AMT"].ToString();
                }
            }
            else 
            {
                rawPoorGrid.Rows.Clear();
            }
            
        }
        #region event logic
        private void chk_total_amt_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e);
        }

        private void pass_amt_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e);
        }

        private void pri_non_pass_amt_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e);
        }

        private void upd_com_amt_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e);
        }

        private void final_non_pass_amt_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e);
        }

        private void final_pass_amt_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e);
        }
        #endregion event lotic

        private void tbRawControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbRawControl.SelectedIndex == 0)
            {
                chk_req_list();
            }
            else if (tbRawControl.SelectedIndex == 1)
            {
                chk_complete_list();
            }
            else
            {
                chk_omit_list();
            }
        }

        private void btnOmit_Click(object sender, EventArgs e)
        {
            if (tbRawControl.SelectedIndex == 0)
            {
                if (rawChkGrid.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();

                    bool chk = false;


                    



                    for (int i = 0; i < rawChkGrid.Rows.Count; i++)
                    {
                        if ((bool)rawChkGrid.Rows[i].Cells[0].Value == true)
                        {
                            if (rawChkGrid.Rows[i].Cells[16].Value.ToString().Equals("S"))
                            {

                                
                                    
                                    
                                

                                DataTable dt = new DataTable();
                                decimal final_amt = decimal.Parse(rawChkGrid.Rows[i].Cells[6].Value.ToString().Replace(",", ""));
                                sb.AppendLine(" update F_RAW_DETAIL ");
                                sb.AppendLine(" set CHECK_YN = 'O' , TOTAL_AMT = " + final_amt + " , CURR_AMT = " + final_amt + "");
                                sb.AppendLine(" where INPUT_DATE = '" + rawChkGrid.Rows[i].Cells[1].Value.ToString() + "' ");
                                sb.AppendLine("     and INPUT_CD = '" + rawChkGrid.Rows[i].Cells[2].Value.ToString() + "' ");
                                sb.AppendLine("     and SEQ = '" + rawChkGrid.Rows[i].Cells[17].Value.ToString() + "' ");
                                sb.AppendLine(" update N_RAW_CODE ");
                                sb.AppendLine(" set BAL_STOCK = BAL_STOCK +'" + final_amt +"' ");
                                sb.AppendLine(" where RAW_MAT_CD = (select RAW_MAT_CD from F_RAW_DETAIL ");
                                sb.AppendLine(" where INPUT_DATE = '" + rawChkGrid.Rows[i].Cells[1].Value.ToString() + "' ");
                                sb.AppendLine("     and INPUT_CD = '" + rawChkGrid.Rows[i].Cells[2].Value.ToString() + "' ");
                                sb.AppendLine("     and SEQ = '" + rawChkGrid.Rows[i].Cells[17].Value.ToString() + "') ");
                                sb.AppendLine("    ");


                                Console.WriteLine(sb.ToString());

                                chk = true;
                                
                            }
                        }
                    }

                    if (chk == true)
                    {
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
                }

            }
        }

        private void RawPoorGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //DataGridView dgv = (DataGridView)sender;

            //if (dgv.Columns[dgv.CurrentCell.ColumnIndex].Name.Equals("POOR_TYPE"))
            //{
            //    ComboBox cmbprocess = e.Control as ComboBox;

            //    if (p_num > 0) 
            //    {
            //        cmbprocess.Leave -= new EventHandler(cmb_flow_pass_Leave);
            //    }

            //    cmbprocess.Leave += new EventHandler(cmb_flow_pass_Leave);
            //    p_type_idx = dgv.CurrentRow.Index;
            //}
        }

        private void cmb_flow_pass_Leave(object sender, EventArgs e)
        {
            p_num++;
            ComboBox cmbprocess = (ComboBox)sender;

            if (cmbprocess.SelectedValue == null) cmbprocess.SelectedValue = "";
            if (cmbprocess.SelectedValue != null) 
            {
                wnDm wDm = new wnDm();
                DataTable dt = wDm.fn_query_Poor(cmbprocess.SelectedValue.ToString());

                ((DataGridViewComboBoxColumn)rawPoorGrid.Columns["POOR_CD"]).DataSource = dt;
            }
        }
    }
}
