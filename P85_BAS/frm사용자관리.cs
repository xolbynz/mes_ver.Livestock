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
    public partial class frm사용자관리 : Form
    {
        private wnGConstant wConst = new wnGConstant();
        private bool bEditText = false;
        private bool bData = false;
        private bool bData_sub = false;
        private int intTotalRecords = 0;
        private int intPageSize = 0;
        private int intPageCount = 0;
        private int intCurrentPage = 1;
        private int nPageSize = int.Parse(Common.p_PageSize);
        public frm사용자관리()
        {
            InitializeComponent();
        }

        private void frm사용자관리_Load(object sender, EventArgs e)
        {
            ComInfo.gridHeaderSet(dataUserGrid);
            ComInfo.gridHeaderSet(dataDeptGrid);
            ComInfo.gridHeaderSet(dataPosGrid);

            btnDelete.Enabled = false;
            init_ComboBox();
            user_list();
            //this.dtp거래개시일.Text = DateTime.Parse(dt.Rows[0]["REG_DATE"].ToString().Trim().Replace(" ", "")).ToString("yyyy-MM-dd");
        }

        private void init_ComboBox() {
            string sqlQuery = "";
            cmb_dept.ValueMember = "코드";
            cmb_dept.DisplayMember = "명칭";
            sqlQuery = " select DEPT_CD as 코드, DEPT_NM as 명칭 from N_DEPT_CODE where 1=1 ";
            wConst.ComboBox_Read_Blank(cmb_dept, sqlQuery);

            cmb_pos.ValueMember = "코드";
            cmb_pos.DisplayMember = "명칭";
            sqlQuery = " select POS_CD as 코드, POS_NM as 명칭 from N_POS_CODE where 1=1 ";
            wConst.ComboBox_Read_Blank(cmb_pos, sqlQuery);

            cmb_stor.ValueMember = "코드";
            cmb_stor.DisplayMember = "명칭";
            sqlQuery = " select STORAGE_CD as 코드, STORAGE_NM as 명칭 from N_STORAGE_CODE where 1=1";
            wConst.ComboBox_Read_Blank(cmb_stor, sqlQuery);
           
            cmb_auth.ValueMember = "코드";
            cmb_auth.DisplayMember = "명칭";

            sqlQuery = "select '1' as 코드,'사용정지' as 명칭";
            sqlQuery += " union all ";
            sqlQuery += "select '2' as 코드,'정보조회' as 명칭";
            sqlQuery += " union all ";
            sqlQuery += "select '3' as 코드,'자료입력' as 명칭";
            sqlQuery += " union all ";
            sqlQuery += "select '4' as 코드,'정보분석' as 명칭";
            sqlQuery += " union all ";
            sqlQuery += "select '5' as 코드,'관리통제' as 명칭";

            wConst.ComboBox_Read_Blank(cmb_auth, sqlQuery);
        }

        #region common logic

        private void btnNew_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            if (tabControl.SelectedIndex == 0)
            { //사용자 등록
                lbl_user_gbn.Text = "";
                txt_user_cd.Text = "";
                txt_user_cd.Enabled = true;
                txt_user_nm.Text = "";
                cmb_dept.SelectedIndex = 0;
                cmb_pos.SelectedIndex = 0;
                cmb_stor.SelectedIndex = 0;
                txt_phone.Text = "";
                txt_login.Text = "";
                txt_pw.Text = "";
                cmb_auth.SelectedIndex = 0;
                txt_user_cmt.Text = "";
            }
            else if (tabControl.SelectedIndex == 1) //부서 등록
            {
                lbl_dept_gbn.Text = "";
                txtDeptCd.Text = "";
                txtDeptCd.Enabled = true;
                txtDeptNm.Text = "";
                txtDeptCmt.Text = "";
            }
            else if (tabControl.SelectedIndex == 2) //직위 등록
            {
                lbl_pos_gbn.Text = "";
                txt_pos_cd.Text = "";
                txt_pos_cd.Enabled = true;
                txt_pos_nm.Text = "";
                txt_pos_cmt.Text = "";
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            { //사용자 등록
                user_del();
            }
            else if (tabControl.SelectedIndex == 1) //부서 등록
            {
                dept_del();
            }
            else if (tabControl.SelectedIndex == 2) //직위 등록
            {
                pos_del();
            }
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            { //사용자 등록
                user_logic();
            }
            else if (tabControl.SelectedIndex == 1) //부서 등록
            {
                dept_logic();
            }
            else if (tabControl.SelectedIndex == 2) //직위 등록
            {
                pos_logic();
            }
            
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                user_list();
            }
            else if (tabControl.SelectedIndex == 1)
            {
                dept_list();
            }
            else if (tabControl.SelectedIndex == 2)
            {
                pos_list();
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion common logic
        
        #region user logic

        private void user_logic()
        {
            if (txt_user_cd.Text.ToString().Equals(""))
            {
                MessageBox.Show("사원코드를 입력하시기 바랍니다.");
                return;
            }
            if (txt_user_nm.Text.ToString().Equals(""))
            {
                MessageBox.Show("사원명을 입력하시기 바랍니다.");
                return;
            }
            if (cmb_dept.SelectedIndex == 0) {
                MessageBox.Show("부서를 선택하시기 바랍니다.");
                return;
            }
            if (cmb_pos.SelectedIndex == 0)
            {
                MessageBox.Show("직위를 선택하시기 바랍니다.");
                return;
            }
            if (cmb_stor.SelectedIndex == 0)
            {
                MessageBox.Show("창고를 선택하시기 바랍니다.");
                return;
            }
            if (cmb_auth.SelectedIndex == 0) {
                MessageBox.Show("권한을 선택하시기 바랍니다.");
                return;
            }

            if (cmb_dept.SelectedValue == null) cmb_dept.SelectedValue = "";
            if (cmb_pos.SelectedValue == null) cmb_pos.SelectedValue = "";
            if (cmb_stor.SelectedValue == null) cmb_stor.SelectedValue = "";
            if (cmb_auth.SelectedValue == null) cmb_auth.SelectedValue = "";

            if (lbl_user_gbn.Text != "1")
            {
                wnDm wDm = new wnDm();
                int rsNum = wDm.insertStaff(txt_user_cd.Text.ToString()
                    , txt_user_nm.Text.ToString()
                    , cmb_dept.SelectedValue.ToString()
                    , cmb_pos.SelectedValue.ToString()
                    , cmb_stor.SelectedValue.ToString()
                    , input_date.Text.ToString()
                    , txt_phone.Text.ToString()
                    , txt_login.Text.ToString()
                    , txt_pw.Text.ToString()
                    , cmb_auth.SelectedValue.ToString()
                    , txt_user_cmt.Text.ToString());

                if (rsNum == 0)
                {
                    txt_user_cd.Text = "";
                    txt_user_nm.Text = "";
                    cmb_dept.SelectedIndex = 0;
                    cmb_pos.SelectedIndex = 0;
                    cmb_stor.SelectedIndex = 0;
                    txt_phone.Text = "";
                    txt_login.Text = "";
                    txt_pw.Text = "";
                    cmb_auth.SelectedIndex = 0;
                    txt_user_cmt.Text = "";

                    user_list();
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
                int rsNum = wDm.updateStaff(txt_user_cd.Text.ToString()
                    , txt_user_nm.Text.ToString()
                    , cmb_dept.SelectedValue.ToString()
                    , cmb_pos.SelectedValue.ToString()
                    , cmb_stor.SelectedValue.ToString()
                    , input_date.Text.ToString()
                    , txt_phone.Text.ToString()
                    , txt_login.Text.ToString()
                    , txt_pw.Text.ToString()
                    , cmb_auth.SelectedValue.ToString()
                    , txt_user_cmt.Text.ToString());

                if (rsNum == 0)
                {
                    user_list();
                    MessageBox.Show("성공적으로 수정하였습니다.");
                }
                else if (rsNum == 1)
                    MessageBox.Show("저장에 실패하였습니다");
                else
                    MessageBox.Show("Exception 에러");
            }

        }
        private void user_list()
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Staff_List("");

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.dataUserGrid.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataUserGrid.Rows[i].Cells[0].Value = dt.Rows[i]["STAFF_CD"].ToString();
                        dataUserGrid.Rows[i].Cells[1].Value = dt.Rows[i]["STAFF_NM"].ToString();
                        dataUserGrid.Rows[i].Cells[2].Value = dt.Rows[i]["DEPT_NM"].ToString();
                        dataUserGrid.Rows[i].Cells[3].Value = dt.Rows[i]["POS_NM"].ToString();
                        dataUserGrid.Rows[i].Cells[4].Value = dt.Rows[i]["COMMENT"].ToString();
                    }
                }
                else
                {
                    dataUserGrid.Rows.Clear();
                }
            }
            catch (Exception e)
            {

            }
        }

        private void user_del()
        {
            wnDm wDm = new wnDm();
            int rsNum = wDm.deleteStaff(txt_user_cd.Text.ToString());
            if (rsNum == 0)
            {
                lbl_user_gbn.Text = "";
                btnDelete.Enabled = false;
                txt_user_cd.Enabled = true;
                txt_user_cd.Text = "";
                txt_user_nm.Text = "";
                cmb_dept.SelectedIndex = 0;
                cmb_pos.SelectedIndex = 0;
                cmb_stor.SelectedIndex = 0;
                txt_phone.Text = "";
                txt_login.Text = "";
                txt_pw.Text = "";
                cmb_auth.SelectedIndex = 0;
                txt_user_cmt.Text = "";
                user_list();
                MessageBox.Show("성공적으로 삭제하였습니다.");
            }
            else if (rsNum == 1)
                MessageBox.Show("삭제에 실패하였습니다");
            else
                MessageBox.Show("Exception 에러");
        }

        private void dataUserGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            btnDelete.Enabled = true;
            lbl_user_gbn.Text = "1";
            txt_user_cd.Enabled = false;

            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                string condition = "where staff_cd = '" + dataUserGrid.Rows[e.RowIndex].Cells[0].Value.ToString() + "'";
                dt = wDm.fn_Staff_List(condition);

                if (dt != null && dt.Rows.Count > 0)
                {

                    txt_user_cd.Text = dt.Rows[0]["STAFF_CD"].ToString();
                    txt_user_nm.Text = dt.Rows[0]["STAFF_NM"].ToString();
                    cmb_dept.SelectedValue = dt.Rows[0]["DEPT_CD"].ToString();
                    cmb_pos.SelectedValue = dt.Rows[0]["POS_CD"].ToString();
                    cmb_stor.SelectedValue = dt.Rows[0]["STORAGE_CD"].ToString();
                    txt_phone.Text = dt.Rows[0]["PHONE_NUM"].ToString();
                    txt_login.Text = dt.Rows[0]["LOGIN_ID"].ToString();
                    txt_pw.Text = dt.Rows[0]["PW"].ToString();
                    cmb_auth.SelectedValue = int.Parse(dt.Rows[0]["AUTH_SET"].ToString());
                    txt_user_cmt.Text = dt.Rows[0]["COMMENT"].ToString();
                }
                else
                {
                    //MessageBox.Show("존재하지 않는 자료입니다.");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txt_user_cd_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e);
        }

        #endregion user logic

        #region dept logic

        private void dept_logic() { 
            if(txtDeptCd.Text.ToString().Equals("")){
                MessageBox.Show("부서코드를 입력하시기 바랍니다.");
                return;
            }          
            if(txtDeptNm.Text.ToString().Equals("")){
                MessageBox.Show("부서명을 입력하시기 바랍니다.");
                return;
            }
            if (lbl_dept_gbn.Text != "1")
            {
                wnDm wDm = new wnDm();
                int rsNum = wDm.insertDept(txtDeptCd.Text.ToString(), txtDeptNm.Text.ToString(), txtDeptCmt.Text.ToString());

                if (rsNum == 0)
                {
                    txtDeptCd.Text = "";
                    txtDeptNm.Text = "";
                    txtDeptCmt.Text = "";

                    dept_list();
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
            else {
                wnDm wDm = new wnDm();
                int rsNum = wDm.updateDept(txtDeptCd.Text.ToString(), txtDeptNm.Text.ToString(), txtDeptCmt.Text.ToString());
                if (rsNum == 0)
                {
                    dept_list();
                    MessageBox.Show("성공적으로 수정하였습니다.");
                }
                else if (rsNum == 1)
                    MessageBox.Show("저장에 실패하였습니다");
                else
                    MessageBox.Show("Exception 에러");   
            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
        }

        private void dept_del() {
            wnDm wDm = new wnDm();
            int rsNum = wDm.deleteDept(txtDeptCd.Text.ToString());
            if (rsNum == 0)
            {
                btnDelete.Enabled = false;
                txtDeptCd.Enabled = true;
                txtDeptCd.Text = "";
                txtDeptNm.Text = "";
                txtDeptCmt.Text = "";
                lbl_dept_gbn.Text = "";

                dept_list();
                MessageBox.Show("성공적으로 삭제하였습니다.");
            }
            else if (rsNum == 1)
                MessageBox.Show("삭제에 실패하였습니다");
            else
                MessageBox.Show("Exception 에러");   
        }
        private void dept_list()
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Dept_List();

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.dataDeptGrid.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataDeptGrid.Rows[i].Cells[0].Value = dt.Rows[i]["DEPT_CD"].ToString();
                        dataDeptGrid.Rows[i].Cells[1].Value = dt.Rows[i]["DEPT_NM"].ToString();
                        dataDeptGrid.Rows[i].Cells[2].Value = dt.Rows[i]["COMMENT"].ToString();
                    }
                }
                else
                {
                    dataDeptGrid.Rows.Clear();
                }
            }
            catch (Exception e)
            {

            }
        }

        private void dataDeptGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            btnDelete.Enabled = true;
            lbl_dept_gbn.Text = "1";
            txtDeptCd.Enabled = false;
            txtDeptCd.Text = dataDeptGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtDeptNm.Text = dataDeptGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtDeptCmt.Text = dataDeptGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void txtDeptCd_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e);
        }

        #endregion dept logic

        #region pos logic
        private void pos_logic()
        {
            if (txt_pos_cd.Text.ToString().Equals(""))
            {
                MessageBox.Show("직위코드를 입력하시기 바랍니다.");
                return;
            }
            if (txt_pos_nm.Text.ToString().Equals(""))
            {
                MessageBox.Show("직위명을 입력하시기 바랍니다.");
                return;
            }

            if (lbl_pos_gbn.Text != "1")
            {
                wnDm wDm = new wnDm();
                int rsNum = wDm.insertPos(txt_pos_cd.Text.ToString(), txt_pos_nm.Text.ToString(), txt_pos_cmt.Text.ToString());

                if (rsNum == 0)
                {
                    txt_pos_cd.Text = "";
                    txt_pos_nm.Text = "";
                    txt_pos_cmt.Text = "";

                    pos_list();
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
            else {
                wnDm wDm = new wnDm();
                int rsNum = wDm.updatePos(txt_pos_cd.Text.ToString(), txt_pos_nm.Text.ToString(), txt_pos_cmt.Text.ToString());
                if (rsNum == 0)
                {
                    pos_list();
                    MessageBox.Show("성공적으로 수정하였습니다.");
                }
                else if (rsNum == 1)
                    MessageBox.Show("저장에 실패하였습니다");
                else
                    MessageBox.Show("Exception 에러2");   
            }
        }
        private void pos_del() {
            wnDm wDm = new wnDm();
            int rsNum = wDm.deletePos(txt_pos_cd.Text.ToString());
            if (rsNum == 0)
            {
                lbl_pos_gbn.Text = "";
                txt_pos_cd.Enabled = true;
                txt_pos_cd.Text = "";
                txt_pos_nm.Text = "";
                txt_pos_cmt.Text = "";
                btnDelete.Enabled = false;
                pos_list();
                MessageBox.Show("성공적으로 삭제하였습니다.");
            }
            else if (rsNum == 1)
                MessageBox.Show("삭제에 실패하였습니다");
            else
                MessageBox.Show("Exception 에러");   
        }
        private void pos_list()
        {
            try
            {
                wnDm wDm = new wnDm();
                DataTable dt = null;
                dt = wDm.fn_Pos_List();

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.dataPosGrid.RowCount = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataPosGrid.Rows[i].Cells[0].Value = dt.Rows[i]["POS_CD"].ToString();
                        dataPosGrid.Rows[i].Cells[1].Value = dt.Rows[i]["POS_NM"].ToString();
                        dataPosGrid.Rows[i].Cells[2].Value = dt.Rows[i]["COMMENT"].ToString();
                    }
                }
                else
                {
                    dataPosGrid.Rows.Clear();
                }
            }
            catch (Exception e)
            {

            }
        }
        private void dataPosGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            btnDelete.Enabled = true;
            lbl_pos_gbn.Text = "1";
            txt_pos_cd.Enabled = false;
            txt_pos_cd.Text = dataPosGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
            txt_pos_nm.Text = dataPosGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
            txt_pos_cmt.Text = dataPosGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
        private void txt_pos_cd_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e);
        }
        #endregion pos logic

        #region storage logic
        

       
        private void conTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComInfo.onlyNum(sender, e); 
        }

        
        #endregion storage logic
    }
}
