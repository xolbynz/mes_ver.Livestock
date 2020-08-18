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


namespace 스마트팩토리.P85_BAS
{
    public partial class frm공정분류 : Form
    {
        private wnGConstant wConst = new wnGConstant();

        public frm공정분류()
        {
            InitializeComponent();
        }

        private void frm공정코드등록_Load(object sender, EventArgs e)
        {

            ComInfo.gridHeaderSet(dataTypeGrid);
            ComInfo.gridHeaderSet(dataLineGrid);
            ComInfo.gridHeaderSet(dataPoorGrid);
            ComInfo.gridHeaderSet(dataUnitGrid);
            ComInfo.gridHeaderSet(dataChugJonggridView);
            ComInfo.gridHeaderSet(dataExprtGridView);
            ComInfo.gridHeaderSet(dataCountryGridView);
            ComInfo.gridHeaderSet(dataMeatClassGridView);
            ComInfo.gridHeaderSet(dataSlauHouseGridView);
            ComInfo.gridHeaderSet(dataGradeGridView);
            ComInfo.gridHeaderSet(locGrid);
            ComInfo.gridHeaderSet(GridRecord);

            btnDelete.Enabled = false;
            init_ComboBox();
            type_list();
            line_list();
            poor_list();
            unit_list();
            chugjong_list();
            Exprt_list();
            Country_list();
            MeatClass_list();
            SlauHouse_list();
            Grade_list();
            Storage_list();
            loc_list();
        
            
        }

        

        #region common logic

