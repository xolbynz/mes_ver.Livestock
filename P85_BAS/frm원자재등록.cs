using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 스마트팩토리.CLS;

namespace 스마트팩토리.P85_BAS
{
    public partial class frm원자재등록 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private ComInfo comInfo = new ComInfo();

        public frm원자재등록()
        {
            InitializeComponent();
        }

        private void chk_manager_yn_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void frm원자재등록_Load(object sender, EventArgs e)
        {
            ComInfo.gridHeaderSet(dataRawCdGrid);

            init_ComboBox();
            raw_list();
        }

        private void init_ComboBox()
        {
            ComInfo comInfo = new ComInfo();
            string sqlQuery = "";
            //불량유형 시작

            cmb_cd_srch.Items.Add("전체 검색");
            cmb_cd_srch.Items.Add("코드별 검색");
            cmb_cd_srch.Items.Add("이름별 검색");
            cmb_cd_srch.SelectedIndex = 0;

            cmb_type.ValueMember = "코드";
            cmb_type.DisplayMember = "명칭";
            sqlQuery = comInfo.queryType();
            wConst.ComboBox_Read_Blank(cmb_type, sqlQuery);

            cmb_raw_mat_gbn.ValueMember = "코드";
            cmb_raw_mat_gbn.DisplayMember = "명칭";
            sqlQuery = comInfo.queryRawList();
            wConst.ComboBox_Read_Blank(cmb_raw_mat_gbn, sqlQuery);

            cmb_raw2.ValueMember = "코드";
            cmb_raw2.DisplayMember = "명칭";
            sqlQuery = comInfo.queryRawList();
            wConst.ComboBox_Read_ALL(cmb_raw2, sqlQuery);

            cmb_input_unit.ValueMember = "코드";
            cmb_input_unit.DisplayMember = "명칭";
            sqlQuery = comInfo.queryUnit();
            wConst.ComboBox_Read_Blank(cmb_input_unit, sqlQuery);

            cmb_output_unit.ValueMember = "코드";
            cmb_output_unit.DisplayMember = "명칭";
            wConst.ComboBox_Read_Blank(cmb_output_unit, sqlQuery);

            cmb_used.ValueMember = "코드";
            cmb_used.DisplayMember = "명칭";
            sqlQuery = comInfo.queryUsedYn();
            wConst.ComboBox_Read_NoBlank(cmb_used, sqlQuery);

            cmb_line.ValueMember = "코드";
            cmb_line.DisplayMember = "명칭";
            sqlQuery = comInfo.queryLine();
            wConst.ComboBox_Read_Blank(cmb_line, sqlQuery);

            cmb_cust.ValueMember = "코드";
            cmb_cust.DisplayMember = "명칭";
            sqlQuery = comInfo.queryCustUsed("2"); //구매처
            wConst.ComboBox_Read_Blank(cmb_cust, sqlQuery);

            cmb_raw_chk.ValueMember = "코드";
            cmb_raw_chk.DisplayMember = "명칭";
            sqlQuery = comInfo.queryCode("601"); //수입검사여부
            wConst.ComboBox_Read_Blank(cmb_raw_chk, sqlQuery);

            cmb_used_srch.ValueMember = "코드";
            cmb_used_srch.DisplayMember = "명칭";
            sqlQuery = comInfo.queryCustUsedYnAll(); //사용여부검색
            wConst.ComboBox_Read_NoBlank(cmb_used_srch, sqlQuery);

            cmb_raw_stor.ValueMember = "코드";
            cmb_raw_stor.DisplayMember = "명칭";
            sqlQuery = comInfo.queryStorageAll(); //기본창고 선택 ( 안사용될듯 삭제요망 )
            wConst.ComboBox_Read_NoBlank(cmb_raw_stor, sqlQuery);

            cmb_chugjong_txt.ValueMember = "코드";
            cmb_chugjong_txt.DisplayMember = "명칭";
            sqlQuery = comInfo.queryChugjongAll(); //축종
            wConst.ComboBox_Read_NoBlank(cmb_chugjong_txt, sqlQuery);

            cmb_class_txt.ValueMember = "코드";
            cmb_class_txt.DisplayMember = "명칭";
            sqlQuery = comInfo.queryClassAll(); //분류
            wConst.ComboBox_Read_NoBlank(cmb_class_txt, sqlQuery);

            cmb_country_txt.ValueMember = "코드";
            cmb_country_txt.DisplayMember = "명칭";
            sqlQuery = comInfo.queryCountryAll(); //원산지
            wConst.ComboBox_Read_NoBlank(cmb_country_txt, sqlQuery);

            cmb_grade_txt.ValueMember = "코드";
            cmb_grade_txt.DisplayMember = "명칭";
            sqlQuery = comInfo.queryGradeAll(); //등급
            wConst.ComboBox_Read_NoBlank(cmb_grade_txt, sqlQuery);

            cmb_pattern_gubun.ValueMember = "코드";
            cmb_pattern_gubun.DisplayMember = "명칭";
            sqlQuery = comInfo.queryPatternAll(); //등급
            wConst.ComboBox_Read_NoBlank(cmb_pattern_gubun, sqlQuery);

            cmb_vat_cd.ValueMember = "코드";
            cmb_vat_cd.DisplayMember = "명칭";
            sqlQuery = comInfo.queryVat(); // 과세구분
            wConst.ComboBox_Read_NoBlank(cmb_vat_cd, sqlQuery);

            txt_box_amt.Text = "0";
            txt_hamyang_txt.Text = "";
            txt_label_nm_txt.Text = ""; 
            txt_exprt_count.Text = ""; //유통일수 ( 삭제요망 )



            
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            resetSetting();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            raw_mat_logic();
        }