        private void init_ComboBox() // 콤보 박스 구성
        {
            ComInfo comInfo = new ComInfo();
            string sqlQuery = "";
            //유통일수 쿰보박스
            cmb_exprt_gbn.Items.Add("M");
            cmb_exprt_gbn.Items.Add("D");

            //냉장냉동 콤보박스(일,월)
            ////cmb_SlauHouse_dategbn.Items.Add("M");
           // cmb_SlauHouse_dategbn.Items.Add("D");

            //원산지 콤보박스
            cmb_Country_usdcd.ValueMember = "코드";
            cmb_Country_usdcd.DisplayMember = "명칭";
            sqlQuery = comInfo.queryUsedYn();
            wConst.ComboBox_Read_Blank(cmb_Country_usdcd, sqlQuery);

           

            //불량유형 시작
            cmb_poor.ValueMember = "코드";
            cmb_poor.DisplayMember = "명칭";
            sqlQuery = comInfo.queryPoor(); //" select TYPE_CD as 코드, TYPE_NM as 명칭 from N_TYPE_CODE where 1=1 and POOR_TYPE_YN = 'Y'";
            wConst.ComboBox_Read_Blank(cmb_poor, sqlQuery);

            cmb_poor_type.ValueMember = "코드";
            cmb_poor_type.DisplayMember = "명칭";
            wConst.ComboBox_Read_Blank(cmb_poor_type, sqlQuery);
            //불량유형 끝

            //담당자 
            cmb_manager.ValueMember = "코드";
            cmb_manager.DisplayMember = "명칭";
            sqlQuery = comInfo.queryStaff();
            wConst.ComboBox_Read_Blank(cmb_manager, sqlQuery);

            //창고 
            cmb_storage.ValueMember = "코드";
            cmb_storage.DisplayMember = "명칭";
            sqlQuery = comInfo.queryStorage();
            wConst.ComboBox_Read_Blank(cmb_storage, sqlQuery);

            

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;

            if (tabGrade.SelectedIndex == 0) //유형 탭 선택
            { //사용자 등록
                txt_type_cd.Enabled = true;
                txt_type_cd.Text = "";
                txt_type_nm.Text = "";
                txt_type_cmt.Text = "";
                lbl_type_gbn.Text = "";
                chk_type.Checked = false;

            }
            else if (tabGrade.SelectedIndex == 1) // 단위 탭 선택
            {
                lbl_unit_gbn.Text = "";
                txt_unit_cd.Text = "";
                txt_unit_cd.Enabled = true;
                txt_unit_nm.Text = "";
                txt_unit_cmt.Text = "";
            }
            else if (tabGrade.SelectedIndex == 2) // 라인 탭 선택
            {
                lbl_line_gbn.Text = "";
                txt_line_cd.Text = "";
                txt_line_cd.Enabled = true;
                txt_line_nm.Text = "";
                txt_line_cmt.Text = "";
            }
            else if (tabGrade.SelectedIndex == 3) // 불량 탭 선택
            {
                lbl_poor_gbn.Text = "";
                txt_poor_cd.Text = "";
                txt_poor_cd.Enabled = true;
                txt_poor_nm.Text = "";
                txt_poor_cmt.Text = "";
                cmb_poor.SelectedIndex = 0;
            }
            else if (tabGrade.SelectedIndex == 4) // 축종 탭 선택
            {
                lbl_ChugJong_gbn.Text = "";
                txt_ChugJong_cd.Text = "";
                txt_ChugJong_cd.Enabled = true;
                txt_ChugJong_nm.Text = "";
                txt_ChugJong_cmt.Text = "";
            }
            else if (tabGrade.SelectedIndex == 5) // 유통 일수 탭 선택
            {
                lbl_exprt_gbn.Text = "";
                txt_exprt_date.Text = "";
                txt_exprt_date.Enabled = true;
                cmb_exprt_gbn.SelectedItem = null;
            }
            else if (tabGrade.SelectedIndex == 6) // 원산지 탭 선택
            {
                lbl_country_gbn.Text = "";
                txt_Country_cd.Text = "";
                txt_Country_cd.Enabled = true;
                txt_Country_nm.Text = "";
                txt_Country_cmt.Text = "";
                cmb_Country_usdcd.SelectedIndex = 0;
            }
            else if (tabGrade.SelectedIndex == 7) // 육류 분류 탭 선택
            {
                lbl_MeatClass_gbn.Text = "";
                txt_MeatClass_cd.Text = "";
                txt_MeatClass_cd.Enabled = true;
                txt_MeatClass_nm.Text = "";
                txt_HamYang.Text = "";
            }
            else if (tabGrade.SelectedIndex == 8) // 도축장 탭 선택
            {
                lbl_saluhouse_gubun.Text = "";
                txt_saluhouse_cd.Text = "";
                txt_saluhouse_cd.Enabled = true;
                txt_slauhouse_nm.Text = "";
               
                txt_slauhouse_cmt.Text = "";
            }
            else if(tabGrade.SelectedIndex == 9) // 육류 등급 탭 선택
            {
                lbl_grade_gbn.Text = "";
                txt_Grade_cd.Text = "";
                txt_Grade_nm.Text = "";
            }
            else if (tabGrade.SelectedIndex == 10) // 창고 탭 선택
            {
                txt_storage_cd.Enabled = true;
                lbl_storage_gubun.Text = "";
                txt_storage_cd.Text = "";
                txt_storage_nm.Text = "";
            }
            else if (tabGrade.SelectedIndex == 11) // LOC탭 선택
            {
                txt_loc_cd.Enabled = true;
                lbl_loc_gubun.Text = "";
                txt_loc_cd.Text = "";
                txt_loc_nm.Text = "";
                txt_loc_cmt.Text = "";
                cmb_storage.SelectedIndex = 0;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (tabGrade.SelectedIndex == 0)  // 유형 삭제
            {
                type_del();
            }
            else if (tabGrade.SelectedIndex == 1) // 단위 삭제
            {
                unit_del();
            }
            else if (tabGrade.SelectedIndex == 2) // 라인 삭제
            {
                line_del();
            }
            else if (tabGrade.SelectedIndex == 3) // 불량 삭제
            {
                poor_del();
            }
            else if (tabGrade.SelectedIndex == 4) // 축종 삭제
            {
                chugjong_del();
            }
            else if (tabGrade.SelectedIndex == 5) // 유통 일수 삭제
            {
                exprt_del();
            }
            else if (tabGrade.SelectedIndex == 6) // 원산지 삭제
            {
                country_del();
            }
            else if (tabGrade.SelectedIndex == 7) // 육류 분류 삭제
            {
                meatclass_del();
            }
            else if (tabGrade.SelectedIndex == 8) // 냉장 냉동 삭제
            {
                SlauHouse_del();
            }
            else if(tabGrade.SelectedIndex == 9)// 육류 등급 삭제
            {
                grade_del();
            }
            else if (tabGrade.SelectedIndex == 10) //창고 삭제
            {
                storage_del();
            }
            else if (tabGrade.SelectedIndex == 11) // LOC 삭제
            {
                loc_del();
            }
        }

        

        private void btnSave_Click(object sender, EventArgs e) // 코드 저장
        {
            if (tabGrade.SelectedIndex == 0)
            { //유형 등록
                type_logic();
            }
            else if (tabGrade.SelectedIndex == 1) // 단위 등록
            {
                unit_logic();
            }
            else if (tabGrade.SelectedIndex == 2) // 라인 등록
            {
                line_logic();
            }
            else if (tabGrade.SelectedIndex == 3) // 불량 등록
            {
                poor_logic();
            }
            else if (tabGrade.SelectedIndex == 4) // 축종 등록
            {
                chugjong_logic();
            }
            else if (tabGrade.SelectedIndex == 5) // 유통 일수 등록
            {
                exprt_logic();
            }
            else if (tabGrade.SelectedIndex == 6) // 원산지 등록
            {
                country_logic();
            }
            else if (tabGrade.SelectedIndex == 7) // 육류 분류 등록
            {
                meatClass_logic();
            }
            else if (tabGrade.SelectedIndex == 8) //도축장 등록
            {
                SlauHouse_logic();
            }
            else if(tabGrade.SelectedIndex == 9) // 육류 드급 등록
            {
                grade_logic();
            }
            else if (tabGrade.SelectedIndex == 10) //창고 등록
            {
                storage_logic();
            }
            else if (tabGrade.SelectedIndex == 11) // LOC 삭제
            {
                loc_logic();
            }

        }

        

        

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabGrade.SelectedIndex == 0) // 유형 그리드뷰
            {
                type_list();
            }
            else if (tabGrade.SelectedIndex == 1) // 단위 그리드뷰
            {
                unit_list();
            }
            else if (tabGrade.SelectedIndex == 2) // 라인 그리드뷰
            {
                line_list();
            }
            else if (tabGrade.SelectedIndex == 3)// 불량 그리드뷰 
            {
                poor_list();
            }
            else if (tabGrade.SelectedIndex == 4) // 축종 그리드뷰
            {
                chugjong_list();
            }
            else if (tabGrade.SelectedIndex == 5) // 유통 일수 그리드뷰
            {
                Exprt_list();
            }
            else if (tabGrade.SelectedIndex == 6) // 원산지 그리드뷰
            {
                Country_list();
            }
            else if (tabGrade.SelectedIndex == 7) // 육류 분류 그리드뷰
            {
                MeatClass_list();
            }
            else if (tabGrade.SelectedIndex == 8) // 도축장 그리드뷰 
            {
                SlauHouse_list();
            }
            else if (tabGrade.SelectedIndex == 9) // 육류 등급 그리드뷰
            {
                Grade_list();
            }
            else if (tabGrade.SelectedIndex == 10) //창고 그리드뷰
            {
                Storage_list();
            }
            else if (tabGrade.SelectedIndex == 11) // LOC 삭제
            {
                loc_list();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion common logic

        #region type logic
        private void type_logic()
        {
            if (txt_type_cd.Text.ToString().Equals(""))
            {
                MessageBox.Show("유형코드를 입력하시기 바랍니다.");
                return;
            }
            if (txt_type_nm.Text.ToString().Equals(""))
            {
                MessageBox.Show("유형명을 입력하시기 바랍니다.");
                return;
            }
            if (lbl_type_gbn.Text != "1")
            {
                wnDm wDm = new wnDm();
                string chk_type_yn = "";
                if (chk_type.Checked == true)
                {
                    chk_type_yn = "Y";
                }
                else
                {
                    chk_type_yn = "N";
                }
                int rsNum = wDm.insertType(txt_type_cd.Text.ToString(), txt_type_nm.Text.ToString(), chk_type_yn, txt_type_cmt.Text.ToString());

                if (rsNum == 0)
                {
                    txt_type_cd.Text = "";
                    txt_type_nm.Text = "";
                    txt_type_cmt.Text = "";
                    chk_type.Checked = false;

                    type_list();
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
                string chk_type_yn = "";
                if (chk_type.Checked == true)
                {
                    chk_type_yn = "Y";
                }
                else
                {
                    chk_type_yn = "N";
                }
                int rsNum = wDm.updateType(txt_type_cd.Text.ToString(), txt_type_nm.Text.ToString(), chk_type_yn, txt_type_cmt.Text.ToString());
                if (rsNum == 0)
                {
                    type_list();
                    MessageBox.Show("성공적으로 수정하였습니다.");
                }
                else if (rsNum == 1)
                    MessageBox.Show("저장에 실패하였습니다");
                else
                    MessageBox.Show("Exception 에러");
            }
        }

        private void type_del()
        {
            wnDm wDm = new wnDm();
            int rsNum = wDm.deleteType(txt_type_cd.Text.ToString());
            if (rsNum == 0)
            {
                lbl_type_gbn.Text = "";
                txt_type_cd.Enabled = true;
                txt_type_cd.Text = "";
                txt_type_nm.Text = "";
                txt_type_cmt.Text = "";
                chk_type.Checked = false;

                btnDelete.Enabled = false;

                type_list();
                MessageBox.Show("성공적으로 삭제하였습니다.");
            }
            else if (rsNum == 1)
            {
                MessageBox.Show("삭제에 실패하였습니다.");
            }
        }

        private void type_list()
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Type_List();

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.dataTypeGrid.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataTypeGrid.Rows[i].Cells["TTYPE_CD"].Value = dt.Rows[i]["TYPE_CD"].ToString();
                        dataTypeGrid.Rows[i].Cells["TTYPE_NM"].Value = dt.Rows[i]["TYPE_NM"].ToString();
                        dataTypeGrid.Rows[i].Cells["TCOMMENT"].Value = dt.Rows[i]["COMMENT"].ToString();

                        dataTypeGrid.Rows[i].Cells["POOR_TYPE_YN"].Value = dt.Rows[i]["POOR_TYPE_YN"].ToString();
                        dataTypeGrid.Rows[i].Cells["POOR_YN"].Value = dt.Rows[i]["POOR_TYPE_YN"].ToString().Equals("Y") ? "예" : "아니오";

                    }
                }
                else
                {
                    dataTypeGrid.Rows.Clear();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void dataTypeGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            btnDelete.Enabled = true;
            lbl_type_gbn.Text = "1";
            txt_type_cd.Enabled = false;
            txt_type_cd.Text = dataTypeGrid.Rows[e.RowIndex].Cells["TTYPE_CD"].Value.ToString();
            txt_type_nm.Text = dataTypeGrid.Rows[e.RowIndex].Cells["TTYPE_NM"].Value.ToString();
            txt_type_cmt.Text = dataTypeGrid.Rows[e.RowIndex].Cells["TCOMMENT"].Value.ToString();
            if (dataTypeGrid.Rows[e.RowIndex].Cells["POOR_TYPE_YN"].Value.ToString().Equals("Y"))
                chk_type.Checked = true;
            else
                chk_type.Checked = false;

        }