        private void resetSetting() 
        {
            lbl_raw_mat_gbn.Text = "";
            btnDelete.Enabled = false;
            txt_raw_mat_cd.Text = "";
            txt_raw_mat_cd.Enabled = true;
            txt_raw_mat_nm.Text = "";
            txt_spec.Text = "";
            txt_quality.Text = "";
            cmb_raw_mat_gbn.SelectedIndex = 0;
            cmb_type.SelectedIndex = 0;
            cmb_input_unit.SelectedIndex = 0;
            cmb_output_unit.SelectedIndex = 0;
            txt_input_price.Text = "0";
            txt_output_price.Text = "0";
            txt_conver_ratio.Text = "0";
            cmb_line.SelectedIndex = 0;
            chk_stock_yn.Checked = false;
            cmb_vat_cd.SelectedIndex = 0;
            //cmb_raw_stor.SelectedIndex = 0;
            cmb_cust.SelectedIndex = 0;
            txt_part_no.Text = "";
            txt_box_amt.Text = "0";
            txt_hamyang_txt.Text = "";
            txt_label_nm_txt.Text = "";
            txt_exprt_count.Text = "";

            cmb_raw_stor.SelectedIndex = 0;
            cmb_country_txt.SelectedIndex = 0;
            cmb_class_txt.SelectedIndex = 0;
            cmb_grade_txt.SelectedIndex = 0;
            cmb_chugjong_txt.SelectedIndex = 0;
            cmb_pattern_gubun.SelectedIndex = 0;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            raw_del();
        }