        private void txt_type_cd_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e);
        }
        #endregion type logic

        #region unit logic
        private void unit_logic()
        {
            if (txt_unit_cd.Text.ToString().Equals(""))
            {
                MessageBox.Show("단위코드를 입력하시기 바랍니다.");
                return;
            }
            if (txt_unit_nm.Text.ToString().Equals(""))
            {
                MessageBox.Show("단위명을 입력하시기 바랍니다.");
                return;
            }
            if (lbl_unit_gbn.Text != "1")
            {
                wnDm wDm = new wnDm();
                int rsNum = wDm.insertUnit(txt_unit_cd.Text.ToString(), txt_unit_nm.Text.ToString(), txt_unit_cmt.Text.ToString());

                if (rsNum == 0)
                {
                    txt_unit_cd.Text = "";
                    txt_unit_nm.Text = "";
                    txt_unit_cmt.Text = "";

                    unit_list();
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
                int rsNum = wDm.updateUnit(txt_unit_cd.Text.ToString(), txt_unit_nm.Text.ToString(), txt_unit_cmt.Text.ToString());
                if (rsNum == 0)
                {
                    unit_list();
                    MessageBox.Show("성공적으로 수정하였습니다.");
                }
                else if (rsNum == 1)
                    MessageBox.Show("저장에 실패하였습니다");
                else
                    MessageBox.Show("Exception 에러2");
            }
        }

        private void unit_del()
        {
            wnDm wDm = new wnDm();
            int rsNum = wDm.deleteUnit(txt_unit_cd.Text.ToString());
            if (rsNum == 0)
            {
                lbl_unit_gbn.Text = "";
                txt_unit_cd.Enabled = true;
                txt_unit_cd.Text = "";
                txt_unit_nm.Text = "";
                txt_unit_cmt.Text = "";
                btnDelete.Enabled = false;

                unit_list();
                MessageBox.Show("성공적으로 삭제하였습니다.");
            }
            else if (rsNum == 1)
            {
                MessageBox.Show("삭제에 실패하였습니다.");
            }
        }

        private void unit_list()
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Unit_List();

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.dataUnitGrid.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataUnitGrid.Rows[i].Cells[0].Value = dt.Rows[i]["UNIT_CD"].ToString();
                        dataUnitGrid.Rows[i].Cells[1].Value = dt.Rows[i]["UNIT_NM"].ToString();
                        dataUnitGrid.Rows[i].Cells[2].Value = dt.Rows[i]["COMMENT"].ToString();
                    }
                }
                else
                {
                    dataUnitGrid.Rows.Clear();
                }
            }
            catch (Exception e)
            {

            }
        }

        private void dataUnitGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            btnDelete.Enabled = true;
            lbl_unit_gbn.Text = "1";
            txt_unit_cd.Enabled = false;
            txt_unit_cd.Text = dataUnitGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
            txt_unit_nm.Text = dataUnitGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
            txt_unit_cmt.Text = dataUnitGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void txt_unit_cd_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e);
        }

        #endregion unit logic

        #region line logic
        private void line_logic()
        {
            if (txt_line_cd.Text.ToString().Equals(""))
            {
                MessageBox.Show("라인코드를 입력하시기 바랍니다.");
                return;
            }
            if (txt_line_nm.Text.ToString().Equals(""))
            {
                MessageBox.Show("라인명을 입력하시기 바랍니다.");
                return;
            }
            if (lbl_line_gbn.Text != "1")
            {
                wnDm wDm = new wnDm();
                int rsNum = wDm.insertLine(txt_line_cd.Text.ToString(), txt_line_nm.Text.ToString(), txt_line_cmt.Text.ToString());

                if (rsNum == 0)
                {
                    txt_line_cd.Text = "";
                    txt_line_nm.Text = "";
                    txt_line_cmt.Text = "";
                    line_list();
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
                int rsNum = wDm.updateLine(txt_line_cd.Text.ToString(), txt_line_nm.Text.ToString(), txt_line_cmt.Text.ToString());
                if (rsNum == 0)
                {
                    line_list();
                    MessageBox.Show("성공적으로 수정하였습니다.");
                }
                else if (rsNum == 1)
                    MessageBox.Show("저장에 실패하였습니다");
                else
                    MessageBox.Show("Exception 에러2");
            }
        }
        private void line_del()
        {
            wnDm wDm = new wnDm();
            int rsNum = wDm.deleteLine(txt_line_cd.Text.ToString());
            if (rsNum == 0)
            {
                lbl_line_gbn.Text = "";
                txt_line_cd.Enabled = true;
                txt_line_cd.Text = "";
                txt_line_nm.Text = "";
                txt_line_cmt.Text = "";
                btnDelete.Enabled = false;
                line_list();
                MessageBox.Show("성공적으로 삭제하였습니다.");
            }
            else if (rsNum == 1)
            {
                MessageBox.Show("삭제에 실패하였습니다.");
            }
        }

        private void line_list()
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Line_List();

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.dataLineGrid.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataLineGrid.Rows[i].Cells[0].Value = dt.Rows[i]["LINE_CD"].ToString();
                        dataLineGrid.Rows[i].Cells[1].Value = dt.Rows[i]["LINE_NM"].ToString();
                        dataLineGrid.Rows[i].Cells[2].Value = dt.Rows[i]["COMMENT"].ToString();
                    }
                }
                else
                {
                    dataLineGrid.Rows.Clear();
                }
            }
            catch (Exception e)
            {

            }
        }

        private void dataLineGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            btnDelete.Enabled = true;
            lbl_line_gbn.Text = "1";
            txt_line_cd.Enabled = false;
            txt_line_cd.Text = dataLineGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
            txt_line_nm.Text = dataLineGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
            txt_line_cmt.Text = dataLineGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void txt_line_cd_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e);
        }

        #endregion line logic
        #region poor logic


        private void poor_logic() // 불량 등록
        {
            if (cmb_poor.SelectedValue == null) cmb_poor.SelectedValue = "";

            if (txt_poor_cd.Text.ToString().Equals(""))
            {
                MessageBox.Show("불량코드를 입력하시기 바랍니다.");
                return;
            }
            if (txt_poor_nm.Text.ToString().Equals(""))
            {
                MessageBox.Show("불량명을 입력하시기 바랍니다.");
                return;
            }
            if (cmb_poor.SelectedIndex == 0)
            {
                MessageBox.Show("불량유형을 선택하시기 바랍니다.");
                return;
            }
            if (lbl_poor_gbn.Text != "1")
            {
                wnDm wDm = new wnDm();
                int rsNum = wDm.insertPoor(txt_poor_cd.Text.ToString(),
                    txt_poor_nm.Text.ToString(),
                    cmb_poor.SelectedValue.ToString(),
                    txt_poor_cmt.Text.ToString());

                if (rsNum == 0)
                {
                    txt_poor_cd.Text = "";
                    txt_poor_nm.Text = "";
                    txt_poor_cmt.Text = "";
                    cmb_poor.SelectedIndex = 0;

                    poor_list();
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
                int rsNum = wDm.updatePoor(txt_poor_cd.Text.ToString(),
                    txt_poor_nm.Text.ToString(),
                    cmb_poor.SelectedValue.ToString(),
                    txt_poor_cmt.Text.ToString());

                if (rsNum == 0)
                {
                    poor_list();
                    MessageBox.Show("성공적으로 수정하였습니다.");
                }
                else if (rsNum == 1)
                    MessageBox.Show("저장에 실패하였습니다");
                else
                    MessageBox.Show("Exception 에러2");
            }
        }

        private void poor_list() // 불량 그리드뷰
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Poor_List();

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.dataPoorGrid.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataPoorGrid.Rows[i].Cells["POOR_CD"].Value = dt.Rows[i]["POOR_CD"].ToString();
                        dataPoorGrid.Rows[i].Cells["POOR_NM"].Value = dt.Rows[i]["POOR_NM"].ToString();
                        dataPoorGrid.Rows[i].Cells["TYPE_CD"].Value = dt.Rows[i]["TYPE_CD"].ToString();
                        dataPoorGrid.Rows[i].Cells["TYPE_NM"].Value = dt.Rows[i]["TYPE_NM"].ToString();
                        dataPoorGrid.Rows[i].Cells["COMMENT"].Value = dt.Rows[i]["COMMENT"].ToString();
                    }
                }
                else
                {
                    dataPoorGrid.Rows.Clear();
                }
            }
            catch (Exception e)
            {

            }
        }


        private void dataPoorGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e) // 불량 그리드뷰 선택
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            btnDelete.Enabled = true;
            lbl_poor_gbn.Text = "1";
            txt_poor_cd.Enabled = false;
            txt_poor_cd.Text = dataPoorGrid.Rows[e.RowIndex].Cells["POOR_CD"].Value.ToString();
            txt_poor_nm.Text = dataPoorGrid.Rows[e.RowIndex].Cells["POOR_NM"].Value.ToString();
            cmb_poor.SelectedValue = dataPoorGrid.Rows[e.RowIndex].Cells["TYPE_CD"].Value.ToString();
            txt_poor_cmt.Text = dataPoorGrid.Rows[e.RowIndex].Cells["COMMENT"].Value.ToString();

        }
        private void poor_del() // 불량 삭제
        {
            wnDm wDm = new wnDm();
            int rsNum = wDm.deletePoor(txt_poor_cd.Text.ToString());
            if (rsNum == 0)
            {
                lbl_poor_gbn.Text = "";
                txt_poor_cd.Enabled = true;
                txt_poor_cd.Text = "";
                txt_poor_nm.Text = "";
                txt_poor_cmt.Text = "";
                cmb_poor.SelectedIndex = 0;
                btnDelete.Enabled = false;

                poor_list();
                MessageBox.Show("성공적으로 삭제하였습니다.");
            }
            else if (rsNum == 1)
            {
                MessageBox.Show("삭제에 실패하였습니다.");
            }
        }
        #endregion poor logic

        private void chugjong_logic() // 축종 등록
        {

            if (txt_ChugJong_cd.Text.ToString().Equals(""))
            {
                MessageBox.Show("축종코드를 입력하시기 바랍니다.");
                return;
            }
            if (txt_ChugJong_nm.Text.ToString().Equals(""))
            {
                MessageBox.Show("축종명을 입력하시기 바랍니다.");
                return;
            }
            if (lbl_ChugJong_gbn.Text != "1")
            {
                wnDm wDm = new wnDm();
                int rsNum = wDm.insertChugjong(txt_ChugJong_cd.Text.ToString(),
                    txt_ChugJong_nm.Text.ToString(),
                    txt_ChugJong_cmt.Text.ToString());

                if (rsNum == 0)
                {
                    txt_ChugJong_cd.Text = "";
                    txt_ChugJong_nm.Text = "";
                    txt_ChugJong_cmt.Text = "";


                    chugjong_list();
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
                int rsNum = wDm.updateChugJong(txt_ChugJong_cd.Text.ToString(),
                    txt_ChugJong_nm.Text.ToString(),
                    txt_ChugJong_cmt.Text.ToString());

                if (rsNum == 0)
                {
                    chugjong_list();
                    MessageBox.Show("성공적으로 수정하였습니다.");
                }
                else if (rsNum == 1)
                    MessageBox.Show("저장에 실패하였습니다");
                else
                    MessageBox.Show("Exception 에러2");
            }
        }



        private void chugjong_list() // 축종 그리드뷰
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_ChugJong_List();
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.dataChugJonggridView.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataChugJonggridView.Rows[i].Cells[0].Value = dt.Rows[i]["CHUGJONG_CD"].ToString();
                        dataChugJonggridView.Rows[i].Cells[1].Value = dt.Rows[i]["CHUGJONG_NM"].ToString();
                        dataChugJonggridView.Rows[i].Cells[2].Value = dt.Rows[i]["COMMENT"].ToString();
                    }
                }
                else
                {
                    dataChugJonggridView.Rows.Clear();
                }
            }
            catch (Exception e)
            {

            }

        }

        private void dataChugJonggridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e) // 축종 그리드뷰 선택
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            btnDelete.Enabled = true;
            lbl_ChugJong_gbn.Text = "1";
            txt_ChugJong_cd.Enabled = false;
            txt_ChugJong_cd.Text = dataChugJonggridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            txt_ChugJong_nm.Text = dataChugJonggridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            txt_ChugJong_cmt.Text = dataChugJonggridView.Rows[e.RowIndex].Cells[2].Value.ToString();
        }


        private void chugjong_del() // 축종 삭제
        {
            wnDm wDm = new wnDm();
            int rsNum = wDm.deleteChugJong(txt_ChugJong_cd.Text.ToString());
            if (rsNum == 0)
            {
                lbl_ChugJong_gbn.Text = "";
                txt_ChugJong_cd.Enabled = true;
                txt_ChugJong_cd.Text = "";
                txt_ChugJong_nm.Text = "";
                txt_ChugJong_cmt.Text = "";
                btnDelete.Enabled = false;

                chugjong_list();
                MessageBox.Show("성공적으로 삭제하였습니다.");
            }
            else if (rsNum == 1)
            {
                MessageBox.Show("삭제에 실패하였습니다.");
            }
        }


        // 유통 일수 관련


        private void exprt_logic() // 유통 일수 등록
        {
            wnDm wDm = new wnDm();
            if (txt_exprt_date.Text.ToString().Equals(""))
            {
                MessageBox.Show("유통일수를 입력하시기 바랍니다.");
                return;
            }
            else if (wDm.numChk(txt_exprt_date.Text.ToString()) == false){
                MessageBox.Show("유통일수는 숫자만 입력하시기 바랍니다.");
                return;
            }
            else if (cmb_exprt_gbn.SelectedItem.ToString().Equals(""))
            {
                MessageBox.Show("일, 월 구분을 입력하시기 바랍니다.");
                return;
            }
            if (lbl_exprt_gbn.Text != "1")
            {
                int rsNum = wDm.insertExprt(txt_exprt_date.Text.ToString(),
                    cmb_exprt_gbn.SelectedItem.ToString());

                if (rsNum == 0)
                {
                    txt_exprt_date.Text = "";
                    cmb_exprt_gbn.SelectedIndex = 0;

                    Exprt_list();
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
                int rsNum = wDm.updateExprt(txt_exprt_date.Text.ToString(),
                    cmb_exprt_gbn.SelectedItem.ToString());

                if (rsNum == 0)
                {
                    Exprt_list();
                    MessageBox.Show("성공적으로 수정하였습니다.");
                }
                else if (rsNum == 1)
                    MessageBox.Show("저장에 실패하였습니다");
                else
                    MessageBox.Show("Exception 에러2");
            }
        }


        private void Exprt_list() // 유통 일수 그리드뷰
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Exprt_List();
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.dataExprtGridView.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataExprtGridView.Rows[i].Cells[0].Value = dt.Rows[i]["EXPRT_COUNT"].ToString();
                        dataExprtGridView.Rows[i].Cells[1].Value = dt.Rows[i]["EXPRT_GUBUN"].ToString();
                    }
                }
                else
                {
                    dataExprtGridView.Rows.Clear();
                }
            }
            catch (Exception e)
            {

            }

        }

        private void dataExprtGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e) // 유통 일수 그리드뷰 선택
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            btnDelete.Enabled = true;
            lbl_exprt_gbn.Text = "1";
            txt_exprt_date.Enabled = false;
            txt_exprt_date.Text = dataExprtGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            cmb_exprt_gbn.SelectedItem = dataExprtGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void exprt_del() // 유통 일수 삭제
        {
            wnDm wDm = new wnDm();
            int rsNum = wDm.deleteExprt(txt_exprt_date.Text.ToString(), cmb_exprt_gbn.SelectedItem.ToString());
            if (rsNum == 0)
            {
                lbl_exprt_gbn.Text = "";
                txt_exprt_date.Enabled = true;
                txt_exprt_date.Text = "";
                cmb_exprt_gbn.SelectedIndex = 0;
                btnDelete.Enabled = false;
                Exprt_list();
                MessageBox.Show("성공적으로 삭제하였습니다.");
            }
            else if (rsNum == 1)
            {
                MessageBox.Show("삭제에 실패하였습니다.");
            }
        }

        // 원산지

        private void country_logic() // 원산지 등록
        {
            if (txt_Country_cd.Text.ToString().Equals(""))
            {
                MessageBox.Show("원산지코드를 입력하시기 바랍니다.");
                return;
            }
            if (txt_Country_nm.Text.ToString().Equals(""))
            {
                MessageBox.Show("원산지을 입력하시기 바랍니다.");
                return;
            }
            if (cmb_Country_usdcd.SelectedValue.ToString().Equals(""))
            {
                MessageBox.Show("사용여부를 입력하시기 바랍니다.");
                return;
            }

            if (lbl_country_gbn.Text != "1")
            {
                wnDm wDm = new wnDm();
                int rsNum = wDm.insertCountry(txt_Country_cd.Text.ToString(),
                    txt_Country_nm.Text.ToString(),
                    txt_Country_cmt.Text.ToString(),
                    cmb_Country_usdcd.SelectedValue.ToString());

                if (rsNum == 0)
                {
                    txt_Country_cd.Text = "";
                    txt_Country_nm.Text = "";
                    txt_Country_cmt.Text = "";
                    cmb_Country_usdcd.SelectedIndex= 0;

                    Country_list();
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
                int rsNum = wDm.updateCountry(txt_Country_cd.Text.ToString(),
                    txt_Country_nm.Text.ToString(),
                    txt_Country_cmt.Text.ToString(),
                    cmb_Country_usdcd.SelectedValue.ToString());

                if (rsNum == 0)
                {
                    Country_list();
                    MessageBox.Show("성공적으로 수정하였습니다.");
                }
                else if (rsNum == 1)
                    MessageBox.Show("저장에 실패하였습니다");
                else
                    MessageBox.Show("Exception 에러2");
            }
        }

        private void Country_list() // 원산지 그리드뷰
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Country_List();
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.dataCountryGridView.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataCountryGridView.Rows[i].Cells[0].Value = dt.Rows[i]["COUNTRY_CD"].ToString();
                        dataCountryGridView.Rows[i].Cells[1].Value = dt.Rows[i]["COUNTRY_NM"].ToString();
                        dataCountryGridView.Rows[i].Cells[2].Value = dt.Rows[i]["USED_NM"].ToString();
                        dataCountryGridView.Rows[i].Cells[3].Value = dt.Rows[i]["COMMENT"].ToString();
                        dataCountryGridView.Rows[i].Cells[4].Value = dt.Rows[i]["USED_CD"].ToString();
                    }
                }
                else
                {
                    dataMeatClassGridView.Rows.Clear();
                }
            }
            catch (Exception e)
            {

            }
        }
        private void dataCountryGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e) // 원산지 그리드뷰 선택
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            btnDelete.Enabled = true;
            lbl_country_gbn.Text = "1";
            txt_Country_cd.Enabled = false;
            txt_Country_cd.Text = dataCountryGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            txt_Country_nm.Text = dataCountryGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            cmb_Country_usdcd.SelectedValue = dataCountryGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
            txt_Country_cmt.Text = dataCountryGridView.Rows[e.RowIndex].Cells[3].Value.ToString();

        }

        private void country_del() // 원산지 삭제
        {
            wnDm wDm = new wnDm();
            int rsNum = wDm.deleteCountry(txt_Country_cd.Text.ToString());
            if (rsNum == 0)
            {
                lbl_country_gbn.Text = "";
                txt_Country_cd.Enabled = true;
                txt_Country_cd.Text = "";
                txt_Country_nm.Text = "";
                txt_Country_cmt.Text = "";
                cmb_Country_usdcd.SelectedIndex = 0;
                btnDelete.Enabled = false;
                Country_list();
                MessageBox.Show("성공적으로 삭제하였습니다.");
            }
            else if (rsNum == 1)
            {
                MessageBox.Show("삭제에 실패하였습니다.");
            }
        }



        // 육류 분류 관련

        private void meatClass_logic() // 육류 분류 등록
        {
            if (txt_MeatClass_cd.Text.ToString().Equals(""))
            {
                MessageBox.Show("분류코드를 입력하시기 바랍니다.");
                return;
            }
            if (txt_MeatClass_nm.Text.ToString().Equals(""))
            {
                MessageBox.Show("분류명을 입력하시기 바랍니다.");
                return;
            }
            if (txt_HamYang.Text.ToString().Equals(""))
            {
                MessageBox.Show("함량을 입력하시기 바랍니다.");
                return;
            }

            if (lbl_MeatClass_gbn.Text != "1")
            {
                wnDm wDm = new wnDm();
                int rsNum = wDm.insertMeatClass(txt_MeatClass_cd.Text.ToString(),
                    txt_MeatClass_nm.Text.ToString(),
                    txt_HamYang.Text.ToString());

                if (rsNum == 0)
                {
                    txt_MeatClass_cd.Text = "";
                    txt_MeatClass_nm.Text = "";
                    txt_HamYang.Text = "";

                    MeatClass_list();
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
                int rsNum = wDm.updateMeatClass(txt_MeatClass_cd.Text.ToString(),
                    txt_MeatClass_nm.Text.ToString(),
                    txt_HamYang.Text.ToString());

                if (rsNum == 0)
                {
                    MeatClass_list();
                    MessageBox.Show("성공적으로 수정하였습니다.");
                }
                else if (rsNum == 1)
                    MessageBox.Show("저장에 실패하였습니다");
                else
                    MessageBox.Show("Exception 에러2");
            }
        }

        private void MeatClass_list() // 육류 분류 그리드뷰
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_MeatClass_List();
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.dataMeatClassGridView.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataMeatClassGridView.Rows[i].Cells[0].Value = dt.Rows[i]["CLASS_CD"].ToString();
                        dataMeatClassGridView.Rows[i].Cells[1].Value = dt.Rows[i]["CLASS_NM"].ToString();
                        dataMeatClassGridView.Rows[i].Cells[2].Value = dt.Rows[i]["HAMYANG"].ToString();
                    }
                }
                else
                {
                    dataMeatClassGridView.Rows.Clear();
                }
            }
            catch (Exception e)
            {

            }
        }
         private void dataMeatClassGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e) // 육류 분류 그리드뷰 선택
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            btnDelete.Enabled = true;
            lbl_MeatClass_gbn.Text = "1";
            txt_MeatClass_cd.Enabled = false;
            txt_MeatClass_cd.Text = dataMeatClassGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            txt_MeatClass_nm.Text = dataMeatClassGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            txt_HamYang.Text = dataMeatClassGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
        }


         private void meatclass_del() // 육류 분류 삭제
         {
             wnDm wDm = new wnDm();
             int rsNum = wDm.deleteMeatClass(txt_MeatClass_cd.Text.ToString());
             if (rsNum == 0)
             {
                 lbl_MeatClass_gbn.Text = "";
                 txt_MeatClass_cd.Enabled = true;
                 txt_MeatClass_cd.Text = "";
                 txt_MeatClass_nm.Text = "";
                 txt_HamYang.Text = "";
                 btnDelete.Enabled = false;
                 MeatClass_list();
                 MessageBox.Show("성공적으로 삭제하였습니다.");
             }
             else if (rsNum == 1)
             {
                 MessageBox.Show("삭제에 실패하였습니다.");
             }
         }
           

        //  냉장 냉동 관련

        private void SlauHouse_logic() // 냉장 냉동 등록
        {
            wnDm wDm = new wnDm();
            if (txt_saluhouse_cd.Text.ToString().Equals(""))
            {
                MessageBox.Show("도축장코드를 입력하시기 바랍니다.");
                return;
            }

            if (txt_slauhouse_nm.Text.ToString().Equals(""))
            {
                MessageBox.Show("도축장명을 입력하시기 바랍니다.");
                return;
            }
            

            if (lbl_saluhouse_gubun.Text != "1")
            {
                int rsNum = wDm.insertSlauHouse(txt_saluhouse_cd.Text.ToString(),
                    txt_slauhouse_nm.Text.ToString(),
                    txt_slauhouse_cmt.Text);

                if (rsNum == 0)
                {
                    txt_saluhouse_cd.Text = "";
                    txt_slauhouse_nm.Text = "";
                    txt_slauhouse_cmt.Text = "";

                    SlauHouse_list();
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
                int rsNum = wDm.updateSlauHouse(txt_saluhouse_cd.Text.ToString(),
                    txt_slauhouse_nm.Text.ToString(),
                    txt_slauhouse_cmt.Text);

                if (rsNum == 0)
                {
                    SlauHouse_list();
                    MessageBox.Show("성공적으로 수정하였습니다.");
                }
                else if (rsNum == 1)
                    MessageBox.Show("저장에 실패하였습니다");
                else
                    MessageBox.Show("Exception 에러2");
            }
        }

        private void SlauHouse_list() // 냉장 냉동 그리드뷰
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_SlauHouse_List("where 1=1 ");
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.dataSlauHouseGridView.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataSlauHouseGridView.Rows[i].Cells[0].Value = dt.Rows[i]["SlauHouse_CD"].ToString();
                        dataSlauHouseGridView.Rows[i].Cells[1].Value = dt.Rows[i]["SlauHouse_NM"].ToString();
                        dataSlauHouseGridView.Rows[i].Cells[2].Value = dt.Rows[i]["COMMENT"].ToString();

                    }
                }
                else
                {
                    dataSlauHouseGridView.Rows.Clear();
                }
            }
            catch (Exception e)
            {

            }
        }


        private void dataSlauHouseGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e) // 냉장 냉동 그리드뷰 선택
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            btnDelete.Enabled = true;
            lbl_saluhouse_gubun.Text = "1";
            txt_saluhouse_cd.Enabled = false;
            txt_saluhouse_cd.Text = dataSlauHouseGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            txt_slauhouse_nm.Text = dataSlauHouseGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            txt_slauhouse_cmt.Text = dataSlauHouseGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

       

        private void SlauHouse_del() // 냉장 냉동 삭제
        {
            wnDm wDm = new wnDm();
            int rsNum = wDm.deleteSlauHouse(txt_saluhouse_cd.Text.ToString());
            if (rsNum == 0)
            {
                lbl_saluhouse_gubun.Text = "";
                txt_saluhouse_cd.Enabled = true;
                txt_saluhouse_cd.Text = "";
                txt_slauhouse_nm.Text = "";
                txt_slauhouse_cmt.Text = "";
                SlauHouse_list();
                MessageBox.Show("성공적으로 삭제하였습니다.");
            }
            else if (rsNum == 1)
            {
                MessageBox.Show("삭제에 실패하였습니다.");
            }
        }

        // 육류 등급

        private void grade_logic() // 육류 등급 등록
        {
            if (txt_Grade_cd.Text.ToString().Equals(""))
            {
                MessageBox.Show("등급코드를 입력하시기 바랍니다.");
                return;
            }
            if (txt_Grade_nm.Text.ToString().Equals(""))
            {
                MessageBox.Show("등급명을 입력하시기 바랍니다.");
                return;
            }
            

            if (lbl_grade_gbn.Text != "1")
            {
                wnDm wDm = new wnDm();
                int rsNum = wDm.insertGrade(txt_Grade_cd.Text.ToString(),
                    txt_Grade_nm.Text.ToString());

                if (rsNum == 0)
                {
                    txt_Grade_cd.Text = "";
                    txt_Grade_nm.Text = "";

                    Grade_list();
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
                int rsNum = wDm.updateGrade(txt_Grade_cd.Text.ToString(),
                    txt_Grade_nm.Text.ToString());

                if (rsNum == 0)
                {
                    Grade_list();
                    MessageBox.Show("성공적으로 수정하였습니다.");
                }
                else if (rsNum == 1)
                    MessageBox.Show("저장에 실패하였습니다");
                else
                    MessageBox.Show("Exception 에러2");
            }
        }

        private void storage_logic() //창고 등록 수정
        {
            if (txt_storage_cd.Text.ToString().Equals(""))
            {
                MessageBox.Show("창고코드를 입력하시기 바랍니다.");
                return;
            }
            if (txt_storage_nm.Text.ToString().Equals(""))
            {
                MessageBox.Show("창고명을 입력하시기 바랍니다.");
                return;
            }


            if (lbl_storage_gubun.Text != "1")
            {
                wnDm wDm = new wnDm();
                int rsNum = wDm.insertStor(txt_storage_cd.Text.ToString(),
                    txt_storage_nm.Text.ToString());

                if (rsNum == 0)
                {
                    txt_storage_cd.Text = "";
                    txt_storage_nm.Text = "";

                    Storage_list();
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
                int rsNum = wDm.updateStor(txt_storage_cd.Text.ToString(),
                    txt_storage_nm.Text.ToString());

                if (rsNum == 0)
                {
                    Grade_list();
                    MessageBox.Show("성공적으로 수정하였습니다.");
                }
                else if (rsNum == 1)
                    MessageBox.Show("저장에 실패하였습니다");
                else
                    MessageBox.Show("Exception 에러2");
            }
        }

        private void Grade_list() // 육류 등급 그리드뷰
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Grade_List();
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.dataGradeGridView.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataGradeGridView.Rows[i].Cells[0].Value = dt.Rows[i]["GRADE_CD"].ToString();
                        dataGradeGridView.Rows[i].Cells[1].Value = dt.Rows[i]["GRADE_NM"].ToString();
                    }
                }
                else
                {
                    dataGradeGridView.Rows.Clear();
                }
            }
            catch (Exception e)
            {

            }
        }

        private void Storage_list()
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_STORECODE_List("and 1=1 ");

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.GridRecord.RowCount = dt.Rows.Count;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.GridRecord.Rows[i].Cells[0].Value = dt.Rows[i]["STORAGE_CD"].ToString();
                        this.GridRecord.Rows[i].Cells[1].Value = dt.Rows[i]["STORAGE_NM"].ToString();
                        this.GridRecord.Rows[i].Cells[2].Value = dt.Rows[i]["COMMENT"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("검색중에 오류가 발생했습니다.");
                wnLog.writeLog(wnLog.LOG_ERROR, ex.Message + " - " + ex.ToString());
            }
        }

        private void dataGradeGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e) // 육류 등급 그리드뷰 선택
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            btnDelete.Enabled = true;
            lbl_grade_gbn.Text = "1";
            txt_Grade_cd.Enabled = false;
            txt_Grade_cd.Text = dataGradeGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            txt_Grade_nm.Text = dataGradeGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void grade_del() // 육류 등급 삭제
        {
            wnDm wDm = new wnDm();
            int rsNum = wDm.deleteGrade(txt_Grade_cd.Text.ToString());
            if (rsNum == 0)
            {
                lbl_grade_gbn.Text = "";
                txt_Grade_cd.Enabled = true;
                txt_Grade_cd.Text = "";
                txt_Grade_nm.Text = "";
                Grade_list();
                MessageBox.Show("성공적으로 삭제하였습니다.");
            }
            else if (rsNum == 1)
            {
                MessageBox.Show("삭제에 실패하였습니다.");
            }
        }
        private void storage_del() // 창고 삭제
        {
            wnDm wDm = new wnDm();
            int rsNum = wDm.deleteStor(txt_storage_cd.Text.ToString());
            if (rsNum == 0)
            {
                lbl_storage_gubun.Text = "";
                txt_storage_cd.Text = "";
                txt_storage_nm.Text = "";
                txt_storage_cd.Enabled = true;


                Storage_list();
                MessageBox.Show("성공적으로 삭제하였습니다.");
            }
            else if (rsNum == 1)
            {
                MessageBox.Show("삭제에 실패하였습니다.");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void GridRecord_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            btnDelete.Enabled = true;
            lbl_storage_gubun.Text = "1";
            txt_storage_cd.Enabled = false;
            txt_storage_cd.Text = GridRecord.Rows[e.RowIndex].Cells[0].Value.ToString();
            txt_storage_nm.Text = GridRecord.Rows[e.RowIndex].Cells[1].Value.ToString();
            
        }


        private void loc_list()
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_loc_list();
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.locGrid.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        locGrid.Rows[i].Cells[0].Value = dt.Rows[i]["LOC_CD"].ToString();
                        locGrid.Rows[i].Cells[1].Value = dt.Rows[i]["LOC_NM"].ToString();
                        locGrid.Rows[i].Cells[2].Value = dt.Rows[i]["STORAGE_NM"].ToString();
                        locGrid.Rows[i].Cells[3].Value = dt.Rows[i]["COMMENT"].ToString();
                        locGrid.Rows[i].Cells[4].Value = dt.Rows[i]["STORAGE_CD"].ToString();
                    }
                }
                else
                {
                    locGrid.Rows.Clear();
                }
            }
            catch (Exception e)
            {

            }
        }

        private void loc_del()
        {
            wnDm wDm = new wnDm();
            int rsNum = wDm.deleteLoc(txt_loc_cd.Text.ToString());
            if (rsNum == 0)
            {
                txt_loc_cd.Enabled = true;
                lbl_loc_gubun.Text = "";
                txt_loc_cd.Text = "";
                txt_loc_nm.Text = "";
                txt_loc_cmt.Text = "";
                cmb_storage.SelectedIndex = 0;


                loc_list();
                MessageBox.Show("성공적으로 삭제하였습니다.");
            }
            else if (rsNum == 1)
            {
                MessageBox.Show("삭제에 실패하였습니다.");
            }
        }
        private void loc_logic()
        {
            if (txt_loc_cd.Text.ToString().Equals(""))
            {
                MessageBox.Show("LOC코드를 입력하시기 바랍니다.");
                return;
            }
            if (txt_loc_nm.Text.ToString().Equals(""))
            {
                MessageBox.Show("LOC명을 입력하시기 바랍니다.");
                return;
            }
            if (lbl_loc_gubun.Text != "1")
            {
                wnDm wDm = new wnDm();
                int rsNum = wDm.insertLoc(txt_loc_cd.Text.ToString(),
                    txt_loc_nm.Text.ToString(),
                    cmb_storage.SelectedValue.ToString(),
                    txt_loc_cmt.Text.ToString());

                if (rsNum == 0)
                {
                    txt_loc_cd.Enabled = true;
                    lbl_loc_gubun.Text = "";
                    txt_loc_cd.Text = "";
                    txt_loc_nm.Text = "";
                    txt_loc_cmt.Text = "";
                    cmb_storage.SelectedIndex = 0;


                    loc_list();
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
                int rsNum = wDm.updateLoc(txt_loc_cd.Text.ToString(),
                    txt_loc_nm.Text.ToString(),
                    cmb_storage.SelectedValue.ToString(),
                    txt_loc_cmt.Text.ToString());

                if (rsNum == 0)
                {
                    loc_list();
                    MessageBox.Show("성공적으로 수정하였습니다.");
                }
                else if (rsNum == 1)
                    MessageBox.Show("저장에 실패하였습니다");
                else
                    MessageBox.Show("Exception 에러2");
            }
        }

        private void locGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            btnDelete.Enabled = true;
            lbl_loc_gubun.Text = "1";
            txt_loc_cd.Enabled = false;
            txt_loc_cd.Text = locGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
            txt_loc_nm.Text = locGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
            cmb_storage.SelectedValue = locGrid.Rows[e.RowIndex].Cells[4].Value.ToString();
            txt_loc_cmt.Text = locGrid.Rows[e.RowIndex].Cells[3].Value.ToString();
            
        }

        
    }
}