        private void raw_mat_logic() 
        {
            try
            {
                if (cmb_raw_mat_gbn.SelectedValue == null) cmb_raw_mat_gbn.SelectedValue = "";
                if (cmb_type.SelectedValue == null) cmb_type.SelectedValue = "";
                if (cmb_input_unit.SelectedValue == null) cmb_input_unit.SelectedValue = "";
                if (cmb_output_unit.SelectedValue == null) cmb_output_unit.SelectedValue = "";
                if (cmb_line.SelectedValue == null) cmb_line.SelectedValue = "";
                if (cmb_raw_stor.SelectedValue == null) cmb_raw_stor.SelectedValue = "";
                if (cmb_cust.SelectedValue == null) cmb_cust.SelectedValue = "";
                if (cmb_used.SelectedValue == null) cmb_used.SelectedValue = "";
                if (cmb_raw_chk.SelectedValue == null) cmb_raw_chk.SelectedValue = "";
                if (cmb_raw_stor.SelectedValue == null) cmb_raw_stor.SelectedIndex = 0;
                if (cmb_grade_txt.SelectedValue == null) cmb_grade_txt.SelectedIndex = 0;
                if (cmb_country_txt.SelectedValue == null) cmb_country_txt.SelectedIndex = 0;
                if (cmb_chugjong_txt.SelectedValue == null) cmb_chugjong_txt.SelectedIndex = 0;
                if (cmb_class_txt.SelectedValue == null) cmb_class_txt.SelectedIndex = 0;
                if (cmb_pattern_gubun.SelectedValue == null) cmb_pattern_gubun.SelectedIndex = 0;
                if (cmb_vat_cd.SelectedValue == null) cmb_vat_cd.SelectedIndex = 0;


                if (txt_raw_mat_cd.Text.ToString().Equals(""))
                {
                    MessageBox.Show("원부재료코드를 입력하시기 바랍니다.");
                    return;
                }
                if (txt_raw_mat_nm.Text.ToString().Equals(""))
                {
                    MessageBox.Show("원부재료명을 입력하시기 바랍니다.");
                    return;
                }
                if (cmb_input_unit.SelectedValue.ToString().Equals("")) 
                {
                    MessageBox.Show("입고단위를 입력하시기 바랍니다.");
                    return;
                }
                if (cmb_output_unit.SelectedValue.ToString().Equals("")) 
                {
                    MessageBox.Show("사용단위를 입력하시기 바랍니다. ");
                    return;
                }
                if (cmb_raw_chk.SelectedValue.ToString().Equals(""))
                {
                    MessageBox.Show("수입검사여부를 입력하시기 바랍니다. ");
                    return;
                }
                if (cmb_vat_cd.SelectedValue.ToString().Equals(""))
                {
                    MessageBox.Show("과세구분을 선택하시기 바랍니다");
                }
                string st_status_yn = comInfo.resultYn(chk_stock_yn);

                if (lbl_raw_mat_gbn.Text != "1")
                {
                    wnDm wDm = new wnDm();
                    int rsNum = wDm.insertRawMat(
                                      txt_raw_mat_cd.Text.ToString()
                                    , txt_raw_mat_nm.Text.ToString()
                                    , txt_spec.Text.ToString()
                                    , txt_quality.Text.ToString()
                                    , cmb_raw_mat_gbn.SelectedValue.ToString()
                                    , cmb_type.SelectedValue.ToString()
                                    , cmb_input_unit.SelectedValue.ToString()
                                    , cmb_output_unit.SelectedValue.ToString()
                                    , double.Parse(txt_conver_ratio.Text.ToString().Replace(",",""))
                                    , double.Parse(txt_input_price.Text.ToString().Replace(",", ""))
                                    , double.Parse(txt_output_price.Text.ToString().Replace(",", ""))
                                    , cmb_line.SelectedValue.ToString()
                                    , st_status_yn
                                    , cmb_raw_stor.SelectedValue.ToString()
                                    , cmb_used.SelectedValue.ToString()
                                    , cmb_cust.SelectedValue.ToString()
                                    , cmb_raw_chk.SelectedValue.ToString()
                                    , txt_part_no.Text.ToString()
                                    , txt_comment.Text.ToString()
                                    , cmb_chugjong_txt.SelectedValue.ToString()
                                    ,cmb_class_txt.SelectedValue.ToString()
                                    ,cmb_country_txt.SelectedValue.ToString()
                                    ,cmb_grade_txt.SelectedValue.ToString()
                                    ,txt_hamyang_txt.Text.ToString()
                                    ,txt_label_nm_txt.Text.ToString()
                                    ,txt_box_amt.Text.ToString()
                                    ,txt_exprt_count.Text.ToString()
                                    ,cmb_pattern_gubun.SelectedValue.ToString()
                                    ,cmb_vat_cd.SelectedValue.ToString());

                    if (rsNum == 0)
                    {
                        resetSetting();
                        raw_list();
                        MessageBox.Show("성공적으로 등록하였습니다.");
                    }
                    else if (rsNum == 1)
                        MessageBox.Show("저장에 실패하였습니다");
                    else if (rsNum == 2)
                        MessageBox.Show("SQL COMMAND 에러");
                    else if (rsNum == 3)
                        MessageBox.Show("기존 코드가 있으니 \n 다른 코드로 입력 바랍니다.");
                    else
                        MessageBox.Show("Exception 에러1");
                }
                else
                {
                    wnDm wDm = new wnDm();
                    int rsNum = wDm.updateRawMat(
                                      txt_raw_mat_cd.Text.ToString()
                                    , txt_raw_mat_nm.Text.ToString()
                                    , txt_spec.Text.ToString()
                                    , txt_quality.Text.ToString()
                                    , cmb_raw_mat_gbn.SelectedValue.ToString()
                                    , cmb_type.SelectedValue.ToString()
                                    , cmb_input_unit.SelectedValue.ToString()
                                    , cmb_output_unit.SelectedValue.ToString()
                                    , double.Parse(txt_conver_ratio.Text.ToString().Replace(",", ""))
                                    , double.Parse(txt_input_price.Text.ToString().Replace(",", ""))
                                    , double.Parse(txt_output_price.Text.ToString().Replace(",", ""))
                                    , cmb_line.SelectedValue.ToString()
                                    , st_status_yn
                                    , cmb_raw_stor.SelectedValue.ToString()
                                    , cmb_used.SelectedValue.ToString()
                                    , cmb_cust.SelectedValue.ToString()
                                    , cmb_raw_chk.SelectedValue.ToString()
                                    , txt_part_no.Text.ToString()
                                    , txt_comment.Text.ToString()
                                    , cmb_chugjong_txt.SelectedValue.ToString()
                                    , cmb_class_txt.SelectedValue.ToString()
                                    , cmb_country_txt.SelectedValue.ToString()
                                    , cmb_grade_txt.SelectedValue.ToString()
                                    , txt_hamyang_txt.Text.ToString()
                                    , txt_label_nm_txt.Text.ToString()
                                    , txt_box_amt.Text.ToString()
                                    , txt_exprt_count.Text.ToString()
                                    , cmb_pattern_gubun.SelectedValue.ToString()
                                    , cmb_vat_cd.SelectedValue.ToString());
                                    

                    if (rsNum == 0)
                    {
                        raw_list();
                        MessageBox.Show("성공적으로 수정하였습니다.");
                    }
                    else if (rsNum == 1)
                        MessageBox.Show("저장에 실패하였습니다");
                    else
                        MessageBox.Show("Exception 에러");

                }
            }
            catch (Exception e) {
                MessageBox.Show(e.ToString());
            }
        }

        private void raw_list()
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Raw_List("");

                raw_list_rs(dataRawCdGrid,dt,lbl_cnt);
            }
            catch (Exception e)
            {

            }
        }

        private void dataRawCdGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            raw_detail_logic(dataRawCdGrid, e);
        }

        private void raw_list_rs(DataGridView dg, DataTable dt,Label lbl_cnt) 
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                lbl_cnt.Text = dt.Rows.Count.ToString();
                dg.RowCount = dt.Rows.Count;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["USED_CD"].ToString().Equals("2"))
                    {
                        dg.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else if (dt.Rows[i]["USED_CD"].ToString().Equals("3"))
                    {
                        dg.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                    else if (dt.Rows[i]["USED_CD"].ToString().Equals("1"))
                    {
                        dg.Rows[i].DefaultCellStyle.BackColor = Color.Empty;
                    }

                    dg.Rows[i].Cells["RAW_MAT_CD"].Value = dt.Rows[i]["RAW_MAT_CD"].ToString();
                    dg.Rows[i].Cells["RAW_MAT_NM"].Value = dt.Rows[i]["RAW_MAT_NM"].ToString();
                    dg.Rows[i].Cells["SPEC"].Value = dt.Rows[i]["SPEC"].ToString();
                    dg.Rows[i].Cells["COMMENT"].Value = dt.Rows[i]["COMMENT"].ToString();
                    dg.Rows[i].Cells["CHUGJONG_NM"].Value = dt.Rows[i]["CHUGJONG_NM"].ToString();
                    dg.Rows[i].Cells["CLASS_NM"].Value = dt.Rows[i]["CLASS_NM"].ToString();
                    dg.Rows[i].Cells["COUNTRY_NM"].Value = dt.Rows[i]["COUNTRY_NM"].ToString();
                    dg.Rows[i].Cells["TYPE_NM"].Value = dt.Rows[i]["TYPE_NM"].ToString();
                    dg.Rows[i].Cells["LABEL_NM"].Value = dt.Rows[i]["LABEL_NM"].ToString();
                }
            }
            else
            {
                dg.Rows.Clear();
            }
        }
        private void raw_del() {

            ComInfo comInfo = new ComInfo();
            DialogResult msgOk = comInfo.deleteConfrim("원자재", txt_raw_mat_nm.Text.ToString());

            if (msgOk == DialogResult.No)
            {
                return;

            }
            wnDm wDm = new wnDm();
            int rsNum = wDm.deleteRaw(txt_raw_mat_cd.Text.ToString());
            if (rsNum == 0)
            {
                resetSetting();

                raw_list();
                MessageBox.Show("성공적으로 삭제하였습니다.");
            }
            else if (rsNum == 1)
            {
                MessageBox.Show("삭제에 실패하였습니다.");
            }
        }
        private void txt_conver_ratio_TextChanged(object sender, EventArgs e)
        {
            if (txt_conver_ratio.Text.ToString().Equals("")) {
                txt_conver_ratio.Text = "0";
            }
        }

        private void txt_output_price_TextChanged(object sender, EventArgs e)
        {
            if (txt_output_price.Text.ToString().Equals(""))
            {
                txt_output_price.Text = "0";
            }
        }

        private void txt_input_price_TextChanged(object sender, EventArgs e)
        {
            if (txt_input_price.Text.ToString().Equals(""))
            {
                txt_input_price.Text = "0";
            }
        }

        private void txt_output_price_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e);
        }

        private void txt_input_price_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e);
        }

        private void txt_conver_ratio_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            wnDm wDm = new wnDm();
            DataTable dt = null;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("   WHERE 1 = 1  ");


            if (cmb_cd_srch.SelectedIndex == 1)
            {
                if (!txt_srch.Text.ToString().Equals("")) 
                {
                    sb.AppendLine(" AND RAW_MAT_CD like '%" + txt_srch.Text.ToString() + "%'  ");
                }
            }
            else if (cmb_cd_srch.SelectedIndex == 2)
            {
                if (!txt_srch.Text.ToString().Equals(""))
                {
                    sb.AppendLine("  AND LABEL_NM like '%" + txt_srch.Text.ToString() + "%'  ");
                }
            }
            else
            {
                if (!txt_srch.Text.ToString().Equals(""))
                {
                    sb.AppendLine("  AND (LABEL_NM like '%" + txt_srch.Text.ToString() + "%' OR RAW_MAT_CD like '%" + txt_srch.Text.ToString() + "%' ) ");
                }
            }

            if (cmb_used_srch.SelectedIndex != 0)
            {
                sb.AppendLine(" AND USED_CD = '" + cmb_used_srch.SelectedValue.ToString() + "' ");
            }
            
            if (cmb_raw2.SelectedIndex != 0)
            {
                sb.AppendLine(" AND RAW_MAT_GUBUN = '" + cmb_raw2.SelectedValue.ToString() + "' ");
            }
            

            dt = wDm.fn_Raw_List(sb.ToString());

            raw_list_rs(dataRawCdGrid, dt,lbl_cnt);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        

       

        private void raw_detail_logic(DataGridView dg, DataGridViewCellEventArgs e) 
        {
            btnDelete.Enabled = true;
            lbl_raw_mat_gbn.Text = "1";
            txt_raw_mat_cd.Enabled = false;

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                string condition = "WHERE raw_mat_cd = '" + dg.Rows[e.RowIndex].Cells[0].Value.ToString() + "'   ";
                dt = wDm.fn_Raw_List(condition);

                if (dt != null && dt.Rows.Count > 0)
                {

                    txt_raw_mat_cd.Text = dt.Rows[0]["RAW_MAT_CD"].ToString();
                    txt_raw_mat_nm.Text = dt.Rows[0]["RAW_MAT_NM"].ToString();
                    txt_spec.Text = dt.Rows[0]["SPEC"].ToString();
                    txt_quality.Text = dt.Rows[0]["EX_STAN_QUALITY"].ToString();
                    cmb_raw_mat_gbn.SelectedValue = dt.Rows[0]["RAW_MAT_GUBUN"].ToString();
                    cmb_type.SelectedValue = dt.Rows[0]["TYPE_CD"].ToString();
                    cmb_input_unit.SelectedValue = dt.Rows[0]["INPUT_UNIT"].ToString();
                    cmb_output_unit.SelectedValue = dt.Rows[0]["OUTPUT_UNIT"].ToString();
                    txt_input_price.Text = (decimal.Parse(dt.Rows[0]["INPUT_PRICE"].ToString())).ToString("#,0.######");
                    txt_output_price.Text = (decimal.Parse(dt.Rows[0]["OUTPUT_PRICE"].ToString())).ToString("#,0.######");
                    txt_conver_ratio.Text = (decimal.Parse(dt.Rows[0]["CVR_RATIO"].ToString())).ToString("#,0.######");
                   
                    cmb_line.SelectedValue = dt.Rows[0]["LINE_CD"].ToString();
                    txt_comment.Text = dt.Rows[0]["COMMENT"].ToString();
                    cmb_used.SelectedValue = dt.Rows[0]["USED_CD"].ToString();
                    cmb_cust.SelectedValue = dt.Rows[0]["CUST_CD"].ToString();
                    cmb_raw_chk.SelectedValue = dt.Rows[0]["CHECK_GUBUN"].ToString();
                    txt_part_no.Text = dt.Rows[0]["PART_NO"].ToString();
                    //공정체크 
                    if (dt.Rows[0]["ST_STATUS_YN"].ToString().Equals("Y"))
                        chk_stock_yn.Checked = true;
                    else
                        chk_stock_yn.Checked = false;

                    cmb_raw_stor.SelectedValue = dt.Rows[0]["RAW_STORAGE"].ToString();
                    cmb_chugjong_txt.SelectedValue = dt.Rows[0]["CHUGJONG_CD"].ToString();
                    cmb_class_txt.SelectedValue = dt.Rows[0]["CLASS_CD"].ToString();
                    cmb_grade_txt.SelectedValue = dt.Rows[0]["GRADE_CD"].ToString();
                    cmb_country_txt.SelectedValue = dt.Rows[0]["COUNTRY_CD"].ToString();
                    txt_hamyang_txt.Text= dt.Rows[0]["HAMYANG"].ToString();
                    txt_label_nm_txt.Text = dt.Rows[0]["LABEL_NM"].ToString();
                    txt_box_amt.Text = dt.Rows[0]["BOX_AMT"].ToString();
                    txt_exprt_count.Text = dt.Rows[0]["EXPRT_COUNT"].ToString();
                    cmb_pattern_gubun.SelectedValue = dt.Rows[0]["PATTERN_CD"].ToString();
                    cmb_vat_cd.SelectedValue = dt.Rows[0]["VAT_CD"].ToString();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void cmb_input_unit_SelectedIndexChanged(object sender, EventArgs e)
        {
           u_change_logic();
        }

        private void cmb_output_unit_SelectedIndexChanged(object sender, EventArgs e)
        {
            u_change_logic();
        }

        private void u_change_logic() 
        {
            if (((string)cmb_output_unit.SelectedValue == "" || cmb_output_unit.SelectedValue == null) || (string)cmb_input_unit.SelectedValue == "" || cmb_input_unit.SelectedValue == null)
            {
                txt_conver_ratio.Text = "0";
            }
            else 
            {
                string out_unit = cmb_output_unit.SelectedValue.ToString().ToLower();
                string in_unit = cmb_input_unit.SelectedValue.ToString().ToLower();
                string out_unit_nm = cmb_output_unit.Text.ToLower();
                string in_unit_nm = cmb_input_unit.Text.ToLower();

                if (cmb_input_unit.SelectedValue.ToString().Equals(cmb_output_unit.SelectedValue.ToString()))
                {
                    txt_conver_ratio.Text = "1";
                }
                else
                {
                    if (in_unit_nm.ToString().Equals("kg") && out_unit_nm.ToString().Equals("g"))
                    {
                        txt_conver_ratio.Text = "0.001";
                    }
                    else if (in_unit_nm.ToString().Equals("g") && out_unit_nm.ToString().Equals("kg"))
                    {
                        txt_conver_ratio.Text = "1,000"; 
                    }
                    else if (in_unit_nm.ToString().Equals("l") && out_unit_nm.ToString().Equals("ml"))
                    {
                        txt_conver_ratio.Text = "0.001";
                    }
                    else if (in_unit_nm.ToString().Equals("ml") && out_unit_nm.ToString().Equals("l"))
                    {
                        txt_conver_ratio.Text = "1,000";
                    }
                    else
                    {
                        txt_conver_ratio.Text = "1";
                    }
                }
            }
        }

        private void btn_Cust_Click(object sender, EventArgs e)
        {
            Popup.pop거래처검색 frm = new Popup.pop거래처검색();

            frm.sCustGbn = "2";
            frm.ShowDialog();

            if (frm.sCode != "")
            {
                cmb_cust.SelectedValue = frm.sCode.Trim();
            }
            else
            {
               // txt_cust_cd.Text = old_cust_nm;
            }

            frm.Dispose();
            frm = null;
        }

        private void cmb_input_unit_SelectedValueChanged(object sender, EventArgs e)
        {
            cmb_output_unit.SelectedValue = cmb_input_unit.SelectedValue;
        }

        private void txt_srch_Leave(object sender, EventArgs e)
        {
            wnDm wDm = new wnDm();
            DataTable dt = null;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("   WHERE 1 = 1  ");


            if (cmb_cd_srch.SelectedIndex == 1)
            {
                if (!txt_srch.Text.ToString().Equals(""))
                {
                    sb.AppendLine(" AND RAW_MAT_CD like '%" + txt_srch.Text.ToString() + "%'  ");
                }
            }
            else if (cmb_cd_srch.SelectedIndex == 2)
            {
                if (!txt_srch.Text.ToString().Equals(""))
                {
                    sb.AppendLine("  AND LABEL_NM like '%" + txt_srch.Text.ToString() + "%'  ");
                }
            }
            else
            {
                if (!txt_srch.Text.ToString().Equals(""))
                {
                    sb.AppendLine("  AND (LABEL_NM like '%" + txt_srch.Text.ToString() + "%' OR RAW_MAT_CD like '%" + txt_srch.Text.ToString() + "%' ) ");
                }
            }

            if (cmb_used_srch.SelectedIndex != 0)
            {
                sb.AppendLine(" AND USED_CD = '" + cmb_used_srch.SelectedValue.ToString() + "' ");
            }

            if (cmb_raw2.SelectedIndex != 0)
            {
                sb.AppendLine(" AND RAW_MAT_GUBUN = '" + cmb_raw2.SelectedValue.ToString() + "' ");
            }


            dt = wDm.fn_Raw_List(sb.ToString());

            raw_list_rs(dataRawCdGrid, dt, lbl_cnt);
        }
    }
}
